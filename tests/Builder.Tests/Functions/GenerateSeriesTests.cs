using System;
using Xunit;

namespace Builder.Tests.Functions
{
    public class GenerateSeriesTests
    {
        [Fact]
        public void Evaluate_With_Function_GenerateSeries_Int_Test()
        {
            QueryBuilder builder = new();

            string actualQuery = builder.Evaluate(x =>
            {
                x.Functions(f => f.GenerateSeries(g =>
                {
                    g.Column(1, 10);
                }));
            })
                .BuildRaw();

            string expectedQuery = $@"EVALUATE GENERATESERIES ( 1, 10, 1 )";

            Assert.Equal(expectedQuery, actualQuery, ignoreWhiteSpaceDifferences: true);
        }

        [Fact]
        public void Evaluate_With_Function_GenerateSeries_Date_Test()
        {
            QueryBuilder builder = new();

            string actualQuery = builder.Evaluate(x =>
            {
                x.Functions(f => f.GenerateSeries(g =>
                {
                    g.Column(DateTime.Now, DateTime.Now.AddDays(10));
                }));
            })
                .BuildRaw();

            string expectedQuery = $@"EVALUATE GENERATESERIES ( DATE ({DateTime.Now.Year}, {DateTime.Now.Month}, {DateTime.Now.Day}), DATE ({DateTime.Now.Year}, {DateTime.Now.Month}, {DateTime.Now.AddDays(10).Day}), 1 )";

            Assert.Equal(expectedQuery, actualQuery, ignoreWhiteSpaceDifferences: true);
        }

        [Fact]
        public void Evaluate_With_Function_GenerateSeries_Time_Test()
        {
            QueryBuilder builder = new();

            string actualQuery = builder.Evaluate(x =>
            {
                x.Functions(f => f.GenerateSeries(g =>
                {
                    g.Column(TimeSpan.FromHours(1), TimeSpan.FromHours(3));
                }));
            })
                .BuildRaw();

            string expectedQuery = $@"EVALUATE GENERATESERIES ( TIME ({TimeSpan.FromHours(1).Hours}, {TimeSpan.FromHours(1).Minutes}, {TimeSpan.FromHours(1).Seconds}), TIME ({TimeSpan.FromHours(3).Hours}, {TimeSpan.FromHours(1).Minutes}, {TimeSpan.FromHours(1).Seconds}), TIME ({TimeSpan.FromHours(1).Hours}, {TimeSpan.FromHours(0).Minutes}, {TimeSpan.FromHours(0).Seconds}) )";

            Assert.Equal(expectedQuery, actualQuery, ignoreWhiteSpaceDifferences: true);
        }

        [Fact]
        public void Evaluate_With_Function_GenerateSeries_With_SelectColumns_Test()
        {
            QueryBuilder builder = new();

            string actualQuery = builder.Evaluate(x =>
            {
                x.Functions(f => f.SelectColumns(c =>
                {
                    c.Table(ff =>
                    {
                        ff.GenerateSeries(g =>
                        {
                            g.Column(DateTime.Now, DateTime.Now.AddDays(10));
                        });
                    });

                    c.Column("Time", "FORMAT ( [Value], 'YYYY:MM:dd' )");
                }));
            })
            .BuildRaw();

            string expectedQuery = $@"EVALUATE SELECTCOLUMNS ( GENERATESERIES(DATE (2021, 12, 8), DATE (2021, 12, 18), 1),'Time', FORMAT ( [Value], 'YYYY:MM:dd' ))"
                                    .Replace("'", "\"");

            Assert.Equal(expectedQuery, actualQuery, ignoreWhiteSpaceDifferences: true);
        }
    }
}
