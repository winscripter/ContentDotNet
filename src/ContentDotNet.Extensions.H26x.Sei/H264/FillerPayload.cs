using ContentDotNet.BitStream;
using ContentDotNet.Extensions.H26x.Sei.H264.Parameters;

namespace ContentDotNet.Extensions.H26x.Sei.H264;

public sealed class FillerPayloadHandler : ISeiHandler
{
    public uint Code => 3;

    public Type HandleType => typeof(FillerPayloadModelHandle);

    public ISeiModelHandle SkipData(BitStreamReader reader, uint payloadType, uint payloadSize)
    {
        var start = reader.GetState();

        for (int i = 0; i < payloadSize; i++)
            _ = reader.ReadBit();

        var end = reader.GetState();

        return new FillerPayloadModelHandle(start, end);
    }
}

public sealed record FillerPayloadModelHandle : ISeiModelHandle
{
    public Type ModelType => typeof(FillerPayloadModel);

    public ReaderState Start { get; }

    public ReaderState End { get; }

    public FillerPayloadModelHandle(ReaderState start, ReaderState end)
    {
        Start = start;
        End = end;
    }

    public ISeiModel Read(BitStreamReader reader, uint payloadType, uint payloadSize)
    {
        return new FillerPayloadModel();
    }
}

// It's just filler data.
public sealed record FillerPayloadModel : ISeiModel
{
    public void Write(BitStreamWriter writer, SeiModelParameterList? parameterList)
    {
        SeiHelpers.RequireParameterList(parameterList, nameof(parameterList));

        FillerPayloadParameters? parameters = parameterList!.TryGet<FillerPayloadParameters>()
            ?? throw new ArgumentException("Missing parameter of type FillerPayloadParameters", nameof(parameterList));

        for (int i = 0; i < parameters.BitsToWrite; i++)
            writer.WriteBit(parameters.UseOneBits);
    }

    public async Task WriteAsync(BitStreamWriter writer, SeiModelParameterList? parameterList)
    {
        SeiHelpers.RequireParameterList(parameterList, nameof(parameterList));

        FillerPayloadParameters? parameters = parameterList!.TryGet<FillerPayloadParameters>()
            ?? throw new ArgumentException("Missing parameter of type FillerPayloadParameters", nameof(parameterList));

        for (int i = 0; i < parameters.BitsToWrite; i++)
            await writer.WriteBitAsync(parameters.UseOneBits);
    }

    public bool RequiresParametersForWrite => true;
}
