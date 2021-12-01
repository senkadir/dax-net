using Xunit;

namespace Builder.Tests
{
    public class DefineTests
    {
        [Fact]
        public void Evaluate_With_Var_Test()
        {
            QueryBuilder builder = new();

            string actualQuery = builder.Evaluate(x =>
            {
                x.Table("TestTable1")
                 .Define(d => d.Var("Variable1", "MAX (Sales[Order Date])"));
            })
                .BuildRaw();

            string expectedQuery = $@"DEFINE VAR Variable1 = MAX (Sales[Order Date]) EVALUATE('TestTable1')";

            Assert.Equal(expectedQuery, actualQuery, ignoreWhiteSpaceDifferences: true);
        }

        [Fact]
        public void Evaluate_With_Column_Test()
        {
            QueryBuilder builder = new();

            string actualQuery = builder.Evaluate(x =>
            {
                x.Table("TestTable1")
                 .Define(d => d.Column("Variable1", "MAX (Sales[Order Date])"));
            })
                .BuildRaw();

            string expectedQuery = $@"DEFINE COLUMN Variable1 = MAX (Sales[Order Date]) EVALUATE('TestTable1')";

            Assert.Equal(expectedQuery, actualQuery, ignoreWhiteSpaceDifferences: true);
        }

        [Fact]
        public void Evaluate_With_Multiple_Measures_Test()
        {
            QueryBuilder builder = new();

            string actualQuery = builder.Evaluate(x =>
            {
                x.Table("TestTable1")
                 .Define(d =>
                 {
                     d.Mesaure("Measure1", "MAX (Sales[Order Date])")
                      .Mesaure("Measure2", "MAX (Sales[Order Date])");
                 });

            })
                .BuildRaw();

            string expectedQuery = $@"DEFINE MEASURE Measure1 = MAX (Sales[Order Date]) MEASURE Measure2 = MAX (Sales[Order Date]) EVALUATE('TestTable1')";

            Assert.Equal(expectedQuery, actualQuery, ignoreWhiteSpaceDifferences: true);
        }

        [Fact]
        public void Evaluate_With_Multiple_Tables_Test()
        {
            QueryBuilder builder = new();

            string actualQuery = builder.Evaluate(x =>
            {
                x.Table("TestTable1")
                 .Define(d =>
                 {
                     d.Table("Table1", "DISTINCT (Sales)")
                      .Table("Table2", "DISTINCT (Sales)");
                 });

            })
                .BuildRaw();

            string expectedQuery = $@"DEFINE TABLE Table1 = DISTINCT (Sales) TABLE Table2 = DISTINCT (Sales) EVALUATE('TestTable1')";

            Assert.Equal(expectedQuery, actualQuery, ignoreWhiteSpaceDifferences: true);
        }

    }
}
