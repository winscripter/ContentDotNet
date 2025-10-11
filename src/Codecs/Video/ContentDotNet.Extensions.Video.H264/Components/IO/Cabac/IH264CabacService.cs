namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac
{
    using ContentDotNet.BitStream;
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine;

    /// <summary>
    ///   H.264 CABAC Service
    /// </summary>
    public interface IH264CabacService
    {
        /// <summary>
        ///   Creates a new CABAC Decoder instance.
        /// </summary>
        /// <returns>A CABAC decoder</returns>
        IH264CabacDecoder CreateDecoder(IH264ArithmeticReader rawBitStream);
    }
}
