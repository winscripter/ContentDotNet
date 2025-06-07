using ContentDotNet.Extensions.H264.Internal.Encoding.Predicted;
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
}
