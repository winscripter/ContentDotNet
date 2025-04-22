namespace ContentDotNet.Extensions.H264.Models;

/// <summary>
/// Kind of the H.264 parameter set.
/// </summary>
public enum ParameterSetKind
{
    /// <summary>
    /// A Sequence Parameter Set (SPS) - see <see cref="SequenceParameterSet"/>.
    /// </summary>
    Sequence,

    /// <summary>
    /// A Picture Parameter Set (PPS) - see <see cref="PictureParameterSet"/>.
    /// </summary>
    Picture
}
