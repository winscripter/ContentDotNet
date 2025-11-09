namespace ContentDotNet.Subtitles
{
    using ContentDotNet.Api;
    using System.Drawing;

    /// <summary>
    ///   Styling of a subtitle line.
    /// </summary>
    public interface ISubtitleStyling
    {
        /// <summary>
        ///   The background color
        /// </summary>
        Color BackgroundColor { get; set; }

        /// <summary>
        ///   The foreground color
        /// </summary>
        Color ForegroundColor { get; set; }

        /// <summary>
        ///   The text decoration
        /// </summary>
        TextDecoration TextDecoration { get; set; }
    }
}
