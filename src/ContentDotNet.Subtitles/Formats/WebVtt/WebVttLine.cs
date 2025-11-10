namespace ContentDotNet.Subtitles.Formats.WebVtt
{
    using System;
    using System.Text;

    public class WebVttLine : ISubtitleLine
    {
        public string Text { get; set; }
        public ISubtitleStyling? Styling { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public string? Cue { get; set; }

        public WebVttLine(string text, TimeSpan start, TimeSpan end, string? cue = null)
        {
            Text = text;
            Styling = null;
            Start = start;
            End = end;
            Cue = cue;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (Cue != null)
                sb.AppendLine(Cue);
            sb.AppendLine($"{WebVttSubtitleTimeFormatter.Instance.Format(Start)} --> {WebVttSubtitleTimeFormatter.Instance.Format(End)}");
            sb.AppendLine(Text);
            return sb.ToString();
        }
    }
}
