namespace ContentDotNet.Extensions.Video.H264.Components.Common
{
    internal static class Clipping
    {
        public static int Clip(int x, int y, int z)
        {
            if (z < x)
            {
                return x;
            }
            else if (z > y)
            {
                return y;
            }
            return z;
        }

        public static int Clip1Y(int value, int BitDepthY)
        {
            return Clip(0, 1 << BitDepthY, value);
        }

        public static int Clip1C(int value, int BitDepthC)
        {
            return Clip(0, 1 << BitDepthC, value);
        }
    }
}
