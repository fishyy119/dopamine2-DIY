using Digimezzo.Foundation.Core.Utils;
using Dopamine.Core.Base;
using Dopamine.Core.Extensions;
using System;
using System.Globalization;
using System.Text;
using ToolGood.Words.FirstPinyin;
using MyNihongo.KanaConverter;
using MyNihongo.KanaDetector;

namespace Dopamine.Core.Utils
{
    public static class FormatUtils
    {
        public static string FormatDuration(long duration)
        {
            var sb = new StringBuilder();

            TimeSpan ts = TimeSpan.FromMilliseconds(duration);

            if (ts.Days > 0)
            {
                return string.Concat(string.Format("{0:n1}", ts.TotalDays), " ", ts.TotalDays < 1.1 ? ResourceUtils.GetString("Language_Day") : ResourceUtils.GetString("Language_Days"));
            }

            if (ts.Hours > 0)
            {
                return string.Concat(string.Format("{0:n1}", ts.TotalHours), " ", ts.TotalHours < 1.1 ? ResourceUtils.GetString("Language_Hour") : ResourceUtils.GetString("Language_Hours"));
            }

            if (ts.Minutes > 0)
            {
                sb.Append(string.Concat(ts.ToString("%m"), " ", ts.Minutes == 1 ? ResourceUtils.GetString("Language_Minute") : ResourceUtils.GetString("Language_Minutes"), " "));
            }

            if (ts.Seconds > 0)
            {
                sb.Append(string.Concat(ts.ToString("%s"), " ", ts.Seconds == 1 ? ResourceUtils.GetString("Language_Second") : ResourceUtils.GetString("Language_Seconds")));
            }

            return sb.ToString();
        }

        public static string FormatTime(TimeSpan ts)
        {
            if (ts.Hours > 0)
            {
                return ts.ToString("hh\\:mm\\:ss");
            }
            else
            {
                return ts.ToString("m\\:ss");
            }
        }

        public static string FormatFileSize(long sizeInBytes, bool showByteSize = true)
        {

            string humanReadableSize = string.Empty;

            if (sizeInBytes >= Constants.GigaByteInBytes)
            {
                humanReadableSize = string.Format("{0:#.#} {1}", (double)sizeInBytes / Constants.GigaByteInBytes, ResourceUtils.GetString("Language_Gigabytes_Short"));
            }
            else if (sizeInBytes >= Constants.MegaByteInBytes)
            {
                humanReadableSize = string.Format("{0:#.#} {1}", (double)sizeInBytes / Constants.MegaByteInBytes, ResourceUtils.GetString("Language_Megabytes_Short"));
            }
            else if (sizeInBytes >= Constants.KiloByteInBytes)
            {
                humanReadableSize = string.Format("{0:#.#} {1}", (double)sizeInBytes / Constants.KiloByteInBytes, ResourceUtils.GetString("Language_Kilobytes_Short"));
            }

            NumberFormatInfo nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            nfi.NumberGroupSeparator = " ";

            if (showByteSize)
            {
                return string.Format("{0} ({1} {2})", humanReadableSize, sizeInBytes.ToString("#,#", nfi), ResourceUtils.GetString("Language_Bytes").ToLower());
            }
            else
            {
                return string.Format("{0}", humanReadableSize);
            }
        }

        public static bool ParseLyricsTime(string input, out TimeSpan result)
        {
            try
            {
                var split = input.Split(':');
                if (split.Length == 0)
                {
                    result = new TimeSpan();
                    return false;
                }
                int minutes = Convert.ToInt32(split[0]);
                string secondsAndMilliseconds = split[1];

                split = secondsAndMilliseconds.Split('.');
                int seconds = Convert.ToInt32(split[0]);
                int milliseconds = split.Length == 1 ? 0 : Convert.ToInt32(split[1]);

                result = TimeSpan.FromMilliseconds(minutes * 60000 + seconds * 1000 + milliseconds);
                return true;
            }
            catch (Exception)
            {
            }

            result = new TimeSpan();
            return false;
        }

        public static string GetSortableString(string originalString, bool removePrefix = false)
        {
            if (string.IsNullOrEmpty(originalString)) return string.Empty;

            string trimmed = originalString.ToLower().Trim();

            if (removePrefix)
            {
                try
                {
                    trimmed = trimmed.TrimStart("the ").Trim();
                }
                catch (Exception)
                {
                    // Swallow
                }
            }

            // 初始化返回字符串为小写
            string returnString = trimmed.ToLower();

            if (returnString.Length == 0)
                return returnString;

            string firstElement = new StringInfo(originalString).SubstringByTextElements(0, 1);
            string prefix = string.Empty;

            // 判断是否为汉字
            if (WordsHelper.IsAllChinese(firstElement))
            {
                prefix = WordsHelper.GetFirstPinyin(firstElement).ToLower();
            }
            // 判断是否为日语假名（平假名/片假名）
            else if (firstElement.IsKana())
            {
                string romaji = firstElement.ToRomaji();
                prefix = !string.IsNullOrEmpty(romaji) ? romaji.Substring(0, 1).ToLower() : firstElement.ToLower();
            }

            return prefix + returnString;

        }

        public static string TrimValue(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return value.Trim();
            }
            else
            {
                return string.Empty;
            }
        }

        public static string DelimitValue(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return $"{Constants.ColumnValueDelimiter}{value.Trim()}{Constants.ColumnValueDelimiter}";
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
