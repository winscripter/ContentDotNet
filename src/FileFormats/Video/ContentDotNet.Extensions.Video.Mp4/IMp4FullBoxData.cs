namespace ContentDotNet.Extensions.Video.Mp4
{
    /// <summary>
    ///   The full MP4 box data.
    /// </summary>
    public interface IMp4FullBoxData : IMp4BoxData
    {
        /// <summary>
        ///   The version.
        /// </summary>
        byte? Version { get; set; }

        /// <summary>
        ///   The flags.
        /// </summary>
        uint? Flags { get; set; }
    }
}
