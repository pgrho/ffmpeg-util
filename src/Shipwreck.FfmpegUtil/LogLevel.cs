using System;

namespace Shipwreck.FfmpegUtil
{
    public enum LogLevel : byte
    {
        Default = 0,

        /// <summary>
        /// Show nothing at all; be silent.
        /// </summary>
        Quiet = 1,

        /// <summary>
        /// Only show fatal errors which could lead the process to crash, such as an assertion failure
        /// </summary>
        [Obsolete("his is not currently used for anything. ")]
        Panic = 2,

        /// <summary>
        /// Only show fatal errors. These are errors after which the process absolutely cannot continue.
        /// </summary>
        Fatal = 3,

        /// <summary>
        /// Show all errors, including ones which can be recovered from.
        /// </summary>
        Error = 4,

        /// <summary>
        /// Show all warnings and errors. Any message related to possibly incorrect or unexpected
        /// events will be shown.
        /// </summary>
        Warning = 5,

        /// <summary>
        /// Show informative messages during processing. This is in addition to warnings and errors.
        /// This is the default value.
        /// </summary>
        Info = 6,

        /// <summary>
        /// Same as <see cref="Info" />, except more verbose.
        /// </summary>
        Verbose = 7,

        /// <summary>
        /// Show everything, including debugging information.
        /// </summary>
        Debug = 8,

        Trace = 9
    }
}