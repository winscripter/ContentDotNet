namespace ContentDotNet.Extensions.H264.Containers;

/// <summary>
/// A 16x16 matrix that uses <see cref="Container32UInt32"/>.
/// </summary>
public struct ContainerMatrix8x8Int32
{
    private Container32UInt32 _values;

    /// <summary>  
    /// Initializes a new instance of the <see cref="ContainerMatrix8x8Int32"/> struct.  
    /// </summary>  
    /// <param name="values">The initial values for the matrix, represented as a <see cref="Container32UInt32"/>.</param>  
    public ContainerMatrix8x8Int32(Container32UInt32 values)
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
        readonly get => (int)_values[x * 8 + y];
        set => _values[x * 8 + y] = (uint)value;
    }

    /// <summary>  
    /// Clears all values in the matrix by setting them to 0.  
    /// </summary>  
    public void Clear()
    {
        for (int i = 0; i < 32; i++)
            _values[i] = 0;
    }
}
