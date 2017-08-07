using Xunit;

namespace Shipwreck.FfmpegUtil
{
    public class FfmpegArgsTest
    {
        [Fact]
        public void OverwriteOutputFilesTest_True()
        {
            var target = new FfmpegArgs();
            Assert.Null(target.OverwriteOutputFiles);
            target.OverwriteOutputFiles = true;
            Assert.True(target.OverwriteOutputFiles);
            Assert.Equal("-y", target.ToString());
        }

        [Fact]
        public void OverwriteOutputFilesTest_False()
        {
            var target = new FfmpegArgs();
            Assert.Null(target.OverwriteOutputFiles);
            target.OverwriteOutputFiles = false;
            Assert.False(target.OverwriteOutputFiles);
            Assert.Equal("-n", target.ToString());
        }
    }
}