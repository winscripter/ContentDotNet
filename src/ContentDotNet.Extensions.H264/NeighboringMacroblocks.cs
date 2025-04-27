namespace ContentDotNet.Extensions.H264;

/// <summary>
/// Represents the neighboring macroblocks in an H.264 video stream.
/// </summary>
public struct NeighboringMacroblocks : IEquatable<NeighboringMacroblocks>
{
    /// <summary>
    /// Address of the macroblock to the left (A).
    /// </summary>
    public int MbAddrA;

    /// <summary>
    /// Address of the macroblock above (B).
    /// </summary>
    public int MbAddrB;

    /// <summary>
    /// Address of the macroblock above and to the right (C).
    /// </summary>
    public int MbAddrC;

    /// <summary>
    /// Address of the macroblock above and to the left (D).
    /// </summary>
    public int MbAddrD;

    /// <summary>
    /// Indicates whether the macroblock to the left (A) is available.
    /// </summary>
    public bool IsMbAddrAAvailable;

    /// <summary>
    /// Indicates whether the macroblock above (B) is available.
    /// </summary>
    public bool IsMbAddrBAvailable;

    /// <summary>
    /// Indicates whether the macroblock above and to the right (C) is available.
    /// </summary>
    public bool IsMbAddrCAvailable;

    /// <summary>
    /// Indicates whether the macroblock above and to the left (D) is available.
    /// </summary>
    public bool IsMbAddrDAvailable;

    /// <summary>
    /// Initializes a new instance of the <see cref="NeighboringMacroblocks"/> struct.
    /// </summary>
    /// <param name="mbAddrA">Address of the macroblock to the left (A).</param>
    /// <param name="mbAddrB">Address of the macroblock above (B).</param>
    /// <param name="mbAddrC">Address of the macroblock above and to the right (C).</param>
    /// <param name="mbAddrD">Address of the macroblock above and to the left (D).</param>
    /// <param name="isMbAddrAAvailable">Indicates whether the macroblock to the left (A) is available.</param>
    /// <param name="isMbAddrBAvailable">Indicates whether the macroblock above (B) is available.</param>
    /// <param name="isMbAddrCAvailable">Indicates whether the macroblock above and to the right (C) is available.</param>
    /// <param name="isMbAddrDAvailable">Indicates whether the macroblock above and to the left (D) is available.</param>
    public NeighboringMacroblocks(int mbAddrA, int mbAddrB, int mbAddrC, int mbAddrD, bool isMbAddrAAvailable, bool isMbAddrBAvailable, bool isMbAddrCAvailable, bool isMbAddrDAvailable)
    {
        MbAddrA = mbAddrA;
        MbAddrB = mbAddrB;
        MbAddrC = mbAddrC;
        MbAddrD = mbAddrD;
        IsMbAddrAAvailable = isMbAddrAAvailable;
        IsMbAddrBAvailable = isMbAddrBAvailable;
        IsMbAddrCAvailable = isMbAddrCAvailable;
        IsMbAddrDAvailable = isMbAddrDAvailable;
    }

    /// <inheritdoc/>
    public readonly override bool Equals(object? obj)
    {
        return obj is NeighboringMacroblocks macroblocks && Equals(macroblocks);
    }

    /// <summary>
    /// Determines whether the specified <see cref="NeighboringMacroblocks"/> is equal to the current instance.
    /// </summary>
    /// <param name="other">The <see cref="NeighboringMacroblocks"/> to compare with the current instance.</param>
    /// <returns><c>true</c> if the specified <see cref="NeighboringMacroblocks"/> is equal to the current instance; otherwise, <c>false</c>.</returns>
    public readonly bool Equals(NeighboringMacroblocks other)
    {
        return MbAddrA == other.MbAddrA &&
               MbAddrB == other.MbAddrB &&
               MbAddrC == other.MbAddrC &&
               MbAddrD == other.MbAddrD &&
               IsMbAddrAAvailable == other.IsMbAddrAAvailable &&
               IsMbAddrBAvailable == other.IsMbAddrBAvailable &&
               IsMbAddrCAvailable == other.IsMbAddrCAvailable &&
               IsMbAddrDAvailable == other.IsMbAddrDAvailable;
    }

    /// <inheritdoc/>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(MbAddrA, MbAddrB, MbAddrC, MbAddrD, IsMbAddrAAvailable, IsMbAddrBAvailable, IsMbAddrCAvailable, IsMbAddrDAvailable);
    }

    /// <summary>
    /// Determines whether two <see cref="NeighboringMacroblocks"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="NeighboringMacroblocks"/> to compare.</param>
    /// <param name="right">The second <see cref="NeighboringMacroblocks"/> to compare.</param>
    /// <returns><c>true</c> if the two <see cref="NeighboringMacroblocks"/> instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(NeighboringMacroblocks left, NeighboringMacroblocks right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="NeighboringMacroblocks"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="NeighboringMacroblocks"/> to compare.</param>
    /// <param name="right">The second <see cref="NeighboringMacroblocks"/> to compare.</param>
    /// <returns><c>true</c> if the two <see cref="NeighboringMacroblocks"/> instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(NeighboringMacroblocks left, NeighboringMacroblocks right)
    {
        return !(left == right);
    }
}
