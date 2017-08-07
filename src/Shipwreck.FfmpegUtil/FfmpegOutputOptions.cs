using System;
using System.Text;

namespace Shipwreck.FfmpegUtil
{
    public sealed class FfmpegOutputOptions : FfmpegFileOptions
    {
        public TimeSpan To
        {
            get => GetTimeSpan();
            set => SetValue(value);
        }

        public long FileSize
        {
            get => GetInt64();
            set => SetValue(value);
        }

        public DateTime TimeStamp
        {
            get => GetDateTime();
            set => SetValue(value);
        }

        internal override void AppendArgs(StringBuilder builder)
        {
            builder.AppendIf("-t", Duration);
            builder.AppendIf("-to", To);
            builder.AppendIf("-fs", FileSize);
            builder.AppendIf("-ss", SeekTo);
            builder.AppendIf("-sseof", SeekToLast);
            builder.AppendIf("-timestamp", TimeStamp);

            if (ShouldSerializeStreams())
            {
                foreach (var s in Streams)
                {
                    s.AppendArgs(builder);
                }
            }

            if (!string.IsNullOrEmpty(FilePath))
            {
                builder.Append('"').Append(FilePath).Append("\" ");
            }
        }
    }
}