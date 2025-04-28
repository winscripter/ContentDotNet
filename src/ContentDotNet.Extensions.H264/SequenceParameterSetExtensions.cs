using ContentDotNet.Extensions.H264.Models;

namespace ContentDotNet.Extensions.H264;

/// <summary>
///   SPS extension methods.
/// </summary>
internal static class SequenceParameterSetExtensions
{
    internal static uint GetChromaArrayType(this SequenceParameterSet sps) => sps.SeparateColourPlaneFlag ? 0 : sps.ChromaFormatIdc;
}
