# Binarization
Inside the `ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.Binarization` namespace, there is a static class called `H264Binarization` that performs parsing of bins into integers.

> [!NOTE]
> This does not obtain the binarization of the syntax element, like maxBinIdxCtx and ctxIdxOffset. That is documented [here](ctxidx-model.md). It just performs the parsing of bins, like U, TU, UEGk,
> mb_type, FL, you name it.

See the following table that maps the types of binarizations to clauses in the H.264 spec and their
respective method names.

| Type of Binarization | Clause in the H.264 Spec | Method Name |
| -------------------- | ------------------------ | ----------- |
| U | 9.3.2.1 | `static int U` |
| TU | 9.3.2.2 | `static int TU` |
| UEGk | 9.3.2.3 | `static int Uegk` |
| FL | 9.3.2.4 | `static int FL` |
| mb_type/sub_mb_type | 9.3.2.5 | `static int MbType` |
| Coded block pattern | 9.3.2.6 | `static int CodedBlockPattern` |
| mb_qp_delta | 9.3.2.7 | `static int MbQpDelta` |
