using System;
using System.Globalization;
using System.Text;

namespace Shipwreck.FfmpegUtil
{
    internal static class StringBuilderHelper
    {
        public static StringBuilder TrimEnd(this StringBuilder b)
        {
            if (b.Length > 0 && b[b.Length - 1] == ' ')
            {
                b.Length--;
            }
            return b;
        }

        public static StringBuilder AppendIf(this StringBuilder b, string key, long value)
        {
            if (value != 0)
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