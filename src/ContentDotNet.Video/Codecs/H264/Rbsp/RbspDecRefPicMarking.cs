namespace ContentDotNet.Video.Codecs.H264.Rbsp
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
