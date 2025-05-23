using ContentDotNet.BitStream;

namespace ContentDotNet.Extensions.H26x.Sei.H264;

public sealed class StereoVideoInfoHandler : ISeiHandler
{
    public uint Code => 21;

    public Type HandleType => typeof(StereoVideoInfoHandle);

    public ISeiModelHandle SkipData(BitStreamReader reader, uint payloadType, uint payloadSize)
    {
        var start = reader.GetState();

        reader.ReadBits(reader.ReadBit() ? 1u : 2u);
        reader.ReadBits(2);

        var end = reader.GetState();

        return new StereoVideoInfoHandle(start, end);
    }
}

public sealed record StereoVideoInfoHandle : ISeiModelHandle
{
    public ReaderState Start { get; }

    public ReaderState End { get; }

    public StereoVideoInfoHandle(ReaderState start, ReaderState end)
    {
        Start = start;
        End = end;
    }

    public Type ModelType => typeof(StereoVideoInfoModel);

    public ISeiModel Read(BitStreamReader reader, uint payloadType, uint payloadSize)
    {
        var thisPos = reader.GetState();
        reader.GoTo(Start);

        bool fieldViewsFlag = reader.ReadBit();
        bool topFieldIsLeftViewFlag = false;
        bool currentFrameIsLeftViewFlag = false;
        bool nextFrameIsSecondViewFlag = false;

        if (fieldViewsFlag)
        {
            topFieldIsLeftViewFlag = reader.ReadBit();
        }
        else
        {
            currentFrameIsLeftViewFlag = reader.ReadBit();
            nextFrameIsSecondViewFlag = reader.ReadBit();
        }

        bool leftViewSelfContainedFlag = reader.ReadBit();
        bool rightViewSelfContainedFlag = reader.ReadBit();

        reader.GoTo(thisPos);

        return new StereoVideoInfoModel(
            fieldViewsFlag,
            topFieldIsLeftViewFlag,
            currentFrameIsLeftViewFlag,
            nextFrameIsSecondViewFlag,
            leftViewSelfContainedFlag,
            rightViewSelfContainedFlag
        );
    }
}

public sealed record StereoVideoInfoModel : ISeiModel
{
    public bool FieldViewsFlag { get; set; }
    public bool TopFieldIsLeftViewFlag { get; set; }
    public bool CurrentFrameIsLeftViewFlag { get; set; }
    public bool NextFrameIsSecondViewFlag { get; set; }
    public bool LeftViewSelfContainedFlag { get; set; }
    public bool RightViewSelfContainedFlag { get; set; }

    public StereoVideoInfoModel(bool fieldViewsFlag, bool topFieldIsLeftViewFlag, bool currentFrameIsLeftViewFlag, bool nextFrameIsSecondViewFlag, bool leftViewSelfContainedFlag, bool rightViewSelfContainedFlag)
    {
        FieldViewsFlag = fieldViewsFlag;
        TopFieldIsLeftViewFlag = topFieldIsLeftViewFlag;
        CurrentFrameIsLeftViewFlag = currentFrameIsLeftViewFlag;
        NextFrameIsSecondViewFlag = nextFrameIsSecondViewFlag;
        LeftViewSelfContainedFlag = leftViewSelfContainedFlag;
        RightViewSelfContainedFlag = rightViewSelfContainedFlag;
    }

    public bool RequiresParametersForWrite => false;

    public void Write(BitStreamWriter writer, SeiModelParameterList? parameterList)
    {
        writer.WriteBit(FieldViewsFlag);
        if (FieldViewsFlag)
        {
            writer.WriteBit(TopFieldIsLeftViewFlag);
        }
        else
        {
            writer.WriteBit(CurrentFrameIsLeftViewFlag);
            writer.WriteBit(NextFrameIsSecondViewFlag);
        }
        writer.WriteBit(LeftViewSelfContainedFlag);
        writer.WriteBit(RightViewSelfContainedFlag);
    }

    public async Task WriteAsync(BitStreamWriter writer, SeiModelParameterList? parameterList)
    {
        await writer.WriteBitAsync(FieldViewsFlag);
        if (FieldViewsFlag)
        {
            await writer.WriteBitAsync(TopFieldIsLeftViewFlag);
        }
        else
        {
            await writer.WriteBitAsync(CurrentFrameIsLeftViewFlag);
            await writer.WriteBitAsync(NextFrameIsSecondViewFlag);
        }
        await writer.WriteBitAsync(LeftViewSelfContainedFlag);
        await writer.WriteBitAsync(RightViewSelfContainedFlag);
    }
}
