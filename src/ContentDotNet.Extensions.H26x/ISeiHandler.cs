namespace ContentDotNet.Extensions.H26x;

/// <summary>
///   Handles parsing SEI models by their code.
/// </summary>
public interface ISeiHandler
{
    /// <summary>
    ///   The integer type of the SEI model that it accepts.
    /// </summary>
    uint Code { get; }

    /// <summary>
    ///   Type of the underlying model handle.
    /// </summary>
    Type HandleType { get; }

    /// <summary>
    ///   Given that the specified bitstream is at the location of SEI
    ///   data, skips that data and creates the handle type.
    /// </summary>
    /// <param name="reader">Reader</param>
    /// <param name="payloadType">Type of the payload</param>
    /// <param name="payloadSize">Size of the payload</param>
    /// <returns>A handle.</returns>
    ISeiModelHandle SkipData(BitStreamReader reader, uint payloadType, uint payloadSize);
}
