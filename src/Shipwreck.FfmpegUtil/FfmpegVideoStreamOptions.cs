using System.Text;

namespace Shipwreck.FfmpegUtil
{
    public sealed class FfmpegVideoStreamOptions : FfmpegStreamOptions
    {
        internal override void AppendArgs(StringBuilder builder)
        {
            base.AppendArgs(builder);
        }
    }
}