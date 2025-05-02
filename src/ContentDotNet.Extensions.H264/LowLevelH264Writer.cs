using ContentDotNet.Extensions.H264.Models;

namespace ContentDotNet.Extensions.H264;

/// <summary>
///   A low level H.264 writer writes parts of H.264 directly.
/// </summary>
public sealed class LowLevelH264Writer : ILowLevelH264Writer
{
    private readonly BitStreamWriter _bitStream;

    /// <summary>
    ///   Initializes a new instance of the <see cref="LowLevelH264Writer"/> class.
    /// </summary>
    /// <param name="bitStream">Bit stream writer</param>
    public LowLevelH264Writer(BitStreamWriter bitStream)
    {
        _bitStream = bitStream;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="LowLevelH264Writer"/> class.
    /// </summary>
    /// <param name="stream">Output stream</param>
    public LowLevelH264Writer(Stream stream)
        : this(new BitStreamWriter(stream))
    {
    }

    /// <summary>
    ///   The stream where all H.264 magic is written to.
    /// </summary>
    public Stream BaseStream => _bitStream.BaseStream;

    /// <summary>
    ///   Everything is written to this bitstream writer.
    /// </summary>
    public BitStreamWriter? BitStreamWriter => _bitStream;

    /// <summary>
    ///   Is this bitstream based? (yes it is)
    /// </summary>
    public bool IsBitStreamBased => true;

    /// <inheritdoc />
    public void Dispose()
    {
        this._bitStream.Dispose();
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc />
    public void WriteDecRefPicMarking(DecRefPicMarking marking, bool idrPicFlag, Span<DecRefPicMarkingEntry> entries)
    {
        marking.Write(this._bitStream, idrPicFlag, entries);
    }

    /// <inheritdoc />
    public void WriteDecRefPicMarking(DecRefPicMarking marking, bool idrPicFlag, Memory<DecRefPicMarkingEntry> entries)
    {
        marking.Write(this._bitStream, idrPicFlag, entries.Span);
    }

    /// <inheritdoc />
    public async Task WriteDecRefPicMarkingAsync(DecRefPicMarking marking, bool idrPicFlag, Memory<DecRefPicMarkingEntry> entries)
    {
        await marking.WriteAsync(this._bitStream, idrPicFlag, entries);
    }

    /// <inheritdoc />
    public void WriteHrdParameters(HrdParameters hrd, ReadOnlySpan<uint> bitRateValueMinus1, ReadOnlySpan<uint> cpbSizeValueMinus1, ReadOnlySpan<bool> cbrFlag)
    {
        hrd.Write(_bitStream, bitRateValueMinus1, cpbSizeValueMinus1, cbrFlag);
    }

    /// <inheritdoc />
    public void WriteHrdParameters(HrdParameters hrd, ReadOnlySpan<uint> bitRateValueMinus1, ReadOnlySpan<uint> cpbSizeValueMinus1, ReadOnlyMemory<bool> cbrFlag)
    {
        WriteHrdParameters(hrd, bitRateValueMinus1, cpbSizeValueMinus1, cbrFlag.Span);
    }

    /// <inheritdoc />
    public void WriteHrdParameters(HrdParameters hrd, ReadOnlySpan<uint> bitRateValueMinus1, ReadOnlyMemory<uint> cpbSizeValueMinus1, ReadOnlySpan<bool> cbrFlag)
    {
        WriteHrdParameters(hrd, bitRateValueMinus1, cpbSizeValueMinus1.Span, cbrFlag);
    }

    /// <inheritdoc />
    public void WriteHrdParameters(HrdParameters hrd, ReadOnlySpan<uint> bitRateValueMinus1, ReadOnlyMemory<uint> cpbSizeValueMinus1, ReadOnlyMemory<bool> cbrFlag)
    {
        WriteHrdParameters(hrd, bitRateValueMinus1, cpbSizeValueMinus1.Span, cbrFlag.Span);
    }

    /// <inheritdoc />
    public void WriteHrdParameters(HrdParameters hrd, ReadOnlyMemory<uint> bitRateValueMinus1, ReadOnlySpan<uint> cpbSizeValueMinus1, ReadOnlySpan<bool> cbrFlag)
    {
        WriteHrdParameters(hrd, bitRateValueMinus1.Span, cpbSizeValueMinus1, cbrFlag);
    }

    /// <inheritdoc />
    public void WriteHrdParameters(HrdParameters hrd, ReadOnlyMemory<uint> bitRateValueMinus1, ReadOnlySpan<uint> cpbSizeValueMinus1, ReadOnlyMemory<bool> cbrFlag)
    {
        WriteHrdParameters(hrd, bitRateValueMinus1.Span, cpbSizeValueMinus1, cbrFlag.Span);
    }

    /// <inheritdoc />
    public void WriteHrdParameters(HrdParameters hrd, ReadOnlyMemory<uint> bitRateValueMinus1, ReadOnlyMemory<uint> cpbSizeValueMinus1, ReadOnlySpan<bool> cbrFlag)
    {
        WriteHrdParameters(hrd, bitRateValueMinus1.Span, cpbSizeValueMinus1.Span, cbrFlag);
    }

    /// <inheritdoc />
    public void WriteHrdParameters(HrdParameters hrd, ReadOnlyMemory<uint> bitRateValueMinus1, ReadOnlyMemory<uint> cpbSizeValueMinus1, ReadOnlyMemory<bool> cbrFlag)
    {
        WriteHrdParameters(hrd, bitRateValueMinus1.Span, cpbSizeValueMinus1.Span, cbrFlag.Span);
    }

    /// <inheritdoc />
    public async Task WriteHrdParametersAsync(HrdParameters hrd, ReadOnlyMemory<uint> bitRateValueMinus1, ReadOnlyMemory<uint> cpbSizeValueMinus1, ReadOnlyMemory<bool> cbrFlag)
    {
        await hrd.WriteAsync(this._bitStream, bitRateValueMinus1, cpbSizeValueMinus1, cbrFlag);
    }

    /// <inheritdoc />
    public void WriteNalUnit(NalUnit nalu) => nalu.Write(this._bitStream);

    /// <inheritdoc />
    public void WriteNalUnit(uint nalUnitType, uint nalRefIdc, INalUnitHeaderExtension? extension = null)
    {
        var nalu = new NalUnit(nalRefIdc, nalUnitType, extension is SvcNalUnitHeaderExtension, extension is Avc3DNalUnitHeaderExtension, extension);
        WriteNalUnit(nalu);
    }

    /// <inheritdoc />
    public async Task WriteNalUnitAsync(NalUnit nalu) => await nalu.WriteAsync(this._bitStream);

    /// <inheritdoc />
    public async Task WriteNalUnitAsync(uint nalUnitType, uint nalRefIdc, INalUnitHeaderExtension? extension = null)
    {
        var nalu = new NalUnit(nalRefIdc, nalUnitType, extension is SvcNalUnitHeaderExtension, extension is Avc3DNalUnitHeaderExtension, extension);
        await WriteNalUnitAsync(nalu);
    }

    /// <inheritdoc />
    public void WriteNalUnitIdr(uint nalRefIdc)
    {
        WriteNalUnit(5, nalRefIdc, null);
    }

    /// <inheritdoc />
    public async Task WriteNalUnitIdrAsync(uint nalRefIdc)
    {
        await WriteNalUnitAsync(5, nalRefIdc, null);
    }

    /// <inheritdoc />
    public void WriteNalUnitNonIdr(uint nalRefIdc)
    {
        WriteNalUnit(1, nalRefIdc, null);
    }

    /// <inheritdoc />
    public async Task WriteNalUnitNonIdrAsync(uint nalRefIdc)
    {
        await WriteNalUnitAsync(1, nalRefIdc, null);
    }

    /// <inheritdoc />
    public void WriteNalUnitPps(uint nalRefIdc)
    {
        WriteNalUnit(8, nalRefIdc, null);
    }

    /// <inheritdoc />
    public async Task WriteNalUnitPpsAsync(uint nalRefIdc)
    {
        await WriteNalUnitAsync(8, nalRefIdc, null);
    }

    /// <inheritdoc />
    public void WriteNalUnitSei(uint nalRefIdc)
    {
        WriteNalUnit(6, nalRefIdc, null);
    }

    /// <inheritdoc />
    public async Task WriteNalUnitSeiAsync(uint nalRefIdc)
    {
        await WriteNalUnitAsync(6, nalRefIdc, null);
    }

    /// <inheritdoc />
    public void WriteNalUnitSps(uint nalRefIdc)
    {
        WriteNalUnit(7, nalRefIdc, null);
    }

    /// <inheritdoc />
    public async Task WriteNalUnitSpsAsync(uint nalRefIdc)
    {
        await WriteNalUnitAsync(7, nalRefIdc, null);
    }

    /// <inheritdoc />
    public void WritePps(PictureParameterSet pps, Span<uint> runLengthMinus1, Span<uint> sliceGroupId, bool moreRbspData, ScalingMatrixBuilder build)
    {
        pps.Write(_bitStream, runLengthMinus1, sliceGroupId, moreRbspData, build);
    }

    /// <inheritdoc />
    public void WritePps(PictureParameterSet pps, Memory<uint> runLengthMinus1, Span<uint> sliceGroupId, bool moreRbspData, ScalingMatrixBuilder build)
    {
        pps.Write(_bitStream, runLengthMinus1.Span, sliceGroupId, moreRbspData, build);
    }

    /// <inheritdoc />
    public void WritePps(PictureParameterSet pps, Span<uint> runLengthMinus1, Memory<uint> sliceGroupId, bool moreRbspData, ScalingMatrixBuilder build)
    {
        pps.Write(_bitStream, runLengthMinus1, sliceGroupId.Span, moreRbspData, build);
    }

    /// <inheritdoc />
    public void WritePps(PictureParameterSet pps, Memory<uint> runLengthMinus1, Memory<uint> sliceGroupId, bool moreRbspData, ScalingMatrixBuilder build)
    {
        pps.Write(_bitStream, runLengthMinus1.Span, sliceGroupId.Span, moreRbspData, build);
    }

    /// <inheritdoc />
    public async Task WritePpsAsync(PictureParameterSet pps, Memory<uint> runLengthMinus1, Memory<uint> sliceGroupId, bool moreRbspData, ScalingMatrixBuilder build)
    {
        await pps.WriteAsync(_bitStream, runLengthMinus1, sliceGroupId, moreRbspData, build);
    }

    /// <inheritdoc />
    public void WritePredWeightTable(PredWeightTable table, int chromaArrayType, int sliceType, PredWeightTableListWriteOptions l0, PredWeightTableListWriteOptions l1)
    {
        table.Write(_bitStream, chromaArrayType, sliceType, l0, l1);
    }

    /// <inheritdoc />
    public void WritePredWeightTable(PredWeightTable table, int chromaArrayType, int sliceType, MemoryPredWeightTableListWriteOptions l0, MemoryPredWeightTableListWriteOptions l1)
    {
        table.Write(_bitStream, chromaArrayType, sliceType, l0, l1);
    }

    /// <inheritdoc />
    public void WritePredWeightTable(PredWeightTable table, int chromaArrayType, int sliceType, ReadOnlySpan<(PredWeightTableWeightOffsetEntry luma, PredWeightTableWeightOffsetEntry chroma1, PredWeightTableWeightOffsetEntry chroma2)> l0, ReadOnlySpan<(PredWeightTableWeightOffsetEntry luma, PredWeightTableWeightOffsetEntry chroma1, PredWeightTableWeightOffsetEntry chroma2)> l1)
    {
        Span<bool> lumaL0Include = stackalloc bool[l0.Length];
        Span<bool> chromaL0Include = stackalloc bool[l1.Length];

        Span<bool> lumaL1Include = stackalloc bool[l0.Length];
        Span<bool> chromaL1Include = stackalloc bool[l1.Length];

        lumaL0Include.Fill(true);
        chromaL0Include.Fill(true);
        lumaL1Include.Fill(true);
        chromaL1Include.Fill(true);

        Span<PredWeightTableWeightOffsetEntry> l0Luma = stackalloc PredWeightTableWeightOffsetEntry[l0.Length];
        Span<(PredWeightTableWeightOffsetEntry cb, PredWeightTableWeightOffsetEntry cr)> l0Chroma = stackalloc (PredWeightTableWeightOffsetEntry cb, PredWeightTableWeightOffsetEntry cr)[l0.Length];

        for (int i = 0; i < l0.Length; i++)
        {
            l0Luma[i] = l0[i].luma;
            l0Chroma[i] = (l0[i].chroma1, l0[i].chroma2);
        }

        Span<PredWeightTableWeightOffsetEntry> l1Luma = stackalloc PredWeightTableWeightOffsetEntry[l1.Length];
        Span<(PredWeightTableWeightOffsetEntry cb, PredWeightTableWeightOffsetEntry cr)> l1Chroma = stackalloc (PredWeightTableWeightOffsetEntry cb, PredWeightTableWeightOffsetEntry cr)[l1.Length];

        for (int i = 0; i < l1.Length; i++)
        {
            l1Luma[i] = l1[i].luma;
            l1Chroma[i] = (l1[i].chroma1, l1[i].chroma2);
        }

        table.Write(
            _bitStream,
            chromaArrayType,
            sliceType,
            new PredWeightTableListWriteOptions(lumaL0Include, chromaL0Include, l0Luma, l0Chroma),
            new PredWeightTableListWriteOptions(lumaL1Include, chromaL1Include, l1Luma, l1Chroma));
    }

    /// <inheritdoc />
    public void WritePredWeightTable(PredWeightTable table, int chromaArrayType, int sliceType, Memory<(PredWeightTableWeightOffsetEntry luma, PredWeightTableWeightOffsetEntry chroma1, PredWeightTableWeightOffsetEntry chroma2)> l0, Memory<(PredWeightTableWeightOffsetEntry luma, PredWeightTableWeightOffsetEntry chroma1, PredWeightTableWeightOffsetEntry chroma2)> l1)
    {
        Memory<bool> lumaL0Include = new(new bool[l0.Length]);
        Memory<bool> chromaL0Include = new(new bool[l1.Length]);

        Memory<bool> lumaL1Include = new(new bool[l0.Length]);
        Memory<bool> chromaL1Include = new(new bool[l1.Length]);

        lumaL0Include.Span.Fill(true);
        chromaL0Include.Span.Fill(true);
        lumaL1Include.Span.Fill(true);
        chromaL1Include.Span.Fill(true);

        Memory<PredWeightTableWeightOffsetEntry> l0Luma = new(new PredWeightTableWeightOffsetEntry[l0.Length]);
        Memory<(PredWeightTableWeightOffsetEntry cb, PredWeightTableWeightOffsetEntry cr)> l0Chroma = new(new (PredWeightTableWeightOffsetEntry cb, PredWeightTableWeightOffsetEntry cr)[l0.Length]);

        for (int i = 0; i < l0.Length; i++)
        {
            l0Luma.Span[i] = l0.Span[i].luma;
            l0Chroma.Span[i] = (l0.Span[i].chroma1, l0.Span[i].chroma2);
        }

        Memory<PredWeightTableWeightOffsetEntry> l1Luma = new(new PredWeightTableWeightOffsetEntry[l1.Length]);
        Memory<(PredWeightTableWeightOffsetEntry cb, PredWeightTableWeightOffsetEntry cr)> l1Chroma = new(new (PredWeightTableWeightOffsetEntry cb, PredWeightTableWeightOffsetEntry cr)[l1.Length]);

        for (int i = 0; i < l1.Length; i++)
        {
            l1Luma.Span[i] = l1.Span[i].luma;
            l1Chroma.Span[i] = (l1.Span[i].chroma1, l1.Span[i].chroma2);
        }

        table.Write(
            _bitStream,
            chromaArrayType,
            sliceType,
            new MemoryPredWeightTableListWriteOptions(lumaL0Include, chromaL0Include, l0Luma, l0Chroma),
            new MemoryPredWeightTableListWriteOptions(lumaL1Include, chromaL1Include, l1Luma, l1Chroma));
    }

    /// <inheritdoc />
    public async Task WritePredWeightTableAsync(PredWeightTable table, int chromaArrayType, int sliceType, Memory<(PredWeightTableWeightOffsetEntry luma, PredWeightTableWeightOffsetEntry chroma1, PredWeightTableWeightOffsetEntry chroma2)> l0, Memory<(PredWeightTableWeightOffsetEntry luma, PredWeightTableWeightOffsetEntry chroma1, PredWeightTableWeightOffsetEntry chroma2)> l1)
    {
        Memory<bool> lumaL0Include = new(new bool[l0.Length]);
        Memory<bool> chromaL0Include = new(new bool[l1.Length]);

        Memory<bool> lumaL1Include = new(new bool[l0.Length]);
        Memory<bool> chromaL1Include = new(new bool[l1.Length]);

        lumaL0Include.Span.Fill(true);
        chromaL0Include.Span.Fill(true);
        lumaL1Include.Span.Fill(true);
        chromaL1Include.Span.Fill(true);

        Memory<PredWeightTableWeightOffsetEntry> l0Luma = new(new PredWeightTableWeightOffsetEntry[l0.Length]);
        Memory<(PredWeightTableWeightOffsetEntry cb, PredWeightTableWeightOffsetEntry cr)> l0Chroma = new(new (PredWeightTableWeightOffsetEntry cb, PredWeightTableWeightOffsetEntry cr)[l0.Length]);

        for (int i = 0; i < l0.Length; i++)
        {
            l0Luma.Span[i] = l0.Span[i].luma;
            l0Chroma.Span[i] = (l0.Span[i].chroma1, l0.Span[i].chroma2);
        }

        Memory<PredWeightTableWeightOffsetEntry> l1Luma = new(new PredWeightTableWeightOffsetEntry[l1.Length]);
        Memory<(PredWeightTableWeightOffsetEntry cb, PredWeightTableWeightOffsetEntry cr)> l1Chroma = new(new (PredWeightTableWeightOffsetEntry cb, PredWeightTableWeightOffsetEntry cr)[l1.Length]);

        for (int i = 0; i < l1.Length; i++)
        {
            l1Luma.Span[i] = l1.Span[i].luma;
            l1Chroma.Span[i] = (l1.Span[i].chroma1, l1.Span[i].chroma2);
        }

        await table.WriteAsync(
            _bitStream,
            chromaArrayType,
            sliceType,
            new MemoryPredWeightTableListWriteOptions(lumaL0Include, chromaL0Include, l0Luma, l0Chroma),
            new MemoryPredWeightTableListWriteOptions(lumaL1Include, chromaL1Include, l1Luma, l1Chroma));
    }

    /// <inheritdoc />
    public async Task WritePredWeightTableAsync(PredWeightTable table, int chromaArrayType, int sliceType, Memory<(PredWeightTableWeightOffsetEntry luma, PredWeightTableWeightOffsetEntry chroma1, PredWeightTableWeightOffsetEntry chroma2)> l0, Memory<(PredWeightTableWeightOffsetEntry luma, PredWeightTableWeightOffsetEntry chroma1, PredWeightTableWeightOffsetEntry chroma2)> l1, Memory<bool> writeL0, Memory<bool> writeL1)
    {
        Memory<PredWeightTableWeightOffsetEntry> l0Luma = new(new PredWeightTableWeightOffsetEntry[l0.Length]);
        Memory<(PredWeightTableWeightOffsetEntry cb, PredWeightTableWeightOffsetEntry cr)> l0Chroma = new(new (PredWeightTableWeightOffsetEntry cb, PredWeightTableWeightOffsetEntry cr)[l0.Length]);

        for (int i = 0; i < l0.Length; i++)
        {
            l0Luma.Span[i] = l0.Span[i].luma;
            l0Chroma.Span[i] = (l0.Span[i].chroma1, l0.Span[i].chroma2);
        }

        Memory<PredWeightTableWeightOffsetEntry> l1Luma = new(new PredWeightTableWeightOffsetEntry[l1.Length]);
        Memory<(PredWeightTableWeightOffsetEntry cb, PredWeightTableWeightOffsetEntry cr)> l1Chroma = new(new (PredWeightTableWeightOffsetEntry cb, PredWeightTableWeightOffsetEntry cr)[l1.Length]);

        for (int i = 0; i < l1.Length; i++)
        {
            l1Luma.Span[i] = l1.Span[i].luma;
            l1Chroma.Span[i] = (l1.Span[i].chroma1, l1.Span[i].chroma2);
        }

        await table.WriteAsync(
            _bitStream,
            chromaArrayType,
            sliceType,
            new MemoryPredWeightTableListWriteOptions(writeL0, writeL0, l0Luma, l0Chroma),
            new MemoryPredWeightTableListWriteOptions(writeL1, writeL1, l1Luma, l1Chroma));
    }

    /// <inheritdoc />
    public void WriteRefPicListModification(RefPicListModification modification, Span<RefPicListModificationEntry> entries, int sliceType)
    {
        modification.Write(_bitStream, entries, (uint)sliceType);
    }

    /// <inheritdoc />
    public void WriteRefPicListModification(RefPicListModification modification, Memory<RefPicListModificationEntry> entries, int sliceType)
    {
        modification.Write(_bitStream, entries.Span, (uint)sliceType);
    }

    /// <inheritdoc />
    public async Task WriteRefPicListModificationAsync(RefPicListModification modification, Memory<RefPicListModificationEntry> entries, int sliceType)
    {
        await modification.WriteAsync(_bitStream, entries, (uint)sliceType);
    }

    /// <inheritdoc />
    public void WriteRefPicListModificationMvc(RefPicListMvcModification mvc, Span<RefPicListMvcModificationEntry> l0, Span<RefPicListMvcModificationEntry> l1, bool includeL0, bool includeL1, int sliceType)
    {
        mvc.Write(_bitStream, sliceType, l0, l1);
    }

    /// <inheritdoc />
    public void WriteRefPicListModificationMvc(RefPicListMvcModification mvc, Memory<RefPicListMvcModificationEntry> l0, Memory<RefPicListMvcModificationEntry> l1, bool includeL0, bool includeL1, int sliceType)
    {
        mvc.Write(_bitStream, sliceType, l0.Span, l1.Span);
    }

    /// <inheritdoc />
    public async Task WriteRefPicListModificationMvcAsync(RefPicListMvcModification mvc, Memory<RefPicListMvcModificationEntry> l0, Memory<RefPicListMvcModificationEntry> l1, bool includeL0, bool includeL1, int sliceType)
    {
        await mvc.WriteAsync(_bitStream, sliceType, l0, l1);
    }

    /// <inheritdoc />
    public void WriteScalingMatrix(ScalingMatrixBuilder builder, int listCount)
    {
        builder.ListCount = listCount;
        for (int i = 0; i < listCount; i++)
        {
            _Core();

            void _Core()
            {
                Span<int> span = stackalloc int[i < 6 ? 16 : 64];
                builder.BuildSink(i, i < 6 ? 16 : 64, span, out bool present);

                _bitStream.WriteBit(present);
                if (present)
                {
                    for (int j = 0; j < listCount; j++)
                    {
                        _bitStream.WriteSE(span[j]);
                    }
                }
            }
        }
    }

    /// <inheritdoc />
    public async Task WriteScalingMatrixAsync(ScalingMatrixBuilder builder, int listCount)
    {
        builder.ListCount = listCount;
        for (int i = 0; i < listCount; i++)
        {
            await _Core();

            async Task _Core()
            {
                Memory<int> span = new(new int[i < 16 ? 16 : 64]);
                builder.BuildSink(i, i < 6 ? 16 : 64, span.Span, out bool present);

                await _bitStream.WriteBitAsync(present);
                if (present)
                {
                    for (int j = 0; j < listCount; j++)
                    {
                        await _bitStream.WriteSEAsync(span.Span[j]);
                    }
                }
            }
        }
    }

    /// <inheritdoc />
    public void WriteSliceHeader(SliceHeader sliceHeader, NalUnit nalu, SequenceParameterSet sps, PictureParameterSet pps, MemorySliceHeaderWriteOptions options)
    {
        sliceHeader.Write(_bitStream, nalu, sps, pps, options);
    }

    /// <inheritdoc />
    public void WriteSliceHeader(SliceHeader sliceHeader, NalUnit nalu, SequenceParameterSet sps, PictureParameterSet pps, SliceHeaderWriteOptions options)
    {
        sliceHeader.Write(_bitStream, nalu, sps, pps, options);
    }

    /// <inheritdoc />
    public async Task WriteSliceHeaderAsync(SliceHeader sliceHeader, NalUnit nalu, SequenceParameterSet sps, PictureParameterSet pps, MemorySliceHeaderWriteOptions options)
    {
        await sliceHeader.WriteAsync(_bitStream, nalu, sps, pps, options);
    }

    /// <inheritdoc />
    public void WriteSps(SequenceParameterSet sps, Span<int> offsetForRefFrames, VuiWriteOptions vuiWriteOptions, ScalingMatrixBuilder? builder)
    {
        sps.Write(_bitStream, offsetForRefFrames, vuiWriteOptions, builder);
    }

    /// <inheritdoc />
    public async Task WriteSpsAsync(SequenceParameterSet sps, Memory<int> offsetForRefFrames, MemoryVuiWriteOptions vuiWriteOptions, ScalingMatrixBuilder? builder)
    {
        await sps.WriteAsync(_bitStream, offsetForRefFrames, vuiWriteOptions, builder);
    }

    /// <inheritdoc />
    public void WriteVuiParameters(VuiParameters vuiParameters, VuiWriteOptions options)
    {
        vuiParameters.Write(_bitStream, options);
    }

    /// <inheritdoc />
    public async Task WriteVuiParametersAsync(VuiParameters vuiParameters, MemoryVuiWriteOptions options)
    {
        await vuiParameters.WriteAsync(_bitStream, options);
    }
}
