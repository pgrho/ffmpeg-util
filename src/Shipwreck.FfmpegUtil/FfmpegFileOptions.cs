using System;
using System.Text;

namespace Shipwreck.FfmpegUtil
{
    public abstract class FfmpegFileOptions
    {
        public string FilePath { get; set; }

        public TimeSpan Duration { get; set; }
        public TimeSpan SeekTo { get; set; }
        public TimeSpan SeekToLast { get; set; }

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