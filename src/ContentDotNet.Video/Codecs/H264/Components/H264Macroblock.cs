namespace ContentDotNet.Video.Codecs.H264.Components
{
    using ContentDotNet.Video.Codecs.H264.Rbsp;

    /// <summary>
    ///   The H.264 macroblock.
    /// </summary>
    public record H264Macroblock
    {
        /// <summary>
        ///   The macroblock layer.
        /// </summary>
        public RbspMacroblockLayer MacroblockLayer { get; set; }

        /// <summary>
        ///   Is MBAFF enabled?
        /// </summary>
        public bool MbaffEnabled { get; set; }

        /// <summary>
        ///   mb_field_decoding_flag
        /// </summary>
        public bool MbFieldDecodingFlag { get; set; }

        /// <summary>
        ///   Is this macroblock skipped?
        /// </summary>
        public bool Skipped { get; set; }

        /// <summary>
        ///   Macroblock CurrMbAddr.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        ///   The H.264 slice type.
        /// </summary>
        public H264SliceType SliceType { get; set; }

        /// <summary>
        ///   Frame macroblock?
        /// </summary>
        public bool IsFrame => MbaffEnabled && !MbFieldDecodingFlag;

        /// <summary>
        ///   Field macroblock?
        /// </summary>
        public bool IsField => MbaffEnabled && MbFieldDecodingFlag;

        public H264Macroblock(RbspMacroblockLayer macroblockLayer, bool mbaffEnabled, bool mbFieldDecodingFlag, bool skipped, int index, H264SliceType sliceType)
        {
            MacroblockLayer = macroblockLayer;
            MbaffEnabled = mbaffEnabled;
            MbFieldDecodingFlag = mbFieldDecodingFlag;
            Skipped = skipped;
            Index = index;
            SliceType = sliceType;
        }

        public static bool operator ==(H264Macroblock mb, H264MacroblockType mbType)
        {
            return mbType.Inferred ? mb.Skipped : (mb.MacroblockLayer.MbType == mbType.MacroblockTypeNumber && mb.SliceType == mbType.SliceType);
        }

        public static bool operator !=(H264Macroblock mb, H264MacroblockType mbType) => !(mb == mbType);
    }
}
