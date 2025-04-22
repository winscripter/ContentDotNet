namespace ContentDotNet.Extensions.Mp4.Models.Boxes;

/// <summary>
///   A single MP4 box.
/// </summary>
public readonly struct Box
{
    /// <summary>
    ///   Box header.
    /// </summary>
    public readonly BoxHeader Header;

    /// <summary>
    ///   Data of the MP4 box.
    /// </summary>
    public readonly IBoxData Data;

    public Box(BoxHeader header, IBoxData data)
    {
        Header = header;
        Data = data;
    }
}
