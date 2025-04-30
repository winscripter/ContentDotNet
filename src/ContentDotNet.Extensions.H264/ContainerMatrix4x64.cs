using ContentDotNet.Abstractions;

namespace ContentDotNet.Extensions.H264;

/// <summary>
/// A 4x64 matrix that uses <see cref="Container256UInt32"/>.
/// </summary>
public struct ContainerMatrix4x64
{
    private Container256UInt32 _values;

    /// <summary>  
    /// Initializes a new instance of the <see cref="ContainerMatrix4x64"/> struct.  
    /// </summary>  
    /// <param name="values">The initial values for the matrix, represented as a <see cref="Container256UInt32"/>.</param>  
    public ContainerMatrix4x64(Container256UInt32 values)
    {
        _values = values;
    }

    /// <summary>
    ///   Gets/sets an element at x, y.
    /// </summary>
    /// <param name="x">X</param>
    /// <param name="y">Y</param>
    /// <returns>Element at <paramref name="x"/>/<paramref name="y"/>.</returns>
    public uint this[int x, int y]
    {
        readonly get => _values[x * 64 + y];
        set => _values[x * 64 + y] = value;
    }

    /// <summary>  
    /// Clears all values in the matrix by setting them to 0.  
    /// </summary>  
    public void Clear()
    {
        for (int i = 0; i < 256; i++)
            _values[i] = 0u;
    }
}
