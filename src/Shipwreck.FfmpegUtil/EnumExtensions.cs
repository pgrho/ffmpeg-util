namespace Shipwreck.FfmpegUtil
{
    public static class EnumExtensions
    {
        public static char ToArg(this FfmpegStreamType value)
        {
            switch (value)
            {
                case FfmpegStreamType.Video:
                    return 'V';

                case FfmpegStreamType.AllVideo:
                    return 'v';

                case FfmpegStreamType.Audio:
                    return 'a';

                case FfmpegStreamType.Subtitle:
                    return 's';

                case FfmpegStreamType.Data:
                    return 'd';

                case FfmpegStreamType.Attachments:
                default:
                    return 't';
            }
        }

        public static string ToArg(this LogLevel value)
        {
            switch (value)
            {
                case LogLevel.Quiet:
                    return "quiet";

#pragma warning disable CS0618 // �^�܂��̓����o�[���Â��`���ł�
                case LogLevel.Panic:
#pragma warning restore CS0618 // �^�܂��̓����o�[���Â��`���ł�
                    return "panic";

                case LogLevel.Fatal:
                    return "fatal";

                case LogLevel.Error:
                    return "error";

                case LogLevel.Warning:
                    return "warning";

                case LogLevel.Info:
                    return "info";

                case LogLevel.Debug:
                    return "debug";

                case LogLevel.Trace:
                    return "trace";

                default:
                    return value.ToString().ToLowerInvariant();
            }
        }

        public static string ToArg(this VSync value)
        {
            switch (value)
            {
                case VSync.Auto:
                    return "auto";

                case VSync.Passthrough:
                    return "passthrough";

                case VSync.ConstantFrameRate:
                    return "cfr";

                case VSync.VariableFrameRate:
                    return "vfr";

                case VSync.Drop:
                    return "drop";

                default:
                    return value.ToString().ToLowerInvariant();
            }
        }
    }
}