namespace ContentDotNet.Extensions.H264.Internal.Decoding;

internal record struct PocContext
{
    public int PicOrderCntLsb { get; set; }
    public int PicOrderCntMsb { get; set; }
    public int PrevPicOrderCntLsb { get; set; }
    public int PrevPicOrderCntMsb { get; set; }

    public PocContext(int picOrderCntLsb, int picOrderCntMsb, int prevPicOrderCntLsb, int prevPicOrderCntMsb)
    {
        PicOrderCntLsb = picOrderCntLsb;
        PicOrderCntMsb = picOrderCntMsb;
        PrevPicOrderCntLsb = prevPicOrderCntLsb;
        PrevPicOrderCntMsb = prevPicOrderCntMsb;
    }
}
