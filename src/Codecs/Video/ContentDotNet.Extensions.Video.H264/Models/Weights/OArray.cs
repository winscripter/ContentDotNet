namespace ContentDotNet.Extensions.Video.H264.Models.Weights
{
    using ContentDotNet.Colors;
    using System.Runtime.CompilerServices;
    using System.Runtime.Intrinsics;

    /// <summary>
    ///   The O array.
    /// </summary>
    public struct OArray
    {
        private Vector128<int> _pair1;
        private Vector128<int> _pair2;

        /// <summary>
        ///   Initializes a new instance of the <see cref="OArray"/> structure.
        /// </summary>
        public OArray()
        {
            _pair1 = Vector128<int>.Zero;
            _pair2 = Vector128<int>.Zero;
        }

        /// <summary>
        ///   The value c0l.
        /// </summary>
        public int C0L
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _pair1[0];

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _pair1 = _pair1.WithElement(0, value);
        }

        /// <summary>
        ///   The value c1l.
        /// </summary>
        public int C1L
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _pair1[1];

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _pair1 = _pair1.WithElement(1, value);
        }

        /// <summary>
        ///   The value c0cb.
        /// </summary>
        public int C0Cb
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _pair1[2];

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _pair1 = _pair1.WithElement(2, value);
        }

        /// <summary>
        ///   The value c1cb.
        /// </summary>
        public int C1Cb
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _pair1[3];

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _pair1 = _pair1.WithElement(3, value);
        }

        /// <summary>
        ///   The value c0cr.
        /// </summary>
        public int C0Cr
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _pair2[0];

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _pair2 = _pair2.WithElement(0, value);
        }

        /// <summary>
        ///   The value c1cr.
        /// </summary>
        public int C1Cr
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _pair2[1];

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _pair2 = _pair2.WithElement(1, value);
        }

        /// <summary>
        ///   Return <see cref="C0L"/> or <see cref="C1L"/> by index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>C0L or C1L or -1 if neither is specified.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly int GetCLOrMinus1(int index)
        {
            return index == 1 ? C1L : index == 0 ? C0L : -1;
        }

        /// <summary>
        ///   Return <see cref="C0Cb"/> or <see cref="C1Cb"/> by index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>C0Cb or C1Cb or -1 if neither is specified.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly int GetCCbOrMinus1(int index)
        {
            return index == 1 ? C1Cb : index == 0 ? C0Cb : -1;
        }

        /// <summary>
        ///   Return <see cref="C0Cr"/> or <see cref="C1Cr"/> by index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>C0Cr or C1Cr or -1 if neither is specified.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly int GetCCrOrMinus1(int index)
        {
            return index == 1 ? C1Cr : index == 0 ? C0Cr : -1;
        }

        /// <summary>
        ///   Set <see cref="C0L"/> or <see cref="C1L"/> according to <paramref name="index"/> to be <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <param name="index">The index.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetCLOrNothing(int value, int index)
        {
            if (index == 0) C0L = value;
            else if (index == 1) C1L = value;
        }

        /// <summary>
        ///   Set <see cref="C0Cb"/> or <see cref="C1Cr"/> according to <paramref name="index"/> to be <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <param name="index">The index.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetCCbOrNothing(int value, int index)
        {
            if (index == 0) C0Cb = value;
            else if (index == 1) C1Cb = value;
        }

        /// <summary>
        ///   Set <see cref="C0Cr"/> or <see cref="C1Cr"/> according to <paramref name="index"/> to be <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <param name="index">The index.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetCCrOrNothing(int value, int index)
        {
            if (index == 0) C0Cr = value;
            else if (index == 1) C1Cr = value;
        }

        /// <summary>
        ///   Returns the element at <paramref name="index"/> or -1 if it's out of range, indexed by <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index</param>
        /// <param name="cc">The chromma channel</param>
        /// <returns>The element value of -1 if it's out of range.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly int GetElementOrMinus1(int index, ChromaChannel cc)
        {
            if (cc == ChromaChannel.Y) return GetCLOrMinus1(index);
            else if (cc == ChromaChannel.U) return GetCCbOrMinus1(index);
            else return GetCCrOrMinus1(index);
        }

        /// <summary>
        ///   Set element at <paramref name="index"/> and chromma channel <paramref name="cc"/> to be <paramref name="value"/> or do nothing.
        /// </summary>
        /// <param name="index">The index</param>
        /// <param name="cc">The chromma channel</param>
        /// <param name="value">The value to set</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetElementOrNothing(int index, ChromaChannel cc, int value)
        {
            if (cc == ChromaChannel.Y) SetCLOrNothing(value, index);
            else if (cc == ChromaChannel.U) SetCCbOrNothing(value, index);
            else SetCCrOrNothing(value, index);
        }
    }
}
