using ContentDotNet.Extensions.H264.Models;
using ContentDotNet.Extensions.H264.Utilities;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace ContentDotNet.Extensions.H264;

/// <summary>
///   Various H.264 related extension methods.
/// </summary>
public static class H264Extensions
{
    /// <summary>
    ///   Is the given NAL unit an IDR NAL unit?
    /// </summary>
    /// <param name="nal">NAL unit</param>
    /// <returns>A boolean that indicates whether its type is 6.</returns>
    public static bool IsIdr(this NalUnit nal) => nal.NalUnitType == 6;

    /// <summary>
    ///   Is the given NAL unit an SPS NAL unit?
    /// </summary>
    /// <param name="nal">NAL unit</param>
    /// <returns>A boolean that indicates whether its type is 7.</returns>
    public static bool IsSps(this NalUnit nal) => nal.NalUnitType == 7;

    /// <summary>
    ///   Is the given NAL unit a PPS NAL unit?
    /// </summary>
    /// <param name="nal">NAL unit</param>
    /// <returns>A boolean that indicates whether its type is 8.</returns>
    public static bool IsPps(this NalUnit nal) => nal.NalUnitType == 8;

    /// <summary>
    ///   Returns the Chroma Array Type from the SPS.
    /// </summary>
    /// <param name="sps">SPS</param>
    /// <returns>Chroma array type</returns>
    public static uint GetChromaArrayType(this SequenceParameterSet sps) => sps.SeparateColourPlaneFlag ? 0 : sps.ChromaFormatIdc;

    /// <summary>
    ///   Returns the size of the H.264 frame, in pixels. Also accounts for frame cropping scenarios and
    ///   Sample Aspect Ratio (SAR) in VUI parameters, if present.
    /// </summary>
    /// <param name="sps">All options representing the image size is taken from the SPS.</param>
    /// <returns>Size of the frame, in pixels.</returns>
    public static Size GetFrameSize(this SequenceParameterSet sps)
    {
        uint width = (sps.PicWidthInMbsMinus1 + 1) * 16;
        uint height = (sps.PicHeightInMapUnitsMinus1 + 1) * 16;

        if (sps.FrameCroppingFlag)
        {
            width -= sps.FrameCropLeftOffset + sps.FrameCropRightOffset;
            height -= sps.FrameCropTopOffset + sps.FrameCropBottomOffset;
        }

        if (sps.VuiParametersPresentFlag && sps.VuiParameters is VuiParameters vuip && vuip.SarWidth != 0 && vuip.SarHeight != 0)
        {
            double sarRatio = (double)vuip.SarWidth / vuip.SarHeight;
            width = (uint)(width * sarRatio);
        }

        return new Size((int)width, (int)height);
    }

    /// <summary>
    ///   Returns the picture size in map units from the SPS.
    /// </summary>
    /// <param name="sps">SPS to take picture size in map units of.</param>
    /// <returns>Picture size in map units</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetPicSizeInMapUnits(this SequenceParameterSet sps) =>
        (int)((sps.PicWidthInMbsMinus1 + 1) * ((2 - Int32Boolean.I32(sps.FrameMbsOnlyFlag)) * (sps.PicHeightInMapUnitsMinus1 + 1)));

    /// <summary>
    ///   Returns the picture height in samples for the Luma channel.
    /// </summary>
    /// <param name="sps">SPS</param>
    /// <returns>Picture height in samples for the Luma channel.</returns>
    public static int GetPicHeightInSamplesL(this SequenceParameterSet sps)
    {
        int picHeightInMapUnits = (int)sps.PicHeightInMapUnitsMinus1 + 1;
        int frameHeightInMbs = picHeightInMapUnits;
        if (!sps.FrameMbsOnlyFlag)
            frameHeightInMbs *= 2;
        return frameHeightInMbs * 16;
    }

    /// <summary>
    ///   Returns the picture width in samples for the Luma channel.
    /// </summary>
    /// <param name="sps">SPS</param>
    /// <returns>Picture width in samples for the Luma channel.</returns>
    public static int GetPicWidthInSamplesL(this SequenceParameterSet sps)
    {
        int picWidthInMbs = (int)sps.PicWidthInMbsMinus1 + 1;
        int frameWidthInMbs = picWidthInMbs;
        if (!sps.FrameMbsOnlyFlag)
            frameWidthInMbs *= 2;
        return frameWidthInMbs * 16;
    }
}
