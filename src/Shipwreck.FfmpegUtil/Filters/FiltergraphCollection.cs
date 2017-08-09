using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Shipwreck.FfmpegUtil.Filters
{

    public sealed class FiltergraphCollection : Collection<Filtergraph>
    {
        internal void AppendTo(StringBuilder builder)
        {
            foreach (var item in this)
            {
                item.AppendTo(builder);
                builder.Append(';');
            }
            builder.TrimEnd(';');
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            AppendTo(sb);
            return sb.ToString();
        }
    }

}