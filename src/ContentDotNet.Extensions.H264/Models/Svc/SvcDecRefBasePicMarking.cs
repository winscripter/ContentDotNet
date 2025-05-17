using ContentDotNet.BitStream;
using System.Reflection.PortableExecutable;

namespace ContentDotNet.Extensions.H264.Models.Svc;

/// <summary>
/// Represents an entry for SVC Decoded Reference Base Picture Marking.
/// </summary>
public struct SvcDecRefBasePicMarkingEntry : IEquatable<SvcDecRefBasePicMarkingEntry>
{
    /// <summary>
    /// Gets or sets the memory management base control operation.
    /// </summary>
    public uint MemoryManagementBaseControlOperation;

    /// <summary>
    /// Gets or sets the difference of base picture numbers minus one.
    /// </summary>
    public uint DifferenceOfBasePicNumsMinus1;

    /// <summary>
    /// Gets or sets the long-term base picture number.
    /// </summary>
    public uint LongTermBasePicNum;

    /// <summary>
    /// Initializes a new instance of the <see cref="SvcDecRefBasePicMarkingEntry"/> struct.
    /// </summary>
    /// <param name="memoryManagementBaseControlOperation">The memory management base control operation.</param>
    /// <param name="differenceOfBasePicNumsMinus1">The difference of base picture numbers minus one.</param>
    /// <param name="longTermBasePicNum">The long-term base picture number.</param>
    public SvcDecRefBasePicMarkingEntry(uint memoryManagementBaseControlOperation, uint differenceOfBasePicNumsMinus1, uint longTermBasePicNum)
    {
        MemoryManagementBaseControlOperation = memoryManagementBaseControlOperation;
        DifferenceOfBasePicNumsMinus1 = differenceOfBasePicNumsMinus1;
        LongTermBasePicNum = longTermBasePicNum;
    }

    /// <summary>
    ///   Reads the entry from the bitstream.
    /// </summary>
    /// <param name="reader">Bitstream where the entry is read.</param>
    /// <returns>The entry, read from the bitstream.</returns>
    public static SvcDecRefBasePicMarkingEntry Read(BitStreamReader reader)
    {
        uint memoryManagementBaseControlOperation = reader.ReadUE();
        uint differenceOfBasePicNumsMinus1 = 0u;
        uint longTermBasePicNum = 0u;

        if (memoryManagementBaseControlOperation == 1)
            differenceOfBasePicNumsMinus1 = reader.ReadUE();
        else if (memoryManagementBaseControlOperation == 2)
            longTermBasePicNum = reader.ReadUE();

        return new SvcDecRefBasePicMarkingEntry(memoryManagementBaseControlOperation, differenceOfBasePicNumsMinus1, longTermBasePicNum);
    }

    /// <summary>
    ///   Writes the entry.
    /// </summary>
    /// <param name="writer">Bitstream where the entry is written to.</param>
    public readonly void Write(BitStreamWriter writer)
    {
        writer.WriteUE(MemoryManagementBaseControlOperation);

        if (MemoryManagementBaseControlOperation == 1)
            writer.WriteUE(DifferenceOfBasePicNumsMinus1);
        else if (MemoryManagementBaseControlOperation == 2)
            writer.WriteUE(LongTermBasePicNum);
    }

    /// <summary>
    ///   Writes the entry.
    /// </summary>
    /// <param name="writer">Bitstream where the entry is written to.</param>
    public readonly async Task WriteAsync(BitStreamWriter writer)
    {
        await writer.WriteUEAsync(MemoryManagementBaseControlOperation);

        if (MemoryManagementBaseControlOperation == 1)
            await writer.WriteUEAsync(DifferenceOfBasePicNumsMinus1);
        else if (MemoryManagementBaseControlOperation == 2)
            await writer.WriteUEAsync(LongTermBasePicNum);
    }

    /// <inheritdoc />
    public readonly override bool Equals(object? obj)
    {
        return obj is SvcDecRefBasePicMarkingEntry entry && Equals(entry);
    }

    /// <summary>
    /// Determines whether the specified <see cref="SvcDecRefBasePicMarkingEntry"/> is equal to the current instance.
    /// </summary>
    /// <param name="other">The other <see cref="SvcDecRefBasePicMarkingEntry"/> to compare.</param>
    /// <returns><c>true</c> if the specified instance is equal to the current instance; otherwise, <c>false</c>.</returns>
    public readonly bool Equals(SvcDecRefBasePicMarkingEntry other)
    {
        return MemoryManagementBaseControlOperation == other.MemoryManagementBaseControlOperation &&
               DifferenceOfBasePicNumsMinus1 == other.DifferenceOfBasePicNumsMinus1 &&
               LongTermBasePicNum == other.LongTermBasePicNum;
    }

    /// <inheritdoc />
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(MemoryManagementBaseControlOperation, DifferenceOfBasePicNumsMinus1, LongTermBasePicNum);
    }

    /// <summary>
    /// Determines whether two <see cref="SvcDecRefBasePicMarkingEntry"/> instances are equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(SvcDecRefBasePicMarkingEntry left, SvcDecRefBasePicMarkingEntry right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="SvcDecRefBasePicMarkingEntry"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(SvcDecRefBasePicMarkingEntry left, SvcDecRefBasePicMarkingEntry right)
    {
        return !(left == right);
    }
}

