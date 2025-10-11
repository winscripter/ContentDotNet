namespace ContentDotNet.LoopExecutors
{
    /// <summary>
    ///   A parallel loop executor that makes use of multiple CPU threads. Can consume lots of CPU.
    /// </summary>
    public class ParallelLoopExecutor : ILoopExecutor
    {
        /// <summary>
        ///   Instance parallel loop executor.
        /// </summary>
        public static readonly ParallelLoopExecutor Instance = new();

        /// <inheritdoc cref="ILoopExecutor.For(int, int, Action{int})" />
        public void For(int minInclusive, int maxInclusive, Action<int> body)
        {
            Parallel.For(minInclusive, maxInclusive, body);
        }

        /// <inheritdoc cref="ILoopExecutor.ForEach{T}(IEnumerable{T}, Action{T})" />
        public void ForEach<T>(IEnumerable<T> source, Action<T> body)
        {
            Parallel.ForEach(source, body);
        }
    }
}
