namespace ContentDotNet.Subtitles
{
    using System;

    /// <summary>
    ///   A line that only accepts text.
    /// </summary>
    public abstract class TextOnlyLine : ISubtitleLine
    {
        public string Text { get; set; }
        public ISubtitleStyling? Styling { get => throw new InvalidOperationException(); set => throw new InvalidOperationException(); }
        public TimeSpan Start { get => throw new InvalidOperationException(); set => throw new InvalidOperationException(); }
        public TimeSpan End { get => throw new InvalidOperationException(); set => throw new InvalidOperationException(); }

        protected TextOnlyLine(string text)
        {
            Text = text;
        }
    }
}
