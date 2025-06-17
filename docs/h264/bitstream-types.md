# Bitstream types
These types provide raw H.264 data from the bitstream, with XML documentation, as well as methods to read them from the bitstream or write them from the bitstream either asynchronously or synchronously, both of which **may** require dependencies to other bitstream types or syntax elements. They also provide methods to compare them with other bitstream types of same kind, compute the hash code, equality operators (`==` and `!=`), and also inherit from the `System.IEquatable<T>` namespace.

There is a number of them, all of which are documented in this article.
They're all located under the `ContentDotNet.Extensions.H264.Models` namespace.

They're all value types.

To provide best performance, raw values are provided as public fields instead of properties. In C#, properties consist of several layers: there's always a generated field containing the property value (called a backing field), a property references the get/set method, and for the get/set accessor, a hidden method is generated to retrieve (get) or alter (set) the field. This can make things slow - H.264 is a performance-critical scenario, so we have to make raw values public fields as an optimization method.

# NAL units

### NalUnit
This structure represents the NAL unit. However, it's important to note that it doesn't actually store the RBSP data. **Instead, it represents the offset to the RBSP in the bitstream**, of type `ReaderState`. See also [RbspBitStreamReader](rbspIO.md).

It also contains the `INalUnitHeaderExtension` nullable interface in case the NAL unit contains an MVC, 3DAVC, or SVC extension.

```cs
public struct NalUnit : IEquatable<NalUnit>
```

An RBSP reader can be obtained as follows:
```cs
static BitStreamReader GetRbspReader(NalUnit nal, BitStreamReader originalReader)
{
    var backing = new BitStreamReader(originalReader.BaseStream);
    backing.GoTo(nal.Rbsp);
    var rbsp = new RbspBitStreamReader(backing);
    return rbsp; // Now in the RBSP part
}
```

### INalUnitHeaderExtension
```cs
public interface INalUnitHeaderExtension
```
This is a marker interface for three below structures.

#### Avc3DNalUnitHeaderExtension
This structure inherits from `INalUnitHeaderExtension` and is a NAL Unit Header extension for 3DAVC (a.k.a. MVCD or Multiview With Depth Coding).

#### MvcNalUnitHeaderExtension
This structure inherits from `INalUnitHeaderExtension` and is a NAL Unit Header extension for MVC (a.k.a. Multiview Video Coding).

#### SvcNalUnitHeaderExtension
This structure inherits from `INalUnitHeaderExtension` and is a NAL Unit Header extension for SVC (a.k.a. Scalable Video Coding).

### Parameter Sets
These two structures described below are part of Parameter Sets, specifically SPS and PPS.

#### SequenceParameterSet
This structure represents the Sequence Parameter Set from H.264, + methods to read/write it. Values are provided raw.

```cs
public struct SequenceParameterSet : IEquatable<SequenceParameterSet>
```

The method `Read` does not require dependencies. However, the `Write` and `WriteAsync` methods may require a scaling matrix builder to be provided if one needs to support writing scaling matrices in the SPS.

#### PictureParameterSet
This structure represents the Picture Parameter Set (PPS).

```cs
public struct PictureParameterSet : IEquatable<SequenceParameterSet>
```

The method `Read` requires a dependency to the last `SequenceParameterSet` ever decoded.

### Slice Header and related structures
The five below structures represent the Slice Header, Reference Picture List / Reference Picture List MVC Modification, Decoder Reference Picture Marking, and Prediction Weight Table.

#### SliceHeader
Represents the Slice Header.

```cs
public struct SliceHeader : IEquatable<SliceHeader>
```

The methods `Read`, `Write` and `WriteAsync` require a reference to the current NAL unit, and the last decoded SPS and PPS.

#### Decoder Reference Picture Markings

##### DecRefPicMarking
Represents the Decoder Reference Picture Marking.

```cs
public struct DecRefPicMarking : IEquatable<DecRefPicMarking>
```

The methods `Read`, `Write` and `WriteAsync` require the symbol `IdrPicFlag`, which is obtained with the method `H264Extensions.IsIdr(NalUnit)`.

The decoder reference picture marking consists of up to 32 `DecRefPicMarkingEntry` structures.

