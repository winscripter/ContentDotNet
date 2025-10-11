namespace ContentDotNet.Extensions.Video.H264.Extensions
{
    using ContentDotNet.Extensions.Video.H264.RbspModels;
    using System.Drawing;

    /// <summary>
    ///   Extension methods for <see cref="AbstractH264Decoder"/>.
    /// </summary>
    public static class AbstractH264DecoderExtensions
    {
        /// <summary>
        ///   Returns the cropping rectangle, or <see langword="null"/> if missing.
        /// </summary>
        /// <param name="decoder">The source H.264 decoder.</param>
        /// <returns>The cropping rectangle or null.</returns>
        public static Rectangle? CroppingRectangleOrNull(
            this AbstractH264Decoder decoder)
        {
            RbspSequenceParameterSetData? sps = decoder.FetchSps();
            if (sps == null)
            {
                return null;
            }
            Rectangle rect = new(
                (int)sps.FrameCropLeftOffset,
                (int)sps.FrameCropTopOffset,
                (int)sps.FrameCropRightOffset - (int)sps.FrameCropLeftOffset,
                (int)sps.FrameCropBottomOffset - (int)sps.FrameCropTopOffset);
            return rect;
        }
    }
}
