using Xunit;

namespace Shipwreck.FfmpegUtil
{
    public class CommandLineArgsTest
    {
        [Fact]
        public void LogLevelTest()
        {
            var target = new FfprobeArgs();
            Assert.Equal(LogLevel.Default, target.LogLevel);
            target.LogLevel = LogLevel.Verbose;
            Assert.Equal(LogLevel.Verbose, target.LogLevel);
            Assert.Equal("-loglevel verbose", target.ToString());
        }

        [Fact]
        public void RepeatLogTest()
        {
            var target = new FfprobeArgs();
            Assert.False(target.RepeatLog);
            target.RepeatLog = true;
            Assert.True(target.RepeatLog);
            Assert.Equal("-loglevel repeat", target.ToString());
        }

        [Fact]
        public void RepeatedLogLevelTest()
        {
            var target = new FfprobeArgs();
            target.LogLevel = LogLevel.Verbose;
            target.RepeatLog = true;
            Assert.Equal(LogLevel.Verbose, target.LogLevel);
            Assert.True(target.RepeatLog);
            Assert.Equal("-loglevel repeat+verbose", target.ToString());
        }
    }
}