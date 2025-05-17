using ContentDotNet.BitStream;
using ContentDotNet.Primitives;

namespace ContentDotNet.Extensions.H264.Models;

/// <summary>
/// HRD parameters.
/// </summary>
public struct HrdParameters : IEquatable<HrdParameters>
{
    /// <summary>
    ///   Specifies the CPB count minus 1.
    /// </summary>
    public uint CpbCntMinus1;

    /// <summary>
    ///   Scale factor for bitrate values.
    /// </summary>
    public uint BitRateScale;

    /// <summary>
    ///   Scale factor for CPB size values.
    /// </summary>
    public uint CpbSizeScale;

    /// <summary>
    ///   Array of bitrate values minus 1 (of type UE).
    /// </summary>
    public ArrayReferrer BitRateValueMinus1;

    /// <summary>
    ///   Array of CPB size values minus 1 (of type UE).
    /// </summary>
    public ArrayReferrer CpbSizeValueMinus1;

    /// <summary>
    ///   Array of constant bitrate flags (of type u(1)).
    /// </summary>
    public ArrayReferrer CbrFlag;

    /// <summary>
    ///   Specifies the initial CPB removal delay length minus 1.
    /// </summary>
    public uint InitialCpbRemovalDelayLengthMinus1;

    /// <summary>
    ///   Specifies the CPB removal delay length minus 1.
    /// </summary>
    public uint CpbRemovalDelayLengthMinus1;

    /// <summary>
    ///   Specifies the DPB output delay length minus 1.
    /// </summary>
    public uint DpbOutputDelayLengthMinus1;

    /// <summary>
    ///   Specifies the time offset length.
    /// </summary>
    public uint TimeOffsetLength;

    /// <summary>
    ///   Initializes a new instance of the <see cref="HrdParameters"/> structure.
    /// </summary>
    /// <param name="cpbCntMinus1">Part of the HRD parameters.</param>
    /// <param name="bitRateScale">Part of the HRD parameters.</param>
    /// <param name="cpbSizeScale">Part of the HRD parameters.</param>
    /// <param name="bitRateValueMinus1">Part of the HRD parameters.</param>
    /// <param name="cpbSizeValueMinus1">Part of the HRD parameters.</param>
    /// <param name="cbrFlag">Part of the HRD parameters.</param>
    /// <param name="initialCpbRemovalDelayLengthMinus1">Part of the HRD parameters.</param>
    /// <param name="cpbRemovalDelayLengthMinus1">Part of the HRD parameters.</param>
    /// <param name="dpbOutputDelayLengthMinus1">Part of the HRD parameters.</param>
    /// <param name="timeOffsetLength">Part of the HRD parameters.</param>
    public HrdParameters(uint cpbCntMinus1, uint bitRateScale, uint cpbSizeScale, ArrayReferrer bitRateValueMinus1, ArrayReferrer cpbSizeValueMinus1, ArrayReferrer cbrFlag, uint initialCpbRemovalDelayLengthMinus1, uint cpbRemovalDelayLengthMinus1, uint dpbOutputDelayLengthMinus1, uint timeOffsetLength)
    {
        CpbCntMinus1 = cpbCntMinus1;
        BitRateScale = bitRateScale;
        CpbSizeScale = cpbSizeScale;
        BitRateValueMinus1 = bitRateValueMinus1;
        CpbSizeValueMinus1 = cpbSizeValueMinus1;
        CbrFlag = cbrFlag;
        InitialCpbRemovalDelayLengthMinus1 = initialCpbRemovalDelayLengthMinus1;
        CpbRemovalDelayLengthMinus1 = cpbRemovalDelayLengthMinus1;
        DpbOutputDelayLengthMinus1 = dpbOutputDelayLengthMinus1;
        TimeOffsetLength = timeOffsetLength;
    }

