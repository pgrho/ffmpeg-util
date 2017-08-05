using System;
using System.Text;

namespace Shipwreck.FfmpegUtil
{
    public sealed class FfprobeArgs : CommandLineArgs
    {
        #region Flags

        [Flags]
        private enum Flags : byte
        {
            HideBanner = 1 << 0,
            ShowFormat = 1 << 1,
            ShowStreams = 1 << 2,
        }

        private Flags _Flags;

        public bool HideBanner
        {
            get => (_Flags & Flags.HideBanner) != 0;
            set => _Flags = value ? _Flags | Flags.HideBanner : (_Flags & ~Flags.HideBanner);
        }

        public bool ShowFormat
        {
            get => (_Flags & Flags.ShowFormat) != 0;
            set => _Flags = value ? _Flags | Flags.ShowFormat : (_Flags & ~Flags.ShowFormat);
        }

        public bool ShowStreams
        {
            get => (_Flags & Flags.ShowStreams) != 0;
            set => _Flags = value ? _Flags | Flags.ShowStreams : (_Flags & ~Flags.ShowStreams);
        }

        #endregion Flags

        internal override void AppendArgs(StringBuilder builder)
        {
            builder.AppendIf("-hide_banner", HideBanner);
            builder.AppendIf("-show_format", ShowFormat);
            builder.AppendIf("-show_streams", ShowStreams);
        }
    }
}