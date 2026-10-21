using System;
using System.Linq;
using System.Text;

namespace Dopamine.Core.Search
{
    public static class SearchText
    {
        // 统一字符形式、平片假名和大小写，便于搜索匹配。
        public static string Normalize(string text)
        {
            text = NormalizeWidth(text);
            var result = new StringBuilder(text.Length);
            foreach (char c in text)
            {
                // 将片假名统一为平假名，保留长音符号。
                if ((c >= '\u30a1' && c <= '\u30f6') || c == '\u30fd' || c == '\u30fe')
                    result.Append((char)(c - 0x60));
                else if (c >= '\u30f7' && c <= '\u30fa')
                    result.Append((char)(c - 0x68)).Append('\u3099');
                else
                    result.Append(c);
            }
            return result.ToString().ToLowerInvariant();
        }

        // 使用 NFKC 统一全半角及其他兼容字符形式。
        public static string NormalizeWidth(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            // 元数据可能包含孤立的代理项，保留原文以便搜索。
            try { return text.Normalize(NormalizationForm.FormKC); }
            catch (ArgumentException) { return text; }
        }

        // 移除所有空白字符，支持连续输入拼音或罗马音。
        public static string Compact(string text)
        {
            return new string(text.Where(c => !char.IsWhiteSpace(c)).ToArray());
        }
    }
}
