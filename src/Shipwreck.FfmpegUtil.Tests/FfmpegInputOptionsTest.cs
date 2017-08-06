using System;
using Xunit;

namespace Shipwreck.FfmpegUtil
{
    public class FfmpegInputOptionsTest
    {
        [Fact]
        public void TimeSpanOffsetTest()
        {
            var target = new FfmpegInputOptions();
            Assert.Equal(TimeSpan.Zero, target.TimeSpanOffset);
            target.TimeSpanOffset = TimeSpan.FromSeconds(1.5);
            Assert.Equal(TimeSpan.FromSeconds(1.5), target.TimeSpanOffset);
            Assert.Equal("-itsoffset 1.5", target.ToString());
        }
    }

    public class FfmpegOutputOptionsTest
    {
        [Fact]
        public void ToTest()
        {
            var target = new FfmpegOutputOptions();
            Assert.Equal(TimeSpan.Zero, target.To);
            target.To = TimeSpan.FromSeconds(1.5);
            Assert.Equal(TimeSpan.FromSeconds(1.5), target.To);
            Assert.Equal("-to 1.5", target.ToString());
        }

        [Fact]
        public void FileSizeTest()
        {
            var target = new FfmpegOutputOptions();
            Assert.Equal(0, target.FileSize);
            target.FileSize = 1500;
            Assert.Equal(1500, target.FileSize);
            Assert.Equal("-fs 1500", target.ToString());
        }

        [Fact]
        public void TimeStampTest()
        {
            var target = new FfmpegOutputOptions();
            Assert.Equal(DateTime.MinValue, target.TimeStamp);
            target.TimeStamp = new DateTime(1, 2, 3, 4, 5, 6, 7);
            Assert.Equal(new DateTime(1, 2, 3, 4, 5, 6, 7), target.TimeStamp);
            Assert.Equal("-timestamp 00010203T040506.007", target.ToString());
        }
    }
}