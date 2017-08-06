using System;
using Xunit;

namespace Shipwreck.FfmpegUtil
{
    public class FfprobeArgsTest
    {
        [Fact]
        public void HideBannerTest()
        {
            var target = new FfprobeArgs();
            Assert.False(target.HideBanner);
            target.HideBanner = true;
            Assert.True(target.HideBanner);
            Assert.Equal("-hide_banner", target.ToString());
            target.HideBanner = false;
            Assert.False(target.HideBanner);
        }

        [Fact]
        public void ShowFormatTest()
        {
            var target = new FfprobeArgs();
            Assert.False(target.ShowFormat);
            target.ShowFormat = true;
            Assert.True(target.ShowFormat);
            Assert.Equal("-show_format", target.ToString());
        }

        [Fact]
        public void ShowStreamsTest()
        {
            var target = new FfprobeArgs();
            Assert.False(target.ShowStreams);
            target.ShowStreams = true;
            Assert.True(target.ShowStreams);
            Assert.Equal("-show_streams", target.ToString());
        }
    }
}