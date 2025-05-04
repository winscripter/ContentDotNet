namespace ContentDotNet.Extensions.H264.Models;

/// <summary>
/// Represents an entry with flag, weight, and offset information for prediction weight tables.
/// </summary>
public struct PredWeightTableWeightOffsetEntry : IEquatable<PredWeightTableWeightOffsetEntry>
{
    /// <summary>
    /// Gets or sets the flag indicating whether weight and offset should be applied.
    /// </summary>
    public bool Flag; // Applies to L0 or L1 depending on the context

    /// <summary>
    /// Gets or sets the weight value, applicable if the flag is set to true.
    /// </summary>
    public int Weight;

    /// <summary>
    /// Gets or sets the offset value, applicable if the flag is set to true.
    /// </summary>
    public int Offset;

    /// <summary>
    /// Initializes a new instance of the <see cref="PredWeightTableWeightOffsetEntry"/> struct.
    /// </summary>
    /// <param name="flag">The flag indicating application context.</param>
    /// <param name="weight">The weight value.</param>
    /// <param name="offset">The offset value.</param>
    public PredWeightTableWeightOffsetEntry(bool flag, int weight, int offset)
    {
        Flag = flag;
        Weight = weight;
        Offset = offset;
    }

    /// <summary>
    /// Reads a <see cref="PredWeightTableWeightOffsetEntry"/> from the specified <see cref="BitStreamReader"/>.
    /// </summary>
    /// <param name="reader">The reader from which the entry is read.</param>
    /// <returns>The <see cref="PredWeightTableWeightOffsetEntry"/> read from the stream.</returns>
    public static PredWeightTableWeightOffsetEntry Read(BitStreamReader reader)
    {
        bool flag = reader.ReadBit();
        int weight = 0;
        int offset = 0;

        if (flag)
        {
            weight = reader.ReadSE();
            offset = reader.ReadSE();
        }

        return new PredWeightTableWeightOffsetEntry(flag, weight, offset);
    }

    /// <summary>
    /// Determines whether the current instance is equal to a specified object.
    /// </summary>
    /// <param name="obj">The object to compare.</param>
    /// <returns>True if equal; otherwise, false.</returns>
    public readonly override bool Equals(object? obj)
    {
        return obj is PredWeightTableWeightOffsetEntry entry && Equals(entry);
    }

    /// <summary>
    /// Determines whether the current instance is equal to another <see cref="PredWeightTableWeightOffsetEntry"/>.
    /// </summary>
    /// <param name="other">The other instance to compare.</param>
    /// <returns>True if equal; otherwise, false.</returns>
    public readonly bool Equals(PredWeightTableWeightOffsetEntry other)
    {
        return Flag == other.Flag &&
               Weight == other.Weight &&
               Offset == other.Offset;
    }

    /// <summary>
    /// Returns the hash code for the current instance.
    /// </summary>
    /// <returns>A hash code for the current instance.</returns>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(Flag, Weight, Offset);
    }

    /// <summary>
    /// Writes the current instance to the specified <see cref="BitStreamWriter"/>.
    /// </summary>
    /// <param name="writer">The writer to which the instance is written.</param>
    public readonly void Write(BitStreamWriter writer)
    {
        writer.WriteBit(Flag);
        if (Flag)
        {
            writer.WriteSE(Weight);
            writer.WriteSE(Offset);
        }
    }

    /// <summary>
    /// Asynchronously writes the current instance to the specified <see cref="BitStreamWriter"/>.
    /// </summary>
    /// <param name="writer">The writer to which the instance is written.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public readonly async Task WriteAsync(BitStreamWriter writer)
    {
        await writer.WriteBitAsync(Flag);
        if (Flag)
        {
            await writer.WriteSEAsync(Weight);
            await writer.WriteSEAsync(Offset);
        }
    }

    /// <summary>
    /// Determines whether two instances of <see cref="PredWeightTableWeightOffsetEntry"/> are equal.
    /// </summary>
    /// <param name="left">The first instance.</param>
    /// <param name="right">The second instance.</param>
    /// <returns>True if equal; otherwise, false.</returns>
    public static bool operator ==(PredWeightTableWeightOffsetEntry left, PredWeightTableWeightOffsetEntry right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two instances of <see cref="PredWeightTableWeightOffsetEntry"/> are not equal.
    /// </summary>
    /// <param name="left">The first instance.</param>
    /// <param name="right">The second instance.</param>
    /// <returns>True if not equal; otherwise, false.</returns>
    public static bool operator !=(PredWeightTableWeightOffsetEntry left, PredWeightTableWeightOffsetEntry right)
    {
        return !(left == right);
    }
}

