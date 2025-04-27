using ContentDotNet.Abstractions;
using ContentDotNet.Extensions.H264.Models;

namespace ContentDotNet.Extensions.H264.Tests.ModelsIO;

public class SPSTests
{
    [Fact]
    public void SpsTest_WriteAndRead_ValidatesFields()
    {
        const uint profileIDC = 1;
        const bool constraintSet0Flag = false, constraintSet1Flag = false, constraintSet2Flag = false,
                   constraintSet3Flag = false, constraintSet4Flag = false, constraintSet5Flag = false;
        const uint reservedZBits = 1;
        const uint levelIDC = 1;
        const uint spsID = 1;
        const uint chromaFormatIDC = 1;
        const bool separateColourPlaneFlag = false;
        const uint bitDepthLumaMinus8 = 1;
        const uint bitDepthChromaMinus8 = 1;
        const bool qpprimeYZeroTransformBypassFlag = false;
        const bool matrixPresentFlag = false;
        ScalingMatrices? matrix = null;
        const uint log2MaxFrameNumMinus4 = 1;
        const uint picOrderCntType = 2;
        const uint log2MaxPicOrderCntLsbMinus4 = 1;
        const bool deltaPicOrderAlwaysZeroFlag = false;
        const int offsetForNonRefPic = 1;
        const int offsetForTopToBottomField = 1;
        const uint numRefFramesInPicOrderCntCycle = 1;
        const uint maxNumRefFrames = 1;
        const bool gapsInFrameNumValueAllowedFlag = false;
        const uint picWidthInMbsMinus1 = 1;
        const uint picHeightInMapUnitsMinus1 = 1;
        const bool frameMbsOnlyFlag = false;
        const bool macroblockAdaptiveFrameFieldFlag = false;
        const bool direct8x8InferenceFlag = false;
        const bool frameCroppingFlag = false;
        const uint frameCropLeftOffset = 1,
                   frameCropRightOffset = 1,
                   frameCropTopOffset = 1,
                   frameCropBottomOffset = 1;
        const bool vuiParametersPresentFlag = false;
        VuiParameters? vuip = null;

        var sps = new SequenceParameterSet(
            profileIDC, constraintSet0Flag, constraintSet1Flag, constraintSet2Flag, constraintSet3Flag, constraintSet4Flag, constraintSet5Flag,
            reservedZBits, levelIDC, spsID, chromaFormatIDC, separateColourPlaneFlag, bitDepthLumaMinus8, bitDepthChromaMinus8,
            qpprimeYZeroTransformBypassFlag, matrixPresentFlag, matrix, log2MaxFrameNumMinus4, picOrderCntType, log2MaxPicOrderCntLsbMinus4,
            deltaPicOrderAlwaysZeroFlag, offsetForNonRefPic, offsetForTopToBottomField, numRefFramesInPicOrderCntCycle,
            maxNumRefFrames, gapsInFrameNumValueAllowedFlag, picWidthInMbsMinus1, picHeightInMapUnitsMinus1, frameMbsOnlyFlag,
            macroblockAdaptiveFrameFieldFlag, direct8x8InferenceFlag, frameCroppingFlag, frameCropLeftOffset, frameCropRightOffset, frameCropTopOffset, frameCropBottomOffset,
            vuiParametersPresentFlag, vuip);

        ValidateSpsFields(sps);
    }

