namespace ContentDotNet.Extensions.Video.H264.SeiModel.Abstractions
{
    using ContentDotNet.BitStream;

    /// <summary>
    ///   Provides access to the SEI model I/O.
    /// </summary>
    public interface IH264SeiIO<TSeiElement>
    {
        /// <summary>
        ///   Reads the SEI model.
        /// </summary>
        /// <param name="rbspState">The RBSP state</param>
        /// <param name="bitStreamReader">The bitstream reader</param>
        /// <returns>The parsed SEI element.</returns>
        TSeiElement Read(H264RbspState rbspState, BitStreamReader bitStreamReader);

        /// <summary>
        ///   Reads the SEI model.
        /// </summary>
        /// <param name="rbspState">The RBSP state</param>
        /// <param name="bitStreamReader">The bitstream reader</param>
        /// <returns>The parsed SEI element.</returns>
        Task<TSeiElement> ReadAsync(H264RbspState rbspState, BitStreamReader bitStreamReader);

        /// <summary>
        ///   Writes the SEI element.
        /// </summary>
        /// <param name="element">The SEI element to write.</param>
        /// <param name="bitStreamWriter">The bit-stream writer</param>
        /// <param name="rbspState">The RBSP state</param>
        void Write(TSeiElement element, BitStreamWriter bitStreamWriter, H264RbspState rbspState);

        /// <summary>
        ///   Writes the SEI element.
        /// </summary>
        /// <param name="element">The SEI element to write.</param>
        /// <param name="bitStreamWriter">The bit-stream writer</param>
        /// <param name="rbspState">The RBSP state</param>
        Task WriteAsync(TSeiElement element, BitStreamWriter bitStreamWriter, H264RbspState rbspState);
    }
}
