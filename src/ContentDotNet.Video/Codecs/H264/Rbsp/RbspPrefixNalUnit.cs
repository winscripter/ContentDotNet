namespace ContentDotNet.Video.Codecs.H264.Rbsp
{
    public record RbspPrefixNalUnit(
        bool SvcExtensionFlag,
        SvcRbspPrefixNalUnitSvc? PrefixNalUnitSvc // Present if SvcExtensionFlag == true
    );
}
