using Xunit;

namespace Shipwreck.FfmpegUtil
{
    public class FfmpegStreamOptionsTest
    {
        [Fact]
        public void FilterTest()
        {
            var target = new FfmpegStreamOptions();
            target.StreamType = FfmpegStreamType.Video;
            Assert.Null(target.Filter);

            var value = "default";
            target.Filter = value;
            Assert.Equal(value, target.Filter);
            Assert.Equal("-filter:V default", target.ToString());
        }

        [Fact]
        public void DispositionTest()
        {
            var target = new FfmpegStreamOptions();
            target.StreamType = FfmpegStreamType.Video;
            Assert.Null(target.Disposition);

            var value = "default";
            target.Disposition = value;
            Assert.Equal(value, target.Disposition);
            Assert.Equal("-disposition:V default", target.ToString());
        }

        [Fact]
        public void FrameCountTest()
        {
            var target = new FfmpegStreamOptions();
            target.StreamType = FfmpegStreamType.Video;
            Assert.Equal(0, target.FrameCount);

            var value = 10;
            target.FrameCount = value;
            Assert.Equal(value, target.FrameCount);
            Assert.Equal("-frames:V 10", target.ToString());
        }

        [Fact]
        public void QualityScaleTest()
        {
            var target = new FfmpegStreamOptions();
            target.StreamType = FfmpegStreamType.Video;
            Assert.Equal(0, target.QualityScale);

            byte value = 10;
            target.QualityScale = value;
            Assert.Equal(value, target.QualityScale);
            Assert.Equal("-q:V 10", target.ToString());
        }
    }
}