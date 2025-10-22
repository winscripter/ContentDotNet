# Context Index Model
In the H.264 Binarization clause (9.3.2), there is a table that maps syntax elements to:
- maxBinIdxCtx
- ctxIdxOffset
- bypassFlag

To obtain those variables, use this method:
```cs
ContentDotNet.Extensions.Video.H264.Components.IO.Cabac
	.H264BaseCtxIdxAssignments.GetParserWithCtxIdx(H264SyntaxElement se, int ctxBlockCat, bool isFrameMacroblock, H264SliceType sliceType)
```
where:
- se is the syntax element being parsed
- ctxBlockCat is only necessary for coeff_abs_level_minus1, last_significant_coeff_flag and significant_coeff_flag. If so, see ctx-block-cat.md. Otherwise, pass 0.
- isFrameMacroblock, when true, specifies that the current macroblock is a Frame macroblock
- sliceType is the type of the current slice, specified by the slice_type syntax element in the Slice Header

This returns a ContextIndexAndParser.

# What's ContextIndexAndParser?
It is:
- `UnprocessedContextIndexRecord Record` -> contains maxBinIdxCtx, ctxIdxOffset and bypassFlag
- `FireParserCallback Callback` -> when invoked, performs binarization

`UnprocessedContextIndexRecord` is:
- `IContextIndexValue MaxBinIdxCtx` - the value `maxBinIdxCtx`
- `IContextIndexValue CtxIdxOffset` - the value `ctxIdxOffset`

`IContextIndexValue` contains a property `UsesDecodeBypass` which assigns bypassFlag to the arithmetic decoder, as well as `HasSuffix` which, when false, indicates that there is no suffix value for the specified affix value.

`IContextIndexValue` can be:
- `ContextIndexIntegerValue` - when the value consists of just the integer value as the ctxIdxOffset or maxBinIdxCtx
- `ContextIndexAffixValue` - which is when the value is composed of prefix/suffix values

`ContextIndexIntegerValue` contains the property `Value` of type `int`, and `ContextIndexAffixValue` consists of `int Prefix` and `int Suffix`.

`FireParserCallback` is:
```
public delegate int FireParserCallback(IH264CabacDecoder decoder, UnprocessedContextIndexRecord rec, H264SliceType sliceType);
```
where:
- decoder is the input H.264 CABAC decoder for reading bins
- rec is the source UnprocessedContextIndexRecord
- sliceType is the type of the H.264 slice

When this delegate is invoked, it performs binarization as specified [here](binarization.md).
