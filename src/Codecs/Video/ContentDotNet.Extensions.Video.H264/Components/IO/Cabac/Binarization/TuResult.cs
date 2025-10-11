namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.Binarization
{
    internal record struct TuResult
    {
        public int Value;
        public int BinsRead;

        public TuResult(int value, int binsRead)
        {
            Value = value;
            BinsRead = binsRead;
        }
    }
}
