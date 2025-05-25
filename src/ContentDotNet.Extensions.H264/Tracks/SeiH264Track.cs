using ContentDotNet.BitStream;
using ContentDotNet.Extensions.H264.Models;
using ContentDotNet.Extensions.H26x;

namespace ContentDotNet.Extensions.H264.Tracks;

/// <summary>
///   An SEI H.264 track.
/// </summary>
public sealed class SeiH264Track : H264Track
{
    /// <summary>
    ///   Gets the SEI model associated with this SEI track.
    /// </summary>
    public ISeiModel Model { get; }

    /// <summary>
    ///   Parameter list for writing SEI models.
    /// </summary>
    public SeiModelParameterList? ParameterList { get; } = null;

    /// <inheritdoc />
    public SeiH264Track(BitStreamReader reader, NalUnit nalUnit, ISeiModel? model) : base(reader, nalUnit)
    {
        Model = model ?? throw new ArgumentNullException(nameof(model), "SEI model cannot be null.");
    }

    /// <inheritdoc />
    public override bool IsFrame => false;

    /// <inheritdoc />
    protected override void WriteData(BitStreamWriter writer)
    {
        this.Model.Write(writer, this.ParameterList);
    }

    /// <inheritdoc />
    protected override async Task WriteDataAsync(BitStreamWriter writer)
    {
        await this.Model.WriteAsync(writer, this.ParameterList);
    }
}
