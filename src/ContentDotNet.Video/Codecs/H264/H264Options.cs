namespace ContentDotNet.Video.Codecs.H264
{
    /// <summary>
    ///   Shared options between the decoder and the encoder.
    /// </summary>
    public class H264Options
    {
        /// <summary>
        ///   When true, parallelism is used.
        /// </summary>
        public bool Parallelize { get; set; }

        /// <summary>
        ///   Options for parallelism.
        /// </summary>
        public ParallelOptions? ParallelOptions { get; set; }
    }
}