    /// <summary>
    ///   Reads the HRD parameters from the given bitstream reader.
    /// </summary>
    /// <param name="reader">Bitstream reader where HRD parameters are read from.</param>
    /// <returns>HRD parameters, read from the bitstream.</returns>
    public static HrdParameters Read(BitStreamReader reader)
    {
        uint cpbCntMinus1 = reader.ReadUE();
        uint bitRateScale = reader.ReadBits(4);
        uint cpbSizeScale = reader.ReadBits(4);

        var referrer = new ArrayReferrer((int)cpbCntMinus1 + 1);

        for (int SchedSelIdx = 0; SchedSelIdx <= cpbCntMinus1; SchedSelIdx++)
        {
            _ = reader.ReadUE(); // bitRateValueMinus1[SchedSelIdx]
            _ = reader.ReadUE(); // cpbSizeValueMinus1[SchedSelIdx]
            _ = reader.ReadBit(); // cbrFlag[SchedSelIdx]
        }

        uint initialCpbRemovalDelayLengthMinus1 = reader.ReadBits(5);
        uint cpbRemovalDelayLengthMinus1 = reader.ReadBits(5);
        uint dpbOutputDelayLengthMinus1 = reader.ReadBits(5);
        uint timeOffsetLength = reader.ReadBits(5);

        return new HrdParameters(
            cpbCntMinus1,
            bitRateScale,
            cpbSizeScale,
            referrer, referrer, referrer,
            initialCpbRemovalDelayLengthMinus1,
            cpbRemovalDelayLengthMinus1,
            dpbOutputDelayLengthMinus1,
            timeOffsetLength);
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current <see cref="HrdParameters"/> instance.
    /// </summary>
    /// <param name="obj">The object to compare with the current <see cref="HrdParameters"/> instance.</param>
    /// <returns><c>true</c> if the specified object is equal to the current instance; otherwise, <c>false</c>.</returns>
    public readonly override bool Equals(object? obj)
    {
        return obj is HrdParameters parameters && Equals(parameters);
    }

    /// <summary>
    /// Determines whether the specified <see cref="HrdParameters"/> instance is equal to the current instance.
    /// </summary>
    /// <param name="other">The <see cref="HrdParameters"/> instance to compare with the current instance.</param>
    /// <returns><c>true</c> if the specified instance is equal to the current instance; otherwise, <c>false</c>.</returns>
    public readonly bool Equals(HrdParameters other)
    {
        return CpbCntMinus1 == other.CpbCntMinus1 &&
               BitRateScale == other.BitRateScale &&
               CpbSizeScale == other.CpbSizeScale &&
               EqualityComparer<ArrayReferrer>.Default.Equals(BitRateValueMinus1, other.BitRateValueMinus1) &&
               EqualityComparer<ArrayReferrer>.Default.Equals(CpbSizeValueMinus1, other.CpbSizeValueMinus1) &&
               EqualityComparer<ArrayReferrer>.Default.Equals(CbrFlag, other.CbrFlag) &&
               InitialCpbRemovalDelayLengthMinus1 == other.InitialCpbRemovalDelayLengthMinus1 &&
               CpbRemovalDelayLengthMinus1 == other.CpbRemovalDelayLengthMinus1 &&
               DpbOutputDelayLengthMinus1 == other.DpbOutputDelayLengthMinus1 &&
               TimeOffsetLength == other.TimeOffsetLength;
    }

    /// <summary>
    /// Serves as the default hash function for the <see cref="HrdParameters"/> structure.
    /// </summary>
    /// <returns>A hash code for the current <see cref="HrdParameters"/> instance.</returns>
    public readonly override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(CpbCntMinus1);
        hash.Add(BitRateScale);
        hash.Add(CpbSizeScale);
        hash.Add(BitRateValueMinus1);
        hash.Add(CpbSizeValueMinus1);
        hash.Add(CbrFlag);
        hash.Add(InitialCpbRemovalDelayLengthMinus1);
        hash.Add(CpbRemovalDelayLengthMinus1);
        hash.Add(DpbOutputDelayLengthMinus1);
        hash.Add(TimeOffsetLength);
        return hash.ToHashCode();
    }

