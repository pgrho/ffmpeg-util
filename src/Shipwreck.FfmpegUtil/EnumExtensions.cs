namespace Shipwreck.FfmpegUtil
{
    public static class EnumExtensions
    {
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