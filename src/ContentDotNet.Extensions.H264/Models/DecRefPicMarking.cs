using ContentDotNet.Abstractions;

namespace ContentDotNet.Extensions.H264.Models;

/// <summary>
/// Represents an entry in the Decoded Reference Picture Marking process.
/// </summary>
public struct DecRefPicMarkingEntry : IEquatable<DecRefPicMarkingEntry>
{
    /// <summary>
    /// Specifies the memory management control operation.
    /// </summary>
    public uint MemoryManagementControlOperation;

    /// <summary>
    /// Specifies the difference of picture numbers minus 1.
    /// </summary>
    public uint DifferenceOfPicNumsMinus1;

    /// <summary>
    /// Specifies the long-term picture number.
    /// </summary>
    public uint LongTermPicNum;

    /// <summary>
    /// Specifies the long-term frame index.
    /// </summary>
    public uint LongTermFrameIdx;

    /// <summary>
    /// Specifies the maximum long-term frame index plus 1.
    /// </summary>
    public uint MaxLongTermFrameIdxPlus1;

    /// <summary>
    /// Initializes a new instance of the <see cref="DecRefPicMarkingEntry"/> struct.
    /// </summary>
    /// <param name="memoryManagementControlOperation">The memory management control operation.</param>
    /// <param name="differenceOfPicNumsMinus1">The difference of picture numbers minus 1.</param>
    /// <param name="longTermPicNum">The long-term picture number.</param>
    /// <param name="longTermFrameIdx">The long-term frame index.</param>
    /// <param name="maxLongTermFrameIdxPlus1">The maximum long-term frame index plus 1.</param>
    public DecRefPicMarkingEntry(uint memoryManagementControlOperation, uint differenceOfPicNumsMinus1, uint longTermPicNum, uint longTermFrameIdx, uint maxLongTermFrameIdxPlus1)
    {
        MemoryManagementControlOperation = memoryManagementControlOperation;
        DifferenceOfPicNumsMinus1 = differenceOfPicNumsMinus1;
        LongTermPicNum = longTermPicNum;
        LongTermFrameIdx = longTermFrameIdx;
        MaxLongTermFrameIdxPlus1 = maxLongTermFrameIdxPlus1;
    }

    /// <summary>
    /// Reads a <see cref="DecRefPicMarkingEntry"/> from the specified <see cref="BitStreamReader"/>.
    /// </summary>
    /// <param name="reader">The bitstream reader.</param>
    /// <returns>A new instance of <see cref="DecRefPicMarkingEntry"/>.</returns>
    public static DecRefPicMarkingEntry Read(BitStreamReader reader)
    {
        uint mmControlOperation = reader.ReadUE();

        uint differenceOfPicNumsMinus1 = 0u;
        uint longTermPicNum = 0u;
        uint longTermFrameIdx = 0u;
        uint maxLongTermFrameIdxPlus1 = 0u;

        if (mmControlOperation is 1 or 3)
            differenceOfPicNumsMinus1 = reader.ReadUE();

        if (mmControlOperation == 2)
            longTermPicNum = reader.ReadUE();

        if (mmControlOperation is 3 or 6)
            longTermFrameIdx = reader.ReadUE();

        if (mmControlOperation == 4)
            maxLongTermFrameIdxPlus1 = reader.ReadUE();

        return new DecRefPicMarkingEntry(mmControlOperation, differenceOfPicNumsMinus1, longTermPicNum, longTermFrameIdx, maxLongTermFrameIdxPlus1);
    }

    /// <summary>
    /// Writes the current <see cref="DecRefPicMarkingEntry"/> to the specified <see cref="BitStreamWriter"/>.
    /// </summary>
    /// <param name="writer">The bitstream writer.</param>
    public readonly void Write(BitStreamWriter writer)
    {
        writer.WriteUE(MemoryManagementControlOperation);

        if (MemoryManagementControlOperation is 1 or 3)
            writer.WriteUE(DifferenceOfPicNumsMinus1);

        if (MemoryManagementControlOperation == 2)
            writer.WriteUE(LongTermPicNum);

        if (MemoryManagementControlOperation is 3 or 6)
            writer.WriteUE(LongTermFrameIdx);

        if (MemoryManagementControlOperation == 4)
            writer.WriteUE(MaxLongTermFrameIdxPlus1);
    }

    /// <summary>
    /// Asynchronously writes the current <see cref="DecRefPicMarkingEntry"/> to the specified <see cref="BitStreamWriter"/>.
    /// </summary>
    /// <param name="writer">The bitstream writer.</param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    public readonly async Task WriteAsync(BitStreamWriter writer)
    {
        await writer.WriteUEAsync(MemoryManagementControlOperation);

        if (MemoryManagementControlOperation is 1 or 3)
            await writer.WriteUEAsync(DifferenceOfPicNumsMinus1);

        if (MemoryManagementControlOperation == 2)
            await writer.WriteUEAsync(LongTermPicNum);

        if (MemoryManagementControlOperation is 3 or 6)
            await writer.WriteUEAsync(LongTermFrameIdx);

        if (MemoryManagementControlOperation == 4)
            await writer.WriteUEAsync(MaxLongTermFrameIdxPlus1);
    }

