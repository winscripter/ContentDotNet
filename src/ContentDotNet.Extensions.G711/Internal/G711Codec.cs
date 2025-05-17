using ContentDotNet.Abstractions;
using ContentDotNet.BitStream;

namespace ContentDotNet.Extensions.G711.Internal;

internal sealed class G711Codec : IPcmAudioCodec
{
    public BitStreamReader Stream { get; private set; }
    public G711Law Law { get; private set; }

    public G711Codec(BitStreamReader stream, G711Law law)
    {
        Stream = stream;
        Law = law;
    }

    public int SampleRate
    {
        get => 8000;
        set
        {
            throw new InvalidOperationException("Bit rate of this codec is constant");
        }
    }

    public int BitRate
    {
        get => 64000;
        set
        {
            throw new InvalidOperationException("Bit rate of this codec is constant");
        }
    }

    public bool CanChangeBitRate => false;

    public bool CanChangeSampleRate => false;

    public int ChannelCount { get; set; } = 1;

    public string Name => "G711";

    public string DisplayName => "G.711";

    public void Dispose()
    {
        this.Stream.Dispose();
        GC.SuppressFinalize(this);
    }

    public void ReadSamples(Span<short> samplesBuffer)
    {
        for (int i = 0; i < samplesBuffer.Length; i++)
            samplesBuffer[i] = ReadSample();
    }

    public void ReadSamples(Span<byte> samplesBuffer)
    {
        for (int i = 0; i < samplesBuffer.Length; i++)
            samplesBuffer[i] = ReadSampleAsByte();
    }

    public void ReadSamples(Span<short> channel1, Span<short> channel2, Span<short> channel3, int length)
    {
        if (length > channel1.Length || (ChannelCount > 1 && length > channel2.Length) || (ChannelCount > 2 && length > channel3.Length))
            throw new ArgumentException("Buffers are too small for the specified length.");

        for (int i = 0; i < length; i++)
        {
            if (ChannelCount >= 1)
                channel1[i] = ReadSample();

            if (ChannelCount >= 2)
                channel2[i] = ReadSample();

            if (ChannelCount >= 3)
                channel3[i] = ReadSample();
        }
    }

    public void ReadSamples(Span<byte> channel1, Span<byte> channel2, Span<byte> channel3, int length)
    {
        if (length > channel1.Length || (ChannelCount > 1 && length > channel2.Length) || (ChannelCount > 2 && length > channel3.Length))
            throw new ArgumentException("Buffers are too small for the specified length.");

        for (int i = 0; i < length; i++)
        {
            if (ChannelCount >= 1)
                channel1[i] = ReadSampleAsByte();

            if (ChannelCount >= 2)
                channel2[i] = ReadSampleAsByte();

            if (ChannelCount >= 3)
                channel3[i] = ReadSampleAsByte();
        }
    }

    public void ReadInterleavedSamples(Span<short> samplesBuffer, int length)
    {
        if (samplesBuffer.Length != length * ChannelCount)
            throw new ArgumentException($"Buffer size must be {length * ChannelCount} for {ChannelCount} channels.");

        for (int i = 0; i < length; i++)
            for (int j = 0; j < ChannelCount; j++)
                samplesBuffer[i * ChannelCount + j] = ReadSample();
    }

    public void ReadInterleavedSamples(Span<byte> samplesBuffer, int length)
    {
        if (samplesBuffer.Length != length * ChannelCount)
            throw new ArgumentException($"Buffer size must be {length * ChannelCount} for {ChannelCount} channels.");

        for (int i = 0; i < length; i++)
            for (int j = 0; j < ChannelCount; j++)
                samplesBuffer[i * ChannelCount + j] = ReadSampleAsByte();
    }

    private short ReadSample()
    {
        return this.Law == G711Law.A
                        ? G711Lookups.AToPcm[(byte)this.Stream.ReadByte()]
                        : G711Lookups.MuToPcm[(byte)this.Stream.ReadByte()];
    }

    private byte ReadSampleAsByte()
    {
        return (byte)(this.Law == G711Law.A
                        ? G711Lookups.AToPcm[(byte)this.Stream.ReadByte()]
                        : G711Lookups.MuToPcm[(byte)this.Stream.ReadByte()]);
    }
}
