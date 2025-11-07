namespace ContentDotNet.Api.Primitives
{
    /// <summary>
    ///   Motion vector
    /// </summary>
    public struct MotionVector
    {
        /// <summary>
        ///   X value
        /// </summary>
        public int X { get; set; }

        /// <summary>
        ///   Y value
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="MotionVector"/> struct.
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        public MotionVector(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
