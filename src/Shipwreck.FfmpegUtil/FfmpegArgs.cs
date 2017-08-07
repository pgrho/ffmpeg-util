using System.ComponentModel;
using System.Text;

namespace Shipwreck.FfmpegUtil
{
    public sealed class FfmpegArgs : CommandLineArgs
    {
        /// <summary>
        /// Gets or sets a value indicating whether the FFmpeg will overwrite output files without asking.
        /// </summary>
        [DefaultValue(null)]
        public bool? OverwriteOutputFiles
        {
            get => GetNullableBoolean();
            set => SetValue(value);
        }

        public FfmpegInputOptions InputOptions { get; set; }

        public FfmpegOutputOptions OutputOptions { get; set; }

        internal override void AppendArgs(StringBuilder builder)
        {
            base.AppendArgs(builder);

            var oof = OverwriteOutputFiles;
            if (oof != null)
            {
                builder.Append(oof.Value ? "-y " : "-n ");
            }

            InputOptions?.AppendArgs(builder);
            OutputOptions?.AppendArgs(builder);
        }
    }
}