namespace ContentDotNet.Extensions.Video.H264.Components.InterPrediction
{
    using ContentDotNet.Colors;
    using ContentDotNet.Extensions.Video.H264.Components.Common;
    using ContentDotNet.Extensions.Video.H264.Models;
    using ContentDotNet.Pictures;

    /// <summary>
    ///   H.264 Motion Compensation
    /// </summary>
    public static partial class MotionCompensation
    {
        private static ReadOnlySpan<int> XDzl =>
        [
            0, 1, 0, 1, -2, -1, 0, 1, 2, 3, -2, -1, 0, 1, 2, 3, 0, 1, 0, 1
        ];

        private static ReadOnlySpan<int> YDzl =>
        [
            -2, -2, -1, -1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 2, 2, 3, 3
        ];

        private static readonly Func<YCbCr, int, YCbCr> MutateCb = (x, y) =>
        {
            x.Cb = (byte)y;
            return x;
        };

        private static readonly Func<YCbCr, int, YCbCr> MutateCr = (x, y) =>
        {
            x.Cr = (byte)y;
            return x;
        };

        private static readonly Func<YCbCr, int, YCbCr> MutateY = (x, y) =>
        {
            x.Cr = (byte)y;
            return x;
        };

        /// <summary>
        ///   Performs Interpolation of Fractional sample.
        /// </summary>
        /// <param name="mbIndexX"></param>
        /// <param name="mbIndexY"></param>
        /// <param name="partWidth"></param>
        /// <param name="partHeight"></param>
        /// <param name="mb_field_decoding_flag"></param>
        /// <param name="state"></param>
        /// <param name="mvLX"></param>
        /// <param name="mvCLX"></param>
        /// <param name="refPicLX"></param>
        /// <param name="predPartLX"></param>
        public static void InterpolateFractionalSample(
            int mbIndexX,
            int mbIndexY,
            int partWidth,
            int partHeight,
            bool mb_field_decoding_flag,
            H264State state,
            H264MotionVector mvLX,
            H264MotionVector mvCLX,
            Picture<YCbCr> refPicLX,
            Picture<YCbCr> predPartLX)
        {
            int xAL = mbIndexX * 16;
            int yAL = mbIndexY * 16;

            for (int xL = 0; xL < partWidth; xL++)
            {
                for (int yL = 0; yL < partHeight; yL++)
                {
                    int xIntL = xAL + (mvLX.X >> 2) + xL;
                    int yIntL = yAL + (mvLX.Y >> 2) + yL;
                    int xFracL = mvLX.X & 3;
                    int yFracL = mvLX.Y & 3;

                    InterpolateLumaSample(xIntL, yIntL, xFracL, yFracL, xL, yL,
                        state.DerivePicHeightInSamplesL(), state.DerivePicWidthInSamplesL(), state.DeriveMbaffFrameFlag(),
                        mb_field_decoding_flag, state.DeriveBitDepthY(), refPicLX, predPartLX);
                }
            }

            if (state.DeriveChromaArrayType() != 0)
            {
                int ChromaArrayType = state.DeriveChromaArrayType();

                var cf = state.DeriveChromaFormat();
                int SubWidthC = cf.ChromaWidth;
                int SubHeightC = cf.ChromaHeight;

                for (int xC = 0; xC < partWidth; xC++)
                {
                    for (int yC = 0; yC < partHeight; yC++)
                    {
                        int xIntC, yIntC, xFracC, yFracC;

                        if (ChromaArrayType == 1)
                        {
                            xIntC = (xAL / SubWidthC) + (mvCLX.X >> 3) + xC;
                            yIntC = (yAL / SubHeightC) + (mvCLX.Y >> 3) + yC;
                            xFracC = mvCLX.X & 7;
                            yFracC = mvCLX.Y & 7;
                        }
                        else if (ChromaArrayType == 2)
                        {
                            xIntC = (xAL / SubWidthC) + (mvCLX.X >> 3) + xC;
                            yIntC = (yAL / SubHeightC) + (mvCLX.Y >> 2) + yC;
                            xFracC = mvCLX.X & 7;
                            yFracC = (mvCLX.Y & 3) << 1;
                        }
                        else /*ChromaArrayType == 3*/
                        {
                            xIntC = xAL + (mvLX.X >> 2) + xC;
                            yIntC = yAL + (mvLX.Y >> 2) + yC;
                            xFracC = (mvCLX.X & 3);
                            yFracC = (mvCLX.Y & 3);
                        }

                        if (ChromaArrayType != 3)
                        {
                            InterpolateChromaSample(xIntC, yIntC, xFracC, yFracC, xC, yC,
                                state.DerivePicHeightInSamplesC(), state.DerivePicWidthInSamplesC(), state.DeriveMbaffFrameFlag(),
                                mb_field_decoding_flag, refPicLX, predPartLX, MutateCb);
                            InterpolateChromaSample(xIntC, yIntC, xFracC, yFracC, xC, yC,
                                state.DerivePicHeightInSamplesC(), state.DerivePicWidthInSamplesC(), state.DeriveMbaffFrameFlag(),
                                mb_field_decoding_flag, refPicLX, predPartLX, MutateCr);
                        }
                        else
                        {
                            InterpolateLumaSample(xIntC, yIntC, xFracC, yFracC, xC, yC,
                                state.DerivePicHeightInSamplesL(), state.DerivePicWidthInSamplesL(), state.DeriveMbaffFrameFlag(),
                                mb_field_decoding_flag, state.DeriveBitDepthY(), refPicLX, predPartLX, MutateCb);
                            InterpolateLumaSample(xIntC, yIntC, xFracC, yFracC, xC, yC,
                                state.DerivePicHeightInSamplesL(), state.DerivePicWidthInSamplesL(), state.DeriveMbaffFrameFlag(),
                                mb_field_decoding_flag, state.DeriveBitDepthY(), refPicLX, predPartLX, MutateCr);
                        }
                    }
                }
            }
        }

