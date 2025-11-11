namespace ContentDotNet.Image.Formats.Jpeg.Components
{
    internal ref struct JpegBufferReader
    {
        /// <summary>
        ///   Actual data
        /// </summary>
        public JpegDataBuffer Data;

        /// <summary>
        ///   Byte pointer
        /// </summary>
        public int BP;

        /// <summary>
        ///   Pointer to byte before start of entropy-coded segment
        /// </summary>
        public int BPST;

        public JpegBufferReader(JpegDataBuffer data, int bP, int bPST)
        {
            Data = data;
            BP = bP;
            BPST = bPST;
        }

        /// <summary>
        ///   Current byte
        /// </summary>
        public readonly byte B => Data.Data[BP];
    }
}
