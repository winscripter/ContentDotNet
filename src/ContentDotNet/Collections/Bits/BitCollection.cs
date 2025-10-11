namespace ContentDotNet.Collections.Bits
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    ///   Defines a bit collection that allows defining bit arrays to loop through.
    /// </summary>
    public class BitCollection : IBitCollection
    {
        private readonly List<uint> storage;
        private int nBits;
        private int pointer;
        private uint currBuffer;
        internal bool isReadonly = false;

        /// <summary>
        ///   Initializes a new instance of the <see cref="BitCollection"/> class.
        /// </summary>
        public BitCollection()
        {
            this.storage = [];
            this.nBits = 0;
            this.pointer = 31; // Force initial write
        }

        public int Count => nBits;

        public bool IsReadOnly => isReadonly;

        /// <summary>
        ///   The backing buffer.
        /// </summary>
        public IList<uint> Buffer => this.storage;

        /// <summary>
        ///   Adds a single bit to this bit collection.
        /// </summary>
        /// <param name="item">The bit to add to the collection.</param>
        /// <exception cref="NotSupportedException"></exception>
        public void Add(bool item)
        {
            ThrowIfReadOnly();

            currBuffer <<= 1;
            currBuffer |= (uint)item.AsInt32();
            pointer++;

            if (pointer == 32)
            {
                storage.Add(currBuffer);
                currBuffer = 0;
                pointer = 0;
            }

            nBits++;
        }

        /// <summary>
        ///   Clears all bits from this bit collection.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Clear()
        {
            ThrowIfReadOnly();

            this.storage.Clear();
            this.nBits = 0;
            this.pointer = 0;
            this.currBuffer = 0;
        }

        /// <summary>
        ///   Checks if the specified bit is in this bit collection.
        /// </summary>
        /// <param name="item">The bit to check for presence.</param>
        /// <returns>A boolean indicating if <paramref name="item"/> is a valid bit in this collection.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Contains(bool item)
        {
            for (int i = 0; i < pointer; i++)
            {
                if (IsBitSet(currBuffer, i) == item)
                    return true;
            }

            foreach (uint obj in storage)
            {
                for (int i = 0; i < 32; i++)
                {
                    if (IsBitSet(obj, i) == item)
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        ///   Copies the bit collection to <paramref name="array"/>.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">The array index.</param>
        public void CopyTo(bool[] array, int arrayIndex)
        {
            int requiredSize = storage.Count * 32 + pointer;
            if (array.Length - arrayIndex < requiredSize)
                throw new InvalidOperationException("Array buffer is too small");

            int bitIndex = 0;
            foreach (uint block in storage)
            {
                for (int i = 0; i < 32; i++)
                {
                    array[arrayIndex + bitIndex++] = ((block >> i) & 1) == 1;
                }
            }

            for (int i = 0; i < pointer; i++)
            {
                array[arrayIndex + bitIndex++] = ((currBuffer >> i) & 1) == 1;
            }
        }

        /// <summary>
        ///   Returns the enumerator for this bit collection.
        /// </summary>
        /// <returns>The enumerator for this bit collection.</returns>
        public IEnumerator<bool> GetEnumerator()
        {
            return new BitCollectionEnumerator(this);
        }

        /// <summary>
        ///   Removes the first bit seen.
        /// </summary>
        /// <param name="item">The item</param>
        /// <returns></returns>
        public bool Remove(bool item)
        {
            ThrowIfReadOnly();

            throw new NotImplementedException();
        }

        /// <summary>
        ///   Returns the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void ThrowIfReadOnly()
        {
            if (IsReadOnly)
            {
                throw new InvalidOperationException("This collection is read-only and cannot be modified.");
            }
        }

        private static bool IsBitSet(uint value, int position)
        {
            return ((value >> position) & 1) == 1;
        }
    }
}
