namespace ContentDotNet.Extensions.H264.Models.Svc;

/// <summary>
/// Represents the SVC (Scalable Video Coding) sequence parameter set for H.264.
/// </summary>
public struct SvcSequenceParameterSet : IEquatable<SvcSequenceParameterSet>
{
    /// <summary>
    /// Indicates whether the inter-layer deblocking filter control is present.
    /// </summary>
    public bool InterLayerDeblockingFilterControlPresentFlag;

    /// <summary>
    /// Specifies the extended spatial scalability identifier.
    /// </summary>
    public uint ExtendedSpatialScalabilityIdc;

    /// <summary>
    /// Indicates whether the chroma phase X plus 1 flag is set.
    /// </summary>
    public bool ChromaPhaseXPlus1Flag;

    /// <summary>
    /// Specifies the chroma phase Y plus 1 value.
    /// </summary>
    public uint ChromaPhaseYPlus1;

    /// <summary>
    /// Indicates whether the reference layer chroma phase X plus 1 flag is set.
    /// </summary>
    public bool SeqRefLayerChromaPhaseXPlus1Flag;

    /// <summary>
    /// Specifies the reference layer chroma phase Y plus 1 value.
    /// </summary>
    public uint SeqRefLayerChromaPhaseYPlus1;

    /// <summary>
    /// Specifies the left offset for the scaled reference layer.
    /// </summary>
    public int SeqScaledRefLayerLeftOffset;

    /// <summary>
    /// Specifies the top offset for the scaled reference layer.
    /// </summary>
    public int SeqScaledRefLayerTopOffset;

    /// <summary>
    /// Specifies the right offset for the scaled reference layer.
    /// </summary>
    public int SeqScaledRefLayerRightOffset;

    /// <summary>
    /// Specifies the bottom offset for the scaled reference layer.
    /// </summary>
    public int SeqScaledRefLayerBottomOffset;

    /// <summary>
    /// Indicates whether the sequence T-coefficient level prediction flag is set.
    /// </summary>
    public bool SeqTCoeffLevelPredictionFlag;

    /// <summary>
    /// Indicates whether adaptive T-coefficient level prediction is enabled.
    /// </summary>
    public bool AdaptiveTCoeffLevelPredictionFlag;

    /// <summary>
    /// Indicates whether slice header restrictions are applied.
    /// </summary>
    public bool SliceHeaderRestrictionFlag;

    /// <summary>
    /// Initializes a new instance of the <see cref="SvcSequenceParameterSet"/> struct.
    /// </summary>
    /// <param name="interLayerDeblockingFilterControlPresentFlag">Indicates if inter-layer deblocking filter control is present.</param>
    /// <param name="extendedSpatialScalabilityIdc">The extended spatial scalability identifier.</param>
    /// <param name="chromaPhaseXPlus1Flag">Indicates if chroma phase X plus 1 flag is set.</param>
    /// <param name="chromaPhaseYPlus1">The chroma phase Y plus 1 value.</param>
    /// <param name="seqRefLayerChromaPhaseXPlus1Flag">Indicates if reference layer chroma phase X plus 1 flag is set.</param>
    /// <param name="seqRefLayerChromaPhaseYPlus1">The reference layer chroma phase Y plus 1 value.</param>
    /// <param name="seqScaledRefLayerLeftOffset">The left offset for the scaled reference layer.</param>
    /// <param name="seqScaledRefLayerTopOffset">The top offset for the scaled reference layer.</param>
    /// <param name="seqScaledRefLayerRightOffset">The right offset for the scaled reference layer.</param>
    /// <param name="seqScaledRefLayerBottomOffset">The bottom offset for the scaled reference layer.</param>
    /// <param name="seqTCoeffLevelPredictionFlag">Indicates if sequence T-coefficient level prediction flag is set.</param>
    /// <param name="adaptiveTCoeffLevelPredictionFlag">Indicates if adaptive T-coefficient level prediction is enabled.</param>
    /// <param name="sliceHeaderRestrictionFlag">Indicates if slice header restrictions are applied.</param>
    public SvcSequenceParameterSet(
        bool interLayerDeblockingFilterControlPresentFlag,
        uint extendedSpatialScalabilityIdc,
        bool chromaPhaseXPlus1Flag,
        uint chromaPhaseYPlus1,
        bool seqRefLayerChromaPhaseXPlus1Flag,
        uint seqRefLayerChromaPhaseYPlus1,
        int seqScaledRefLayerLeftOffset,
        int seqScaledRefLayerTopOffset,
        int seqScaledRefLayerRightOffset,
        int seqScaledRefLayerBottomOffset,
        bool seqTCoeffLevelPredictionFlag,
        bool adaptiveTCoeffLevelPredictionFlag,
        bool sliceHeaderRestrictionFlag)
    {
        InterLayerDeblockingFilterControlPresentFlag = interLayerDeblockingFilterControlPresentFlag;
        ExtendedSpatialScalabilityIdc = extendedSpatialScalabilityIdc;
        ChromaPhaseXPlus1Flag = chromaPhaseXPlus1Flag;
        ChromaPhaseYPlus1 = chromaPhaseYPlus1;
        SeqRefLayerChromaPhaseXPlus1Flag = seqRefLayerChromaPhaseXPlus1Flag;
        SeqRefLayerChromaPhaseYPlus1 = seqRefLayerChromaPhaseYPlus1;
        SeqScaledRefLayerLeftOffset = seqScaledRefLayerLeftOffset;
        SeqScaledRefLayerTopOffset = seqScaledRefLayerTopOffset;
        SeqScaledRefLayerRightOffset = seqScaledRefLayerRightOffset;
        SeqScaledRefLayerBottomOffset = seqScaledRefLayerBottomOffset;
        SeqTCoeffLevelPredictionFlag = seqTCoeffLevelPredictionFlag;
        AdaptiveTCoeffLevelPredictionFlag = adaptiveTCoeffLevelPredictionFlag;
        SliceHeaderRestrictionFlag = sliceHeaderRestrictionFlag;
    }

