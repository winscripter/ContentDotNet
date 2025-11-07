namespace ContentDotNet.Video.Codecs.H264.Components
{
    internal static class H264Functions
    {
        public static bool Avail(H264Macroblock? mb) => mb != null;
        public static bool Frame(H264Macroblock? mb) => Avail(mb) && !mb!.MbFieldDecodingFlag && mb.MbaffEnabled;
        public static bool Avail2(IMacroblockUtility mbUtil, H264AddressAndAvailability address, out H264Macroblock? mb)
        {
            if (address.Availability)
            {
                mb = mbUtil.GetMacroblockOrNull(address.Address);
            }
            else
            {
                mb = null;
            }
            return mb != null;
        }
        public static int Pred_LX(H264ListType listType) => listType == H264ListType.L0 ? H264PredictionModes.Pred_L0 : H264PredictionModes.Pred_L1;
    }
}
