using ContentDotNet.Extensions.H264.Helpers;
using ContentDotNet.Extensions.H264.Macroblocks;
using ContentDotNet.Extensions.H264.Models;
using ContentDotNet.Extensions.H264.Utilities;
using System.Runtime.CompilerServices;

namespace ContentDotNet.Extensions.H264.Internal.Decoding;

internal sealed partial class BaselineDecoder
{
    public sealed partial class Inter
    {
        public static void InterpolateFractionalSample(
            int mbIndexX,
            int mbIndexY,
            MacroblockSizeChroma size,
            int mbPartIdx,
            int subMbPartIdx,
            int partWidth,
            int partHeight,
            MotionVector mvLX,
            MotionVector mvCLX,
            int[] refPicLXL,
            int[]? refPicLXCB,
            int[]? refPicLXCR,
            /*out*/ Matrix16x16 predPartLXL,
            /*out*/ Matrix16x16 predPartLXCB,
            /*out*/ Matrix16x16 predPartLXCR)
        {
            int xAL = (mbIndexX * 16) + (subMbPartIdx * size.Width);
            int yAL = (mbIndexY * 16) + (subMbPartIdx * size.Height);

        }

        public static void InterpolateLumaSample(
            Matrix6x6 refPicLXL,
            SequenceParameterSet sps,
            bool mbaffFrameFlag,
            int xIntL,
            int yIntL,
            int xFracL,
            int yFracL,
            int xL,
            int yL,
            int bitDepthY,
            Matrix16x16 predPartLXL)
        {
            int PicHeightInSamplesL = sps.GetPicHeightInSamplesL();
            int PicWidthInSamplesL = sps.GetPicHeightInSamplesL();

            int refPicHeightEffectiveL = mbaffFrameFlag ? PicHeightInSamplesL / 2 : PicHeightInSamplesL;

            int xAL = Util264.Clip3(0, PicWidthInSamplesL - 1, xIntL + 0);
            int yAL = Util264.Clip3(0, refPicHeightEffectiveL - 1, yIntL + -2);

            int xBL = Util264.Clip3(0, PicWidthInSamplesL - 1, xIntL + 1);
            int yBL = Util264.Clip3(0, refPicHeightEffectiveL - 1, yIntL + -2);

            int xCL = Util264.Clip3(0, PicWidthInSamplesL - 1, xIntL + 0);
            int yCL = Util264.Clip3(0, refPicHeightEffectiveL - 1, yIntL + -1);

            int xDL = Util264.Clip3(0, PicWidthInSamplesL - 1, xIntL + 1);
            int yDL = Util264.Clip3(0, refPicHeightEffectiveL - 1, yIntL + -1);

            int xEL = Util264.Clip3(0, PicWidthInSamplesL - 1, xIntL + -2);
            int yEL = Util264.Clip3(0, refPicHeightEffectiveL - 1, yIntL + 0);

            int xFL = Util264.Clip3(0, PicWidthInSamplesL - 1, xIntL + -1);
            int yFL = Util264.Clip3(0, refPicHeightEffectiveL - 1, yIntL + 0);

            int xGL = Util264.Clip3(0, PicWidthInSamplesL - 1, xIntL);
            int yGL = Util264.Clip3(0, refPicHeightEffectiveL - 1, yIntL);

            int xHL = Util264.Clip3(0, PicWidthInSamplesL - 1, xIntL + 1);
            int yHL = Util264.Clip3(0, refPicHeightEffectiveL - 1, yIntL);

            int xIL = Util264.Clip3(0, PicWidthInSamplesL - 1, xIntL + 2);
            int yIL = Util264.Clip3(0, refPicHeightEffectiveL - 1, yIntL);

            int xJL = Util264.Clip3(0, PicWidthInSamplesL - 1, xIntL + 3);
            int yJL = Util264.Clip3(0, refPicHeightEffectiveL - 1, yIntL);

            int xKL = Util264.Clip3(0, PicWidthInSamplesL - 1, xIntL + -2);
            int yKL = Util264.Clip3(0, refPicHeightEffectiveL - 1, yIntL + 1);

            int xLL = Util264.Clip3(0, PicWidthInSamplesL - 1, xIntL + -1);
            int yLL = Util264.Clip3(0, refPicHeightEffectiveL - 1, yIntL + 1);

            int xML = Util264.Clip3(0, PicWidthInSamplesL - 1, xIntL + 0); // xML? more like XML! ;)
            int yML = Util264.Clip3(0, refPicHeightEffectiveL - 1, yIntL + 1);

            int xNL = Util264.Clip3(0, PicWidthInSamplesL - 1, xIntL + 1);
            int yNL = Util264.Clip3(0, refPicHeightEffectiveL - 1, yIntL + 1);

            int xPL = Util264.Clip3(0, PicWidthInSamplesL - 1, xIntL + 2);
            int yPL = Util264.Clip3(0, refPicHeightEffectiveL - 1, yIntL + 1);

            int xQL = Util264.Clip3(0, PicWidthInSamplesL - 1, xIntL + 3);
            int yQL = Util264.Clip3(0, refPicHeightEffectiveL - 1, yIntL + 1);

            int xRL = Util264.Clip3(0, PicWidthInSamplesL - 1, xIntL + 0);
            int yRL = Util264.Clip3(0, refPicHeightEffectiveL - 1, yIntL + 2);

            int xSL = Util264.Clip3(0, PicWidthInSamplesL - 1, xIntL + 1);
            int ySL = Util264.Clip3(0, refPicHeightEffectiveL - 1, yIntL + 2);

            int xTL = Util264.Clip3(0, PicWidthInSamplesL - 1, xIntL + 0);
            int yTL = Util264.Clip3(0, refPicHeightEffectiveL - 1, yIntL + 3);

            int xUL = Util264.Clip3(0, PicWidthInSamplesL - 1, xIntL + 1);
            int yUL = Util264.Clip3(0, refPicHeightEffectiveL - 1, yIntL + 3);

            int A = refPicLXL[xAL, yAL];
            int B = refPicLXL[xBL, yBL];
            int C = refPicLXL[xCL, yCL];
            int D = refPicLXL[xDL, yDL];
            int E = refPicLXL[xEL, yEL];
            int F = refPicLXL[xFL, yFL];
            int G = refPicLXL[xGL, yGL];
            int H = refPicLXL[xHL, yHL];
            int I = refPicLXL[xIL, yIL];
            int J = refPicLXL[xJL, yJL];
            int K = refPicLXL[xKL, yKL];
            int L = refPicLXL[xLL, yLL];
            int M = refPicLXL[xML, yML];
            int N = refPicLXL[xNL, yNL];
            int P = refPicLXL[xPL, yPL];
            int Q = refPicLXL[xQL, yQL];
            int R = refPicLXL[xRL, yRL];
            int S = refPicLXL[xSL, ySL];
            int T = refPicLXL[xTL, yTL];
            int U = refPicLXL[xUL, yUL];

            int cc = Util264.Middle(E, K);
            int dd = Util264.Middle(F, L);
            int ee = Util264.Middle(I, P);
            int ff = Util264.Middle(J, Q);

            int b1 = E - 5 * F + 20 * G + 20 * H - 5 * I + J;
            int h1 = A - 5 * C + 20 * G + 20 * M - 5 * R + T;

            int b = Util264.Clip1Y((b1 + 16) >> 5, bitDepthY);
            int h = Util264.Clip1Y((h1 + 16) >> 5, bitDepthY);

            int m1 = K - 5 * L + 20 * M + 20 * N - 5 * P + Q;
            int s1 = B - 5 * D + 20 * H + 20 * N - 5 * S + U;

            int j1 = cc - 5 * dd + 20 * h1 + 20 * m1 - 5 * ee + ff;

            int j = Util264.Clip1Y((j1 + 512) >> 10, bitDepthY);

            int s = Util264.Clip1Y((s1 + 16) >> 5, bitDepthY);
            int m = Util264.Clip1Y((m1 + 16) >> 5, bitDepthY);

            int a = (G + b + 1) >> 1;
            int c = (H + b + 1) >> 1;
            int d = (G + h + 1) >> 1;
            int n = (M + h + 1) >> 1;
            int f = (b + j + 1) >> 1;
            int i = (h + j + 1) >> 1;
            int k = (j + m + 1) >> 1;
            int q = (j + s + 1) >> 1;

            int e = (b + h + 1) >> 1;
            int g = (b + m + 1) >> 1;
            int p = (h + s + 1) >> 1;
            int r = (m + s + 1) >> 1;

            predPartLXL[xL, yL] = (xFracL, yFracL) switch
            {
                (0, 0) => G,
                (0, 1) => d,
                (0, 2) => h,
                (0, 3) => n,
                (1, 0) => a,
                (1, 1) => e,
                (1, 2) => i,
                (1, 3) => p,
                (2, 0) => b,
                (2, 1) => f,
                (2, 2) => j,
                (2, 3) => q,
                (3, 0) => c,
                (3, 1) => g,
                (3, 2) => k,
                (3, 3) => r,
                _ => throw new InvalidOperationException("xFracL and yFracL may only range from 0 to 3")
            };
        }
    }
}
