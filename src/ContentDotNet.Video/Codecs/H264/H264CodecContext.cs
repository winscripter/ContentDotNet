namespace ContentDotNet.Video.Codecs.H264
{
    using ContentDotNet.Video.Codecs.H264.Components;
    using ContentDotNet.Video.Codecs.H264.Rbsp;
    using System.Drawing;

    /// <summary>
    ///   H.264 Codec context
    /// </summary>
    public class H264CodecContext
    {
        /// <summary>
        ///   The NAL unit.
        /// </summary>
        public RbspNalUnit? NalUnit { get; set; }

        /// <summary>
        ///   The SPS
        /// </summary>
        public RbspSequenceParameterSetData? Sps { get; set; }

        /// <summary>
        ///   The PPS
        /// </summary>
        public RbspPictureParameterSet? Pps { get; set; }

        /// <summary>
        ///   The slice header.
        /// </summary>
        public RbspSliceHeader? SliceHeader { get; set; }

        /// <summary>
        ///   The macroblock utility
        /// </summary>
        public IMacroblockUtility? MacroblockUtility { get; set; }

        /// <summary>
        ///   Active H.264 macroblock.
        /// </summary>
        public H264Macroblock? CurrentMacroblock { get; set; }

        /// <summary>
        ///   Current macroblock address.
        /// </summary>
        public int CurrMbAddr { get; set; }

        /// <summary>
        ///   The macroblock index of the MB being decoded.
        /// </summary>
        public Point MacroblockIndex { get; set; }

        public H264SliceType SliceType => H264SliceTypes.

        /// <summary>
        ///   Non-nullable macroblock utility
        /// </summary>
        public IMacroblockUtility Utility => MacroblockUtility ?? throw new InvalidOperationException("Missing macroblock utility");
    }
}