/// <summary>
///   A single list (L0 or L1) of the prediction weight table.
/// </summary>
public struct PredWeightTableList : IEquatable<PredWeightTableList>
{
    /// <summary>
    /// The offset in the reader state.
    /// </summary>
    public ReaderState Offset;

    /// <summary>
    /// The count of elements in the list.
    /// </summary>
    public int Count;

    /// <summary>
    /// Initializes a new instance of the <see cref="PredWeightTableList"/> struct.
    /// </summary>
    /// <param name="offset">The offset in the reader state.</param>
    /// <param name="count">The count of elements.</param>
    public PredWeightTableList(ReaderState offset, int count)
    {
        Offset = offset;
        Count = count;
    }

    /// <summary>
    /// Retrieves an element from the list.
    /// </summary>
    /// <param name="originalReader">The bit stream reader.</param>
    /// <param name="index">The index of the element.</param>
    /// <param name="chromaArrayType">The chroma array type.</param>
    /// <returns>A tuple containing luma and chroma entries.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the index exceeds the count.</exception>
    public readonly (PredWeightTableWeightOffsetEntry luma, PredWeightTableWeightOffsetEntry chroma1, PredWeightTableWeightOffsetEntry chroma2) GetElement(BitStreamReader originalReader, int index, int chromaArrayType)
    {
        if (index + 1 > Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        PredWeightTableWeightOffsetEntry chroma1 = default;
        PredWeightTableWeightOffsetEntry chroma2 = default;

        ReaderState prev = originalReader.GetState();
        originalReader.GoTo(this.Offset);

        PredWeightTableWeightOffsetEntry luma = PredWeightTableWeightOffsetEntry.Read(originalReader);
        if (chromaArrayType != 0)
        {
            chroma1 = PredWeightTableWeightOffsetEntry.Read(originalReader);
            chroma2 = PredWeightTableWeightOffsetEntry.Read(originalReader);
        }

        originalReader.GoTo(prev);
        return (luma, chroma1, chroma2);
    }

    /// <summary>
    /// Reads a prediction weight table list from the stream.
    /// </summary>
    /// <param name="reader">The bit stream reader.</param>
    /// <param name="chromaArrayType">The chroma array type.</param>
    /// <param name="numRefIdxLActiveMinus1">The number of active references minus one.</param>
    /// <returns>A <see cref="PredWeightTableList"/> instance.</returns>
    public static PredWeightTableList Read(BitStreamReader reader, int chromaArrayType, int numRefIdxLActiveMinus1)
    {
        ReaderState poolOffset = reader.GetState();

        for (int i = 0; i <= numRefIdxLActiveMinus1; i++)
        {
            bool lumaWeightFlag = reader.ReadBit();
            if (lumaWeightFlag)
            {
                _ = reader.ReadSE();
                _ = reader.ReadSE();
            }

            if (chromaArrayType != 0)
            {
                bool chromaWeightFlag = reader.ReadBit();
                if (chromaWeightFlag)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        _ = reader.ReadSE();
                        _ = reader.ReadSE();
                    }
                }
            }
        }

