using ContentDotNet.Extensions.H264.Models;

namespace ContentDotNet.Extensions.H264;

/// <summary>
///   A scaling matrix builder.
/// </summary>
public sealed class ScalingMatrixBuilder
{
    /// <summary>
    ///   This is the number of scaling lists that will be built. This
    ///   is automatically set when one calls the <see cref="SequenceParameterSet.WriteScalingMatrix(ContentDotNet.Abstractions.BitStreamWriter, ContentDotNet.Abstractions.BitStreamReader)"/>
    ///   or similar method. Value can either be 8 or 12.
    /// </summary>
    public int ListCount { get; internal set; }

    /// <summary>
    ///   Invoked when building a single scaling list is required.
    /// </summary>
    public ScalingListBuild BuildSink { get; }

    public ScalingMatrixBuilder(ScalingListBuild buildSink)
    {
        BuildSink = buildSink;
    }
}
