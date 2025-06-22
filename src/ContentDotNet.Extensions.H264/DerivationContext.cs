using ContentDotNet.Extensions.H264.Macroblocks;

namespace ContentDotNet.Extensions.H264;

/// <summary>
/// Represents the derivation context used in H.264 processing.
/// </summary>
public sealed class DerivationContext : IEquatable<DerivationContext>
{
    /// <summary>
    /// Gets or sets the macroblock address X.
    /// </summary>
    public int MbAddrX;

    /// <summary>
    /// Gets or sets a value indicating whether the macroblock is in MBAFF mode.
    /// </summary>
    public bool IsMbaff { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the macroblock is a field macroblock in MBAFF mode.
    /// </summary>
    public bool IsMbaffFieldMacroblock { get; set; }

    /// <summary>
    /// Gets or sets the chroma macroblock sizes.
    /// </summary>
    public MacroblockSizeChroma Sizes { get; set; }

    /// <summary>
    /// Gets or sets the neighboring macroblocks.
    /// </summary>
    public NeighboringMacroblocks NeighboringMacroblocks { get; set; }

    /// <summary>
    /// Gets or sets the current macroblock address.
    /// </summary>
    public int CurrMbAddr { get; set; }

    /// <summary>
    /// Gets or sets the macroblock type.
    /// </summary>
    public int MbType { get; set; }

    /// <summary>
    /// Gets or sets the sub-macroblock type.
    /// </summary>
    public int SubMbType { get; set; }

    /// <summary>
    /// Gets or sets the picture width in samples for the luma component.
    /// </summary>
    public int PictureWidthInSamplesL { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the macroblock address X is in frame mode.
    /// </summary>
    public bool MbAddrXFrameFlag { get; set; }

    /// <summary>
    /// Gets or sets the bit depth for the luma component.
    /// </summary>
    public int BitDepthY { get; set; }

    /// <summary>
    /// Gets or sets the bit depth for the chroma component.
    /// </summary>
    public int BitDepthC { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DerivationContext"/> struct.
    /// </summary>
    /// <param name="mbAddrX">The macroblock address X.</param>
    /// <param name="isMbaff">Indicates whether the macroblock is in MBAFF mode.</param>
    /// <param name="isMbaffFieldMacroblock">Indicates whether the macroblock is a field macroblock in MBAFF mode.</param>
    /// <param name="sizes">The chroma macroblock sizes.</param>
    /// <param name="neighboringMacroblocks">The neighboring macroblocks.</param>
    /// <param name="currMbAddr">The current macroblock address.</param>
    /// <param name="mbType">The macroblock type.</param>
    /// <param name="subMbType">The sub-macroblock type.</param>
    /// <param name="pictureWidthInSamplesL">The picture width in samples for the luma component.</param>
    /// <param name="mbAddrXFrameFlag">Indicates whether the macroblock address X is in frame mode.</param>
    /// <param name="bitDepthY">The bit depth for the luma component.</param>
    /// <param name="bitDepthC">The bit depth for the chroma component.</param>
    public DerivationContext(
        int mbAddrX,
        bool isMbaff,
        bool isMbaffFieldMacroblock,
        MacroblockSizeChroma sizes,
        NeighboringMacroblocks neighboringMacroblocks,
        int currMbAddr,
        int mbType,
        int subMbType,
        int pictureWidthInSamplesL,
        bool mbAddrXFrameFlag,
        int bitDepthY,
        int bitDepthC)
    {
        MbAddrX = mbAddrX;
        IsMbaff = isMbaff;
        IsMbaffFieldMacroblock = isMbaffFieldMacroblock;
        Sizes = sizes;
        NeighboringMacroblocks = neighboringMacroblocks;
        CurrMbAddr = currMbAddr;
        MbType = mbType;
        SubMbType = subMbType;
        PictureWidthInSamplesL = pictureWidthInSamplesL;
        MbAddrXFrameFlag = mbAddrXFrameFlag;
        BitDepthY = bitDepthY;
        BitDepthC = bitDepthC;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is DerivationContext context && Equals(context);
    }

    /// <summary>
    /// Determines whether the specified <see cref="DerivationContext"/> is equal to the current instance.
    /// </summary>
    /// <param name="other">The other <see cref="DerivationContext"/> to compare.</param>
    /// <returns><c>true</c> if the specified instance is equal; otherwise, <c>false</c>.</returns>
    public bool Equals(DerivationContext? other)
    {
        return other is not null &&
               MbAddrX == other.MbAddrX &&
               IsMbaff == other.IsMbaff &&
               IsMbaffFieldMacroblock == other.IsMbaffFieldMacroblock &&
               Sizes.Equals(other.Sizes) &&
               EqualityComparer<NeighboringMacroblocks>.Default.Equals(NeighboringMacroblocks, other.NeighboringMacroblocks) &&
               CurrMbAddr == other.CurrMbAddr &&
               MbType == other.MbType &&
               SubMbType == other.SubMbType &&
               PictureWidthInSamplesL == other.PictureWidthInSamplesL &&
               MbAddrXFrameFlag == other.MbAddrXFrameFlag &&
               BitDepthY == other.BitDepthY &&
               BitDepthC == other.BitDepthC;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(MbAddrX);
        hash.Add(IsMbaff);
        hash.Add(IsMbaffFieldMacroblock);
        hash.Add(Sizes);
        hash.Add(NeighboringMacroblocks);
        hash.Add(CurrMbAddr);
        hash.Add(MbType);
        hash.Add(SubMbType);
        hash.Add(PictureWidthInSamplesL);
        hash.Add(MbAddrXFrameFlag);
        hash.Add(BitDepthY);
        hash.Add(BitDepthC);
        return hash.ToHashCode();
    }

    /// <summary>
    /// Determines whether two <see cref="DerivationContext"/> instances are equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(DerivationContext left, DerivationContext right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="DerivationContext"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(DerivationContext left, DerivationContext right)
    {
        return !(left == right);
    }
}
