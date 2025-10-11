namespace ContentDotNet.Extensions.Video.H264.Components.SliceDecoding
{
    using ContentDotNet.Extensions.Video.H264.Components.Dpb;
    using ContentDotNet.Extensions.Video.H264.Components.Dpb.Pictures;
    using ContentDotNet.Extensions.Video.H264.Extensions;
    using ContentDotNet.Extensions.Video.H264.Models;

    internal class SliceDecoderService : ISliceDecoder
    {
        public static readonly SliceDecoderService Instance = new();

        private static H264PictureOrderCount DerivePictureOrderCountsCore(H264State h264, PictureDescriptor prevPic, int frameNumOffsetOfPrevFrameIfPocTypeIs1)
        {
            int pictureOrderCountType = (int)h264.H264RbspState!.SequenceParameterSetData!.PicOrderCntType;
            int pic_order_cnt_lsb = (int?)h264.H264RbspState!.SliceHeader!.PicOrderCntLsb ?? 0;
            int delta_pic_order_cnt_bottom = h264.H264RbspState!.SliceHeader.DeltaPicOrderCntBottom ?? 0;
            bool field_pic_flag = h264.H264RbspState.SliceHeader.FieldPicFlag ?? false;
            int frame_num = (int)h264.H264RbspState.SliceHeader.FrameNum;
            int num_ref_frames_in_pic_order_cnt_cycle = (int)h264.H264RbspState.SequenceParameterSetData.NumRefFramesInPicOrderCntCycle;
            int nal_ref_idc = (int)h264.H264RbspState.NalUnit!.NalRefIdc;
            int offset_for_non_ref_pic = h264.H264RbspState.SequenceParameterSetData.OffsetForNonRefPic;
            int[] offset_for_ref_frame = h264.H264RbspState.SequenceParameterSetData.OffsetForRefFrame;
            int[] delta_pic_order_cnt = h264.H264RbspState.SliceHeader.DeltaPicOrderCnt ?? [];
            int offset_for_top_to_bottom_field = h264.H264RbspState.SequenceParameterSetData.OffsetForTopToBottomField;
            bool bottom_field_flag = h264.H264RbspState.SliceHeader.BottomFieldFlag ?? false;

            int ExpectedDeltaPerPicOrderCntCycle = 0;
            for (int i = 0; i < num_ref_frames_in_pic_order_cnt_cycle; i++)
                ExpectedDeltaPerPicOrderCntCycle += offset_for_ref_frame[i];

            int PicOrderCntMsb = 0;
            int TopFieldOrderCnt = 0,
                BottomFieldOrderCnt = 0;

            if (pictureOrderCountType == 0)
            {
                int prevPicOrderCntMsb = 0;
                int prevPicOrderCntLsb = 0;

                if (!h264.DeriveIdrPicFlag())
                {
                    if (prevPic.Mmco == 5)
                    {
                        if (prevPic.Picture is not FieldPicture)
                        {
                            prevPicOrderCntLsb = prevPic.Poc.Top;
                        }
                        // Otherwise, both are explicitly initialized to zero,
                        // but we already do that here as the default value.
                    }
                    else
                    {
                        prevPicOrderCntMsb = prevPic.Poc.Msb;
                        prevPicOrderCntLsb = (int?)prevPic.Picture.State!.H264RbspState!.SliceHeader!.PicOrderCntLsb ?? 0;
                    }
                }

                int MaxPicOrderCntLsb = h264.DeriveMaxPicOrderCntLsb();

                if ((pic_order_cnt_lsb < prevPicOrderCntLsb) && ((prevPicOrderCntLsb - pic_order_cnt_lsb) >= (MaxPicOrderCntLsb / 2)))
                    PicOrderCntMsb = prevPicOrderCntMsb + MaxPicOrderCntLsb;
                else if ((pic_order_cnt_lsb > prevPicOrderCntLsb) && ((pic_order_cnt_lsb - prevPicOrderCntLsb) > (MaxPicOrderCntLsb / 2)))
                    PicOrderCntMsb = prevPicOrderCntMsb - MaxPicOrderCntLsb;
                else
                    PicOrderCntMsb = prevPicOrderCntMsb;

                TopFieldOrderCnt = PicOrderCntMsb + pic_order_cnt_lsb;

                if (!field_pic_flag)
                    BottomFieldOrderCnt = TopFieldOrderCnt + delta_pic_order_cnt_bottom;
                else
                    BottomFieldOrderCnt = PicOrderCntMsb + pic_order_cnt_lsb;
            }
            else if (pictureOrderCountType == 1)
            {
                int prevFrameNum = (int?)prevPic.Picture.State?.H264RbspState?.SliceHeader?.FrameNum ?? 0;
                int prevFrameNumOffset = prevPic.Mmco == 5 ? 0 : frameNumOffsetOfPrevFrameIfPocTypeIs1;

                int MaxFrameNum = h264.DeriveMaxFrameNum();

                int FrameNumOffset;
                if (h264.DeriveIdrPicFlag())
                    FrameNumOffset = 0;
                else if (prevFrameNum > frame_num)
                    FrameNumOffset = prevFrameNumOffset + MaxFrameNum;
                else
                    FrameNumOffset = prevFrameNumOffset;

                int absFrameNum;
                if (num_ref_frames_in_pic_order_cnt_cycle != 0)
                    absFrameNum = FrameNumOffset + frame_num;
                else
                    absFrameNum = 0;
                if (nal_ref_idc == 0 && absFrameNum > 0)
                    absFrameNum--;

                int picOrderCntCycleCnt = (absFrameNum - 1) / num_ref_frames_in_pic_order_cnt_cycle;
                int frameNumInPicOrderCntCycle = (absFrameNum - 1) % num_ref_frames_in_pic_order_cnt_cycle;

                int expectedPicOrderCnt;
                if (absFrameNum > 0)
                {
                    expectedPicOrderCnt = picOrderCntCycleCnt * ExpectedDeltaPerPicOrderCntCycle;
                    for (int i = 0; i <= frameNumInPicOrderCntCycle; i++)
                        expectedPicOrderCnt += offset_for_ref_frame[i];
                }
                else
                    expectedPicOrderCnt = 0;
                if (nal_ref_idc == 0)
                    expectedPicOrderCnt += offset_for_non_ref_pic;

                if (!field_pic_flag)
                {
                    TopFieldOrderCnt = expectedPicOrderCnt + delta_pic_order_cnt[0];
                    BottomFieldOrderCnt = TopFieldOrderCnt + offset_for_top_to_bottom_field + delta_pic_order_cnt[1];
                }
                else if (!bottom_field_flag)
                {
                    TopFieldOrderCnt = expectedPicOrderCnt + delta_pic_order_cnt[0];
                }
                else
                {
                    BottomFieldOrderCnt = expectedPicOrderCnt + offset_for_top_to_bottom_field + delta_pic_order_cnt[0];
                }
            }
            else if (pictureOrderCountType == 2)
            {
                int prevFrameNum = (int?)prevPic.Picture.State?.H264RbspState?.SliceHeader?.FrameNum ?? 0;
                int prevFrameNumOffset = prevPic.Mmco == 5 ? 0 : frameNumOffsetOfPrevFrameIfPocTypeIs1;

                int FrameNumOffset;
                if (h264.DeriveIdrPicFlag())
                    FrameNumOffset = 0;
                else if (prevFrameNum > frame_num)
                    FrameNumOffset = prevFrameNumOffset + h264.DeriveMaxFrameNum();
                else
                    FrameNumOffset = prevFrameNumOffset;

                int tempPicOrderCnt;
                if (h264.DeriveIdrPicFlag())
                    tempPicOrderCnt = 0;
                else if (nal_ref_idc == 0)
                    tempPicOrderCnt = 2 * (FrameNumOffset + frame_num) - 1;
                else
                    tempPicOrderCnt = 2 * (FrameNumOffset + frame_num);

                if (!field_pic_flag)
                {
                    TopFieldOrderCnt = tempPicOrderCnt;
                    BottomFieldOrderCnt = tempPicOrderCnt;
                }
                else if (bottom_field_flag)
                {
                    BottomFieldOrderCnt = tempPicOrderCnt;
                }
                else
                {
                    TopFieldOrderCnt = tempPicOrderCnt;
                }
            }

            return new H264PictureOrderCount(0, TopFieldOrderCnt, BottomFieldOrderCnt, PicOrderCntMsb);
        }
    
        public int PictureOrderCount(PictureDescriptor picX, PictureDescriptor prevPic, int frameNumOffsetOfPrevFrameIfPocTypeIs1)
        {
            H264PictureOrderCount poc = DerivePictureOrderCountsCore(picX.Picture.State!, prevPic, frameNumOffsetOfPrevFrameIfPocTypeIs1);

            if (picX.Picture is ComplementaryFieldPair)
            {
                return Math.Min(poc.Top, poc.Bottom);
            }
            else if (picX.Picture.IsTopField())
            {
                return picX.Poc.Top;
            }
            else if (picX.Picture.IsBottomField())
            {
                return picX.Poc.Bottom;
            }
            else
            {
                return 0;
            }
        }

        public int DiffPicOrderCnt(int pocA, int pocB) => pocA - pocB;

        public H264PictureOrderCount DerivePictureOrderCounts(PictureDescriptor currPic, PictureDescriptor prevPic, int frameNumOffsetOfPrevFrameIfPocTypeIs1)
        {
            var poc = DerivePictureOrderCountsCore(currPic.Picture.State!, prevPic, frameNumOffsetOfPrevFrameIfPocTypeIs1);
            var pocNum = PictureOrderCount(currPic, prevPic, frameNumOffsetOfPrevFrameIfPocTypeIs1);
            return new H264PictureOrderCount(pocNum, poc.Top, poc.Bottom, poc.Msb);
        }

        public void PopulateWithMapUnitToSliceGroupMap(H264State h264, IList<int> mapUnitToSliceGroupMap)
        {
            int MapUnitsInSliceGroup0 = h264.DeriveMapUnitsInSliceGroup0();
            int PicSizeInMapUnits = h264.DerivePicSizeInMapUnits();
            bool slice_group_change_direction_flag = h264.H264RbspState?.PictureParameterSet?.SliceGroupChangeDirectionFlag ?? false;
            int PicWidthInMbs = h264.DerivePicWidthInMbs();

            int sizeOfUpperLeftGroup = (slice_group_change_direction_flag ? (PicSizeInMapUnits - MapUnitsInSliceGroup0) : MapUnitsInSliceGroup0);

            int slice_group_map_type = (int?)h264.H264RbspState?.PictureParameterSet?.SliceGroupMapType ?? 0;
            int num_slice_groups_minus1 = (int?)h264.H264RbspState?.PictureParameterSet?.NumSliceGroupsMinus1 ?? 0;
            int[] run_length_minus1 = h264.H264RbspState?.PictureParameterSet?.RunLengthMinus1?.CastAsInt32() ?? [];
            int[] top_left = h264.H264RbspState?.PictureParameterSet?.TopLeft?.CastAsInt32() ?? [];
            int[] bottom_right = h264.H264RbspState?.PictureParameterSet?.BottomRight?.CastAsInt32() ?? [];
            int PicHeightInMapUnits = h264.H264RbspState?.PicHeightInMapUnits() ?? 0;

            if (slice_group_map_type == 0)
            {
                int i = 0;
                do
                    for (int iGroup = 0; iGroup <= num_slice_groups_minus1 && i < PicSizeInMapUnits; i += run_length_minus1[iGroup++] + 1)
                        for (int j = 0; j <= run_length_minus1[iGroup] && i + j < PicSizeInMapUnits; j++)
                            mapUnitToSliceGroupMap[i + j] = iGroup;
                while (i < PicSizeInMapUnits);
            }
            else if (slice_group_map_type == 1)
            {
                for (int i = 0; i < PicSizeInMapUnits; i++)
                    mapUnitToSliceGroupMap[i] = ((i % PicWidthInMbs) +
                                                 (((i / PicWidthInMbs) * (num_slice_groups_minus1 + 1)) / 2)) % (num_slice_groups_minus1 + 1);
            }
            else if (slice_group_map_type == 2)
            {
                for (int i = 0; i < PicSizeInMapUnits; i++)
                    mapUnitToSliceGroupMap[i] = num_slice_groups_minus1;

                for (int iGroup = num_slice_groups_minus1 - 1; iGroup >= 0; iGroup--)
                {
                    int yTopLeft = top_left[iGroup] / PicWidthInMbs;
                    int xTopLeft = top_left[iGroup] % PicWidthInMbs;
                    int yBottomRight = bottom_right[iGroup] / PicWidthInMbs;
                    int xBottomRight = bottom_right[iGroup] % PicWidthInMbs;
                    for (int y = yTopLeft; y <= yBottomRight; y++)
                        for (int x = xTopLeft; x <= xBottomRight; x++)
                            mapUnitToSliceGroupMap[y * PicWidthInMbs + x] = iGroup;
                }
            }
            else if (slice_group_map_type == 3)
            {
                for (int i = 0; i < PicSizeInMapUnits; i++)
                    mapUnitToSliceGroupMap[i] = 1;
                int x = (PicWidthInMbs - slice_group_change_direction_flag.AsInt32()) / 2;
                int y = (PicHeightInMapUnits - slice_group_change_direction_flag.AsInt32()) / 2;
                (int leftBound, int topBound) = (x, y);
                (int rightBound, int bottomBound) = (x, y);
                (int xDir, int yDir) = (slice_group_change_direction_flag.AsInt32() - 1, slice_group_change_direction_flag.AsInt32());
                int mapUnitVacant;
                for (int k = 0; k < MapUnitsInSliceGroup0; k += mapUnitVacant)
                {
                    mapUnitVacant = (mapUnitToSliceGroupMap[y * PicWidthInMbs + x] == 1).AsInt32();
                    if (mapUnitVacant.AsBoolean())
                        mapUnitToSliceGroupMap[y * PicWidthInMbs + x] = 0;
                    if (xDir == -1 && x == leftBound)
                    {
                        leftBound = Math.Max(leftBound - 1, 0);
                        x = leftBound;
                        (xDir, yDir) = (0, 2 * slice_group_change_direction_flag.AsInt32() - 1);
                    }
                    else if (xDir == 1 && x == rightBound)
                    {
                        rightBound = Math.Min(rightBound + 1, PicWidthInMbs - 1);
                        x = rightBound;
                        (xDir, yDir) = (0, 1 - 2 * slice_group_change_direction_flag.AsInt32());
                    }
                    else if (yDir == -1 && y == topBound)
                    {
                        topBound = Math.Max(topBound - 1, 0);
                        y = topBound;
                        (xDir, yDir) = (1 - 2 * slice_group_change_direction_flag.AsInt32(), 0);
                    }
                    else if (yDir == 1 && y == bottomBound)
                    {
                        bottomBound = Math.Min(bottomBound + 1, PicHeightInMapUnits - 1);
                        y = bottomBound;
                        (xDir, yDir) = (2 * slice_group_change_direction_flag.AsInt32() - 1, 0);
                    }
                    else
                        (x, y) = (x + xDir, y + yDir);
                }
            }
            else if (slice_group_map_type == 4)
            {
                for (int i = 0; i < PicSizeInMapUnits; i++)
                    if (i < sizeOfUpperLeftGroup)
                        mapUnitToSliceGroupMap[i] = slice_group_change_direction_flag.AsInt32();
                    else
                        mapUnitToSliceGroupMap[i] = 1 - slice_group_change_direction_flag.AsInt32();
            }
            else if (slice_group_map_type == 5)
            {
                int k = 0;
                for (int j = 0; j < PicWidthInMbs; j++)
                    for (int i = 0; i < PicHeightInMapUnits; i++)
                        if (k++ < sizeOfUpperLeftGroup)
                            mapUnitToSliceGroupMap[i * PicWidthInMbs + j] = slice_group_change_direction_flag.AsInt32();
                        else
                            mapUnitToSliceGroupMap[i * PicWidthInMbs + j] = 1 - slice_group_change_direction_flag.AsInt32();
            }
            else if (slice_group_map_type == 6)
            {
                uint[] slice_group_id = h264.H264RbspState?.PictureParameterSet?.SliceGroupId ?? [];
                int n = PicWidthInMbs * h264.DerivePicHeightInMbs();
                for (int i = 0; i < n; i++)
                {
                    mapUnitToSliceGroupMap[i] = (int)slice_group_id[i];
                }
            }
        }

        public void ConvertMapUnitToSliceGroupMapToMacroblockToSliceGroupMap(H264State h264, IList<int> mapUnitToSliceGroupMap, IList<int> macroblockToSliceGroupMap)
        {
            if ((h264.H264RbspState?.SequenceParameterSetData?.FrameMbsOnlyFlag ?? false) ||
                (h264.H264RbspState?.SliceHeader?.FieldPicFlag ?? false))
            {
                for (int i = 0; i < mapUnitToSliceGroupMap.Count; i++)
                    macroblockToSliceGroupMap.Add(mapUnitToSliceGroupMap[i]);
            }
            else if (h264.DeriveMbaffFrameFlag())
            {
                for (int i = 0; i < mapUnitToSliceGroupMap.Count; i++)
                    macroblockToSliceGroupMap.Add(mapUnitToSliceGroupMap[i / 2]);
            }
            else
            {
                int PicWidthInMbs = h264.DerivePicWidthInMbs();

                for (int i = 0; i < mapUnitToSliceGroupMap.Count; i++)
                    macroblockToSliceGroupMap.Add(mapUnitToSliceGroupMap[(i / (2 * PicWidthInMbs)) * PicWidthInMbs + (i % PicWidthInMbs)]);
            }
        }

        public int NextMbAddress(H264State h264, IList<int> mbToSliceGroupMap, int n)
        {
            int i = n + 1;
            while (i < h264.DerivePicSizeInMbs() && mbToSliceGroupMap[i] != mbToSliceGroupMap[n])
                i++;
            int nextMbAddress = i;
            return nextMbAddress;
        }
    }
}
