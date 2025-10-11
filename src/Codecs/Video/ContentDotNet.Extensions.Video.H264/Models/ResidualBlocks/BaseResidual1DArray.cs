namespace ContentDotNet.Extensions.Video.H264.Models.ResidualBlocks
{
    /// <summary>
    ///   Abstracts a residual 2D array.
    /// </summary>
    public abstract class BaseResidual1DArray
    {
        private readonly List<int> array1D;

        /// <summary>
        ///   Initializes a new instance of the <see cref="BaseResidual1DArray"/> class.
        /// </summary>
        /// <param name="array1D">The 1D array</param>
        protected BaseResidual1DArray(List<int> array1D)
        {
            this.array1D = array1D;
        }

        /// <summary>
        ///   Creates a 1D array.
        /// </summary>
        /// <param name="len0">Top length</param>
        /// <returns>The 1D array</returns>
        protected static List<int> Create1DArray(int len0)
        {
            var list = new List<int>();
            for (int i = 0; i < len0; i++)
            {
                list.Add(0);
            }

            return list;
        }

        /// <summary>
        ///   The raw value of the residual.
        /// </summary>
        public List<int> Value => this.array1D;
    }
}
