using System;
using Xunit;

namespace Shipwreck.FfmpegUtil
{
    public class FfmpegInputOptionsTest
    {
        [Fact]
        public void StreamLoopTest()
        {
            var target = new FfmpegInputOptions();
            Assert.Equal(0, target.StreamLoop);

            var value = -1;
            target.StreamLoop = value;
            Assert.Equal(value, target.StreamLoop);
            Assert.Equal("-stream_loop -1", target.ToString());
        }

        [Fact]
        public void TimeSpanOffsetTest()
        {
            var target = new FfmpegInputOptions();
            Assert.Equal(TimeSpan.Zero, target.TimeSpanOffset);

            var value = TimeSpan.FromSeconds(1.5);
            target.TimeSpanOffset = value;
            Assert.Equal(value, target.TimeSpanOffset);
            Assert.Equal("-itsoffset 1.5", target.ToString());
        }
    }
}