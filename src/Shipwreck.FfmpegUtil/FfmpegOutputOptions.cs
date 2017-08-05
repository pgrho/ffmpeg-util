using System;
using System.Text;

namespace Shipwreck.FfmpegUtil
{
    public sealed class FfmpegOutputOptions : FfmpegFileOptions
    {
        public TimeSpan To { get; set; }

        public long FileSize { get; set; }

        public DateTime TimeStamp { get; set; }

        internal override void AppendArgs(StringBuilder builder)
        {
            builder.AppendIf("-t", Duration);
            builder.AppendIf("-to", To);
            builder.AppendIf("-fs", FileSize);
            builder.AppendIf("-ss", SeekTo);
            builder.AppendIf("-sseof", SeekToLast);
            builder.AppendIf("-timestamp", TimeStamp);

            builder.Append(FilePath).Append(' ');
        }
    }
}