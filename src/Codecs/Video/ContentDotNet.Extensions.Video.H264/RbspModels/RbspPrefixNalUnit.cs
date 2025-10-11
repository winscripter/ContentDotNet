namespace ContentDotNet.Extensions.Video.H264.RbspModels
{
    public record RbspPrefixNalUnit(
        bool SvcExtensionFlag,
        SvcRbspPrefixNalUnitSvc? PrefixNalUnitSvc // Present if SvcExtensionFlag == true
    );
}
