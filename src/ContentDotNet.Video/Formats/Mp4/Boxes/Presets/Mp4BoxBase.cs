namespace ContentDotNet.Video.Formats.Mp4.Boxes.Presets
{
    using ContentDotNet.Api.Primitives;

    public class Mp4BoxBase
    {
        public long Size { get; set; }
        public FourCC Type { get; set; }

        protected void WriteBase(BinaryWriter writer)
        {
            if (Size > int.MaxValue)
            {
                writer.Write(1);
                writer.Write(Type.Value);
                writer.Write(Size);
            }
            else
            {
                writer.Write((int)Size);
                writer.Write(Type.Value);
            }
        }
    }
}
