namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ContextIndexModel
{
    using ContentDotNet.Extensions.Video.H264.Components.IO.Abstractions.Cabac;
    using ContentDotNet.Extensions.Video.H264.Enumerations;

    /// <summary>
    ///   The context index and parser.
    /// </summary>
    /// <param name="Record">Context index</param>
    /// <param name="Callback">Parser</param>
    public record ContextIndexAndParser(UnprocessedContextIndexRecord Record, FireParserCallback Callback)
    {
        /// <summary>
        ///   Performs binarization and parses the result.
        /// </summary>
        /// <param name="decoder">Source decoder</param>
        /// <param name="sliceType">The slice type</param>
        /// <returns>Binarized integer value</returns>
        public int Parse(IH264CabacDecoder decoder, H264SliceType sliceType) => Callback(decoder, Record, sliceType);
    }
}
