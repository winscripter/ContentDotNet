using ContentDotNet.Primitives;

namespace ContentDotNet.Extensions.H26x;

/// <summary>
///   Represents a frame that's directly bound to a stream.
/// </summary>
public sealed class StreamFrame : IFrame
{
    private readonly int _width;
    private readonly int _height;
    private readonly Stream _stream;

    /// <summary>
    ///   Initializes a new instance of the <see cref="StreamFrame"/> class.
    /// </summary>
    /// <param name="width">The width of the frame.</param>
    /// <param name="height">The height of the frame.</param>
    /// <param name="stream">The stream that backs the frame.</param>
    public StreamFrame(int width, int height, Stream stream)
    {
        _width = width;
        _height = height;
        _stream = stream;

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                stream.WriteByte(0);
                stream.WriteByte(0);
                stream.WriteByte(0);
            }
        }
    }

    /// <inheritdoc cref="IFrame.this[int, int]" />
    /// <summary>
    ///   Gets or sets the YUV value at the specified coordinates.
    /// </summary>
    /// <param name="x">The x-coordinate of the pixel.</param>
    /// <param name="y">The y-coordinate of the pixel.</param>
    /// <returns>The <see cref="Yuv"/> value at the specified coordinates.</returns>
    public Yuv this[int x, int y]
    {
        get
        {
            long offset = x * _width * 3 + y * 3;
            long prevOffset = _stream.Position;
            _stream.Position = offset;

            var yuv = new Yuv((byte)_stream.ReadByte(), (byte)_stream.ReadByte(), (byte)_stream.ReadByte());

            _stream.Position = prevOffset;
            return yuv;
        }
        set
        {
            long offset = x * _width * 3 + y * 3;
            long prevOffset = _stream.Position;
            _stream.Position = offset;

            _stream.WriteByte(value.Y);
            _stream.WriteByte(value.U);
            _stream.WriteByte(value.V);

            _stream.Position = prevOffset;
        }
    }

    /// <inheritdoc cref="IFrame.Width" />
    /// <summary>
    ///   Gets or sets the width of the frame.
    /// </summary>
    public int Width
    {
        get => _width;
        set => throw new NotImplementedException();
    }

    /// <inheritdoc cref="IFrame.Height" />
    /// <summary>
    ///   Gets or sets the height of the frame.
    /// </summary>
    public int Height
    {
        get => _height;
        set => throw new NotImplementedException();
    }

    /// <inheritdoc cref="IDisposable.Dispose()" />
    /// <summary>
    ///   Releases all resources used by the <see cref="StreamFrame"/>.
    /// </summary>
    public void Dispose()
    {
        _stream.Dispose();
        GC.SuppressFinalize(this);
    }
}
