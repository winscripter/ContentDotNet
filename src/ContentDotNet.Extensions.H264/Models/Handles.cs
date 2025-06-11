using ContentDotNet.BitStream;
using ContentDotNet.Containers;
using ContentDotNet.Extensions.H264.Macroblocks;
using System.Diagnostics.CodeAnalysis;

namespace ContentDotNet.Extensions.H264.Models;

/// <summary>
/// Represents a handle for a Prediction Weight Table.
/// </summary>
public readonly struct PredWeightTableHandle : IEquatable<PredWeightTableHandle>
{
    /// <summary>
    /// Gets the reader state associated with this handle.
    /// </summary>
    public ReaderState ReaderState { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PredWeightTableHandle"/> struct.
    /// </summary>
    /// <param name="readerState">The reader state.</param>
    public PredWeightTableHandle(ReaderState readerState)
    {
        this.ReaderState = readerState;
    }

    /// <summary>
    /// Determines whether the specified object is equal to this instance.
    /// </summary>
    /// <param name="obj">The object to compare with this instance.</param>
    /// <returns><c>true</c> if the objects are equal; otherwise, <c>false</c>.</returns>
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is PredWeightTableHandle handle && Equals(handle);
    }

    /// <summary>
    /// Determines whether the specified <see cref="PredWeightTableHandle"/> is equal to this instance.
    /// </summary>
    /// <param name="other">The other <see cref="PredWeightTableHandle"/> instance.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public bool Equals(PredWeightTableHandle other)
    {
        return this.ReaderState == other.ReaderState;
    }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>A hash code for this instance.</returns>
    public override int GetHashCode() => this.ReaderState.GetHashCode();

    /// <summary>
    /// Retrieves the Prediction Weight Table using the specified parameters.
    /// </summary>
    /// <param name="reader">The bit stream reader.</param>
    /// <param name="chromaArrayType">The chroma array type.</param>
    /// <param name="sliceType">The slice type.</param>
    /// <param name="numRefIdxL0ActiveMinus1">The number of active reference indices for list 0.</param>
    /// <param name="numRefIdxL1ActiveMinus1">The number of active reference indices for list 1.</param>
    /// <returns>The retrieved <see cref="PredWeightTable"/>.</returns>
    public PredWeightTable Get(BitStreamReader reader, int chromaArrayType, int sliceType, int numRefIdxL0ActiveMinus1, int numRefIdxL1ActiveMinus1)
    {
        ReaderState prev = reader.GetState();
        reader.GoTo(ReaderState);

        var result = PredWeightTable.Read(reader, chromaArrayType, sliceType, numRefIdxL0ActiveMinus1, numRefIdxL1ActiveMinus1);
        
        reader.GoTo(prev);

        return result;
    }

    /// <summary>
    /// Determines whether two <see cref="PredWeightTableHandle"/> instances are equal.
    /// </summary>
    /// <param name="left">The left instance to compare.</param>
    /// <param name="right">The right instance to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(PredWeightTableHandle left, PredWeightTableHandle right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="PredWeightTableHandle"/> instances are not equal.
    /// </summary>
    /// <param name="left">The left instance to compare.</param>
    /// <param name="right">The right instance to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(PredWeightTableHandle left, PredWeightTableHandle right)
    {
        return !(left == right);
    }
}

/// <summary>
/// Represents a handle for Decoded Reference Picture Marking.
/// </summary>
public readonly struct DecRefPicMarkingHandle : IEquatable<DecRefPicMarkingHandle>
{
    /// <summary>
    /// Gets the reader state associated with this handle.
    /// </summary>
    public ReaderState ReaderState { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DecRefPicMarkingHandle"/> struct.
    /// </summary>
    /// <param name="readerState">The reader state.</param>
    public DecRefPicMarkingHandle(ReaderState readerState)
    {
        this.ReaderState = readerState;
    }

    /// <summary>
    /// Retrieves the Decoded Reference Picture Marking using the specified parameters.
    /// </summary>
    /// <param name="reader">The bit stream reader.</param>
    /// <param name="idrPicFlag">Indicates whether the current picture is an IDR picture.</param>
    /// <returns>The retrieved <see cref="DecRefPicMarking"/>.</returns>
    public DecRefPicMarking Get(BitStreamReader reader, bool idrPicFlag)
    {
        ReaderState prev = reader.GetState();
        reader.GoTo(ReaderState);

        var result = DecRefPicMarking.Read(reader, idrPicFlag);

        reader.GoTo(prev);

        return result;
    }

    /// <summary>
    /// Determines whether the specified object is equal to this instance.
    /// </summary>
    /// <param name="obj">The object to compare with this instance.</param>
    /// <returns><c>true</c> if the objects are equal; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? obj)
    {
        return obj is DecRefPicMarkingHandle handle && Equals(handle);
    }

    /// <summary>
    /// Determines whether the specified <see cref="DecRefPicMarkingHandle"/> is equal to this instance.
    /// </summary>
    /// <param name="other">The other <see cref="DecRefPicMarkingHandle"/> instance.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public bool Equals(DecRefPicMarkingHandle other)
    {
        return ReaderState.Equals(other.ReaderState);
    }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>A hash code for this instance.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(ReaderState);
    }

    /// <summary>
    /// Determines whether two <see cref="DecRefPicMarkingHandle"/> instances are equal.
    /// </summary>
    /// <param name="left">The left instance to compare.</param>
    /// <param name="right">The right instance to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(DecRefPicMarkingHandle left, DecRefPicMarkingHandle right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="DecRefPicMarkingHandle"/> instances are not equal.
    /// </summary>
    /// <param name="left">The left instance to compare.</param>
    /// <param name="right">The right instance to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(DecRefPicMarkingHandle left, DecRefPicMarkingHandle right)
    {
        return !(left == right);
    }
}

