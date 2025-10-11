namespace ContentDotNet.Extensions.Video.H264.Models.Weights
{
    using ContentDotNet.Colors;
    using System.Runtime.CompilerServices;
    using System.Runtime.Intrinsics;

    /// <summary>
    ///   The W array.
    /// </summary>
    public struct WArray
    {
        private Vector128<int> _pair1;
        private Vector128<int> _pair2;

        /// <summary>
        ///   Initializes a new instance of the <see cref="WArray"/> structure.
        /// </summary>
        public WArray()
        {
            _pair1 = Vector128<int>.Zero;
            _pair2 = Vector128<int>.Zero;
        }

        /// <summary>
        ///   The value w0l.
        /// </summary>
        public int W0L
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _pair1[0];

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _pair1 = _pair1.WithElement(0, value);
        }

        /// <summary>
        ///   The value w1l.
        /// </summary>
        public int W1L
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _pair1[1];

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _pair1 = _pair1.WithElement(1, value);
        }

        /// <summary>
        ///   The value w0cb.
        /// </summary>
        public int W0Cb
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _pair1[2];

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _pair1 = _pair1.WithElement(2, value);
        }

        /// <summary>
        ///   The value w1cb.
        /// </summary>
        public int W1Cb
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _pair1[3];

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _pair1 = _pair1.WithElement(3, value);
        }

        /// <summary>
        ///   The value w0cr.
        /// </summary>
        public int W0Cr
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _pair2[0];

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _pair2 = _pair2.WithElement(0, value);
        }

        /// <summary>
        ///   The value w1cr.
        /// </summary>
        public int W1Cr
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _pair2[1];

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _pair2 = _pair2.WithElement(1, value);
        }

        /// <summary>
        ///   Return <see cref="W0L"/> or <see cref="W1L"/> by index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>W0L or W1L or -1 if neither is specified.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly int GetWLOrMinus1(int index)
        {
            return index == 1 ? W1L : index == 0 ? W0L : -1;
        }

        /// <summary>
        ///   Return <see cref="W0Cb"/> or <see cref="W1Cb"/> by index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>W0Cb or W1Cb or -1 if neither is specified.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly int GetWCbOrMinus1(int index)
        {
            return index == 1 ? W1Cb : index == 0 ? W0Cb : -1;
        }

        /// <summary>
        ///   Return <see cref="W0Cr"/> or <see cref="W1Cr"/> by index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>W0Cr or W1Cr or -1 if neither is specified.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly int GetWCrOrMinus1(int index)
        {
            return index == 1 ? W1Cr : index == 0 ? W0Cr : -1;
        }

        /// <summary>
        ///   Set <see cref="W0L"/> or <see cref="W1L"/> according to <paramref name="index"/> to be <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <param name="index">The index.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetWLOrNothing(int value, int index)
        {
            if (index == 0) W0L = value;
            else if (index == 1) W1L = value;
        }

        /// <summary>
        ///   Set <see cref="W0Cb"/> or <see cref="W1Cr"/> according to <paramref name="index"/> to be <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <param name="index">The index.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetWCbOrNothing(int value, int index)
        {
            if (index == 0) W0Cb = value;
            else if (index == 1) W1Cb = value;
        }

        /// <summary>
        ///   Set <see cref="W0Cr"/> or <see cref="W1Cr"/> according to <paramref name="index"/> to be <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <param name="index">The index.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetWCrOrNothing(int value, int index)
        {
            if (index == 0) W0Cr = value;
            else if (index == 1) W1Cr = value;
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
            if (cc == ChromaChannel.Y) return GetWLOrMinus1(index);
            else if (cc == ChromaChannel.U) return GetWCbOrMinus1(index);
            else return GetWCrOrMinus1(index);
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
            if (cc == ChromaChannel.Y) SetWLOrNothing(value, index);
            else if (cc == ChromaChannel.U) SetWCbOrNothing(value, index);
            else SetWCrOrNothing(value, index);
        }
    }
}
