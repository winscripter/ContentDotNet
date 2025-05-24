namespace ContentDotNet.Extensions.Mp4.Models.Boxes.Movie;

[BoxInfo("dref", "Data Reference Box", "Contains information about the data source for a movie track, such as the location of the media data.")]
public sealed class DrefBox : IBoxData
{
    public uint Size { get; set; }
    public uint Type { get; set; }
}
