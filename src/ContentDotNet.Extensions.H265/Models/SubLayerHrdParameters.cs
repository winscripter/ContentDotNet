using ContentDotNet.BitStream;
using ContentDotNet.Containers;

namespace ContentDotNet.Extensions.H265.Models;

/// <summary>
/// Represents the sub-layer HRD (Hypothetical Reference Decoder) parameters for H.265/HEVC bitstreams.
/// </summary>
public struct SubLayerHrdParameters : IEquatable<SubLayerHrdParameters>
{
    /// <summary>
    /// Gets or sets the number of CPB (Coded Picture Buffer) entries.
    /// </summary>
    /// <remarks>
    /// This value determines how many elements are in the associated containers.
    /// Modifying this field does not influence the <see cref="WriteAsync(BitStreamWriter, uint, bool)"/>
    /// and <see cref="Write(BitStreamWriter, uint, bool)"/> method.
    /// </remarks>
    public uint CpbCnt;

    /// <summary>
    /// Gets or sets the bit rate value minus 1 for each CPB entry.
    /// </summary>
    public Container32UInt32 BitRateValueMinus1;

    /// <summary>
    /// Gets or sets the CPB size value minus 1 for each CPB entry.
    /// </summary>
    public Container32UInt32 CpbSizeValueMinus1;

    /// <summary>
    /// Gets or sets the CPB size DU (Decoding Unit) value minus 1 for each CPB entry.
    /// </summary>
    public Container32UInt32 CpbSizeDuValueMinus1;

    /// <summary>
    /// Gets or sets the bit rate DU (Decoding Unit) value minus 1 for each CPB entry.
    /// </summary>
    public Container32UInt32 BitRateDuValueMinus1;

    /// <summary>
    /// Gets or sets the CBR (Constant Bit Rate) flag for each CPB entry.
    /// </summary>
    public Container32Boolean CbrFlag;

    /// <summary>
    /// Initializes a new instance of the <see cref="SubLayerHrdParameters"/> struct.
    /// </summary>
    /// <param name="cpbCnt">The number of CPB entries.</param>
    /// <param name="bitRateValueMinus1">The bit rate value minus 1 container.</param>
    /// <param name="cpbSizeValueMinus1">The CPB size value minus 1 container.</param>
    /// <param name="cpbSizeDuValueMinus1">The CPB size DU value minus 1 container.</param>
    /// <param name="bitRateDuValueMinus1">The bit rate DU value minus 1 container.</param>
    /// <param name="cbrFlag">The CBR flag container.</param>
    public SubLayerHrdParameters(uint cpbCnt, Container32UInt32 bitRateValueMinus1, Container32UInt32 cpbSizeValueMinus1, Container32UInt32 cpbSizeDuValueMinus1, Container32UInt32 bitRateDuValueMinus1, Container32Boolean cbrFlag)
    {
        CpbCnt = cpbCnt;
        BitRateValueMinus1 = bitRateValueMinus1;
        CpbSizeValueMinus1 = cpbSizeValueMinus1;
        CpbSizeDuValueMinus1 = cpbSizeDuValueMinus1;
        BitRateDuValueMinus1 = bitRateDuValueMinus1;
        CbrFlag = cbrFlag;
    }

    /// <summary>
    /// Reads a <see cref="SubLayerHrdParameters"/> instance from the specified <see cref="BitStreamReader"/>.
    /// </summary>
    /// <param name="reader">The bitstream reader.</param>
    /// <param name="cpbCnt">The number of CPB entries to read.</param>
    /// <param name="subPicHrdParamsPresentFlag">Indicates if sub-picture HRD parameters are present.</param>
    /// <returns>A populated <see cref="SubLayerHrdParameters"/> instance.</returns>
    public static SubLayerHrdParameters Read(BitStreamReader reader, uint cpbCnt, bool subPicHrdParamsPresentFlag)
    {
        Container32UInt32 bitRateValueMinus1 = default;
        Container32UInt32 cpbSizeValueMinus1 = default;
        Container32UInt32 cpbSizeDuValueMinus1 = default;
        Container32UInt32 bitRateDuValueMinus1 = default;
        Container32Boolean cbrFlag = default;

        for (int i = 0; i < cpbCnt; i++)
        {
            bitRateValueMinus1[i] = reader.ReadUE();
            cpbSizeValueMinus1[i] = reader.ReadUE();
            if (subPicHrdParamsPresentFlag)
            {
                cpbSizeDuValueMinus1[i] = reader.ReadUE();
                bitRateDuValueMinus1[i] = reader.ReadUE();
            }
            cbrFlag[i] = reader.ReadBit();
        }

        return new SubLayerHrdParameters(cpbCnt, bitRateValueMinus1, cpbSizeValueMinus1, cpbSizeDuValueMinus1, bitRateDuValueMinus1, cbrFlag);
    }

