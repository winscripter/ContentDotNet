namespace ContentDotNet.Extensions.Video.H264.Enumerations
{
    /// <summary>
    /// Represents the fundamental H.264 slice types based on slice_type % 5.
    /// </summary>
    public enum H264SliceType
    {
        /// <summary>
        /// I slice (Intra-coded slice). Contains only intra-predicted macroblocks.
        /// Corresponds to slice_type values 0 and 5.
        /// </summary>
        I = 0,

        /// <summary>
        /// P slice (Predicted slice). Contains inter-predicted macroblocks using forward prediction.
        /// Corresponds to slice_type values 1 and 6.
        /// </summary>
        P = 1,

        /// <summary>
        /// B slice (Bi-predictive slice). Contains macroblocks predicted using both past and future frames.
        /// Corresponds to slice_type values 2 and 7.
        /// </summary>
        B = 2,

        /// <summary>
        /// SP slice (Switching P slice). Used for switching between bitstreams with low overhead.
        /// Corresponds to slice_type values 3 and 8.
        /// </summary>
        SP = 3,

        /// <summary>
        /// SI slice (Switching I slice). Used for switching between bitstreams with intra-coded content.
        /// Corresponds to slice_type values 4 and 9.
        /// </summary>
        SI = 4
    }
}
