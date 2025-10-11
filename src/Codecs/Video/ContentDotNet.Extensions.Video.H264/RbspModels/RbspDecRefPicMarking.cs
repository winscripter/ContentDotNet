namespace ContentDotNet.Extensions.Video.H264.RbspModels
{
    public record RbspDecRefPicMarking(
        // For IdrPicFlag == true
        bool? NoOutputOfPriorPicsFlag,
        bool? LongTermReferenceFlag,

        // For IdrPicFlag == false
        bool? AdaptiveRefPicMarkingModeFlag,
        List<MemoryManagementControl>? MemoryManagementControls
    );

    public record MemoryManagementControl(
        uint MemoryManagementControlOperation,
        uint? DifferenceOfPicNumsMinus1,
        uint? LongTermPicNum,
        uint? LongTermFrameIdx,
        uint? MaxLongTermFrameIdxPlus1
    );
}