/// <summary>
/// Represents a handle for Hypothetical Reference Decoder (HRD) parameters.
/// </summary>
public readonly struct HrdParametersHandle : IEquatable<HrdParametersHandle>
{
    /// <summary>
    /// Gets the reader state associated with this handle.
    /// </summary>
    public ReaderState ReaderState { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="HrdParametersHandle"/> struct.
    /// </summary>
    /// <param name="readerState">The reader state.</param>
    public HrdParametersHandle(ReaderState readerState)
    {
        this.ReaderState = readerState;
    }

    /// <summary>
    /// Retrieves the HRD parameters using the specified reader.
    /// </summary>
    /// <param name="reader">The bit stream reader.</param>
    /// <returns>The retrieved <see cref="HrdParameters"/>.</returns>
    public HrdParameters Get(BitStreamReader reader)
    {
        ReaderState prev = reader.GetState();
        reader.GoTo(ReaderState);

        var result = HrdParameters.Read(reader);

        reader.GoTo(prev);

        return result;
    }

    /// <summary>
    /// Determines whether the specified object is equal to this instance.
    /// </summary>
    /// <param name="obj">The object to compare with this instance.</param>
    /// <returns><c>true</c> if the objects are equal; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? obj)
    {
        return obj is HrdParametersHandle handle && Equals(handle);
    }

    /// <summary>
    /// Determines whether the specified <see cref="HrdParametersHandle"/> is equal to this instance.
    /// </summary>
    /// <param name="other">The other <see cref="HrdParametersHandle"/> instance.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public bool Equals(HrdParametersHandle other)
    {
        return ReaderState.Equals(other.ReaderState);
    }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>A hash code for this instance.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(ReaderState);
    }

    /// <summary>
    /// Determines whether two <see cref="HrdParametersHandle"/> instances are equal.
    /// </summary>
    /// <param name="left">The left instance to compare.</param>
    /// <param name="right">The right instance to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(HrdParametersHandle left, HrdParametersHandle right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="HrdParametersHandle"/> instances are not equal.
    /// </summary>
    /// <param name="left">The left instance to compare.</param>
    /// <param name="right">The right instance to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(HrdParametersHandle left, HrdParametersHandle right)
    {
        return !(left == right);
    }
}

/// <summary>
/// Represents a handle for Picture Parameter Set (PPS).
/// </summary>
public readonly struct PictureParameterSetHandle : IEquatable<PictureParameterSetHandle>
{
    /// <summary>
    /// Gets the reader state associated with this handle.
    /// </summary>
    public ReaderState ReaderState { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PictureParameterSetHandle"/> struct.
    /// </summary>
    /// <param name="readerState">The reader state.</param>
    public PictureParameterSetHandle(ReaderState readerState)
    {
        this.ReaderState = readerState;
    }

    /// <summary>
    /// Retrieves the Picture Parameter Set using the specified reader and Sequence Parameter Set.
    /// </summary>
    /// <param name="reader">The bit stream reader.</param>
    /// <param name="len">Number of bytes in the NAL Unit + RBSP</param>
    /// <param name="sequenceParameterSet">The sequence parameter set.</param>
    /// <returns>The retrieved <see cref="PictureParameterSet"/>.</returns>
    public PictureParameterSet Get(BitStreamReader reader, long len, SequenceParameterSet sequenceParameterSet)
    {
        ReaderState prev = reader.GetState();
        reader.GoTo(ReaderState);

        var result = PictureParameterSet.Read(reader, len, sequenceParameterSet);

        reader.GoTo(prev);

        return result;
    }

    /// <summary>
    /// Determines whether the specified object is equal to this instance.
    /// </summary>
    /// <param name="obj">The object to compare with this instance.</param>
    /// <returns><c>true</c> if the objects are equal; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? obj)
    {
        return obj is PictureParameterSetHandle handle && Equals(handle);
    }

    /// <summary>
    /// Determines whether the specified <see cref="PictureParameterSetHandle"/> is equal to this instance.
    /// </summary>
    /// <param name="other">The other <see cref="PictureParameterSetHandle"/> instance.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public bool Equals(PictureParameterSetHandle other)
    {
        return ReaderState.Equals(other.ReaderState);
    }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>A hash code for this instance.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(ReaderState);
    }

    /// <summary>
    /// Determines whether two <see cref="PictureParameterSetHandle"/> instances are equal.
    /// </summary>
    /// <param name="left">The left instance to compare.</param>
    /// <param name="right">The right instance to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(PictureParameterSetHandle left, PictureParameterSetHandle right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="PictureParameterSetHandle"/> instances are not equal.
    /// </summary>
    /// <param name="left">The left instance to compare.</param>
    /// <param name="right">The right instance to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(PictureParameterSetHandle left, PictureParameterSetHandle right)
    {
        return !(left == right);
    }
}

