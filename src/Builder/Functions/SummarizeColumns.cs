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
}
