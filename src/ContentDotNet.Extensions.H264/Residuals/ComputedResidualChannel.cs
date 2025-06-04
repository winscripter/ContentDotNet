using ContentDotNet.Containers;

namespace ContentDotNet.Extensions.H264.Residuals;

/// <summary>
///   Represents raw values of residuals per channel (luma/cb/cr).
/// </summary>
public struct ComputedResidualChannel : IEquatable<ComputedResidualChannel>
{
    /// <summary>
    ///   The DC level.
    /// </summary>
    public Container64UInt32 DC;

    /// <summary>
    ///   The AC level.
    /// </summary>
    public ContainerMatrix16x16 AC;

    /// <summary>
    ///   8x8 level
    /// </summary>
    public ContainerMatrix4x64 Level8x8;

    /// <summary>
    ///   4x4 level
    /// </summary>
    public ContainerMatrix4x64 Level4x4;

    /// <summary>
    ///   Initializes a new instance of the <see cref="ComputedResidualChannel"/> struct.
    /// </summary>
    /// <param name="dc">The DC level as a <see cref="Container64UInt32"/>.</param>
    /// <param name="ac">The AC level as a <see cref="ContainerMatrix16x16"/>.</param>
    /// <param name="level8x8">The 8x8 level as a <see cref="ContainerMatrix4x64"/>.</param>
    /// <param name="level4x4">The 4x4 level as a <see cref="ContainerMatrix4x64"/>.</param>
    public ComputedResidualChannel(Container64UInt32 dc, ContainerMatrix16x16 ac, ContainerMatrix4x64 level8x8, ContainerMatrix4x64 level4x4)
    {
        DC = dc;
        AC = ac;
        Level8x8 = level8x8;
        Level4x4 = level4x4;
    }

    /// <summary>
    ///   Determines whether the specified object is equal to the current <see cref="ComputedResidualChannel"/>.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns><c>true</c> if the specified object is equal to the current instance; otherwise, <c>false</c>.</returns>
    public readonly override bool Equals(object? obj)
    {
        return obj is ComputedResidualChannel channel && Equals(channel);
    }

    /// <summary>
    ///   Indicates whether the current <see cref="ComputedResidualChannel"/> is equal to another <see cref="ComputedResidualChannel"/>.
    /// </summary>
    /// <param name="other">A <see cref="ComputedResidualChannel"/> to compare with this instance.</param>
    /// <returns><c>true</c> if the current instance is equal to the <paramref name="other"/> parameter; otherwise, <c>false</c>.</returns>
    public readonly bool Equals(ComputedResidualChannel other)
    {
        return DC.Equals(other.DC) &&
               EqualityComparer<ContainerMatrix16x16>.Default.Equals(AC, other.AC) &&
               EqualityComparer<ContainerMatrix4x64>.Default.Equals(Level8x8, other.Level8x8) &&
               EqualityComparer<ContainerMatrix4x64>.Default.Equals(Level4x4, other.Level4x4);
    }

    /// <summary>
    ///   Returns a hash code for the current <see cref="ComputedResidualChannel"/>.
    /// </summary>
    /// <returns>A hash code for the current instance.</returns>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(DC, AC, Level8x8, Level4x4);
    }

    /// <summary>
    ///   Determines whether two <see cref="ComputedResidualChannel"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="ComputedResidualChannel"/> to compare.</param>
    /// <param name="right">The second <see cref="ComputedResidualChannel"/> to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(ComputedResidualChannel left, ComputedResidualChannel right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///   Determines whether two <see cref="ComputedResidualChannel"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="ComputedResidualChannel"/> to compare.</param>
    /// <param name="right">The second <see cref="ComputedResidualChannel"/> to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(ComputedResidualChannel left, ComputedResidualChannel right)
    {
        return !(left == right);
    }
}
