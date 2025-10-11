namespace ContentDotNet.Extensions.Video.H264.Components.IntraPrediction.Abstractions
{
    using ContentDotNet.Colors;
    using ContentDotNet.Extensions.Video.H264.Models;
    using ContentDotNet.Pictures;

    /// <summary>
    ///   The intra service.
    /// </summary>
    public interface IIntraService
    {
        /// <summary>
        ///   The H.264 state.
        /// </summary>
        H264State State { get; }

        /// <summary>
        ///   The intra predictors.
        /// </summary>
        IIntraPredictors Predictors { get; }

        /// <summary>
        ///   The current macroblock.
        /// </summary>
        H264MacroblockInfo? CurrentMacroblock { get; }

        /// <summary>
        ///   Performs intra 4x4 prediction.
        /// </summary>
        /// <param name="samples">The samples.</param>
        void Predict4x4(IntraPredictionSamples samples);

        /// <summary>
        ///   Creates intra 4x4 samples.
        /// </summary>
        /// <param name="pic">The source picture.</param>
        /// <returns>The intra prediction samples.</returns>
        IntraPredictionSamples CreateIntra4x4Samples(Picture<YCbCr> pic);

        /// <summary>
        ///   Performs intra 8x8 prediction.
        /// </summary>
        /// <param name="samples">The samples.</param>
        void Predict8x8(IntraPredictionSamples samples);

        /// <summary>
        ///   Creates intra 8x8 samples.
        /// </summary>
        /// <param name="pic">The source picture.</param>
        /// <returns>The intra prediction samples.</returns>
        IntraPredictionSamples CreateIntra8x8Samples(Picture<YCbCr> pic);

        /// <summary>
        ///   Performs intra 16x16 prediction.
        /// </summary>
        /// <param name="samples">The samples.</param>
        void Predict16x16(IntraPredictionSamples samples);

        /// <summary>
        ///   Creates intra 16x16 samples.
        /// </summary>
        /// <param name="pic">The source picture.</param>
        /// <returns>The intra prediction samples.</returns>
        IntraPredictionSamples CreateIntra16x16Samples(Picture<YCbCr> pic);

        /// <summary>
        ///   Performs intra chroma prediction.
        /// </summary>
        /// <param name="samples">The samples.</param>
        void PredictChroma(IntraPredictionSamples samples);

        /// <summary>
        ///   Creates intra chroma samples.
        /// </summary>
        /// <param name="pic">The source picture.</param>
        /// <returns>The intra prediction samples.</returns>
        IntraPredictionSamples CreateIntraChromaSamples(Picture<YCbCr> pic);

        /// <summary>
        ///   Performs intra PCM prediction.
        /// </summary>
        /// <param name="sl">The output samples (Luma).</param>
        /// <param name="scb">The output samples (Cb).</param>
        /// <param name="scr">The output samples (Cr).</param>
        void PredictPcm(int[,] sl, int[,] scb, int[,] scr);
    }
}
