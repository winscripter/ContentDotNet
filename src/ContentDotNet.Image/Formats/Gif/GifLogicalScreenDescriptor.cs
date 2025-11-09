namespace ContentDotNet.Image.Formats.Gif
{
    public struct GifLogicalScreenDescriptor
    {
        public ushort Width, Height;
        public byte PackedFields, BackgroundColorIndex, PixelAspectRatio;

        public GifLogicalScreenDescriptor(ushort width, ushort height, byte packedFields, byte backgroundColorIndex, byte pixelAspectRatio)
        {
            Width = width;
            Height = height;
            PackedFields = packedFields;
            BackgroundColorIndex = backgroundColorIndex;
            PixelAspectRatio = pixelAspectRatio;
        }

        public static GifLogicalScreenDescriptor Read(BinaryReader reader)
        {
            return new(reader.ReadUInt16(), reader.ReadUInt16(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
        }

        public readonly void Write(BinaryWriter writer)
        {
            writer.Write(Width);
            writer.Write(Height);
            writer.Write(PackedFields);
            writer.Write(BackgroundColorIndex);
            writer.Write(PixelAspectRatio);
        }
    }
}
