using ContentDotNet.Extensions.H264.Internal.Decoding;
using ContentDotNet.Extensions.H264.Utilities;

namespace ContentDotNet.Extensions.H264.Tests.Prediction;

public class Intra4x4PredictionTests
{
    [Fact]
    public void Intra4x4SamplePredict_VerticalMode_ValidInputs_CorrectPrediction()
    {
        var dc = new DerivationContext();
        var luma4x4BlkIdx = 0;
        var mbAddrNAvailable = true;
        var constrainedInterPredFlag = false;
        var mbaffFrameAndMbIsField = false;
        var mbAddrN = 0;
        var mbaffFrameFlag = false;
        var pictureWidthInSamplesL = 16;
        var isFrame = true;
        var isField = false;
        var bitDepthY = 8;
        var p = new Span<int>(new int[16]);
        var cSL = new Matrix16x16();
        var predL = new Matrix4x4();
        var availableForIntraPred = new Span<int>(new int[16]);
        var intra4x4PredMode = new Span<int>([0]);

        Decoder264.Intra4x4SamplePredict(dc, luma4x4BlkIdx, mbAddrNAvailable, constrainedInterPredFlag,
                                         mbaffFrameAndMbIsField, mbAddrN, mbaffFrameFlag, pictureWidthInSamplesL,
                                         isFrame, isField, bitDepthY, p, cSL, predL, availableForIntraPred,
                                         intra4x4PredMode);

        Assert.Equal(predL[0, 0], p[0]);
        Assert.Equal(predL[1, 0], p[1]);
        Assert.Equal(predL[2, 0], p[2]);
        Assert.Equal(predL[3, 0], p[3]);
    }

    [Fact]
    public void Intra4x4SamplePredict_ValidInputs_InvalidPredictionMode_DoesNotPredictVertically()
    {
        var dc = new DerivationContext();
        var luma4x4BlkIdx = 0;
        var mbAddrNAvailable = true;
        var constrainedInterPredFlag = false;
        var mbaffFrameAndMbIsField = false;
        var mbAddrN = 0;
        var mbaffFrameFlag = false;
        var pictureWidthInSamplesL = 16;
        var isFrame = true;
        var isField = false;
        var bitDepthY = 8;
        var p = new Span<int>(new int[16]);
        var cSL = new Matrix16x16();
        var predL = new Matrix4x4();
        var availableForIntraPred = new Span<int>(new int[16]);
        var intra4x4PredMode = new Span<int>([1]);

        Decoder264.Intra4x4SamplePredict(dc, luma4x4BlkIdx, mbAddrNAvailable, constrainedInterPredFlag,
                                         mbaffFrameAndMbIsField, mbAddrN, mbaffFrameFlag, pictureWidthInSamplesL,
                                         isFrame, isField, bitDepthY, p, cSL, predL, availableForIntraPred,
                                         intra4x4PredMode);

        // in this case, the vertical prediction mode should not apply,
        // so we assert that predL is not the same as p.
        Assert.NotEqual(predL[0, 0], p[0]);
    }

    [Fact]
    public void Intra4x4SamplePredict_VerticalMode_NeighboringSamplesUnavailable_SkipsPrediction()
    {
        var dc = new DerivationContext();
        var luma4x4BlkIdx = 0;
        var mbAddrNAvailable = false;
        var constrainedInterPredFlag = false;
        var mbaffFrameAndMbIsField = false;
        var mbAddrN = 0;
        var mbaffFrameFlag = false;
        var pictureWidthInSamplesL = 16;
        var isFrame = true;
        var isField = false;
        var bitDepthY = 8;
        var p = new Span<int>(new int[16]);
        var cSL = new Matrix16x16();
        var predL = new Matrix4x4();
        var availableForIntraPred = new Span<int>(new int[16]);
        var intra4x4PredMode = new Span<int>([0]);

        Decoder264.Intra4x4SamplePredict(dc, luma4x4BlkIdx, mbAddrNAvailable, constrainedInterPredFlag,
                                         mbaffFrameAndMbIsField, mbAddrN, mbaffFrameFlag, pictureWidthInSamplesL,
                                         isFrame, isField, bitDepthY, p, cSL, predL, availableForIntraPred,
                                         intra4x4PredMode);

        // prediction should be skipped if the neighboring samples are unavailable
        Assert.NotEqual(predL[0, 0], p[0]);
    }

    [Fact]
    public void Intra4x4SamplePredict_VerticalMode_FrameAndFieldContext_CorrectPrediction()
    {
        var dc = new DerivationContext();
        var luma4x4BlkIdx = 0;
        var mbAddrNAvailable = true;
        var constrainedInterPredFlag = false;
        var mbaffFrameAndMbIsField = true;
        var mbAddrN = 0;
        var mbaffFrameFlag = false;
        var pictureWidthInSamplesL = 16;
        var isFrame = true;
        var isField = false;
        var bitDepthY = 8;
        var p = new Span<int>(new int[16]);
        var cSL = new Matrix16x16();
        var predL = new Matrix4x4();
        var availableForIntraPred = new Span<int>(new int[16]);
        var intra4x4PredMode = new Span<int>([0]);

        Decoder264.Intra4x4SamplePredict(dc, luma4x4BlkIdx, mbAddrNAvailable, constrainedInterPredFlag,
                                         mbaffFrameAndMbIsField, mbAddrN, mbaffFrameFlag, pictureWidthInSamplesL,
                                         isFrame, isField, bitDepthY, p, cSL, predL, availableForIntraPred,
                                         intra4x4PredMode);

        // ensure prediction is applied based on field context
        Assert.Equal(predL[0, 0], p[0]);
    }

    [Fact]
    public void Intra4x4SamplePredict_VerticalMode_BoundaryConditions_CorrectPrediction()
    {
        var dc = new DerivationContext();
        var luma4x4BlkIdx = 15;
        var mbAddrNAvailable = true;
        var constrainedInterPredFlag = false;
        var mbaffFrameAndMbIsField = false;
        var mbAddrN = 0;
        var mbaffFrameFlag = false;
        var pictureWidthInSamplesL = 16;
        var isFrame = true;
        var isField = false;
        var bitDepthY = 8;
        var p = new Span<int>(new int[16]);
        var cSL = new Matrix16x16();
        var predL = new Matrix4x4();
        var availableForIntraPred = new Span<int>(new int[16]);
        var intra4x4PredMode = new Span<int>([0]);

        Decoder264.Intra4x4SamplePredict(dc, luma4x4BlkIdx, mbAddrNAvailable, constrainedInterPredFlag,
                                         mbaffFrameAndMbIsField, mbAddrN, mbaffFrameFlag, pictureWidthInSamplesL,
                                         isFrame, isField, bitDepthY, p, cSL, predL, availableForIntraPred,
                                         intra4x4PredMode);

        // ensure prediction works correctly even at boundaries
        Assert.Equal(predL[0, 0], p[0]);
    }
}
