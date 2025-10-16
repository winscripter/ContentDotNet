namespace SerializersGenerator
{
    using System.Collections.Specialized;
    using System.Text.RegularExpressions;

    internal class ClassGenerator
    {
        private readonly NameValueCollection nvcList;
        private readonly TextReader iocode;

        public ClassGenerator(NameValueCollection nvcList, TextReader iocode)
        {
            this.nvcList = nvcList;
            this.iocode = iocode;
        }
    }

    internal static partial class LineParser
    {
        [GeneratedRegex(@"(?<typeOfLine>\s*(unsigned int|uint|int|signed int|char|utf8string|string)\s*)(?<parentheses>\s*\([a-zA-Z0-9_]*\)\s*)?")]
        public static partial Regex TypeRegex();
    }
}
