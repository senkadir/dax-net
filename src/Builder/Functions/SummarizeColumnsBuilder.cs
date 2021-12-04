namespace Builder
{
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
}
