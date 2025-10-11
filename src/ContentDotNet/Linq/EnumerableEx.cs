namespace ContentDotNet.Linq
{
    /// <summary>
    ///   Extended enumerables.
    /// </summary>
    public static class EnumerableEx
    {
        /// <summary>
        ///   Applies an IndexOf using the delegate.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable.</typeparam>
        /// <param name="source">The source enumerable.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>An index of the first item that satisfies the delegate <paramref name="selector"/>, or -1 if none found.</returns>
        public static int IndexOfBy<T>(
            this IEnumerable<T> source,
            Func<T, bool> selector)
        {
            int index = -1;
            foreach (T item in source)
            {
                if (selector(item))
                {
                    index++;
                    return index;
                }
            }
            return -1;
        }
    }
}
