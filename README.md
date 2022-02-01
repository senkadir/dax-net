# dax-net
Create DAX queries with C# with fluent syntax.

          QueryBuilder builder = new();
            string actualQuery = builder.Evaluate(x =>
            {
                x.Functions(f => f.SummarizeColumns(c =>
                 {
                     c.Column("'Date Dim'[Day name]");
                     c.Column("'Date Dim'[Day name abbreviated]");
                 }));
            })
                .BuildRaw();
                
                Result:

            EVALUATE 
               SUMMARIZECOLUMNS 
                (
                 'Date Dim'[Day name], 
                 'Date Dim'[Day name abbreviated]
                )
