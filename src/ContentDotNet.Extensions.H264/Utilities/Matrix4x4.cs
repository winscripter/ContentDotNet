namespace ContentDotNet.Extensions.H264.Utilities;

internal readonly ref struct Matrix4x4
{
    private readonly Span<int> _values;

    public Matrix4x4(Span<int> values)
    {
        if (values.Length !=  4 * 4)
            throw new ArgumentException("The Span must have a length of 16 (4x4).", nameof(values));

        _values = values;
    }

    public int this[int x, int y]
    {
        get => _values[x * 4 + y];
        set => _values[x * 4 + y] = value;
    }

    public readonly void Clear()
    {
        _values.Clear();
    }
}