/// <summary>
/// Represents a handle for Reference Picture List Modification.
/// </summary>
public readonly struct RefPicListModificationHandle : IEquatable<RefPicListModificationHandle>
{
    /// <summary>
    /// Gets the reader state associated with this handle.
    /// </summary>
    public ReaderState ReaderState { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RefPicListModificationHandle"/> struct.
    /// </summary>
    /// <param name="readerState">The reader state.</param>
    public RefPicListModificationHandle(ReaderState readerState)
    {
        this.ReaderState = readerState;
    }

    /// <summary>
    /// Retrieves the Reference Picture List Modification using the specified reader and slice type.
    /// </summary>
    /// <param name="reader">The bit stream reader.</param>
    /// <param name="sliceType">The slice type.</param>
    /// <returns>The retrieved <see cref="RefPicListModification"/>.</returns>
    public RefPicListModification Get(BitStreamReader reader, uint sliceType)
    {
        ReaderState prev = reader.GetState();
        reader.GoTo(ReaderState);

        var result = RefPicListModification.Read(reader, sliceType);

        reader.GoTo(prev);

        return result;
    }

    /// <summary>
    /// Determines whether the specified object is equal to this instance.
    /// </summary>
    /// <param name="obj">The object to compare with this instance.</param>
    /// <returns><c>true</c> if the objects are equal; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? obj)
    {
        return obj is RefPicListModificationHandle handle && Equals(handle);
    }

    /// <summary>
    /// Determines whether the specified <see cref="RefPicListModificationHandle"/> is equal to this instance.
    /// </summary>
    /// <param name="other">The other <see cref="RefPicListModificationHandle"/> instance.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public bool Equals(RefPicListModificationHandle other)
    {
        return ReaderState.Equals(other.ReaderState);
    }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>A hash code for this instance.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(ReaderState);
    }

    /// <summary>
    /// Determines whether two <see cref="RefPicListModificationHandle"/> instances are equal.
    /// </summary>
    /// <param name="left">The left instance to compare.</param>
    /// <param name="right">The right instance to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(RefPicListModificationHandle left, RefPicListModificationHandle right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="RefPicListModificationHandle"/> instances are not equal.
    /// </summary>
    /// <param name="left">The left instance to compare.</param>
    /// <param name="right">The right instance to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(RefPicListModificationHandle left, RefPicListModificationHandle right)
    {
        return !(left == right);
    }
}

/// <summary>
/// Represents a handle for Reference Picture List MVC Modification.
/// </summary>
public readonly struct RefPicListMvcModificationHandle : IEquatable<RefPicListMvcModificationHandle>
{
    /// <summary>
    /// Gets the reader state associated with this handle.
    /// </summary>
    public ReaderState ReaderState { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RefPicListMvcModificationHandle"/> struct.
    /// </summary>
    /// <param name="readerState">The reader state.</param>
    public RefPicListMvcModificationHandle(ReaderState readerState)
    {
        this.ReaderState = readerState;
    }

    /// <summary>
    /// Retrieves the Reference Picture List MVC Modification using the specified reader and slice type.
    /// </summary>
    /// <param name="reader">The bit stream reader.</param>
    /// <param name="sliceType">The slice type.</param>
    /// <returns>The retrieved <see cref="RefPicListMvcModification"/>.</returns>
    public RefPicListMvcModification Get(BitStreamReader reader, int sliceType)
    {
        ReaderState prev = reader.GetState();
        reader.GoTo(ReaderState);

        var result = RefPicListMvcModification.Read(reader, sliceType);

        reader.GoTo(prev);

        return result;
    }

    /// <summary>
    /// Determines whether the specified object is equal to this instance.
    /// </summary>
    /// <param name="obj">The object to compare with this instance.</param>
    /// <returns><c>true</c> if the objects are equal; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? obj)
    {
        return obj is RefPicListMvcModificationHandle handle && Equals(handle);
    }

    /// <summary>
    /// Determines whether the specified <see cref="RefPicListMvcModificationHandle"/> is equal to this instance.
    /// </summary>
    /// <param name="other">The other <see cref="RefPicListMvcModificationHandle"/> instance.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public bool Equals(RefPicListMvcModificationHandle other)
    {
        return ReaderState.Equals(other.ReaderState);
    }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>A hash code for this instance.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(ReaderState);
    }

    /// <summary>
    /// Determines whether two <see cref="RefPicListMvcModificationHandle"/> instances are equal.
    /// </summary>
    /// <param name="left">The left instance to compare.</param>
    /// <param name="right">The right instance to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(RefPicListMvcModificationHandle left, RefPicListMvcModificationHandle right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="RefPicListMvcModificationHandle"/> instances are not equal.
    /// </summary>
    /// <param name="left">The left instance to compare.</param>
    /// <param name="right">The right instance to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(RefPicListMvcModificationHandle left, RefPicListMvcModificationHandle right)
    {
        return !(left == right);
    }
}

