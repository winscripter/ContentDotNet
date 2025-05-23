using ContentDotNet.BitStream;

namespace ContentDotNet.Extensions.H26x.Sei.H264;

public sealed class RecoveryPointHandler : ISeiHandler
{
    public uint Code => 6;

    public Type HandleType => typeof(RecoveryPointModelHandle);

    public ISeiModelHandle SkipData(BitStreamReader reader, uint payloadType, uint payloadSize)
    {
        var start = reader.GetState();

        reader.ReadUE();
        reader.ReadBit();
        reader.ReadBit();
        reader.ReadBits(2);

        var end = reader.GetState();

        return new RecoveryPointModelHandle(start, end);
    }
}

public sealed record RecoveryPointModelHandle : ISeiModelHandle
{
    public ReaderState Start { get; }

    public ReaderState End { get; }

    public RecoveryPointModelHandle(ReaderState start, ReaderState end)
    {
        Start = start;
        End = end;
    }

    public Type ModelType => typeof(RecoveryPointModelHandle);

    public ISeiModel Read(BitStreamReader reader, uint payloadType, uint payloadSize)
    {
        var thisPos = reader.GetState();
        reader.GoTo(Start);

        var model = new RecoveryPointModel(reader.ReadUE(), reader.ReadBit(), reader.ReadBit(), reader.ReadBits(2));

        reader.GoTo(thisPos);

        return model;
    }
}

public sealed record RecoveryPointModel : ISeiModel
{
    public uint RecoveryFrameCnt { get; set; }
    public bool ExactMatchFlag { get; set; }
    public bool BrokenLinkFlag { get; set; }
    public uint ChangingSliceGroupIdc { get; set; }

    public RecoveryPointModel(uint recoveryFrameCnt, bool exactMatchFlag, bool brokenLinkFlag, uint changingSliceGroupIdc)
    {
        RecoveryFrameCnt = recoveryFrameCnt;
        ExactMatchFlag = exactMatchFlag;
        BrokenLinkFlag = brokenLinkFlag;
        ChangingSliceGroupIdc = changingSliceGroupIdc;
    }

    public bool RequiresParametersForWrite => false;

    public void Write(BitStreamWriter writer, SeiModelParameterList? parameterList)
    {
        writer.WriteUE(RecoveryFrameCnt);
        writer.WriteBit(ExactMatchFlag);
        writer.WriteBit(BrokenLinkFlag);
        writer.WriteBits(ChangingSliceGroupIdc, 2);
    }

    public async Task WriteAsync(BitStreamWriter writer, SeiModelParameterList? parameterList)
    {
        await writer.WriteUEAsync(RecoveryFrameCnt);
        await writer.WriteBitAsync(ExactMatchFlag);
        await writer.WriteBitAsync(BrokenLinkFlag);
        await writer.WriteBitsAsync(ChangingSliceGroupIdc, 2);
    }
}
