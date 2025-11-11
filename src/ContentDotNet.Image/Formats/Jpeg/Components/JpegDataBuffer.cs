namespace ContentDotNet.Image.Formats.Jpeg.Components
{
    // We believe we can use a ref struct implementation because a single
    // marker can hold up to 65533 bytes.

    internal ref struct JpegDataBuffer
    {
        public ReadOnlySpan<byte> Data;

        public JpegDataBuffer(ReadOnlySpan<byte> data)
        {
            Data = data;
        }
    }
}
