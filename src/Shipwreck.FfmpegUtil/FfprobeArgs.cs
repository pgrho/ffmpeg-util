using System.ComponentModel;
using System.Text;

namespace Shipwreck.FfmpegUtil
{
    public sealed class FfprobeArgs : CommandLineArgs
    {
        /// <summary>
        /// Gets or sets a file path.
        /// </summary>
        [DefaultValue(null)]
        public string FilePath
        {
            get => GetString();
            set => SetValue(value);
        }

        [DefaultValue(false)]
        public bool HideBanner
        {
            get => GetBoolean();
            set => SetValue(value);
        }

        [DefaultValue(false)]
        public bool ShowFormat
        {
            get => GetBoolean();
            set => SetValue(value);
        }

        [DefaultValue(false)]
        public bool ShowStreams
        {
            get => GetBoolean();
            set => SetValue(value);
        }

        public StreamSpecifier SelectStreams
        {
            get => GetStreamSpecifier();
            set => SetValue(value);
        }

        [DefaultValue(false)]
        public bool ShowFrames
        {
            get => GetBoolean();
            set => SetValue(value);
        }

        [DefaultValue(null)]
        public string ShowEntries
        {
            get => GetString();
            set => SetValue(value);
        }

        [DefaultValue(null)]
        public string PrintFormat
        {
            get => GetString();
            set => SetValue(value);
        }

        [DefaultValue(null)]
        public string SkipFrame
        {
            get => GetString();
            set => SetValue(value);
        }

        internal override void AppendArgs(StringBuilder builder)
        {
            builder.AppendFilePath(FilePath);

            base.AppendArgs(builder);

            builder.AppendIf("-hide_banner", HideBanner);
            builder.AppendIf("-select_streams", SelectStreams);
            builder.AppendIf("-show_format", ShowFormat);
            builder.AppendIf("-show_streams", ShowStreams);
            builder.AppendIf("-show_frames", ShowFrames);
            builder.AppendIf("-show_entries", ShowEntries);
            builder.AppendIf("-of", PrintFormat);
            builder.AppendIf("-skip_frame", SkipFrame);
        }
    }
}