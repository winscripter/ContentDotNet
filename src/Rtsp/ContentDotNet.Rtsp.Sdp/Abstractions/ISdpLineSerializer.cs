namespace ContentDotNet.Rtsp.Sdp.Abstractions
{
    /// <summary>
    ///   Abstract, non-generic SDP line serializer.
    /// </summary>
    public interface ISdpLineSerializer
    {
        /// <summary>
        ///   Writes the source SDP line model to the specified text writer.
        /// </summary>
        /// <param name="model">The SDP model</param>
        /// <param name="writer">The output text writer</param>
        void Write(ISdpLineModel model, TextWriter writer);

        /// <summary>
        ///   Writes the source SDP line model to the specified text writer.
        /// </summary>
        /// <param name="model">The SDP model</param>
        /// <param name="writer">The output text writer</param>
        Task WriteAsync(ISdpLineModel model, TextWriter writer);

        /// <summary>
        ///   Reads the SDP line model from the specified text reader.
        /// </summary>
        /// <param name="reader">The text reader to read from.</param>
        /// <returns>The parsed SDP line model.</returns>
        ISdpLineModel Read(TextReader reader);

        /// <summary>
        ///   Reads the SDP line model from the specified text reader.
        /// </summary>
        /// <param name="reader">The text reader to read from.</param>
        /// <returns>The parsed SDP line model.</returns>
        Task<ISdpLineModel> ReadAsync(TextReader reader);
    }
}
