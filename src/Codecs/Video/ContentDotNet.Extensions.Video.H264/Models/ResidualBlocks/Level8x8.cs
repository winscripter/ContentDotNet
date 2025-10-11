namespace ContentDotNet.Extensions.Video.H264.Models.ResidualBlocks
{
    /// <summary>
    ///   level8x8 variable
    /// </summary>
    public class Level8x8 : BaseResidual2DArray
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="Level8x8"/> class.
        /// </summary>
        public Level8x8()
            : base(Create2DArray(16, 16))
        {
        }
    }
}
