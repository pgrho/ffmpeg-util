using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Shipwreck.FfmpegUtil.Filters
{
    public abstract class Filter
    {
        public abstract string Name { get; }

        #region InputLabels

        private Collection<string> _InputLabels;

        public IList<string> InputLabels
        {
            get => CollectionHelper.GetCollection(ref _InputLabels);
            set => CollectionHelper.SetCollection(ref _InputLabels, value);
        }

        public bool ShouldSerializeInputLabels()
             => _InputLabels?.Count > 0;

        public void ResetInputLabels()
             => _InputLabels?.Clear();

        #endregion InputLabels

        #region OutputLabels

        private Collection<string> _OutputLabels;

        public IList<string> OutputLabels
        {
            get => CollectionHelper.GetCollection(ref _OutputLabels);
            set => CollectionHelper.SetCollection(ref _OutputLabels, value);
        }

        public bool ShouldSerializeOutputLabels()
             => _OutputLabels?.Count > 0;

        public void ResetOutputLabels()
             => _OutputLabels?.Clear();

        #endregion OutputLabels

        internal void AppendTo(StringBuilder builder)
        {
            if (ShouldSerializeInputLabels())
            {
                foreach (var l in InputLabels)
                {
                    builder.Append('[').Append(l).Append(']');
                }
            }

            builder.Append(Name).Append('=');

            var p = builder.Length;

            AppendArguments(builder);

            if (builder.Length == p)
            {
                builder.Length--;
            }

            if (ShouldSerializeOutputLabels())
            {
                foreach (var l in OutputLabels)
                {
                    builder.Append('[').Append(l).Append(']');
                }
            }
        }

        internal virtual void AppendArguments(StringBuilder builder)
        {
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            AppendTo(sb);
            return sb.ToString();
        }
    }
}