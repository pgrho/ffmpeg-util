using System;
using System.Text;

namespace Shipwreck.FfmpegUtil
{
    public sealed class FfmpegInputOptions : FfmpegFileOptions
    {
        public TimeSpan TimeSpanOffset
        {
            get => GetTimeSpan();
            set => SetValue(value);
        }

        internal override void AppendArgs(StringBuilder builder)
        {
            builder.AppendIf("-t", Duration);
            builder.AppendIf("-ss", SeekTo);
            builder.AppendIf("-sseof", SeekToLast);
            builder.AppendIf("-itsoffset", TimeSpanOffset);

            if (ShouldSerializeStreams())
            {
                foreach (var s in Streams)
                {
                    s.AppendArgs(builder);
                }
            }

            if (!string.IsNullOrEmpty(FilePath))
            {
                builder.Append("-i \"").Append(FilePath).Append("\" ");
            }
        }
    }
}