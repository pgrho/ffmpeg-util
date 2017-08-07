using Xunit;

namespace Shipwreck.FfmpegUtil
{
    public class FfmpegStreamOptionsTest
    {
        [Fact]
        public void DispositionTest()
        {
            var target = new FfmpegVideoStreamOptions();
            target.StreamType = FfmpegStreamType.Video;
            Assert.Null(target.Disposition);

            var value = "default";
            target.Disposition = value;
            Assert.Equal(value, target.Disposition);
            Assert.Equal("-disposition:V default", target.ToString());
        }
    }
}