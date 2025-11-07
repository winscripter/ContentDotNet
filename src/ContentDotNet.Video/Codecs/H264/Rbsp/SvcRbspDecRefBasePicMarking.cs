namespace ContentDotNet.Video.Codecs.H264.Rbsp
{
    using ContentDotNet.Video.Shared.ItuT.DescriptorAnnotations;

    public record SvcRbspMemoryManagementBaseControlOperation(
        [property: UeDescriptor] uint MemoryManagementBaseControlOperation,
        [property: UeDescriptor] uint? DifferenceOfBasePicNumsMinus1 = null,
        [property: UeDescriptor] uint? LongTermBasePicNum = null
    );

    public record SvcRbspDecRefBasePicMarking(
        [property: UDescriptor("1")] bool AdaptiveRefBasePicMarkingModeFlag,
        List<SvcRbspMemoryManagementBaseControlOperation>? MemoryManagementOperations = null
    );
}
