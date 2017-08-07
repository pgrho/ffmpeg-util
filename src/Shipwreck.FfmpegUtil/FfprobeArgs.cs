using System.Text;

namespace Shipwreck.FfmpegUtil
{
    public sealed class FfprobeArgs : CommandLineArgs
    {
        public bool HideBanner
        {
            get => GetBoolean();
            set => SetValue(value);
        }

        public bool ShowFormat
        {
            get => GetBoolean();
            set => SetValue(value);
        }

        public bool ShowStreams
        {
            get => GetBoolean();
            set => SetValue(value);
        }

        internal override void AppendArgs(StringBuilder builder)
        {
            base.AppendArgs(builder);

            builder.AppendIf("-hide_banner", HideBanner);
            builder.AppendIf("-show_format", ShowFormat);
            builder.AppendIf("-show_streams", ShowStreams);
        }
    }
}