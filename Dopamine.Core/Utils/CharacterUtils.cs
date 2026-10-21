using MyNihongo.KanaConverter;
using System.Text;

namespace Dopamine.Core.Utils
{
    public static class CharacterUtils
    {
        public static bool IsHan(int codePoint)
        {
            return codePoint == 0x3007 ||
                (codePoint >= 0x3400 && codePoint <= 0x4dbf) ||
                (codePoint >= 0x4e00 && codePoint <= 0x9fff) ||
                (codePoint >= 0xf900 && codePoint <= 0xfaff) ||
                (codePoint >= 0x20000 && codePoint <= 0x2ee5f) ||
                (codePoint >= 0x2f800 && codePoint <= 0x2fa1f) ||
                (codePoint >= 0x30000 && codePoint <= 0x3347f);
        }

        public static bool ContainsHan(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                int codePoint = text[i];
                if (char.IsHighSurrogate(text[i]) && i + 1 < text.Length && char.IsLowSurrogate(text[i + 1]))
                {
                    codePoint = char.ConvertToUtf32(text[i], text[++i]);
                }
                if (IsHan(codePoint)) return true;
            }
            return false;
        }

        // 按 Unicode 码点提取汉字，保留扩展区汉字的完整代理对。
        public static string ExtractHan(string text)
        {
            var result = new StringBuilder();
            if (string.IsNullOrEmpty(text)) return string.Empty;
            for (int position = 0; position < text.Length;)
            {
                int length = char.IsHighSurrogate(text[position]) && position + 1 < text.Length &&
                    char.IsLowSurrogate(text[position + 1]) ? 2 : 1;
                int codePoint = length == 2 ? char.ConvertToUtf32(text, position) : text[position];
                if (IsHan(codePoint)) result.Append(text, position, length);
                position += length;
            }
            return result.ToString();
        }

        public static bool IsKana(char c)
        {
            // 与 KanaDetector 1.0.3 的字符检测范围一致，包含中点和长音符。
            return (c >= '\u3041' && c <= '\u3096') ||
                (c >= '\u30a1' && c <= '\u30fc');
        }

        public static bool IsKana(string text)
        {
            // 检查整个文本元素，避免漏掉组合字符中的非假名部分。
            if (string.IsNullOrEmpty(text)) return false;
            foreach (char c in text)
            {
                if (!IsKana(c)) return false;
            }
            return true;
        }

        public static string RomanizeKana(string text)
        {
            // 仅转换连续的假名片段，避免转换库丢弃标点。
            // 中点虽参与假名检测，转换时仍按标点原样保留。
            var result = new StringBuilder();
            int position = 0;
            while (position < text.Length)
            {
                if (text[position] == '\u30fb' || !IsKana(text[position]))
                {
                    result.Append(text[position++]);
                    continue;
                }
                int start = position++;
                while (position < text.Length && text[position] != '\u30fb' && IsKana(text[position])) position++;
                string kana = text.Substring(start, position - start);
                result.Append(kana.ToRomaji(UnrecognisedCharacterPolicy.Append));
            }
            return result.ToString();
        }
    }
}
