namespace ContentDotNet.Video.Codecs.H264.Rbsp
{
    using ContentDotNet.Video.Shared.ItuT.DescriptorAnnotations;

    public record SvcRbspSeqParameterSetSvcExtension(
        [property: UDescriptor("1")] bool InterLayerDeblockingFilterControlPresentFlag,
        [property: UDescriptor("2")] uint ExtendedSpatialScalabilityIdc,

        // Conditional on ChromaArrayType == 1 or 2
        [property: UDescriptor("1")] bool ChromaPhaseXPlus1Flag,

        // Conditional on ChromaArrayType == 1
        [property: UDescriptor("2")] uint ChromaPhaseYPlus1,

        // Conditional on ExtendedSpatialScalabilityIdc == 1 and ChromaArrayType > 0
        [property: UDescriptor("1")] bool SeqRefLayerChromaPhaseXPlus1Flag,
        [property: UDescriptor("2")] uint SeqRefLayerChromaPhaseYPlus1,

        // Conditional on ExtendedSpatialScalabilityIdc == 1
        [property: SeDescriptor] int SeqScaledRefLayerLeftOffset,
        [property: SeDescriptor] int SeqScaledRefLayerTopOffset,
        [property: SeDescriptor] int SeqScaledRefLayerRightOffset,
        [property: SeDescriptor] int SeqScaledRefLayerBottomOffset,

        [property: UDescriptor("1")] bool SeqTcoeffLevelPredictionFlag,

        // Conditional on SeqTcoeffLevelPredictionFlag == true
        [property: UDescriptor("1")] bool AdaptiveTcoeffLevelPredictionFlag,

        [property: UDescriptor("1")] bool SliceHeaderRestrictionFlag
    );
}
