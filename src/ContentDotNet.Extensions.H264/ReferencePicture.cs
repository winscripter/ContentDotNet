using ContentDotNet.Extensions.H264.Models;
using ContentDotNet.Extensions.H26x;

namespace ContentDotNet.Extensions.H264;

/// <summary>
///   Represents a reference picture.
/// </summary>
public sealed class ReferencePicture : IEquatable<ReferencePicture?>, IDisposable
{
    /// <summary>
    ///   The actual frame.
    /// </summary>
    public IFrame Frame { get; set; }

    /// <summary>
    ///   The NAL unit associated with this reference picture.
    /// </summary>
    public NalUnit NalUnit { get; set; }

    /// <summary>
    ///   The associated SPS with this reference picture.
    /// </summary>
    public SequenceParameterSet SequenceParameterSet { get; set; }

    /// <summary>
    ///   The associated Picture Parameter Set with this reference picture.
    /// </summary>
    public PictureParameterSet PictureParameterSet { get; set; }

    /// <summary>
    ///   The associated Slice Header with this reference picture.
    /// </summary>
    public SliceHeader SliceHeader { get; set; }

    internal ReferencePicture(IFrame frame, NalUnit nalUnit, SequenceParameterSet sequenceParameterSet, PictureParameterSet pictureParameterSet, SliceHeader sliceHeader)
    {
        Frame = frame;
        NalUnit = nalUnit;
        SequenceParameterSet = sequenceParameterSet;
        PictureParameterSet = pictureParameterSet;
        SliceHeader = sliceHeader;
    }

    /// <summary>  
    /// Determines whether the specified object is equal to the current instance.  
    /// </summary>  
    /// <param name="obj">The object to compare with the current instance.</param>  
    /// <returns><c>true</c> if the specified object is equal to the current instance; otherwise, <c>false</c>.</returns>  
    public override bool Equals(object? obj)
    {
        return Equals(obj as ReferencePicture);
    }

    /// <summary>  
    /// Determines whether the specified <see cref="ReferencePicture"/> is equal to the current instance.  
    /// </summary>  
    /// <param name="other">The <see cref="ReferencePicture"/> to compare with the current instance.</param>  
    /// <returns><c>true</c> if the specified instance is equal to the current instance; otherwise, <c>false</c>.</returns>  
    public bool Equals(ReferencePicture? other)
    {
        return other is not null &&
               EqualityComparer<IFrame>.Default.Equals(Frame, other.Frame) &&
               NalUnit.Equals(other.NalUnit) &&
               SequenceParameterSet.Equals(other.SequenceParameterSet) &&
               PictureParameterSet.Equals(other.PictureParameterSet) &&
               SliceHeader.Equals(other.SliceHeader);
    }

    /// <summary>  
    /// Serves as the default hash function.  
    /// </summary>  
    /// <returns>A hash code for the current object.</returns>  
    public override int GetHashCode()
    {
        return HashCode.Combine(Frame, NalUnit, SequenceParameterSet, PictureParameterSet, SliceHeader);
    }
    
    /// <summary>
    ///   Releases memory.
    /// </summary>
    public void Dispose()
    {
        Frame.Dispose();
        GC.SuppressFinalize(this);
    }

    /// <summary>  
    /// Determines whether two <see cref="ReferencePicture"/> instances are equal.  
    /// </summary>  
    /// <param name="left">The first <see cref="ReferencePicture"/> to compare.</param>  
    /// <param name="right">The second <see cref="ReferencePicture"/> to compare.</param>  
    /// <returns><c>true</c> if the two instances are equal; otherwise, <c>false</c>.</returns>  
    public static bool operator ==(ReferencePicture? left, ReferencePicture? right)
    {
        return EqualityComparer<ReferencePicture>.Default.Equals(left, right);
    }

    /// <summary>  
    /// Determines whether two <see cref="ReferencePicture"/> instances are not equal.  
    /// </summary>  
    /// <param name="left">The first <see cref="ReferencePicture"/> to compare.</param>  
    /// <param name="right">The second <see cref="ReferencePicture"/> to compare.</param>  
    /// <returns><c>true</c> if the two instances are not equal; otherwise, <c>false</c>.</returns>  
    public static bool operator !=(ReferencePicture? left, ReferencePicture? right)
    {
        return !(left == right);
    }
}
