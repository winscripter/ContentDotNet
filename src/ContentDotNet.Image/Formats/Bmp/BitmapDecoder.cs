namespace ContentDotNet.Image.Formats.Bmp
{
    using ContentDotNet.Api.Pictures;
    using System.Drawing;

    /// <summary>
    ///   Bitmap decoder
    /// </summary>
    public class BitmapDecoder(BitmapReader bitmapReader) : IImageDecoder
    {
        private const int MaxPaletteSize = 1048576;

        /// <summary>
        ///   The actual reader.
        /// </summary>
        public BitmapReader Reader { get; set; } = bitmapReader;

        /// <summary>
        ///   No async decoding.
        /// </summary>
        public bool SupportsAsync => false;

        /// <summary>
        ///   Returns BMP
        /// </summary>
        public string Name => "BMP";

        /// <summary>
        ///   Returns Bitmap.
        /// </summary>
        public string DisplayName => "Bitmap";

        public IPicture Decode()
        {
            BitmapFileHeader bfh = Reader.ReadFileHeader();
            IBitmapHeader dibHeader = Reader.ReadDibHeader();
            return DecodeBmp(Reader.BinaryReader, in bfh, dibHeader);
        }

        public Task<IPicture> DecodeAsync()
        {
            throw new NotImplementedException("No async support");
        }

        private static uint ReadSinglePixel(IBitmapHeader dibHeader, BinaryReader reader, uint[]? palette, int x, int y)
        {
            int width = dibHeader.Width();
            int height = Math.Abs(dibHeader.Height());
            int bpp = dibHeader.BitsPerPixel();

            if (x < 0 || x >= width || y < 0 || y >= height)
                throw new ArgumentOutOfRangeException(x < 0 || x >= width ? nameof(x) : nameof(y), "Pixel coordinates out of bounds");

            bool isTopDown = dibHeader.Height() < 0;
            int rowIndex = isTopDown ? y : (height - 1 - y);

            int rowBytes = ((width * bpp + 31) / 32) * 4;
            long rowStart = reader.BaseStream.Position + rowIndex * rowBytes;

            reader.BaseStream.Seek(rowStart, SeekOrigin.Begin);

            if (bpp == 1)
            {
                int bitIndex = 7 - (x % 8);
                byte b = reader.ReadByte();
                int paletteIndex = (b >> bitIndex) & 0x1;
                return palette![paletteIndex];
            }
            else if (bpp == 4)
            {
                byte b = reader.ReadByte();
                int paletteIndex = (x % 2 == 0) ? (b >> 4) : (b & 0xF);
                return palette![paletteIndex];
            }
            else if (bpp == 8)
            {
                byte index = reader.ReadByte();
                return palette![index];
            }
            else if (bpp == 24)
            {
                byte blue = reader.ReadByte();
                byte green = reader.ReadByte();
                byte red = reader.ReadByte();
                return (uint)(0xFF << 24 | red << 16 | green << 8 | blue);
            }
            else if (bpp == 32)
            {
                byte blue = reader.ReadByte();
                byte green = reader.ReadByte();
                byte red = reader.ReadByte();
                byte alpha = reader.ReadByte();
                return (uint)(alpha << 24 | red << 16 | green << 8 | blue);
            }
            else
            {
                throw new NotSupportedException($"Bit depth {bpp} not supported");
            }
        }

        /// <summary>
        /// Reads the color palette for paletted BMP images (1, 4, or 8 bpp).
        /// </summary>
        /// <param name="dibHeader">The DIB header.</param>
        /// <param name="reader">The binary reader positioned after the DIB header.</param>
        /// <returns>Array of colors in 0xAARRGGBB format.</returns>
        private static uint[]? ReadPalette(IBitmapHeader dibHeader, BinaryReader reader)
        {
            int bpp = dibHeader.BitsPerPixel();
            if (bpp > 8)
                return null;

            int paletteSize = dibHeader.ColorCount();
            if (paletteSize > MaxPaletteSize)
                throw new InvalidOperationException("The palette is too large");

            uint[] palette = new uint[paletteSize];
            for (int i = 0; i < paletteSize; i++)
            {
                byte blue = reader.ReadByte();
                byte green = reader.ReadByte();
                byte red = reader.ReadByte();
                reader.ReadByte();

                palette[i] = (uint)(0xFF << 24 | red << 16 | green << 8 | blue);
            }

            return palette;
        }

        private static MemoryPicture ReadPixels(IBitmapHeader dib, BinaryReader reader, uint[]? palette, long pixelDataOffset)
        {
            int width = dib.Width();
            int height = Math.Abs(dib.Height());
            int bpp = dib.BitsPerPixel();
            bool topDown = dib.Height() < 0;

            MemoryPicture result = new(4);
            result.AppendBlankPerPlanePictureData(new Size(width, height));

            reader.BaseStream.Seek(pixelDataOffset, SeekOrigin.Begin);

            int rowBytes = ((width * bpp + 31) / 32) * 4;
            byte[] rowBuffer = new byte[rowBytes];

            for (int row = 0; row < height; row++)
            {
                int targetRow = topDown ? row : height - 1 - row;
                reader.BaseStream.Read(rowBuffer, 0, rowBytes);

                if (bpp == 1)
                {
                    for (int col = 0; col < width; col++)
                    {
                        int byteIndex = col / 8;
                        int bitIndex = 7 - (col % 8);
                        int paletteIndex = (rowBuffer[byteIndex] >> bitIndex) & 0x1;
                        SetPixel(targetRow, col, Color.FromArgb((int)palette![paletteIndex]));
                    }
                }
                else if (bpp == 4)
                {
                    for (int col = 0; col < width; col++)
                    {
                        int byteIndex = col / 2;
                        int paletteIndex = (col % 2 == 0) ? (rowBuffer[byteIndex] >> 4) : (rowBuffer[byteIndex] & 0xF);
                        SetPixel(targetRow, col, Color.FromArgb((int)palette![paletteIndex]));
                    }
                }
                else if (bpp == 8)
                {
                    for (int col = 0; col < width; col++)
                    {
                        byte idx = rowBuffer[col];
                        SetPixel(targetRow, col, Color.FromArgb((int)palette![idx]));
                    }
                }
                else if (bpp == 24)
                {
                    for (int col = 0; col < width; col++)
                    {
                        int offset = col * 3;
                        byte b = rowBuffer[offset];
                        byte g = rowBuffer[offset + 1];
                        byte r = rowBuffer[offset + 2];
                        SetPixel(targetRow, col, Color.FromArgb(255, r, g, b));
                    }
                }
                else if (bpp == 32)
                {
                    for (int col = 0; col < width; col++)
                    {
                        int offset = col * 4;
                        byte b = rowBuffer[offset];
                        byte g = rowBuffer[offset + 1];
                        byte r = rowBuffer[offset + 2];
                        byte a = rowBuffer[offset + 3];
                        SetPixel(targetRow, col, Color.FromArgb(a, r, g, b));
                    }
                }
            }

            return result;

            void SetPixel(int x, int y, Color clr)
            {
                result[0, x, y] = clr.R;
                result[1, x, y] = clr.G;
                result[2, x, y] = clr.B;
                result[3, x, y] = clr.A;
            }
        }

        private static MemoryPicture DecodeRle8(IBitmapHeader dib, BinaryReader reader, uint[] palette, long pixelDataOffset)
        {
            int width = dib.Width();
            int height = Math.Abs(dib.Height());
            bool topDown = dib.Height() < 0;
            var result = new MemoryPicture(3);
            result.AppendBlankPerPlanePictureData(new Size(width, height));

            reader.BaseStream.Seek(pixelDataOffset, SeekOrigin.Begin);

            int x = 0, y = 0;

            while (y < height)
            {
                byte count = reader.ReadByte();
                byte value = reader.ReadByte();

                if (count > 0)
                {
                    // Encoded mode
                    for (int i = 0; i < count; i++)
                    {
                        int targetY = topDown ? y : height - 1 - y;
                        var clr = Color.FromArgb((int)palette[value]);
                        result[0, targetY, x] = clr.R;
                        result[1, targetY, x] = clr.G;
                        result[2, targetY, x] = clr.B;
                        result[3, targetY, x] = clr.A;
                        x++;
                        if (x >= width) { x = 0; y++; }
                    }
                }
                else
                {
                    // Escape codes
                    switch (value)
                    {
                        case 0: x = 0; y++; break; // End of line
                        case 1: return result;      // End of bitmap
                        case 2:                     // Delta
                            {
                                byte dx = reader.ReadByte();
                                byte dy = reader.ReadByte();
                                x += dx;
                                y += dy;
                                break;
                            }
                        default:
                            {
                                // Absolute mode: next 'value' bytes are literal pixel indices
                                int absCount = value;
                                for (int i = 0; i < absCount; i++)
                                {
                                    byte idx = reader.ReadByte();
                                    int targetY = topDown ? y : height - 1 - y;
                                    var clr = Color.FromArgb((int)palette[idx]);
                                    result[0, targetY, x] = clr.R;
                                    result[1, targetY, x] = clr.G;
                                    result[2, targetY, x] = clr.B;
                                    result[3, targetY, x] = clr.A;
                                    x++;
                                    if (x >= width) { x = 0; y++; }
                                }
                                if ((absCount & 1) == 1) reader.ReadByte(); // pad to word boundary
                                break;
                            }
                    }
                }
            }

            return result;
        }

        private static MemoryPicture DecodeBmp(BinaryReader reader, in BitmapFileHeader fileHeader, IBitmapHeader dibHeader)
        {
            uint[]? palette = ReadPalette(dibHeader, reader);

            long pixelOffset = fileHeader.BfOffBits;

            if (dibHeader.IsRleCompressed())
            {
                if (dibHeader.BitsPerPixel() == 8)
                    return DecodeRle8(dibHeader, reader, palette!, pixelOffset);
                else
                    throw new NotImplementedException("RLE4 not implemented yet");
            }
            else
            {
                return ReadPixels(dibHeader, reader, palette, pixelOffset);
            }
        }
    }
}
