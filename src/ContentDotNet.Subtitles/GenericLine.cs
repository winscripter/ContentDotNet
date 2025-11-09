namespace ContentDotNet.Subtitles
{
    using System;

    /// <summary>
    ///   A line that represents everything a <see cref="ISubtitleLine"/> can represent.
    /// </summary>
    public class GenericLine : ISubtitleLine
    {
        public string Text { get; set; }
        public ISubtitleStyling? Styling { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }

        public GenericLine(string text, ISubtitleStyling? styling, TimeSpan start, TimeSpan end)
        {
            Text = text;
            Styling = styling;
            Start = start;
            End = end;
        }
    }
}