        return new PredWeightTableList(poolOffset, numRefIdxLActiveMinus1 + 1);
    }

    /// <summary>
    /// Writes a prediction weight table list to the stream.
    /// </summary>
    /// <param name="writer">The bit stream writer.</param>
    /// <param name="chromaArrayType">The chroma array type.</param>
    /// <param name="numRefIdxL0ActiveMinus1">The number of active references minus one.</param>
    /// <param name="includeLuma">Flags indicating inclusion of luma entries.</param>
    /// <param name="includeChroma">Flags indicating inclusion of chroma entries.</param>
    /// <param name="luma">The luma entries.</param>
    /// <param name="chroma">The chroma entries.</param>
    public static void Write(
        BitStreamWriter writer,
        int chromaArrayType,
        int numRefIdxL0ActiveMinus1,
        ReadOnlySpan<bool> includeLuma,
        ReadOnlySpan<bool> includeChroma,
        ReadOnlySpan<PredWeightTableWeightOffsetEntry> luma,
        ReadOnlySpan<(PredWeightTableWeightOffsetEntry cb, PredWeightTableWeightOffsetEntry cr)> chroma)
    {
        for (int i = 0; i <= numRefIdxL0ActiveMinus1; i++)
        {
            writer.WriteBit(includeLuma[i]);
            if (includeLuma[i])
            {
                PredWeightTableWeightOffsetEntry lumaChannel = luma[i];
                writer.WriteSE(lumaChannel.Weight);
                writer.WriteSE(lumaChannel.Offset);
            }

            if (chromaArrayType != 0)
            {
                writer.WriteBit(includeChroma[i]);
                if (includeChroma[i])
                {
                    PredWeightTableWeightOffsetEntry cb = chroma[i].cb;
                    PredWeightTableWeightOffsetEntry cr = chroma[i].cr;

                    writer.WriteSE(cb.Weight);
                    writer.WriteSE(cb.Offset);
                    writer.WriteSE(cr.Weight);
                    writer.WriteSE(cr.Offset);
                }
            }
        }
    }


    /// <summary>
    ///   Writes the prediction weight table list.
    /// </summary>
    /// <param name="writer">Bitstream writer where the list is written to.</param>
    /// <param name="chromaArrayType">Chroma Array Type should be taken from the SPS.</param>
    /// <param name="numRefIdxLActiveMinus1">Should be taken from the PPS.</param>
    /// <param name="options">Options for writing the prediction weight table list.</param>
    public static void Write(
        BitStreamWriter writer,
        int chromaArrayType,
        int numRefIdxLActiveMinus1,
        PredWeightTableListWriteOptions options)
        => Write(writer, chromaArrayType, numRefIdxLActiveMinus1, options.IncludeLuma, options.IncludeChroma, options.Luma, options.Chroma);

    /// <summary>
    ///   Writes the prediction weight table list.
    /// </summary>
    /// <param name="writer">Bitstream writer where the list is written to.</param>
    /// <param name="chromaArrayType">Chroma Array Type should be taken from the SPS.</param>
    /// <param name="numRefIdxLActiveMinus1">Should be taken from the PPS.</param>
    /// <param name="options">Options for writing the prediction weight table list.</param>
    public static void Write(
        BitStreamWriter writer,
        int chromaArrayType,
        int numRefIdxLActiveMinus1,
        MemoryPredWeightTableListWriteOptions options)
        => Write(writer, chromaArrayType, numRefIdxLActiveMinus1, options.IncludeLuma.Span, options.IncludeChroma.Span, options.Luma.Span, options.Chroma.Span);

    /// <summary>
    /// Writes a prediction weight table list to the stream.
    /// </summary>
    /// <param name="writer">The bit stream writer.</param>
    /// <param name="chromaArrayType">The chroma array type.</param>
    /// <param name="numRefIdxL0ActiveMinus1">The number of active references minus one.</param>
    /// <param name="includeLuma">Flags indicating inclusion of luma entries.</param>
    /// <param name="includeChroma">Flags indicating inclusion of chroma entries.</param>
    /// <param name="luma">The luma entries.</param>
    /// <param name="chroma">The chroma entries.</param>
    public static async Task WriteAsync(
        BitStreamWriter writer,
        int chromaArrayType,
        int numRefIdxL0ActiveMinus1,
        ReadOnlyMemory<bool> includeLuma,
        ReadOnlyMemory<bool> includeChroma,
        ReadOnlyMemory<PredWeightTableWeightOffsetEntry> luma,
        ReadOnlyMemory<(PredWeightTableWeightOffsetEntry cb, PredWeightTableWeightOffsetEntry cr)> chroma)
    {
        for (int i = 0; i <= numRefIdxL0ActiveMinus1; i++)
        {
            await writer.WriteBitAsync(includeLuma.Span[i]);
            if (includeLuma.Span[i])
            {
                PredWeightTableWeightOffsetEntry lumaChannel = luma.Span[i];
                await writer.WriteSEAsync(lumaChannel.Weight);
                await writer.WriteSEAsync(lumaChannel.Offset);
            }

            if (chromaArrayType != 0)
            {
                await writer.WriteBitAsync(includeChroma.Span[i]);
                if (includeChroma.Span[i])
                {
                    PredWeightTableWeightOffsetEntry cb = chroma.Span[i].cb;
                    PredWeightTableWeightOffsetEntry cr = chroma.Span[i].cr;

                    await writer.WriteSEAsync(cb.Weight);
                    await writer.WriteSEAsync(cb.Offset);
                    await writer.WriteSEAsync(cr.Weight);
                    await writer.WriteSEAsync(cr.Offset);
                }
            }
        }
    }

    /// <summary>
    ///   Writes the prediction weight table list.
    /// </summary>
    /// <param name="writer">Bitstream writer where the list is written to.</param>
    /// <param name="chromaArrayType">Chroma Array Type should be taken from the SPS.</param>
    /// <param name="numRefIdxLActiveMinus1">Should be taken from the PPS.</param>
    /// <param name="options">Options for writing the prediction weight table list.</param>
    public static async Task WriteAsync(
        BitStreamWriter writer,
        int chromaArrayType,
        int numRefIdxLActiveMinus1,
        MemoryPredWeightTableListWriteOptions options)
        => await WriteAsync(writer, chromaArrayType, numRefIdxLActiveMinus1, options.IncludeLuma, options.IncludeChroma, options.Luma, options.Chroma);
    
    /// <summary>
    /// Checks whether the current object is equal to another <see cref="PredWeightTableList"/> object.
    /// </summary>
    /// <param name="obj">The object to compare with.</param>
    /// <returns>True if the objects are equal; otherwise, false.</returns>
    public readonly override bool Equals(object? obj)
    {
        return obj is PredWeightTableList list && Equals(list);
    }

    /// <summary>
    /// Checks whether the current object is equal to another <see cref="PredWeightTableList"/> instance.
    /// </summary>
    /// <param name="other">The instance to compare with.</param>
    /// <returns>True if the instances are equal; otherwise, false.</returns>
    public readonly bool Equals(PredWeightTableList other)
    {
        return EqualityComparer<ReaderState>.Default.Equals(Offset, other.Offset) &&
               Count == other.Count;
    }

    /// <summary>
    /// Gets the hash code for the current object.
    /// </summary>
    /// <returns>The hash code.</returns>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(Offset, Count);
    }


    /// <summary>  
    /// Determines whether two instances of <see cref="PredWeightTableList"/> are equal.  
    /// </summary>  
    /// <param name="left">The first instance to compare.</param>  
    /// <param name="right">The second instance to compare.</param>  
    /// <returns>True if the instances are equal; otherwise, false.</returns>  
    public static bool operator ==(PredWeightTableList left, PredWeightTableList right)
    {
        return left.Equals(right);
    }

    /// <summary>  
    /// Determines whether two instances of <see cref="PredWeightTableList"/> are not equal.  
    /// </summary>  
    /// <param name="left">The first instance to compare.</param>  
    /// <param name="right">The second instance to compare.</param>  
    /// <returns>True if the instances are not equal; otherwise, false.</returns>  
    public static bool operator !=(PredWeightTableList left, PredWeightTableList right)
    {
        return !(left == right);
    }
}

