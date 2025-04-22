using ContentDotNet.Abstractions;

namespace ContentDotNet.Extensions.H264.Models;

/// <summary>
/// HRD parameters.
/// </summary>
public struct HrdParameters
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
}
