namespace ContentDotNet.Rtsp.Sdp.Abstractions
{
    /// <summary>
    ///   Abstracts a model for an SDP line.
    /// </summary>
    public interface ISdpLineModel
    {
        /// <summary>
        ///   The character that appears before the equals character.
        /// </summary>
        char Character { get; }

        /// <summary>
        ///   The actual raw SDP line, including the character and the equal sign.
        /// </summary>
        string? RawText { get; set; }

        /// <summary>
		///   Returns a boolean indicating if this SDP line is syntactically correct.
		/// </summary>
		/// <returns>
		///   <see langword="true" /> if all items in this SDP line are valid and can be decoded meaningfully.
		///   Otherwise, <see langword="false" /> if there's at least one incorrect part of this line.
		/// </returns>
        bool IsValid();
    }
}
