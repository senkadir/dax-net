# dax-net
This library's focuses to create DAX queries with C# as fluent syntax.

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
