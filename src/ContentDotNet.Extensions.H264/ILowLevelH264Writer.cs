using ContentDotNet.Extensions.H264.Models;

namespace ContentDotNet.Extensions.H264;

/// <summary>
///   Low level H.264 writer.
/// </summary>
/// <remarks>
///   Note to implementers: this interface is very likely to change
///   over time.
/// </remarks>
public interface ILowLevelH264Writer : ILowLevelWriter
{
    /// <summary>
    ///   Writes the NAL unit.
    /// </summary>
    /// <param name="nalu">NAL unit</param>
    void WriteNalUnit(NalUnit nalu);

    /// <summary>
    ///   Writes the NAL unit.
    /// </summary>
    /// <param name="nalu">NAL unit</param>
    Task WriteNalUnitAsync(NalUnit nalu);

    /// <summary>
    ///   Writes the Scaling Parameter Set (SPS) to the output bitstream.
    /// </summary>
    /// <param name="sps">SPS to write</param>
    /// <param name="offsetForRefFrames">Offsets for ref frames</param>
    /// <param name="vuiWriteOptions">Write options for VUI, if present.</param>
    /// <param name="builder">Builder for scaling lists, if present.</param>
    void WriteSps(SequenceParameterSet sps, Span<int> offsetForRefFrames, VuiWriteOptions vuiWriteOptions, ScalingMatrixBuilder? builder);

    /// <summary>
    ///   Writes a scaling matrix to the bitstream.
    /// </summary>
    /// <param name="builder">Periodic builder for specific scaling lists.</param>
    /// <param name="listCount">Number of scaling lists in the scaling matrix.</param>
    void WriteScalingMatrix(ScalingMatrixBuilder builder, int listCount);

    /// <summary>
    ///   Writes a scaling matrix to the bitstream.
    /// </summary>
    /// <param name="builder">Periodic builder for specific scaling lists.</param>
    /// <param name="listCount">Number of scaling lists in the scaling matrix.</param>
    Task WriteScalingMatrixAsync(ScalingMatrixBuilder builder, int listCount);

    /// <summary>
    ///   Writes the Picture Parameter Set (PPS) to the bitstream.
    /// </summary>
    /// <param name="pps">The picture parameter set to write</param>
    /// <param name="runLengthMinus1">Run Length values.</param>
    /// <param name="sliceGroupId">Identifiers for slice groups,</param>
    /// <param name="moreRbspData">Is there more RBSP data remaining?</param>
    /// <param name="build">Scaling matrix builder</param>
    void WritePps(PictureParameterSet pps, Span<uint> runLengthMinus1, Span<uint> sliceGroupId, bool moreRbspData, ScalingMatrixBuilder build);

    /// <summary>
    ///   Writes the Picture Parameter Set (PPS) to the bitstream.
    /// </summary>
    /// <param name="pps">The picture parameter set to write</param>
    /// <param name="runLengthMinus1">Run Length values.</param>
    /// <param name="sliceGroupId">Identifiers for slice groups,</param>
    /// <param name="moreRbspData">Is there more RBSP data remaining?</param>
    /// <param name="build">Scaling matrix builder</param>
    void WritePps(PictureParameterSet pps, Memory<uint> runLengthMinus1, Span<uint> sliceGroupId, bool moreRbspData, ScalingMatrixBuilder build);

    /// <summary>
    ///   Writes the Picture Parameter Set (PPS) to the bitstream.
    /// </summary>
    /// <param name="pps">The picture parameter set to write</param>
    /// <param name="runLengthMinus1">Run Length values.</param>
    /// <param name="sliceGroupId">Identifiers for slice groups,</param>
    /// <param name="moreRbspData">Is there more RBSP data remaining?</param>
    /// <param name="build">Scaling matrix builder</param>
    void WritePps(PictureParameterSet pps, Span<uint> runLengthMinus1, Memory<uint> sliceGroupId, bool moreRbspData, ScalingMatrixBuilder build);

    /// <summary>
    ///   Writes the Picture Parameter Set (PPS) to the bitstream.
    /// </summary>
    /// <param name="pps">The picture parameter set to write</param>
    /// <param name="runLengthMinus1">Run Length values.</param>
    /// <param name="sliceGroupId">Identifiers for slice groups,</param>
    /// <param name="moreRbspData">Is there more RBSP data remaining?</param>
    /// <param name="build">Scaling matrix builder</param>
    void WritePps(PictureParameterSet pps, Memory<uint> runLengthMinus1, Memory<uint> sliceGroupId, bool moreRbspData, ScalingMatrixBuilder build);

