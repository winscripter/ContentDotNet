namespace ContentDotNet.Extensions.Video.H264.Models
{
    using System.Runtime.Intrinsics;

    /// <summary>
    ///   Represents the Picture Order Count (POC).
    /// </summary>
    public readonly struct H264PictureOrderCount
    {
        private readonly Vector128<int> _raw;

        /// <summary>
        ///   Initializes a new instance of the <see cref="H264PictureOrderCount"/> structure.
        /// </summary>
        /// <param name="poc">Picture order count</param>
        /// <param name="top">Top value</param>
        /// <param name="bottom">Bottom value</param>
        /// <param name="msb">MSB</param>
        public H264PictureOrderCount(int poc, int top, int bottom, int msb)
        {
            var vec = Vector128.Create(poc, top, bottom, msb);
            _raw = vec;
        }

        /// <summary>
        ///   The POC value.
        /// </summary>
        public int PictureOrderCount
        {
            get => _raw[0];
        }

        /// <summary>
        ///   The Top value.
        /// </summary>
        public int Top
        {
            get => _raw[1];
        }

        /// <summary>
        ///   The Bottom value.
        /// </summary>
        public int Bottom
        {
            get => _raw[2];
        }

        /// <summary>
        ///   MSB
        /// </summary>
        public int Msb
        {
            get => _raw[3];
        }
    }
}
