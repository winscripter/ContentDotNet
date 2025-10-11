namespace ContentDotNet.Extensions.Video.H264.Models.ResidualBlocks
{
    /// <summary>
    ///   i16x16DCLevel variable
    /// </summary>
    public class DcLevel16x16 : BaseResidual1DArray
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="DcLevel16x16"/> class.
        /// </summary>
        public DcLevel16x16()
            : base(Create1DArray(16))
        {
        }
    }
}
