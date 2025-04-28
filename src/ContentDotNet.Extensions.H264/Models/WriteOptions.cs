namespace ContentDotNet.Extensions.H264.Models;

/// <summary>
/// HRD write options.
/// </summary>
public struct MemoryHrdWriteOptions
{
    /// <summary>
    ///   Part of the HRD that will be written.
    /// </summary>
    public ReadOnlyMemory<uint> BitRateValueMinus1;

    /// <summary>
    ///   Part of the HRD that will be written.
    /// </summary>
    public ReadOnlyMemory<uint> CpbSizeValueMinus1;

    /// <summary>
    ///   Part of the HRD that will be written.
    /// </summary>
    public ReadOnlyMemory<bool> CbrFlag;

    /// <summary>
    ///   Initializes a new instance of the <see cref="MemoryHrdWriteOptions"/> structure.
    /// </summary>
    /// <param name="bitRateValueMinus1">Part of the HRD that will be written.</param>
    /// <param name="cpbSizeValueMinus1">Part of the HRD that will be written.</param>
    /// <param name="cbrFlag">Part of the HRD that will be written.</param>
    public MemoryHrdWriteOptions(ReadOnlyMemory<uint> bitRateValueMinus1, ReadOnlyMemory<uint> cpbSizeValueMinus1, ReadOnlyMemory<bool> cbrFlag)
    {
        BitRateValueMinus1 = bitRateValueMinus1;
        CpbSizeValueMinus1 = cpbSizeValueMinus1;
        CbrFlag = cbrFlag;
    }
}

/// <summary>
/// VUI write options that use the <see cref="Memory{T}"/> type for heap allocation.
/// </summary>
public readonly struct MemoryVuiWriteOptions
{
    /// <summary>
    ///   <see cref="Memory{T}"/> HRD write options for NAL
    /// </summary>
    public readonly MemoryHrdWriteOptions NalHrdWriteOptions;

    /// <summary>
    ///   <see cref="Memory{T}"/> HRD write options for VCL
    /// </summary>
    public readonly MemoryHrdWriteOptions VclHrdWriteOptions;

    /// <summary>
    ///   Dictates whether <see cref="NalHrdWriteOptions"/> should be written.
    /// </summary>
    public readonly bool IsNalPresent;

    /// <summary>
    ///   Dictates whether <see cref="VclHrdWriteOptions"/> should be written.
    /// </summary>
    public readonly bool IsVclPresent;

    /// <summary>
    ///   Initializes a new instance of the <see cref="MemoryVuiWriteOptions"/> structure.
    /// </summary>
    /// <param name="nalHrdWriteOptions"><see cref="Memory{T}"/> HRD write options for NAL</param>
    /// <param name="vclHrdWriteOptions"><see cref="Memory{T}"/> HRD write options for VCL</param>
    /// <param name="isNalPresent">Dictates whether <see cref="NalHrdWriteOptions"/> should be written.</param>
    /// <param name="isVclPresent">Dictates whether <see cref="VclHrdWriteOptions"/> should be written.</param>
    public MemoryVuiWriteOptions(MemoryHrdWriteOptions nalHrdWriteOptions, MemoryHrdWriteOptions vclHrdWriteOptions, bool isNalPresent, bool isVclPresent)
    {
        NalHrdWriteOptions = nalHrdWriteOptions;
        VclHrdWriteOptions = vclHrdWriteOptions;
        IsNalPresent = isNalPresent;
        IsVclPresent = isVclPresent;
    }
}

