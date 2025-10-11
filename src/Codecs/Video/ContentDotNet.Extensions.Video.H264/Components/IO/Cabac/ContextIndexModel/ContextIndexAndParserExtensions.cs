namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ContextIndexModel
{
    using ContentDotNet.Extensions.Video.H264.Enumerations;

    /// <summary>
    ///   Context index and parser extensions
    /// </summary>
    public static class ContextIndexAndParserExtensions
    {
        public static int GetPrefixOffset(
            this ContextIndexAndParser parser)
            => parser.Record.CtxIdxOffset is ContextIndexAffixValue affix
                ? affix.Prefix
                : ((ContextIndexIntegerValue)parser.Record.CtxIdxOffset).Value;

        public static int GetSuffixOffset(
            this ContextIndexAndParser parser)
            => parser.Record.CtxIdxOffset is ContextIndexAffixValue affix
                ? affix.Suffix
                : 0;

        public static int GetOffset(
            this ContextIndexAndParser parser,
            IH264CabacDecoder decoder)
            => decoder.Affix == H264Affix.Suffix ? parser.GetSuffixOffset() : parser.GetPrefixOffset();
    }
}
