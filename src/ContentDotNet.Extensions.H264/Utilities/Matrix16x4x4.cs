namespace ContentDotNet.Extensions.H264.Utilities;

internal readonly ref struct Matrix16x4x4
{
    private readonly Span<int> _values;

    public Matrix16x4x4(Span<int> values)
    {
        if (values.Length != 16 * 4 * 4)
            throw new ArgumentException("The Span must have a length of 256 (16x4x4).", nameof(values));

        _values = values;
    }

    public int this[int x, int y, int z]
    {
        get => _values[x * 16 + y * 4 + z];
        set => _values[x * 16 + y * 4 + z] = value;
    }

    public readonly void Clear()
    {
        _values.Clear();
    }
}