/// <summary>
/// VUI write options that use the <see cref="Span{T}"/> type for stack allocation.
/// </summary>
public readonly ref struct VuiWriteOptions
{
    /// <summary>
    ///   <see cref="Span{T}"/> HRD write options for NAL
    /// </summary>
    public readonly HrdWriteOptions NalHrdWriteOptions;

    /// <summary>
    ///   <see cref="Span{T}"/> HRD write options for VCL
    /// </summary>
    public readonly HrdWriteOptions VclHrdWriteOptions;

    /// <summary>
    ///   Dictates whether <see cref="NalHrdWriteOptions"/> should be written.
    /// </summary>
    public readonly bool IsNalPresent;

    /// <summary>
    ///   Dictates whether <see cref="VclHrdWriteOptions"/> should be written.
    /// </summary>
    public readonly bool IsVclPresent;

    /// <summary>
    ///   Initializes a new instance of the <see cref="VuiWriteOptions"/> structure.
    /// </summary>
    /// <param name="nalHrdWriteOptions"><see cref="Span{T}"/> HRD write options for NAL</param>
    /// <param name="vclHrdWriteOptions"><see cref="Span{T}"/> HRD write options for VCL</param>
    /// <param name="isNalPresent">Dictates whether <see cref="NalHrdWriteOptions"/> should be written.</param>
    /// <param name="isVclPresent">Dictates whether <see cref="VclHrdWriteOptions"/> should be written.</param>
    public VuiWriteOptions(HrdWriteOptions nalHrdWriteOptions, HrdWriteOptions vclHrdWriteOptions, bool isNalPresent, bool isVclPresent)
    {
        NalHrdWriteOptions = nalHrdWriteOptions;
        VclHrdWriteOptions = vclHrdWriteOptions;
        IsNalPresent = isNalPresent;
        IsVclPresent = isVclPresent;
    }
}

/// <summary>
/// HRD write options.
/// </summary>
public ref struct HrdWriteOptions
{
    /// <summary>
    ///   Part of the HRD that will be written.
    /// </summary>
    public ReadOnlySpan<uint> BitRateValueMinus1;

    /// <summary>
    ///   Part of the HRD that will be written.
    /// </summary>
    public ReadOnlySpan<uint> CpbSizeValueMinus1;

    /// <summary>
    ///   Part of the HRD that will be written.
    /// </summary>
    public ReadOnlySpan<bool> CbrFlag;

    /// <summary>
    ///   Initializes a new instance of the <see cref="HrdWriteOptions"/> structure.
    /// </summary>
    /// <param name="bitRateValueMinus1">Part of the HRD that will be written.</param>
    /// <param name="cpbSizeValueMinus1">Part of the HRD that will be written.</param>
    /// <param name="cbrFlag">Part of the HRD that will be written.</param>
    public HrdWriteOptions(ReadOnlySpan<uint> bitRateValueMinus1, ReadOnlySpan<uint> cpbSizeValueMinus1, ReadOnlySpan<bool> cbrFlag)
    {
        BitRateValueMinus1 = bitRateValueMinus1;
        CpbSizeValueMinus1 = cpbSizeValueMinus1;
        CbrFlag = cbrFlag;
    }
}

/// <summary>
/// Represents the write options for a slice header using spans.
/// </summary>
public ref struct SliceHeaderWriteOptions
{
    /// <summary>
    /// Prediction weight table list write options for list 0.
    /// </summary>
    public PredWeightTableListWriteOptions PredWeightTableL0;

    /// <summary>
    /// Prediction weight table list write options for list 1.
    /// </summary>
    public PredWeightTableListWriteOptions PredWeightTableL1;

    /// <summary>
    /// Reference picture list modification entries.
    /// </summary>
    public Span<RefPicListModificationEntry> RefPicListModificationEntries;

    /// <summary>
    /// Reference picture list MVC modification entries for list 0.
    /// </summary>
    public Span<RefPicListMvcModificationEntry> RefPicListMvcModificationL0;

    /// <summary>
    /// Reference picture list MVC modification entries for list 1.
    /// </summary>
    public Span<RefPicListMvcModificationEntry> RefPicListMvcModificationL1;

    /// <summary>
    /// Decoded reference picture marking entries.
    /// </summary>
    public Span<DecRefPicMarkingEntry> DecRefPicMarkingEntries;

    /// <summary>
    /// Initializes a new instance of the <see cref="SliceHeaderWriteOptions"/> struct.
    /// </summary>
    /// <param name="predWeightTableL0">Prediction weight table list write options for list 0.</param>
    /// <param name="predWeightTableL1">Prediction weight table list write options for list 1.</param>
    /// <param name="refPicListModificationEntries">Reference picture list modification entries.</param>
    /// <param name="refPicListMvcModificationL0">Reference picture list MVC modification entries for list 0.</param>
    /// <param name="refPicListMvcModificationL1">Reference picture list MVC modification entries for list 1.</param>
    /// <param name="decRefPicMarkingEntries">Decoded reference picture marking entries.</param>
    public SliceHeaderWriteOptions(
        PredWeightTableListWriteOptions predWeightTableL0,
        PredWeightTableListWriteOptions predWeightTableL1,
        Span<RefPicListModificationEntry> refPicListModificationEntries,
        Span<RefPicListMvcModificationEntry> refPicListMvcModificationL0,
        Span<RefPicListMvcModificationEntry> refPicListMvcModificationL1,
        Span<DecRefPicMarkingEntry> decRefPicMarkingEntries)
    {
        PredWeightTableL0 = predWeightTableL0;
        PredWeightTableL1 = predWeightTableL1;
        RefPicListModificationEntries = refPicListModificationEntries;
        RefPicListMvcModificationL0 = refPicListMvcModificationL0;
        RefPicListMvcModificationL1 = refPicListMvcModificationL1;
        DecRefPicMarkingEntries = decRefPicMarkingEntries;
    }
}

