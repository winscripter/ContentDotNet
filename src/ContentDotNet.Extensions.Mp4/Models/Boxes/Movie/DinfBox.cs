namespace ContentDotNet.Extensions.Mp4.Models.Boxes.Movie;

[BoxInfo("dinf", "Data Information Box", "Contains information about the data source for a movie track, such as the location of the media data.")]
public sealed class DinfBox
{
    private readonly MinfBox? _minf;
    
    public UrlBox? Url { get; set; }

}
