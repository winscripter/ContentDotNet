using System.Numerics;

namespace ContentDotNet.Api.Colors;

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
}
