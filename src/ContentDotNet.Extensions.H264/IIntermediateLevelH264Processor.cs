using ContentDotNet.Abstractions;
using ContentDotNet.Extensions.H264.Models;

namespace ContentDotNet.Extensions.H264;

/// <summary>
///   Represents an intermediate level H.264 processor.
/// </summary>
public interface IIntermediateLevelH264Processor
{
    /// <summary>
    ///   Decodes an I-frame.
    /// </summary>
    /// <param name="reader">The bitstream reader</param>
    /// <param name="nal">NAL unit</param>
    /// <param name="sps">SPS</param>
    /// <param name="pps">PPS</param>
    /// <param name="sliceHeader">Slice header</param>
    /// <returns>A heap-allocated YUV grid representing actual frame.</returns>
    Memory<Memory<Yuv>> DecodeIFrameAsGrid(BitStreamReader reader, NalUnit nal, SequenceParameterSet sps, PictureParameterSet pps, SliceHeader sliceHeader);
}
