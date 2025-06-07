namespace ContentDotNet.Compression.Lz;

/// <summary>
///   LZ77 compression and decompression.
/// </summary>
public static class Lz77
{
    private const int WindowSize = 4096;

    /// <summary>
    ///   Compresses a byte array using LZ77.
    /// </summary>
    public static byte[] Compress(byte[] input)
    {
        using MemoryStream ms = new();
        using BinaryWriter writer = new(ms);

        int i = 0;
        while (i < input.Length)
        {
            int searchStart = Math.Max(0, i - WindowSize);
            ReadOnlySpan<byte> searchBuffer = input.AsSpan(searchStart, i - searchStart);
            ReadOnlySpan<byte> lookaheadBuffer = input.AsSpan(i);

            int matchLength = 0, matchOffset = 0;

            for (int j = 1; j < lookaheadBuffer.Length; j++)
            {
                ReadOnlySpan<byte> segment = lookaheadBuffer[..j];
                int index = searchBuffer.LastIndexOf(segment);

                if (index == -1) break;
                matchLength = j;
                matchOffset = searchBuffer.Length - index;
            }

            byte nextByte = lookaheadBuffer[matchLength];
            writer.Write((ushort)matchOffset);
            writer.Write((ushort)matchLength);
            writer.Write(nextByte);

            i += matchLength + 1;
        }

        return ms.ToArray();
    }

    /// <summary>
    ///   Decompresses an LZ77-encoded byte array.
    /// </summary>
    public static byte[] Decompress(byte[] compressedData)
    {
        using MemoryStream ms = new(compressedData);
        using BinaryReader reader = new(ms);
        List<byte> output = [];

        while (ms.Position < ms.Length)
        {
            int offset = reader.ReadUInt16();
            int length = reader.ReadUInt16();
            byte nextByte = reader.ReadByte();

            int startIndex = output.Count - offset;
            for (int i = 0; i < length; i++)
            {
                output.Add(output[startIndex + i]);
            }

            output.Add(nextByte);
        }

        return [.. output];
    }

    /// <summary>
    ///   Compresses a Stream using LZ77.
    /// </summary>
    public static Stream Compress(Stream inputStream)
    {
        byte[] input = StreamToByteArray(inputStream);
        byte[] compressed = Compress(input);
        return new MemoryStream(compressed);
    }

    /// <summary>
    ///   Decompresses an LZ77-encoded Stream.
    /// </summary>
    public static Stream Decompress(Stream compressedStream)
    {
        byte[] compressed = StreamToByteArray(compressedStream);
        byte[] decompressed = Decompress(compressed);
        return new MemoryStream(decompressed);
    }

    /// <summary>
    ///   Converts a Stream into a byte array.
    /// </summary>
    private static byte[] StreamToByteArray(Stream stream)
    {
        using MemoryStream ms = new();
        stream.CopyTo(ms);
        return ms.ToArray();
    }

    /// <summary>
    ///   Compresses a ReadOnlySpan<byte> using LZ77.
    /// </summary>
    public static byte[] Compress(ReadOnlySpan<byte> span)
    {
        using MemoryStream ms = new();
        using BinaryWriter writer = new(ms);

        int i = 0;
        while (i < span.Length)
        {
            int searchStart = Math.Max(0, i - 4096);
            ReadOnlySpan<byte> searchBuffer = span[searchStart..i];
            ReadOnlySpan<byte> lookaheadBuffer = span[i..];

            int matchLength = 0, matchOffset = 0;

            for (int j = 1; j < lookaheadBuffer.Length; j++)
            {
                ReadOnlySpan<byte> segment = lookaheadBuffer[..j];
                int index = searchBuffer.LastIndexOf(segment);

                if (index == -1) break;
                matchLength = j;
                matchOffset = searchBuffer.Length - index;
            }

            byte nextByte = lookaheadBuffer[matchLength];
            writer.Write((ushort)matchOffset);
            writer.Write((ushort)matchLength);
            writer.Write(nextByte);

            i += matchLength + 1;
        }

        return ms.ToArray();
    }

    /// <summary>
    ///   Decompresses an LZ77-encoded ReadOnlySpan<byte>.
    /// </summary>
    public static byte[] Decompress(ReadOnlySpan<byte> span)
    {
        using MemoryStream ms = new(span.ToArray());
        using BinaryReader reader = new(ms);
        List<byte> output = [];

        while (ms.Position < ms.Length)
        {
            int offset = reader.ReadUInt16();
            int length = reader.ReadUInt16();
            byte nextByte = reader.ReadByte();

            int startIndex = output.Count - offset;
            for (int i = 0; i < length; i++)
            {
                output.Add(output[startIndex + i]);
            }

            output.Add(nextByte);
        }

        return [.. output];
    }
}
