namespace ContentDotNet.Extensions.Image.Jpeg.Processing
{
    internal class JpegEncoder
    {
        private readonly BinaryWriter writer;

        public JpegEncoder(BinaryWriter stream) => this.writer = stream;

        public ushort SofMarker { get; set; } = JpegMarkers.Sof0;

        public void EncodeImage()
        {
            writer.Write(JpegMarkers.Soi);
            EncodeFrame();
            writer.Write(JpegMarkers.Eoi);
        }

        private void EncodeFrame()
        {

        }
    }
}
