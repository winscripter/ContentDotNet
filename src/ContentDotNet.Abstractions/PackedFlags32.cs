using System.Runtime.CompilerServices;

namespace ContentDotNet.Abstractions;

/// <summary>
///   32 bits that use the backing <see cref="uint"/> type to be represented.
/// </summary>
/// <remarks>
///   This structure is optimized for performance critical scenarios and thus does
///   not include any safety checks.
/// </remarks>
public struct PackedFlags32 : IEquatable<PackedFlags32>
{
    private uint _flags;

    /// <summary>
    ///   Initializes a new instance of the <see cref="PackedFlags32"/> structure.
    /// </summary>
    public PackedFlags32()
    {
        _flags = 0;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="PackedFlags32"/> structure.
    /// </summary>
    /// <param name="flags">Flags</param>
    public PackedFlags32(uint flags)
    {
        _flags = flags;
    }

    /// <summary>
    ///   Gets/sets the backing flags.
    /// </summary>
    public uint Flags
    {
        readonly get => _flags;
        set => _flags = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void Set(uint index, bool state) =>
        _flags = state ? (_flags | (uint)(1 << (int)index)) : (_flags &= ~(uint)(1 << (int)index));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private readonly bool Get(uint index) =>
        (_flags & (uint)(1 << (int)index)) != 0;

    /// <summary>
    ///   Access or set the bit at given index.
    /// </summary>
    /// <param name="index">Index of the bit.</param>
    /// <returns>Boolean that represents whether bit at index is 1.</returns>
    public bool this[uint index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly get => Get(index);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => Set(index, value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void Set(int index, bool state) =>
        _flags = state ? (_flags | (uint)(1 << index)) : (_flags &= ~(uint)(1 << index));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private readonly bool Get(int index) =>
        (_flags & (uint)(1 << index)) != 0;

    /// <summary>
    ///   Checks if this structure and the given object is equal.
    /// </summary>
    /// <param name="obj">Nullable object to compare with</param>
    /// <returns>
    ///   If <paramref name="obj"/> is not <see cref="PackedFlags32"/> or is <see langword="null"/>,
    ///   <see langword="false"/>. Otherwise, the boolean indicates whether the <see cref="Flags"/>
    ///   property is equal.
    /// </returns>
    public readonly override bool Equals(object? obj)
    {
        return obj is PackedFlags32 flags && Equals(flags);
    }

    /// <summary>
    ///   Checks if this structure and the given instance is equal.
    /// </summary>
    /// <param name="other"><see cref="PackedFlags32"/> to compare with.</param>
    /// <returns>
    ///   Boolean, indicating whether the <see cref="Flags"/> property is equal.
    /// </returns>
    public readonly bool Equals(PackedFlags32 other)
    {
        return _flags == other._flags &&
               Flags == other.Flags;
    }

    /// <summary>
    ///   Compute the hash code for this instance.
    /// </summary>
    /// <returns>The hash code.</returns>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(_flags);
    }

    /// <summary>
    ///   Access or set the bit at given index.
    /// </summary>
    /// <param name="index">Index of the bit.</param>
    /// <returns>Boolean that represents whether bit at index is 1.</returns>
    public bool this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly get => Get(index);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => Set(index, value);
    }

    /// <summary>  
    ///   Determines whether two <see cref="PackedFlags32"/> instances are equal.  
    /// </summary>  
    /// <param name="left">The first <see cref="PackedFlags32"/> instance to compare.</param>  
    /// <param name="right">The second <see cref="PackedFlags32"/> instance to compare.</param>  
    /// <returns><see langword="true"/> if the two instances are equal; otherwise, <see langword="false"/>.</returns>  
    public static bool operator ==(PackedFlags32 left, PackedFlags32 right)
    {
        return left.Equals(right);
    }

    /// <summary>  
    ///   Determines whether two <see cref="PackedFlags32"/> instances are not equal.  
    /// </summary>  
    /// <param name="left">The first <see cref="PackedFlags32"/> instance to compare.</param>  
    /// <param name="right">The second <see cref="PackedFlags32"/> instance to compare.</param>  
    /// <returns><see langword="true"/> if the two instances are not equal; otherwise, <see langword="false"/>.</returns>  
    public static bool operator !=(PackedFlags32 left, PackedFlags32 right)
    {
        return !(left == right);
    }
}
