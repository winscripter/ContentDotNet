namespace ContentDotNet.Video.Codecs.H264.Components
{
    using ContentDotNet.Utilities;

    /// <summary>
    ///   Motion Vector Differences for H.264.
    /// </summary>
    public class H264Mvd
    {
        /// <summary>
        ///   Private MVD data
        /// </summary>
        private int[][][] _mvdData;

        /// <summary>
        ///   Initializes a new instance of the <see cref="H264Mvd"/> class
        /// </summary>
        /// <param name="numberOfPartitions"></param>
        /// <param name="subMacroblockPartitions"></param>
        public H264Mvd(int numberOfPartitions, int subMacroblockPartitions)
        {
            _mvdData = JaggedArrayFactory.Create3D<int>(numberOfPartitions, subMacroblockPartitions, 2);
        }

        /// <summary>
        ///   Raw motion vector difference data
        /// </summary>
        public int[][][] RawMvdData
        {
            get => _mvdData;
            set => _mvdData = value;
        }
    }
}
