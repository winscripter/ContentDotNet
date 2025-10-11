namespace ContentDotNet.Extensions.Video.H264.RbspModels
{
    public record RbspMacroblockLayer
    {
        public uint MbType { get; set; }

        // For MbType == I_PCM
        public List<byte>? PcmSampleLuma { get; set; }
        public List<byte>? PcmSampleChroma { get; set; }

        // For other MbTypes
        public RbspSubMbPred? SubMbPred { get; set; }

        // Conditional flags
        public bool TransformSize8x8Flag { get; set; }
        public RbspMbPred? MbPred { get; set; }
        public int CodedBlockPattern { get; set; }

        // QP delta and residuals
        public int MbQpDelta { get; set; }
        public RbspResidual? Residual { get; set; }

        public RbspMacroblockLayer(uint mbType, List<byte>? pcmSampleLuma, List<byte>? pcmSampleChroma, RbspSubMbPred? subMbPred, bool transformSize8x8Flag, RbspMbPred? mbPred, int codedBlockPattern, int mbQpDelta, RbspResidual? residual)
        {
            MbType = mbType;
            PcmSampleLuma = pcmSampleLuma;
            PcmSampleChroma = pcmSampleChroma;
            SubMbPred = subMbPred;
            TransformSize8x8Flag = transformSize8x8Flag;
            MbPred = mbPred;
            CodedBlockPattern = codedBlockPattern;
            MbQpDelta = mbQpDelta;
            Residual = residual;
        }

        public RbspMacroblockLayer()
            : this(0, null, null, null, false, null, 0, 0, null)
        {
        }
    }
}
