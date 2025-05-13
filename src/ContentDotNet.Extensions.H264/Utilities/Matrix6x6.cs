namespace ContentDotNet.Extensions.H264.Utilities;

/// <summary>
/// Represents a 6x6 matrix of integers backed by a <see cref="Span{T}"/>.
/// </summary>
public readonly ref struct Matrix6x6
{
    private readonly Span<int> _values;

    /// <summary>
    /// Initializes a new instance of the <see cref="Matrix6x6"/> struct.
    /// </summary>
    /// <param name="values">A span of integers with a length of 36 (6x6).</param>
    /// <exception cref="ArgumentException">Thrown when the span does not have a length of 36.</exception>
    public Matrix6x6(Span<int> values)
    {
        if (values.Length != 6 * 6)
            throw new ArgumentException("The Span must have a length of 36 (6x6).", nameof(values));

        _values = values;
    }

    /// <summary>
    /// Gets or sets the value at the specified position in the matrix.
    /// </summary>
    /// <param name="x">The row index (0-based).</param>
    /// <param name="y">The column index (0-based).</param>
    /// <returns>The value at the specified position.</returns>
    public int this[int x, int y]
    {
        get => _values[x * 6 + y];
        set => _values[x * 6 + y] = value;
    }

    /// <summary>
    /// Clears all values in the matrix by setting them to their default value (0).
    /// </summary>
    public readonly void Clear()
    {
        _values.Clear();
    }
}
