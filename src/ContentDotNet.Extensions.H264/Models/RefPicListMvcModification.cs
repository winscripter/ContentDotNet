using ContentDotNet.Abstractions;

namespace ContentDotNet.Extensions.H264.Models;

/// <summary>
/// Represents an entry in the reference picture list modification for MVC.
/// </summary>
public struct RefPicListMvcModificationEntry : IEquatable<RefPicListMvcModificationEntry>
{
    /// <summary>
    /// Indicates the type of modification for picture numbers.
    /// </summary>
    public uint ModificationOfPicNumsIdc;

    /// <summary>
    /// Represents the absolute difference of picture numbers minus 1.
    /// </summary>
    public uint AbsDiffPicNumMinus1;

    /// <summary>
    /// Represents the long-term picture number.
    /// </summary>
    public uint LongTermPicNum;

    /// <summary>
    /// Represents the absolute difference of view indices minus 1.
    /// </summary>
    public uint AbsDiffViewIdxMinus1;

    /// <summary>
    /// Initializes a new instance of the <see cref="RefPicListMvcModificationEntry"/> struct.
    /// </summary>
    /// <param name="modificationOfPicNumsIdc">The type of modification for picture numbers.</param>
    /// <param name="absDiffPicNumMinus1">The absolute difference of picture numbers minus 1.</param>
    /// <param name="longTermPicNum">The long-term picture number.</param>
    /// <param name="absDiffViewIdxMinus1">The absolute difference of view indices minus 1.</param>
    public RefPicListMvcModificationEntry(uint modificationOfPicNumsIdc, uint absDiffPicNumMinus1, uint longTermPicNum, uint absDiffViewIdxMinus1)
    {
        ModificationOfPicNumsIdc = modificationOfPicNumsIdc;
        AbsDiffPicNumMinus1 = absDiffPicNumMinus1;
        LongTermPicNum = longTermPicNum;
        AbsDiffViewIdxMinus1 = absDiffViewIdxMinus1;
    }

    /// <summary>
    /// Reads a <see cref="RefPicListMvcModificationEntry"/> from the specified <see cref="BitStreamReader"/>.
    /// </summary>
    /// <param name="reader">The bitstream reader to read from.</param>
    /// <returns>A new instance of <see cref="RefPicListMvcModificationEntry"/>.</returns>
    public static RefPicListMvcModificationEntry Read(BitStreamReader reader)
    {
        uint modificationOfPicNumsIdc = reader.ReadUE();
        uint absDiffPicNumMinus1 = 0u;
        uint longTermPicNum = 0u;
        uint absDiffViewIdxMinus1 = 0u;

        if (modificationOfPicNumsIdc is 0 or 1)
            absDiffPicNumMinus1 = reader.ReadUE();
        else if (modificationOfPicNumsIdc == 2)
            longTermPicNum = reader.ReadUE();
        else if (modificationOfPicNumsIdc is 4 or 5)
            absDiffViewIdxMinus1 = reader.ReadUE();

        return new RefPicListMvcModificationEntry(modificationOfPicNumsIdc, absDiffPicNumMinus1, longTermPicNum, absDiffViewIdxMinus1);
    }

    /// <summary>
    /// Writes the current instance to the specified <see cref="BitStreamWriter"/>.
    /// </summary>
    /// <param name="writer">The bitstream writer to write to.</param>
    public readonly void Write(BitStreamWriter writer)
    {
        writer.WriteUE(ModificationOfPicNumsIdc);

        if (ModificationOfPicNumsIdc is 0 or 1)
            writer.WriteUE(AbsDiffPicNumMinus1);
        else if (ModificationOfPicNumsIdc == 2)
            writer.WriteUE(LongTermPicNum);
        else if (ModificationOfPicNumsIdc is 4 or 5)
            writer.WriteUE(AbsDiffViewIdxMinus1);
    }

    /// <summary>
    /// Asynchronously writes the current instance to the specified <see cref="BitStreamWriter"/>.
    /// </summary>
    /// <param name="writer">The bitstream writer to write to.</param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    public readonly async Task WriteAsync(BitStreamWriter writer)
    {
        await writer.WriteUEAsync(ModificationOfPicNumsIdc);

        if (ModificationOfPicNumsIdc is 0 or 1)
            await writer.WriteUEAsync(AbsDiffPicNumMinus1);
        else if (ModificationOfPicNumsIdc == 2)
            await writer.WriteUEAsync(LongTermPicNum);
        else if (ModificationOfPicNumsIdc is 4 or 5)
            await writer.WriteUEAsync(AbsDiffViewIdxMinus1);
    }

    /// <inheritdoc/>
    public readonly override bool Equals(object? obj)
    {
        return obj is RefPicListMvcModificationEntry entry && Equals(entry);
    }

