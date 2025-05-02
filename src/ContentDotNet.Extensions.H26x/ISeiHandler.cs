namespace ContentDotNet.Extensions.H26x;

/// <summary>
///   Handles parsing SEI models by their code.
/// </summary>
public interface ISeiHandler
{
    /// <summary>
    ///   The integer type of the SEI model that it accepts.
    /// </summary>
    static abstract uint Code { get; }

    /// <summary>
    ///   Type of the underlying model handle.
    /// </summary>
    static abstract Type HandleType { get; }

    /// <summary>
    ///   Given that the specified bitstream is at the location of SEI
    ///   data, skips that data and creates the handle type.
    /// </summary>
    /// <param name="reader">Reader</param>
    /// <returns>A handle.</returns>
    static abstract ISeiModelHandle SkipData(BitStreamReader reader);
}
