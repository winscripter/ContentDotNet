# <dfn title="Raw Byte Sequence Payload">RBSP</dfn> <dfn title="Input/Output">I/O</dfn>
<small>Hover on abbreviations to see the unabbreviated form</small>

In the world of H.264, the RBSP (Raw Byte Sequence Payload) is simply important. It's essentially the data of the NAL unit, which contains anything - from critical decoder flow like SPS or PPS picture sets to literal frames like Slice Headers, Slice Data, Macroblocks, and Residuals. This data is read at bit-level, e.g. instead of reading bytes, you read part of that byte. A plain `BitStreamReader` would work, but there's a problem: *the Emulation Prevention 3 Bytes*.

You see, the RBSP contains Emulation Prevention 3 Bytes which must be skipped. Once the decoder sees 0x00 0x00 0x03, it needs to account the first 0x00 0x00 as part of the RBSP, and then disqualify (skip) the 0x03 byte and move on. These are the Emulation Prevention 3 Bytes. Initially, the idea was to start by skipping the Emulation Prevention 3 Bytes as we read the RBSP, and then store the raw RBSP bytes without Emulation Prevention 3 Bytes as a `System.IO.MemoryStream`. This worked, but it was more of an easy approach. In the world of ContentDotNet, we care about performance and memory efficiency, and the way `MemoryStream` works is by storing the values as a `byte` array, which is heap-allocated, thus putting pressure on the Garbage Collector (GC). This isn't a problem, but in performance critical scenarios, we have to find another approach.

That approach? Is the `RbspBitStreamReader`. It's essentially a class that inherits from `BitStreamReader`. Except, it overrides the `ReadBit()` method to account for Emulation Prevention 3 Bytes.

Every time you invoke `ReadBit()`, if the current bit position is 0, it will peek the next 3 bytes to ensure that they're 0x00 0x00 0x03 - if they are, it will store the offset of the 0x03 as a field. Next time when the `ReadBit()` method is invoked, if the current bit position is 0 and the offset is the same as the offset of the 0x03 byte, it will just skip it, thus putting close to no GC memory pressure.

You can use it the same way as `BitStreamReader` since it inherits from it:
```cs
var bitStreamReader = new RbspBitStreamReader(...);
var sps = SequenceParameterSet.Read(bitStreamReader);
// ...
```
