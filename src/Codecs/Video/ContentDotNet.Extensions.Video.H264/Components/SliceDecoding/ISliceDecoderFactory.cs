namespace ContentDotNet.Extensions.Video.H264.Components.SliceDecoding
{
    /// <summary>
    ///   Factory for slice decoders
    /// </summary>
    public interface ISliceDecoderFactory
    {
        /// <summary>
        ///   Creates the slice decoder.
        /// </summary>
        /// <returns>The slice decoder.</returns>
        ISliceDecoder CreateSliceDecoder();
    }
}