/// <summary>
/// Represents the write options for a prediction weight table list using spans.
/// </summary>
public ref struct PredWeightTableListWriteOptions
{
    /// <summary>
    /// Flags indicating inclusion of luma entries.
    /// </summary>
    public ReadOnlySpan<bool> IncludeLuma;

    /// <summary>
    /// Flags indicating inclusion of chroma entries.
    /// </summary>
    public ReadOnlySpan<bool> IncludeChroma;

    /// <summary>
    /// The luma entries.
    /// </summary>
    public ReadOnlySpan<PredWeightTableWeightOffsetEntry> Luma;

    /// <summary>
    /// The chroma entries, consisting of Cb and Cr channels.
    /// </summary>
    public ReadOnlySpan<(PredWeightTableWeightOffsetEntry cb, PredWeightTableWeightOffsetEntry cr)> Chroma;

    /// <summary>
    /// Initializes a new instance of the <see cref="PredWeightTableListWriteOptions"/> struct.
    /// </summary>
    /// <param name="includeLuma">Flags indicating inclusion of luma entries.</param>
    /// <param name="includeChroma">Flags indicating inclusion of chroma entries.</param>
    /// <param name="luma">The luma entries.</param>
    /// <param name="chroma">The chroma entries.</param>
    public PredWeightTableListWriteOptions(
        ReadOnlySpan<bool> includeLuma,
        ReadOnlySpan<bool> includeChroma,
        ReadOnlySpan<PredWeightTableWeightOffsetEntry> luma,
        ReadOnlySpan<(PredWeightTableWeightOffsetEntry cb, PredWeightTableWeightOffsetEntry cr)> chroma)
    {
        IncludeLuma = includeLuma;
        IncludeChroma = includeChroma;
        Luma = luma;
        Chroma = chroma;
    }
}

