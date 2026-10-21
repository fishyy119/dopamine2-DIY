using Digimezzo.Foundation.Core.Logging;
using Dopamine.Core.Search;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Dopamine.Services.Search
{
    internal sealed class SearchProjectionService : IDisposable
    {
        private sealed class Entry
        {
            public string Text;
            public int Generation;
            public SearchTextProjection Projection;
            public readonly TaskCompletionSource<bool> Completion =
                new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        }

        private readonly object gate = new object();
        private readonly Dictionary<string, Entry> entries = new Dictionary<string, Entry>(StringComparer.Ordinal);
        private readonly Queue<Entry> queue = new Queue<Entry>();
        private readonly IReadingConverter converter;
        private Task worker;
        private Task shutdown;
        private bool disposed;
        private int generation;

        public SearchProjectionService(IReadingConverter converter)
        {
            this.converter = converter;
        }

        public event Action ProjectionsChanged = delegate { };

        private Entry GetOrQueueEntry(string text)
        {
            text = text ?? string.Empty;
            lock (this.gate)
            {
                if (this.entries.TryGetValue(text, out Entry existing)) return existing;
                var entry = new Entry
                {
                    Text = text,
                    Generation = this.generation
                };
                if (this.disposed)
                {
                    entry.Completion.TrySetResult(false);
                    return entry;
                }
                this.entries.Add(text, entry);
                this.queue.Enqueue(entry);
                if (this.worker == null) this.worker = Task.Run(this.ProcessQueue);
                return entry;
            }
        }

        // 返回已生成的投影；尚无结果时返回 false，并安排后台准备。
        public bool TryGetOrQueue(string text, out SearchTextProjection projection)
        {
            Entry entry = this.GetOrQueueEntry(text);
            lock (this.gate)
            {
                projection = entry.Projection;
                return projection != null;
            }
        }

        public Task PrepareAsync(IEnumerable<string> fields)
        {
            var tasks = new List<Task>();
            if (fields == null) return Task.CompletedTask;
            foreach (string field in fields) tasks.Add(this.GetOrQueueEntry(field).Completion.Task);
            return Task.WhenAll(tasks);
        }

        private void ProcessQueue()
        {
            int changed = 0;
            while (true)
            {
                Entry entry;
                lock (this.gate)
                {
                    if (this.disposed) return;
                    if (this.queue.Count == 0)
                    {
                        // 与入队共用同一把锁；退出后不再访问转换器，新工作可以启动新任务。
                        this.worker = null;
                        return;
                    }
                    entry = this.queue.Dequeue();
                }
                SearchTextProjection projection = null;
                try
                {
                    projection = this.converter.Convert(entry.Text);
                }
                catch (Exception ex)
                {
                    LogClient.Error("Could not prepare search reading. Exception: {0}", ex.Message);
                }
                bool notify;
                lock (this.gate)
                {
                    if (!this.disposed && entry.Generation == this.generation && projection != null)
                    {
                        entry.Projection = projection;
                        changed++;
                    }
                    entry.Completion.TrySetResult(projection != null);
                    notify = !this.disposed && changed > 0 && (changed >= 64 || this.queue.Count == 0);
                    if (notify) changed = 0;
                }
                if (notify) this.NotifyChanged();
            }
        }

        private void NotifyChanged()
        {
            // 订阅者不在工作任务内执行，避免关闭时相互等待；接收方检查自身是否已关闭。
            ThreadPool.QueueUserWorkItem(_ => this.ProjectionsChanged());
        }

        public void Clear()
        {
            lock (this.gate)
            {
                this.ClearEntries();
            }
        }

        // 调用方持有 gate 锁。
        private void ClearEntries()
        {
            this.generation++;
            foreach (Entry entry in this.entries.Values) entry.Completion.TrySetResult(false);
            this.entries.Clear();
            this.queue.Clear();
        }

        public void Dispose()
        {
            Task stopping;
            lock (this.gate)
            {
                if (!this.disposed)
                {
                    this.disposed = true;
                    this.ClearEntries();
                    this.ProjectionsChanged = delegate { };
                    // 禁止新工作后，在当前任务结束后释放转换器；空闲时直接安排释放。
                    this.shutdown = (this.worker ?? Task.CompletedTask).ContinueWith(
                        _ => this.converter.Dispose(), TaskScheduler.Default);
                }
                stopping = this.shutdown;
            }
            // 等待期间不持锁；重复关闭也等待同一个任务完成。
            stopping.GetAwaiter().GetResult();
        }
    }
}
