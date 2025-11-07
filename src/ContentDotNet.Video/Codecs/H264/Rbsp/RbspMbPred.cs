namespace ContentDotNet.Video.Codecs.H264.Rbsp
{
    public record RbspMbPred(
        // For Intra_4x4 mode (16 blocks)
        List<bool>? PrevIntra4x4PredModeFlag,
        List<uint>? RemIntra4x4PredMode,

        // For Intra_8x8 mode (4 blocks)
        List<bool>? PrevIntra8x8PredModeFlag,
        List<uint>? RemIntra8x8PredMode,

        // For Intra_8x8 or Intra_16x16 with chroma
        uint? IntraChromaPredMode,  // ue(v) | ae(v)

        // For Inter prediction parts
        List<uint>? RefIdxL0,       // te(v) | ae(v)
        List<uint>? RefIdxL1,       // te(v) | ae(v)

        // Motion vector differences for L0 and L1: dimensions [NumMbPart][1][2]
        List<List<List<int>>>? MvdL0,  // se(v) | ae(v)
        List<List<List<int>>>? MvdL1   // se(v) | ae(v)
    );
}