##### DecRefPicMarkingEntry
A single entry of the Decoder Reference Picture Marking.

```cs
public struct DecRefPicMarkingEntry : IEquatable<DecRefPicMarkingEntry>
```

#### PredWeightTable
Represents the prediction weight table, often used for the Weighted Prediction process.

```cs
public struct PredWeightTable : IEquatable<PredWeightTable>
```

The methods `Read`, `Write` and `WriteAsync` require references to:
- `ChromaArrayType`, see `H264Extensions.GetChromaArrayType(SequenceParameterSet)`, with the passed SPS being the last decoded one
- `slice_type`, from the Slice Header
- `num_ref_idx_LX_active_minus1` with LX being replaced by L0 or L1, obtained from the last decoded Picture Parameter Set

#### Reference picture list modifications

##### RefPicListModification
Represents the Reference Picture List Modification.

```cs
public struct RefPicListModification : IEquatable<RefPicListModification>
```

The methods `Read`, `Write` and `WriteAsync` require a reference to `slice_num`, a field in the Slice Header.

##### RefPicListModificationEntry
A single entry in Reference Picture List Modifications.

```cs
public struct RefPicListModificationEntry : IEquatable<RefPicListModificationEntry>
```

#### Reference picture list modifications (MVC)

##### RefPicListMvcModification
Represents the Reference Picture List Modification for Multiview Video Coding (MVC).

```cs
public struct RefPicListMvcModificationList : IEquatable<RefPicListMvcModificationList>
```

##### RefPicListMvcModificationEntry
A single entry of the Reference Picture List Modification for Multiview Video Coding (MVC).

```cs
public struct RefPicListMvcModificationEntry : IEquatable<RefPicListMvcModificationEntry>
```

### Macroblocks and predictions

#### MacroblockPrediction
This structure represents the Macroblock Prediction, with values like Motion Vector Difference and Reference Picture Index. It supports both entropy codings - CAVLC and CABAC.

```cs
public struct MacroblockPrediction : IEquatable<MacroblockPrediction>
```

The `Read`, `Write` and `WriteAsync` methods require:
- `mb_type` from the Macroblock
- `MbaffFrameFlag`, obtained from `H264Extensions.GetMbaffFrameFlag(SliceHeader, SequenceParameterSet)`
- `EntropyCodingMode`, an enum version of `entropy_coding_mode_flag` from the PPS
- `GeneralSliceType`, see `H264Extensions.GetSliceType(SliceHeader)`
- `transform_size_8x8_flag` from the Macroblock
- `num_ref_idx_lx_active_minus1` with lx replaced by L0 or L1, from the PPS
- `mb_field_decoding_flag` from the Macroblock
- `field_pic_flag` from the Slice Header
- `ChromaArrayType`, see `H264Extensions.GetChromaArrayType(SequenceParameterSet)`

The `Read` method may also require `CabacManager` when the entropy coding mode is CABAC, in order to read CABAC syntax elements.

#### SubMacroblockPrediction
Prediction information for sub-macroblocks.

```cs
public struct SubMacroblockPrediction : IEquatable<SubMacroblockPrediction>
```

The `Read`, `Write` and `WriteAsync` methods require:
- `mb_type` from the Macroblock
- `MbaffFrameFlag`, obtained from `H264Extensions.GetMbaffFrameFlag(SliceHeader, SequenceParameterSet)`
- `EntropyCodingMode`, an enum version of `entropy_coding_mode_flag` from the PPS
- `GeneralSliceType`, see `H264Extensions.GetSliceType(SliceHeader)`
- `transform_size_8x8_flag` from the Macroblock
- `num_ref_idx_lx_active_minus1` with lx replaced by L0 or L1, from the PPS
- `mb_field_decoding_flag` from the Macroblock
- `field_pic_flag` from the Slice Header

The `Read` method may also require `CabacManager` when the entropy coding mode is CABAC, in order to read CABAC syntax elements.

#### MacroblockLayer
The actual macroblock, which also supports residuals, PCM macroblocks, and prediction information.

```cs
public struct MacroblockLayer : IEquatable<MacroblockLayer>
```
