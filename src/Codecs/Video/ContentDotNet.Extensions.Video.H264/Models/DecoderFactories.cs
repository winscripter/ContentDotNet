namespace ContentDotNet.Extensions.Video.H264.Models
{
    using ContentDotNet.Extensions.Video.H264.Components.SliceDecoding;

    /// <summary>
    ///   A collection of factories used by the H.264 decoder.
    /// </summary>
    public class DecoderFactories
    {
        /// <summary>
        ///   The slice decoder factory.
        /// </summary>
        public ISliceDecoderFactory SliceDecoderFactory { get; set; } = DefaultSliceDecoderFactory.Instance;
    }
}
