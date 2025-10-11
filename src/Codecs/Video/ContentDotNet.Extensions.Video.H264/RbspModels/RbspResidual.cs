namespace ContentDotNet.Extensions.Video.H264.RbspModels
{
    /// <summary>
    ///   RBSP luma residual.
    /// </summary>
    /// <param name="Intra16x16DCLevel"></param>
    /// <param name="Intra16x16ACLevel"></param>
    /// <param name="LumaLevel4x4"></param>
    /// <param name="LumaLevel8x8"></param>
    /// <param name="ChromaDCLevel"></param>
    /// <param name="ChromaACLevel"></param>
    /// <param name="CbIntra16x16DCLevel"></param>
    /// <param name="CbIntra16x16ACLevel"></param>
    /// <param name="CbLevel4x4"></param>
    /// <param name="CbLevel8x8"></param>
    /// <param name="CrIntra16x16DCLevel"></param>
    /// <param name="CrIntra16x16ACLevel"></param>
    /// <param name="CrLevel4x4"></param>
    /// <param name="CrLevel8x8"></param>
    /// <param name="CodedBlockFlag">This is specific to CABAC.</param>
    public record RbspResidual(
        List<int> Intra16x16DCLevel,
        List<List<int>> Intra16x16ACLevel,
        List<List<int>> LumaLevel4x4,
        List<List<int>> LumaLevel8x8,

        List<List<int>>? ChromaDCLevel,
        List<List<List<int>>>? ChromaACLevel,

        List<int> CbIntra16x16DCLevel,
        List<List<int>> CbIntra16x16ACLevel,
        List<List<int>> CbLevel4x4,
        List<List<int>> CbLevel8x8,

        List<int> CrIntra16x16DCLevel,
        List<List<int>> CrIntra16x16ACLevel,
        List<List<int>> CrLevel4x4,
        List<List<int>> CrLevel8x8,

        bool CodedBlockFlag // <-- CABAC only. Necessary.
    );
}
