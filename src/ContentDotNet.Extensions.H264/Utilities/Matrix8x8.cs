namespace ContentDotNet.Extensions.H264.Utilities;

internal readonly ref struct Matrix8x8
{
    private readonly Span<int> _values;

    public Matrix8x8(Span<int> values)
    {
        if (values.Length != 8 * 8)
            throw new ArgumentException("The Span must have a length of 64 (8x8).", nameof(values));

        _values = values;
    }

    public int this[int x, int y]
    {
        get => _values[x * 8 + y];
        set => _values[x * 8 + y] = value;
    }

    public readonly void Clear()
    {
        _values.Clear();
    }
}
