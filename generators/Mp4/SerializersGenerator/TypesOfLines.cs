namespace SerializersGenerator
{
    internal abstract class Line
    {
        public abstract string Name { get; set; }
        public abstract string Raw { get; set; }
        public abstract string Type { get; set; }
    }

    internal class FiniteArrayLine : Line
    {
        public override required string Name { get; set; }
        public override required string Raw { get; set; }
        public override required string Type { get; set; }

        public required string Limit { get; set; }

        public override string ToString()
        {
            return $"{Type} {Name}[{Limit}]";
        }
    }

    internal class UntilBoxEndArrayLine : Line
    {
        public override required string Name { get; set; }
        public override required string Raw { get; set; }
        public override required string Type { get; set; }

        public override string ToString()
        {
            return $"{Type} {Name}[]";
        }
    }

    internal class SimpleLine : Line
    {
        public override required string Name { get; set; }
        public override required string Raw { get; set; }
        public override required string Type { get; set; }
    }
}
