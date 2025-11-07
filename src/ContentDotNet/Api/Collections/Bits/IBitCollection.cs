namespace ContentDotNet.Api.Collections.Bits
{
    /// <summary>
    ///   Abstract collection for bits.
    /// </summary>
    public interface IBitCollection : ICollection<bool>
    {
        /// <summary>
        ///   The backing buffer.
        /// </summary>
        IList<uint> Buffer { get; }
    }
}
