namespace ContentDotNet.Video.Formats.Mp4
{
    using ContentDotNet.Api.Primitives;

    public class StsdSampleEntry
    {
        public FourCC Type { get; set; }
        public byte[] Data { get; set; } = []; // full sample entry payload
        public override string ToString() => $"{Type} ({Data?.Length ?? 0} bytes)";
    }
}
