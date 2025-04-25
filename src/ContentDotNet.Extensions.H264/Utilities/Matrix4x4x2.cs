namespace ContentDotNet.Extensions.H264.Utilities;

/// <summary>
/// Mutable 4x4x2 matrix containing 32 total elements.
/// </summary>
internal readonly ref struct Matrix4x4x2
{
    private readonly Span<uint> m_Data;

    public Matrix4x4x2(Span<uint> data) => m_Data = data;

    public int this[uint index]
    {
        get => (int)m_Data[(int)index];
        set => m_Data[(int)index] = (uint)value;
    }

    public uint this[uint a, uint b, uint c]
    {
        get => m_Data[(int)(a * 8 + b * 2 + c)];
        set => m_Data[(int)(a * 8 + b * 2 + c)] = value;
    }

    public int this[int index]
    {
        get => (int)m_Data[index];
        set => m_Data[index] = (uint)value;
    }

    public int this[int a, int b, int c]
    {
        get => (int)m_Data[a * 8 + b * 2 + c];
        set => m_Data[a * 8 + b * 2 + c] = (uint)value;
    }

    public void Clear()
    {
        m_Data.Clear();
    }
}
