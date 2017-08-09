using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Shipwreck.FfmpegUtil
{
    public sealed class FfprobeSection
    {
        private Collection<string> _Entries;

        [DefaultValue(null)]
        public string Name { get; set; }

        public IList<string> Entries
        {
            get => CollectionHelper.GetCollection(ref _Entries);
            set => CollectionHelper.SetCollection(ref _Entries, value);
        }

        public bool ShouldSerializeEntries()
             => _Entries?.Count > 0;

        public void ResetEntries()
             => _Entries?.Clear();

        public static FfprobeSection Parse(string s)
        {
            if (!TryParse(s, out FfprobeSection r))
            {
                throw new InvalidCastException();
            }
            return r;
        }

        public static bool TryParse(string s, out FfprobeSection result)
            => TryParse(s, 0, s?.Length ?? 0, out result);

        public static bool TryParse(string s, int startIndex, int length, out FfprobeSection result)
        {
            if (s == null)
            {
                result = null;
                return false;
            }

            var i = s.IndexOf('=', startIndex);

            if (0 <= i && i < startIndex + length)
            {
                var name = s.Substring(startIndex, i - startIndex).Trim();

                if (string.IsNullOrEmpty(name))
                {
                    result = null;
                    return false;
                }

                result = new FfprobeSection()
                {
                    Name = name
                };

                var si = i + 1;

                var j = s.IndexOf(',', si);

                while (0 <= j && j < startIndex + length)
                {
                    var e = s.Substring(si, j - si).Trim();

                    if (string.IsNullOrEmpty(e))
                    {
                        result = null;
                        return false;
                    }

                    result.Entries.Add(e);
                    si = j + 1;
                    j = s.IndexOf(',', si);
                }

                {
                    var e = s.Substring(si, startIndex + length - si).Trim();
                    if (string.IsNullOrEmpty(e))
                    {
                        result = null;
                        return false;
                    }

                    result.Entries.Add(e);
                }
            }
            else
            {
                var name = s.Substring(startIndex, length).Trim();

                if (string.IsNullOrEmpty(name))
                {
                    result = null;
                    return false;
                }
                result = new FfprobeSection()
                {
                    Name = name
                };
            }

            return true;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Name) && !(_Entries?.Count > 0))
            {
                return string.Empty;
            }
            var b = new StringBuilder();
            AppendTo(b);

            return b.ToString();
        }

        internal void AppendTo(StringBuilder b)
        {
            b.Append(Name);

            if (ShouldSerializeEntries())
            {
                b.Append('=');

                foreach (var e in _Entries)
                {
                    b.Append(e);
                    b.Append(',');
                }
                b.TrimEnd(',');
            }
        }
    }
}