    /// <summary>
    ///   Writes the Picture Parameter Set (PPS) to the bitstream.
    /// </summary>
    /// <param name="pps">The picture parameter set to write</param>
    /// <param name="runLengthMinus1">Run Length values.</param>
    /// <param name="sliceGroupId">Identifiers for slice groups,</param>
    /// <param name="moreRbspData">Is there more RBSP data remaining?</param>
    /// <param name="build">Scaling matrix builder</param>
    Task WritePpsAsync(PictureParameterSet pps, Memory<uint> runLengthMinus1, Memory<uint> sliceGroupId, bool moreRbspData, ScalingMatrixBuilder build);

    /// <summary>
    ///   Writes the Decoder Reference Picture Marking (DecRefPicMarking) to the bitstream.
    /// </summary>
    /// <param name="marking"><see cref="DecRefPicMarking"/> to write</param>
    /// <param name="idrPicFlag">Is the current NAL unit of IDR picture type?</param>
    /// <param name="entries">All DecRefPicMarking entries.</param>
    void WriteDecRefPicMarking(DecRefPicMarking marking, bool idrPicFlag, Span<DecRefPicMarkingEntry> entries);

    /// <summary>
    ///   Writes the Decoder Reference Picture Marking (DecRefPicMarking) to the bitstream.
    /// </summary>
    /// <param name="marking"><see cref="DecRefPicMarking"/> to write</param>
    /// <param name="idrPicFlag">Is the current NAL unit of IDR picture type?</param>
    /// <param name="entries">All DecRefPicMarking entries.</param>
    void WriteDecRefPicMarking(DecRefPicMarking marking, bool idrPicFlag, Memory<DecRefPicMarkingEntry> entries);

    /// <summary>
    ///   Writes the Decoder Reference Picture Marking (DecRefPicMarking) to the bitstream.
    /// </summary>
    /// <param name="marking"><see cref="DecRefPicMarking"/> to write</param>
    /// <param name="idrPicFlag">Is the current NAL unit of IDR picture type?</param>
    /// <param name="entries">All DecRefPicMarking entries.</param>
    Task WriteDecRefPicMarkingAsync(DecRefPicMarking marking, bool idrPicFlag, Memory<DecRefPicMarkingEntry> entries);

    /// <summary>
    ///   Writes Hypothetical Reference Decoder (HRD) parameters to the bitstream.
    /// </summary>
    /// <param name="hrd">HRD parameters to write</param>
    /// <param name="bitRateValueMinus1">Bit rate value minus 1 values.</param>
    /// <param name="cpbSizeValueMinus1">CPB size value minus 1 values.</param>
    /// <param name="cbrFlag">CBR flag values.</param>
    void WriteHrdParameters(HrdParameters hrd, ReadOnlySpan<uint> bitRateValueMinus1, ReadOnlySpan<uint> cpbSizeValueMinus1, ReadOnlySpan<bool> cbrFlag);

    /// <summary>
    ///   Writes Hypothetical Reference Decoder (HRD) parameters to the bitstream.
    /// </summary>
    /// <param name="hrd">HRD parameters to write</param>
    /// <param name="bitRateValueMinus1">Bit rate value minus 1 values.</param>
    /// <param name="cpbSizeValueMinus1">CPB size value minus 1 values.</param>
    /// <param name="cbrFlag">CBR flag values.</param>
    void WriteHrdParameters(HrdParameters hrd, ReadOnlySpan<uint> bitRateValueMinus1, ReadOnlySpan<uint> cpbSizeValueMinus1, ReadOnlyMemory<bool> cbrFlag);

    /// <summary>
    ///   Writes Hypothetical Reference Decoder (HRD) parameters to the bitstream.
    /// </summary>
    /// <param name="hrd">HRD parameters to write</param>
    /// <param name="bitRateValueMinus1">Bit rate value minus 1 values.</param>
    /// <param name="cpbSizeValueMinus1">CPB size value minus 1 values.</param>
    /// <param name="cbrFlag">CBR flag values.</param>
    void WriteHrdParameters(HrdParameters hrd, ReadOnlySpan<uint> bitRateValueMinus1, ReadOnlyMemory<uint> cpbSizeValueMinus1, ReadOnlySpan<bool> cbrFlag);

