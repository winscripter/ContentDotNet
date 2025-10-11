namespace ContentDotNet.Extensions.Video.H264.Components.IntraPrediction.Implementation
{
    using ContentDotNet.Colors;
    using ContentDotNet.Extensions.Video.H264.Components.IntraPrediction.Abstractions;
    using ContentDotNet.Extensions.Video.H264.Models;
    using ContentDotNet.Pictures;

    internal class IntraService : IIntraService
    {
        public H264State State { get; }

        public IIntraPredictors Predictors { get; }

        public H264MacroblockInfo? CurrentMacroblock { get; }

        public IntraService(H264State state)
            : this(state, new DefaultIntraPredictors(), null)
        {
        }

        public IntraService(H264State state, IIntraPredictors predictors)
            : this(state, predictors, null)
        {
        }

        public IntraService(H264State state, IIntraPredictors predictors, H264MacroblockInfo? currentMacroblock)
        {
            State = state;
            Predictors = predictors;
            CurrentMacroblock = currentMacroblock;
        }

        public IntraPredictionSamples CreateIntra16x16Samples(Picture<YCbCr> pic)
        {
            throw new NotImplementedException();
        }

        public IntraPredictionSamples CreateIntra4x4Samples(Picture<YCbCr> pic)
        {
            throw new NotImplementedException();
        }

        public IntraPredictionSamples CreateIntra8x8Samples(Picture<YCbCr> pic)
        {
            throw new NotImplementedException();
        }

        public IntraPredictionSamples CreateIntraChromaSamples(Picture<YCbCr> pic)
        {
            throw new NotImplementedException();
        }

        public void Predict16x16(IntraPredictionSamples samples)
        {
            throw new NotImplementedException();
        }

        public void Predict4x4(IntraPredictionSamples samples)
        {
            throw new NotImplementedException();
        }

        public void Predict8x8(IntraPredictionSamples samples)
        {
            throw new NotImplementedException();
        }

        public void PredictChroma(IntraPredictionSamples samples)
        {
            throw new NotImplementedException();
        }

        public void PredictPcm(int[,] sl, int[,] scb, int[,] scr)
        {
            throw new NotImplementedException();
        }
    }
}
