namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ResidualBlocks.Dimensional
{
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ResidualBlocks;
    using ContentDotNet.Extensions.Video.H264.RbspModels;

    internal class ResidualBlock2D : ResidualBlockBase
    {
        private readonly List<List<int>> _coefficients;

        public ResidualBlock2D(RbspResidual rbspResidual, List<List<int>> coefficients)
        {
            RbspResidual = rbspResidual;
            _coefficients = coefficients;
        }

        public override ResidualBlockDimension Dimensions => ResidualBlockDimension.TwoD;

        public override List<int> GetAs1DList()
        {
            throw new InvalidOperationException("This residual block is 2D and cannot be represented as 1D.");
        }

        public override List<List<int>> GetAs2DList()
        {
            return _coefficients;
        }

        public override List<List<List<int>>> GetAs3DList()
        {
            throw new InvalidOperationException("This residual block is 2D and cannot be represented as 3D.");
        }
    }
}
