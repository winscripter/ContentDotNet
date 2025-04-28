namespace ContentDotNet.Extensions.H264.Utilities;

internal sealed class ArrayMatrix4x4x2
{
    private readonly uint[] m_Data;

    public ArrayMatrix4x4x2(uint[] data) => m_Data = data;

    public ArrayMatrix4x4x2()
        : this(new uint[4 * 4 * 2])
    {
    }

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
        Array.Clear(m_Data);
    }
}

