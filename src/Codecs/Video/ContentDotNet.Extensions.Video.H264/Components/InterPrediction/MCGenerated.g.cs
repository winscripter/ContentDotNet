namespace ContentDotNet.Extensions.Video.H264.Components.InterPrediction
{
    using ContentDotNet.Colors;
    using ContentDotNet.Extensions.Video.H264.Components.Common;
    using ContentDotNet.Pictures;

    public static partial class MotionCompensation
    {
        private static void Generated_InterpolateLumaSample(
            int xIntL,
            int yIntL,
            int xFracL,
            int yFracL,
            int xL,
            int yL,
            int PicHeightInSamplesL,
            int PicWidthInSamplesL,
            bool MbaffFrameFlag,
            bool mb_field_decoding_flag,
            int BitDepthY,
            Picture<YCbCr> refPicLX,
            Picture<YCbCr> predPartLX,
            Func<YCbCr, int, YCbCr> mutateChannel)
        {
            int refPicHeightEffectiveL = !MbaffFrameFlag || !mb_field_decoding_flag ? PicHeightInSamplesL : PicHeightInSamplesL / 2;

            
            int xDAL = XDzl[0];
            int yDAL = YDzl[0];
            int xAL = Clipping.Clip(0, PicWidthInSamplesL - 1, xIntL + xDAL);
            int yAL = Clipping.Clip(0, refPicHeightEffectiveL - 1, yIntL + yDAL);
            int A = refPicLX[xAL, yAL].Y;
            
            int xDBL = XDzl[1];
            int yDBL = YDzl[1];
            int xBL = Clipping.Clip(0, PicWidthInSamplesL - 1, xIntL + xDBL);
            int yBL = Clipping.Clip(0, refPicHeightEffectiveL - 1, yIntL + yDBL);
            int B = refPicLX[xBL, yBL].Y;
            
            int xDCL = XDzl[2];
            int yDCL = YDzl[2];
            int xCL = Clipping.Clip(0, PicWidthInSamplesL - 1, xIntL + xDCL);
            int yCL = Clipping.Clip(0, refPicHeightEffectiveL - 1, yIntL + yDCL);
            int C = refPicLX[xCL, yCL].Y;
            
            int xDDL = XDzl[3];
            int yDDL = YDzl[3];
            int xDL = Clipping.Clip(0, PicWidthInSamplesL - 1, xIntL + xDDL);
            int yDL = Clipping.Clip(0, refPicHeightEffectiveL - 1, yIntL + yDDL);
            int D = refPicLX[xDL, yDL].Y;
            
            int xDEL = XDzl[4];
            int yDEL = YDzl[4];
            int xEL = Clipping.Clip(0, PicWidthInSamplesL - 1, xIntL + xDEL);
            int yEL = Clipping.Clip(0, refPicHeightEffectiveL - 1, yIntL + yDEL);
            int E = refPicLX[xEL, yEL].Y;
            
            int xDFL = XDzl[5];
            int yDFL = YDzl[5];
            int xFL = Clipping.Clip(0, PicWidthInSamplesL - 1, xIntL + xDFL);
            int yFL = Clipping.Clip(0, refPicHeightEffectiveL - 1, yIntL + yDFL);
            int F = refPicLX[xFL, yFL].Y;
            
            int xDGL = XDzl[6];
            int yDGL = YDzl[6];
            int xGL = Clipping.Clip(0, PicWidthInSamplesL - 1, xIntL + xDGL);
            int yGL = Clipping.Clip(0, refPicHeightEffectiveL - 1, yIntL + yDGL);
            int G = refPicLX[xGL, yGL].Y;
            
            int xDHL = XDzl[7];
            int yDHL = YDzl[7];
            int xHL = Clipping.Clip(0, PicWidthInSamplesL - 1, xIntL + xDHL);
            int yHL = Clipping.Clip(0, refPicHeightEffectiveL - 1, yIntL + yDHL);
            int H = refPicLX[xHL, yHL].Y;
            
            int xDIL = XDzl[8];
            int yDIL = YDzl[8];
            int xIL = Clipping.Clip(0, PicWidthInSamplesL - 1, xIntL + xDIL);
            int yIL = Clipping.Clip(0, refPicHeightEffectiveL - 1, yIntL + yDIL);
            int I = refPicLX[xIL, yIL].Y;
            
            int xDJL = XDzl[9];
            int yDJL = YDzl[9];
            int xJL = Clipping.Clip(0, PicWidthInSamplesL - 1, xIntL + xDJL);
            int yJL = Clipping.Clip(0, refPicHeightEffectiveL - 1, yIntL + yDJL);
            int J = refPicLX[xJL, yJL].Y;
            
            int xDKL = XDzl[10];
            int yDKL = YDzl[10];
            int xKL = Clipping.Clip(0, PicWidthInSamplesL - 1, xIntL + xDKL);
            int yKL = Clipping.Clip(0, refPicHeightEffectiveL - 1, yIntL + yDKL);
            int K = refPicLX[xKL, yKL].Y;
            
            int xDLL = XDzl[11];
            int yDLL = YDzl[11];
            int xLL = Clipping.Clip(0, PicWidthInSamplesL - 1, xIntL + xDLL);
            int yLL = Clipping.Clip(0, refPicHeightEffectiveL - 1, yIntL + yDLL);
            int L = refPicLX[xLL, yLL].Y;
            
            int xDML = XDzl[12];
            int yDML = YDzl[12];
            int xML = Clipping.Clip(0, PicWidthInSamplesL - 1, xIntL + xDML);
            int yML = Clipping.Clip(0, refPicHeightEffectiveL - 1, yIntL + yDML);
            int M = refPicLX[xML, yML].Y;
            
            int xDNL = XDzl[13];
            int yDNL = YDzl[13];
            int xNL = Clipping.Clip(0, PicWidthInSamplesL - 1, xIntL + xDNL);
            int yNL = Clipping.Clip(0, refPicHeightEffectiveL - 1, yIntL + yDNL);
            int N = refPicLX[xNL, yNL].Y;
            
            int xDOL = XDzl[14];
            int yDOL = YDzl[14];
            int xOL = Clipping.Clip(0, PicWidthInSamplesL - 1, xIntL + xDOL);
            int yOL = Clipping.Clip(0, refPicHeightEffectiveL - 1, yIntL + yDOL);
            int O = refPicLX[xOL, yOL].Y;
            
            int xDPL = XDzl[15];
            int yDPL = YDzl[15];
            int xPL = Clipping.Clip(0, PicWidthInSamplesL - 1, xIntL + xDPL);
            int yPL = Clipping.Clip(0, refPicHeightEffectiveL - 1, yIntL + yDPL);
            int P = refPicLX[xPL, yPL].Y;
            
            int xDQL = XDzl[16];
            int yDQL = YDzl[16];
            int xQL = Clipping.Clip(0, PicWidthInSamplesL - 1, xIntL + xDQL);
            int yQL = Clipping.Clip(0, refPicHeightEffectiveL - 1, yIntL + yDQL);
            int Q = refPicLX[xQL, yQL].Y;
            
            int xDRL = XDzl[17];
            int yDRL = YDzl[17];
            int xRL = Clipping.Clip(0, PicWidthInSamplesL - 1, xIntL + xDRL);
            int yRL = Clipping.Clip(0, refPicHeightEffectiveL - 1, yIntL + yDRL);
            int R = refPicLX[xRL, yRL].Y;
            
            int xDSL = XDzl[18];
            int yDSL = YDzl[18];
            int xSL = Clipping.Clip(0, PicWidthInSamplesL - 1, xIntL + xDSL);
            int ySL = Clipping.Clip(0, refPicHeightEffectiveL - 1, yIntL + yDSL);
            int S = refPicLX[xSL, ySL].Y;
            
            int xDTL = XDzl[19];
            int yDTL = YDzl[19];
            int xTL = Clipping.Clip(0, PicWidthInSamplesL - 1, xIntL + xDTL);
            int yTL = Clipping.Clip(0, refPicHeightEffectiveL - 1, yIntL + yDTL);
            int T = refPicLX[xTL, yTL].Y;
            
            int xDUL = XDzl[20];
            int yDUL = YDzl[20];
            int xUL = Clipping.Clip(0, PicWidthInSamplesL - 1, xIntL + xDUL);
            int yUL = Clipping.Clip(0, refPicHeightEffectiveL - 1, yIntL + yDUL);
            int U = refPicLX[xUL, yUL].Y;
            
                        int cc = CommonFunctions.Middle(E, K);
                        int dd = CommonFunctions.Middle(F, L);
                        int aa = CommonFunctions.Middle(A, B);
                        int bb = CommonFunctions.Middle(C, D);
                        int ee = CommonFunctions.Middle(I, P);
                        int ff = CommonFunctions.Middle(J, Q);
                        int gg = CommonFunctions.Middle(R, S);
                        int hh = CommonFunctions.Middle(T, U);
            
            int m1 = K - 5 * L + 20 * M + 20 * N - 5 * P + Q;
            int s1 = B - 5 * D + 20 * H + 20 * N - 5 * S + U;
            int b1 = (E - 5 * F + 20 * G + 20 * H - 5 * I + J);
            int h1 = (A - 5 * C + 20 * G + 20 * M - 5 * R + T);

            int b = Clipping.Clip1Y((b1 + 16) >> 5, BitDepthY);
            int h = Clipping.Clip1Y((h1 + 16) >> 5, BitDepthY);

            int j1 = cc - 5 * dd + 20 * h1 + 20 * m1 - 5 * ee + ff;

            int j = Clipping.Clip1Y((j1 + 512) >> 10, BitDepthY);

            int s = Clipping.Clip1Y((s1 + 16) >> 5, BitDepthY);   
            int m = Clipping.Clip1Y((m1 + 16) >> 5, BitDepthY);

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

            var predPart = predPartLX[xL, yL];
            predPart = mutateChannel(predPart, (xFracL, yFracL) switch
            {
                (0, 0) => (byte)G,
                (0, 1) => (byte)d,
                (0, 2) => (byte)h,
                (0, 3) => (byte)n,
                (1, 0) => (byte)a,
                (1, 1) => (byte)e,
                (1, 2) => (byte)i,
                (1, 3) => (byte)p,
                (2, 0) => (byte)b,
                (2, 1) => (byte)f,
                (2, 2) => (byte)j,
                (2, 3) => (byte)q,
                (3, 0) => (byte)c,
                (3, 1) => (byte)g,
                (3, 2) => (byte)k,
                (3, 3) => (byte)r,
                _ => throw new InvalidOperationException("xFracL and yFracL may only range from 0 to 3")
            });
            predPartLX[xL, yL] = predPart;
        }
    }
}
