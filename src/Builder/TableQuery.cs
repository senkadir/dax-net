using System.Text;

namespace Builder
{
    public class TableQuery : QueryBase
    {
        public string Name { get; set; }

        public string Expression { get; set; }

        public override StringBuilder Generate(StringBuilder builder)
        {
            builder.AppendLine($"TABLE {Name} = {Expression} ");

            return builder;
        }
    }
}
