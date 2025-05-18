using ContentDotNet.Abstractions;
using ContentDotNet.BitStream;

namespace ContentDotNet.Extensions.G711.Internal;

internal sealed class G711CodecWriter : IPcmAudioCodecWriter
{
    public BitStreamWriter Stream { get; private set; }
    public G711Law Law { get; private set; }

    public G711CodecWriter(BitStreamWriter stream, G711Law law)
    {
        this.Stream = stream;
        this.Law = law;
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

    public void WriteInterleavedSamples(ReadOnlySpan<short> samples, int length)
    {
        if (samples.Length < (length * ChannelCount))
            throw new ArgumentOutOfRangeException(nameof(length), "Not enough samples");

        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < ChannelCount; j++)
            {
                int sampleIndex = (i * ChannelCount) + j;
                WriteSample(samples[sampleIndex]);
            }
        }
    }

    public void WriteInterleavedSamples(ReadOnlySpan<byte> samples, int length)
    {
        if (samples.Length < (length * ChannelCount))
            throw new ArgumentOutOfRangeException(nameof(length), "Not enough samples");

        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < ChannelCount; j++)
            {
                int sampleIndex = (i * ChannelCount) + j;
                WriteSample(samples[sampleIndex]);
            }
        }
    }

    public async Task WriteInterleavedSamplesAsync(ReadOnlyMemory<short> samples, int length, CancellationToken cancellationToken = default)
    {
        if (samples.Length < (length * ChannelCount))
            throw new ArgumentOutOfRangeException(nameof(length), "Not enough samples");

        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < ChannelCount; j++)
            {
                int sampleIndex = (i * ChannelCount) + j;
                await WriteSampleAsync(samples.Span[sampleIndex]);

                cancellationToken.ThrowIfCancellationRequested();
            }
        }
    }

    public async Task WriteInterleavedSamplesAsync(ReadOnlyMemory<byte> samples, int length, CancellationToken cancellationToken = default)
    {
        if (samples.Length < (length * ChannelCount))
            throw new ArgumentOutOfRangeException(nameof(length), "Not enough samples");

        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < ChannelCount; j++)
            {
                int sampleIndex = (i * ChannelCount) + j;
                await WriteSampleAsync(samples.Span[sampleIndex]);

                cancellationToken.ThrowIfCancellationRequested();
            }
        }
    }

    public void WriteSamples(ReadOnlySpan<short> samples)
    {
        for (int i = 0; i < samples.Length; i++)
            WriteSample(samples[i]);
    }

    public void WriteSamples(ReadOnlySpan<byte> samples)
    {
        for (int i = 0; i < samples.Length; i++)
            WriteSample(samples[i]);
    }

    public void WriteSamples(ReadOnlySpan<short> channel1, ReadOnlySpan<short> channel2, ReadOnlySpan<short> channel3, int length)
    {
        EnsureSampleLengths(channel1.Length, channel2.Length, channel3.Length, length, nameof(channel1), nameof(channel2), nameof(channel3));

        for (int i = 0; i < length; i++)
        {
            ConditionalWriteSample(channel1[i], channel2[i], channel3[i]);
        }
    }

    public void WriteSamples(ReadOnlySpan<byte> channel1, ReadOnlySpan<byte> channel2, ReadOnlySpan<byte> channel3, int length)
    {
        EnsureSampleLengths(channel1.Length, channel2.Length, channel3.Length, length, nameof(channel1), nameof(channel2), nameof(channel3));

        for (int i = 0; i < length; i++)
        {
            ConditionalWriteSample(channel1[i], channel2[i], channel3[i]);
        }
    }

    public async Task WriteSamplesAsync(ReadOnlyMemory<short> samples, CancellationToken cancellationToken = default)
    {
        for (int i = 0; i < samples.Length; i++)
        {
            await WriteSampleAsync(samples.Span[i]);
            cancellationToken.ThrowIfCancellationRequested();
        }
    }

    public async Task WriteSamplesAsync(ReadOnlyMemory<byte> samples, CancellationToken cancellationToken = default)
    {
        for (int i = 0; i < samples.Length; i++)
        {
            await WriteSampleAsync(samples.Span[i]);
            cancellationToken.ThrowIfCancellationRequested();
        }
    }

    public async Task WriteSamplesAsync(ReadOnlyMemory<short> channel1, ReadOnlyMemory<short> channel2, ReadOnlyMemory<short> channel3, int length, CancellationToken cancellationToken = default)
    {
        EnsureSampleLengths(channel1.Length, channel2.Length, channel3.Length, length, nameof(channel1), nameof(channel2), nameof(channel3));

        for (int i = 0; i < length; i++)
        {
            await ConditionalWriteSampleAsync(channel1.Span[i], channel2.Span[i], channel3.Span[i]);
            cancellationToken.ThrowIfCancellationRequested();
        }
    }

    public async Task WriteSamplesAsync(ReadOnlyMemory<byte> channel1, ReadOnlyMemory<byte> channel2, ReadOnlyMemory<byte> channel3, int length, CancellationToken cancellationToken = default)
    {
        EnsureSampleLengths(channel1.Length, channel2.Length, channel3.Length, length, nameof(channel1), nameof(channel2), nameof(channel3));

        for (int i = 0; i < length; i++)
        {
            await ConditionalWriteSampleAsync(channel1.Span[i], channel2.Span[i], channel3.Span[i]);
            cancellationToken.ThrowIfCancellationRequested();
        }
    }

    private void ConditionalWriteSample(short a, short b, short c)
    {
        if (ChannelCount >= 1)
            WriteSample(a);
        if (ChannelCount >= 2)
            WriteSample(b);
        if (ChannelCount >= 3)
            WriteSample(c);
    }

    private async Task ConditionalWriteSampleAsync(short a, short b, short c)
    {
        if (ChannelCount >= 1)
            await WriteSampleAsync(a);
        if (ChannelCount >= 2)
            await WriteSampleAsync(b);
        if (ChannelCount >= 3)
            await WriteSampleAsync(c);
    }

    private static void EnsureSampleLengths(int channel1Length, int channel2Length, int channel3Length, int length, string channel1Param, string channel2Param, string channel3Param)
    {
        if (channel1Length < length)
            throw new ArgumentOutOfRangeException(channel1Param, "There must be at least length number of parameters");
        if (channel2Length < length)
            throw new ArgumentOutOfRangeException(channel2Param, "There must be at least length number of parameters");
        if (channel3Length < length)
            throw new ArgumentOutOfRangeException(channel3Param, "There must be at least length number of parameters");
    }

    private void WriteSample(short value)
    {
        this.Stream.WriteBits(this.Law == G711Law.A ? G711Lookups.PcmToA[value] : G711Lookups.PcmToMu[value], 8);
    }

    private async Task WriteSampleAsync(short value)
    {
        await this.Stream.WriteBitsAsync(this.Law == G711Law.A ? G711Lookups.PcmToA[value] : G711Lookups.PcmToMu[value], 8);
    }
}
