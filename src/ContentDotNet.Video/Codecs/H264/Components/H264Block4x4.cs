namespace ContentDotNet.Video.Codecs.H264.Components
{
    using System.Runtime.Intrinsics;

    internal struct H264Block4x4
    {
        public H264Block4x4(Vector128<byte> vec)
        {
            Vector = vec;
        }

        public Vector128<byte> Vector;
    }
}
