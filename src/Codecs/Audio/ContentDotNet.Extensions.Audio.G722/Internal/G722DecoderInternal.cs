namespace ContentDotNet.Extensions.Audio.G722.Internal
{
    internal class G722DecoderInternal
    {
        private readonly SbAdpcm adpcm;

        public G722DecoderInternal(SbAdpcm adpcm)
        {
            this.adpcm = adpcm;
        }

        public SbAdpcm Adpcm => this.adpcm;

        public short DecodeSample(short sample)
        {
            adpcm.Encoder.InitializeSample();

            G722Variables curr = adpcm.Encoder.Last22Variables[0];
            curr.xin = sample;
            adpcm.Encoder.Last22Variables[0] = curr;

            adpcm.Decoder.ComputeOutputSignal();

            return (short)adpcm.Encoder.Last22Variables[0].xout;
        }
    }
}