/// <summary>
/// Represents a handle for CABAC residuals.
/// </summary>
public readonly struct CabacResidualHandle : IEquatable<CabacResidualHandle>
{
    /// <summary>
    /// Gets the reader state associated with this handle.
    /// </summary>
    public ReaderState ReaderState { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CabacResidualHandle"/> struct.
    /// </summary>
    /// <param name="readerState">The reader state.</param>
    public CabacResidualHandle(ReaderState readerState)
    {
        this.ReaderState = readerState;
    }

    /// <summary>
    /// Retrieves the CABAC residual using the specified parameters.
    /// </summary>
    /// <param name="reader">The bit stream reader.</param>
    /// <param name="coeffLevel">The coefficient levels.</param>
    /// <param name="startIdx">The start index.</param>
    /// <param name="endIdx">The end index.</param>
    /// <param name="maxNumCoeff">The maximum number of coefficients.</param>
    /// <param name="chromaArrayType">The chroma array type.</param>
    /// <returns>The retrieved <see cref="CabacResidual"/>.</returns>
    public CabacResidual Get(BitStreamReader reader, Span<uint> coeffLevel, int startIdx, int endIdx, int maxNumCoeff, int chromaArrayType)
    {
        ReaderState prev = reader.GetState();
        reader.GoTo(ReaderState);

        var result = CabacResidual.Read(reader, coeffLevel, startIdx, endIdx, maxNumCoeff, chromaArrayType);

        reader.GoTo(prev);

        return result;
    }

    /// <summary>
    /// Determines whether the specified object is equal to this instance.
    /// </summary>
    /// <param name="obj">The object to compare with this instance.</param>
    /// <returns><c>true</c> if the objects are equal; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? obj)
    {
        return obj is CabacResidualHandle handle && Equals(handle);
    }

    /// <summary>
    /// Determines whether the specified <see cref="CabacResidualHandle"/> is equal to this instance.
    /// </summary>
    /// <param name="other">The other <see cref="CabacResidualHandle"/> instance.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public bool Equals(CabacResidualHandle other)
    {
        return ReaderState.Equals(other.ReaderState);
    }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>A hash code for this instance.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(ReaderState);
    }

    /// <summary>
    /// Determines whether two <see cref="CabacResidualHandle"/> instances are equal.
    /// </summary>
    /// <param name="left">The left instance to compare.</param>
    /// <param name="right">The right instance to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(CabacResidualHandle left, CabacResidualHandle right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="CabacResidualHandle"/> instances are not equal.
    /// </summary>
    /// <param name="left">The left instance to compare.</param>
    /// <param name="right">The right instance to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(CabacResidualHandle left, CabacResidualHandle right)
    {
        return !(left == right);
    }
}

