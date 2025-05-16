using ContentDotNet.Extensions.H264.Utilities;

namespace ContentDotNet.Extensions.H264.Cabac;

internal struct BitString
{
    private int _value;
    private int _length;

    public BitString(int value, int length)
    {
        _value = value;
        _length = length;
    }

    public bool this[int index]
    {
        readonly get => (_value & (1 << index)) != 0;
        set
        {
            if (value)
                _value |= (1 << index);
            else
                _value &= ~(1 << index);
        }
    }

    public readonly int Value => _value;

    public int Length
    {
        readonly get => _length;
        set => _length = value;
    }

    public readonly bool IsContiguousOnes => Intrinsic.IsContiguousOnes(_value);

    public static BitString operator +(BitString left, BitString right)
    {
        return new BitString((left.Value << right.Length) | right.Value, left.Length + right.Length);
    }
}
