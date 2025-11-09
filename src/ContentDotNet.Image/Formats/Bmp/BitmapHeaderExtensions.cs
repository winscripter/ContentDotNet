namespace ContentDotNet.Image.Formats.Bmp
{
    public static class BitmapHeaderExtensions
    {
        public static int Width(this IBitmapHeader header) =>
            header switch
            {
                BitmapCoreHeader c => c.BcWidth,
                BitmapInfoHeader i => (int)i.BiWidth,
                BitmapV4Header v4 => (int)v4.BV4Width,
                BitmapV5Header v5 => (int)v5.BV5Width,
                _ => throw new InvalidOperationException()
            };

        public static int Height(this IBitmapHeader header) =>
            header switch
            {
                BitmapCoreHeader c => c.BcHeight,
                BitmapInfoHeader i => (int)i.BiHeight,
                BitmapV4Header v4 => (int)v4.BV4Height,
                BitmapV5Header v5 => (int)v5.BV5Height,
                _ => throw new InvalidOperationException()
            };

        public static int BitsPerPixel(this IBitmapHeader header) =>
            header switch
            {
                BitmapCoreHeader c => c.BcBitCount,
                BitmapInfoHeader i => i.BiBitCount,
                BitmapV4Header v4 => v4.BV4BitCount,
                BitmapV5Header v5 => v5.BV5BitCount,
                _ => throw new InvalidOperationException()
            };

        public static int ColorCount(this IBitmapHeader header)
        {
            int bpp = header.BitsPerPixel();
            int clrUsed = header switch
            {
                BitmapInfoHeader i => i.BiClrUsed,
                BitmapV4Header v4 => v4.BV4ClrUsed,
                BitmapV5Header v5 => v5.BV5ClrUsed,
                BitmapCoreHeader => 0,
                _ => 0
            };

            return clrUsed > 0 ? (int)clrUsed : (1 << bpp);
        }

        public static bool IsRleCompressed(this IBitmapHeader h) =>
            h switch
            {
                BitmapInfoHeader i => i.BiCompression == 1 || i.BiCompression == 2,
                BitmapV4Header v4 => v4.BV4V4Compression == 1 || v4.BV4V4Compression == 2,
                BitmapV5Header v5 => v5.BV5Compression == 1 || v5.BV5Compression == 2,
                _ => false
            };
    }
}
