namespace ContentDotNet.Api.Collections.Bits
{
    using System.Collections;

    /// <summary>
    ///   Enumerator for bit collections.
    /// </summary>
    public class BitCollectionEnumerator : IEnumerator<bool>
    {
        private readonly IBitCollection source;
        private int uint32Ptr = 0;
        private int bitPtr = 0;
        private int totalBitsRead = 0;

        /// <summary>
        ///   Initializes a new instance of the <see cref="BitCollectionEnumerator"/> class.
        /// </summary>
        /// <param name="source">The source bit collection.</param>
        public BitCollectionEnumerator(IBitCollection source)
        {
            this.source = source;
        }

        /// <summary>
        ///   Current object.
        /// </summary>
        public bool Current => ((source.Buffer[uint32Ptr] >> bitPtr) & 1) != 0; // Buffer returns 'this.source' in BitCollection

        /// <summary>
        ///   Current object.
        /// </summary>
        object IEnumerator.Current => Current;

        /// <summary>
        ///   Disposes of the current enumerator.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///   Moves to the next value.
        /// </summary>
        /// <returns>The next value.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool MoveNext()
        {
            if (totalBitsRead >= source.Count)
                return false;

            if (++bitPtr == 32)
            {
                bitPtr = 0;
                uint32Ptr++;
            }

            totalBitsRead++;
            return true;
        }

        /// <summary>
        ///   Resets the enumerator.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Reset()
        {
            uint32Ptr = 0;
            bitPtr = 0;
        }
    }
}
