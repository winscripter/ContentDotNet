namespace ContentDotNet.Video.Codecs.H264.Rbsp
{
    public record RbspRefPicListModification(
        // slice_type mod 5 != 2 && != 4
        bool? RefPicListModificationFlagL0,
        List<RefPicListModificationEntry>? RefPicListModificationsL0,

        // slice_type mod 5 == 1
        bool? RefPicListModificationFlagL1,
        List<RefPicListModificationEntry>? RefPicListModificationsL1
    );

    public record RefPicListModificationEntry(
        uint ModificationOfPicNumsIdc,
        uint? AbsDiffPicNumMinus1,  // present if ModificationOfPicNumsIdc == 0 or 1
        uint? LongTermPicNum        // present if ModificationOfPicNumsIdc == 2
    );
}