    /// <summary>
    ///   Reads the SVC SPS from the bitstream.
    /// </summary>
    /// <param name="reader">Bitstream reader where the SPS is read from.</param>
    /// <param name="chromaArrayType">Chroma array type</param>
    /// <returns>An SPS for SVC</returns>
    public static SvcSequenceParameterSet Read(BitStreamReader reader, int chromaArrayType)
    {
        bool interLayerDeblockingFilterControlPresentFlag = reader.ReadBit();
        uint extendedSpatialScalabilityIdc = reader.ReadBits(2);

        bool chromaPhaseXPlus1Flag = false;
        uint chromaPhaseYPlus1 = 0u;

        if (chromaArrayType is 1 or 2)
            chromaPhaseXPlus1Flag = reader.ReadBit();

        if (chromaArrayType == 1)
            chromaPhaseYPlus1 = reader.ReadBits(2);

        bool seqRefLayerChromaPhaseXPlus1Flag = false;
        uint seqRefLayerChromaPhaseXPlus1 = 0u;
        int seqScaledRefLayerLeftOffset = 0;
        int seqScaledRefLayerTopOffset = 0;
        int seqScaledRefLayerRightOffset = 0;
        int seqScaledRefLayerBottomOffset = 0;
        bool seqTCoeffLevelPredictionFlag;
        bool adaptiveTCoeffLevelPredictionFlag = false;
        bool sliceHeaderRestrictionFlag;

        if (extendedSpatialScalabilityIdc == 1)
        {
            if (chromaArrayType > 0)
            {
                seqRefLayerChromaPhaseXPlus1Flag = reader.ReadBit();
                seqRefLayerChromaPhaseXPlus1 = reader.ReadBits(2);
            }

            seqScaledRefLayerLeftOffset = reader.ReadSE();
            seqScaledRefLayerTopOffset = reader.ReadSE();
            seqScaledRefLayerRightOffset = reader.ReadSE();
            seqScaledRefLayerBottomOffset = reader.ReadSE();
        }

        seqTCoeffLevelPredictionFlag = reader.ReadBit();
        if (seqTCoeffLevelPredictionFlag)
            adaptiveTCoeffLevelPredictionFlag = reader.ReadBit();

        sliceHeaderRestrictionFlag = reader.ReadBit();

        return new SvcSequenceParameterSet(interLayerDeblockingFilterControlPresentFlag, extendedSpatialScalabilityIdc,
            chromaPhaseXPlus1Flag, chromaPhaseYPlus1, seqRefLayerChromaPhaseXPlus1Flag, seqRefLayerChromaPhaseXPlus1,
            seqScaledRefLayerLeftOffset, seqScaledRefLayerTopOffset, seqScaledRefLayerRightOffset, seqScaledRefLayerBottomOffset,
            seqTCoeffLevelPredictionFlag, adaptiveTCoeffLevelPredictionFlag, sliceHeaderRestrictionFlag);
    }

    /// <summary>
    ///   Writes the SVC SPS to the bitstream.
    /// </summary>
    /// <param name="writer">Bitstream where the SVC SPS is written to.</param>
    /// <param name="chromaArrayType">Chroma array type</param>
    public readonly void Write(BitStreamWriter writer, int chromaArrayType)
    {
        writer.WriteBit(InterLayerDeblockingFilterControlPresentFlag);
        writer.WriteBits(ExtendedSpatialScalabilityIdc, 2);

        if (chromaArrayType is 1 or 2)
            writer.WriteBit(ChromaPhaseXPlus1Flag);

        if (chromaArrayType == 1)
            writer.WriteBits(ChromaPhaseYPlus1, 2);

        if (ExtendedSpatialScalabilityIdc == 1)
        {
            if (chromaArrayType > 0)
            {
                writer.WriteBit(SeqRefLayerChromaPhaseXPlus1Flag);
                writer.WriteBits(SeqRefLayerChromaPhaseYPlus1, 2);
            }

            writer.WriteSE(SeqScaledRefLayerLeftOffset);
            writer.WriteSE(SeqScaledRefLayerTopOffset);
            writer.WriteSE(SeqScaledRefLayerRightOffset);
            writer.WriteSE(SeqScaledRefLayerBottomOffset);
        }

        writer.WriteBit(SeqTCoeffLevelPredictionFlag);

        if (SeqTCoeffLevelPredictionFlag)
            writer.WriteBit(AdaptiveTCoeffLevelPredictionFlag);

        writer.WriteBit(SliceHeaderRestrictionFlag);
    }

