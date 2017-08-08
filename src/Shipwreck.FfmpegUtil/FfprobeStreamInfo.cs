using System;
using System.ComponentModel;
using System.Text;

namespace Shipwreck.FfmpegUtil
{
    public sealed class FfprobeStreamInfo : FfmpegStreamOptions
    {
        [DefaultValue(null)]
        public string CodecLongName
        {
            get => GetString();
            set => SetValue(value);
        }

        public TimeSpan Duration
        {
            get => GetTimeSpan();
            set => SetValue(value);
        }

        internal override void AppendArgs(StringBuilder builder)
        {
            base.AppendArgs(builder);
            builder.AppendIf("-codec_long_name", CodecLongName);
            builder.AppendIf("-t", Duration);
        }
    }
}