/// <summary>
/// Represents an SVC Decoded Reference Base Picture Marking.
/// </summary>
public struct SvcDecRefBasePicMarking : IEquatable<SvcDecRefBasePicMarking>
{
    /// <summary>
    /// Gets or sets a value indicating whether adaptive reference base picture marking mode is enabled.
    /// </summary>
    public bool AdaptiveRefBasePicMarkingModeFlag;

    /// <summary>
    /// Gets or sets the reader state handle where all SVC Decoded Reference Base Picture Marking entries are located.
    /// </summary>
    public ReaderState Handle;

    /// <summary>
    /// Initializes a new instance of the <see cref="SvcDecRefBasePicMarking"/> struct.
    /// </summary>
    /// <param name="adaptiveRefBasePicMarkingModeFlag">Indicates whether adaptive reference base picture marking mode is enabled.</param>
    /// <param name="handle">The reader state handle where entries are located.</param>
    public SvcDecRefBasePicMarking(bool adaptiveRefBasePicMarkingModeFlag, ReaderState handle)
    {
        AdaptiveRefBasePicMarkingModeFlag = adaptiveRefBasePicMarkingModeFlag;
        Handle = handle;
    }

    /// <summary>
    ///   Reads the decoded reference base picture marking.
    /// </summary>
    /// <param name="reader">Bitstream where everything is read from.</param>
    /// <returns>A decoded reference base picture marking, taken from the bitstream reader.</returns>
    public static SvcDecRefBasePicMarking Read(BitStreamReader reader)
    {
        bool adaptiveRefBasePicMarkingModeFlag = reader.ReadBit();
        ReaderState dataHandle = reader.GetState();

        if (adaptiveRefBasePicMarkingModeFlag)
        {
            SvcDecRefBasePicMarkingEntry last;
            do
            {
                last = SvcDecRefBasePicMarkingEntry.Read(reader);
            }
            while (last.MemoryManagementBaseControlOperation != 0u);
        }

        return new SvcDecRefBasePicMarking(adaptiveRefBasePicMarkingModeFlag, dataHandle);
    }

    /// <summary>
    ///   Writes the decoded reference base picture marking.
    /// </summary>
    /// <param name="writer">Bitstream where everything is written to.</param>
    /// <param name="originalReader">Reader for the same H.264 stream as the one used to parse this decoded reference base picture marking.</param>
    public readonly void Write(BitStreamWriter writer, BitStreamReader originalReader)
    {
        writer.WriteBit(AdaptiveRefBasePicMarkingModeFlag);
        ReaderState previous = originalReader.GetState();
        originalReader.GoTo(Handle);

        if (AdaptiveRefBasePicMarkingModeFlag)
        {
            SvcDecRefBasePicMarkingEntry last;
            do
            {
                last = SvcDecRefBasePicMarkingEntry.Read(originalReader);
                last.Write(writer);
            }
            while (last.MemoryManagementBaseControlOperation != 0u);
        }

        originalReader.GoTo(previous);
    }

    /// <summary>
    ///   Writes the decoded reference base picture marking.
    /// </summary>
    /// <param name="writer">Bitstream where everything is written to.</param>
    /// <param name="originalReader">Reader for the same H.264 stream as the one used to parse this decoded reference base picture marking.</param>
    /// <returns>An awaitable task.</returns>
    public readonly async Task WriteAsync(BitStreamWriter writer, BitStreamReader originalReader)
    {
        await writer.WriteBitAsync(AdaptiveRefBasePicMarkingModeFlag);
        ReaderState previous = originalReader.GetState();
        originalReader.GoTo(Handle);

        if (AdaptiveRefBasePicMarkingModeFlag)
        {
            SvcDecRefBasePicMarkingEntry last;
            do
            {
                last = SvcDecRefBasePicMarkingEntry.Read(originalReader);
                await last.WriteAsync(writer);
            }
            while (last.MemoryManagementBaseControlOperation != 0u);
        }

        originalReader.GoTo(previous);
    }

    /// <inheritdoc />
    public readonly override bool Equals(object? obj)
    {
        return obj is SvcDecRefBasePicMarking marking && Equals(marking);
    }

    /// <summary>
    /// Determines whether the specified <see cref="SvcDecRefBasePicMarking"/> is equal to the current instance.
    /// </summary>
    /// <param name="other">The other <see cref="SvcDecRefBasePicMarking"/> to compare.</param>
    /// <returns><c>true</c> if the specified instance is equal to the current instance; otherwise, <c>false</c>.</returns>
    public readonly bool Equals(SvcDecRefBasePicMarking other)
    {
        return AdaptiveRefBasePicMarkingModeFlag == other.AdaptiveRefBasePicMarkingModeFlag &&
               Handle.Equals(other.Handle);
    }

    /// <inheritdoc />
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(AdaptiveRefBasePicMarkingModeFlag, Handle);
    }

    /// <summary>
    /// Determines whether two <see cref="SvcDecRefBasePicMarking"/> instances are equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(SvcDecRefBasePicMarking left, SvcDecRefBasePicMarking right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="SvcDecRefBasePicMarking"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(SvcDecRefBasePicMarking left, SvcDecRefBasePicMarking right)
    {
        return !(left == right);
    }
}
