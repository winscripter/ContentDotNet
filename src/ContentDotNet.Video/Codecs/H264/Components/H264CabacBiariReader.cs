namespace ContentDotNet.Video.Codecs.H264.Components
{
    using ContentDotNet.Api.BitStream;

    /// <summary>
    ///   Binary arithmetic (BiAri) reader for H.264.
    /// </summary>
    public class H264CabacBiariReader
    {
        private int codIRange, codIOffset;
        private BitStreamReader _bitStream;

        public H264CabacBiariReader(int codIRange, int codIOffset, BitStreamReader bitStream)
        {
            this.codIRange = codIRange;
            this.codIOffset = codIOffset;
            _bitStream = bitStream;
        }

        public H264CabacBiariReader(int codIOffset, BitStreamReader bitStream)
            : this(510, codIOffset, bitStream)
        {
        }

        public H264CabacBiariReader(BitStreamReader bitStream)
            : this((int)bitStream.ReadBits(9), bitStream)
        {
        }

        public int Range
        {
            get => codIRange;
            set => codIRange = value;
        }

        public int Offset
        {
            get => codIOffset;
            set => codIOffset = value;
        }

        public BitStreamReader BitStream
        {
            get => _bitStream;
        }

        public bool DecodeDecision(ref int pStateIdx, ref bool valMPS) { throw new NotImplementedException(); }
        public bool DecodeBypass(ref int pStateIdx, ref bool valMPS) { throw new NotImplementedException(); }
        public bool DecodeTerminate(ref int pStateIdx, ref bool valMPS) { throw new NotImplementedException(); }
    }
}
