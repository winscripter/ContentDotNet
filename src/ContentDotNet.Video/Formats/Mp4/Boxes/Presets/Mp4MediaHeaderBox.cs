namespace ContentDotNet.Video.Formats.Mp4.Boxes.Presets
{
    /// <summary>
    ///   Marker class that indicates that derived classes are media header boxes, such as <c>vmhd</c> or <c>smhd</c>.
    /// </summary>
    public abstract class Mp4MediaHeaderBox : Mp4BoxBase
    {
        public abstract void Write(BinaryWriter writer);
    }
}
