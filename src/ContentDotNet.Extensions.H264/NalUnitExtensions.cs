using ContentDotNet.Extensions.H264.Models;

namespace ContentDotNet.Extensions.H264;

/// <summary>
///   Provides extensions to ease working with NAL units.
/// </summary>
public static class NalUnitExtensions
{
    public static bool IsIdr(this NalUnit nal) => nal.NalUnitType == 6;
}