    /// <inheritdoc/>
    public readonly override bool Equals(object? obj)
    {
        return obj is DecRefPicMarkingEntry entry && Equals(entry);
    }

    /// <summary>
    /// Determines whether the specified <see cref="DecRefPicMarkingEntry"/> is equal to the current instance.
    /// </summary>
    /// <param name="other">The other <see cref="DecRefPicMarkingEntry"/> to compare.</param>
    /// <returns><c>true</c> if the specified instance is equal to the current instance; otherwise, <c>false</c>.</returns>
    public readonly bool Equals(DecRefPicMarkingEntry other)
    {
        return MemoryManagementControlOperation == other.MemoryManagementControlOperation &&
               DifferenceOfPicNumsMinus1 == other.DifferenceOfPicNumsMinus1 &&
               LongTermPicNum == other.LongTermPicNum &&
               LongTermFrameIdx == other.LongTermFrameIdx &&
               MaxLongTermFrameIdxPlus1 == other.MaxLongTermFrameIdxPlus1;
    }

    /// <inheritdoc/>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(MemoryManagementControlOperation, DifferenceOfPicNumsMinus1, LongTermPicNum, LongTermFrameIdx, MaxLongTermFrameIdxPlus1);
    }

    /// <summary>
    /// Determines whether two <see cref="DecRefPicMarkingEntry"/> instances are equal.
    /// </summary>
    /// <param name="left">The first instance.</param>
    /// <param name="right">The second instance.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(DecRefPicMarkingEntry left, DecRefPicMarkingEntry right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="DecRefPicMarkingEntry"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first instance.</param>
    /// <param name="right">The second instance.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(DecRefPicMarkingEntry left, DecRefPicMarkingEntry right)
    {
        return !(left == right);
    }
}

/// <summary>
/// Represents the Decoded Reference Picture Marking process.
/// </summary>
public struct DecRefPicMarking : IEquatable<DecRefPicMarking>
{
    /// <summary>
    /// Indicates whether prior pictures should not be output.
    /// </summary>
    public bool NoOutputOfPriorPicsFlag;

    /// <summary>
    /// Indicates whether the current picture is a long-term reference picture.
    /// </summary>
    public bool LongTermReferenceFlag;

    /// <summary>
    /// Indicates whether adaptive reference picture marking mode is enabled.
    /// </summary>
    public bool AdaptiveRefPicMarkingModeFlag;

    /// <summary>
    /// The offset in the bitstream where the entries are located.
    /// </summary>
    public ReaderState EntriesOffset;

    /// <summary>
    /// The number of entries in the marking process.
    /// </summary>
    public int EntryCount;

    /// <summary>
    /// Initializes a new instance of the <see cref="DecRefPicMarking"/> struct.
    /// </summary>
    /// <param name="noOutputOfPriorPicsFlag">Indicates whether prior pictures should not be output.</param>
    /// <param name="longTermReferenceFlag">Indicates whether the current picture is a long-term reference picture.</param>
    /// <param name="adaptiveRefPicMarkingModeFlag">Indicates whether adaptive reference picture marking mode is enabled.</param>
    /// <param name="entriesOffset">The offset in the bitstream where the entries are located.</param>
    /// <param name="entryCount">The number of entries in the marking process.</param>
    public DecRefPicMarking(bool noOutputOfPriorPicsFlag, bool longTermReferenceFlag, bool adaptiveRefPicMarkingModeFlag, ReaderState entriesOffset, int entryCount)
    {
        NoOutputOfPriorPicsFlag = noOutputOfPriorPicsFlag;
        LongTermReferenceFlag = longTermReferenceFlag;
        AdaptiveRefPicMarkingModeFlag = adaptiveRefPicMarkingModeFlag;
        EntriesOffset = entriesOffset;
        EntryCount = entryCount;
    }

    /// <summary>
    /// Retrieves a specific entry from the marking process.
    /// </summary>
    /// <param name="reader">The bitstream reader.</param>
    /// <param name="index">The index of the entry to retrieve.</param>
    /// <returns>The <see cref="DecRefPicMarkingEntry"/> at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is out of range.</exception>
    public readonly DecRefPicMarkingEntry GetEntry(BitStreamReader reader, int index)
    {
        if (index + 1 > this.EntryCount)
            throw new ArgumentOutOfRangeException(nameof(index));

        ReaderState prev = reader.GetState();
        reader.GoTo(this.EntriesOffset);

        DecRefPicMarkingEntry lastEntry = new();
        for (int i = 0; i < index; i++)
            lastEntry = DecRefPicMarkingEntry.Read(reader);

        reader.GoTo(prev);
        return lastEntry;
    }

