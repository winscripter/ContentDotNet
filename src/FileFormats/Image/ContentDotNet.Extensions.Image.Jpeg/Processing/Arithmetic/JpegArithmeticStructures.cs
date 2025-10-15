namespace ContentDotNet.Extensions.Image.Jpeg.Processing.Arithmetic
{
    // By reference because if by value then:
    //    some_array[x].Qe = 123;
    // won't work.
    internal class JpegArithmeticContextVariable
    {
        public int MPS { get; set; }
        public int Qe { get; set; }
        public int Index { get; set; }
    }

    internal class JpegArithmeticRegisters
    {
        private readonly Stream stream;

        public JpegArithmeticRegisters(Stream stream) => this.stream = stream;

        public int A;
        public int C;
        public int CT;
        public int T;
        public int B;
        public int BPST;
        public int BP;
        public int ST;
    }
}
