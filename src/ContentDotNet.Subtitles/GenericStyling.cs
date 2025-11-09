namespace ContentDotNet.Subtitles
{
    using ContentDotNet.Api;
    using System.Drawing;

    /// <summary>
    ///   A line that represents everything a <see cref="ISubtitleStyling"/> can represent.
    /// </summary>
    public class GenericStyling : ISubtitleStyling
    {
        public Color BackgroundColor { get; set; }
        public Color ForegroundColor { get; set; }
        public TextDecoration TextDecoration { get; set; }

        public GenericStyling(Color backgroundColor, Color foregroundColor, TextDecoration textDecoration)
        {
            BackgroundColor = backgroundColor;
            ForegroundColor = foregroundColor;
            TextDecoration = textDecoration;
        }
    }
}
