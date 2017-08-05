using System.Text;

namespace Shipwreck.FfmpegUtil
{
    public abstract class CommandLineArgs
    {
        internal abstract void AppendArgs(StringBuilder builder);

        public override string ToString()
        {
            var sb = new StringBuilder();

            AppendArgs(sb);

            sb.TrimEnd();
            return sb.ToString();
        }
    }
}