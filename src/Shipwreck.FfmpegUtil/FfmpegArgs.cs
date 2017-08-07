using System;
using System.ComponentModel;
using System.Text;

namespace Shipwreck.FfmpegUtil
{
    public sealed class FfmpegArgs : CommandLineArgs
    {
        public FfmpegInputOptions InputOptions { get; set; }

        public FfmpegOutputOptions OutputOptions { get; set; }

        internal override void AppendArgs(StringBuilder builder)
        {
            InputOptions?.AppendArgs(builder);
            OutputOptions?.AppendArgs(builder);
        }
    }

}