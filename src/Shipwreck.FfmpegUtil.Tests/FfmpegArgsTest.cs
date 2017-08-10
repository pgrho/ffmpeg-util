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
        public void VSyncTest()
        {
            var target = new FfmpegArgs();
            Assert.Equal(VSync.Auto, target.VSync);
            target.VSync = VSync.ConstantFrameRate;
            Assert.Equal(VSync.ConstantFrameRate, target.VSync);
            Assert.Equal("-vsync cfr", target.ToString());
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

        [Fact]
        public void ToStringTest_Complex()
        {
            var target = new FfmpegArgs()
            {
                InputOptions = new FfmpegInputOptions()
                {
                    FilePath = "INPUT"
                },
                OutputOptions = new FfmpegOutputOptions()
                {
                    FilePath = "OUTPUT",
                    AllStream = new FfmpegStreamOptions()
                    {
                        FrameCount = 24
                    },
                    VideoStream = new FfmpegStreamOptions()
                    {
                        QualityScale = 2
                    },
                    AudioStream = new FfmpegStreamOptions()
                    {
                        Codec = "aac"
                    }
                }
            };

            Assert.Equal("-i \"INPUT\" -frames 24 -q:v 2 -c:a aac \"OUTPUT\"", target.ToString());
        }
    }
}