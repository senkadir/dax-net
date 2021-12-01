using System.Text;

namespace Builder
{
    public class ColumnQuery : QueryBase
    {
        public string Name { get; set; }

        public string Expression { get; set; }

        public override StringBuilder Generate(StringBuilder builder)
        {
            builder.AppendLine($"COLUMN {Name} = {Expression} ");

            return builder;
        }
    }
}
