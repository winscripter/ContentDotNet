namespace ContentDotNet.Video.Codecs.H264.Models
{
    /// <summary>
    ///   H.264 RBSP element.
    /// </summary>
    public interface IRbspElement
    {
        /// <summary>
        ///   ITU-T Function signature
        /// </summary>
        string ItuFunctionSignature { get; }
    }

    /// <summary>
    ///   H.264 NAL unit extension
    /// </summary>
    public interface INalUnitHeaderExtension : IRbspElement
    {
    }
}
