using ContentDotNet.Extensions.H264.Containers;

namespace ContentDotNet.Extensions.H264;

/// <summary>
///   Represents an H.264 residual that either didn't yet undergo
///   quantization/transforms, or was inverse quantized/transformed.
/// </summary>
public struct ComputedResidual : IEquatable<ComputedResidual>
{
    /// <summary>
    ///   Raw residual data for the Luma channel.
    /// </summary>
    public ComputedResidualChannel Luma;

    /// <summary>
    ///   Raw residual data for the Chroma channel.
    /// </summary>
    public ComputedResidualChannel? Cb;

    /// <summary>
    ///   Raw residual data for the Chroma channel.
    /// </summary>
    public ComputedResidualChannel? Cr;

    /// <summary>
    ///   Initializes a new instance of the <see cref="ComputedResidual"/> struct.
    /// </summary>
    /// <param name="luma">Raw residual data for the Luma channel.</param>
    /// <param name="cb">Raw residual data for the Cb Chroma channel.</param>
    /// <param name="cr">Raw residual data for the Cr Chroma channel.</param>
    public ComputedResidual(ComputedResidualChannel luma, ComputedResidualChannel? cb, ComputedResidualChannel? cr)
    {
        Luma = luma;
        Cb = cb;
        Cr = cr;
    }

    /// <summary>
    ///   Determines whether the specified object is equal to the current <see cref="ComputedResidual"/>.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns><c>true</c> if the specified object is equal to the current instance; otherwise, <c>false</c>.</returns>
    public readonly override bool Equals(object? obj)
    {
        return obj is ComputedResidual residual && Equals(residual);
    }

    /// <summary>
    ///   Indicates whether the current <see cref="ComputedResidual"/> is equal to another <see cref="ComputedResidual"/>.
    /// </summary>
    /// <param name="other">A <see cref="ComputedResidual"/> to compare with this instance.</param>
    /// <returns><c>true</c> if the current instance is equal to the <paramref name="other"/> parameter; otherwise, <c>false</c>.</returns>
    public readonly bool Equals(ComputedResidual other)
    {
        return Luma.Equals(other.Luma) &&
               EqualityComparer<ComputedResidualChannel?>.Default.Equals(Cb, other.Cb) &&
               EqualityComparer<ComputedResidualChannel?>.Default.Equals(Cr, other.Cr);
    }

    /// <summary>
    ///   Returns a hash code for the current <see cref="ComputedResidual"/>.
    /// </summary>
    /// <returns>A hash code for the current instance.</returns>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(Luma, Cb, Cr);
    }

    /// <summary>
    ///   Determines whether two <see cref="ComputedResidual"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="ComputedResidual"/> to compare.</param>
    /// <param name="right">The second <see cref="ComputedResidual"/> to compare.</param>
    /// <returns><c>true</c> if the two <see cref="ComputedResidual"/> instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(ComputedResidual left, ComputedResidual right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///   Determines whether two <see cref="ComputedResidual"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="ComputedResidual"/> to compare.</param>
    /// <param name="right">The second <see cref="ComputedResidual"/> to compare.</param>
    /// <returns><c>true</c> if the two <see cref="ComputedResidual"/> instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(ComputedResidual left, ComputedResidual right)
    {
        return !(left == right);
    }
}
