namespace ContentDotNet.Extensions.Bmp.Models.Headers;

/// <summary>
/// Represents the file header of a BMP (bitmap) file.
/// </summary>
public struct BitmapFileHeader : IBitmapHeader<BitmapFileHeader>
{
    /// <summary>
    /// The file type; must be 'BM' for bitmap files.
    /// </summary>
    public ushort Type;

    /// <summary>
    /// The size of the BMP file in bytes.
    /// </summary>
    public uint Size;

    /// <summary>
    /// Reserved; must be zero.
    /// </summary>
    public ushort Reserved1;

    /// <summary>
    /// Reserved; must be zero.
    /// </summary>
    public ushort Reserved2;

    /// <summary>
    /// The offset, in bytes, from the beginning of the file to the bitmap bits.
    /// </summary>
    public uint OffsetBits;

    /// <summary>
    /// Initializes a new instance of the <see cref="BitmapFileHeader"/> struct.
    /// </summary>
    /// <param name="type">The file type.</param>
    /// <param name="size">The size of the file in bytes.</param>
    /// <param name="reserved1">Reserved; must be zero.</param>
    /// <param name="reserved2">Reserved; must be zero.</param>
    /// <param name="offsetBits">The offset to the bitmap bits.</param>
    public BitmapFileHeader(ushort type, uint size, ushort reserved1, ushort reserved2, uint offsetBits)
    {
        Type = type;
        Size = size;
        Reserved1 = reserved1;
        Reserved2 = reserved2;
        OffsetBits = offsetBits;
    }

    /// <summary>
    /// Reads a <see cref="BitmapFileHeader"/> from the specified <see cref="BinaryReader"/>.
    /// </summary>
    /// <param name="reader">The binary reader to read from.</param>
    /// <returns>The read <see cref="BitmapFileHeader"/>.</returns>
    public static BitmapFileHeader Read(BinaryReader reader)
    {
        return new(reader.ReadUInt16(), reader.ReadUInt32(), reader.ReadUInt16(), reader.ReadUInt16(), reader.ReadUInt32());
    }

    /// <summary>
    /// Writes this <see cref="BitmapFileHeader"/> to the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="writer">The binary writer to write to.</param>
    public readonly void Write(BinaryWriter writer)
    {
        writer.Write(Type);
        writer.Write(Size);
        writer.Write(Reserved1);
        writer.Write(Reserved2);
        writer.Write(OffsetBits);
    }

    /// <summary>
    /// Asynchronously writes this <see cref="BitmapFileHeader"/> to the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="writer">The binary writer to write to.</param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    public readonly async Task WriteAsync(BinaryWriter writer)
    {
        await AsyncBinaryWriteHelper.WriteAsync(writer, Type);
        await AsyncBinaryWriteHelper.WriteAsync(writer, Size);
        await AsyncBinaryWriteHelper.WriteAsync(writer, Reserved1);
        await AsyncBinaryWriteHelper.WriteAsync(writer, Reserved2);
        await AsyncBinaryWriteHelper.WriteAsync(writer, OffsetBits);
    }

    /// <inheritdoc/>
    public readonly override bool Equals(object? obj)
    {
        return obj is BitmapFileHeader header && Equals(header);
    }

    /// <inheritdoc/>
    public readonly bool Equals(BitmapFileHeader other)
    {
        return Type == other.Type &&
               Size == other.Size &&
               Reserved1 == other.Reserved1 &&
               Reserved2 == other.Reserved2 &&
               OffsetBits == other.OffsetBits;
    }

    /// <inheritdoc/>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(Type, Size, Reserved1, Reserved2, OffsetBits);
    }

    /// <summary>
    /// Determines whether two <see cref="BitmapFileHeader"/> instances are equal.
    /// </summary>
    /// <param name="left">The first instance.</param>
    /// <param name="right">The second instance.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(BitmapFileHeader left, BitmapFileHeader right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="BitmapFileHeader"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first instance.</param>
    /// <param name="right">The second instance.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(BitmapFileHeader left, BitmapFileHeader right)
    {
        return !(left == right);
    }
}
