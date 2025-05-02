using ContentDotNet.Extensions.H264.Models;

namespace ContentDotNet.Extensions.H264.Containers;

/// <summary>
/// Represents a 2x4x4 container matrix backed by a <see cref="Container32CavlcResidual"/>.
/// </summary>
public struct ContainerMatrix2x4x4CavlcResidual : IEquatable<ContainerMatrix2x4x4CavlcResidual>
{
    /// <summary>
    /// The underlying data storage for the matrix.
    /// </summary>
    private Container32CavlcResidual m_Data;

    /// <summary>
    /// Initializes a new instance of the <see cref="ContainerMatrix2x4x4CavlcResidual"/> struct with the specified data.
    /// </summary>
    /// <param name="data">The data to initialize the matrix with.</param>
    public ContainerMatrix2x4x4CavlcResidual(Container32CavlcResidual data) => m_Data = data;

    /// <summary>
    /// Initializes a new instance of the <see cref="ContainerMatrix2x4x4CavlcResidual"/> struct with default data.
    /// </summary>
    public ContainerMatrix2x4x4CavlcResidual()
        : this(new())
    {
    }

    /// <summary>
    /// Gets or sets the value at the specified index.
    /// </summary>
    /// <param name="index">The index of the value to get or set.</param>
    /// <returns>The value at the specified index.</returns>
    public CavlcResidual this[uint index]
    {
        get => m_Data[(int)index];
        set => m_Data[(int)index] = value;
    }

    /// <summary>  
    /// Gets or sets the value at the specified 3D coordinates.  
    /// </summary>  
    /// <param name="a">The first coordinate.</param>  
    /// <param name="b">The second coordinate.</param>  
    /// <param name="c">The third coordinate.</param>  
    /// <returns>The value at the specified coordinates.</returns>  
    public CavlcResidual this[uint a, uint b, uint c]
    {
        get => m_Data[(int)(a * 16 + b * 4 + c)];
        set => m_Data[(int)(a * 16 + b * 4 + c)] = value;
    }

    /// <summary>
    /// Gets or sets the value at the specified index.
    /// </summary>
    /// <param name="index">The index of the value to get or set.</param>
    /// <returns>The value at the specified index.</returns>
    public CavlcResidual this[int index]
    {
        get => m_Data[index];
        set => m_Data[index] = value;
    }

    /// <summary>
    /// Gets or sets the value at the specified 3D coordinates.
    /// </summary>
    /// <param name="a">The first coordinate.</param>
    /// <param name="b">The second coordinate.</param>
    /// <param name="c">The third coordinate.</param>
    /// <returns>The value at the specified coordinates.</returns>
    public CavlcResidual this[int a, int b, int c]
    {
        get => m_Data[a * 16 + b * 4 + c];
        set => m_Data[a * 16 + b * 4 + c] = value;
    }

    /// <summary>
    /// Clears all values in the matrix by setting them to zero.
    /// </summary>
    public void Clear()
    {
        for (int i = 0; i < 32; i++)
            m_Data[i] = default;
    }

    /// <inheritdoc/>
    public readonly override bool Equals(object? obj)
    {
        return obj is ContainerMatrix2x4x4CavlcResidual x && Equals(x);
    }

    /// <summary>
    /// Determines whether the current matrix is equal to another matrix.
    /// </summary>
    /// <param name="other">The other matrix to compare with.</param>
    /// <returns><c>true</c> if the matrices are equal; otherwise, <c>false</c>.</returns>
    public readonly bool Equals(ContainerMatrix2x4x4CavlcResidual other)
    {
        return m_Data.Equals(other.m_Data);
    }

    /// <inheritdoc/>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(m_Data);
    }

    /// <summary>
    /// Determines whether two <see cref="ContainerMatrix2x4x4CavlcResidual"/> instances are equal.
    /// </summary>
    /// <param name="left">The first matrix to compare.</param>
    /// <param name="right">The second matrix to compare.</param>
    /// <returns><c>true</c> if the matrices are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(ContainerMatrix2x4x4CavlcResidual left, ContainerMatrix2x4x4CavlcResidual right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="ContainerMatrix2x4x4CavlcResidual"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first matrix to compare.</param>
    /// <param name="right">The second matrix to compare.</param>
    /// <returns><c>true</c> if the matrices are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(ContainerMatrix2x4x4CavlcResidual left, ContainerMatrix2x4x4CavlcResidual right)
    {
        return !(left == right);
    }
}
