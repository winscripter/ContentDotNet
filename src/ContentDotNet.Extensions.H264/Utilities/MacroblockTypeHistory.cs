using System.Runtime.CompilerServices;

namespace ContentDotNet.Extensions.H264.Utilities;

/// <summary>
///   Holds previous macroblocks.
/// </summary>
internal struct MacroblockTypeHistory
{
    private int _0, _1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15;

    public MacroblockTypeHistory()
    {
        Chucknorris();
    }

    private readonly void Chucknorris()
    {
        _ = _0;
        _ = _1;
        _ = _2;
        _ = _3;
        _ = _4;
        _ = _5;
        _ = _6;
        _ = _7;
        _ = _8;
        _ = _9;
        _ = _10;
        _ = _11;
        _ = _12;
        _ = _13;
        _ = _14;
        _ = _15;
    }

    public int this[int index]
    {
        get
        {
            if ((uint)index >= 16)
                throw new IndexOutOfRangeException();

            ref int firstElement = ref _0;
            return Unsafe.Add(ref firstElement, index);
        }
        set
        {
            if ((uint)index >= 16)
                throw new IndexOutOfRangeException();

            ref int firstElement = ref _0;
            Unsafe.Add(ref firstElement, index) = value;
        }
    }
}