/// <summary>
/// Represents a handle for CAVLC residuals.
/// </summary>
public readonly struct CavlcResidualHandle : IEquatable<CavlcResidualHandle>
{
    /// <summary>
    /// Gets the reader state associated with this handle.
    /// </summary>
    public ReaderState ReaderState { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CavlcResidualHandle"/> struct.
    /// </summary>
    /// <param name="readerState">The reader state.</param>
    public CavlcResidualHandle(ReaderState readerState)
    {
        this.ReaderState = readerState;
    }

    /// <summary>
    /// Retrieves the CAVLC residual using the specified parameters.
    /// </summary>
    /// <param name="reader">The bit stream reader.</param>
    /// <param name="coeffLevel">The coefficient levels.</param>
    /// <param name="startIdx">The start index.</param>
    /// <param name="endIdx">The end index.</param>
    /// <param name="maxNumCoeff">The maximum number of coefficients.</param>
    /// <param name="nalu">The NAL unit.</param>
    /// <param name="dc">The derivation context.</param>
    /// <param name="chromaArrayType">The chroma array type.</param>
    /// <param name="luma4x4BlkIdx">The luma 4x4 block index.</param>
    /// <param name="cb4x4BlkIdx">The chroma blue 4x4 block index.</param>
    /// <param name="cr4x4BlkIdx">The chroma red 4x4 block index.</param>
    /// <param name="chroma4x4BlkIdx">The chroma 4x4 block index.</param>
    /// <param name="mode">The residual mode.</param>
    /// <param name="util">The macroblock utility.</param>
    /// <param name="constrainedIntraPredFlag">The constrained intra prediction flag.</param>
    /// <returns>The retrieved <see cref="CavlcResidual"/>.</returns>
    public CavlcResidual Get(
        BitStreamReader reader,
        Span<uint> coeffLevel,
        int startIdx,
        int endIdx,
        int maxNumCoeff,
        NalUnit nalu,
        DerivationContext dc,
        int chromaArrayType,
        ref int luma4x4BlkIdx,
        ref int cb4x4BlkIdx,
        ref int cr4x4BlkIdx,
        int chroma4x4BlkIdx,
        ResidualMode mode,
        IMacroblockUtility util,
        bool constrainedIntraPredFlag)
    {
        ReaderState prev = reader.GetState();
        reader.GoTo(ReaderState);

        var result = CavlcResidual.Read(reader, coeffLevel, startIdx, endIdx, maxNumCoeff, nalu, dc, chromaArrayType, ref luma4x4BlkIdx, ref cb4x4BlkIdx, ref cr4x4BlkIdx, chroma4x4BlkIdx, mode, util, constrainedIntraPredFlag);

        reader.GoTo(prev);

        return result;
    }

    /// <summary>
    /// Determines whether the specified object is equal to this instance.
    /// </summary>
    /// <param name="obj">The object to compare with this instance.</param>
    /// <returns><c>true</c> if the objects are equal; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? obj)
    {
        return obj is CavlcResidualHandle handle && Equals(handle);
    }

    /// <summary>
    /// Determines whether the specified <see cref="CavlcResidualHandle"/> is equal to this instance.
    /// </summary>
    /// <param name="other">The other <see cref="CavlcResidualHandle"/> instance.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public bool Equals(CavlcResidualHandle other)
    {
        return ReaderState.Equals(other.ReaderState);
    }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>A hash code for this instance.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(ReaderState);
    }

    /// <summary>
    /// Determines whether two <see cref="CavlcResidualHandle"/> instances are equal.
    /// </summary>
    /// <param name="left">The left instance to compare.</param>
    /// <param name="right">The right instance to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(CavlcResidualHandle left, CavlcResidualHandle right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="CavlcResidualHandle"/> instances are not equal.
    /// </summary>
    /// <param name="left">The left instance to compare.</param>
    /// <param name="right">The right instance to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(CavlcResidualHandle left, CavlcResidualHandle right)
    {
        return !(left == right);
    }
}

