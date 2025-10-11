namespace ContentDotNet.Extensions.Video.H264.Models.ResidualBlocks
{
    /// <summary>
    ///   i16x16ACLevel variable
    /// </summary>
    public class AcLevel16x16 : BaseResidual2DArray
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="AcLevel16x16"/> class.
        /// </summary>
        public AcLevel16x16()
            : base(Create2DArray(16, 16))
        {
        }
    }
}
