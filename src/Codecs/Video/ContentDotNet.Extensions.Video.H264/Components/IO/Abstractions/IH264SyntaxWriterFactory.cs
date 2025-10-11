namespace ContentDotNet.Extensions.Video.H264.Components.IO.Abstractions
{
    using ContentDotNet.BitStream;

    /// <summary>
    ///   The H.264 syntax writer factory.
    /// </summary>
    public interface IH264SyntaxWriterFactory
    {
        /// <summary>
        ///   Creates an H.264 syntax writer.
        /// </summary>
        /// <param name="state">The H.264 state</param>
        /// <param name="writer">The bitstream writer</param>
        /// <returns>The H.264 syntax writer</returns>
        IH264SyntaxWriter CreateH264SyntaxWriter(H264State state, BitStreamWriter writer);
    }
}