    /// <summary>
    ///   Writes Hypothetical Reference Decoder (HRD) parameters to the bitstream.
    /// </summary>
    /// <param name="hrd">HRD parameters to write</param>
    /// <param name="bitRateValueMinus1">Bit rate value minus 1 values.</param>
    /// <param name="cpbSizeValueMinus1">CPB size value minus 1 values.</param>
    /// <param name="cbrFlag">CBR flag values.</param>
    void WriteHrdParameters(HrdParameters hrd, ReadOnlySpan<uint> bitRateValueMinus1, ReadOnlyMemory<uint> cpbSizeValueMinus1, ReadOnlyMemory<bool> cbrFlag);

    /// <summary>
    ///   Writes Hypothetical Reference Decoder (HRD) parameters to the bitstream.
    /// </summary>
    /// <param name="hrd">HRD parameters to write</param>
    /// <param name="bitRateValueMinus1">Bit rate value minus 1 values.</param>
    /// <param name="cpbSizeValueMinus1">CPB size value minus 1 values.</param>
    /// <param name="cbrFlag">CBR flag values.</param>
    void WriteHrdParameters(HrdParameters hrd, ReadOnlyMemory<uint> bitRateValueMinus1, ReadOnlySpan<uint> cpbSizeValueMinus1, ReadOnlySpan<bool> cbrFlag);

    /// <summary>
    ///   Writes Hypothetical Reference Decoder (HRD) parameters to the bitstream.
    /// </summary>
    /// <param name="hrd">HRD parameters to write</param>
    /// <param name="bitRateValueMinus1">Bit rate value minus 1 values.</param>
    /// <param name="cpbSizeValueMinus1">CPB size value minus 1 values.</param>
    /// <param name="cbrFlag">CBR flag values.</param>
    void WriteHrdParameters(HrdParameters hrd, ReadOnlyMemory<uint> bitRateValueMinus1, ReadOnlySpan<uint> cpbSizeValueMinus1, ReadOnlyMemory<bool> cbrFlag);

    /// <summary>
    ///   Writes Hypothetical Reference Decoder (HRD) parameters to the bitstream.
    /// </summary>
    /// <param name="hrd">HRD parameters to write</param>
    /// <param name="bitRateValueMinus1">Bit rate value minus 1 values.</param>
    /// <param name="cpbSizeValueMinus1">CPB size value minus 1 values.</param>
    /// <param name="cbrFlag">CBR flag values.</param>
    void WriteHrdParameters(HrdParameters hrd, ReadOnlyMemory<uint> bitRateValueMinus1, ReadOnlyMemory<uint> cpbSizeValueMinus1, ReadOnlySpan<bool> cbrFlag);

    /// <summary>
    ///   Writes Hypothetical Reference Decoder (HRD) parameters to the bitstream.
    /// </summary>
    /// <param name="hrd">HRD parameters to write</param>
    /// <param name="bitRateValueMinus1">Bit rate value minus 1 values.</param>
    /// <param name="cpbSizeValueMinus1">CPB size value minus 1 values.</param>
    /// <param name="cbrFlag">CBR flag values.</param>
    void WriteHrdParameters(HrdParameters hrd, ReadOnlyMemory<uint> bitRateValueMinus1, ReadOnlyMemory<uint> cpbSizeValueMinus1, ReadOnlyMemory<bool> cbrFlag);

    /// <summary>
    ///   Writes Hypothetical Reference Decoder (HRD) parameters to the bitstream.
    /// </summary>
    /// <param name="hrd">HRD parameters to write</param>
    /// <param name="bitRateValueMinus1">Bit rate value minus 1 values.</param>
    /// <param name="cpbSizeValueMinus1">CPB size value minus 1 values.</param>
    /// <param name="cbrFlag">CBR flag values.</param>
    Task WriteHrdParametersAsync(HrdParameters hrd, ReadOnlyMemory<uint> bitRateValueMinus1, ReadOnlyMemory<uint> cpbSizeValueMinus1, ReadOnlyMemory<bool> cbrFlag);

