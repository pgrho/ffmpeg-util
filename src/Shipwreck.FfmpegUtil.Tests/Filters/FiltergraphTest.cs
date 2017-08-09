using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Shipwreck.FfmpegUtil.Filters
{
    public class FiltergraphTest
    {
        [Fact]
        public void ToStringTest()
        {
            var target = new Filtergraph()
            {
                new ScaleFilter() { Width = 160, Height = 72 },
                new FrameStepFilter() { Step = 24 },
                new TileFilter() { ColumnCount = 3, RowCount = 2 },
            };

            Assert.Equal("scale=160:72,framestep=24,tile=3x2", target.ToString());
        }
    }
}
