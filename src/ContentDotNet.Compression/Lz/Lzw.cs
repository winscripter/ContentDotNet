namespace ContentDotNet.Compression.Lz;

/// <summary>
///   LZW compression and decompression
/// </summary>
public static class Lzw
{
    /// <summary>
    ///   Decompresses an LZW-encoded byte array.
    /// </summary>
    public static byte[] Decompress(byte[] compressedData)
    {
        int[] compressed = GetInts(compressedData);
        Dictionary<int, List<byte>> dictionary = [];
        for (int i = 0; i < 256; i++)
            dictionary[i] = [(byte)i];

        List<byte> output = [.. dictionary[compressed[0]]];
        List<byte> window = [.. output];

        for (int i = 1; i < compressed.Length; i++)
        {
            List<byte> entry = dictionary.TryGetValue(compressed[i], out List<byte>? value)
                ? new(value)
                : new(window) { window[0] };

            output.AddRange(entry);
            dictionary[dictionary.Count] = [.. window, entry[0]];
            window = entry;
        }

        return [.. output];
    }

    private static int[] GetInts(byte[] values)
    {
        int[] result = new int[values.Length / 4];
        Buffer.BlockCopy(values, 0, result, 0, values.Length);
        return result;
    }

    /// <summary>
    ///   Compresses a byte array using LZW.
    /// </summary>
    public static byte[] Compress(byte[] input)
    {
        Dictionary<List<byte>, int> dictionary = [];
        for (int i = 0; i < 256; i++)
            dictionary[[(byte)i]] = i;

        List<int> compressed = [];
        List<byte> window = [];

        foreach (byte b in input)
        {
            List<byte> windowChain = [.. window, b];
            if (dictionary.ContainsKey(windowChain))
            {
                window.Clear();
                window.AddRange(windowChain);
            }
            else
            {
                compressed.Add(dictionary[window]);
                dictionary[windowChain] = dictionary.Count;
                window.Clear();
                window.Add(b);
            }
        }

        if (window.Count > 0)
            compressed.Add(dictionary[window]);

        return GetBytes([.. compressed]);
    }

    private static byte[] GetBytes(int[] values)
    {
        byte[] result = new byte[values.Length * 4];
        Buffer.BlockCopy(values, 0, result, 0, result.Length);
        return result;
    }
}
