using ContentDotNet.Extensions.H264.Models;

namespace ContentDotNet.Extensions.H264;

/// <summary>
/// Represents a refined slice group map type for H.264 video encoding, including the map type and direction flag.
/// </summary>
public readonly struct RefinedSliceGroupMapType : IEquatable<RefinedSliceGroupMapType>
{
    /// <summary>
    /// Box Out Clockwise slice group map type (type 3, direction false).
    /// </summary>
    public static readonly RefinedSliceGroupMapType BoxOutClockwise = new(3, false);

    /// <summary>
    /// Box Out Counterclockwise slice group map type (type 3, direction true).
    /// </summary>
    public static readonly RefinedSliceGroupMapType BoxOutCounterclockwise = new(3, true);

    /// <summary>
    /// Raster Scan slice group map type (type 4, direction false).
    /// </summary>
    public static readonly RefinedSliceGroupMapType RasterScan = new(4, false);

    /// <summary>
    /// Reverse Raster Scan slice group map type (type 4, direction true).
    /// </summary>
    public static readonly RefinedSliceGroupMapType ReverseRasterScan = new(4, true);

    /// <summary>
    /// Wipe Right slice group map type (type 5, direction false).
    /// </summary>
    public static readonly RefinedSliceGroupMapType WipeRight = new(5, false);

    /// <summary>
    /// Wipe Left slice group map type (type 5, direction true).
    /// </summary>
    public static readonly RefinedSliceGroupMapType WipeLeft = new(5, true);

    /// <summary>
    /// Creates a <see cref="RefinedSliceGroupMapType"/> from the specified slice group map type and direction flag.
    /// </summary>
    /// <param name="sliceGroupMapType">The slice group map type value.</param>
    /// <param name="sliceGroupChangeDirectionFlag">The direction flag for the slice group change.</param>
    /// <returns>A <see cref="RefinedSliceGroupMapType"/> instance representing the specified type and direction.</returns>
    public static RefinedSliceGroupMapType From(uint sliceGroupMapType, bool sliceGroupChangeDirectionFlag)
        => new(sliceGroupMapType, sliceGroupChangeDirectionFlag);

    /// <summary>
    /// Creates a <see cref="RefinedSliceGroupMapType"/> from a <see cref="PictureParameterSet"/> instance.
    /// </summary>
    /// <param name="pps">The <see cref="PictureParameterSet"/> to extract the slice group map type and direction flag from.</param>
    /// <returns>A <see cref="RefinedSliceGroupMapType"/> instance representing the type and direction from the PPS.</returns>
    public static RefinedSliceGroupMapType From(PictureParameterSet pps)
        => From(pps.SliceGroupMapType, pps.SliceGroupChangeDirectionFlag);

    private readonly uint _sliceGroupMapType;
    private readonly bool _sliceGroupChangeDirectionFlag;

    /// <summary>
    /// Initializes a new instance of the <see cref="RefinedSliceGroupMapType"/> struct.
    /// </summary>
    /// <param name="sliceGroupMapType">The slice group map type value.</param>
    /// <param name="sliceGroupChangeDirectionFlag">The direction flag for the slice group change.</param>
    internal RefinedSliceGroupMapType(uint sliceGroupMapType, bool sliceGroupChangeDirectionFlag)
    {
        _sliceGroupMapType = sliceGroupMapType;
        _sliceGroupChangeDirectionFlag = sliceGroupChangeDirectionFlag;
    }

    /// <summary>
    /// Gets the slice group map type value.
    /// </summary>
    public uint SliceGroupMapType => _sliceGroupMapType;

    /// <summary>
    /// Gets a value indicating whether the slice group change direction flag is set.
    /// </summary>
    public bool SliceGroupChangeDirectionFlag => _sliceGroupChangeDirectionFlag;

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is RefinedSliceGroupMapType type && Equals(type);
    }

    /// <inheritdoc/>
    public bool Equals(RefinedSliceGroupMapType other)
    {
        return _sliceGroupMapType == other._sliceGroupMapType &&
               _sliceGroupChangeDirectionFlag == other._sliceGroupChangeDirectionFlag &&
               SliceGroupMapType == other.SliceGroupMapType &&
               SliceGroupChangeDirectionFlag == other.SliceGroupChangeDirectionFlag;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(_sliceGroupMapType, _sliceGroupChangeDirectionFlag, SliceGroupMapType, SliceGroupChangeDirectionFlag);
    }

    /// <summary>
    /// Determines whether two <see cref="RefinedSliceGroupMapType"/> instances are equal.
    /// </summary>
    /// <param name="left">The first instance.</param>
    /// <param name="right">The second instance.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(RefinedSliceGroupMapType left, RefinedSliceGroupMapType right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="RefinedSliceGroupMapType"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first instance.</param>
    /// <param name="right">The second instance.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(RefinedSliceGroupMapType left, RefinedSliceGroupMapType right)
    {
        return !(left == right);
    }
}