    /// <summary>
    /// Determines whether the specified <see cref="RefPicListMvcModificationEntry"/> is equal to the current instance.
    /// </summary>
    /// <param name="other">The other instance to compare with.</param>
    /// <returns><c>true</c> if the specified instance is equal to the current instance; otherwise, <c>false</c>.</returns>
    public readonly bool Equals(RefPicListMvcModificationEntry other)
    {
        return ModificationOfPicNumsIdc == other.ModificationOfPicNumsIdc &&
               AbsDiffPicNumMinus1 == other.AbsDiffPicNumMinus1 &&
               LongTermPicNum == other.LongTermPicNum &&
               AbsDiffViewIdxMinus1 == other.AbsDiffViewIdxMinus1;
    }

    /// <inheritdoc/>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(ModificationOfPicNumsIdc, AbsDiffPicNumMinus1, LongTermPicNum, AbsDiffViewIdxMinus1);
    }

    /// <summary>
    /// Determines whether two <see cref="RefPicListMvcModificationEntry"/> instances are equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(RefPicListMvcModificationEntry left, RefPicListMvcModificationEntry right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="RefPicListMvcModificationEntry"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(RefPicListMvcModificationEntry left, RefPicListMvcModificationEntry right)
    {
        return !(left == right);
    }
}

/// <summary>
/// Represents a list of reference picture list MVC modification entries.
/// </summary>
public struct RefPicListMvcModificationList : IEquatable<RefPicListMvcModificationList>
{
    /// <summary>
    /// Indicates whether the list is active.
    /// </summary>
    public bool Flag;

    /// <summary>
    /// The offset in the bitstream where the entries start.
    /// </summary>
    public ReaderState EntriesOffset;

    /// <summary>
    /// The number of entries in the list.
    /// </summary>
    public int EntriesCount;

    /// <summary>
    /// Initializes a new instance of the <see cref="RefPicListMvcModificationList"/> struct.
    /// </summary>
    /// <param name="flag">Indicates whether the list is active.</param>
    /// <param name="entriesOffset">The offset in the bitstream where the entries start.</param>
    /// <param name="entriesCount">The number of entries in the list.</param>
    public RefPicListMvcModificationList(bool flag, ReaderState entriesOffset, int entriesCount)
    {
        Flag = flag;
        EntriesOffset = entriesOffset;
        EntriesCount = entriesCount;
    }

    /// <summary>
    /// Reads a <see cref="RefPicListMvcModificationList"/> from the specified <see cref="BitStreamReader"/>.
    /// </summary>
    /// <param name="reader">The bitstream reader to read from.</param>
    /// <returns>A new instance of <see cref="RefPicListMvcModificationList"/>.</returns>
    public static RefPicListMvcModificationList Read(BitStreamReader reader)
    {
        bool flag = reader.ReadBit();
        ReaderState offset = reader.GetState();
        int count = 0;

        RefPicListMvcModificationEntry lastEntry;
        do
        {
            lastEntry = RefPicListMvcModificationEntry.Read(reader);
            count++;
        }
        while (lastEntry.ModificationOfPicNumsIdc != 3);

        return new RefPicListMvcModificationList(flag, offset, count);
    }

    /// <summary>
    /// Writes the current instance to the specified <see cref="BitStreamWriter"/>.
    /// </summary>
    /// <param name="writer">The bitstream writer to write to.</param>
    /// <param name="entries">The entries to write.</param>
    public readonly void Write(BitStreamWriter writer, Span<RefPicListMvcModificationEntry> entries)
    {
        writer.WriteBit(Flag);
        if (Flag)
        {
            foreach (RefPicListMvcModificationEntry entry in entries)
            {
                entry.Write(writer);
            }
        }
    }

    /// <summary>
    /// Asynchronously writes the current instance to the specified <see cref="BitStreamWriter"/>.
    /// </summary>
    /// <param name="writer">The bitstream writer to write to.</param>
    /// <param name="entries">The entries to write.</param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    public readonly async Task WriteAsync(BitStreamWriter writer, Memory<RefPicListMvcModificationEntry> entries)
    {
        await writer.WriteBitAsync(Flag);
        if (Flag)
        {
            for (int i = 0; i < entries.Length; i++)
                await entries.Span[i].WriteAsync(writer);
        }
    }

    /// <inheritdoc/>
    public readonly override bool Equals(object? obj)
    {
        return obj is RefPicListMvcModificationList list && Equals(list);
    }

