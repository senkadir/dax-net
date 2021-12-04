using System;

namespace Builder
{
    public class GenerateSeriesBuilder : QueryBuilderBase
    {
        GenerateSeries _generateSeries;

        public GenerateSeriesBuilder()
        {
            _generateSeries = new();
        }

        public GenerateSeriesBuilder Column(int start, int end, int increment = 1)
        {
            _generateSeries.Columns.Add($"{start}, {end}, {increment}");

            return this;
        }

        public GenerateSeriesBuilder Column(DateTime start, DateTime end, int increment = 1)
        {
            _generateSeries.Columns.Add($"DATE ({start.Year}, {start.Month}, {start.Day})");

            _generateSeries.Columns.Add($"DATE ({end.Year}, {end.Month}, {end.Day})");

            _generateSeries.Columns.Add($"{increment}");

            return this;
        }

        public GenerateSeries Build()
        {
            return _generateSeries;
        }
    }
}
