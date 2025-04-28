namespace ContentDotNet.Extensions.H264.Utilities;

internal sealed class ArrayMatrix16x16
{
    private readonly int[] _values;

    public ArrayMatrix16x16(int[] values)
    {
        if (values.Length != 16 * 16)
            throw new ArgumentException("The array must have a length of 256 (16x16).", nameof(values));

        _values = values;
    }

    public int this[int x, int y]
    {
        get => _values[x * 16 + y];
        set => _values[x * 16 + y] = value;
    }

    public void Clear()
    {
        Array.Clear(_values);
    }
}

