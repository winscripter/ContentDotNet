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
    /// <param name="payloadType">Type of the payload</param>
    /// <param name="payloadSize">Size of the payload</param>
    /// <returns>Actual SEI model</returns>
    ISeiModel Read(BitStreamReader reader, uint payloadType, uint payloadSize);

    /// <summary>
    ///   Type of the SEI model created by the <see cref="Read(BitStreamReader)"/> method.
    /// </summary>
    Type ModelType { get; }
}
