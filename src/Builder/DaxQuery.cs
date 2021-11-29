using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Builder
{
    public class DaxQuery
    {
        public List<EvaluateQuery> Evaluates { get; set; }
    }

    public interface IQueryBuilder
    {
        IQueryBuilder Evaluate(Action<EvaluateBuilder> evaluateBuilder);

        string Build();

        string BuildRaw();
    }

    public abstract class QueryBuilderBase
    {
    }

    public class QueryBuilder : QueryBuilderBase, IQueryBuilder
    {
        private DaxQuery query;

        public QueryBuilder()
        {
            query = new DaxQuery
            {
                Evaluates = new()
            };
        }

        public IQueryBuilder Evaluate(Action<EvaluateBuilder> evaluateBuilder)
        {
            EvaluateBuilder evaluateBuilderInner = new();

            evaluateBuilder(evaluateBuilderInner);

            query.Evaluates.Add(evaluateBuilderInner.Build());

            return this;
        }

        public string Build()
        {
            StringBuilder builder = new();

            foreach (var evaluate in query.Evaluates)
            {
                builder.AppendLine("EVALUATE(")
                       .AppendLine($"'{evaluate.Table}'")
                       .AppendLine(")");

                if (evaluate.Orders.Any())
                {
                    builder.AppendLine($" ORDER BY ");

                    builder.AppendJoin($",{Environment.NewLine} ", evaluate.Orders);
                }

                if (evaluate.Orders.Any() && evaluate.StartAts.Any())
                {
                    builder.AppendLine($" START AT ");

                    builder.AppendJoin($",{Environment.NewLine} ", evaluate.StartAts);
                }
            }

            return builder.ToString();
        }

        public string BuildRaw()
        {
            return Build().Replace(Environment.NewLine, string.Empty);
        }
    }

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
    }

    public class EvaluateQuery
    {
        public string Table { get; set; }

        public List<string> Orders { get; set; }

        public List<string> StartAts { get; set; }
    }
}
