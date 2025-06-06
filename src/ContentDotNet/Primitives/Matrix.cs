namespace ContentDotNet.Primitives;

/// <summary>
///   Represents a variable matrix of <see cref="int"/>.
/// </summary>
public class Matrix
{
    private readonly int[] _data;
    private readonly int _width, _height;

    /// <summary>
    /// Initializes a new instance of the <see cref="Matrix"/> class with the specified width, height, and data array.
    /// </summary>
    /// <param name="width">The number of columns in the matrix.</param>
    /// <param name="height">The number of rows in the matrix.</param>
    /// <param name="data">The array containing the matrix elements.</param>
    public Matrix(int width, int height, int[] data)
    {
        _width = width;
        _height = height;
        _data = data;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Matrix"/> class with the specified width and height.
    /// The matrix elements are initialized to zero.
    /// </summary>
    /// <param name="width">The number of columns in the matrix.</param>
    /// <param name="height">The number of rows in the matrix.</param>
    public Matrix(int width, int height)
        : this(width, height, new int[width * height])
    {
    }

    /// <summary>
    /// Gets the underlying data array of the matrix.
    /// </summary>
    public int[] Data => _data;

    /// <summary>
    /// Gets the number of columns in the matrix.
    /// </summary>
    public int Width => _width;

    /// <summary>
    /// Gets the number of rows in the matrix.
    /// </summary>
    public int Height => _height;

    /// <summary>
    /// Gets or sets the element at the specified column and row in the matrix.
    /// </summary>
    /// <param name="x">The column index.</param>
    /// <param name="y">The row index.</param>
    /// <returns>The value at the specified position in the matrix.</returns>
    public virtual int this[int x, int y]
    {
        get => _data[x * _width + y];
        set => _data[x * _width + y] = value;
    }
}
