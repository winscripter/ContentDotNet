namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ResidualBlocks.Dimensional
{
    using ContentDotNet.Extensions.Video.H264.RbspModels;

    internal class Indexed3DTo2DResidualBlock : ResidualBlock2D
    {
        public Indexed3DTo2DResidualBlock(RbspResidual rbspResidual, List<List<List<int>>> coefficients, int coeffIndex)
            : base(rbspResidual, coefficients[coeffIndex])
        {
        }
    }
}
