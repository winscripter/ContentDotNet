namespace ContentDotNet.Extensions.Video.H264.Components.InterPrediction
{
    using ContentDotNet.Colors;
    using ContentDotNet.Extensions.Video.H264.Components.Common;
    using ContentDotNet.Extensions.Video.H264.Components.Dpb;
    using ContentDotNet.Extensions.Video.H264.Components.Dpb.Pictures;
    using ContentDotNet.Extensions.Video.H264.Components.SliceDecoding;
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Models;
    using ContentDotNet.Extensions.Video.H264.Models.Weights;
    using ContentDotNet.Extensions.Video.H264.RbspModels;

    /// <summary>
    ///   Derivative for prediction weights.
    /// </summary>
    public static class WeightedDerivative
    {
        /// <summary>
        ///   Performs derivation of prediction weights.
        /// </summary>
        /// <param name="refIdxL0"></param>
        /// <param name="refIdxL1"></param>
        /// <param name="predFlagL0"></param>
        /// <param name="predFlagL1"></param>
        /// <param name="state"></param>
        /// <param name="currentMacroblock"></param>
        /// <param name="currentPicture"></param>
        /// <param name="refPicList0"></param>
        /// <param name="refPicList1"></param>
        /// <param name="sliceDecoder"></param>
        /// <param name="logWDc"></param>
        /// <param name="o"></param>
        /// <param name="w"></param>
        /// <exception cref="NotImplementedException"></exception>
        public static void DerivePredictionWeights(
            int refIdxL0, int refIdxL1,
            bool predFlagL0, bool predFlagL1,
            H264State state, H264MacroblockInfo currentMacroblock, PictureDescriptor currentPicture,
            IDecodedPictureBuffer refPicList0, IDecodedPictureBuffer refPicList1,
            ISliceDecoder sliceDecoder,
            ref LogWDc logWDc, ref OArray o, ref WArray w)
        {
            int weighted_bipred_idc = (int?)state.H264RbspState?.PictureParameterSet?.WeightedBiPredIdc ?? 0;
            int slice_type = (int?)state.H264RbspState?.SliceHeader?.SliceType ?? 0;

            bool implicitModeFlag, explicitModeFlag;

            if (weighted_bipred_idc == 2 &&
                (slice_type % 5) == 1 &&
                predFlagL0 && predFlagL1)
            {
                implicitModeFlag = true;
                explicitModeFlag = false;
            }
            else if (weighted_bipred_idc == 1 &&
                (slice_type % 5) == 1 &&
                predFlagL0.AsInt32() + predFlagL1.AsInt32() is 1 or 2)
            {
                implicitModeFlag = false;
                explicitModeFlag = true;
            }

            else if (weighted_bipred_idc == 1 &&
                (slice_type % 5) is 1 or 3 &&
                predFlagL0)
            {
                implicitModeFlag = false;
                explicitModeFlag = true;
            }
            else
            {
                implicitModeFlag = false;
                explicitModeFlag = false;
            }

            Derive(ref logWDc, ref o, ref w, ChromaChannel.L);
            Derive(ref logWDc, ref o, ref w, ChromaChannel.Cb);
            Derive(ref logWDc, ref o, ref w, ChromaChannel.Cr);

            void Derive(
                ref LogWDc logWDc, ref OArray o, ref WArray w, ChromaChannel cc)
            {
                if (implicitModeFlag)
                {
                    logWDc.SetElement(cc, 5);
                    o.SetElementOrNothing(0, cc, 0);
                    o.SetElementOrNothing(1, cc, 0);

                    PictureDescriptor currPicOrField, pic0, pic1;

                    if (state?.H264RbspState?.SliceHeader?.FieldPicFlag == false &&
                        state?.MacroblockUtility.IsFrame(currentMacroblock) == false)
                    {
                        currPicOrField = WithParity(currentPicture, true);

                        if (refIdxL0 % 2 == 0)
                            pic0 = WithParity(refPicList0[refIdxL0 / 2], true);
                        else
                            pic0 = WithParity(refPicList0[refIdxL0 / 2], false);

                        if (refIdxL1 % 2 == 0)
                            pic1 = WithParity(refPicList1[refIdxL1 / 2], true);
                        else
                            pic1 = WithParity(refPicList1[refIdxL1 / 2], false);
                    }
                    else
                    {
                        currPicOrField = currentPicture;
                        pic0 = refPicList0[refIdxL0];
                        pic1 = refPicList1[refIdxL1];
                    }

                    int tb = Clipping.Clip(-128, 127, sliceDecoder!.DiffPicOrderCnt(currPicOrField.Poc.PictureOrderCount, pic0.Poc.PictureOrderCount));
                    int td = Clipping.Clip(-128, 127, sliceDecoder!.DiffPicOrderCnt(pic1.Poc.PictureOrderCount, pic0.Poc.PictureOrderCount));

                    int tx = (16384 + Math.Abs(td / 2)) / td;
                    int DistScaleFactor = Clipping.Clip(-1024, 1023, (tb * tx + 32) >> 6);

                    if (sliceDecoder.DiffPicOrderCnt(pic1.Poc.PictureOrderCount, pic0.Poc.PictureOrderCount) == 0 ||
                        pic0.Duration == PictureDuration.LongTerm || pic1.Duration == PictureDuration.LongTerm ||
                        (DistScaleFactor >> 2) < -64 || (DistScaleFactor >> 2) > 128)
                    {
                        w.SetElementOrNothing(0, cc, 32);
                        w.SetElementOrNothing(1, cc, 32);
                    }
                    else
                    {
                        w.SetElementOrNothing(0, cc, 64 - (DistScaleFactor >> 2));
                        w.SetElementOrNothing(1, cc, DistScaleFactor >> 2);
                    }
                }
                else if (explicitModeFlag)
                {
                    int refIdxL0WP = state.DeriveMbaffFrameFlag() && !state.MacroblockUtility.IsFrame(currentMacroblock) ? refIdxL0 >> 1 : refIdxL0;
                    int refIdxL1WP = state.DeriveMbaffFrameFlag() && !state.MacroblockUtility.IsFrame(currentMacroblock) ? refIdxL1 >> 1 : refIdxL1;

                    RbspPredWeightTable pwt = (state.H264RbspState?.SliceHeader?.PredWeightTable) ?? throw WeightedThrowHelper.PredWeightTableMissing();

                    if (cc == ChromaChannel.L)
                    {
                        logWDc.SetElement(cc, (int)pwt.LumaLog2WeightDenom);

                        w.SetElementOrNothing(0, cc, pwt.L0[refIdxL0WP].LumaWeightL0 ?? throw new NotImplementedException());
                        w.SetElementOrNothing(1, cc, pwt.L1?[refIdxL1WP].LumaWeightL1 ?? throw new NotImplementedException());

                        o.SetElementOrNothing(0, cc, (pwt.L0[refIdxL0WP].LumaOffsetL0 ?? throw new NotImplementedException()) * (1 << (state.DeriveBitDepthY() - 8)));
                        o.SetElementOrNothing(1, cc, (pwt.L1?[refIdxL1WP].LumaOffsetL1 ?? throw new NotImplementedException()) * (1 << (state.DeriveBitDepthY() - 8)));
                    }
                    else
                    {
                        int iCbCr = (int)cc - 1; // Cb = 1, so this becomes 0. Cr = 2, so this becomes 1.

                        logWDc.SetElement(cc, (int?)pwt.ChromaLog2WeightDenom ?? throw new NotImplementedException());

                        w.SetElementOrNothing(0, cc, pwt.L0[refIdxL0WP].ChromaWeightL0?[iCbCr] ?? throw new NotImplementedException());
                        w.SetElementOrNothing(1, cc, pwt.L1?[refIdxL1WP].ChromaWeightL1?[iCbCr] ?? throw new NotImplementedException());

                        o.SetElementOrNothing(0, cc, (pwt.L0[refIdxL0WP].ChromaOffsetL0?[iCbCr] ?? throw new NotImplementedException()) * (1 << (state.DeriveBitDepthY() - 8)));
                        o.SetElementOrNothing(1, cc, (pwt.L1?[refIdxL1WP].ChromaOffsetL1?[iCbCr] ?? throw new NotImplementedException()) * (1 << (state.DeriveBitDepthY() - 8)));
                    }
                }
                else
                {
                    /* Left as is */
                }
            }

            PictureDescriptor WithParity(PictureDescriptor pic, bool parity)
            {
                if (pic.Picture is ComplementaryFieldPair cfp)
                {
                    if (cfp.Top.Picture.IsSameParity(state) == parity)
                        return cfp.Top;
                    else if (cfp.Bottom.Picture.IsSameParity(state) == parity)
                        return cfp.Bottom;
                    else
                        return cfp.Top;
                }
                else
                {
                    return pic;
                }
            }
        }
    }
}
