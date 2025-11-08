namespace ContentDotNet.Video.Codecs.H264.Components
{
    using ContentDotNet.Api.Primitives;

    /// <summary>
    ///   Sub-macroblock information
    /// </summary>
    public class H264SubMacroblock
    {
        /// <summary>
        ///   Reference index value per list.
        /// </summary>
        public int[] ReferenceIndices { get; set; } = new int[2];

        /// <summary>
        ///   Motion vector values per list.
        /// </summary>
        public MotionVector[] MotionVectors { get; set; } = new MotionVector[2];
    }
}
