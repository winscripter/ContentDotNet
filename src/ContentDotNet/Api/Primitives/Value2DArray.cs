namespace ContentDotNet.Api.Primitives
{
    using System.Runtime.CompilerServices;

    /// <summary>
    ///   Stack-only 2D array.
    /// </summary>
    public readonly ref struct Value2DArray
    {
        /// <summary>
        ///   The actual array data.
        /// </summary>
        private readonly Span<int> v2d; // Means Value 2D Array

        /// <summary>
        ///   The width of the array.
        /// </summary>
        private readonly int width;

        /// <summary>
        ///   The inner width of array.
        /// </summary>
        private readonly int width2;

        /// <summary>
        ///   Initializes a new instance of the <see cref="Value2DArray"/> structure.
        /// </summary>
        /// <param name="span">The backing span.</param>
        /// <param name="width">Width</param>
        /// <param name="width2">Inner width</param>
        /// <remarks>
        ///   Using widths and inner widths too large can result in stack buffer overruns.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Value2DArray(Span<int> span, int width, int width2)
        {
            v2d = span;
            this.width = width;
            this.width2 = width2;
        }

        /// <summary>
        ///   Actual array.
        /// </summary>
        public Span<int> Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => v2d;
        }

        /// <summary>
        ///   The outer width.
        /// </summary>
        public int Width
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => width;
        }

        /// <summary>
        ///   The inner width.
        /// </summary>
        public int InnerWidth
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => width2;
        }

        /// <summary>
        ///   Returns the width by dimension. 0 indicates first dimension, 1 indicates inner dimension.
        /// </summary>
        /// <param name="dimension">The dimension.</param>
        /// <returns>The width.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetWidth(int dimension)
        {
            if (dimension == 0)
            {
                return width;
            }
            else if (dimension == 1)
            {
                return width2;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        ///   Indexes this 2D array.
        /// </summary>
        /// <param name="x">Outer width</param>
        /// <param name="y">Inner width</param>
        /// <returns>The element at <paramref name="x"/>/<paramref name="y"/>.</returns>
        public int this[int x, int y]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => v2d[x * width + y];

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => v2d[x * width + y] = value;
        }

        /// <summary>
        ///   Returns the number of elements to allocate for the backing span of the value 2D array.
        /// </summary>
        /// <param name="width">The outer width</param>
        /// <param name="innerWidth">The inner width</param>
        /// <returns>The number of elements to allocate for the backing span of the value 2D array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetNumberOfElements(int width, int innerWidth) => width * innerWidth;
    }
}
