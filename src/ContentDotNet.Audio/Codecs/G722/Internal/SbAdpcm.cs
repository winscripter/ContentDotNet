namespace ContentDotNet.Audio.Codecs.G722.Internal
{
    using ContentDotNet.Audio.Codecs.G722.Internal.Components;

    internal class SbAdpcm
    {
        public SbAdpcmDecoder Decoder { get; set; }
        public SbAdpcmEncoder Encoder { get; set; }

        public SbAdpcm(Variables buffer)
        {
            Encoder = new() { Last22Variables = buffer };
            Decoder = new(Encoder);
        }
    }
}
