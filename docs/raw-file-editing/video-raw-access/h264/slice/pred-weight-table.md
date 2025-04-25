# Prediction Weight Tables
ContentDotNet makes working with prediction weight tables (usually in slice headers) easy.

All types reside in the `ContentDotNet.Extensions.H264.Models` namespace, under the `ContentDotNet.Extensions.H264` package. The main types for working with prediction weight tables are:

1. `PredWeightTable` represents the luma/chroma denominator and L0/L1 prediction weight table lists.
2. `PredWeightTableList` represents a single list (be it L0 or L1) that lets one read/write luma/chroma weights and offsets.
3. `PredWeightTableWeightOffsetEntry` contains the offset and weight pair, as well as the flag to include them
4. `PredWeightTableListWriteOptions` and `MemoryPredWeightTableListWriteOptions` are dedicated structures to ease writing `PredWeightTableList` to bitstreams.

## PredWeightTable
This is a structure representing the prediction weight table.
```cs
public struct PredWeightTable : IEquatable<PredWeightTable>
```

### Fields

```cs
public uint LumaLog2WeightDenom;
```

The Luma log2 weight denominator.

```cs
public uint ChromaLog2WeightDenom;
```

The Chroma log2 weight denominator.

```cs
public PredWeightTableList L0;
```

The list of weights for reference pictures in list 0 (L0). This is always present.

```cs
public PredWeightTableList? L1;
```

The list of weights for reference pictures in list 1 (L1). This is only present if the slice type in the slice header where this prediction weight table resides from, modulo 5, equals to 1. Otherwise, `null`.

### Constructors

This structure also introduces a single constructor:
```cs
public PredWeightTable(uint lumaLog2WeightDenom, uint chromaLog2WeightDenom, PredWeightTableList l0, PredWeightTableList? l1)
{
    LumaLog2WeightDenom = lumaLog2WeightDenom;
    ChromaLog2WeightDenom = chromaLog2WeightDenom;
    L0 = l0;
    L1 = l1;
}
```

### Methods
`System.Object` methods like GetHashCode and Equals are implemented.

```cs
public static PredWeightTable Read(BitStreamReader reader, int chromaArrayType, int sliceType, int numRefIdxL0ActiveMinus1, int numRefIdxL1ActiveMinus1)
```
This parses the prediction weight table from the bitstream (`reader`), given that it is in the position of the prediction weight table. `chromaArrayType` should be taken from the SPS. `sliceType` should be taken from the Slice Header. `numRefIdxL(0/1)ActiveMinus` should be taken from the PPS.

```cs
public readonly void Write(BitStreamWriter writer, int chromaArrayType, int sliceType, PredWeightTableListWriteOptions optionsL0, PredWeightTableListWriteOptions optionsL1)
```
This writes the current prediction weight table back to the bitstream at current location. `chromaArrayType` should be taken from the SPS. `sliceType` should be taken from the Slice Header. `optionsL(0/1)` are options for writing L0 and L1 lists.

```cs
public readonly async Task WriteAsync(BitStreamWriter writer, int chromaArrayType, int sliceType, MemoryPredWeightTableListWriteOptions optionsL0, MemoryPredWeightTableListWriteOptions? optionsL1)
```

This is just like the standard `Write` method, but it's asynchronous. This writes the current prediction weight table back to the bitstream at current location. `chromaArrayType` should be taken from the SPS. `sliceType` should be taken from the Slice Header. `optionsL(0/1)` are options for writing L0 and L1 lists.

## PredWeightTableList
This represents a single list (L0 or L1) in the prediction weight table.

### Fields
```cs
public ReaderState Offset;
```
The offset for the list in the H.264 reader.

```cs
public int Count;
```
Number of elements, minus 1.

### Constructors
```cs
public PredWeightTableList(ReaderState offset, int count)
{
    Offset = offset;
    Count = count;
}
```

### Methods
```cs
public readonly (PredWeightTableWeightOffsetEntry luma, PredWeightTableWeightOffsetEntry chroma1, PredWeightTableWeightOffsetEntry chroma2) GetElement(BitStreamReader originalReader, int index, int chromaArrayType)
```
Gets the prediction weight/offset element at given index. `originalReader` is the bitstream reader for the same H.264 stream as the one used to parse the prediction weight table list. `index` is the zero-based index of the weight/offset you want to get. `chromaArrayType` should be taken from the SPS.

