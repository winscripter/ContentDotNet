using ContentDotNet.BitStream;

namespace ContentDotNet.Extensions.H264.Models;

/// <summary>
/// Represents an entry for modifying a reference picture list.
/// </summary>
public struct RefPicListModificationEntry : IEquatable<RefPicListModificationEntry>
{
    /// <summary>
    /// Identifier for the type of modification of picture numbers.
    /// </summary>
    public uint ModificationOfPicNumsIdc;

    /// <summary>
    /// Absolute difference between picture numbers, minus one. Used for short-term reference pictures.
    /// </summary>
    public uint AbsDiffPicNumMinus1;

    /// <summary>
    /// Identifier for long-term reference pictures.
    /// </summary>
    public uint LongTermPicNum;

    /// <summary>
    /// Initializes a new instance of the <see cref="RefPicListModificationEntry"/> struct.
    /// </summary>
    /// <param name="modificationOfPicNumsIdc">The type of modification of picture numbers.</param>
    /// <param name="absDiffPicNumMinus1">The absolute difference between picture numbers, minus one.</param>
    /// <param name="longTermPicNum">The identifier for long-term reference pictures.</param>
    public RefPicListModificationEntry(uint modificationOfPicNumsIdc, uint absDiffPicNumMinus1, uint longTermPicNum)
    {
        ModificationOfPicNumsIdc = modificationOfPicNumsIdc;
        AbsDiffPicNumMinus1 = absDiffPicNumMinus1;
        LongTermPicNum = longTermPicNum;
    }

    /// <summary>
    /// Parses a <see cref="RefPicListModificationEntry"/> from a <see cref="BitStreamReader"/>.
    /// </summary>
    /// <param name="reader">The bit stream reader.</param>
    /// <returns>A new <see cref="RefPicListModificationEntry"/> instance.</returns>
    public static RefPicListModificationEntry Parse(BitStreamReader reader)
    {
        uint modificationOfPicNumsIdc = reader.ReadUE();
        uint absDiffPicNumsMinus1 = reader.ReadUE();
        uint longTermPicNum = reader.ReadUE();

        return new RefPicListModificationEntry(
            modificationOfPicNumsIdc,
            absDiffPicNumsMinus1,
            longTermPicNum
        );
    }

    /// <inheritdoc/>
    public readonly override bool Equals(object? obj)
    {
        return obj is RefPicListModificationEntry entry && Equals(entry);
    }

    /// <inheritdoc/>
    public readonly bool Equals(RefPicListModificationEntry other)
    {
        return ModificationOfPicNumsIdc == other.ModificationOfPicNumsIdc &&
               AbsDiffPicNumMinus1 == other.AbsDiffPicNumMinus1 &&
               LongTermPicNum == other.LongTermPicNum;
    }

    /// <inheritdoc/>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(ModificationOfPicNumsIdc, AbsDiffPicNumMinus1, LongTermPicNum);
    }

    /// <summary>
    /// Writes this entry to a <see cref="BitStreamWriter"/>.
    /// </summary>
    /// <param name="writer">The bit stream writer.</param>
    public readonly void Write(BitStreamWriter writer)
    {
        writer.WriteUE(ModificationOfPicNumsIdc);
        if (ModificationOfPicNumsIdc is 0u or 1u)
            writer.WriteUE(AbsDiffPicNumMinus1);
        else if (ModificationOfPicNumsIdc == 2)
            writer.WriteUE(LongTermPicNum);
    }

    /// <summary>
    /// Asynchronously writes this entry to a <see cref="BitStreamWriter"/>.
    /// </summary>
    /// <param name="writer">The bit stream writer.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public readonly async Task WriteAsync(BitStreamWriter writer)
    {
        await writer.WriteUEAsync(ModificationOfPicNumsIdc);
        if (ModificationOfPicNumsIdc is 0u or 1u)
            await writer.WriteUEAsync(AbsDiffPicNumMinus1);
        else if (ModificationOfPicNumsIdc == 2)
            await writer.WriteUEAsync(LongTermPicNum);
    }

    /// <summary>
    /// Determines whether two <see cref="RefPicListModificationEntry"/> instances are equal.
    /// </summary>
    /// <param name="left">The first instance.</param>
    /// <param name="right">The second instance.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(RefPicListModificationEntry left, RefPicListModificationEntry right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="RefPicListModificationEntry"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first instance.</param>
    /// <param name="right">The second instance.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(RefPicListModificationEntry left, RefPicListModificationEntry right)
    {
        return !(left == right);
    }
}

