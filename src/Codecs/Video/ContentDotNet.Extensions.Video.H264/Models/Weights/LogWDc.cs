namespace ContentDotNet.Extensions.Video.H264.Models.Weights
{
    using ContentDotNet.Colors;
    using System.Runtime.CompilerServices;
    using System.Runtime.Intrinsics;

    /// <summary>
    ///   The logWDc structure.
    /// </summary>
    public struct LogWDc
    {
        private Vector128<int> value;

        /// <summary>
        ///   Initializes a new instance of the <see cref="LogWDc"/> structure.
        /// </summary>
        public LogWDc()
        {
            value = Vector128<int>.Zero;
        }

        /// <summary>
        ///   Luma value
        /// </summary>
        public int L
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => value[0];

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => this.value = this.value.WithElement(0, value);
        }

        /// <summary>
        ///   Cb value
        /// </summary>
        public int Cb
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => value[1];

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => this.value = this.value.WithElement(1, value);
        }

        /// <summary>
        ///   Cr value
        /// </summary>
        public int Cr
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => value[2];

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => this.value = this.value.WithElement(2, value);
        }

        /// <summary>
        ///   Returns the element given the chroma channel <paramref name="cc"/>.
        /// </summary>
        /// <param name="cc">The chromma channel.</param>
        /// <returns>The element at chromma channel.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly int GetElement(ChromaChannel cc)
        {
            if (cc == ChromaChannel.Y) return L;
            else if (cc == ChromaChannel.U) return Cb;
            else return Cr;
        }

        /// <summary>
        ///   Change the element at chroma channel <paramref name="cc"/> to be <paramref name="value"/>.
        /// </summary>
        /// <param name="cc">The chroma channel.</param>
        /// <param name="value">The value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetElement(ChromaChannel cc, int value)
        {
            if (cc == ChromaChannel.Y) L = value;
            else if (cc == ChromaChannel.U) Cb = value;
            else Cr = value;
        }
    }
}