/// <summary>
/// Represents the write options for a slice header using memory.
/// </summary>
public struct MemorySliceHeaderWriteOptions
{
    /// <summary>
    /// Prediction weight table list write options for list 0.
    /// </summary>
    public MemoryPredWeightTableListWriteOptions PredWeightTableL0;

    /// <summary>
    /// Prediction weight table list write options for list 1.
    /// </summary>
    public MemoryPredWeightTableListWriteOptions PredWeightTableL1;

    /// <summary>
    /// Reference picture list modification entries.
    /// </summary>
    public Memory<RefPicListModificationEntry> RefPicListModificationEntries;

    /// <summary>
    /// Reference picture list MVC modification entries for list 0.
    /// </summary>
    public Memory<RefPicListMvcModificationEntry> RefPicListMvcModificationL0;

    /// <summary>
    /// Reference picture list MVC modification entries for list 1.
    /// </summary>
    public Memory<RefPicListMvcModificationEntry> RefPicListMvcModificationL1;

    /// <summary>
    /// Decoded reference picture marking entries.
    /// </summary>
    public Memory<DecRefPicMarkingEntry> DecRefPicMarkingEntries;

    /// <summary>
    /// Initializes a new instance of the <see cref="MemorySliceHeaderWriteOptions"/> struct.
    /// </summary>
    /// <param name="predWeightTableL0">Prediction weight table list write options for list 0.</param>
    /// <param name="predWeightTableL1">Prediction weight table list write options for list 1.</param>
    /// <param name="refPicListModificationEntries">Reference picture list modification entries.</param>
    /// <param name="refPicListMvcModificationL0">Reference picture list MVC modification entries for list 0.</param>
    /// <param name="refPicListMvcModificationL1">Reference picture list MVC modification entries for list 1.</param>
    /// <param name="decRefPicMarkingEntries">Decoded reference picture marking entries.</param>
    public MemorySliceHeaderWriteOptions(
        MemoryPredWeightTableListWriteOptions predWeightTableL0,
        MemoryPredWeightTableListWriteOptions predWeightTableL1,
        Memory<RefPicListModificationEntry> refPicListModificationEntries,
        Memory<RefPicListMvcModificationEntry> refPicListMvcModificationL0,
        Memory<RefPicListMvcModificationEntry> refPicListMvcModificationL1,
        Memory<DecRefPicMarkingEntry> decRefPicMarkingEntries)
    {
        PredWeightTableL0 = predWeightTableL0;
        PredWeightTableL1 = predWeightTableL1;
        RefPicListModificationEntries = refPicListModificationEntries;
        RefPicListMvcModificationL0 = refPicListMvcModificationL0;
        RefPicListMvcModificationL1 = refPicListMvcModificationL1;
        DecRefPicMarkingEntries = decRefPicMarkingEntries;
    }
}
