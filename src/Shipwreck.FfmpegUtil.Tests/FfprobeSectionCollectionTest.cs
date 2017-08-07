using Xunit;

namespace Shipwreck.FfmpegUtil
{
    public class FfprobeSectionCollectionTest
    {
        [Fact]
        public void TryParseTest_Null()
        {
            Assert.True(FfprobeSectionCollection.TryParse(null, out FfprobeSectionCollection r));
            Assert.Null(r);
        }

        [Fact]
        public void TryParseTest_Empty()
        {
            Assert.True(FfprobeSectionCollection.TryParse("", out FfprobeSectionCollection r));
            Assert.Null(r);
        }

        [Fact]
        public void TryParseTest_Single()
        {
            Assert.True(FfprobeSectionCollection.TryParse("Abc", out FfprobeSectionCollection r));
            Assert.Equal(1, r.Count);
            Assert.Equal("Abc", r[0].Name);
            Assert.Equal(0, r[0].Entries.Count);
        }

        [Fact]
        public void TryParseTest_Multiple()
        {
            Assert.True(FfprobeSectionCollection.TryParse("Abc:Def=A1:Ghi=B2,C3", out FfprobeSectionCollection r));

            Assert.Equal(3, r.Count);
            Assert.Equal("Abc", r[0].Name);
            Assert.Equal(0, r[0].Entries.Count);

            Assert.Equal(3, r.Count);
            Assert.Equal("Def", r[1].Name);
            Assert.Equal(new[] { "A1" }, r[1].Entries);

            Assert.Equal(3, r.Count);
            Assert.Equal("Ghi", r[2].Name);
            Assert.Equal(new[] { "B2", "C3" }, r[2].Entries);
        }
    }
}