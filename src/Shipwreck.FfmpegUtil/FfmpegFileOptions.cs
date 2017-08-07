using System;
using System.Text;

namespace Shipwreck.FfmpegUtil
{
    public abstract class FfmpegFileOptions : BufferObject
    {
        internal FfmpegFileOptions()
        {
        }

        public string FilePath
        {
            get => GetString();
            set => SetValue(value);
        }

        public TimeSpan Duration
        {
            get => GetTimeSpan();
            set => SetValue(value);
        }

        public TimeSpan SeekTo
        {
            get => GetTimeSpan();
            set => SetValue(value);
        }

        public TimeSpan SeekToLast
        {
            get => GetTimeSpan();
            set => SetValue(value);
        }

        internal abstract void AppendArgs(StringBuilder builder);

        public override string ToString()
        {
            var sb = new StringBuilder();

            AppendArgs(sb);

            sb.TrimEnd();
            return sb.ToString();
        }
    }
}