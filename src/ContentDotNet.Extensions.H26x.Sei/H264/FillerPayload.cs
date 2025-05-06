namespace ContentDotNet.Extensions.H26x.Sei.H264;

public sealed class FillerPayloadHandler : ISeiHandler
{
    public static uint Code => 3;

    public static Type HandleType => typeof(FillerPayloadModelHandle);

    public static ISeiModelHandle SkipData(BitStreamReader reader, uint payloadType, uint payloadSize)
    {
        var start = reader.GetState();

        for (int i = 0; i < payloadSize; i++)
            _ = reader.ReadBit();

        var end = reader.GetState();

        return new FillerPayloadModelHandle(start, end);
    }
}

public sealed class FillerPayloadModelHandle : ISeiModelHandle
{
    public static Type ModelType => typeof(FillerPayloadModel);

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
public sealed class FillerPayloadModel : ISeiModel
{
}
