namespace ContentDotNet.Extensions.H264.Models;

/// <summary>
/// A marker interface representing one of parameter sets - see <see cref="PictureParameterSet"/>
/// and <see cref="SequenceParameterSet"/>.
/// </summary>
public interface IParameterSet
{
    /// <summary>
    ///   Kind of the parameter set.
    /// </summary>
    ParameterSetKind Kind { get; }
}
