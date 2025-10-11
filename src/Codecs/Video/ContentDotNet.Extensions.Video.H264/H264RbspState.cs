namespace ContentDotNet.Extensions.Video.H264
{
    using ContentDotNet.BitStream;
    using ContentDotNet.Extensions.Video.H264.RbspModels;

    /// <summary>
    ///   H.264 RBSP state.
    /// </summary>
    public class H264RbspState
    {
        /// <summary>
        ///   The SPS
        /// </summary>
        public RbspSequenceParameterSetData? SequenceParameterSetData { get; set; }

        /// <summary>
        ///   The picture parameter set.
        /// </summary>
        public RbspPictureParameterSet? PictureParameterSet { get; set; }

        /// <summary>
        ///   The access unit delimiter.
        /// </summary>
        public RbspAccessUnitDelimiter? AccessUnitDelimiter { get; set; }

        /// <summary>
        ///   The slice header.
        /// </summary>
        public RbspSliceHeader? SliceHeader { get; set; }

        /// <summary>
        ///   The NAL unit.
        /// </summary>
        public RbspNalUnit? NalUnit { get; set; }

        /// <summary>
        ///   The end RBSP offset.
        /// </summary>
        public long RbspEndOffset { get; set; }

        /// <summary>
        ///   Is there more RBSP data?
        /// </summary>
        /// <param name="reader">The source bitstream reader</param>
        /// <returns>A boolean indicating if there's more RBSP data</returns>
        public bool MoreRbspData(BitStreamReader reader) => reader.BaseStream.Position < RbspEndOffset;
    }
}
