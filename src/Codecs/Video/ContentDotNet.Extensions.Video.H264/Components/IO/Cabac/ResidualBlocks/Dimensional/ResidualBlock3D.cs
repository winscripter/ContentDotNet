namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ResidualBlocks.Dimensional
{
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ResidualBlocks;
    using ContentDotNet.Extensions.Video.H264.RbspModels;

    internal class ResidualBlock3D : ResidualBlockBase
    {
        private readonly List<List<List<int>>> _coefficients;

        public ResidualBlock3D(RbspResidual rbspResidual, List<List<List<int>>> coefficients)
        {
            RbspResidual = rbspResidual;
            _coefficients = coefficients;
        }

        public override ResidualBlockDimension Dimensions => ResidualBlockDimension.ThreeD;

        public override List<List<List<int>>> GetAs3DList()
        {
            return _coefficients;
        }

        public override List<List<int>> GetAs2DList()
        {
            throw new InvalidOperationException("This residual block is 3D and cannot be represented as 2D.");
        }

        public override List<int> GetAs1DList()
        {
            throw new InvalidOperationException("This residual block is 3D and cannot be represented as 1D.");
        }
    }
}
