using System.ComponentModel;
using System.Text;

namespace Shipwreck.FfmpegUtil
{
    public abstract class CommandLineArgs : BufferObject
    {
        internal CommandLineArgs()
        {
        }

        #region Logging

        /// <summary>
        /// Gets or sets the logging level used by the FFmpeg.
        /// </summary>
        [DefaultValue(LogLevel.Default)]
        public LogLevel LogLevel
        {
            get => GetEnum<LogLevel>();
            set => SetValue(value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the FFmpeg will not omit the repeated log.
        /// </summary>
        [DefaultValue(false)]
        public bool RepeatLog
        {
            get => GetBoolean();
            set => SetValue(value);
        }

        #endregion Logging

        internal virtual void AppendArgs(StringBuilder builder)
        {
            var logLevel = LogLevel;
            var repeatLog = RepeatLog;

            if (logLevel != LogLevel.Default)
            {
                builder.Append("-loglevel ");
                if (repeatLog)
                {
                    builder.Append("repeat+");
                }
                builder.Append(logLevel.ToArg());
                builder.Append(' ');
            }
            else if (repeatLog)
            {
                builder.Append("-loglevel repeat ");
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            AppendArgs(sb);

            sb.TrimEnd();
            return sb.ToString();
        }
    }

}