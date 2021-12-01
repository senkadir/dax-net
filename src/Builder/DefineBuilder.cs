namespace Builder
{
    public class DefineBuilder
    {
        DefineQuery query;

        public DefineBuilder()
        {
            query = new();

            query.Vars = new();
            query.Columns = new();
            query.Measures = new();
            query.Tables = new();
        }

        public DefineBuilder Var(string name, string expression)
        {
            query.Vars.Add(new VarQuery
            {
                Name = name,
                Expression = expression
            });

            return this;
        }

        public DefineBuilder Column(string name, string expression)
        {
            query.Columns.Add(new ColumnQuery
            {
                Name = name,
                Expression = expression
            });

            return this;
        }

        public DefineBuilder Mesaure(string name, string expression)
        {
            query.Measures.Add(new MeasureQuery
            {
                Name = name,
                Expression = expression
            });

            return this;
        }

        public DefineBuilder Table(string name, string expression)
        {
            query.Tables.Add(new TableQuery
            {
                Name = name,
                Expression = expression
            });

            return this;
        }

        public DefineQuery Build()
        {
            return query;
        }
    }
}
