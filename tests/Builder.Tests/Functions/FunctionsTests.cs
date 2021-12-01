using Xunit;

namespace Builder.Tests.Functions
{
    public class FunctionsTests
    {
        [Fact]
        public void Evaluate_With_Function_SummarizeColumns_Test()
        {
            QueryBuilder builder = new();

            string actualQuery = builder.Evaluate(x =>
            {
                x.Functions(f => f.SummarizeColumns(c =>
                 {
                     c.Column("'Date Dim'[Day name]");
                     c.Column("'Date Dim'[Day name abbreviated]");
                 }));
            })
                .BuildRaw();

            string expectedQuery = $@"EVALUATE SUMMARIZECOLUMNS ('Date Dim'[Day name], 'Date Dim'[Day name abbreviated])";

            Assert.Equal(expectedQuery, actualQuery, ignoreWhiteSpaceDifferences: true);
        }
    }
}
