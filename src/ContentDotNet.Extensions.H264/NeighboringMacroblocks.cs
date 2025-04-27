namespace ContentDotNet.Extensions.H264;

public struct NeighboringMacroblocks
{
    public int MbAddrA;
    public int MbAddrB;
    public int MbAddrC;
    public int MbAddrD;
    public bool IsMbAddrAAvailable;
    public bool IsMbAddrBAvailable;
    public bool IsMbAddrCAvailable;
    public bool IsMbAddrDAvailable;

    public NeighboringMacroblocks(int mbAddrA, int mbAddrB, int mbAddrC, int mbAddrD, bool isMbAddrAAvailable, bool isMbAddrBAvailable, bool isMbAddrCAvailable, bool isMbAddrDAvailable)
    {
        MbAddrA = mbAddrA;
        MbAddrB = mbAddrB;
        MbAddrC = mbAddrC;
        MbAddrD = mbAddrD;
        IsMbAddrAAvailable = isMbAddrAAvailable;
        IsMbAddrBAvailable = isMbAddrBAvailable;
        IsMbAddrCAvailable = isMbAddrCAvailable;
        IsMbAddrDAvailable = isMbAddrDAvailable;
    }
}
