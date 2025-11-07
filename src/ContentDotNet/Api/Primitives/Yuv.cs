namespace ContentDotNet.Api.Primitives;

/// <summary>
///   A read-only YUV color representation.
/// </summary>
/// <param name="Y">Y</param>
/// <param name="U">U</param>
/// <param name="V">V</param>
public record struct Yuv(byte Y, byte U, byte V);
