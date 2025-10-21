namespace ContentDotNet.Extensions.Video.H264.Models.Cabac
{
    using System.Runtime.CompilerServices;

    public struct BinHistory
    {
        private uint last32Bits;

        public BinHistory()
        {
        }

        public uint Last32Bits
        {
            readonly get => last32Bits;
            set => last32Bits = value;
        }

        public readonly bool this[int index]
        {
            get => (Last32Bits & index) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Append(bool bin)
        {
            Last32Bits <<= 1;
            if (bin)
                Last32Bits |= 1;
        }
    }
}
