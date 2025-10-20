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
    /// <param name="CbfIntra16x16DCLevel"></param>
    /// <param name="CbfIntra16x16ACLevel"></param>
    /// <param name="CbfLumaLevel4x4"></param>
    /// <param name="CbfLumaLevel8x8"></param>
    /// <param name="CbfChromaDCLevel"></param>
    /// <param name="CbfChromaACLevel"></param>
    /// <param name="CbfCbIntra16x16DCLevel"></param>
    /// <param name="CbfCbIntra16x16ACLevel"></param>
    /// <param name="CbfCbLevel4x4"></param>
    /// <param name="CbfCbLevel8x8"></param>
    /// <param name="CbfCrIntra16x16DCLevel"></param>
    /// <param name="CbfCrIntra16x16ACLevel"></param>
    /// <param name="CbfCrLevel4x4"></param>
    /// <param name="CbfCrLevel8x8"></param>
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

        // Coded Block Flags

        bool CbfIntra16x16DCLevel,
        List<bool> CbfIntra16x16ACLevel,
        List<bool> CbfLumaLevel4x4,
        List<bool> CbfLumaLevel8x8,

        List<bool>? CbfChromaDCLevel,
        List<List<bool>>? CbfChromaACLevel,

        bool CbfCbIntra16x16DCLevel,
        List<bool> CbfCbIntra16x16ACLevel,
        List<bool> CbfCbLevel4x4,
        List<bool> CbfCbLevel8x8,

        bool CbfCrIntra16x16DCLevel,
        List<bool> CbfCrIntra16x16ACLevel,
        List<bool> CbfCrLevel4x4,
        List<bool> CbfCrLevel8x8
    );
}
