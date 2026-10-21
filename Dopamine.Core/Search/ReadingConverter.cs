using Digimezzo.Foundation.Core.Logging;
using Dopamine.Core.Utils;
using EzPinyin;
using NMeCab.Specialized;
using System;
using System.Linq;
using System.Text;

namespace Dopamine.Core.Search
{
    // 由投影服务的串行后台任务调用。
    public sealed class ReadingConverter : IReadingConverter
    {
        private readonly string dictionaryPath;
        private readonly Action<string> reportError;
        private MeCabIpaDicTagger tagger;
        private bool initializationAttempted;
        private bool disposed;

        public ReadingConverter(string dictionaryPath, Action<string> reportError = null)
        {
            this.dictionaryPath = dictionaryPath;
            this.reportError = reportError ?? (message => LogClient.Error(message));
        }

        public SearchTextProjection Convert(string text)
        {
            if (this.disposed) throw new ObjectDisposedException(nameof(ReadingConverter));

            string normalized = SearchText.NormalizeWidth(text);
            string hanText = CharacterUtils.ExtractHan(normalized);
            bool hasHan = hanText.Length > 0;
            if (!hasHan && !normalized.Any(CharacterUtils.IsKana))
                return new SearchTextProjection(text);

            //* 1.仅将汉字部分转换为拼音
            string pinyin = hasHan ? PinyinHelper.GetPinyin(hanText, string.Empty) : string.Empty;

            //* 2.尝试整体转换为日语罗马音
            string reading = normalized;
            if (hasHan && this.TryInitialize())
            {
                var readings = new StringBuilder();
                // 直接拼接词元读音，不回补 MeCab 省略的空白。
                foreach (MeCabIpaDicNode node in this.tagger.Parse(normalized))
                {
                    bool keepSurface = !CharacterUtils.ContainsHan(node.Surface) && !node.Surface.Any(CharacterUtils.IsKana);
                    string kana = keepSurface || IsMissing(node.Reading) ? node.Surface : node.Reading;
                    readings.Append(kana);
                }
                reading = readings.ToString();
            }
            string romaji = CharacterUtils.RomanizeKana(reading);

            return hasHan
                ? new SearchTextProjection(text, pinyin, romaji)
                : new SearchTextProjection(text, romaji);
        }

        private static bool IsMissing(string value)
        {
            return string.IsNullOrEmpty(value) || value == "*";
        }

        private bool TryInitialize()
        {
            if (!this.initializationAttempted)
            {
                this.initializationAttempted = true;
                try
                {
                    this.tagger = MeCabIpaDicTagger.Create(this.dictionaryPath);
                }
                catch (Exception ex)
                {
                    this.reportError($"Japanese search dictionary could not be loaded. Exception: {ex.Message}");
                }
            }
            return this.tagger != null;
        }

        public void Dispose()
        {
            if (this.disposed) return;
            this.disposed = true;
            this.tagger?.Dispose();
        }
    }
}
