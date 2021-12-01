using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Builder
{
    public class EvaluateQuery : QueryBase
    {
        public string Table { get; set; }

        public DefineQuery Define { get; set; }

        public List<string> Orders { get; set; }

        public List<string> StartAts { get; set; }

        public List<Function> Functions { get; set; }

        public override StringBuilder Generate(StringBuilder builder)
        {
            if (Define != null)
            {
                Define.Generate(builder);
            }

            builder.AppendLine("EVALUATE ");

            if (!string.IsNullOrEmpty(Table))
            {
                builder.AppendLine($"'{Table}'");
            }

            Functions.ForEach(x => x.Generate(builder));

            if (Orders.Any())
            {
                builder.AppendLine($" ORDER BY ");

                builder.AppendJoin($",{Environment.NewLine} ", Orders);
            }

            if (Orders.Any() && StartAts.Any())
            {
                builder.AppendLine($" START AT ");

                builder.AppendJoin($",{Environment.NewLine} ", StartAts);
            }

            return builder;
        }
    }
}
