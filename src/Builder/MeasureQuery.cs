using System.Text;

namespace Builder
{
    public class MeasureQuery : QueryBase
    {
        public string Name { get; set; }

        public string Expression { get; set; }

        public override StringBuilder Generate(StringBuilder builder)
        {
            builder.AppendLine($"MEASURE {Name} = {Expression} ");

            return builder;
        }
    }
}