        /// <summary>
        ///   Performs interpolation of the luma sample.
        /// </summary>
        /// <param name="xIntL"></param>
        /// <param name="yIntL"></param>
        /// <param name="xFracL"></param>
        /// <param name="yFracL"></param>
        /// <param name="xL"></param>
        /// <param name="yL"></param>
        /// <param name="PicHeightInSamplesL"></param>
        /// <param name="PicWidthInSamplesL"></param>
        /// <param name="MbaffFrameFlag"></param>
        /// <param name="mb_field_decoding_flag"></param>
        /// <param name="BitDepthY"></param>
        /// <param name="refPicLX"></param>
        /// <param name="predPartLX"></param>
        /// <param name="mutateChannel">The mutate channel delegate</param>
        public static void InterpolateLumaSample(
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
            Func<YCbCr, int, YCbCr>? mutateChannel = null)
        {
            mutateChannel ??= MutateY;

            Generated_InterpolateLumaSample(xIntL, yIntL, xFracL, yFracL, xL, yL, PicHeightInSamplesL, PicWidthInSamplesL, MbaffFrameFlag,
                mb_field_decoding_flag, BitDepthY, refPicLX, predPartLX, mutateChannel);
        }

        /// <summary>
        ///   Performs Interpolation of a Chroma sample.
        /// </summary>
        /// <param name="xIntC"></param>
        /// <param name="yIntC"></param>
        /// <param name="xFracC"></param>
        /// <param name="yFracC"></param>
        /// <param name="xC"></param>
        /// <param name="yC"></param>
        /// <param name="PicHeightInSamplesC"></param>
        /// <param name="PicWidthInSamplesC"></param>
        /// <param name="MbaffFrameFlag"></param>
        /// <param name="mb_field_decoding_flag"></param>
        /// <param name="refPicLX"></param>
        /// <param name="predPartLX"></param>
        /// <param name="mutateChromaChannel"></param>
        public static void InterpolateChromaSample(
            int xIntC,
            int yIntC,
            int xFracC,
            int yFracC,
            int xC,
            int yC,
            int PicHeightInSamplesC,
            int PicWidthInSamplesC,
            bool MbaffFrameFlag,
            bool mb_field_decoding_flag,
            Picture<YCbCr> refPicLX,
            Picture<YCbCr> predPartLX,
            Func<YCbCr, int, YCbCr> mutateChromaChannel)
        {
            int refPicHeightEffectiveC = !MbaffFrameFlag || !mb_field_decoding_flag ? PicHeightInSamplesC : PicHeightInSamplesC / 2;

            int xAC = Clipping.Clip(0, PicWidthInSamplesC - 1, xIntC);
            int xBC = Clipping.Clip(0, PicWidthInSamplesC - 1, xIntC + 1);
            int xCC = Clipping.Clip(0, PicWidthInSamplesC - 1, xIntC);
            int xDC = Clipping.Clip(0, PicWidthInSamplesC - 1, xIntC + 1);
            int yAC = Clipping.Clip(0, refPicHeightEffectiveC - 1, yIntC);
            int yBC = Clipping.Clip(0, refPicHeightEffectiveC - 1, yIntC);
            int yCC = Clipping.Clip(0, refPicHeightEffectiveC - 1, yIntC + 1);
            int yDC = Clipping.Clip(0, refPicHeightEffectiveC - 1, yIntC + 1);

            int A = refPicLX[xAC, yAC].Y;
            int B = refPicLX[xBC, yBC].Y;
            int C = refPicLX[xCC, yCC].Y;
            int D = refPicLX[xDC, yDC].Y;

            YCbCr predPart = predPartLX[xC, yC];
            predPart = mutateChromaChannel(predPart, ((8 - xFracC) * (8 - yFracC) * A + xFracC * (8 - yFracC) * B +
                (8 - xFracC) * yFracC * C + xFracC * yFracC * D + 32) >> 6);
            predPartLX[xC, yC] = predPart;
        }
    }
}
