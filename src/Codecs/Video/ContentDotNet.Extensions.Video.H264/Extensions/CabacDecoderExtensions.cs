namespace ContentDotNet.Extensions.Video.H264.Extensions
{
    using ContentDotNet.Extensions.Video.H264.Components.IO.Abstractions.Cabac;
    using ContentDotNet.Extensions.Video.H264.Enumerations;

    /// <summary>
    ///   Extensions for <see cref="IH264CabacDecoder"/>.
    /// </summary>
    public static class CabacDecoderExtensions
    {
        /// <summary>
        ///   Returns ctxIdx.
        /// </summary>
        /// <param name="decoder">The decoder</param>
        /// <returns>The ctxIdx</returns>
        public static int GetCtxIdx(this IH264CabacDecoder decoder) =>
            decoder.Affix == H264Affix.Prefix ? decoder.PrefixContextIndex : decoder.SuffixContextIndex;

        /// <summary>
        ///   Does the specified decoder actively use prefix-only coding for the binarization?
        /// </summary>
        /// <param name="decoder">The input CABAC decoder</param>
        /// <returns>A boolean</returns>
        public static bool IsPrefixOnly(
            this IH264CabacDecoder decoder)
            => decoder.ContextIndexRecord?.CtxIdxOffset?.HasSuffix == false;
    }
}
