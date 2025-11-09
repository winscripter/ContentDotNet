namespace ContentDotNet.Audio.Codecs.G722
{
    using ContentDotNet.Api.Abstractions;
    using ContentDotNet.Api.BitStream;
    using ContentDotNet.Api.Primitives;
    using ContentDotNet.Audio;
    using ContentDotNet.Audio.Codecs.G722.Internal;
    using ContentDotNet.Audio.Codecs.G722.Internal.Components;

    /// <summary>
    ///   The G.722 decoder.
    /// </summary>
    public class G722Decoder : IAudioCodec
    {
        private readonly G722DecoderInternal _internalDecoder = new(new SbAdpcm(new Variables()));

        /// <inheritdoc cref="IAudioCodec.SampleRate" />
        public int SampleRate
        {
            get => 16000;
            set
            {
                throw new InvalidOperationException();
            }
        }

        /// <inheritdoc cref="IAudioCodec.BitRate" />
        public int BitRate { get; set; } = DataSize.Kilobytes(64);

        /// <inheritdoc cref="IAudioCodec.CanChangeBitRate" />
        public bool CanChangeBitRate => true;

        /// <inheritdoc cref="IAudioCodec.CanChangeSampleRate" />
        public bool CanChangeSampleRate => false;

        /// <inheritdoc cref="IAudioCodec.Stream" />
        public BitStreamReader Stream { get; }

        /// <inheritdoc cref="IAudioCodec.ChannelCount" />
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

        /// <inheritdoc cref="IAudioCodec.ReadInterleavedSamples(Span{short}, int)" />
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

        /// <inheritdoc cref="IAudioCodec.ReadInterleavedSamples(Span{byte}, int)" />
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

        /// <inheritdoc cref="IAudioCodec.ReadSamples(Span{short})" />
        public void ReadSamples(Span<short> samplesBuffer)
        {
            for (int i = 0; i < samplesBuffer.Length; i++)
                samplesBuffer[i] = ReadSample();
        }

        /// <inheritdoc cref="IAudioCodec.ReadSamples(Span{byte})" />
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

        /// <inheritdoc cref="IAudioCodec.ReadSamplesAsync(short[])" />
        public async Task ReadSamplesAsync(short[] samplesBuffer)
        {
            for (int i = 0; i < samplesBuffer.Length; i++)
                samplesBuffer[i] = await ReadSampleAsync();
        }

        /// <inheritdoc cref="IAudioCodec.ReadSamplesAsync(byte[])" />
        public async Task ReadSamplesAsync(byte[] samplesBuffer)
        {
            for (int i = 0; i < samplesBuffer.Length; i++)
                samplesBuffer[i] = (byte)await ReadSampleAsync();
        }

        /// <inheritdoc cref="IAudioCodec.ReadInterleavedSamplesAsync(short[], int)" />
        public async Task ReadInterleavedSamplesAsync(short[] samplesBuffer, int length)
        {
            int currOffset = 0;
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < ChannelCount; j++)
                {
                    samplesBuffer[currOffset] = await ReadSampleAsync();
                    currOffset++;
                }
            }
        }

        /// <inheritdoc cref="IAudioCodec.ReadInterleavedSamplesAsync(byte[], int)" />
        public async Task ReadInterleavedSamplesAsync(byte[] samplesBuffer, int length)
        {
            int currOffset = 0;
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < ChannelCount; j++)
                {
                    samplesBuffer[currOffset] = (byte)await ReadSampleAsync();
                    currOffset++;
                }
            }
        }
    }
}
