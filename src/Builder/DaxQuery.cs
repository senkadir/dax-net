using System;
using System.Collections.Generic;
using System.Text;

namespace Builder
{
    public abstract class QueryBase
    {
        public abstract StringBuilder Generate(StringBuilder builder);
    }

    public class DaxQuery : QueryBase
    {
        public List<EvaluateQuery> Evaluates { get; set; }

        public override StringBuilder Generate(StringBuilder builder)
        {
            return builder;
        }
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

            query.Evaluates.ForEach(x => x.Generate(builder));

            return builder.Replace("'", "\"")
                          .ToString();
        }

        public string BuildRaw()
        {
            return Build().Replace(Environment.NewLine, string.Empty);
        }
    }
}
