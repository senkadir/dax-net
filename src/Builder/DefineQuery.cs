using System.Collections.Generic;
using System.Text;

namespace Builder
{
    public class DefineQuery : QueryBase
    {
        public List<VarQuery> Vars { get; set; }

        public List<ColumnQuery> Columns { get; set; }

        public List<MeasureQuery> Measures { get; set; }

        public List<TableQuery> Tables { get; set; }

        public override StringBuilder Generate(StringBuilder builder)
        {
            builder.AppendLine("DEFINE ");

            Vars.ForEach(x => x.Generate(builder));

            Columns.ForEach(x => x.Generate(builder));

            Measures.ForEach(x => x.Generate(builder));

            Tables.ForEach(x => x.Generate(builder));

            return builder;
        }
    }
}
