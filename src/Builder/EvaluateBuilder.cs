using System;

namespace Builder
{
    public class EvaluateBuilder : QueryBuilderBase
    {
        EvaluateQuery _evaluate;

        public EvaluateBuilder()
        {
            _evaluate = new();

            _evaluate.Orders = new();
            _evaluate.StartAts = new();
        }

        public EvaluateBuilder Table(string table)
        {
            _evaluate.Table = table;

            return this;
        }

        internal EvaluateQuery Build()
        {
            return _evaluate;
        }

        public EvaluateBuilder OrderBy(string expression)
        {
            _evaluate.Orders.Add($"'{_evaluate.Table}'[{expression}] ASC");

            return this;
        }

        public EvaluateBuilder OrderByDescending(string expression)
        {
            _evaluate.Orders.Add($"'{_evaluate.Table}'[{expression}] DESC");

            return this;
        }

        public EvaluateBuilder StartAt(string value)
        {
            _evaluate.StartAts.Add($"\"{value}\"");

            return this;
        }

        public EvaluateBuilder StartAt(int value)
        {
            _evaluate.StartAts.Add(value.ToString());

            return this;
        }

        public EvaluateBuilder Define(Action<DefineBuilder> varBuilder)
        {
            DefineBuilder defineBuilderInner = new();

            varBuilder(defineBuilderInner);

            _evaluate.Define = defineBuilderInner.Build();

            return this;
        }

        public EvaluateBuilder Functions(Action<FunctionsBuilder> functionsBuilder)
        {
            FunctionsBuilder functionsBuilderInner = new();

            functionsBuilder(functionsBuilderInner);

            _evaluate.Functions = functionsBuilderInner.Build();

            return this;
        }
    }
}
