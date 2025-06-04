namespace ContentDotNet.Containers;

/// <summary>
/// A 16x2 matrix that uses <see cref="Container64Int32"/>.
/// </summary>
public struct ContainerMatrix2x32
{
    private Container64Int32 _values;

    /// <summary>  
    /// Initializes a new instance of the <see cref="ContainerMatrix2x32"/> struct.  
    /// </summary>  
    /// <param name="values">The initial values for the matrix, represented as a <see cref="Container64Int32"/>.</param>  
    public ContainerMatrix2x32(Container64Int32 values)
    {
        _values = values;
    }

    /// <summary>
    ///   Gets/sets an element at x, y.
    /// </summary>
    /// <param name="x">X</param>
    /// <param name="y">Y</param>
    /// <returns>Element at <paramref name="x"/>/<paramref name="y"/>.</returns>
    public int this[int x, int y]
    {
        readonly get => _values[x * 2 + y];
        set => _values[x * 2 + y] = value;
    }

    /// <summary>  
    /// Clears all values in the matrix by setting them to 0.  
    /// </summary>  
    public void Clear()
    {
        for (int i = 0; i < 64; i++)
            _values[i] = 0;
    }
}
