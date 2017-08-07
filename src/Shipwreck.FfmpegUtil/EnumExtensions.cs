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

                case LogLevel.Panic:
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
    }
}