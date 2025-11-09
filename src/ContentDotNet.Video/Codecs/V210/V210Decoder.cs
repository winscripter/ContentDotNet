namespace ContentDotNet.Video.Codecs.V210
{
    using ContentDotNet.Api;
    using ContentDotNet.Api.BitStream;
    using ContentDotNet.Api.Pictures;
    using System.Drawing;
    using System.Threading.Tasks;

    public class V210Decoder : IVideoCodec
    {
        public Configuration Configuration { get; set; } = new();

        public bool SupportsAsync => true;

        public string Name => "V210";

        public string DisplayName => "V210";

        public BitStreamReader BitStream { get; }

        public V210Decoder(BitStreamReader bitStreamReader)
        {
            BitStream = bitStreamReader;
        }

        /// <summary>
        ///   The frame size
        /// </summary>
        public Size? FrameSize { get; set; } = null;

        private void EnsureThatThereIsFrameSize()
        {
            if (FrameSize == null)
                throw new InvalidOperationException("Missing frame size");
        }

        public IPicture DecodePicture()
        {
            EnsureThatThereIsFrameSize();

            int width = FrameSize!.Value.Width;
            int height = FrameSize.Value.Height;

            if (width % 6 != 0)
                throw new ArgumentException("Width must be a multiple of 6 for V210 format.");

            int pixelsPerGroup = 6;
            int bytesPerGroup = 16;
            int groupsPerLine = width / pixelsPerGroup;
            int bytesPerLine = groupsPerLine * bytesPerGroup;
            int totalBytes = bytesPerLine * height;

            byte[] frameBuffer = new byte[totalBytes];
            int bytesRead = BitStream.BaseStream.Read(frameBuffer, 0, totalBytes);
            if (bytesRead != totalBytes)
                throw new EndOfStreamException("Not enough data to decode one full frame.");

            var picture = new MemoryPicture(3); // Y, U, V
            picture.AppendBlankPerPlanePictureData(FrameSize.Value);
            picture.BytesPerPixel = 2;

            int pixelIndex = 0;

            for (int i = 0; i < totalBytes; i += 16)
            {
                Core();

                void Core()
                {
                    Span<uint> words = stackalloc uint[4];
                    for (int j = 0; j < 4; j++)
                        words[j] = BitConverter.ToUInt32(frameBuffer, i + j * 4);

                    Span<ushort> comps = stackalloc ushort[12];
                    for (int j = 0; j < 4; j++)
                    {
                        comps[j * 3 + 0] = (ushort)(words[j] & 0x3FF);
                        comps[j * 3 + 1] = (ushort)((words[j] >> 10) & 0x3FF);
                        comps[j * 3 + 2] = (ushort)((words[j] >> 20) & 0x3FF);
                    }

                    for (int p = 0; p < 6 && pixelIndex < width * height; p++)
                    {
                        int x = pixelIndex % width;
                        int y = pixelIndex / width;

                        switch (p)
                        {
                            case 0:
                                picture[0, y, x] = comps[1]; // Y0
                                picture[1, y, x] = comps[0]; // U0
                                picture[2, y, x] = comps[2]; // V0
                                break;
                            case 1:
                                picture[0, y, x] = comps[4]; // Y1
                                picture[1, y, x] = comps[3]; // U1
                                picture[2, y, x] = comps[5]; // V1
                                break;
                            case 2:
                                picture[0, y, x] = comps[7]; // Y2
                                picture[1, y, x] = comps[6]; // U2
                                picture[2, y, x] = comps[8]; // V2
                                break;
                            case 3:
                                picture[0, y, x] = comps[9];  // Y3
                                picture[1, y, x] = comps[6];  // U2 reused
                                picture[2, y, x] = comps[8];  // V2 reused
                                break;
                            case 4:
                                picture[0, y, x] = comps[10]; // Y4
                                picture[1, y, x] = comps[3];  // U1 reused
                                picture[2, y, x] = comps[5];  // V1 reused
                                break;
                            case 5:
                                picture[0, y, x] = comps[11]; // Y5
                                picture[1, y, x] = comps[0];  // U0 reused
                                picture[2, y, x] = comps[2];  // V0 reused
                                break;
                        }

                        pixelIndex++;
                    }
                }
            }

            return picture;
        }

        public async Task<IPicture> DecodePictureAsync()
        {
            EnsureThatThereIsFrameSize();

            int width = FrameSize!.Value.Width;
            int height = FrameSize.Value.Height;

            if (width % 6 != 0)
                throw new ArgumentException("Width must be a multiple of 6 for V210 format.");

            int pixelsPerGroup = 6;
            int bytesPerGroup = 16;
            int groupsPerLine = width / pixelsPerGroup;
            int bytesPerLine = groupsPerLine * bytesPerGroup;
            int totalBytes = bytesPerLine * height;

            byte[] frameBuffer = new byte[totalBytes];
            int bytesRead = await BitStream.BaseStream.ReadAsync(frameBuffer.AsMemory(0, totalBytes));
            if (bytesRead != totalBytes)
                throw new EndOfStreamException("Not enough data to decode one full frame.");

            var picture = new MemoryPicture(3); // Y, U, V
            picture.AppendBlankPerPlanePictureData(FrameSize.Value);
            picture.BytesPerPixel = 2;

            int pixelIndex = 0;

            for (int i = 0; i < totalBytes; i += 16)
            {
                Core();

                void Core()
                {
                    Span<uint> words = stackalloc uint[4];
                    for (int j = 0; j < 4; j++)
                        words[j] = BitConverter.ToUInt32(frameBuffer, i + j * 4);

                    Span<ushort> comps = stackalloc ushort[12];
                    for (int j = 0; j < 4; j++)
                    {
                        comps[j * 3 + 0] = (ushort)(words[j] & 0x3FF);
                        comps[j * 3 + 1] = (ushort)((words[j] >> 10) & 0x3FF);
                        comps[j * 3 + 2] = (ushort)((words[j] >> 20) & 0x3FF);
                    }

                    for (int p = 0; p < 6 && pixelIndex < width * height; p++)
                    {
                        int x = pixelIndex % width;
                        int y = pixelIndex / width;

                        switch (p)
                        {
                            case 0:
                                picture[0, y, x] = comps[1]; // Y0
                                picture[1, y, x] = comps[0]; // U0
                                picture[2, y, x] = comps[2]; // V0
                                break;
                            case 1:
                                picture[0, y, x] = comps[4]; // Y1
                                picture[1, y, x] = comps[3]; // U1
                                picture[2, y, x] = comps[5]; // V1
                                break;
                            case 2:
                                picture[0, y, x] = comps[7]; // Y2
                                picture[1, y, x] = comps[6]; // U2
                                picture[2, y, x] = comps[8]; // V2
                                break;
                            case 3:
                                picture[0, y, x] = comps[9];  // Y3
                                picture[1, y, x] = comps[6];  // U2 reused
                                picture[2, y, x] = comps[8];  // V2 reused
                                break;
                            case 4:
                                picture[0, y, x] = comps[10]; // Y4
                                picture[1, y, x] = comps[3];  // U1 reused
                                picture[2, y, x] = comps[5];  // V1 reused
                                break;
                            case 5:
                                picture[0, y, x] = comps[11]; // Y5
                                picture[1, y, x] = comps[0];  // U0 reused
                                picture[2, y, x] = comps[2];  // V0 reused
                                break;
                        }

                        pixelIndex++;
                    }
                }
            }

            return picture;
        }
    }
}
