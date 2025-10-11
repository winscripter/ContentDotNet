namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ResidualBlocks.Dimensional
{
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ResidualBlocks;
    using ContentDotNet.Extensions.Video.H264.RbspModels;

    internal class ResidualBlock1D : ResidualBlockBase
    {
        private readonly List<int> _coefficients;

        public ResidualBlock1D(RbspResidual rbspResidual, List<int> coefficients)
        {
            RbspResidual = rbspResidual;
            _coefficients = coefficients;
        }

        public override ResidualBlockDimension Dimensions => ResidualBlockDimension.OneD;

        public override List<int> GetAs1DList()
        {
            return _coefficients;
        }

        public override List<List<int>> GetAs2DList()
        {
            throw new InvalidOperationException("This residual block is 1D and cannot be represented as 2D.");
        }

        public override List<List<List<int>>> GetAs3DList()
        {
            throw new InvalidOperationException("This residual block is 1D and cannot be represented as 3D.");
        }
    }
}