    /// <summary>
    /// Writes the <see cref="SubLayerHrdParameters"/> instance to the specified <see cref="BitStreamWriter"/>.
    /// </summary>
    /// <param name="writer">The bitstream writer.</param>
    /// <param name="cpbCnt">The number of CPB entries to write.</param>
    /// <param name="subPicHrdParamsPresentFlag">Indicates if sub-picture HRD parameters are present.</param>
    public readonly void Write(BitStreamWriter writer, uint cpbCnt, bool subPicHrdParamsPresentFlag)
    {
        for (int i = 0; i < cpbCnt; i++)
        {
            writer.WriteUE(BitRateValueMinus1[i]);
            writer.WriteUE(CpbSizeValueMinus1[i]);
            if (subPicHrdParamsPresentFlag)
            {
                writer.WriteUE(CpbSizeDuValueMinus1[i]);
                writer.WriteUE(BitRateDuValueMinus1[i]);
            }
            writer.WriteBit(CbrFlag[i]);
        }
    }

    /// <summary>
    /// Asynchronously writes the <see cref="SubLayerHrdParameters"/> instance to the specified <see cref="BitStreamWriter"/>.
    /// </summary>
    /// <param name="writer">The bitstream writer.</param>
    /// <param name="cpbCnt">The number of CPB entries to write.</param>
    /// <param name="subPicHrdParamsPresentFlag">Indicates if sub-picture HRD parameters are present.</param>
    /// <returns>A task representing the asynchronous write operation.</returns>
    public readonly async Task WriteAsync(BitStreamWriter writer, uint cpbCnt, bool subPicHrdParamsPresentFlag)
    {
        for (int i = 0; i < cpbCnt; i++)
        {
            await writer.WriteUEAsync(BitRateValueMinus1[i]);
            await writer.WriteUEAsync(CpbSizeValueMinus1[i]);
            if (subPicHrdParamsPresentFlag)
            {
                await writer.WriteUEAsync(CpbSizeDuValueMinus1[i]);
                await writer.WriteUEAsync(BitRateDuValueMinus1[i]);
            }
            await writer.WriteBitAsync(CbrFlag[i]);
        }
    }

    /// <inheritdoc/>
    public readonly override bool Equals(object? obj)
    {
        return obj is SubLayerHrdParameters parameters && Equals(parameters);
    }

    /// <summary>
    /// Indicates whether the current object is equal to another <see cref="SubLayerHrdParameters"/> instance.
    /// </summary>
    /// <param name="other">The other <see cref="SubLayerHrdParameters"/> to compare with.</param>
    /// <returns><c>true</c> if the objects are equal; otherwise, <c>false</c>.</returns>
    public readonly bool Equals(SubLayerHrdParameters other)
    {
        return CpbCnt == other.CpbCnt &&
               BitRateValueMinus1.Equals(other.BitRateValueMinus1) &&
               CpbSizeValueMinus1.Equals(other.CpbSizeValueMinus1) &&
               CpbSizeDuValueMinus1.Equals(other.CpbSizeDuValueMinus1) &&
               BitRateDuValueMinus1.Equals(other.BitRateDuValueMinus1) &&
               CbrFlag.Equals(other.CbrFlag);
    }

    /// <inheritdoc/>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(CpbCnt, BitRateValueMinus1, CpbSizeValueMinus1, CpbSizeDuValueMinus1, BitRateDuValueMinus1, CbrFlag);
    }

    /// <summary>
    /// Determines whether two <see cref="SubLayerHrdParameters"/> instances are equal.
    /// </summary>
    /// <param name="left">The first instance.</param>
    /// <param name="right">The second instance.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(SubLayerHrdParameters left, SubLayerHrdParameters right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="SubLayerHrdParameters"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first instance.</param>
    /// <param name="right">The second instance.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(SubLayerHrdParameters left, SubLayerHrdParameters right)
    {
        return !(left == right);
    }
}
