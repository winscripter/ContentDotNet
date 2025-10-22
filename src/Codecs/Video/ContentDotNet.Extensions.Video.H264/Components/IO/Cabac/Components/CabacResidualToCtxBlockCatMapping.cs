namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.Components
{
    using ContentDotNet.Extensions.Video.H264.Enumerations;

    internal static class CabacResidualToCtxBlockCatMapping
    {
        public static int GetCtxBlockCat(ResidualBlockType blockType)
        {
            return blockType switch
            {
                ResidualBlockType.Intra16x16DCLevel => 0,
                ResidualBlockType.Intra16x16ACLevel => 1,
                ResidualBlockType.LumaLevel4x4 => 2,
                ResidualBlockType.ChromaDCLevel => 3,
                ResidualBlockType.ChromaACLevel => 4,
                ResidualBlockType.LumaLevel8x8 => 5,
                ResidualBlockType.Cb16x16DCLevel => 6,
                ResidualBlockType.Cb16x16ACLevel => 7,
                ResidualBlockType.CbLevel4x4 => 8,
                ResidualBlockType.CbLevel8x8 => 9,
                ResidualBlockType.Cr16x16DCLevel => 10,
                ResidualBlockType.Cr16x16ACLevel => 11,
                ResidualBlockType.CrLevel4x4 => 12,
                ResidualBlockType.CrLevel8x8 => 13,
                _ => throw new NotImplementedException($"ResidualBlockType {blockType} is not implemented."),
            };
        }
    }
}
