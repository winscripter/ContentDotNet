namespace ContentDotNet.Security
{
    /// <summary>
    ///   Supports freeing jagged arrays.
    /// </summary>
    public static class JaggedArrayFree
    {
        public static void FreeJaggedArray<T>(
            this Configuration cfg,
            T[] source,
            int sizeOfEachElement)
        {
            cfg.MemoryAllocator.FreeMemory(source.GetLongLength(0) * sizeOfEachElement);
        }

        public static void FreeJaggedArray<T>(
           this Configuration cfg,
           T[][] source,
           int sizeOfEachElement)
        {
            cfg.MemoryAllocator.FreeMemory(source.GetLongLength(0) * source.GetLongLength(1) * sizeOfEachElement);
        }

        public static void FreeJaggedArray<T>(
           this Configuration cfg,
           T[][][] source,
           int sizeOfEachElement)
        {
            cfg.MemoryAllocator.FreeMemory(source.GetLongLength(0) * source.GetLongLength(1) * source.GetLongLength(2) * sizeOfEachElement);
        }

        public static void FreeJaggedArray<T>(
           this Configuration cfg,
           T[][][][] source,
           int sizeOfEachElement)
        {
            cfg.MemoryAllocator.FreeMemory(source.GetLongLength(0) * source.GetLongLength(1) * source.GetLongLength(2) * source.GetLongLength(3) * sizeOfEachElement);
        }

        public static void FreeJaggedArray<T>(
           this Configuration cfg,
           T[][][][][] source,
           int sizeOfEachElement)
        {
            cfg.MemoryAllocator.FreeMemory(source.GetLongLength(0) * source.GetLongLength(1) * source.GetLongLength(2) * source.GetLongLength(3) * source.GetLongLength(4) * sizeOfEachElement);
        }

        public static void FreeJaggedArray<T>(
           this Configuration cfg,
           T[][][][][][] source,
           int sizeOfEachElement)
        {
            cfg.MemoryAllocator.FreeMemory(source.GetLongLength(0) * source.GetLongLength(1) * source.GetLongLength(2) * source.GetLongLength(3) * source.GetLongLength(4) * source.GetLongLength(5) * sizeOfEachElement);
        }
    }
}
