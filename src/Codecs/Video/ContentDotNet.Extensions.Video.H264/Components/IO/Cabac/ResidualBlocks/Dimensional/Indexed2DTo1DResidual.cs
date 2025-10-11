namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ResidualBlocks.Dimensional
{
    using ContentDotNet.Extensions.Video.H264.RbspModels;

    internal class Indexed2DTo1DResidualBlock : ResidualBlock1D
    {
        public Indexed2DTo1DResidualBlock(RbspResidual rbspResidual, List<List<int>> coefficients, int coeffIndex)
            : base(rbspResidual, coefficients[coeffIndex])
        {
        }
    }
}
