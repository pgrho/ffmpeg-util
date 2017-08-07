using System;
using System.Collections.ObjectModel;
using System.Text;

namespace Shipwreck.FfmpegUtil
{
    public sealed class FfprobeSectionCollection : Collection<FfprobeSection>
    {
        public static FfprobeSectionCollection Parse(string s)
        {
            if (!TryParse(s, out FfprobeSectionCollection r))
            {
                throw new InvalidCastException();
            }
            return r;
        }

        public static bool TryParse(string s, out FfprobeSectionCollection result)
        {
            if (string.IsNullOrEmpty(s))
            {
                result = null;
                return true;
            }

            var si = 0;

            result = new FfprobeSectionCollection();

            var j = s.IndexOf(':', si);

            FfprobeSection sec;
            while (0 <= j && j < s.Length)
            {
                if (!FfprobeSection.TryParse(s, si, j - si, out sec))
                {
                    result = null;
                    return false;
                }

                result.Add(sec);
                si = j + 1;
                j = s.IndexOf(':', si);
            }

            if (!FfprobeSection.TryParse(s, si, s.Length - si, out sec))
            {
                result = null;
                return false;
            }

            result.Add(sec);

            return true;
        }

        public override string ToString()
        {
            if (Count == 0)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();

            foreach (var s in this)
            {
                s.AppendTo(sb);
                sb.Append(':');
            }

            return sb.TrimEnd(':').ToString();
        }
    }
}