    private static void ValidateSpsFields(SequenceParameterSet expected)
    {
        UseBSWriterThenReader(
            writer => expected.Write(writer, []),
            reader =>
            {
                SequenceParameterSet composed = SequenceParameterSet.Read(reader);

                Assert.Equal(expected.Kind, composed.Kind);
                Assert.Equal(expected.ProfileIdc, composed.ProfileIdc);
                Assert.Equal(expected.ConstraintSet0Flag, composed.ConstraintSet0Flag);
                Assert.Equal(expected.ConstraintSet1Flag, composed.ConstraintSet1Flag);
                Assert.Equal(expected.ConstraintSet2Flag, composed.ConstraintSet2Flag);
                Assert.Equal(expected.ConstraintSet3Flag, composed.ConstraintSet3Flag);
                Assert.Equal(expected.ConstraintSet4Flag, composed.ConstraintSet4Flag);
                Assert.Equal(expected.ConstraintSet5Flag, composed.ConstraintSet5Flag);
                Assert.Equal(expected.ReservedZero2Bits, composed.ReservedZero2Bits);
                Assert.Equal(expected.LevelIdc, composed.LevelIdc);
                Assert.Equal(expected.SpsId, composed.SpsId);
                Assert.Equal(expected.ChromaFormatIdc, composed.ChromaFormatIdc);
                Assert.Equal(expected.SeparateColourPlaneFlag, composed.SeparateColourPlaneFlag);
                Assert.Equal(expected.BitDepthLumaMinus8, composed.BitDepthLumaMinus8);
                Assert.Equal(expected.BitDepthChromaMinus8, composed.BitDepthChromaMinus8);
                Assert.Equal(expected.QpprimeYZeroTransformBypassFlag, composed.QpprimeYZeroTransformBypassFlag);
                Assert.Equal(expected.SeqScalingMatrixPresentFlag, composed.SeqScalingMatrixPresentFlag);
                Assert.Equal(expected.ScalingMatrix, composed.ScalingMatrix);
                Assert.Equal(expected.Log2MaxFrameNumMinus4, composed.Log2MaxFrameNumMinus4);
                Assert.Equal(expected.PicOrderCntType, composed.PicOrderCntType);
                Assert.Equal(expected.Log2MaxPicOrderCntLsbMinus4, composed.Log2MaxPicOrderCntLsbMinus4);
                Assert.Equal(expected.DeltaPicOrderAlwaysZeroFlag, composed.DeltaPicOrderAlwaysZeroFlag);
                Assert.Equal(expected.OffsetForNonRefPic, composed.OffsetForNonRefPic);
                Assert.Equal(expected.OffsetForTopToBottomField, composed.OffsetForTopToBottomField);
                Assert.Equal(expected.NumRefFramesInPicOrderCntCycle, composed.NumRefFramesInPicOrderCntCycle);
                Assert.Equal(expected.MaxNumRefFrames, composed.MaxNumRefFrames);
                Assert.Equal(expected.GapsInFrameNumValueAllowedFlag, composed.GapsInFrameNumValueAllowedFlag);
                Assert.Equal(expected.PicWidthInMbsMinus1, composed.PicWidthInMbsMinus1);
                Assert.Equal(expected.PicHeightInMapUnitsMinus1, composed.PicHeightInMapUnitsMinus1);
                Assert.Equal(expected.FrameMbsOnlyFlag, composed.FrameMbsOnlyFlag);
                Assert.Equal(expected.MbAdaptiveFrameFieldFlag, composed.MbAdaptiveFrameFieldFlag);
                Assert.Equal(expected.Direct8X8InferenceFlag, composed.Direct8X8InferenceFlag);
                Assert.Equal(expected.FrameCroppingFlag, composed.FrameCroppingFlag);
                Assert.Equal(expected.FrameCropLeftOffset, composed.FrameCropLeftOffset);
                Assert.Equal(expected.FrameCropRightOffset, composed.FrameCropRightOffset);
                Assert.Equal(expected.FrameCropTopOffset, composed.FrameCropTopOffset);
                Assert.Equal(expected.FrameCropBottomOffset, composed.FrameCropBottomOffset);
                Assert.Equal(expected.VuiParametersPresentFlag, composed.VuiParametersPresentFlag);
                Assert.Equal(expected.VuiParameters, composed.VuiParameters);
            });
    }

    private static void UseBSWriterThenReader(Action<BitStreamWriter> writer, Action<BitStreamReader> reader)
    {
        using var msWriter = new MemoryStream();
        using var bw = new BitStreamWriter(msWriter);
        writer(bw);

        using var msReader = new MemoryStream(msWriter.ToArray());
        using var br = new BitStreamReader(msReader);
        reader(br);
    }
}
