using ContentDotNet.Abstractions;
using ContentDotNet.Containers;
using ContentDotNet.Extensions.H264.Internal.Encoding.Predicted;
using ContentDotNet.Extensions.H264.Models;
using ContentDotNet.Extensions.H264.Pictures;
using ContentDotNet.Extensions.H264.Utilities;
using System.Drawing;

namespace ContentDotNet.Extensions.H264.Internal.Encoding;

internal static class H264MacroblockPredictor
{
    public static InheritedMacroblock? PredictInheritedMacroblock(
        Dpb dpbL0, Dpb? dpbL1,
        int dpbSizeL0, int dpbSizeL1,
        Matrix16x16 srcL, Matrix16x16 srcCb, Matrix16x16 srcCr,
        Size partitionSize,
        Rectangle pels)
    {
        if (dpbL1 is null)
            return Predict(srcL, srcCb, srcCr, dpbSizeL0, dpbL0);
        var l0 = Predict(srcL, srcCb, srcCr, dpbSizeL0, dpbL0);
        if (l0 is null)
            return Predict(srcL, srcCb, srcCr, dpbSizeL1, dpbL1);

        return null;

        InheritedMacroblock? Predict(Matrix16x16 srcL, Matrix16x16 srcCb, Matrix16x16 srcCr, int dpbSize, Dpb dpb)
        {
            Span<int> lBacking = stackalloc int[16 * 16];
            Span<int> cbBacking = stackalloc int[16 * 16];
            Span<int> crBacking = stackalloc int[16 * 16];
            Matrix16x16 l = new(lBacking);
            Matrix16x16 cb = new(cbBacking);
            Matrix16x16 cr = new(crBacking);

            for (int i = 0; i < dpbSize; i++)
            {
                if (_Core(l, cb, cr, srcL, srcCb, srcCr))
                {
                    return new InheritedMacroblock()
                    {
                        Direction = false,
                        ReferencePictureIndex = i,
                        Size = partitionSize
                    };
                }

                bool _Core(Matrix16x16 l, Matrix16x16 cb, Matrix16x16 cr, Matrix16x16 srcL, Matrix16x16 srcCb, Matrix16x16 srcCr)
                {
                    var picRef = dpb[i];
                    if (picRef is null)
                        return false;

                    for (int x = 0; x < partitionSize.Width; x++)
                    {
                        for (int y = 0; y < partitionSize.Height; y++)
                        {
                            l[x, y] = picRef.Frame.Y[x, y];
                            cb[x, y] = picRef.Frame.U[x, y];
                            cr[x, y] = picRef.Frame.V[x, y];

                            if (l[x, y] != srcL[x, y] ||
                                cb[x, y] != srcCb[x, y] ||
                                cr[x, y] != srcCr[x, y])
                            {
                                return false;
                            }
                        }
                    }

                    return true;
                }
            }

            return null;
        }
    }

    // I used ChatGPT for this 💀🙏🏻 wish me luck
    // if only the spec provided how to actually encode H.264
    public static WeightedPredictionResult? AnalyzeWeightedPrediction(
        Dpb dpbL0,
        int dpbSizeL0,
        Matrix16x16 srcL,
        Size partitionSize, bool isBSlice)
    {
        const uint defaultDenom = 5;
        var lumaWeightL0 = new Container32Int32();
        var lumaOffsetL0 = new Container32Int32();
        var chromaWeightL0 = new ContainerMatrix2x32();
        var chromaOffsetL0 = new ContainerMatrix2x32();
        var lumaWeightL0Flag = new Container32Boolean();
        var chromaWeightL0Flag = new Container32Boolean();

        bool anyWeightingUsed = false;

        for (int i = 0; i < dpbSizeL0; i++)
        {
            var refPic = dpbL0[i];
            if (refPic is null) continue;

            int weightSum = 0, offsetSum = 0, pixelCount = 0;

            for (int y = 0; y < partitionSize.Height; y++)
            {
                for (int x = 0; x < partitionSize.Width; x++)
                {
                    int refVal = refPic.Frame.Y[x, y];
                    int srcVal = srcL[x, y];

                    int offset = srcVal - refVal;
                    offsetSum += offset;
                    weightSum += (srcVal * refVal);
                    pixelCount++;
                }
            }

            if (pixelCount == 0) continue;

            int avgOffset = offsetSum / pixelCount;
            int avgRef = Math.Max(1, weightSum / pixelCount);

            int weight = (srcL[0, 0] << (int)defaultDenom) / avgRef;

            if (weight != (1 << (int)defaultDenom) || avgOffset != 0)
            {
                lumaWeightL0Flag[i] = true;
                lumaWeightL0[i] = weight;
                lumaOffsetL0[i] = avgOffset;
                anyWeightingUsed = true;
            }
        }

        var table = new PredWeightTable(
            lumaLog2WeightDenom: defaultDenom,
            chromaLog2WeightDenom: defaultDenom,
            lumaWeightL0Flag,
            lumaWeightL0,
            lumaOffsetL0,
            chromaWeightL0Flag,
            chromaWeightL0,
            chromaOffsetL0,
            default, default, default,
            default, default, default
        );

        if (!anyWeightingUsed)
            return null;

        return new WeightedPredictionResult(
            table,
            predFlag: !isBSlice,
            bipredIdc: (byte)(isBSlice ? 1 : 0)
        );
    }
}
