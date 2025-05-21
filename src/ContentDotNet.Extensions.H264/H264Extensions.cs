using ContentDotNet.Extensions.H264.Models;
using ContentDotNet.Primitives;
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

    /// <summary>
    ///   Returns the picture width in samples for the Chroma channel.
    /// </summary>
    /// <param name="sps">SPS</param>
    /// <returns>Picture width in samples for the Chroma channel.</returns>
    public static int GetPicWidthInSamplesC(this SequenceParameterSet sps)
    {
        if (sps.ChromaFormatIdc == 0)
            return 0;

        if (sps.ChromaFormatIdc == 3)
            return sps.GetPicWidthInSamplesL() * (2 - Int32Boolean.I32(sps.FrameMbsOnlyFlag));

        return (sps.GetPicWidthInSamplesL() / 2) * (2 - Int32Boolean.I32(sps.FrameMbsOnlyFlag));
    }

    /// <summary>
    ///   Returns the picture height in samples for the Chroma channel.
    /// </summary>
    /// <param name="sps">SPS</param>
    /// <returns>Picture height in samples for the Chroma channel.</returns>
    public static int GetPicHeightInSamplesC(this SequenceParameterSet sps)
    {
        if (sps.ChromaFormatIdc == 0)
            return 0;

        if (sps.ChromaFormatIdc == 3)
            return sps.GetPicHeightInSamplesL(); // no subsampling in height

        return sps.GetPicHeightInSamplesL() / 2; // 4:2:0 or 4:2:2
    }

    /// <summary>
    ///   Gets the coded block pattern for the luma component of the macroblock.
    /// </summary>
    /// <param name="mbLayer">The macroblock layer.</param>
    /// <returns>The coded block pattern for luma (0-15).</returns>
    public static int GetCodedBlockPatternLuma(this MacroblockLayer mbLayer) => mbLayer.CodedBlockPattern % 16;

    /// <summary>
    ///   Gets the coded block pattern for the chroma component of the macroblock.
    /// </summary>
    /// <param name="mbLayer">The macroblock layer.</param>
    /// <returns>The coded block pattern for chroma (0-3).</returns>
    public static int GetCodedBlockPatternChroma(this MacroblockLayer mbLayer) => mbLayer.CodedBlockPattern / 16;

    /// <summary>
    ///   Returns the residual block for the given chroma component (see <see cref="Residual.CabacCbCr"/>).
    ///   <paramref name="iCbCr"/> must be 0 or 1.
    /// </summary>
    /// <param name="residual">Input residual.</param>
    /// <param name="iCbCr">0 if Cb; 1 if Cr.</param>
    /// <returns>
    /// A CABAC residual indexed from <see cref="Residual.CabacCbCr"/>, or <see langword="null"/> if
    /// the <see cref="Residual.CabacCbCr"/> property is <see langword="null"/>, which is under one
    /// of the following cases:
    /// <list type="number">
    ///   <item>The <see cref="Residual.PreferCabac"/> property is <see langword="false"/>,</item>
    ///   <item>The <c>ChromaArrayType</c> wasn't 1 and wasn't 2.</item>
    /// </list>
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="iCbCr"/> is neither 0 nor 1</exception>
    public static CabacResidual? GetCbCrResidualBlockCabac(this Residual residual, int iCbCr)
    {
        if (iCbCr is not 0 or 1)
            throw new ArgumentOutOfRangeException(nameof(iCbCr), "iCbCr must be 0 or 1.");

        return iCbCr == 0 ? residual.CabacCbCr?.First : residual.CabacCbCr?.Second;
    }

    /// <summary>
    ///   Returns the residual block for the given chroma component (see <see cref="Residual.CavlcCbCr"/>).
    ///   <paramref name="iCbCr"/> must be 0 or 1.
    /// </summary>
    /// <param name="residual">Input residual.</param>
    /// <param name="iCbCr">0 if Cb; 1 if Cr.</param>
    /// <returns>
    /// A CAVLC residual indexed from <see cref="Residual.CavlcCbCr"/>, or <see langword="null"/> if
    /// the <see cref="Residual.CavlcCbCr"/> property is <see langword="null"/>, which is under one
    /// of the following cases:
    /// <list type="number">
    ///   <item>The <see cref="Residual.PreferCabac"/> property is <see langword="true"/>,</item>
    ///   <item>The <c>ChromaArrayType</c> wasn't 1 and wasn't 2.</item>
    /// </list>
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="iCbCr"/> is neither 0 nor 1</exception>
    public static CavlcResidual? GetCbCrResidualBlockCavlc(this Residual residual, int iCbCr)
    {
        if (iCbCr is not 0 or 1)
            throw new ArgumentOutOfRangeException(nameof(iCbCr), "iCbCr must be 0 or 1.");

        return iCbCr == 0 ? residual.CavlcCbCr?.First : residual.CavlcCbCr?.Second;
    }
}
