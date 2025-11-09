namespace ContentDotNet.Audio;

using ContentDotNet.Api.Abstractions;
using ContentDotNet.Api.BitStream;

/// <summary>
///   PCM audio codec writer.
/// </summary>
public interface IPcmAudioCodecWriter : ICodecWithNames, IDisposable
{
    /// <summary>
    ///   Sample rate, in Hertz.
    /// </summary>
    int SampleRate { get; set; }

    /// <summary>
    ///   Bit rate, in bits per second.
    /// </summary>
    int BitRate { get; set; }

    /// <summary>
    ///   If this is <see langword="true"/> and the setter of <see cref="BitRate"/>
    ///   is invoked, an <see cref="InvalidOperationException"/> will be thrown.
    /// </summary>
    bool CanChangeBitRate { get; }

    /// <summary>
    ///   If this is <see langword="true"/> and the setter of <see cref="SampleRate"/>
    ///   is invoked, an <see cref="InvalidOperationException"/> will be thrown.
    /// </summary>
    bool CanChangeSampleRate { get; }

    /// <summary>
    ///   Gets or sets the number of audio channels (e.g., 1 for mono, 2 for stereo).
    /// </summary>
    /// <remarks>
    ///   This value is ignored if the codec doesn't rely on the number of audio channels.
    /// </remarks>
    int ChannelCount { get; set; }

    /// <summary>
    ///   The stream where the PCM audio codec is being written to.
    /// </summary>
    BitStreamWriter Stream { get; }

    /// <summary>
    ///   Writes given samples to the stream.
    /// </summary>
    /// <param name="samples">Samples to write.</param>
    void WriteSamples(ReadOnlySpan<short> samples);

    /// <summary>
    ///   Writes given samples to the stream.
    /// </summary>
    /// <param name="samples">Samples to write.</param>
    void WriteSamples(ReadOnlySpan<byte> samples);

    /// <summary>
    ///   Writes given interleaved samples to the stream.
    /// </summary>
    /// <param name="samples">Samples to write.</param>
    /// <param name="length">Length of samples without interleaving.</param>
    void WriteInterleavedSamples(ReadOnlySpan<short> samples, int length);

    /// <summary>
    ///   Writes given interleaved samples to the stream.
    /// </summary>
    /// <param name="samples">Samples to write.</param>
    /// <param name="length">Length of samples without interleaving.</param>
    void WriteInterleavedSamples(ReadOnlySpan<byte> samples, int length);

    /// <summary>
    ///   Writes given samples to the stream.
    /// </summary>
    /// <param name="samples">Samples to write.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task WriteSamplesAsync(ReadOnlyMemory<short> samples, CancellationToken cancellationToken = default);

    /// <summary>
    ///   Writes given samples to the stream.
    /// </summary>
    /// <param name="samples">Samples to write.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task WriteSamplesAsync(ReadOnlyMemory<byte> samples, CancellationToken cancellationToken = default);

    /// <summary>
    ///   Writes given interleaved samples to the stream.
    /// </summary>
    /// <param name="samples">Samples to write.</param>
    /// <param name="length">Length of samples without interleaving.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task WriteInterleavedSamplesAsync(ReadOnlyMemory<short> samples, int length, CancellationToken cancellationToken = default);

    /// <summary>
    ///   Writes given interleaved samples to the stream.
    /// </summary>
    /// <param name="samples">Samples to write.</param>
    /// <param name="length">Length of samples without interleaving.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task WriteInterleavedSamplesAsync(ReadOnlyMemory<byte> samples, int length, CancellationToken cancellationToken = default);

    /// <summary>
    ///   Writes given samples per channel to the stream.
    /// </summary>
    /// <param name="channel1">First channel</param>
    /// <param name="channel2">Second channel</param>
    /// <param name="channel3">Third channel</param>
    /// <param name="length">Length of each channel</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task WriteSamplesAsync(ReadOnlyMemory<short> channel1, ReadOnlyMemory<short> channel2, ReadOnlyMemory<short> channel3, int length, CancellationToken cancellationToken = default);

    /// <summary>
    ///   Writes given samples per channel to the stream.
    /// </summary>
    /// <param name="channel1">First channel</param>
    /// <param name="channel2">Second channel</param>
    /// <param name="channel3">Third channel</param>
    /// <param name="length">Length of each channel</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task WriteSamplesAsync(ReadOnlyMemory<byte> channel1, ReadOnlyMemory<byte> channel2, ReadOnlyMemory<byte> channel3, int length, CancellationToken cancellationToken = default);
}
