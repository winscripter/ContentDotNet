namespace ContentDotNet.Extensions.Video.H264.Models
{
    using ContentDotNet.Colors;

    /// <summary>
    ///   The intra prediction samples.
    /// </summary>
    public class IntraPredictionSamples
    {
        private readonly YCbCr[,] _macroblock;
        private readonly YCbCr[]? _left;
        private readonly YCbCr[]? _top;
        private readonly YCbCr[]? _topRight;
        private YCbCr? _topLeft;

        /// <summary>
        ///   Initializes a new instance of the <see cref="IntraPredictionSamples"/> class.
        /// </summary>
        /// <param name="macroblock">The actual current macroblock's pixel data to be predicted and written into.</param>
        /// <param name="left">The pixel data on the left. That is, the right-most pixel data of the macroblock to the left of the current one.</param>
        /// <param name="top">The pixel data on the top. That is, the bottom-most pixel data of the macroblock to the top of the current one.</param>
        /// <param name="topRight">The top-right pixels.</param>
        /// <param name="topLeft">The top-left pixels.</param>
        public IntraPredictionSamples(YCbCr[,] macroblock, YCbCr[]? left, YCbCr[]? top, YCbCr[]? topRight, YCbCr? topLeft)
        {
            _macroblock = macroblock;
            _left = left;
            _top = top;
            _topRight = topRight;
            _topLeft = topLeft;
        }

        /// <summary>
        ///   The actual pixel data of the current macroblock.
        /// </summary>
        public YCbCr[,] Macroblock => _macroblock;

        /// <summary>
        ///   The pixel data on the left. That is, the right-most pixel data of the macroblock to the left of the current one.
        /// </summary>
        public YCbCr[]? Left => _left;

        /// <summary>
        ///   The pixel data on the top. That is, the bottom-most pixel data of the macroblock to the top of the current one.
        /// </summary>
        public YCbCr[]? Top => _top;

        /// <summary>
        ///   The pixel data on the top-right.
        /// </summary>
        public YCbCr[]? TopRight => _topRight;

        /// <summary>
        ///   The pixel data on the top-left.
        /// </summary>
        public YCbCr? TopLeft
        {
            get => _topLeft;
            set => _topLeft = value;
        }

        /// <summary>
        ///   Returns the pixel at x/y.
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <returns><see cref="YCbCr"/></returns>
        public YCbCr this[int x, int y]
        {
            get
            {
                if (x == -1)
                    return Left![y];
                else if (y == -1)
                    if (x > Top!.Length)
                        return TopRight![x - Top.Length];
                    else
                        return TopLeft!.Value;
                else
                    return Macroblock[x, y];
            }
            set
            {
                if (x == -1)
                    Left![y] = value;
                else if (y == -1)
                    if (x > Top!.Length)
                        TopRight![x - Top.Length] = value;
                    else
                        TopLeft = value;
                else
                    Macroblock[x, y] = value;
            }
        }

        /// <summary>
        ///   Checks if x,y is available.
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <returns>A boolean</returns>
        public bool IsAvailable(int x, int y)
        {
            if (x == -1 && y == -1) return TopLeft != null;
            else if (x == -1) return Left != null;
            else if (y == -1)
            {
                if (x >= Top!.Length)
                    return TopRight != null;
                else
                    return Top != null;
            }
            else return true;
        }
    }
}
