namespace ContentDotNet.Extensions.Video.H264.Models
{
    /// <summary>
    ///   Motion vector used in Inter prediction.
    /// </summary>
    public record struct H264MotionVector
    {
        /// <summary>
        ///   X component of the motion vector
        /// </summary>
        public int X { get; set; }

        /// <summary>
        ///   Y component of the motion vector
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="H264MotionVector"/> struct.
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        public H264MotionVector(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
