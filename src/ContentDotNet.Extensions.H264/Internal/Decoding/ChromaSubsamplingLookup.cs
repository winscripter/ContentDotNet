using ContentDotNet.Extensions.H264.Models;

namespace ContentDotNet.Extensions.H264.Internal.Decoding;

/// <summary>
/// The Chroma Subsampling lookup returns chroma subsampling &amp; Sub[Width/Height]C
/// based on the SPS separate_colour_plane_flag and chroma_format_idc.
/// </summary>
internal static class ChromaSubsamplingLookup
{
    // See Rec. ITU-T H.264 (V15) (2024/08): Page 47 out of 854
    public static readonly Dictionary<(uint chromaFormatIdc, bool separateColourPlaneFlag), ChromaSubsamplingAndSize> Lookup = new()
    {
        { (0, false), new ChromaSubsamplingAndSize(ChromaSubsampling.Shared400, 0, 0) },
        { (1, false), new ChromaSubsamplingAndSize(ChromaSubsampling.Shared420, 2, 2) },
        { (2, false), new ChromaSubsamplingAndSize(ChromaSubsampling.Shared422, 2, 1) },
        { (3, false), new ChromaSubsamplingAndSize(ChromaSubsampling.Shared444, 1, 1) },
        { (3, false), new ChromaSubsamplingAndSize(ChromaSubsampling.Shared444, 0, 0) },
    };

    public static ChromaSubsamplingAndSize GetSubsamplingAndSize(uint chromaFormatIdc, bool separateColourPlaneFlag) =>
        Lookup[(chromaFormatIdc, separateColourPlaneFlag)];

    public static ChromaSubsamplingAndSize GetSubsamplingAndSize(SequenceParameterSet sps) =>
        Lookup[(sps.ChromaFormatIdc, sps.SeparateColourPlaneFlag)];
}
