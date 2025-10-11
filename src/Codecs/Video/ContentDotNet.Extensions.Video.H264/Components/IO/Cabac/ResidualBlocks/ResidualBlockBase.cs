namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ResidualBlocks
{
    using ContentDotNet.Extensions.Video.H264.RbspModels;

    internal abstract class ResidualBlockBase
    {
        public RbspResidual? RbspResidual { get; set; }

        public abstract List<int> GetAs1DList();
        public abstract List<List<int>> GetAs2DList();
        public abstract List<List<List<int>>> GetAs3DList();

        public abstract ResidualBlockDimension Dimensions { get; }

        public bool CodedBlockFlag => RbspResidual?.CodedBlockFlag ?? throw new InvalidOperationException("No RbspResidual assigned.");
    }
}
