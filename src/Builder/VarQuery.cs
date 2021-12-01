using System.Text;

namespace Builder
{
    public class VarQuery : QueryBase
    {
        public string Name { get; set; }

        public string Expression { get; set; }

        public override StringBuilder Generate(StringBuilder builder)
        {
            builder.AppendLine($"VAR {Name} = {Expression} ");

            return builder;
        }
    }
}
