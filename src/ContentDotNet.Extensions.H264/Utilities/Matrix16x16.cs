namespace ContentDotNet.Extensions.H264.Utilities;

internal readonly ref struct Matrix16x16
{
    private readonly Span<int> _values;

    public Matrix16x16(Span<int> values)
    {
        if (values.Length != 16 * 16)
            throw new ArgumentException("The Span must have a length of 256 (16x16).", nameof(values));

        _values = values;
    }

    public int this[int x, int y]
    {
        get => _values[x * 16 + y];
        set => _values[x * 16 + y] = value;
    }

    public readonly void Clear()
    {
        _values.Clear();
    }
}