    /// <summary>
    ///   Writes a NAL unit to the bitstream.
    /// </summary>
    /// <param name="nalUnitType">Type of the NAL unit.</param>
    /// <param name="nalRefIdc">Its reference identifier code.</param>
    /// <param name="extension">NALU extension, if present.</param>
    void WriteNalUnit(uint nalUnitType, uint nalRefIdc, INalUnitHeaderExtension? extension = null);

    /// <summary>
    ///   Writes a NAL unit of type SPS.
    /// </summary>
    /// <param name="nalRefIdc">NAL reference identifier code.</param>
    void WriteNalUnitSps(uint nalRefIdc);

    /// <summary>
    ///   Writes a NAL unit of type PPS.
    /// </summary>
    /// <param name="nalRefIdc">NAL reference identifier code.</param>
    void WriteNalUnitPps(uint nalRefIdc);

    /// <summary>
    ///   Writes a NAL unit of type IDR.
    /// </summary>
    /// <param name="nalRefIdc">NAL reference identifier code.</param>
    void WriteNalUnitIdr(uint nalRefIdc);

    /// <summary>
    ///   Writes a NAL unit of type Non-IDR.
    /// </summary>
    /// <param name="nalRefIdc">NAL reference identifier code.</param>
    void WriteNalUnitNonIdr(uint nalRefIdc);

    /// <summary>
    ///   Writes a NAL unit of type SEI.
    /// </summary>
    /// <param name="nalRefIdc">NAL reference identifier code.</param>
    void WriteNalUnitSei(uint nalRefIdc);

    /// <summary>
    ///   Writes a NAL unit to the bitstream.
    /// </summary>
    /// <param name="nalUnitType">Type of the NAL unit.</param>
    /// <param name="nalRefIdc">Its reference identifier code.</param>
    /// <param name="extension">NALU extension, if present.</param>
    Task WriteNalUnitAsync(uint nalUnitType, uint nalRefIdc, INalUnitHeaderExtension? extension = null);

    /// <summary>
    ///   Writes a NAL unit of type SPS.
    /// </summary>
    /// <param name="nalRefIdc">NAL reference identifier code.</param>
    Task WriteNalUnitSpsAsync(uint nalRefIdc);

    /// <summary>
    ///   Writes a NAL unit of type PPS.
    /// </summary>
    /// <param name="nalRefIdc">NAL reference identifier code.</param>
    Task WriteNalUnitPpsAsync(uint nalRefIdc);

    /// <summary>
    ///   Writes a NAL unit of type IDR.
    /// </summary>
    /// <param name="nalRefIdc">NAL reference identifier code.</param>
    Task WriteNalUnitIdrAsync(uint nalRefIdc);

    /// <summary>
    ///   Writes a NAL unit of type Non-IDR.
    /// </summary>
    /// <param name="nalRefIdc">NAL reference identifier code.</param>
    Task WriteNalUnitNonIdrAsync(uint nalRefIdc);

    /// <summary>
    ///   Writes a NAL unit of type SEI.
    /// </summary>
    /// <param name="nalRefIdc">NAL reference identifier code.</param>
    Task WriteNalUnitSeiAsync(uint nalRefIdc);

    /// <summary>
    ///   Writes reference picture list modification to the bitstream.
    /// </summary>
    /// <param name="modification">Modification to write</param>
    /// <param name="entries">Entries of the modification.</param>
    /// <param name="sliceType">Slice type; should be taken from the slice header.</param>
    void WriteRefPicListModification(RefPicListModification modification, Span<RefPicListModificationEntry> entries, int sliceType);

    /// <summary>
    ///   Writes reference picture list modification to the bitstream.
    /// </summary>
    /// <param name="modification">Modification to write</param>
    /// <param name="entries">Entries of the modification.</param>
    /// <param name="sliceType">Slice type; should be taken from the slice header.</param>
    void WriteRefPicListModification(RefPicListModification modification, Memory<RefPicListModificationEntry> entries, int sliceType);

    /// <summary>
    ///   Writes reference picture list modification to the bitstream.
    /// </summary>
    /// <param name="modification">Modification to write</param>
    /// <param name="entries">Entries of the modification.</param>
    /// <param name="sliceType">Slice type; should be taken from the slice header.</param>
    Task WriteRefPicListModificationAsync(RefPicListModification modification, Memory<RefPicListModificationEntry> entries, int sliceType);

