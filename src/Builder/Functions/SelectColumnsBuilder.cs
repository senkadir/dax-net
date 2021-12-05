using System;
using System.Text;

namespace Builder
{
    public class SelectColumnsBuilder : QueryBuilderBase
    {
        SelectColumns _selectColumns;

        public SelectColumnsBuilder()
        {
            _selectColumns = new();
        }

        public SelectColumnsBuilder Table(string table)
        {
            _selectColumns.Table = table;

            return this;
        }

        public SelectColumnsBuilder Table(Action<FunctionsBuilder> functionsBuilder)
        {
            FunctionsBuilder functionsBuilderInner = new();

            functionsBuilder(functionsBuilderInner);

            var functions = functionsBuilderInner.Build();

            StringBuilder tableBuilder = new();

            functions.ForEach(x => x.Generate(tableBuilder));

            _selectColumns.Table = tableBuilder.ToString();

            return this;
        }

        public SelectColumnsBuilder Column(string expression)
        {
            _selectColumns.Columns.Add(expression);

            return this;
        }

        public SelectColumns Build()
        {
            return _selectColumns;
        }
    }
}
