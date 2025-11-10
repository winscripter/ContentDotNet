namespace ContentDotNet.Video.Codecs.H264.Components
{
    using ContentDotNet.Api.BitStream;
    using ContentDotNet.Api.Primitives;
    using ContentDotNet.Video.Shared.ItuT.RbspBuilder;

    public class H264Reader
    {
        private readonly BitStreamReader _bitStreamReader;

        public H264Reader(BitStreamReader reader) => _bitStreamReader = reader;

        public long ProcessNalLength() => H264NalHelpers.GetNalUnitLength(_bitStreamReader);
        public bool SkipToNalStart() => H264NalHelpers.SkipToStartOfNalUnit(_bitStreamReader);

        public IItuRbspBufferFactory RbspBuilderFactory { get; set; } = new CustomRbspBufferFactory(() =>
            new MemoryRbspBufferBuilder()
            {
                MaxSize = DataSize.Megabytes(2)
            });

        #region Structures

        #endregion
    }
}
