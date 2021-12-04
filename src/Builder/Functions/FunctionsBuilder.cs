using System;
using System.Collections.Generic;

namespace Builder
{
    public class FunctionsBuilder : QueryBuilderBase
    {
        List<Function> _function;

        public FunctionsBuilder()
        {
            _function = new();
        }

        public FunctionsBuilder SummarizeColumns(Action<SummarizeColumnsBuilder> summarizeColumnsBuilder)
        {
            SummarizeColumnsBuilder summarizeColumnsBuilderInner = new();

            summarizeColumnsBuilder(summarizeColumnsBuilderInner);

            _function.Add(summarizeColumnsBuilderInner.Build());

            return this;
        }

        public FunctionsBuilder GenerateSeries(Action<GenerateSeriesBuilder> generateSeriesBuilder)
        {
            GenerateSeriesBuilder generateSeriesBuilderInner = new();

            generateSeriesBuilder(generateSeriesBuilderInner);

            _function.Add(generateSeriesBuilderInner.Build());

            return this;
        }

        public List<Function> Build()
        {
            return _function;
        }
    }
}
