using ContentDotNet.Extensions.H264.Models;

namespace ContentDotNet.Extensions.H264.Minimal;

/// <summary>
///   Minimal luma residual
/// </summary>
public readonly struct MinimalLumaResidual
{
    /// <summary>
    ///   StartResidualCabac
    /// </summary>
    public readonly MinimalCabacResidual? StartResidualCabac;

    /// <summary>
    ///   Initializes a new instance of the <see cref="MinimalLumaResidual"/> structure.
    /// </summary>
    /// <param name="startResidualCabac"></param>
    public MinimalLumaResidual(MinimalCabacResidual? startResidualCabac)
    {
        StartResidualCabac = startResidualCabac;
    }

    /// <inheritdoc />
    public static MinimalLumaResidual From(ResidualLuma luma)
        => new(luma.StartResidualCabac is not null ? MinimalCabacResidual.From(luma.StartResidualCabac!.Value) : null);
}
