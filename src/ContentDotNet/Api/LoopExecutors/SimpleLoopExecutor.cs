namespace ContentDotNet.Api.LoopExecutors
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///   A very simple loop executor that uses for loops. Does not consume much CPU usage.
    /// </summary>
    public class SimpleLoopExecutor : ILoopExecutor
    {
        /// <summary>
        ///   Instance simple loop executor.
        /// </summary>
        public static readonly SimpleLoopExecutor Instance = new();

        /// <inheritdoc cref="ILoopExecutor.For(int, int, Action{int})" />
        public void For(int minInclusive, int maxInclusive, Action<int> body)
        {
            for (int i = minInclusive; i < maxInclusive; i++)
            {
                body(i);
            }
        }

        /// <inheritdoc cref="ILoopExecutor.ForEach{T}(IEnumerable{T}, Action{T})" />
        public void ForEach<T>(IEnumerable<T> source, Action<T> body)
        {
            foreach (T item in source)
            {
                body(item);
            }
        }
    }
}
