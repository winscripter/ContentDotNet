namespace ContentDotNet.Extensions.H264.Internal.Decoding;

internal static class SliceDataDecoder
{
    public static (int topFieldOrderCnt, int bottomFieldOrderCnt) DerivePictureOrderCount(
        int picOrderCntMsb,
        int picOrderCntLsb,
        bool isIDRPicture,
        int previousPicMemoryManagementControlOperation,
        int previousPicPrevPicOrderCntMsb,
        int previousPicPrevPicOrderCntLsb,
        int topFieldOrderCntForPreviousPic,
        int MaxPicOrderCntLsb,
        bool bottomField,
        bool topField,
        bool fieldPicFlag,
        int deltaPicOrderCntBottom)
    {
        int prevPicOrderCntMsb = 0;
        int prevPicOrderCntLsb = 0;

        if (!isIDRPicture)
        {
            if (previousPicMemoryManagementControlOperation == 5)
            {
                prevPicOrderCntLsb = topFieldOrderCntForPreviousPic;
            }
            else
            {
                prevPicOrderCntLsb = previousPicPrevPicOrderCntLsb;
                prevPicOrderCntMsb = previousPicPrevPicOrderCntMsb;
            }
        }

        if ((picOrderCntLsb < prevPicOrderCntLsb) &&
            ((prevPicOrderCntLsb - picOrderCntLsb) >= (MaxPicOrderCntLsb / 2)))
            picOrderCntMsb = prevPicOrderCntMsb + MaxPicOrderCntLsb;
        else if ((picOrderCntLsb > prevPicOrderCntLsb) &&
                ((picOrderCntLsb - prevPicOrderCntLsb) > (MaxPicOrderCntLsb / 2)))
            picOrderCntMsb = prevPicOrderCntMsb - MaxPicOrderCntLsb;
        else
            picOrderCntMsb = prevPicOrderCntMsb;

        int topFieldOrderCnt = 0;
        if (!bottomField)
        {
            topFieldOrderCnt = picOrderCntMsb + picOrderCntLsb;
        }

        int bottomFieldOrderCnt = 0;
        if (!topField)
        {
            if (!fieldPicFlag)
                bottomFieldOrderCnt = topFieldOrderCnt + deltaPicOrderCntBottom;
            else
                bottomFieldOrderCnt = picOrderCntMsb + picOrderCntLsb;
        }

        return (topFieldOrderCnt, bottomFieldOrderCnt);
    }
}
