namespace ContentDotNet.Video.Codecs.H264.Components
{
    /// <summary>
    ///   Single context variable.
    /// </summary>
    public struct H264CabacContextVariable
    {
        /// <summary>
        ///   The state.
        /// </summary>
        public ushort State;

        /// <summary>
        ///   MPS?
        /// </summary>
        public bool Mps;

        public H264CabacContextVariable(ushort state, bool mps)
        {
            State = state;
            Mps = mps;
        }
    }
}