/// <summary>
/// Represents the write options for a prediction weight table list using memory.
/// </summary>
public struct MemoryPredWeightTableListWriteOptions
{
    /// <summary>
    /// Flags indicating inclusion of luma entries.
    /// </summary>
    public ReadOnlyMemory<bool> IncludeLuma;

    /// <summary>
    /// Flags indicating inclusion of chroma entries.
    /// </summary>
    public ReadOnlyMemory<bool> IncludeChroma;

    /// <summary>
    /// The luma entries.
    /// </summary>
    public ReadOnlyMemory<PredWeightTableWeightOffsetEntry> Luma;

    /// <summary>
    /// The chroma entries, consisting of Cb and Cr channels.
    /// </summary>
    public ReadOnlyMemory<(PredWeightTableWeightOffsetEntry cb, PredWeightTableWeightOffsetEntry cr)> Chroma;

    /// <summary>
    /// Initializes a new instance of the <see cref="MemoryPredWeightTableListWriteOptions"/> struct.
    /// </summary>
    /// <param name="includeLuma">Flags indicating inclusion of luma entries.</param>
    /// <param name="includeChroma">Flags indicating inclusion of chroma entries.</param>
    /// <param name="luma">The luma entries.</param>
    /// <param name="chroma">The chroma entries.</param>
    public MemoryPredWeightTableListWriteOptions(
        ReadOnlyMemory<bool> includeLuma,
        ReadOnlyMemory<bool> includeChroma,
        ReadOnlyMemory<PredWeightTableWeightOffsetEntry> luma,
        ReadOnlyMemory<(PredWeightTableWeightOffsetEntry cb, PredWeightTableWeightOffsetEntry cr)> chroma)
    {
        IncludeLuma = includeLuma;
        IncludeChroma = includeChroma;
        Luma = luma;
        Chroma = chroma;
    }
}

