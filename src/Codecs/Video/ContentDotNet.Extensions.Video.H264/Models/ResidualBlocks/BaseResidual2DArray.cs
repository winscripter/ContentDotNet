namespace ContentDotNet.Extensions.Video.H264.Models.ResidualBlocks
{
    /// <summary>
    ///   Abstracts a residual 2D array.
    /// </summary>
    public abstract class BaseResidual2DArray
    {
        private readonly List<List<int>> array2D;

        /// <summary>
        ///   Initializes a new instance of the <see cref="BaseResidual2DArray"/> class.
        /// </summary>
        /// <param name="array2D">The 2D array</param>
        protected BaseResidual2DArray(List<List<int>> array2D)
        {
            this.array2D = array2D;
        }

        /// <summary>
        ///   Creates a 2D array.
        /// </summary>
        /// <param name="len0">Top length</param>
        /// <param name="len1">Inner length</param>
        /// <returns>The 2D array</returns>
        protected static List<List<int>> Create2DArray(int len0, int len1)
        {
            var list = new List<List<int>>();
            for (int i = 0; i < len0; i++)
            {
                var row = new List<int>();
                for (int j = 0; j < len1; j++)
                    row.Add(i);
                list.Add(row);
            }

            return list;
        }

        /// <summary>
        ///   The raw value of the residual.
        /// </summary>
        public List<List<int>> Value => this.array2D;
    }
}
