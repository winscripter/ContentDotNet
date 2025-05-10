using ContentDotNet.Extensions.H264.Models;
using ContentDotNet.Extensions.H26x;

namespace ContentDotNet.Extensions.H264.Pictures;

/// <summary>
///   Represents the reference picture.
/// </summary>
public sealed class ReferencePicture : IDisposable
{
    /// <summary>
    ///   Represents the SPS associated with this reference picture.
    /// </summary>
    public SequenceParameterSet Sps { get; set; }

    /// <summary>
    ///   Represents the PPS associated with this reference picture.
    /// </summary>
    public PictureParameterSet Pps { get; set; }

    /// <summary>
    ///   Represents the Slice Header associated with this reference picture.
    /// </summary>
    public SliceHeader SliceHeader { get; set; }

    /// <summary>
    ///   Represents the NAL unit associated with this reference picture.
    /// </summary>
    public NalUnit NalUnit { get; set; }

    /// <summary>
    ///   The frame number.
    /// </summary>
    public int FrameNumber { get; set; }

    /// <summary>
    ///   The POC.
    /// </summary>
    public int PictureOrderCount { get; set; }

    /// <summary>
    ///   The reference type (short term versus long term).
    /// </summary>
    public PictureReferenceType? ReferenceType { get; set; }

    /// <summary>
    ///   The structure.
    /// </summary>
    public PictureStructure PictureStructure { get; set; }

    /// <summary>
    ///   The actual pixels.
    /// </summary>
    public IFrame Frame { get; set; }

    /// <summary>
    ///   The pair field.
    /// </summary>
    public ReferencePicture? PairField { get; set; }

    /// <summary>  
    ///   Initializes a new instance of the <see cref="ReferencePicture"/> class.  
    /// </summary>  
    /// <param name="frameNumber">The frame number of the reference picture.</param>  
    /// <param name="pictureOrderCount">The picture order count (POC) of the reference picture.</param>  
    /// <param name="referenceType">The reference type of the picture (short-term or long-term).</param>  
    /// <param name="pictureStructure">The structure of the picture (frame, top field, or bottom field).</param>  
    /// <param name="frame">The actual frame data associated with the reference picture.</param>  
    /// <param name="pairField">The complementary field, if applicable, for the reference picture.</param>  
    /// <param name="nalu">NAL unit associated with the reference picture</param>
    /// <param name="pps">PPS associated with the reference picture</param>
    /// <param name="sliceHeader">Slice header associated with the reference picture</param>
    /// <param name="sps">SPS associated with the reference picture</param>
    public ReferencePicture(int frameNumber, int pictureOrderCount, PictureReferenceType? referenceType, PictureStructure pictureStructure, IFrame frame, ReferencePicture? pairField, SequenceParameterSet sps, PictureParameterSet pps, SliceHeader sliceHeader, NalUnit nalu)
    {
        FrameNumber = frameNumber;
        PictureOrderCount = pictureOrderCount;
        ReferenceType = referenceType;
        PictureStructure = pictureStructure;
        Frame = frame;
        PairField = pairField;
        Pps = pps;
        SliceHeader = sliceHeader;
    }

    /// <summary>
    ///   Is this a field reference picture?
    /// </summary>
    public bool IsField => PictureStructure == PictureStructure.TopField ||
                           PictureStructure == PictureStructure.BottomField;

    /// <summary>
    ///   Is this picture used for reference?
    /// </summary>
    public bool IsUsedForReference => ReferenceType != null;

    /// <summary>
    ///   Is this a complementary field pair?
    /// </summary>
    /// <param name="other">Frame to compare with</param>
    /// <returns>A boolean, indicating whether or not is this a complementary field pair.</returns>
    public bool IsComplementaryTo(ReferencePicture other)
    {
        if (!this.IsField || !other.IsField) return false;
        if (this.FrameNumber != other.FrameNumber) return false;
        if (this.PictureStructure == other.PictureStructure) return false;

        return true;
    }

    /// <summary>
    ///   Releases memory.
    /// </summary>
    public void Dispose()
    {
        this.Frame.Dispose();
        this.PairField?.Dispose();

        GC.SuppressFinalize(this);
    }
}