    /// <summary>
    ///   Parses the dec ref pic marking from the bitstream.
    /// </summary>
    /// <param name="reader">Bitstream reader where it's parsed from.</param>
    /// <param name="idrPicFlag">Is the current NAL unit an IDR picture?</param>
    /// <returns><see cref="DecRefPicMarking"/></returns>
    public static DecRefPicMarking Read(BitStreamReader reader, bool idrPicFlag)
    {
        if (idrPicFlag)
        {
            bool noOutputOfPriorPicsFlag = reader.ReadBit();
            bool longTermReferenceFlag = reader.ReadBit();
            return new DecRefPicMarking(noOutputOfPriorPicsFlag, longTermReferenceFlag, false, ReaderState.Blank, 0);
        }

        bool adaptiveRefPicMarkingModeFlag = reader.ReadBit();
        DecRefPicMarkingEntry lastEntry;
        ReaderState offset = reader.GetState();
        int count = 0;

        if (adaptiveRefPicMarkingModeFlag)
        {
            do
            {
                lastEntry = DecRefPicMarkingEntry.Read(reader);
                count++;
            }
            while (lastEntry.MemoryManagementControlOperation != 3);
        }

        return new DecRefPicMarking(false, false, adaptiveRefPicMarkingModeFlag, offset, count);
    }

    /// <summary>
    /// Writes the current <see cref="DecRefPicMarking"/> to the specified <see cref="BitStreamWriter"/>.
    /// </summary>
    /// <param name="writer">The bitstream writer.</param>
    /// <param name="idrPicFlag">Indicates whether the current picture is an IDR picture.</param>
    /// <param name="entries">The entries to write.</param>
    public readonly void Write(BitStreamWriter writer, bool idrPicFlag, Span<DecRefPicMarkingEntry> entries)
    {
        if (idrPicFlag)
        {
            writer.WriteBit(NoOutputOfPriorPicsFlag);
            writer.WriteBit(LongTermReferenceFlag);
        }
        else
        {
            writer.WriteBit(AdaptiveRefPicMarkingModeFlag);
            if (AdaptiveRefPicMarkingModeFlag)
            {
                for (int i = 0; i < entries.Length; i++)
                    entries[i].Write(writer);
            }
        }
    }

    /// <summary>
    /// Asynchronously writes the current <see cref="DecRefPicMarking"/> to the specified <see cref="BitStreamWriter"/>.
    /// </summary>
    /// <param name="writer">The bitstream writer.</param>
    /// <param name="idrPicFlag">Indicates whether the current picture is an IDR picture.</param>
    /// <param name="entries">The entries to write.</param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    public readonly async Task WriteAsync(BitStreamWriter writer, bool idrPicFlag, Memory<DecRefPicMarkingEntry> entries)
    {
        if (idrPicFlag)
        {
            await writer.WriteBitAsync(NoOutputOfPriorPicsFlag);
            await writer.WriteBitAsync(LongTermReferenceFlag);
        }
        else
        {
            await writer.WriteBitAsync(AdaptiveRefPicMarkingModeFlag);
            if (AdaptiveRefPicMarkingModeFlag)
            {
                for (int i = 0; i < entries.Length; i++)
                    await entries.Span[i].WriteAsync(writer);
            }
        }
    }

    /// <inheritdoc/>
    public readonly override bool Equals(object? obj)
    {
        return obj is DecRefPicMarking marking && Equals(marking);
    }

    /// <summary>
    /// Determines whether the specified <see cref="DecRefPicMarking"/> is equal to the current instance.
    /// </summary>
    /// <param name="other">The other <see cref="DecRefPicMarking"/> to compare.</param>
    /// <returns><c>true</c> if the specified instance is equal to the current instance; otherwise, <c>false</c>.</returns>
    public readonly bool Equals(DecRefPicMarking other)
    {
        return NoOutputOfPriorPicsFlag == other.NoOutputOfPriorPicsFlag &&
               LongTermReferenceFlag == other.LongTermReferenceFlag &&
               AdaptiveRefPicMarkingModeFlag == other.AdaptiveRefPicMarkingModeFlag &&
               EqualityComparer<ReaderState>.Default.Equals(EntriesOffset, other.EntriesOffset) &&
               EntryCount == other.EntryCount;
    }

    /// <inheritdoc/>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(NoOutputOfPriorPicsFlag, LongTermReferenceFlag, AdaptiveRefPicMarkingModeFlag, EntriesOffset, EntryCount);
    }

    /// <summary>
    /// Determines whether two <see cref="DecRefPicMarking"/> instances are equal.
    /// </summary>
    /// <param name="left">The first instance.</param>
    /// <param name="right">The second instance.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(DecRefPicMarking left, DecRefPicMarking right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="DecRefPicMarking"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first instance.</param>
    /// <param name="right">The second instance.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(DecRefPicMarking left, DecRefPicMarking right)
    {
        return !(left == right);
    }
}