/// <summary>
/// Represents a prediction weight table containing luma and chroma weight denominators and their respective lists.
/// </summary>
public struct PredWeightTable : IEquatable<PredWeightTable>
{
    /// <summary>
    /// The luma log2 weight denominator.
    /// </summary>
    public uint LumaLog2WeightDenom;

    /// <summary>
    /// The chroma log2 weight denominator.
    /// </summary>
    public uint ChromaLog2WeightDenom;

    /// <summary>
    /// The list of weights for reference pictures in list 0 (L0).
    /// </summary>
    public PredWeightTableList L0;

    /// <summary>
    /// The optional list of weights for reference pictures in list 1 (L1).
    /// </summary>
    public PredWeightTableList? L1;

    /// <summary>
    /// Initializes a new instance of the <see cref="PredWeightTable"/> struct.
    /// </summary>
    /// <param name="lumaLog2WeightDenom">The luma log2 weight denominator.</param>
    /// <param name="chromaLog2WeightDenom">The chroma log2 weight denominator.</param>
    /// <param name="l0">The list of weights for reference pictures in L0.</param>
    /// <param name="l1">The optional list of weights for reference pictures in L1.</param>
    public PredWeightTable(uint lumaLog2WeightDenom, uint chromaLog2WeightDenom, PredWeightTableList l0, PredWeightTableList? l1)
    {
        LumaLog2WeightDenom = lumaLog2WeightDenom;
        ChromaLog2WeightDenom = chromaLog2WeightDenom;
        L0 = l0;
        L1 = l1;
    }

    /// <summary>
    /// Reads a prediction weight table from the bitstream.
    /// </summary>
    /// <param name="reader">The bitstream reader.</param>
    /// <param name="chromaArrayType">The chroma array type.</param>
    /// <param name="sliceType">The type of the slice.</param>
    /// <param name="numRefIdxL0ActiveMinus1">The number of active references in L0 minus one.</param>
    /// <param name="numRefIdxL1ActiveMinus1">The number of active references in L1 minus one.</param>
    /// <returns>A <see cref="PredWeightTable"/> instance.</returns>
    public static PredWeightTable Read(BitStreamReader reader, int chromaArrayType, int sliceType, int numRefIdxL0ActiveMinus1, int numRefIdxL1ActiveMinus1)
    {
        uint lumaLog2WeightDenom = reader.ReadUE();
        uint chromaLog2WeightDenom = 0u;
        if (chromaArrayType != 0)
            chromaLog2WeightDenom = reader.ReadUE();

        PredWeightTableList l0 = PredWeightTableList.Read(reader, chromaArrayType, numRefIdxL0ActiveMinus1);
        PredWeightTableList? l1 = null;

        if (sliceType % 5 == 1)
            l1 = PredWeightTableList.Read(reader, chromaArrayType, numRefIdxL1ActiveMinus1);

        return new PredWeightTable(lumaLog2WeightDenom, chromaLog2WeightDenom, l0, l1);
    }

    /// <summary>
    /// Determines whether this instance is equal to another object.
    /// </summary>
    /// <param name="obj">The object to compare with.</param>
    /// <returns>True if the objects are equal; otherwise, false.</returns>
    public readonly override bool Equals(object? obj)
    {
        return obj is PredWeightTable table && Equals(table);
    }

    /// <summary>
    /// Determines whether this instance is equal to another <see cref="PredWeightTable"/>.
    /// </summary>
    /// <param name="other">The other table to compare with.</param>
    /// <returns>True if the tables are equal; otherwise, false.</returns>
    public readonly bool Equals(PredWeightTable other)
    {
        return LumaLog2WeightDenom == other.LumaLog2WeightDenom &&
               ChromaLog2WeightDenom == other.ChromaLog2WeightDenom &&
               L0.Equals(other.L0) &&
               EqualityComparer<PredWeightTableList?>.Default.Equals(L1, other.L1);
    }

