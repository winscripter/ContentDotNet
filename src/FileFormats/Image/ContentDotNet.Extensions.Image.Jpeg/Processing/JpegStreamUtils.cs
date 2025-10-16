namespace ContentDotNet.Extensions.Image.Jpeg.Processing
{
    internal static class JpegStreamUtils
    {
        public static int GetCurrentByte(this Stream stream)
        {
            stream.Position--;
            return stream.ReadByte();
        }
    }
}