// 🐓
/// <summary>
///   Ref pic list modification
/// </summary>
public struct RefPicListModification : IEquatable<RefPicListModification>
{
    /// <summary>
    /// Indicates whether reference picture list modification is enabled for list 0.
    /// </summary>
    public bool RefPicListModificationFlagL0;

    /// <summary>
    /// Indicates whether reference picture list modification is enabled for list 1.
    /// </summary>
    public bool RefPicListModificationFlagL1;

    /// <summary>
    /// Represents the offset in the bitstream pool where the reference picture list modification entries are stored.
    /// </summary>
    public ReaderState PoolOffset;

    /// <summary>
    /// The number of elements in the reference picture list modification.
    /// </summary>
    public int NumberOfElements;

    /// <summary>
    /// Initializes a new instance of the <see cref="RefPicListModification"/> struct.
    /// </summary>
    /// <param name="refPicListModificationFlagL0">Flag indicating if list 0 modification is enabled.</param>
    /// <param name="refPicListModificationFlagL1">Flag indicating if list 1 modification is enabled.</param>
    /// <param name="poolOffset">The offset in the bitstream pool for the modification entries.</param>
    /// <param name="numberOfElements">The number of elements in the modification list.</param>
    public RefPicListModification(bool refPicListModificationFlagL0, bool refPicListModificationFlagL1, ReaderState poolOffset, int numberOfElements)
    {
        RefPicListModificationFlagL0 = refPicListModificationFlagL0;
        RefPicListModificationFlagL1 = refPicListModificationFlagL1;
        PoolOffset = poolOffset;
        NumberOfElements = numberOfElements;
    }

    /// <summary>
    /// Retrieves a specific entry from the reference picture list modification.
    /// </summary>
    /// <param name="reader">The bit stream reader.</param>
    /// <param name="index">The index of the entry to retrieve.</param>
    /// <returns>The requested reference picture list modification entry.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is out of range.</exception>
    public readonly RefPicListModificationEntry GetEntry(BitStreamReader reader, int index)
    {
        if (index + 1 > NumberOfElements)
            throw new ArgumentOutOfRangeException(nameof(index));

        ReaderState prev = reader.GetState();
        reader.GoTo(this.PoolOffset);

        RefPicListModificationEntry last = new();
        for (int i = 0; i < index + 1; i++)
            last = RefPicListModificationEntry.Parse(reader);

        reader.GoTo(prev);
        return last;
    }

    /// <summary>
    /// Reads a <see cref="RefPicListModification"/> from a <see cref="BitStreamReader"/> based on slice type.
    /// </summary>
    /// <param name="reader">The bit stream reader.</param>
    /// <param name="sliceType">The slice type value.</param>
    /// <returns>A new <see cref="RefPicListModification"/> instance.</returns>
    public static RefPicListModification Read(BitStreamReader reader, uint sliceType)
    {
        if (sliceType % 5 != 2 && sliceType % 5 != 4)
        {
            bool refPicListModificationFlagL0 = reader.ReadBit();
            ReaderState poolOffset = reader.GetState();
            RefPicListModificationEntry lastEntry;
            int elementCount = 0;

            if (refPicListModificationFlagL0)
            {
                do
                {
                    lastEntry = RefPicListModificationEntry.Parse(reader);
                    elementCount++;
                }
                while (lastEntry.ModificationOfPicNumsIdc != 3);
            }

            return new RefPicListModification(refPicListModificationFlagL0, false, poolOffset, elementCount);
        }
        else
        {
            bool refPicListModificationFlagL1 = reader.ReadBit();
            ReaderState poolOffset = reader.GetState();
            RefPicListModificationEntry lastEntry;
            int elementCount = 0;

            if (refPicListModificationFlagL1)
            {
                do
                {
                    lastEntry = RefPicListModificationEntry.Parse(reader);
                    elementCount++;
                }
                while (lastEntry.ModificationOfPicNumsIdc != 3);
            }

            return new RefPicListModification(false, refPicListModificationFlagL1, poolOffset, elementCount);
        }
    }

