using System;
using System.ComponentModel;
using System.Text;

namespace Shipwreck.FfmpegUtil
{
    public sealed class FfmpegInputOptions : FfmpegFileOptions
    {
        /// <summary>
        /// Gets or sets number of times input stream shall be looped.
        /// </summary>
        /// <remarks>Loop 0 means no loop, loop -1 means infinite loop.</remarks>
        [DefaultValue(0)]
        public int StreamLoop
        {
            get => GetInt32();
            set => SetValue(value);
        }

        public TimeSpan TimeSpanOffset
        {
            get => GetTimeSpan();
            set => SetValue(value);
        }

        internal override void AppendArgs(StringBuilder builder)
        {
            base.AppendArgs(builder);

            builder.AppendIf("-stream_loop", StreamLoop);
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