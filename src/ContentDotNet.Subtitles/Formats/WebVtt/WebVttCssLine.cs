namespace ContentDotNet.Subtitles.Formats.WebVtt
{
    using System.Text;

    /// <summary>
    ///   CSS WebVTT line.
    /// </summary>
    public class WebVttCssLine : TextOnlyLine
    {
        public WebVttCssLine(string cssText)
            : base(cssText)
        {
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("STYLE");
            sb.AppendLine(Text);
            return sb.ToString();
        }
    }
}
