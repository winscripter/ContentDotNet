namespace ContentDotNet.Extensions.Video.H264.Components.IntraPrediction.Implementation.Predictors
{
    using ContentDotNet.Extensions.Video.H264.Components.Common.Derivative;
    using ContentDotNet.Extensions.Video.H264.Components.IntraPrediction.Abstractions;
    using ContentDotNet.Extensions.Video.H264.Models;
    using ContentDotNet.Primitives;

    internal class DefaultIntraPcmPredictor : IIntraPcmPredictor
    {
        public H264State? H264State { get; set; }

        public void Predict(H264MacroblockInfo currMB, int[,] sl, int[,] scb, int[,] scr)
        {
            if (H264State == null)
                throw IntraThrowHelper.NoH264State();

            int dy = (H264State.DeriveMbaffFrameFlag() && !H264State.MacroblockUtility.IsFrame(currMB)) ? 2 : 1;

            XY xy = H264Derivative.InverseMacroblockScan(H264State.CurrMbAddr, H264State);

            int xP = xy.X;
            int yP = xy.Y;

            for (int i = 0; i < 256; i++)
                sl[xP + (i % 16), yP + dy * (i / 16)] = currMB.Rbsp.PcmSampleLuma![i];

            if (H264State.DeriveChromaArrayType() != 0)
            {
                H264MacroblockChromaSizes sizes = H264State.DeriveChromaMacroblockSizes();
                int MbWidthC = sizes.MbWidthC;
                int MbHeightC = sizes.MbHeightC;

                H264ChromaFormat cf = H264State.DeriveChromaFormat();
                int SubWidthC = cf.ChromaWidth;
                int SubHeightC = cf.ChromaHeight;

                for (int i = 0; i < MbWidthC * MbHeightC; i++)
                {
                    scb[(xP / SubWidthC) + (i % MbWidthC), ((yP + SubHeightC - 1) / SubHeightC) + dy * (i / MbWidthC)] = currMB.Rbsp.PcmSampleChroma![i];
                    scr[(xP / SubWidthC) + (i % MbWidthC), ((yP + SubHeightC - 1) / SubHeightC) + dy * (i / MbWidthC)] = currMB.Rbsp.PcmSampleChroma[i + MbWidthC * MbHeightC];
                }
            }
        }
    }
}
