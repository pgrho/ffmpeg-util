using System;
using System.Text;

namespace Shipwreck.FfmpegUtil
{
    public sealed class FfmpegInputOptions : FfmpegFileOptions
    {
        public TimeSpan TimeSpanOffset { get; set; }

        internal override void AppendArgs(StringBuilder builder)
        {
            builder.AppendIf("-t", Duration);
            builder.AppendIf("-ss", SeekTo);
            builder.AppendIf("-sseof", SeekToLast);
            builder.AppendIf("-itsoffset", TimeSpanOffset);

            builder.Append("-i ").Append(FilePath).Append(' ');
        }
    }
}