    /// <summary>
    ///   Writes the HRD parameters to the given bitstream.
    /// </summary>
    /// <param name="writer">Bitstream where HRD parameters are written to.</param>
    /// <param name="bitRateValueMinus1">All bitRateValueMinus1 values.</param>
    /// <param name="cpbSizeValueMinus1">All cpbSizeValueMinus1 values.</param>
    /// <param name="cbrFlag">All cbrFlag values.</param>
    public readonly void Write(BitStreamWriter writer, ReadOnlySpan<uint> bitRateValueMinus1, ReadOnlySpan<uint> cpbSizeValueMinus1, ReadOnlySpan<bool> cbrFlag)
    {
        writer.WriteUE(CpbCntMinus1);
        writer.WriteUE(BitRateScale);
        writer.WriteUE(CpbSizeScale);

        for (int SchedSelIdx = 0; SchedSelIdx <= CpbCntMinus1; SchedSelIdx++)
        {
            writer.WriteUE(bitRateValueMinus1[SchedSelIdx]);
            writer.WriteUE(cpbSizeValueMinus1[SchedSelIdx]);
            writer.WriteBit(cbrFlag[SchedSelIdx]);
        }

        writer.WriteBits(InitialCpbRemovalDelayLengthMinus1, 5);
        writer.WriteBits(CpbRemovalDelayLengthMinus1, 5);
        writer.WriteBits(DpbOutputDelayLengthMinus1, 5);
        writer.WriteBits(TimeOffsetLength, 5);
    }

    /// <summary>
    ///   Writes the HRD parameters to the given bitstream.
    /// </summary>
    /// <param name="writer">Bitstream where HRD parameters are written to.</param>
    /// <param name="bitRateValueMinus1">All bitRateValueMinus1 values.</param>
    /// <param name="cpbSizeValueMinus1">All cpbSizeValueMinus1 values.</param>
    /// <param name="cbrFlag">All cbrFlag values.</param>
    public readonly async Task WriteAsync(BitStreamWriter writer, ReadOnlyMemory<uint> bitRateValueMinus1, ReadOnlyMemory<uint> cpbSizeValueMinus1, ReadOnlyMemory<bool> cbrFlag)
    {
        await writer.WriteUEAsync(CpbCntMinus1);
        await writer.WriteUEAsync(BitRateScale);
        await writer.WriteUEAsync(CpbSizeScale);

        for (int SchedSelIdx = 0; SchedSelIdx <= CpbCntMinus1; SchedSelIdx++)
        {
            await writer.WriteUEAsync(bitRateValueMinus1.Span[SchedSelIdx]);
            await writer.WriteUEAsync(cpbSizeValueMinus1.Span[SchedSelIdx]);
            await writer.WriteBitAsync(cbrFlag.Span[SchedSelIdx]);
        }

        await writer.WriteBitsAsync(InitialCpbRemovalDelayLengthMinus1, 5);
        await writer.WriteBitsAsync(CpbRemovalDelayLengthMinus1, 5);
        await writer.WriteBitsAsync(DpbOutputDelayLengthMinus1, 5);
        await writer.WriteBitsAsync(TimeOffsetLength, 5);
    }


    /// <summary>
    /// Determines whether two specified <see cref="HrdParameters"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="HrdParameters"/> to compare.</param>
    /// <param name="right">The second <see cref="HrdParameters"/> to compare.</param>
    /// <returns><c>true</c> if the two <see cref="HrdParameters"/> instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(HrdParameters left, HrdParameters right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two specified <see cref="HrdParameters"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="HrdParameters"/> to compare.</param>
    /// <param name="right">The second <see cref="HrdParameters"/> to compare.</param>
    /// <returns><c>true</c> if the two <see cref="HrdParameters"/> instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(HrdParameters left, HrdParameters right)
    {
        return !(left == right);
    }
}
