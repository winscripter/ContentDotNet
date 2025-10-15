namespace ContentDotNet.Extensions.Image.Jpeg.Processing.Arithmetic
{
    internal struct NextIndices
    {
        public int Qe, Lps, Mps;
        public bool SwitchMps;

        public NextIndices(int qe, int lps, int mps, bool switchMps)
        {
            Qe = qe;
            Lps = lps;
            Mps = mps;
            SwitchMps = switchMps;
        }
    }
}
