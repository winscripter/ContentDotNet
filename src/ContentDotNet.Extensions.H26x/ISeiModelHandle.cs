using ContentDotNet.Abstractions;

namespace ContentDotNet.Extensions.H26x;

/// <summary>
///   Represents handle to an SEI model.
/// </summary>
public interface ISeiModelHandle
{
    /// <summary>
    ///   Start of SEI model.
    /// </summary>
    ReaderState Start { get; }

    /// <summary>
    ///   End of SEI model.
    /// </summary>
    ReaderState End { get; }

    /// <summary>
    ///   Reads the model to meaningful data.
    /// </summary>
    /// <param name="reader">Bitstream reader</param>
    /// <returns>Actual SEI model</returns>
    ISeiModel Read(Abstractions.BitStreamReader reader);

    /// <summary>
    ///   Type of the SEI model created by the <see cref="Read(Abstractions.BitStreamReader)"/> method.
    /// </summary>
    static abstract Type ModelType { get; }
}
