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

    internal class JpegArithmeticEncoderRegisters
    {
        private readonly Stream stream;

        public JpegArithmeticEncoderRegisters(Stream stream) => this.stream = stream;

        public Stream Stream => stream;

        public int A;
        public int C;
        public int CT;
        public int T;
        public long BPST;
        public long BP;
        public int ST;
    }

    internal class JpegArithmeticDecoderRegisters
    {
        private readonly Stream stream;

        public JpegArithmeticDecoderRegisters(Stream stream) => this.stream = stream;

        public Stream Stream => stream;

        public int A;
        public int C;
        public int CT;
        public int Cx;
        public int T;
        public long BPST;
        public long BP;
        public int ST;
    }
}
