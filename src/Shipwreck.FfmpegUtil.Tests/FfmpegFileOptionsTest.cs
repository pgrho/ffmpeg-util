using System;
using Xunit;

namespace Shipwreck.FfmpegUtil
{
    public class FfmpegFileOptionsTest
    {
        [Fact]
        public void FilePathTest()
        {
            var target = new FfmpegInputOptions();
            Assert.Null(target.FilePath);
            target.FilePath = "C:\\test.mp4";
            Assert.Equal("C:\\test.mp4", target.FilePath);
            Assert.Equal("-i \"C:\\test.mp4\"", target.ToString());
        }

        [Fact]
        public void DurationTest()
        {
            var target = new FfmpegInputOptions();
            Assert.Equal(TimeSpan.Zero, target.Duration);
            target.Duration = TimeSpan.FromSeconds(1.5);
            Assert.Equal(TimeSpan.FromSeconds(1.5), target.Duration);
            Assert.Equal("-t 1.5", target.ToString());
        }

        [Fact]
        public void SeekToTest()
        {
            var target = new FfmpegInputOptions();
            Assert.Equal(TimeSpan.Zero, target.SeekTo);
            target.SeekTo = TimeSpan.FromSeconds(1.5);
            Assert.Equal(TimeSpan.FromSeconds(1.5), target.SeekTo);
            Assert.Equal("-ss 1.5", target.ToString());
        }

        [Fact]
        public void SeekToLastTest()
        {
            var target = new FfmpegInputOptions();
            Assert.Equal(TimeSpan.Zero, target.SeekToLast);
            target.SeekToLast = TimeSpan.FromSeconds(1.5);
            Assert.Equal(TimeSpan.FromSeconds(1.5), target.SeekToLast);
            Assert.Equal("-sseof 1.5", target.ToString());
        }
    }
}