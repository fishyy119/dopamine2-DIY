using Dopamine.Core.Base;
using Dopamine.Core.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Dopamine.Services.Search
{
    public sealed class SearchService : ISearchService
    {
        private readonly object gate = new object();
        private readonly SearchProjectionService projections;
        private readonly SynchronizationContext context;
        private readonly Timer searchTimer;
        private string searchText = string.Empty;
        private string[] terms = new string[0];
        private DateTime refreshAt;
        private bool refreshPending;
        private bool disposed;

        public SearchService(IReadingConverter converter)
        {
            this.projections = new SearchProjectionService(converter);
            this.context = new DispatcherSynchronizationContext(Application.Current.Dispatcher);
            this.searchTimer = new Timer(this.SearchTimeoutHandler, null, Timeout.Infinite, Timeout.Infinite);
            this.projections.ProjectionsChanged += this.ProjectionsChanged;
        }

        public string SearchText
        {
            // Make sure we never return null
            get { lock (this.gate) return this.searchText; }
            set
            {
                string text = value ?? string.Empty;
                string[] parsed = Core.Search.SearchText.Normalize(text).Trim()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                lock (this.gate)
                {
                    if (this.disposed) return;
                    bool isTextChanged = !this.terms.SequenceEqual(parsed);
                    this.searchText = text;
                    this.terms = parsed;
                    // Only trigger a search if the text has changed
                    if (isTextChanged) this.ScheduleRefresh(TimeSpan.FromSeconds(Constants.SearchTimeoutSeconds));
                }
            }
        }

        public event Action<string> DoSearch = delegate { };

        public bool IsMatch(params string[] fields)
        {
            string[] query;
            lock (this.gate) query = this.terms;
            if (query.Length == 0) return true;
            var candidates = new List<SearchTextProjection>();
            var originals = new List<string>();
            foreach (string field in fields ?? new string[0])
            {
                if (this.projections.TryGetOrQueue(field, out SearchTextProjection projection))
                    candidates.Add(projection);
                else
                    // 读音尚未生成时直接匹配规范化原文，不创建占位投影。
                    originals.Add(Core.Search.SearchText.Normalize(field));
            }
            return query.All(term =>
                originals.Any(original => original.IndexOf(term, StringComparison.Ordinal) >= 0) ||
                candidates.Any(candidate => candidate.Contains(term)));
        }

        public Task PrepareAsync(IEnumerable<string> fields)
        {
            return this.projections.PrepareAsync(fields);
        }

        public void ClearCache()
        {
            this.projections.Clear();
            this.ProjectionsChanged();
        }

        private void ScheduleRefresh(TimeSpan delay)
        {
            this.refreshPending = true;
            this.refreshAt = DateTime.UtcNow.Add(delay);
            this.searchTimer.Change(delay, Timeout.InfiniteTimeSpan);
        }

        private void ProjectionsChanged()
        {
            lock (this.gate)
            {
                // 合并投影完成通知，不延长当前查询的防抖等待时间。
                if (!this.disposed && this.terms.Length > 0 && !this.refreshPending)
                    this.ScheduleRefresh(TimeSpan.FromMilliseconds(100));
            }
        }

        private void SearchTimeoutHandler(object state)
        {
            lock (this.gate)
            {
                if (this.disposed || !this.refreshPending) return;
                TimeSpan remaining = this.refreshAt - DateTime.UtcNow;
                if (remaining > TimeSpan.Zero)
                {
                    this.searchTimer.Change(remaining, Timeout.InfiniteTimeSpan);
                    return;
                }
                this.refreshPending = false;
            }
            this.context.Post(_ => this.PublishSearch(), null);
        }

        private void PublishSearch()
        {
            string current;
            lock (this.gate)
            {
                // 旧输入排队的回调不能绕过新输入的防抖等待。
                if (this.disposed || this.refreshPending) return;
                current = this.searchText;
            }
            this.DoSearch(current);
        }

        public void Dispose()
        {
            lock (this.gate)
            {
                if (!this.disposed)
                {
                    this.disposed = true;
                    this.searchTimer.Dispose();
                    this.projections.ProjectionsChanged -= this.ProjectionsChanged;
                    this.DoSearch = delegate { };
                }
            }
            // 先停止搜索通知，再在锁外等待内部投影组件释放完成。
            this.projections.Dispose();
        }
    }
}
