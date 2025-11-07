namespace ContentDotNet.Api.Security
{
    /// <summary>
    ///   Options that might help mitigate memory leaks and Denial of Service (DoS).
    /// </summary>
    public record struct AntiDenialOfServiceOptions
    {
        /// <summary>
        ///   Maximum bytes that can be requested per allocation.
        /// </summary>
        public int MaximumRequestBytesPerAllocation;

        /// <summary>
        ///   Maximum memory allocations.
        /// </summary>
        public int MaximumAllocations;

        /// <summary>
        ///   Maximum bytes that can be allocated.
        /// </summary>
        public int MaximumAllocateBytes;

        /// <summary>
        ///   When <see langword="true"/>, use <see cref="MaximumAllocateBytes"/>. Otherwise,
        ///   use <see cref="MaximumRequestBytesPerAllocation"/> and <see cref="MaximumAllocations"/>.
        /// </summary>
        public bool UseMaximumAllocateBytes;
    }
}
