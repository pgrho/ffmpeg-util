using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Shipwreck.FfmpegUtil.Filters
{
    public sealed class FrameStepFilter : Filter
    {
        public override string Name
            => "framestep";

        [DefaultValue(0)]
        public int Step { get; set; }

        internal override void AppendArguments(StringBuilder builder)
        {
            builder.Append(Step);
        }
    }
}