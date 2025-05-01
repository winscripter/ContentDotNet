using ContentDotNet.Abstractions;

namespace ContentDotNet.Extensions.H264;

/// <summary>
/// Represents a 4x4x2 container matrix backed by a <see cref="Container32UInt32"/>.
/// </summary>
public struct ContainerMatrix4x4x2 : IEquatable<ContainerMatrix4x4x2>
{
    /// <summary>
    /// The underlying data storage for the matrix.
    /// </summary>
    private Container32UInt32 m_Data;

    /// <summary>
    /// Initializes a new instance of the <see cref="ContainerMatrix4x4x2"/> struct with the specified data.
    /// </summary>
    /// <param name="data">The data to initialize the matrix with.</param>
    public ContainerMatrix4x4x2(Container32UInt32 data) => m_Data = data;

    /// <summary>
    /// Initializes a new instance of the <see cref="ContainerMatrix4x4x2"/> struct with default data.
    /// </summary>
    public ContainerMatrix4x4x2()
        : this(new())
    {
    }

    /// <summary>
    /// Gets or sets the value at the specified index.
    /// </summary>
    /// <param name="index">The index of the value to get or set.</param>
    /// <returns>The value at the specified index.</returns>
    public int this[uint index]
    {
        get => (int)m_Data[(int)index];
        set => m_Data[(int)index] = (uint)value;
    }

    /// <summary>
    /// Gets or sets the value at the specified 3D coordinates.
    /// </summary>
    /// <param name="a">The first coordinate.</param>
    /// <param name="b">The second coordinate.</param>
    /// <param name="c">The third coordinate.</param>
    /// <returns>The value at the specified coordinates.</returns>
    public uint this[uint a, uint b, uint c]
    {
        get => m_Data[(int)(a * 8 + b * 2 + c)];
        set => m_Data[(int)(a * 8 + b * 2 + c)] = value;
    }

    /// <summary>
    /// Gets or sets the value at the specified index.
    /// </summary>
    /// <param name="index">The index of the value to get or set.</param>
    /// <returns>The value at the specified index.</returns>
    public int this[int index]
    {
        get => (int)m_Data[index];
        set => m_Data[index] = (uint)value;
    }

    /// <summary>
    /// Gets or sets the value at the specified 3D coordinates.
    /// </summary>
    /// <param name="a">The first coordinate.</param>
    /// <param name="b">The second coordinate.</param>
    /// <param name="c">The third coordinate.</param>
    /// <returns>The value at the specified coordinates.</returns>
    public int this[int a, int b, int c]
    {
        get => (int)m_Data[a * 8 + b * 2 + c];
        set => m_Data[a * 8 + b * 2 + c] = (uint)value;
    }

    /// <summary>
    /// Clears all values in the matrix by setting them to zero.
    /// </summary>
    public void Clear()
    {
        for (int i = 0; i < 32; i++)
            m_Data[i] = 0u;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is ContainerMatrix4x4x2 x && Equals(x);
    }

    /// <summary>
    /// Determines whether the current matrix is equal to another matrix.
    /// </summary>
    /// <param name="other">The other matrix to compare with.</param>
    /// <returns><c>true</c> if the matrices are equal; otherwise, <c>false</c>.</returns>
    public bool Equals(ContainerMatrix4x4x2 other)
    {
        return m_Data.Equals(other.m_Data);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(m_Data);
    }

    /// <summary>
    /// Determines whether two <see cref="ContainerMatrix4x4x2"/> instances are equal.
    /// </summary>
    /// <param name="left">The first matrix to compare.</param>
    /// <param name="right">The second matrix to compare.</param>
    /// <returns><c>true</c> if the matrices are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(ContainerMatrix4x4x2 left, ContainerMatrix4x4x2 right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="ContainerMatrix4x4x2"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first matrix to compare.</param>
    /// <param name="right">The second matrix to compare.</param>
    /// <returns><c>true</c> if the matrices are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(ContainerMatrix4x4x2 left, ContainerMatrix4x4x2 right)
    {
        return !(left == right);
    }
}
