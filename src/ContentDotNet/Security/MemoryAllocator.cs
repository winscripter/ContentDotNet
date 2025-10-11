namespace ContentDotNet.Security
{
    /// <summary>
    ///   Represents a memory allocator.
    /// </summary>
    public interface IMemoryAllocator
    {
        /// <summary>
        ///   Requests <paramref name="bytes"/> of memory.
        /// </summary>
        /// <param name="bytes">Number of bytes to request.</param>
        void RequestMemory(long bytes);

        /// <summary>
        ///   Free <paramref name="bytes"/> of memory.
        /// </summary>
        /// <param name="bytes">Number of bytes to free.</param>
        void FreeMemory(long bytes);

        /// <summary>
        ///   Number of blocks yet allocated.
        /// </summary>
        int Blocks { get; }

        /// <summary>
        ///   Total memory usage.
        /// </summary>
        long TotalMemoryUsage { get; }

        /// <summary>
        ///   The configuration.
        /// </summary>
        Configuration Configuration { get; }

        /// <summary>
        ///   Allocates an array.
        /// </summary>
        /// <typeparam name="T">Type of the array.</typeparam>
        /// <param name="length">Array's length</param>
        /// <param name="sizeOfEachElement">Size of each element of the array</param>
        /// <returns>The allocated array</returns>
        T[] AllocateArray<T>(int length, int sizeOfEachElement);

        /// <summary>
        ///   Allocates a 2D array.
        /// </summary>
        /// <typeparam name="T">Type of the array.</typeparam>
        /// <param name="length">Array's width</param>
        /// <param name="length2">Array's height</param>
        /// <param name="sizeOfEachElement">Size of each element of the array</param>
        /// <returns>The allocated array</returns>
        T[,] Allocate2DArray<T>(int length, int length2, int sizeOfEachElement);

        /// <summary>
        ///   Disposes of the array.
        /// </summary>
        /// <typeparam name="T">Type of the array</typeparam>
        /// <param name="array">Source array</param>
        /// <param name="sizeOfEachElement">Size of each element of the array</param>
        void DisposeArray<T>(T[] array, int sizeOfEachElement);

        /// <summary>
        ///   Disposes of the 2D array.
        /// </summary>
        /// <typeparam name="T">Type of the array</typeparam>
        /// <param name="array">Source array</param>
        /// <param name="sizeOfEachElement">Size of each element of the array</param>
        void Dispose2DArray<T>(T[,] array, int sizeOfEachElement);
    }

    internal class MemoryAllocator : IMemoryAllocator
    {
        public int Blocks { get; set; }
        public long TotalMemoryUsage { get; set; }
        public Configuration Configuration { get; }

        public MemoryAllocator(Configuration config) => Configuration = config;

        public T[,] Allocate2DArray<T>(int length, int length2, int sizeOfEachElement)
        {
            RequestMemory(length * length2 * sizeOfEachElement);
            return new T[length, length2];
        }

        public T[] AllocateArray<T>(int length, int sizeOfEachElement)
        {
            RequestMemory(length * sizeOfEachElement);
            return new T[length];
        }

        public void FreeMemory(long bytes)
        {
            Blocks--;
            if (Blocks < 0) Blocks = 0;
            TotalMemoryUsage -= bytes;
            if (TotalMemoryUsage < 0) TotalMemoryUsage = 0;
        }

        public void RequestMemory(long bytes)
        {
            if (!Configuration.ProcessingSecurity.DoSOptions.UseMaximumAllocateBytes &&
                Configuration.ProcessingSecurity.DoSOptions.MaximumRequestBytesPerAllocation < bytes)
                throw new OutOfMemoryException("Too large memory block being allocated");

            if (Configuration.ProcessingSecurity.DoSOptions.UseMaximumAllocateBytes)
            {
                if (this.TotalMemoryUsage + bytes > Configuration.ProcessingSecurity.DoSOptions.MaximumAllocateBytes)
                    throw new OutOfMemoryException("Too much memory allocated");
            }
            else
            {
                if (this.Blocks + 1 > Configuration.ProcessingSecurity.DoSOptions.MaximumAllocations)
                    throw new OutOfMemoryException("Too much memory allocated");
            }

            Blocks++;
            TotalMemoryUsage += bytes;
        }

        public void DisposeArray<T>(T[] array, int sizeOfEachElement)
        {
            FreeMemory(array.LongLength * sizeOfEachElement);
        }

        public void Dispose2DArray<T>(T[,] array, int sizeOfEachElement)
        {
            FreeMemory(array.GetLongLength(0) * array.GetLongLength(1) * sizeOfEachElement);
        }
    }
}
