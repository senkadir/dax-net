using System;
using System.Collections.Generic;
using System.Text;

namespace Builder
{
    public class GenerateSeries : Function
    {
        public List<string> Columns { get; set; }

        public GenerateSeries()
        {
            Columns = new();
        }

        public override StringBuilder Generate(StringBuilder builder)
        {
            builder.AppendLine($"GENERATESERIES");
            builder.AppendLine($"(");

            builder.AppendLine(string.Join($", {Environment.NewLine}", Columns));

            builder.AppendLine($")");

            return builder;
        }
    }
}
