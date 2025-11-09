namespace ContentDotNet.Image.Formats.Gif
{
    using ContentDotNet.Api.BitStream;

    /// <summary>
    ///   GIF LZW utilities
    /// </summary>
    public static class GifLzw
    {
        private const int MaxKeys = 1048576; // Safety feature

        /// <summary>
        ///   Decodes the source compressed LZW data. The output uncompressed data is written into <paramref name="outputData"/>.
        /// </summary>
        /// <param name="compressedData">Compressed LZW Data</param>
        /// <param name="minCodeSize">Minimum LZW Code Size</param>
        /// <param name="outputData">This is where you'll receive uncompressed data</param>
        public static void Decode(Stream compressedData, int minCodeSize, Stream outputData)
        {
            int clearCode = 1 << minCodeSize;
            int endCode = clearCode + 1;
            int codeSize = minCodeSize + 1;
            int nextCode = endCode + 1;
            int maxCode = (1 << codeSize) - 1;

            Dictionary<int, List<byte>> dictionary = [];
            for (int i = 0; i < clearCode; i++)
                dictionary[i] = [(byte)i];
            var reader = new BitStreamReader(compressedData);
            int prevCode = -1;

            while (true)
            {
                uint code = reader.ReadBits((uint)codeSize);
                if (code == clearCode)
                {
                    dictionary.Clear();
                    for (int i = 0; i < clearCode; i++) dictionary[i] = [(byte)i];
                    codeSize = minCodeSize + 1;
                    nextCode = endCode + 1;
                    maxCode = (1 << codeSize) - 1;
                    prevCode = -1;
                    continue;
                }
                if (code == endCode) break;
                List<byte> entry;
                if (dictionary.ContainsKey((int)code))
                {
                    entry = [.. dictionary[(int)code]];
                }
                else
                {
                    entry = [.. dictionary[prevCode]];
                    entry.Add(dictionary[prevCode][0]);
                    if (entry.Count > MaxKeys)
                        throw new InvalidOperationException("Entry size too large");
                }
                outputData.Write(entry.ToArray());
                if (prevCode != -1)
                {
                    var newEntry = new List<byte>(dictionary[prevCode])
                    {
                        entry[0]
                    };
                    dictionary[nextCode] = newEntry;
                    nextCode++;
                    if (nextCode > maxCode && codeSize < 12)
                    {
                        codeSize++;
                        maxCode = (1 << codeSize) - 1;
                    }
                }
                prevCode = (int)code;
            }
        }

        /// <summary>
        ///   Compresses uncompressed data into GIF LZW compressed data.
        /// </summary>
        /// <param name="indices">Input uncompressed data.</param>
        /// <param name="minCodeSize">Minimum code size.</param>
        /// <param name="outputCompressedData">Output LZW-compressed data.</param>
        public static void Encode(Stream indices, int minCodeSize, Stream outputCompressedData)
        {
            int clearCode = 1 << minCodeSize;
            int endCode = clearCode + 1;
            int codeSize = minCodeSize + 1;
            int nextCode = endCode + 1;
            int maxCode = (1 << codeSize) - 1;

            Dictionary<string, int> dictionary = [];
            for (int i = 0; i < clearCode; i++)
                dictionary[i.ToString()] = i;

            var writer = new BitStreamWriter(outputCompressedData);
            writer.WriteBits((uint)clearCode, (uint)codeSize);

            string current = "";
            while (true)
            {
                int byteVal = indices.ReadByte();
                if (byteVal == -1)
                    break;
                byte b = (byte)byteVal;
                string combined = current + "," + b;
                if (dictionary.ContainsKey(combined))
                {
                    current = combined;
                }
                else
                {
                    writer.WriteBits((uint)dictionary[current], (uint)codeSize);
                    if (nextCode <= 4095) // GIF max code = 12 bits
                    {
                        dictionary[combined] = nextCode++;
                        if (nextCode > maxCode && codeSize < 12)
                        {
                            codeSize++;
                            maxCode = (1 << codeSize) - 1;
                        }
                    }

                    current = b.ToString();
                }
            }

            if (current != "")
                writer.WriteBits((uint)dictionary[current], (uint)codeSize);

            writer.WriteBits((uint)endCode, (uint)codeSize);

            while (writer.GetState().BitPosition != 0)
                writer.WriteBit(false); // Align to byte boundary
        }
    }
}