    /// <summary>
    /// Writes the current modification to a <see cref="BitStreamWriter"/>.
    /// </summary>
    /// <param name="writer">The bit stream writer.</param>
    /// <param name="entries">The entries to write.</param>
    /// <param name="sliceType">The slice type value.</param>
    public readonly void Write(BitStreamWriter writer, Span<RefPicListModificationEntry> entries, uint sliceType)
    {
        if (sliceType % 5 != 2 && sliceType % 5 != 4)
        {
            writer.WriteBit(RefPicListModificationFlagL0);
            if (RefPicListModificationFlagL0)
            {
                for (int i = 0; i < this.NumberOfElements; i++)
                {
                    entries[i].Write(writer);
                }
            }
        }
        else
        {
            writer.WriteBit(RefPicListModificationFlagL1);
            if (RefPicListModificationFlagL1)
            {
                for (int i = 0; i < this.NumberOfElements; i++)
                {
                    entries[i].Write(writer);
                }
            }
        }
    }

    /// <summary>
    /// Asynchronously writes the current modification to a <see cref="BitStreamWriter"/>.
    /// </summary>
    /// <param name="writer">The bit stream writer.</param>
    /// <param name="entries">The entries to write.</param>
    /// <param name="sliceType">The slice type value.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public readonly async Task WriteAsync(BitStreamWriter writer, Memory<RefPicListModificationEntry> entries, uint sliceType)
    {
        if (sliceType % 5 != 2 && sliceType % 5 != 4)
        {
            await writer.WriteBitAsync(RefPicListModificationFlagL0);
            if (RefPicListModificationFlagL0)
            {
                for (int i = 0; i < this.NumberOfElements; i++)
                {
                    await entries.Span[i].WriteAsync(writer);
                }
            }
        }
        else
        {
            await writer.WriteBitAsync(RefPicListModificationFlagL1);
            if (RefPicListModificationFlagL1)
            {
                for (int i = 0; i < this.NumberOfElements; i++)
                {
                    await entries.Span[i].WriteAsync(writer);
                }
            }
        }
    }

    /// <summary>
    /// Checks if two <see cref="RefPicListModification"/> instances are equal.
    /// </summary>
    /// <param name="obj">The object to compare with.</param>
    /// <returns><c>true</c> if equal; otherwise, <c>false</c>.</returns>
    public readonly override bool Equals(object? obj)
    {
        return obj is RefPicListModification modification && Equals(modification);
    }

    /// <inheritdoc/>
    public readonly bool Equals(RefPicListModification other)
    {
        return RefPicListModificationFlagL0 == other.RefPicListModificationFlagL0 &&
               RefPicListModificationFlagL1 == other.RefPicListModificationFlagL1 &&
               EqualityComparer<ReaderState>.Default.Equals(PoolOffset, other.PoolOffset) &&
               NumberOfElements == other.NumberOfElements;
    }

    /// <inheritdoc/>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(RefPicListModificationFlagL0, RefPicListModificationFlagL1, PoolOffset, NumberOfElements);
    }

    /// <summary>
    /// Checks if two <see cref="RefPicListModification"/> instances are equal.
    /// </summary>
    /// <param name="left">The first instance.</param>
    /// <param name="right">The second instance.</param>
    /// <returns><c>true</c> if equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(RefPicListModification left, RefPicListModification right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Checks if two <see cref="RefPicListModification"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first instance.</param>
    /// <param name="right">The second instance.</param>
    /// <returns><c>true</c> if not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(RefPicListModification left, RefPicListModification right)
    {
        return !(left == right);
    }
}
