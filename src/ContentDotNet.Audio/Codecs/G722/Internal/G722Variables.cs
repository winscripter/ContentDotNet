namespace ContentDotNet.Audio.Codecs.G722.Internal
{
    // Don't care that it's public fields. It's internal anyway. :)

    #pragma warning disable CS0649

    internal struct G722Variables
    {
        public int xin;
        public double xL, xH;
        public double SLp, SHp;
        public double aLi, aHi;
        public double aL1, aL2, aH1, aH2;
        public double rL, rLt, rH;
        public double bLi, bHi;
        public double dL, dLt, dH;
        public double SLz, SHz;
        public double SL, SH;
        public double eL, eH;
        public double triangleUpL, triangleUpH;
        public double triangleDownL, triangleDownH;
        public double IL, ILt, ILH;
        public double PLr, PH;
        public double ILr;
        public int xout;
    }
}
