namespace ContentDotNet.Video.Codecs.H264
{
    /// <summary>
    ///   Options for the H.264 decoder.
    /// </summary>
    public class H264DecoderOptions
    {
        /// <summary>
        ///   The maximum number of SPS's in memory. Default value: 256.
        /// </summary>
        public int MaximumNumberOfSequenceParameterSets { get; set; } = 256;

        /// <summary>
        ///   The maximum number of PPS's in memory. Default value: 256.
        /// </summary>
        public int MaximumNumberOfPictureParameterSets { get; set; } = 256;

        /// <summary>
        ///   The maximum number of reference pictures that is allowed. This setting
        ///   overrides the maximum number of allowed reference pictures specified
        ///   in the SPS. In order to prevent enforcing a limit and only through the SPS values, consider using
        ///   a value -1. Default value: 16.
        /// </summary>
        public int MaximumNumberOfReferencePictures { get; set; } = 16;
    }
}
