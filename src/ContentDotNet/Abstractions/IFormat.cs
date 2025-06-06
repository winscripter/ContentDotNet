namespace ContentDotNet.Abstractions;

/// <summary>
///   Defines a format.
/// </summary>
public interface IFormat
{
    /// <summary>
    ///   Does the format contain either a header or magic?
    /// </summary>
    bool ContainsHeaderOrMagic { get; }

    /// <summary>
    ///   Default MIME type.
    /// </summary>
    string DefaultMimeType { get; }

    /// <summary>
    ///   All MIME types.
    /// </summary>
    string[] MimeTypes { get; }

    /// <summary>
    ///   Default file extension.
    /// </summary>
    string DefaultFileExtension { get; }

    /// <summary>
    ///   All file extensions.
    /// </summary>
    string[] FileExtensions { get; }

    /// <summary>
    ///   Generic name of the format.
    /// </summary>
    string Name { get; }

    /// <summary>
    ///   Name displayed to the user.
    /// </summary>
    string UIName { get; }

    /// <summary>
    ///   Is this format usually embedded in another file?
    /// </summary>
    bool IsEmbedded { get; }
}
