namespace ContentDotNet.Abstractions
{
    /// <summary>
    ///   An interface that represents data that might not be
    ///   necessary but could still be used.
    /// </summary>
    public interface IMiscellaneousData
    {
        /// <summary>
        ///   The displayed category of miscellaneous data.
        /// </summary>
        string? DisplayName { get; }
    }
}
