using Xunit;

namespace Shipwreck.FfmpegUtil
{
    public class FfprobeArgsTest
    {
        [Fact]
        public void FilePathTest()
        {
            var target = new FfprobeArgs();
            Assert.Null(target.FilePath);

            var value = "C:\\t.mp4";
            target.FilePath = value;
            Assert.Equal(value, target.FilePath);
            Assert.Equal($"\"{value}\"", target.ToString());
        }

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

        [Fact]
        public void SelectStreamsTest()
        {
            var target = new FfprobeArgs();
            Assert.True(target.SelectStreams.IsEmpty);

            var value = new StreamSpecifier(FfmpegStreamType.Video, 0);
            target.SelectStreams = value;
            Assert.Equal(value, target.SelectStreams);
            Assert.Equal("-select_streams V:0", target.ToString());
        }

        [Fact]
        public void ShowEntriesTest()
        {
            var target = new FfprobeArgs();
            var value = "default";
            target.ShowEntries = value;
            Assert.Equal(value, target.ShowEntries);
            Assert.Equal("-show_entries default", target.ToString());
        }

        [Fact]
        public void PrintFormatTest()
        {
            var target = new FfprobeArgs();
            var value = "default";
            target.PrintFormat = value;
            Assert.Equal(value, target.PrintFormat);
            Assert.Equal("-of default", target.ToString());
        }

        [Fact]
        public void SkipFrameTest()
        {
            var target = new FfprobeArgs();
            var value = "nokey";
            target.SkipFrame = value;
            Assert.Equal(value, target.SkipFrame);
            Assert.Equal("-skip_frame nokey", target.ToString());
        }
    }
}