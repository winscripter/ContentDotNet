namespace ContentDotNet.Primitives
{
    /// <summary>
    ///   Empty structure.
    /// </summary>
    public readonly struct Empty
    {
        /// <summary>
        ///   Singleton instance.
        /// </summary>
        public static readonly Empty Value = new();

        /// <summary>
        ///   Alternative singleton instance.
        /// </summary>
        public static readonly Empty v = new();
    }
}
