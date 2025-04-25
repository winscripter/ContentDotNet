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