    /// <summary>
    /// Gets the hash code of the current object.
    /// </summary>
    /// <returns>The hash code.</returns>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(LumaLog2WeightDenom, ChromaLog2WeightDenom, L0, L1);
    }

    /// <summary>
    /// Writes the prediction weight table to the bitstream.
    /// </summary>
    /// <param name="writer">The bitstream writer.</param>
    /// <param name="chromaArrayType">The chroma array type.</param>
    /// <param name="sliceType">The type of the slice.</param>
    /// <param name="optionsL0">Write options for L0.</param>
    /// <param name="optionsL1">Write options for L1.</param>
    public readonly void Write(BitStreamWriter writer, int chromaArrayType, int sliceType, PredWeightTableListWriteOptions optionsL0, PredWeightTableListWriteOptions optionsL1)
    {
        writer.WriteUE(LumaLog2WeightDenom);
        if (chromaArrayType != 0)
            writer.WriteUE(ChromaLog2WeightDenom);

        PredWeightTableList.Write(writer, chromaArrayType, L0.Count, optionsL0);

        if (sliceType % 5 == 1)
        {
            PredWeightTableList.Write(writer, chromaArrayType, L1!.Value.Count, optionsL1);
        }
    }

    /// <summary>
    /// Writes the prediction weight table to the bitstream.
    /// </summary>
    /// <param name="writer">The bitstream writer.</param>
    /// <param name="chromaArrayType">The chroma array type.</param>
    /// <param name="sliceType">The type of the slice.</param>
    /// <param name="optionsL0">Write options for L0.</param>
    /// <param name="optionsL1">Write options for L1.</param>
    public readonly void Write(BitStreamWriter writer, int chromaArrayType, int sliceType, MemoryPredWeightTableListWriteOptions optionsL0, MemoryPredWeightTableListWriteOptions optionsL1)
    {
        writer.WriteUE(LumaLog2WeightDenom);
        if (chromaArrayType != 0)
            writer.WriteUE(ChromaLog2WeightDenom);

        PredWeightTableList.Write(writer, chromaArrayType, L0.Count, optionsL0);

        if (sliceType % 5 == 1)
        {
            PredWeightTableList.Write(writer, chromaArrayType, L0.Count, optionsL1);
        }
    }

    /// <summary>
    /// Asynchronously writes the prediction weight table to the bitstream.
    /// </summary>
    /// <param name="writer">The bitstream writer.</param>
    /// <param name="chromaArrayType">The chroma array type.</param>
    /// <param name="sliceType">The type of the slice.</param>
    /// <param name="optionsL0">Write options for L0.</param>
    /// <param name="optionsL1">Optional write options for L1.</param>
    public readonly async Task WriteAsync(BitStreamWriter writer, int chromaArrayType, int sliceType, MemoryPredWeightTableListWriteOptions optionsL0, MemoryPredWeightTableListWriteOptions? optionsL1)
    {
        await writer.WriteUEAsync(LumaLog2WeightDenom);
        if (chromaArrayType != 0)
            await writer.WriteUEAsync(ChromaLog2WeightDenom);

        await PredWeightTableList.WriteAsync(writer, chromaArrayType, L0.Count, optionsL0);

        if (sliceType % 5 == 1)
        {
            await PredWeightTableList.WriteAsync(writer, chromaArrayType, L0.Count, optionsL1!.Value);
        }
    }

    /// <summary>
    /// Defines the equality operator for <see cref="PredWeightTable"/> instances.
    /// </summary>
    public static bool operator ==(PredWeightTable left, PredWeightTable right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Defines the inequality operator for <see cref="PredWeightTable"/> instances.
    /// </summary>
    public static bool operator !=(PredWeightTable left, PredWeightTable right)
    {
        return !(left == right);
    }
}
