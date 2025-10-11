In this article:
- [The slice decoder](#the-slice-decoder)
- [Base namespace](#base-namespace)
- [The ISliceDecoder interface](#islicedecoder)
    - [Methods](#isd-methods)
- [The H264PictureOrderCount struct](#pocstruct)
- [Factories](#factories)
    - [Default implementation](#default-impl)

<a name="the-slice-decoder"></a>
# The Slice Decoder
The Slice Decoder does not necessarily fully parse an H.264 slice. Instead, it implements the clause 8.2
in the [Rec. ITU-T H.264 V15 2024/08 spec](https://www.itu.int/rec/T-REC-H.264-202408-I/en) (Slice Decoding Process). The Slice
Decoding Process primarily includes derivation of Picture Order Counts (POCs) for use in the Decoded Picture Buffer (DPB)
and the implementation of the `NextMbAddress` function used to get the next macroblock address following the specified one.

ContentDotNet includes:
- the `ISliceDecoder` interface (documented in this article)
- the factories for slice decoders
- the actual implementation of `ISliceDecoder`

<a name="base-namespace"></a>
# Base namespace
`ContentDotNet.Extensions.Video.H264.Components.SliceDecoding`

<a name="islicedecoder"></a>
# The ISliceDecoder interface
The actual abstraction of Clause 8.2 in the ITU-T H.264 spec. See methods below.

```cs
public interface ISliceDecoder
{
    ...
}
```

<a name="isd-methods"></a>
## Methods
```cs
int PictureOrderCount(PictureDescriptor picX, PictureDescriptor prevPic, int frameNumOffsetOfPrevFrameIfPocTypeIs1);
```
This implements clause 8.2.1 (Decoding process for picture order count). It returns the Picture Order Count (POC) for the specified picture.

**picX** is the picture to get the POC of.

**prevPic** is the picture before **picX** in decoding order.

**frameNumOffsetOfPrevFrameIfPocTypeIs1** specifies FrameNumOffset of **prevPic**. It is used if **picture_order_count_type** is equal to 1.

```cs
int DiffPicOrderCnt(int pocA, int pocB);
```
This returns the difference between two picture order counts, **pocA** and **pocB**. Although, the method is just as simple as follows:
`pocA - pocB`. It is still a method part of the interface so custom implementations could use their own logic. For instance, logging for debugging purposes.

```cs
H264PictureOrderCount DerivePictureOrderCounts(PictureDescriptor currPic, PictureDescriptor prevPic, int frameNumOffsetOfPrevFrameIfPocTypeIs1);
```
This is logically the same as `PictureOrderCount`, except that it also returns:
- TopFieldOrderCnt
- BottomFieldOrderCnt
- PicOrderCntMsb

Those are internal variables used in the H.264 spec's clause 8.2.1, but could also be used for debugging, logging, or custom logic.

All parameters share the same purpose as the `PictureOrderCount` method.

```cs
void PopulateWithMapUnitToSliceGroupMap(H264State h264, IList<int> mapUnitToSliceGroupMap);
```
This implements clause 8.2.2 of the ITU-T H.264 spec. It is used to take the current H.264 state and generate
an array of map unit to slice group map. The map unit to slice group maps are added to `mapUnitToSliceGroupMap` with the `IList{T}.Add(T)` method.

**h264** represents the current H.264 state.

**mapUnitToSliceGroupMap** is where the map units to slice group maps are added, with the `IList{T}.Add(T)` method. Unless one would like to include other custom map units to slice group maps, this parameter is usually passed as an empty list and gets populated with elements by this method automatically.

```cs
void ConvertMapUnitToSliceGroupMapToMacroblockToSliceGroupMap(H264State h264, IList<int> mapUnitToSliceGroupMap, IList<int> macroblockToSliceGroupMap);
```
This takes the input map units to slice group maps, and populates the `macroblockToSliceGroupMap` with map unit to slice group maps converted into macroblock to slice group maps.

**h264** represents the current H.264 state.

**mapUnitToSliceGroupMap** are map units to slice group maps, which are created with the `PopulateWithMapUnitToSliceGroupMap` method.

**macroblockToSliceGroupMap** is where the macroblock to slice group maps are added, with the `IList{T}.Add(T)` method. Unless one would like to include other custom macroblock to slice group maps, this parameter is usually passed as an empty list and gets populated with elements by this method automatically.

```cs
int NextMbAddress(H264State h264, IList<int> mbToSliceGroupMap, int n);
```
Given the current H.264 state, macroblock to slice group map, and the current macroblock address, computes the address of the macroblock following address `n` in the current slice.

**h264** represents the current H.264 state.

**mbToSliceGroupMap** is created using the `ConvertMapUnitToSliceGroupMapToMacroblockToSliceGroupMap` method.

**n** is the macroblock address to compute the preceding macroblock address of. This is typically the current macroblock address in an H.264 slice.

<a name="pocstruct"></a>
# The H264PictureOrderCount struct

Located in the `ContentDotNet.Extensions.Video.H264.Models` namespace, this is a simple, stack-allocated, vector-accelerated structure that stores:
- the picture order count
- the top field picture order count
- the bottom field picture order count
- the picture order count's MSB

```cs
public readonly struct H264PictureOrderCount
{
    ...
}
```

## Constructor
```cs
public H264PictureOrderCount(int poc, int top, int bottom, int msb)
```
This initializes the structure.

**poc** is the picture order count.

**top** is the TopFieldOrderCnt variable.

**bottom** is the BottomFieldOrderCnt variable.

**msb** is the PicOrderCntMsb variable.

## Properties
```cs
int PictureOrderCount { get; }
```
This is the picture order count.

```cs
int Top { get; }
```
This is TopFieldOrderCnt.

```cs
int Bottom { get; }
```
This is BottomFieldOrderCnt.


```cs
int Msb { get; }
```
This is PicOrderCntMsb.

<a name="factories"></a>
# Factories
The `ISliceDecoderFactory` interface can be used to create any implementation of the `ISliceDecoder` interface.

```cs
public interface ISliceDecoderFactory
{
    ...
}
```

Inside, there's one method:
```cs
ISliceDecoder CreateSliceDecoder();
```
This creates any or custom implementation of the `ISliceDecoder` interface.

<a name="default-impl"></a>
## Default implementation
The `DefaultSliceDecoderFactory` class provides the default `ISliceDecoderFactory` implementation, which creates the default `ISliceDecoder` interface implementation.

It also has a singleton instance field:
```cs
public class DefaultSliceDecoderFactory : ISliceDecoderFactory
{
    public static readonly DefaultSliceDecoderFactory Instance = new();

    ...
}
```

With this in mind, you can create the `ISliceDecoder` interface like so:
```cs
ISliceDecoder decoder = DefautlSliceDecoderFactory.Instance.CreateSliceDecoder();
// ...
```

> [!NOTE]
> **Good to know:**
>
> The `DefaultSliceDecoderFactory`'s `CreateSliceDecoder()` method will never perform extra allocations. The internal
> implementation of the `ISliceDecoder` interface that `DefaultSliceDecoderFactory` creates for you doesn't have any
> instance fields, and when you use `CreateSliceDecoder()` on `DefaultSliceDecoderFactory`, it uses the singleton instance
> of the internal implementation too.
