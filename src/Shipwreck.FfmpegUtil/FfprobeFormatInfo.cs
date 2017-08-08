using System.ComponentModel;
using System.Text;

namespace Shipwreck.FfmpegUtil
{
    public sealed class FfprobeFormatInfo : FfmpegFileOptions
    {
        [DefaultValue(0L)]
        public long FileSize
        {
            get => GetInt64();
            set => SetValue(value);
        }

        internal override void AppendArgs(StringBuilder builder)
        {
            builder.AppendIf("-fs", FileSize);
            base.AppendArgs(builder);
        }
    }
}