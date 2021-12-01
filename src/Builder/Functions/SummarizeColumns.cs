using System;
using System.Collections.Generic;
using System.Text;

namespace Builder
{
    public class SummarizeColumns : Function
    {
        public List<string> Columns { get; set; }

        public override StringBuilder Generate(StringBuilder builder)
        {
            builder.AppendLine("SUMMARIZECOLUMNS (");

            builder.AppendJoin($", {Environment.NewLine}", Columns);

            builder.AppendLine(")");

            return builder;
        }
    }

    public class SummarizeColumnsBuilder : QueryBuilderBase
    {
        SummarizeColumns _summarizeColumns;

        public SummarizeColumnsBuilder()
        {
            _summarizeColumns = new();

            _summarizeColumns.Columns = new();
        }

        public SummarizeColumnsBuilder Column(string expression)
        {
            _summarizeColumns.Columns.Add(expression);

            return this;
        }

        public SummarizeColumnsBuilder Columns(string[] columnExpressions)
        {
            foreach (var expression in columnExpressions)
            {
                _summarizeColumns.Columns.Add(expression);
            }

            return this;
        }

        public SummarizeColumns Build()
        {
            return _summarizeColumns;
        }
    }

    public abstract class Function : QueryBase
    {

    }

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

        public List<Function> Build()
        {
            return _function;
        }
    }
}
