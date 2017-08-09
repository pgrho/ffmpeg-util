using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Shipwreck.FfmpegUtil.Filters
{
    public sealed class TileFilter : Filter
    {
        public override string Name
            => "tile";

        [DefaultValue(0)]
        public int ColumnCount { get; set; }

        [DefaultValue(0)]
        public int RowCount { get; set; }

        internal override void AppendArguments(StringBuilder builder)
        {
            builder.Append(Math.Max(ColumnCount, 1)).Append('x').Append(Math.Max(RowCount, 1));
        }
    }
}