namespace ContentDotNet.Abstractions
{
    /// <summary>
    ///   Describes naming properties for codecs.
    /// </summary>
    public interface ICodecWithNames
    {
        /// <summary>
        ///   An actual, abbreviated name (f.e. H264) using only alphabetical symbols, digits, and underscores.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///   A name displayed to the user, e.g. H.264.
        /// </summary>
        string DisplayName { get; }
    }
}
