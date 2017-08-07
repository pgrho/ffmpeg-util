using System;
using System.Globalization;
using System.Text;

namespace Shipwreck.FfmpegUtil
{
    internal static partial class StringBuilderHelper
    {
        public static StringBuilder AppendFilePath(this StringBuilder b, string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                b.Append('"');
                b.Append(filePath);
                b.Append("\" ");
            }
            return b;
        }

        public static StringBuilder TrimEnd(this StringBuilder b, char @char = ' ')
        {
            while (b.Length > 0 && b[b.Length - 1] == @char)
            {
                b.Length--;
            }
            return b;
        }

        #region AppendIf

        public static StringBuilder AppendIf(this StringBuilder b, string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                b.Append(key).Append(' ').Append(value).Append(' ');
            }
            return b;
        }

        public static StringBuilder AppendIf(this StringBuilder b, string key, DateTime date)
        {
            if (date != DateTime.MinValue)
            {
                b.Append(key).Append(' ').Append(date.ToString("yyyyMMddTHHmmss.fff", DateTimeFormatInfo.InvariantInfo)).Append(' ');
            }
            return b;
        }

        public static StringBuilder AppendIf(this StringBuilder b, string key, TimeSpan time)
        {
            if (time != TimeSpan.Zero)
            {
                b.Append(key).Append(' ').Append(time.TotalSeconds.ToString("0.###")).Append(' ');
            }
            return b;
        }

        public static StringBuilder AppendIf(this StringBuilder b, string value, bool condition)
        {
            if (condition)
            {
                b.Append(value).Append(' ');
            }
            return b;
        }

        #endregion AppendIf

        public static StringBuilder AppendIfStream(this StringBuilder b, string key, string streamSpecifier, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                b.Append(key);
                if (!string.IsNullOrEmpty(streamSpecifier))
                {
                    b.Append(':');
                    b.Append(streamSpecifier);
                }

                b.Append(' ');
                b.Append(value);
                b.Append(' ');
            }

            return b;
        }
    }
}