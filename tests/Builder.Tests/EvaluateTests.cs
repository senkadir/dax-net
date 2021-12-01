using Xunit;

namespace Builder.Tests
{
    public class EvaluateTests
    {
        [Fact]
        public void Evaluate_With_Multiple_Orders_Test()
        {
            QueryBuilder builder = new();

            string actualQuery = builder.Evaluate(x =>
                               {
                                   x.Table("TestTable1")
                                    .OrderBy("Col1")
                                    .OrderByDescending("Col2 With Space");
                               })
                               .BuildRaw();

            string expectedQuery = $@"EVALUATE('TestTable1') ORDER BY 'TestTable1'[Col1] ASC, 'TestTable1'[Col2 With Space] DESC";

            Assert.Equal(expectedQuery, actualQuery, ignoreWhiteSpaceDifferences: true);

        }

        [Fact]
        public void Evaluate_With_Multiple_Orders_And_StartAts_Test()
        {
            QueryBuilder builder = new();

            string actualQuery = builder.Evaluate(x =>
            {
                x.Table("TestTable1")
                 .OrderBy("Col1")
                 .OrderByDescending("Col2 With Space")
                 .StartAt(50)
                 .StartAt("50");
            })
                .BuildRaw();

            string expectedQuery = $"EVALUATE('TestTable1') ORDER BY 'TestTable1'[Col1] ASC, 'TestTable1'[Col2 With Space] DESC START AT 50, \"50\"";

            Assert.Equal(expectedQuery, actualQuery, ignoreWhiteSpaceDifferences: true);
        }
    }
}
