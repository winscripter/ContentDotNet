namespace ContentDotNet.Extensions.Video.H264.RbspModels
{
    using ContentDotNet.Shared.ItuT.DescriptorAnnotations;

    public record MvcRefPicListModificationEntry(
        [property: UeDescriptor] uint ModificationOfPicNumsIdc,
        [property: UeDescriptor] uint? AbsDiffPicNumMinus1 = null,
        [property: UeDescriptor] uint? LongTermPicNum = null,
        [property: UeDescriptor] uint? AbsDiffViewIdxMinus1 = null
    );

    public record MvcRbspRefPicListMvcModification(
        [property: UDescriptor("1")] bool? RefPicListModificationFlagL0,
        List<MvcRefPicListModificationEntry> RefPicListModificationL0Entries,

        [property: UDescriptor("1")] bool? RefPicListModificationFlagL1,
        List<MvcRefPicListModificationEntry> RefPicListModificationL1Entries
    );
}
