namespace ContentDotNet.Extensions.Video.H264.Components.IO.Abstractions
{
    using ContentDotNet.BitStream;

    /// <summary>
    ///   Factory for H.264 syntax readers
    /// </summary>
    public interface IH264SyntaxReaderFactory
    {
        /// <summary>
        ///   Creates a syntax reader.
        /// </summary>
        /// <param name="reader">The H.264 reader</param>
        /// <param name="state">The H.264 state</param>
        /// <returns>The syntax reader.</returns>
        IH264SyntaxReader CreateSyntaxReader(H264State state, BitStreamReader reader);
    }
}