    /// <summary>
    ///   Writes MVC reference picture list modification to the bitstream.
    /// </summary>
    /// <param name="mvc">MVC reference picture list modification to write</param>
    /// <param name="l0">List 0</param>
    /// <param name="l1">List 1</param>
    /// <param name="includeL0">Should List 0 be written?</param>
    /// <param name="includeL1">Should List 1 be written?</param>
    /// <param name="sliceType">Slice type; should be taken from the slice header.</param>
    void WriteRefPicListModificationMvc(RefPicListMvcModification mvc, Span<RefPicListMvcModificationEntry> l0, Span<RefPicListMvcModificationEntry> l1, bool includeL0, bool includeL1, int sliceType);

    /// <summary>
    ///   Writes MVC reference picture list modification to the bitstream.
    /// </summary>
    /// <param name="mvc">MVC reference picture list modification to write</param>
    /// <param name="l0">List 0</param>
    /// <param name="l1">List 1</param>
    /// <param name="includeL0">Should List 0 be written?</param>
    /// <param name="includeL1">Should List 1 be written?</param>
    /// <param name="sliceType">Slice type; should be taken from the slice header.</param>
    void WriteRefPicListModificationMvc(RefPicListMvcModification mvc, Memory<RefPicListMvcModificationEntry> l0, Memory<RefPicListMvcModificationEntry> l1, bool includeL0, bool includeL1, int sliceType);

    /// <summary>
    ///   Writes MVC reference picture list modification to the bitstream.
    /// </summary>
    /// <param name="mvc">MVC reference picture list modification to write</param>
    /// <param name="l0">List 0</param>
    /// <param name="l1">List 1</param>
    /// <param name="includeL0">Should List 0 be written?</param>
    /// <param name="includeL1">Should List 1 be written?</param>
    /// <param name="sliceType">Slice type; should be taken from the slice header.</param>
    Task WriteRefPicListModificationMvcAsync(RefPicListMvcModification mvc, Memory<RefPicListMvcModificationEntry> l0, Memory<RefPicListMvcModificationEntry> l1, bool includeL0, bool includeL1, int sliceType);

    /// <summary>
    ///   Writes the Slice Header to the bitstream.
    /// </summary>
    /// <param name="sliceHeader">Slice header to write.</param>
    /// <param name="nalu">Current NAL unit.</param>
    /// <param name="sps">Last SPS.</param>
    /// <param name="pps">Last PPS.</param>
    /// <param name="options">Options for writing the slice header.</param>
    void WriteSliceHeader(SliceHeader sliceHeader, NalUnit nalu, SequenceParameterSet sps, PictureParameterSet pps, MemorySliceHeaderWriteOptions options);

    /// <summary>
    ///   Writes the Slice Header to the bitstream.
    /// </summary>
    /// <param name="sliceHeader">Slice header to write.</param>
    /// <param name="nalu">Current NAL unit.</param>
    /// <param name="sps">Last SPS.</param>
    /// <param name="pps">Last PPS.</param>
    /// <param name="options">Options for writing the slice header.</param>
    void WriteSliceHeader(SliceHeader sliceHeader, NalUnit nalu, SequenceParameterSet sps, PictureParameterSet pps, SliceHeaderWriteOptions options);

    /// <summary>
    ///   Writes the Slice Header to the bitstream.
    /// </summary>
    /// <param name="sliceHeader">Slice header to write.</param>
    /// <param name="nalu">Current NAL unit.</param>
    /// <param name="sps">Last SPS.</param>
    /// <param name="pps">Last PPS.</param>
    /// <param name="options">Options for writing the slice header.</param>
    Task WriteSliceHeaderAsync(SliceHeader sliceHeader, NalUnit nalu, SequenceParameterSet sps, PictureParameterSet pps, MemorySliceHeaderWriteOptions options);

    /// <summary>
    ///   Writes VUI parameters to the bitstream.
    /// </summary>
    /// <param name="vuiParameters">VUI parameters to write.</param>
    /// <param name="options">Options for writing the VUI parameters.</param>
    void WriteVuiParameters(VuiParameters vuiParameters, VuiWriteOptions options);

    /// <summary>
    ///   Writes VUI parameters to the bitstream.
    /// </summary>
    /// <param name="vuiParameters">VUI parameters to write.</param>
    /// <param name="options">Options for writing the VUI parameters.</param>
    Task WriteVuiParametersAsync(VuiParameters vuiParameters, MemoryVuiWriteOptions options);
}
