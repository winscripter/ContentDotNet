using System.Numerics;

namespace ContentDotNet.Colors;

/// <summary>
///   Represents a color.
/// </summary>
public interface IColor
{
    /// <summary>
    ///   Packs the color to <see cref="uint"/>.
    /// </summary>
    /// <returns>Packed color representation.</returns>
    uint Pack();

    /// <summary>
    ///   Packs the color to <see cref="ulong"/>.
    /// </summary>
    /// <returns>Packed color representation.</returns>
    ulong LongPack();

    static abstract IColor FromVector4(Vector4 v4);
    static abstract IColor FromVector3(Vector3 v3);
}
