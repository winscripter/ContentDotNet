using ContentDotNet.BitStream;

namespace ContentDotNet.Extensions.H26x;

/// <summary>
///   This is a marker interface that could represent SEI models.
/// </summary>
public interface ISeiModel
{
    /// <summary>
    ///   Write the SEI model to the bitstream.
    /// </summary>
    /// <param name="writer">Bitstream where the SEI model is written to.</param>
    /// <param name="parameterList">Parameter list</param>
    void Write(BitStreamWriter writer, SeiModelParameterList? parameterList);

    /// <summary>
    ///   Write the SEI model to the bitstream.
    /// </summary>
    /// <param name="writer">Bitstream where the SEI model is written to.</param>
    /// <param name="parameterList">Parameter list</param>
    /// <returns>An awaitable task.</returns>
    Task WriteAsync(BitStreamWriter writer, SeiModelParameterList? parameterList);

    /// <summary>
    ///   Does the SEI model require <see cref="SeiModelParameterList"/> in
    ///   <see cref="Write(BitStreamWriter, SeiModelParameterList?)"/>
    ///   and <see cref="WriteAsync(BitStreamWriter, SeiModelParameterList?)"/>?
    /// </summary>
    bool RequiresParametersForWrite { get; }
}