    /// <summary>
    /// Determines whether the specified <see cref="RefPicListMvcModificationList"/> is equal to the current instance.
    /// </summary>
    /// <param name="other">The other instance to compare with.</param>
    /// <returns><c>true</c> if the specified instance is equal to the current instance; otherwise, <c>false</c>.</returns>
    public readonly bool Equals(RefPicListMvcModificationList other)
    {
        return Flag == other.Flag &&
               EqualityComparer<ReaderState>.Default.Equals(EntriesOffset, other.EntriesOffset) &&
               EntriesCount == other.EntriesCount;
    }

    /// <inheritdoc/>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(Flag, EntriesOffset, EntriesCount);
    }

    /// <summary>
    /// Determines whether two <see cref="RefPicListMvcModificationList"/> instances are equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(RefPicListMvcModificationList left, RefPicListMvcModificationList right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="RefPicListMvcModificationList"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(RefPicListMvcModificationList left, RefPicListMvcModificationList right)
    {
        return !(left == right);
    }
}

/// <summary>
/// Represents a modification to the reference picture list for MVC.
/// </summary>
public struct RefPicListMvcModification : IEquatable<RefPicListMvcModification>
{
    /// <summary>
    /// The reference picture list modification for list 0.
    /// </summary>
    public RefPicListMvcModificationList? L0;

    /// <summary>
    /// The reference picture list modification for list 1.
    /// </summary>
    public RefPicListMvcModificationList? L1;

    /// <summary>
    /// Initializes a new instance of the <see cref="RefPicListMvcModification"/> struct.
    /// </summary>
    /// <param name="l0">The reference picture list modification for list 0.</param>
    /// <param name="l1">The reference picture list modification for list 1.</param>
    public RefPicListMvcModification(RefPicListMvcModificationList? l0, RefPicListMvcModificationList? l1)
    {
        L0 = l0;
        L1 = l1;
    }

    /// <summary>
    /// Reads a <see cref="RefPicListMvcModification"/> from the specified <see cref="BitStreamReader"/>.
    /// </summary>
    /// <param name="reader">The bitstream reader to read from.</param>
    /// <param name="sliceType">The type of the slice being read.</param>
    /// <returns>A new instance of <see cref="RefPicListMvcModification"/>.</returns>
    public static RefPicListMvcModification Read(BitStreamReader reader, int sliceType)
    {
        RefPicListMvcModificationList? l0 = null;
        RefPicListMvcModificationList? l1 = null;

        if (sliceType % 5 != 2 && sliceType % 5 != 4)
        {
            l0 = RefPicListMvcModificationList.Read(reader);
        }

        if (sliceType % 5 == 1)
        {
            l1 = RefPicListMvcModificationList.Read(reader);
        }

        return new RefPicListMvcModification(l0, l1);
    }

    /// <inheritdoc/>
    public readonly override bool Equals(object? obj)
    {
        return obj is RefPicListMvcModification modification && Equals(modification);
    }

    /// <summary>
    /// Determines whether the specified <see cref="RefPicListMvcModification"/> is equal to the current instance.
    /// </summary>
    /// <param name="other">The other instance to compare with.</param>
    /// <returns><c>true</c> if the specified instance is equal to the current instance; otherwise, <c>false</c>.</returns>
    public readonly bool Equals(RefPicListMvcModification other)
    {
        return EqualityComparer<RefPicListMvcModificationList?>.Default.Equals(L0, other.L0) &&
               EqualityComparer<RefPicListMvcModificationList?>.Default.Equals(L1, other.L1);
    }

    /// <inheritdoc/>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(L0, L1);
    }

    /// <summary>
    /// Writes the current instance to the specified <see cref="BitStreamWriter"/>.
    /// </summary>
    /// <param name="writer">The bitstream writer to write to.</param>
    /// <param name="sliceType">The type of the slice being written.</param>
    /// <param name="l0">The entries for list 0 to write.</param>
    /// <param name="l1">The entries for list 1 to write.</param>
    /// <exception cref="InvalidOperationException">Thrown if a required list is null.</exception>
    public readonly void Write(BitStreamWriter writer, int sliceType, Span<RefPicListMvcModificationEntry> l0, Span<RefPicListMvcModificationEntry> l1)
    {
        if (sliceType % 5 != 2 && sliceType % 5 != 4)
        {
            if (L0 is null)
                throw new InvalidOperationException("L0 is required");

            this.L0!.Value.Write(writer, l0);
        }

        if (sliceType % 5 == 1)
        {
            if (L1 is null)
                throw new InvalidOperationException("L1 is required");

            this.L1!.Value.Write(writer, l1);
        }
    }

    /// <summary>
    /// Determines whether two <see cref="RefPicListMvcModification"/> instances are equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(RefPicListMvcModification left, RefPicListMvcModification right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="RefPicListMvcModification"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(RefPicListMvcModification left, RefPicListMvcModification right)
    {
        return !(left == right);
    }
}