/// <summary>
/// Represents a handle for Residual Luma.
/// </summary>
public readonly struct ResidualLumaHandle : IEquatable<ResidualLumaHandle>
{
    /// <summary>
    /// Gets the reader state associated with this handle.
    /// </summary>
    public ReaderState ReaderState { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResidualLumaHandle"/> struct.
    /// </summary>
    /// <param name="readerState">The reader state.</param>
    public ResidualLumaHandle(ReaderState readerState)
    {
        this.ReaderState = readerState;
    }

    /// <summary>
    /// Retrieves the Residual Luma using the specified parameters.
    /// </summary>
    /// <param name="reader">The bit stream reader.</param>
    /// <param name="entropyCodingMode">The entropy coding mode.</param>
    /// <param name="transformSize8x8Flag">Indicates whether 8x8 transform size is used.</param>
    /// <param name="mbType">The macroblock type.</param>
    /// <param name="codedBlockPatternLuma">The coded block pattern for luma.</param>
    /// <param name="chromaArrayType">The chroma array type.</param>
    /// <param name="i16x16DCLevel">The DC level for 16x16 blocks.</param>
    /// <param name="i16x16ACLevel">The AC level for 16x16 blocks.</param>
    /// <param name="level8x8">The level for 8x8 blocks.</param>
    /// <param name="level4x4">The level for 4x4 blocks.</param>
    /// <param name="sliceType">The slice type.</param>
    /// <param name="startIdx">The start index.</param>
    /// <param name="endIdx">The end index.</param>
    /// <param name="nalu">The NAL unit.</param>
    /// <param name="dc">The derivation context.</param>
    /// <param name="luma4x4BlkIdx">The luma 4x4 block index.</param>
    /// <param name="cb4x4BlkIdx">The chroma blue 4x4 block index.</param>
    /// <param name="cr4x4BlkIdx">The chroma red 4x4 block index.</param>
    /// <param name="chroma4x4BlkIdx">The chroma 4x4 block index.</param>
    /// <param name="util">The macroblock utility.</param>
    /// <param name="mode">The residual mode.</param>
    /// <param name="constrainedIntraPredFlag">The constrained intra prediction flag.</param>
    /// <returns>The retrieved <see cref="ResidualLuma"/>.</returns>
    public ResidualLuma Get(
        BitStreamReader reader,
        EntropyCodingMode entropyCodingMode,
        bool transformSize8x8Flag,
        int mbType,
        int codedBlockPatternLuma,
        int chromaArrayType,
        out Container64UInt32 i16x16DCLevel,
        out ContainerMatrix16x16 i16x16ACLevel,
        out ContainerMatrix4x64 level8x8,
        out ContainerMatrix4x64 level4x4,
        GeneralSliceType sliceType,
        int startIdx,
        int endIdx,
        NalUnit nalu,
        DerivationContext dc,
        ref int luma4x4BlkIdx,
        ref int cb4x4BlkIdx,
        ref int cr4x4BlkIdx,
        int chroma4x4BlkIdx,
        IMacroblockUtility util,
        ResidualMode mode,
        bool constrainedIntraPredFlag)
    {
        ReaderState prev = reader.GetState();
        reader.GoTo(ReaderState);

        var result = ResidualLuma.Read(reader, entropyCodingMode, transformSize8x8Flag, mbType, codedBlockPatternLuma, chromaArrayType, out i16x16DCLevel, out i16x16ACLevel, out level8x8, out level4x4, sliceType, startIdx, endIdx, nalu, dc, ref luma4x4BlkIdx, ref cb4x4BlkIdx, ref cr4x4BlkIdx, chroma4x4BlkIdx, util, mode, constrainedIntraPredFlag);

        reader.GoTo(prev);

        return result;
    }

    /// <summary>
    /// Determines whether the specified object is equal to this instance.
    /// </summary>
    /// <param name="obj">The object to compare with this instance.</param>
    /// <returns><c>true</c> if the objects are equal; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? obj)
    {
        return obj is ResidualLumaHandle handle && Equals(handle);
    }

    /// <summary>
    /// Determines whether the specified <see cref="ResidualLumaHandle"/> is equal to this instance.
    /// </summary>
    /// <param name="other">The other <see cref="ResidualLumaHandle"/> instance.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public bool Equals(ResidualLumaHandle other)
    {
        return ReaderState.Equals(other.ReaderState);
    }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>A hash code for this instance.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(ReaderState);
    }

    /// <summary>
    /// Determines whether two <see cref="ResidualLumaHandle"/> instances are equal.
    /// </summary>
    /// <param name="left">The left instance to compare.</param>
    /// <param name="right">The right instance to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(ResidualLumaHandle left, ResidualLumaHandle right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="ResidualLumaHandle"/> instances are not equal.
    /// </summary>
    /// <param name="left">The left instance to compare.</param>
    /// <param name="right">The right instance to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(ResidualLumaHandle left, ResidualLumaHandle right)
    {
        return !(left == right);
    }
}

