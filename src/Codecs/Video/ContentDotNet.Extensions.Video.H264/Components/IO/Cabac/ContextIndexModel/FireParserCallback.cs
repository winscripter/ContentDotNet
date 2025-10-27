namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ContextIndexModel
{
    using ContentDotNet.Extensions.Video.H264.Components.IO.Abstractions.Cabac;
    using ContentDotNet.Extensions.Video.H264.Enumerations;

    /// <summary>
    ///   Delegate to invoke the actual CABAC binarizer.
    /// </summary>
    /// <param name="decoder">The decoder</param>
    /// <param name="rec">The record</param>
    /// <param name="sliceType">The slice type</param>
    /// <returns>Result of the binarization</returns>
    public delegate int FireParserCallback(IH264CabacDecoder decoder, UnprocessedContextIndexRecord rec, H264SliceType sliceType);
}
