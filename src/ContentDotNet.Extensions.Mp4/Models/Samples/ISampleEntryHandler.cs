namespace ContentDotNet.Extensions.Mp4.Models.Samples;

/// <summary>
///   Handles reading sample entries.
/// </summary>
public interface ISampleEntryHandler
{
    /// <summary>
    ///   Reads the sample entry from the binary reader, not accounting for previous
    ///   fields provided by the sample entry in general.
    /// </summary>
    /// <param name="reader">Reader where the sample entry data is read.</param>
    /// <returns>The sample entry data, read from the binary reader, of type <see cref="DataType"/>.</returns>
    ISampleEntryData Read(BinaryReader reader);

    /// <summary>
    ///   Represents the actual type of the sample entry data when the <see cref="Read(BinaryReader)"/>
    ///   method is invoked.
    /// </summary>
    Type DataType { get; }

    /// <summary>
    ///   Sample entry identifier (f.e. mp4a).
    /// </summary>
    uint Identifier { get; }

    /// <summary>
    ///   Like <see cref="Identifier"/>, but in its string form. Alternatively, this can be
    ///   obtained using only the <see cref="Identifier"/> property with the preceding
    ///   code snippet.
    ///   <code>
    ///     using ContentDotNet;
    ///     
    ///     string identifierText = new FourCC(ISampleEntryHandler.Identifier).ValueText;
    ///   </code>
    /// </summary>
    string IdentifierText { get; }
}
