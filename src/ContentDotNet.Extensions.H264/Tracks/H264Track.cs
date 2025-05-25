using ContentDotNet.BitStream;
using ContentDotNet.Extensions.H264.Models;

namespace ContentDotNet.Extensions.H264.Tracks;

/// <summary>
///   A single H.264 track in a video file.
/// </summary>
public abstract class H264Track
{
    /// <summary>
    ///   Reader used to read this track's data.
    /// </summary>
    public BitStreamReader Reader { get; }

    /// <summary>
    ///   Gets the NAL unit for this track.
    /// </summary>
    public NalUnit NalUnit { get; }

    /// <summary>
    ///   Does this track represent a frame?
    /// </summary>
    public abstract bool IsFrame { get; }

    /// <summary>
    ///   Initializes a new instance of the <see cref="H264Track"/> class.
    /// </summary>
    /// <param name="reader">Reader</param>
    /// <param name="nalUnit">NALU</param>
    protected H264Track(BitStreamReader reader, NalUnit nalUnit)
    {
        Reader = reader;
        NalUnit = nalUnit;
    }

    /// <summary>
    ///   Writes the actual data (except for the NAL unit part).
    /// </summary>
    /// <param name="writer">Bitstream writer where data is written to.</param>
    protected abstract void WriteData(BitStreamWriter writer);

    /// <summary>
    ///   Writes the actual data (except for the NAL unit part).
    /// </summary>
    /// <param name="writer">Bitstream writer where data is written to.</param>
    protected abstract Task WriteDataAsync(BitStreamWriter writer);

    /// <summary>
    ///   Writes the track to <paramref name="writer"/>.
    /// </summary>
    /// <param name="writer">Bitstream writer where to write actual data to.</param>
    public void WriteTo(BitStreamWriter writer)
    {
        writer.WriteBits(0, 8);
        writer.WriteBits(0, 8);
        writer.WriteBits(0, 8);
        writer.WriteBits(1, 8);
        this.NalUnit.Write(writer);
        // Instead of writing the RBSP directly, use WriteData.
        // This allows more flexibility and can be debugged. Also
        // considering that custom data in the underlying track
        // could get edited.
        this.WriteData(writer);
    }

    /// <summary>
    ///   Writes the track to <paramref name="writer"/>.
    /// </summary>
    /// <param name="writer">Bitstream writer where to write actual data to.</param>
    /// <returns>An awaitable task.</returns>
    public async Task WriteToAsync(BitStreamWriter writer)
    {
        await writer.WriteBitsAsync(0, 8);
        await writer.WriteBitsAsync(0, 8);
        await writer.WriteBitsAsync(0, 8);
        await writer.WriteBitsAsync(1, 8);

        await this.NalUnit.WriteAsync(writer);
        // Instead of writing the RBSP directly, use WriteData.
        // This allows more flexibility and can be debugged. Also
        // considering that custom data in the underlying track
        // could get edited.
        await this.WriteDataAsync(writer);
    }
}
