using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac;

namespace ContentDotNet.Extensions.Video.H264.Extensions
{
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
            decoder.Affix == H264Affix.Prefix ? decoder.CtxIdxPrefix : decoder.CtxIdxSuffix;
    }
}
