namespace ContentDotNet.Extensions.Video.H264.Models
{
    /// <summary>
    ///   Motion Vector Difference (MVD) representation.
    /// </summary>
    public class H264MotionVectorDifference
    {
        private readonly List<List<List<int>>> mvdRaw;

        /// <summary>
        ///   Initializes a new instance of the <see cref="H264MotionVectorDifference"/> class.
        /// </summary>
        public H264MotionVectorDifference()
        {
            mvdRaw = [];

            for (int i = 0; i < 4; i++)
            {
                var row = new List<List<int>>();
                for (int j = 0; j < 4; j++)
                {
                    row.Add([0, 0]);
                }
                mvdRaw.Add(row);
            }
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="H264MotionVectorDifference"/> class.
        /// </summary>
        /// <param name="mvdRaw">Raw MVD</param>
        public H264MotionVectorDifference(List<List<List<int>>> mvdRaw)
        {
            this.mvdRaw = mvdRaw;
        }

        /// <summary>
        ///   Accessor for motion vector differences.
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="component">Component</param>
        /// <returns>The MVD value.</returns>
        public int this[int x, int y, int component]
        {
            get => mvdRaw[x][y][component];
            set => mvdRaw[x][y][component] = value;
        }

        /// <summary>
        ///   Raw value.
        /// </summary>
        public List<List<List<int>>> Raw => mvdRaw;
    }
}
