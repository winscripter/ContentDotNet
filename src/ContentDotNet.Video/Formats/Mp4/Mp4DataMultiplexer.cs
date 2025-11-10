namespace ContentDotNet.Video.Formats.Mp4
{
    using ContentDotNet.Video.Formats.Mp4.Boxes;

    /// <summary>
    ///   Abstracts multiplexing MP4 data
    /// </summary>
    public abstract class Mp4DataMultiplexer
    {
        /// <summary>
        ///   Creates the MP4 track.
        /// </summary>
        /// <returns>The track.</returns>
        public abstract TrackBox CreateTrack();
    }
}
