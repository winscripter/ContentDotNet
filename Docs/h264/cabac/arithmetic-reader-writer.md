In this article:
- [Arithmetic reader/writer](#arithmetic-rw)
- [Base namespace](#base-namespace)
- [Reader interface](#reader-interface)
    - [Properties](#reader-interface-properties)
    - [Methods](#reader-interface-methods)
- [Writer interface](#writer-interface)
    - [Properties](#writer-interface-properties)
    - [Methods](#writer-interface-methods)
- [Factories](#factories)
- [Implementations](#implementations)

<a name="arithmetic-rw"></a>
# Arithmetic reader and writer
ContentDotNet's H.264 decoder includes an arithmetic reader that follows the ITU-T H.264's specification, specifically, clause 9.3.3.2 (Arithmetic decoding process) in Rec. ITU-T H.264 V15 2024/08, which you can [download here](https://www.itu.int/rec/T-REC-H.264-202408-I/en) (page 298 out of 854).

<a name="base-namespace"></a>
# Base namespace
`ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ArithmeticEngine`

<a name="reader-interface"></a>
# Reader interface
The `IH264ArithmeticReader` interface in the base namespace implements an arithmetic decoding engine. It depends on `ContentDotNet.BitStream.BitStreamReader` to read source bits off of the bitstream.

```
/// <summary>
///   The arithmetic reader.
/// </summary>
public interface IH264ArithmeticReader
{
    ...
}
```

<a name="reader-interface-properties"></a>
## Properties
```cs
BitStreamReader Reader { get; }
```
This property defines the source `BitStreamReader` (inside the namespace `ContentDotNet.BitStream`). From this bitstream reader,
bits necessary for the arithmetic decoder are read from the bitstream.

> [!NOTE]
> Bits that are read from the bitstream do not include the initial offset value (a.k.a. `codIOffset`). Those are read manually by the calling code during slice data initialization.

```
IBinTracker BinTracker { get; }
```
This is the bin tracker, a simple tool used by the arithmetic reader and writer to track recent bins. It's highly efficient, as the only thing it records are
the previous 32 bins. See [Bin Tracker](#bin-tracker) for more information.

```cs
int Range { get; set; }
int Offset { get; set; }
```
These two are the internal arithmetic reader's registers. Range defines `codIRange`, and Offset defines `codIOffset`. They can be
changed any time, both internally and externally. See the H.264 spec for more information.

> [!CAUTION]
> Do not change these registers externally unless you know what you're doing. Incorrectly changing these registers is almost certainly guaranteed to corrupt the decoding process and/or cause undefined behavior.

<a name="reader-interface-methods"></a>
## Methods
```cs
bool ReadBin(ArithmeticBinType binType, H264ContextVariable? contextVariable);
Task<bool> ReadBinAsync(ArithmeticBinType binType, H264ContextVariable? contextVariable);
```
These two are the logic for reading bins. They read 3 types of bins - Decision, Bypass, and Terminate. The
type of bin to read is controlled by the `binType` parameter, which represents the `ArithmeticBinType` enum as such:
```cs
enum ArithmeticBinType
{
    Decision,    // <-- for decision bin type
    Termination, // <-- for terminate bin type
    Bypass       // <-- for bypass bin type
}
```
The `H264ContextVariable` class is optional and only necessary when reading a decision bin. It's defined [here](common.md#context-variable).

`ReadBinAsync` is the asynchronous verison of `ReadBin`.

```cs
bool ReadBin(int ctxIdx, bool bypassFlag, H264ContextVariable? contextVariable);
Task<bool> ReadBinAsync(int ctxIdx, bool bypassFlag, H264ContextVariable? contextVariable);
```
Compared to the previous `ReadBin` overloads, these ones are more automatic. While the previous overloads
manually parse bins requiring you to manually define the type of bin to parse, this overload will automatically
choose the bin type to parse under the following conditions.
- If `ctxIdx` is equal to 276, read termination,
- If `bypassFlag` is 1, read bypass,
- Otherwise, read decision.

`ctxIdx` is the index of the context variable, while `bypassFlag` is a flag that indicates if forcing bypass decoding is necessary. These variables make more sense if one is aware of the H.264 CABAC decoding process (i.e., has read or implemented the spec).

The `H264ContextVariable` class is optional and only necessary when reading a decision bin. It's defined [here](common.md#context-variable).

`ReadBinAsync` is the asynchronous version of `ReadBin`.


