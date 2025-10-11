namespace ContentDotNet.Extensions.Video.H264.Components.SliceDecoding
{
    /// <summary>
    ///   Default slice decoder factory.
    /// </summary>
    public class DefaultSliceDecoderFactory : ISliceDecoderFactory
    {
        /// <summary>
        ///   Singleton instance of <see cref="DefaultSliceDecoderFactory"/>.
        /// </summary>
        public static readonly DefaultSliceDecoderFactory Instance = new();

        /// <summary>
        ///   Creates a new slice decoder.
        /// </summary>
        /// <returns>The slice decoder.</returns>
        public ISliceDecoder CreateSliceDecoder()
        {
            return SliceDecoderService.Instance;
        }
    }
}