```cs
public static PredWeightTableList Read(BitStreamReader reader, int chromaArrayType, int numRefIdxLActiveMinus1)
```
Reads the prediction weight table list from the given bit stream, given that the prediction weight table list is in the current position. `chromaArrayType` should be taken from the SPS. `numRefIdxLActiveMinus1` could represent ``numRefIdxL0ActiveMinus1` or `numRefIdxL1ActiveMinus1`, depending on which list is being read. Both should be taken from the PPS.

```cs
public static void Write(
    BitStreamWriter writer,
    int chromaArrayType,
    int numRefIdxLActiveMinus1,
    ReadOnlySpan<bool> includeLuma,
    ReadOnlySpan<bool> includeChroma,
    ReadOnlySpan<PredWeightTableWeightOffsetEntry> luma,
    ReadOnlySpan<(PredWeightTableWeightOffsetEntry cb, PredWeightTableWeightOffsetEntry cr)> chroma)
```
Creates the prediction weight table list at the current position in the bitstream. `chromaArrayType` should be taken from the SPS. `numRefIdxLActiveMinus1` could represent `numRefIdxL0ActiveMinus1` or `numRefIdxL1ActiveMinus1`, depending on which list is being read. Both should be taken from the PPS. `includeLuma` represents booleans which dictate whether or not should weight/offset entry from the `luma` parameter be included (e.g., if `includeLuma[i]` is `false`, a 0 bit is written; otherwise, a 1 bit is written and `luma[i]` is written afterwards). Same with `includeChroma` and `chroma`.

```cs
public static void Write(
    BitStreamWriter writer,
    int chromaArrayType,
    int numRefIdxL0ActiveMinus1,
    PredWeightTableListWriteOptions options)
```
This is a wrapper around the previous `Write` method.

```cs
public static async Task WriteAsync(
    BitStreamWriter writer,
    int chromaArrayType,
    int numRefIdxL0ActiveMinus1,
    ReadOnlyMemory<bool> includeLuma,
    ReadOnlyMemory<bool> includeChroma,
    ReadOnlyMemory<PredWeightTableWeightOffsetEntry> luma,
    ReadOnlyMemory<(PredWeightTableWeightOffsetEntry cb, PredWeightTableWeightOffsetEntry cr)> chroma)
```
This is just like the `Write` method, except it's asynchronous. Creates the prediction weight table list at the current position in the bitstream. `chromaArrayType` should be taken from the SPS. `numRefIdxLActiveMinus1` could represent `numRefIdxL0ActiveMinus1` or `numRefIdxL1ActiveMinus1`, depending on which list is being read. Both should be taken from the PPS. `includeLuma` represents booleans which dictate whether or not should weight/offset entry from the `luma` parameter be included (e.g., if `includeLuma[i]` is `false`, a 0 bit is written; otherwise, a 1 bit is written and `luma[i]` is written afterwards). Same with `includeChroma` and `chroma`.

```cs
public static async Task WriteAsync(
    BitStreamWriter writer,
    int chromaArrayType,
    int numRefIdxL0ActiveMinus1,
    MemoryPredWeightTableListWriteOptions options)
```
This is a wrapper around the previous `WriteAsync` method.

## PredWeightTableWeightOffsetEntry
This is just a pair of the weight and offset, as well as the flag that dictates whether it should be written.

### Fields
```cs
public bool Flag;
```
Represents whether should the weight and offset be applied.

```cs
public int Weight;
```
The weight component.

```cs
public int Offset;
```
The offset component.

### Constructors
```cs
public PredWeightTableWeightOffsetEntry(bool flag, int weight, int offset)
{
    Flag = flag;
    Weight = weight;
    Offset = offset;
}
```

### Methods
```cs
public static PredWeightTableWeightOffsetEntry Read(BitStreamReader reader)
```
Reads the weight/offset entry and the flag.

```cs
public readonly void Write(BitStreamWriter writer)
```
Writes the weight/offset entry and the flag.

```cs
public readonly async Task WriteAsync(BitStreamWriter writer)
```
Asynchronously writes the weight/offset entry and the flag.
