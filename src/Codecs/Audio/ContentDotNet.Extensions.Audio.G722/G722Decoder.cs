namespace ContentDotNet.Extensions.Audio.G722
{
    using ContentDotNet.Abstractions;
    using ContentDotNet.BitStream;
    using ContentDotNet.Extensions.Audio.G722.Internal;
    using ContentDotNet.Extensions.Audio.G722.Internal.Components;
    using ContentDotNet.Primitives;
    using System;

    /// <summary>
    ///   The G.722 decoder.
    /// </summary>
    public class G722Decoder : IPcmAudioCodec
    {
        private readonly G722DecoderInternal _internalDecoder = new(new SbAdpcm(new Variables()));

        /// <inheritdoc cref="IPcmAudioCodec.SampleRate" />
        public int SampleRate
        {
            get => 16000;
            set
            {
                throw new InvalidOperationException();
            }
        }

        /// <inheritdoc cref="IPcmAudioCodec.BitRate" />
        public int BitRate { get; set; } = DataSize.Kilobytes(64);

        /// <inheritdoc cref="IPcmAudioCodec.CanChangeBitRate" />
        public bool CanChangeBitRate => true;

        /// <inheritdoc cref="IPcmAudioCodec.CanChangeSampleRate" />
        public bool CanChangeSampleRate => false;

        /// <inheritdoc cref="IPcmAudioCodec.Stream" />
        public BitStreamReader Stream { get; }

        /// <inheritdoc cref="IPcmAudioCodec.ChannelCount" />
        public int ChannelCount { get; set; }

        /// <inheritdoc cref="ICodecWithNames.Name" />
        public string Name => "G722";

        /// <inheritdoc cref="ICodecWithNames.DisplayName" />
        public string DisplayName => "G.722";

        internal G722Decoder(BitStreamReader stream) => Stream = stream;

        /// <inheritdoc cref="IDisposable.Dispose" />
        public void Dispose()
        {
            Stream.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc cref="IPcmAudioCodec.ReadInterleavedSamples(Span{short}, int)" />
        public void ReadInterleavedSamples(Span<short> samplesBuffer, int length)
        {
            int currOffset = 0;
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < ChannelCount; j++)
                {
                    samplesBuffer[currOffset] = ReadSample();
                    currOffset++;
                }
            }
        }

        /// <inheritdoc cref="IPcmAudioCodec.ReadInterleavedSamples(Span{byte}, int)" />
        public void ReadInterleavedSamples(Span<byte> samplesBuffer, int length)
        {
            int currOffset = 0;
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < ChannelCount; j++)
                {
                    samplesBuffer[currOffset] = (byte)ReadSample();
                    currOffset++;
                }
            }
        }

        /// <inheritdoc cref="IPcmAudioCodec.ReadSamples(Span{short})" />
        public void ReadSamples(Span<short> samplesBuffer)
        {
            for (int i = 0; i < samplesBuffer.Length; i++)
                samplesBuffer[i] = ReadSample();
        }

        /// <inheritdoc cref="IPcmAudioCodec.ReadSamples(Span{byte})" />
        public void ReadSamples(Span<byte> samplesBuffer)
        {
            for (int i = 0; i < samplesBuffer.Length; i++)
                samplesBuffer[i] = (byte)ReadSample();
        }

        private short ReadSample()
        {
            return _internalDecoder.DecodeSample((short)Stream.ReadBits(16));
        }

        private async Task<short> ReadSampleAsync()
        {
            return _internalDecoder.DecodeSample((short)await Stream.ReadBitsAsync(16));
        }

        /// <inheritdoc cref="IPcmAudioCodec.ReadSamplesAsync(Memory{short})" />
        public async Task ReadSamplesAsync(Memory<short> samplesBuffer)
        {
            for (int i = 0; i < samplesBuffer.Length; i++)
                samplesBuffer.Span[i] = await ReadSampleAsync();
        }

        /// <inheritdoc cref="IPcmAudioCodec.ReadSamplesAsync(Memory{byte})" />
        public async Task ReadSamplesAsync(Memory<byte> samplesBuffer)
        {
            for (int i = 0; i < samplesBuffer.Length; i++)
                samplesBuffer.Span[i] = (byte)await ReadSampleAsync();
        }

        /// <inheritdoc cref="IPcmAudioCodec.ReadInterleavedSamplesAsync(Memory{short}, int)" />
        public async Task ReadInterleavedSamplesAsync(Memory<short> samplesBuffer, int length)
        {
            int currOffset = 0;
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < ChannelCount; j++)
                {
                    samplesBuffer.Span[currOffset] = await ReadSampleAsync();
                    currOffset++;
                }
            }
        }

        /// <inheritdoc cref="IPcmAudioCodec.ReadInterleavedSamplesAsync(Memory{byte}, int)" />
        public async Task ReadInterleavedSamplesAsync(Memory<byte> samplesBuffer, int length)
        {
            int currOffset = 0;
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < ChannelCount; j++)
                {
                    samplesBuffer.Span[currOffset] = (byte)await ReadSampleAsync();
                    currOffset++;
                }
            }
        }
    }
}
