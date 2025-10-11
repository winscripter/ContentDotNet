namespace ContentDotNet.LoopExecutors
{
    /// <summary>
    ///   Abstract loop executor.
    /// </summary>
    public interface ILoopExecutor
    {
        /// <summary>
        ///   Performs a for loop in this loop executor.
        /// </summary>
        /// <param name="minInclusive">Minimum value (inclusive)</param>
        /// <param name="maxInclusive">Maximum value (exclusive)</param>
        /// <param name="body">Executor logic (with loop index)</param>
        void For(int minInclusive, int maxInclusive, Action<int> body);

        /// <summary>
        ///   Performs a foreach loop in this loop executor.
        /// </summary>
        /// <typeparam name="T">The type of the collection.</typeparam>
        /// <param name="source">The source enumerable (collection).</param>
        /// <param name="body">Executor logic (with the element).</param>
        void ForEach<T>(IEnumerable<T> source, Action<T> body);
    }
}
