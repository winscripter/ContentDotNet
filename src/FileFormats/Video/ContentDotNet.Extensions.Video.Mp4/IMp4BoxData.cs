namespace ContentDotNet.Extensions.Video.Mp4
{
    /// <summary>
    ///   Abstracts actual MP4 box data.
    /// </summary>
    public interface IMp4BoxData
    {
        /// <summary>
        ///   An MP4 box that owns this box (if any).
        /// </summary>
        Mp4Box? Parent { get; set; }
    }
}
