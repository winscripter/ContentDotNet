namespace ContentDotNet.Video.Codecs.H264.Components
{
    internal struct H264PB8Mode
    {
        public int B8mode, B8pdir;

        public H264PB8Mode(int b8mode, int b8pdir)
        {
            B8mode = b8mode;
            B8pdir = b8pdir;
        }
    }
}
