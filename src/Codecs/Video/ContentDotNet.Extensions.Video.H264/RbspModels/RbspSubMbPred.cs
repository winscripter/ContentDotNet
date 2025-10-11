namespace ContentDotNet.Extensions.Video.H264.RbspModels
{
    public record RbspSubMbPred(
        List<uint> SubMbType, // length = 4, ue(v) | ae(v)

        // Conditional lists of reference indices for L0 and L1, length 4
        List<uint>? RefIdxL0,
        List<uint>? RefIdxL1,

        // Motion vector differences for L0 and L1: 3D lists
        // Dimensions: [4][NumSubMbPart(sub_mb_type[i])][2]
        List<List<List<int>>>? MvdL0,
        List<List<List<int>>>? MvdL1
    );
}
