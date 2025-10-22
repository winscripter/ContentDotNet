# Bitstream
ContentDotNet comes with a built-in bit-stream implementation.

# Overview
All types and methods here are located in the `ContentDotNet.BitStream` namespace under the `ContentDotNet` assembly.

# Reader
The `BitStreamReader` class implements a bit-stream reader that connects to a `Stream` to read bytes.

```cs
Stream stream = ...;
var bsr = new BitStreamReader(stream);
```

The `ReadBit()` method reads a single boolean.
```cs
bool firstBit = bsr.ReadBit();
```
`ReadBitAsync()` is the asynchronous version.

The `ReadBits(uint)` method reads the specified bits.
```cs
uint nextFiveBits = bsr.ReadBits(5);
```
`ReadBitsAsync(uint)` is the asynchronous version.

The `ReadUE()` method reads a single Unsigned Exponential Golomb (UE) value, typically used in H.264.
```cs
uint ueGolomb = bsr.ReadUE();
```
`ReadUEAsync()` is the asynchronous version.

The `ReadSE()` method reads a single Signed Exponential Golomb (SE) value, typically used in H.264.
```cs
int seGolomb = bsr.ReadSE();
```
`ReadSEAsync()` is the asynchronous version.

The `GetState()` method returns the state of the bit-stream reader with values like `ByteOffset`, `BitPosition`, and `CurrentByte`. The `GoTo(ReaderState)` method can be used to change the internal bit-stream state into the specified one, that is, obtained with the `GetState()` method.

# Writer
The `BitStreamWriter` class implements a bit-stream writer that connects to a `Stream` to write bytes.

```cs
Stream stream = ...;
var bsw = new BitStreamWriter(stream);
```

The `WriteBit()` method writes a single boolean.
```cs
bsw.WriteBit(true);
```
`WriteBitAsync()` is the asynchronous version.

The `WriteBits()` method writes the specified bits.
```cs
bsw.WriteBits(20, 5); // 5 bits, value 20
```
`WriteBitsAsync()` is the asynchronous version.

The `WriteUE()` method writes a single Unsigned Exponential Golomb (UE) value, typically used in H.264.
```cs
bsw.WriteUE(25);
```
`WriteUEAsync()` is the asynchronous version.

The `WriteSE()` method writes a single Signed Exponential Golomb (SE) value, typically used in H.264.
```cs
bsw.WriteSE(25);
```
`WriteSEAsync()` is the asynchronous version.

The `GetState()` method returns the state of the bit-stream writer with values like `ByteOffset`, `BitPosition`, and `CurrentByte`. The `GoTo(WriterState)` method can be used to change the internal bit-stream state into the specified one, that is, obtained with the `GetState()` method.
