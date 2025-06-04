using ContentDotNet.Containers;
using ContentDotNet.Extensions.H264.Models;

namespace ContentDotNet.Extensions.H264.Models.Svc;

/// <summary>
/// Represents the prediction information for a sub-macroblock in SVC (Scalable Video Coding).
/// </summary>
public struct SvcSubMacroblockPrediction : IEquatable<SvcSubMacroblockPrediction>
{
    /// <summary>
    /// The sub-macroblock type.
    /// </summary>
    public Container4UInt32 SubMbType;

    /// <summary>
    /// The motion prediction flags for list 0.
    /// </summary>
    public Container4Boolean MotionPredictionFlagL0;

    /// <summary>
    /// The motion prediction flags for list 1.
    /// </summary>
    public Container4Boolean MotionPredictionFlagL1;

    /// <summary>
    /// The reference indices for list 0.
    /// </summary>
    public Container4UInt32 RefIdxL0;

    /// <summary>
    /// The reference indices for list 1.
    /// </summary>
    public Container4UInt32 RefIdxL1;

    /// <summary>
    /// The motion vector differences for list 0.
    /// </summary>
    public ContainerMatrix4x4x2 MvdL0;

    /// <summary>
    /// The motion vector differences for list 1.
    /// </summary>
    public ContainerMatrix4x4x2 MvdL1;

    /// <summary>
    /// Initializes a new instance of the <see cref="SvcSubMacroblockPrediction"/> struct.
    /// </summary>
    /// <param name="subMbType">The sub-macroblock type.</param>
    /// <param name="motionPredictionFlagL0">The motion prediction flags for list 0.</param>
    /// <param name="motionPredictionFlagL1">The motion prediction flags for list 1.</param>
    /// <param name="refIdxL0">The reference indices for list 0.</param>
    /// <param name="refIdxL1">The reference indices for list 1.</param>
    /// <param name="mvdL0">The motion vector differences for list 0.</param>
    /// <param name="mvdL1">The motion vector differences for list 1.</param>
    public SvcSubMacroblockPrediction(Container4UInt32 subMbType, Container4Boolean motionPredictionFlagL0, Container4Boolean motionPredictionFlagL1, Container4UInt32 refIdxL0, Container4UInt32 refIdxL1, ContainerMatrix4x4x2 mvdL0, ContainerMatrix4x4x2 mvdL1)
    {
        SubMbType = subMbType;
        MotionPredictionFlagL0 = motionPredictionFlagL0;
        MotionPredictionFlagL1 = motionPredictionFlagL1;
        RefIdxL0 = refIdxL0;
        RefIdxL1 = refIdxL1;
        MvdL0 = mvdL0;
        MvdL1 = mvdL1;
    }

    /// <inheritdoc/>
    public readonly override bool Equals(object? obj)
    {
        return obj is SvcSubMacroblockPrediction prediction && Equals(prediction);
    }

    /// <summary>
    /// Determines whether the current instance is equal to another instance of <see cref="SvcSubMacroblockPrediction"/>.
    /// </summary>
    /// <param name="other">The other instance to compare with.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public readonly bool Equals(SvcSubMacroblockPrediction other)
    {
        return SubMbType.Equals(other.SubMbType) &&
               MotionPredictionFlagL0.Equals(other.MotionPredictionFlagL0) &&
               MotionPredictionFlagL1.Equals(other.MotionPredictionFlagL1) &&
               RefIdxL0.Equals(other.RefIdxL0) &&
               RefIdxL1.Equals(other.RefIdxL1) &&
               MvdL0.Equals(other.MvdL0) &&
               MvdL1.Equals(other.MvdL1);
    }

    /// <inheritdoc/>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(SubMbType, MotionPredictionFlagL0, MotionPredictionFlagL1, RefIdxL0, RefIdxL1, MvdL0, MvdL1);
    }

    /// <summary>
    /// Determines whether two instances of <see cref="SvcSubMacroblockPrediction"/> are equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(SvcSubMacroblockPrediction left, SvcSubMacroblockPrediction right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two instances of <see cref="SvcSubMacroblockPrediction"/> are not equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(SvcSubMacroblockPrediction left, SvcSubMacroblockPrediction right)
    {
        return !(left == right);
    }
}
