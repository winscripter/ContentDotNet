using ContentDotNet.Abstractions;

namespace ContentDotNet.Extensions.H264.Containers;

/// <summary>
/// A 2x16x16 matrix that uses <see cref="Container512UInt32"/>.
/// </summary>
public struct ContainerMatrix2x16x16
{
    private Container512UInt32 _values;

    /// <summary>  
    /// Initializes a new instance of the <see cref="ContainerMatrix2x16x16"/> struct.  
    /// </summary>  
    /// <param name="values">The initial values for the matrix, represented as a <see cref="Container512UInt32"/>.</param>  
    public ContainerMatrix2x16x16(Container512UInt32 values)
    {
        _values = values;
    }

    /// <summary>
    ///   Gets/sets an element at x, y, z.
    /// </summary>
    /// <param name="x">X</param>
    /// <param name="y">Y</param>
    /// <param name="z">Z</param>
    /// <returns>Element at <paramref name="x"/>/<paramref name="y"/>/<paramref name="z"/>.</returns>
    public uint this[int x, int y, int z]
    {
        readonly get => _values[x * 256 + y * 16 + z];
        set => _values[x * 256 + y * 16 + z] = value;
    }

    /// <summary>  
    /// Clears all values in the matrix by setting them to 0.  
    /// </summary>  
    public void Clear()
    {
        for (int i = 0; i < 512; i++)
            _values[i] = 0u;
    }
}

