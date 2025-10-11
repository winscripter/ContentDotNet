namespace ContentDotNet.Extensions.Video.H264.RbspModels
{
    public record RbspPredWeightTable(
        uint LumaLog2WeightDenom,
        uint? ChromaLog2WeightDenom,
        List<PredWeightL0Entry> L0,
        List<PredWeightL1Entry>? L1 // nullable, only if slice_type % 5 == 1
    );

    public record PredWeightL0Entry(
        bool LumaWeightL0Flag,
        int? LumaWeightL0,
        int? LumaOffsetL0,
        bool? ChromaWeightL0Flag,
        int[]? ChromaWeightL0,
        int[]? ChromaOffsetL0
    );

    public record PredWeightL1Entry(
        bool LumaWeightL1Flag,
        int? LumaWeightL1,
        int? LumaOffsetL1,
        bool? ChromaWeightL1Flag,
        int[]? ChromaWeightL1,
        int[]? ChromaOffsetL1
    );
}
