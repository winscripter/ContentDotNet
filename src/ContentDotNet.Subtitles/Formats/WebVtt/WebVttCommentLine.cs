namespace ContentDotNet.Subtitles.Formats.WebVtt
{
    using System.Text;

    /// <summary>
    ///   Comment WebVTT line
    /// </summary>
    public class WebVttCommentLine : TextOnlyLine
    {
        public WebVttCommentLine(string rawText)
            : base(rawText)
        {
        }

        public static WebVttCommentLine Create(string commentText, bool prependAndAppendWhitespaceChar = false)
        {
            var sb = new StringBuilder();
            sb.Append("/*");
            if (prependAndAppendWhitespaceChar)
                sb.Append(' ');
            sb.Append(commentText);
            if (prependAndAppendWhitespaceChar)
                sb.Append(' ');
            sb.Append("*/");
            return new(sb.ToString());
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
