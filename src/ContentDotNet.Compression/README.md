# ContentDotNet.Compression
This tiny library (10.5KB when compiled) can be used to perform compression (LZW/LZ77)
and hashing (Adler32, CRC32, CRC64, XXHash128) functions.

It is typically meant to be a shared library across formats and codecs that need to adapt
compression techniques. For example, PNG files are compressed with DEFLATE, so this component
is here to provide compression for that.

# How to use
- Compute Adler32 checksum:
```cs
Span<int> data = stackalloc int[16];
for (int i = 0; i < data.Length; i++)
    data[i] = Math.Pow(i, 2);
Console.WriteLine(Adler32.ComputeChecksum(data));

// or with stream
var ms = new MemoryStream(new byte[16]);
ms.Position = 0;
for (int i = 0; i < 16; i++)
    ms.WriteByte(Math.Pow(i, 2));
ms.Position = 0;
Console.WriteLine(Adler32.ComputeChecksum(ms));
```
- Compute CRC32 checksum:
```cs
Console.WriteLine(Crc32.ComputeChecksum(data));

Console.WriteLine(Crc32.ComputeChecksum(ms));
```
- Compute CRC64 checksum:
```cs
Console.WriteLine(Crc64.ComputeChecksum(data));

Console.WriteLine(Crc64.ComputeChecksum(ms));
```
- Compute XXHash128 checksum:
```cs
Console.WriteLine(XxHash128.ComputeChecksum(data));

Console.WriteLine(XxHash128.ComputeChecksum(ms));
```

### Compression
- Compute LZ77:
```cs
Console.WriteLine(Lz77.Compress(...));
Console.WriteLine(Lz77.Decompress(...));
```
- Compute LZW:
```cs
Console.WriteLine(Lzw.Compress(...));
Console.WriteLine(Lzw.Decompress(...));
```