/// <summary>
/// Represents a handle for a Sequence Parameter Set (SPS).
/// </summary>
public readonly struct SequenceParameterSetHandle : IEquatable<SequenceParameterSetHandle>
{
    /// <summary>
    /// Gets the reader state associated with this handle.
    /// </summary>
    public ReaderState ReaderState { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SequenceParameterSetHandle"/> struct.
    /// </summary>
    /// <param name="readerState">The reader state.</param>
    public SequenceParameterSetHandle(ReaderState readerState)
    {
        this.ReaderState = readerState;
    }

    /// <summary>
    /// Retrieves the Sequence Parameter Set (SPS) using the specified reader.
    /// </summary>
    /// <param name="reader">The bit stream reader.</param>
    /// <returns>The retrieved <see cref="SequenceParameterSet"/>.</returns>
    public SequenceParameterSet Get(BitStreamReader reader)
    {
        ReaderState prev = reader.GetState();
        reader.GoTo(ReaderState);

        var result = SequenceParameterSet.Read(reader);

        reader.GoTo(prev);

        return result;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is SequenceParameterSetHandle handle && Equals(handle);
    }

    /// <inheritdoc/>
    public bool Equals(SequenceParameterSetHandle other)
    {
        return ReaderState.Equals(other.ReaderState);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(ReaderState);
    }

    /// <summary>
    /// Determines whether two <see cref="SequenceParameterSetHandle"/> instances are equal.
    /// </summary>
    /// <param name="left">The left instance to compare.</param>
    /// <param name="right">The right instance to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(SequenceParameterSetHandle left, SequenceParameterSetHandle right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="SequenceParameterSetHandle"/> instances are not equal.
    /// </summary>
    /// <param name="left">The left instance to compare.</param>
    /// <param name="right">The right instance to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(SequenceParameterSetHandle left, SequenceParameterSetHandle right)
    {
        return !(left == right);
    }
}

/// <summary>
/// Represents a handle for a Slice Header in an H.264 bitstream.
/// </summary>
public readonly struct SliceHeaderHandle : IEquatable<SliceHeaderHandle>
{
    /// <summary>
    /// Gets the reader state associated with this handle.
    /// </summary>
    public ReaderState ReaderState { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SliceHeaderHandle"/> struct.
    /// </summary>
    /// <param name="readerState">The reader state.</param>
    public SliceHeaderHandle(ReaderState readerState)
    {
        this.ReaderState = readerState;
    }

    /// <summary>
    /// Retrieves the Slice Header using the specified parameters.
    /// </summary>
    /// <param name="reader">The bit stream reader.</param>
    /// <param name="nalu">The NAL unit containing the slice.</param>
    /// <param name="sps">The Sequence Parameter Set (SPS) associated with the slice.</param>
    /// <param name="pps">The Picture Parameter Set (PPS) associated with the slice.</param>
    /// <returns>The retrieved <see cref="SliceHeader"/>.</returns>
    public SliceHeader Get(BitStreamReader reader, NalUnit nalu, SequenceParameterSet sps, PictureParameterSet pps)
    {
        ReaderState prev = reader.GetState();
        reader.GoTo(ReaderState);

        var result = SliceHeader.Read(reader, nalu, sps, pps);

        reader.GoTo(prev);

        return result;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is SliceHeaderHandle handle && Equals(handle);
    }

    /// <inheritdoc/>
    public bool Equals(SliceHeaderHandle other)
    {
        return ReaderState.Equals(other.ReaderState);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(ReaderState);
    }

    /// <summary>
    /// Determines whether two <see cref="SliceHeaderHandle"/> instances are equal.
    /// </summary>
    /// <param name="left">The left instance to compare.</param>
    /// <param name="right">The right instance to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(SliceHeaderHandle left, SliceHeaderHandle right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="SliceHeaderHandle"/> instances are not equal.
    /// </summary>
    /// <param name="left">The left instance to compare.</param>
    /// <param name="right">The right instance to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(SliceHeaderHandle left, SliceHeaderHandle right)
    {
        return !(left == right);
    }
}

/// <summary>
/// Represents a handle for Video Usability Information (VUI) parameters.
/// </summary>
public readonly struct VuiParametersHandle : IEquatable<VuiParametersHandle>
{
    /// <summary>
    /// Gets the reader state associated with this handle.
    /// </summary>
    public ReaderState ReaderState { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="VuiParametersHandle"/> struct.
    /// </summary>
    /// <param name="readerState">The reader state.</param>
    public VuiParametersHandle(ReaderState readerState)
    {
        this.ReaderState = readerState;
    }

    /// <summary>
    /// Retrieves the VUI parameters using the specified reader.
    /// </summary>
    /// <param name="reader">The bit stream reader.</param>
    /// <returns>The retrieved <see cref="VuiParameters"/>.</returns>
    public VuiParameters Get(BitStreamReader reader)
    {
        ReaderState prev = reader.GetState();
        reader.GoTo(ReaderState);

        var result = VuiParameters.Read(reader);

        reader.GoTo(prev);

        return result;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is VuiParametersHandle handle && Equals(handle);
    }

    /// <inheritdoc/>
    public bool Equals(VuiParametersHandle other)
    {
        return ReaderState.Equals(other.ReaderState);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(ReaderState);
    }

    /// <summary>
    /// Determines whether two <see cref="VuiParametersHandle"/> instances are equal.
    /// </summary>
    /// <param name="left">The left instance to compare.</param>
    /// <param name="right">The right instance to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(VuiParametersHandle left, VuiParametersHandle right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="VuiParametersHandle"/> instances are not equal.
    /// </summary>
    /// <param name="left">The left instance to compare.</param>
    /// <param name="right">The right instance to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(VuiParametersHandle left, VuiParametersHandle right)
    {
        return !(left == right);
    }
}

/// <summary>
/// Represents a handle for Sub Macroblock Prediction.
/// </summary>
public readonly struct SubMacroblockPredictionHandle : IEquatable<SubMacroblockPredictionHandle>
{
    /// <summary>
    /// Gets the reader state associated with this handle.
    /// </summary>
    public ReaderState ReaderState { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SubMacroblockPredictionHandle"/> struct.
    /// </summary>
    /// <param name="readerState">The reader state.</param>
    public SubMacroblockPredictionHandle(ReaderState readerState)
    {
        this.ReaderState = readerState;
    }

    /// <summary>
    /// Retrieves the sub macroblock prediction using the specified reader.
    /// </summary>
    /// <param name="reader">The bit stream reader.</param>
    /// <returns>The retrieved <see cref="SubMacroblockPrediction"/>.</returns>
#pragma warning disable CS1573
    public SubMacroblockPrediction Get(BitStreamReader reader, EntropyCodingMode entropyCodingMode, int numRefIdxL0ActiveMinus1, int numRefIdxL1ActiveMinus1, bool mbFieldDecodingFlag, bool fieldPicFlag, GeneralSliceType sliceType, int mbType, bool mbaffFrameFlag)
    {
        ReaderState prev = reader.GetState();
        reader.GoTo(ReaderState);

        var result = SubMacroblockPrediction.Read(reader, mbaffFrameFlag, sliceType, entropyCodingMode, mbType, numRefIdxL0ActiveMinus1, numRefIdxL1ActiveMinus1, mbFieldDecodingFlag, fieldPicFlag);

        reader.GoTo(prev);

        return result;
    }
#pragma warning restore

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is SubMacroblockPredictionHandle handle && Equals(handle);
    }

    /// <inheritdoc/>
    public bool Equals(SubMacroblockPredictionHandle other)
    {
        return ReaderState.Equals(other.ReaderState);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(ReaderState);
    }

    /// <summary>
    /// Determines whether two <see cref="SubMacroblockPredictionHandle"/> instances are equal.
    /// </summary>
    /// <param name="left">The left instance to compare.</param>
    /// <param name="right">The right instance to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(SubMacroblockPredictionHandle left, SubMacroblockPredictionHandle right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="SubMacroblockPredictionHandle"/> instances are not equal.
    /// </summary>
    /// <param name="left">The left instance to compare.</param>
    /// <param name="right">The right instance to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(SubMacroblockPredictionHandle left, SubMacroblockPredictionHandle right)
    {
        return !(left == right);
    }
}

/// <summary>
/// Represents a handle for Macroblock Prediction.
/// </summary>
public readonly struct MacroblockPredictionHandle : IEquatable<MacroblockPredictionHandle>
{
    /// <summary>
    /// Gets the reader state associated with this handle.
    /// </summary>
    public ReaderState ReaderState { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MacroblockPredictionHandle"/> struct.
    /// </summary>
    /// <param name="readerState">The reader state.</param>
    public MacroblockPredictionHandle(ReaderState readerState)
    {
        this.ReaderState = readerState;
    }

    /// <summary>
    /// Retrieves the Macroblock prediction using the specified reader.
    /// </summary>
    /// <param name="reader">The bit stream reader.</param>
    /// <param name="mbType">The macroblock type.</param>
    /// <param name="mbaffFrameFlag">Indicates if MBAFF is used in the frame.</param>
    /// <param name="codingMode">The entropy coding mode (CAVLC or CABAC).</param>
    /// <param name="sliceType">The general slice type.</param>
    /// <param name="transformSize8x8Flag">Indicates if 8x8 transform size is used.</param>
    /// <param name="numRefIdxL0ActiveMinus1">The number of active reference indices for L0 minus 1.</param>
    /// <param name="numRefIdxL1ActiveMinus1">The number of active reference indices for L1 minus 1.</param>
    /// <param name="mbFieldDecodingFlag">Indicates if macroblock field decoding is used.</param>
    /// <param name="fieldPicFlag">Indicates if the picture is a field picture.</param>
    /// <param name="chromaArrayType">The chroma array type.</param>
    /// <returns>The retrieved <see cref="MacroblockPrediction"/>.</returns>
    public MacroblockPrediction Get(BitStreamReader reader, int mbType, bool mbaffFrameFlag, EntropyCodingMode codingMode, GeneralSliceType sliceType, bool transformSize8x8Flag, int numRefIdxL0ActiveMinus1, int numRefIdxL1ActiveMinus1, bool mbFieldDecodingFlag, bool fieldPicFlag, int chromaArrayType)
    {
        ReaderState prev = reader.GetState();
        reader.GoTo(ReaderState);

        var result = MacroblockPrediction.Read(reader, mbType, mbaffFrameFlag, codingMode, sliceType, transformSize8x8Flag, numRefIdxL0ActiveMinus1, numRefIdxL1ActiveMinus1, mbFieldDecodingFlag, fieldPicFlag, chromaArrayType);

        reader.GoTo(prev);

        return result;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is MacroblockPredictionHandle handle && Equals(handle);
    }

    /// <inheritdoc/>
    public bool Equals(MacroblockPredictionHandle other)
    {
        return ReaderState.Equals(other.ReaderState);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(ReaderState);
    }

    /// <summary>
    /// Determines whether two <see cref="MacroblockPredictionHandle"/> instances are equal.
    /// </summary>
    /// <param name="left">The left instance to compare.</param>
    /// <param name="right">The right instance to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(MacroblockPredictionHandle left, MacroblockPredictionHandle right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="MacroblockPredictionHandle"/> instances are not equal.
    /// </summary>
    /// <param name="left">The left instance to compare.</param>
    /// <param name="right">The right instance to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(MacroblockPredictionHandle left, MacroblockPredictionHandle right)
    {
        return !(left == right);
    }
}
