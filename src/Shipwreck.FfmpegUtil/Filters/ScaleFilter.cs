using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Shipwreck.FfmpegUtil.Filters
{
    public sealed class ScaleFilter : Filter
    {
        public override string Name
            => "scale";

        [DefaultValue(0)]
        public int Width { get; set; }

        [DefaultValue(0)]
        public int Height { get; set; }

        internal override void AppendArguments(StringBuilder builder)
        {
            builder.Append(Width).Append(':').Append(Height);
        }
    }
}