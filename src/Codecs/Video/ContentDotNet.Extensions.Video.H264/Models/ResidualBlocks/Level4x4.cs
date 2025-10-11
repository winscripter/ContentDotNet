namespace ContentDotNet.Extensions.Video.H264.Models.ResidualBlocks
{
    /// <summary>
    ///   level4x4 variable
    /// </summary>
    public class Level4x4 : BaseResidual2DArray
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="Level4x4"/> class.
        /// </summary>
        public Level4x4()
            : base(Create2DArray(16, 16))
        {
        }
    }
}