    /// <summary>
    ///   Writes the SVC SPS to the bitstream.
    /// </summary>
    /// <param name="writer">Bitstream where the SVC SPS is written to.</param>
    /// <param name="chromaArrayType">Chroma array type</param>
    public readonly async Task WriteAsync(BitStreamWriter writer, int chromaArrayType)
    {
        await writer.WriteBitAsync(InterLayerDeblockingFilterControlPresentFlag);
        await writer.WriteBitsAsync(ExtendedSpatialScalabilityIdc, 2);

        if (chromaArrayType is 1 or 2)
            await writer.WriteBitAsync(ChromaPhaseXPlus1Flag);

        if (chromaArrayType == 1)
            await writer.WriteBitsAsync(ChromaPhaseYPlus1, 2);

        if (ExtendedSpatialScalabilityIdc == 1)
        {
            if (chromaArrayType > 0)
            {
                await writer.WriteBitAsync(SeqRefLayerChromaPhaseXPlus1Flag);
                await writer.WriteBitsAsync(SeqRefLayerChromaPhaseYPlus1, 2);
            }

            await writer.WriteSEAsync(SeqScaledRefLayerLeftOffset);
            await writer.WriteSEAsync(SeqScaledRefLayerTopOffset);
            await writer.WriteSEAsync(SeqScaledRefLayerRightOffset);
            await writer.WriteSEAsync(SeqScaledRefLayerBottomOffset);
        }

        await writer.WriteBitAsync(SeqTCoeffLevelPredictionFlag);

        if (SeqTCoeffLevelPredictionFlag)
            await writer.WriteBitAsync(AdaptiveTCoeffLevelPredictionFlag);

        await writer.WriteBitAsync(SliceHeaderRestrictionFlag);
    }

    /// <inheritdoc/>
    public readonly override bool Equals(object? obj)
    {
        return obj is SvcSequenceParameterSet set && Equals(set);
    }

    /// <inheritdoc/>
    public readonly bool Equals(SvcSequenceParameterSet other)
    {
        return InterLayerDeblockingFilterControlPresentFlag == other.InterLayerDeblockingFilterControlPresentFlag &&
               ExtendedSpatialScalabilityIdc == other.ExtendedSpatialScalabilityIdc &&
               ChromaPhaseXPlus1Flag == other.ChromaPhaseXPlus1Flag &&
               ChromaPhaseYPlus1 == other.ChromaPhaseYPlus1 &&
               SeqRefLayerChromaPhaseXPlus1Flag == other.SeqRefLayerChromaPhaseXPlus1Flag &&
               SeqRefLayerChromaPhaseYPlus1 == other.SeqRefLayerChromaPhaseYPlus1 &&
               SeqScaledRefLayerLeftOffset == other.SeqScaledRefLayerLeftOffset &&
               SeqScaledRefLayerTopOffset == other.SeqScaledRefLayerTopOffset &&
               SeqScaledRefLayerRightOffset == other.SeqScaledRefLayerRightOffset &&
               SeqScaledRefLayerBottomOffset == other.SeqScaledRefLayerBottomOffset &&
               SeqTCoeffLevelPredictionFlag == other.SeqTCoeffLevelPredictionFlag &&
               AdaptiveTCoeffLevelPredictionFlag == other.AdaptiveTCoeffLevelPredictionFlag &&
               SliceHeaderRestrictionFlag == other.SliceHeaderRestrictionFlag;
    }

    /// <inheritdoc/>
    public readonly override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(InterLayerDeblockingFilterControlPresentFlag);
        hash.Add(ExtendedSpatialScalabilityIdc);
        hash.Add(ChromaPhaseXPlus1Flag);
        hash.Add(ChromaPhaseYPlus1);
        hash.Add(SeqRefLayerChromaPhaseXPlus1Flag);
        hash.Add(SeqRefLayerChromaPhaseYPlus1);
        hash.Add(SeqScaledRefLayerLeftOffset);
        hash.Add(SeqScaledRefLayerTopOffset);
        hash.Add(SeqScaledRefLayerRightOffset);
        hash.Add(SeqScaledRefLayerBottomOffset);
        hash.Add(SeqTCoeffLevelPredictionFlag);
        hash.Add(AdaptiveTCoeffLevelPredictionFlag);
        hash.Add(SliceHeaderRestrictionFlag);
        return hash.ToHashCode();
    }

    /// <summary>
    /// Determines whether two <see cref="SvcSequenceParameterSet"/> instances are equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(SvcSequenceParameterSet left, SvcSequenceParameterSet right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="SvcSequenceParameterSet"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(SvcSequenceParameterSet left, SvcSequenceParameterSet right)
    {
        return !(left == right);
    }
}
