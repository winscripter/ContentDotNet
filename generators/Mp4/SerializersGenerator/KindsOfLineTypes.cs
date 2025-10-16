namespace SerializersGenerator
{
    internal enum KindOfType
    {
        UInt,
        Int,
        Char,
        Utf8String,
        String
    }

    internal abstract class LineTypeKind
    {
        public abstract string Raw { get; set; }
    }

    internal class SimpleLineType : LineTypeKind
    {
        public override required string Raw { get; set; }
        public required KindOfType KindOfType { get; set; }
        public required string? InsideParentheses { get; set; }

        public override string ToString()
        {
            if (InsideParentheses == null)
                return $"{KindOfType.ToString().ToLower()}";
            else
                return $"{KindOfType.ToString().ToLower()}({InsideParentheses})";
        }
    }

    internal class ArrayLineType : LineTypeKind
    {
        public override required string Raw { get; set; }
        public required LineTypeKind Inner { get; set; }
        public required string ArrayLimit { get; set; }

        public override string ToString()
        {
            return $"{Inner}[{ArrayLimit}]";
        }
    }
}
