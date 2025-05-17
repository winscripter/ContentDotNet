using ContentDotNet.BitStream;
namespace ContentDotNet.Abstractions;

/// <summary>
///   Represents a PCM audio codec.
/// </summary>
public interface IPcmAudioCodec : ICodecWithNames, IDisposable
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
    ///   The stream where the PCM audio codec is being read from.
    /// </summary>
    BitStreamReader Stream { get; }

    /// <summary>
    ///   Gets or sets the number of audio channels (e.g., 1 for mono, 2 for stereo).
    /// </summary>
    /// <remarks>
    ///   This value may be ignored by codecs that assume a fixed channel layout.
    /// </remarks>
    int ChannelCount { get; set; }

    /// <summary>
    ///   Reads samples one time, storing them into the given buffer.
    /// </summary>
    /// <param name="samplesBuffer">Buffer where samples are stored.</param>
    void ReadSamples(Span<short> samplesBuffer);

    /// <summary>
    ///   Reads samples one time, storing them into the given buffer.
    /// </summary>
    /// <param name="samplesBuffer">Buffer where samples are stored.</param>
    void ReadSamples(Span<byte> samplesBuffer);

    /// <summary>
    ///   Reads samples one time, storing them into the given buffer.
    /// </summary>
    /// <param name="channel1">Decoded samples for Channel 1.</param>
    /// <param name="channel2">Decoded samples for Channel 2.</param>
    /// <param name="channel3">Decoded samples for Channel 3.</param>
    /// <param name="length">Length of each channel.</param>
    void ReadSamples(Span<short> channel1, Span<short> channel2, Span<short> channel3, int length);

    /// <summary>
    ///   Reads samples one time, storing them into the given buffer.
    /// </summary>
    /// <param name="channel1">Decoded samples for Channel 1.</param>
    /// <param name="channel2">Decoded samples for Channel 2.</param>
    /// <param name="channel3">Decoded samples for Channel 3.</param>
    /// <param name="length">Length of each channel.</param>
    void ReadSamples(Span<byte> channel1, Span<byte> channel2, Span<byte> channel3, int length);

    /// <summary>
    ///   Reads samples one time, storing them into the given buffer, with interleaving support
    ///   for multiple channels.
    /// </summary>
    /// <param name="samplesBuffer">Buffer where samples are stored.</param>
    /// <param name="length">Number of samples per channel.</param>
    void ReadInterleavedSamples(Span<short> samplesBuffer, int length);

    /// <summary>
    ///   Reads samples one time, storing them into the given buffer, with interleaving support
    ///   for multiple channels.
    /// </summary>
    /// <param name="samplesBuffer">Buffer where samples are stored.</param>
    /// <param name="length">Number of samples per channel.</param>
    void ReadInterleavedSamples(Span<byte> samplesBuffer, int length);
}
