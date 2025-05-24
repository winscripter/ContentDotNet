namespace ContentDotNet.Extensions.Mp4.Models.Boxes;

/// <summary>
///   A single MP4 box.
/// </summary>
public abstract class Box
{
    /// <summary>
    ///   Size of the box.
    /// </summary>
    public uint Size { get; set; }

    /// <summary>
    ///   Type of the box (a fourCC).
    /// </summary>
    public uint Type { get; set; }

    public static (uint size, uint type) Parse(BinaryReader reader)
    {
        return (reader.ReadUInt32(), reader.ReadUInt32());
    }
}
