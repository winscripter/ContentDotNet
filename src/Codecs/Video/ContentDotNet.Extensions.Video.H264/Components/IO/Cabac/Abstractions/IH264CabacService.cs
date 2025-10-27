namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.Abstractions
{
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine;

    /// <summary>
    ///   H.264 CABAC Service
    /// </summary>
    public interface IH264CabacService
    {
        /// <summary>
        ///   Creates a new CABAC Decoder instance.
        /// </summary>
        /// <param name="rawBitStream">The bit stream</param>
        /// <param name="state">The H.264 state</param>
        /// <returns>A CABAC decoder</returns>
        IH264CabacDecoder CreateDecoder(IH264ArithmeticReader rawBitStream, H264State state);
    }
}
