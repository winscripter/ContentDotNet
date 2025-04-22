namespace ContentDotNet.Abstractions;

/// <summary>
/// Specifies that something can be written to the bitstream.
/// </summary>
public interface IBitstreamWriteable
{
    /// <summary>
    /// Represents whether or not does the object support writing to the bitstream asynchronously.
    /// </summary>
    bool SupportsAsynchronousWrite { get; }

    /// <summary>
    /// Writes the object to the bitstream.
    /// </summary>
    /// <param name="writer">Bitstream to write to.</param>
    void Write(BitStreamWriter writer);

    /// <summary>
    /// Writes the object to the bitstream.
    /// </summary>
    /// <param name="writer">Bitstream to write to.</param>
    /// <exception cref="NotSupportedException">May be thrown if <see cref="SupportsAsynchronousWrite"/> is <see langword="false"/>.</exception>
    Task WriteAsync(BitStreamWriter writer);
}
