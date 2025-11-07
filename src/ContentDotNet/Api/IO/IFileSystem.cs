using System.Text;

namespace ContentDotNet.Api.IO;

/// <summary>
///   Abstracts a file system.
/// </summary>
public interface IFileSystem : IDisposable
{
    /// <summary>
    /// Reads all text from the specified file.
    /// </summary>
    /// <param name="path">The file path to read from.</param>
    /// <returns>The contents of the file as a string.</returns>
    string ReadAllText(string path);

    /// <summary>
    /// Reads all text from the specified file using the given encoding.
    /// </summary>
    /// <param name="path">The file path to read from.</param>
    /// <param name="encoding">The encoding to use, or null for the default encoding.</param>
    /// <returns>The contents of the file as a string.</returns>
    string ReadAllText(string path, Encoding? encoding);

    /// <summary>
    /// Writes the specified text to a file, overwriting if it exists.
    /// </summary>
    /// <param name="path">The file path to write to.</param>
    /// <param name="contents">The text to write to the file.</param>
    void WriteAllText(string path, string contents);

    /// <summary>
    /// Writes the specified text to a file using the given encoding, overwriting if it exists.
    /// </summary>
    /// <param name="path">The file path to write to.</param>
    /// <param name="contents">The text to write to the file.</param>
    /// <param name="encoding">The encoding to use, or null for the default encoding.</param>
    void WriteAllText(string path, string contents, Encoding? encoding);

    /// <summary>
    /// Asynchronously reads all text from the specified file.
    /// </summary>
    /// <param name="path">The file path to read from.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, with the file contents as a string.</returns>
    Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously reads all text from the specified file using the given encoding.
    /// </summary>
    /// <param name="path">The file path to read from.</param>
    /// <param name="encoding">The encoding to use, or null for the default encoding.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, with the file contents as a string.</returns>
    Task<string> ReadAllTextAsync(string path, Encoding? encoding, CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously writes the specified text to a file, overwriting if it exists.
    /// </summary>
    /// <param name="path">The file path to write to.</param>
    /// <param name="contents">The text to write to the file.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous write operation.</returns>
    Task WriteAllTextAsync(string path, string contents, CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously writes the specified text to a file using the given encoding, overwriting if it exists.
    /// </summary>
    /// <param name="path">The file path to write to.</param>
    /// <param name="contents">The text to write to the file.</param>
    /// <param name="encoding">The encoding to use, or null for the default encoding.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous write operation.</returns>
    Task WriteAllTextAsync(string path, string contents, Encoding? encoding, CancellationToken cancellationToken);

    /// <summary>
    /// Opens a file for reading.
    /// </summary>
    /// <param name="path">The file path to open.</param>
    /// <returns>A read-only stream for the file.</returns>
    Stream OpenRead(string path);

    /// <summary>
    /// Opens a file with the specified mode.
    /// </summary>
    /// <param name="path">The file path to open.</param>
    /// <param name="mode">The file mode to use.</param>
    /// <returns>A stream for the file.</returns>
    Stream Open(string path, FileMode mode);

    /// <summary>
    /// Asynchronously opens a file for reading.
    /// </summary>
    /// <param name="path">The file path to open.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, with a read-only stream for the file.</returns>
    Task<Stream> OpenReadAsync(string path, CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously opens a file with the specified mode.
    /// </summary>
    /// <param name="path">The file path to open.</param>
    /// <param name="mode">The file mode to use.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, with a stream for the file.</returns>
    Task<Stream> OpenAsync(string path, FileMode mode, CancellationToken cancellationToken);

    /// <summary>
    /// Determines whether the specified file exists.
    /// </summary>
    /// <param name="path">The file path to check.</param>
    /// <returns>True if the file exists; otherwise, false.</returns>
    bool FileExists(string path);

    /// <summary>
    /// Determines whether the specified directory exists.
    /// </summary>
    /// <param name="path">The directory path to check.</param>
    /// <returns>True if the directory exists; otherwise, false.</returns>
    bool DirectoryExists(string path);

    /// <summary>
    /// Enumerates files in the specified directory that match the search pattern and option.
    /// </summary>
    /// <param name="path">The directory path to search.</param>
    /// <param name="searchPattern">The search string to match against file names.</param>
    /// <param name="searchOption">Specifies whether to search all subdirectories or only the top directory.</param>
    /// <returns>An enumerable collection of file paths.</returns>
    IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption);

    /// <summary>
    /// Enumerates directories in the specified directory that match the search pattern and option.
    /// </summary>
    /// <param name="path">The directory path to search.</param>
    /// <param name="searchPattern">The search string to match against directory names.</param>
    /// <param name="searchOption">Specifies whether to search all subdirectories or only the top directory.</param>
    /// <returns>An enumerable collection of directory paths.</returns>
    IEnumerable<string> EnumerateDirectories(string path, string searchPattern, SearchOption searchOption);

    /// <summary>
    /// Creates a directory at the specified path.
    /// </summary>
    /// <param name="path">The directory path to create.</param>
    void CreateDirectory(string path);

    /// <summary>
    /// Deletes the specified directory.
    /// </summary>
    /// <param name="path">The directory path to delete.</param>
    /// <param name="recursive">True to delete subdirectories and files; otherwise, false.</param>
    void DeleteDirectory(string path, bool recursive);

    /// <summary>
    /// Deletes the specified file.
    /// </summary>
    /// <param name="path">The file path to delete.</param>
    void DeleteFile(string path);

    /// <summary>
    /// Moves a file to a new location.
    /// </summary>
    /// <param name="sourcePath">The file to move.</param>
    /// <param name="destinationPath">The destination path.</param>
    void MoveFile(string sourcePath, string destinationPath);

    /// <summary>
    /// Moves a directory to a new location.
    /// </summary>
    /// <param name="sourcePath">The directory to move.</param>
    /// <param name="destinationPath">The destination path.</param>
    void MoveDirectory(string sourcePath, string destinationPath);

    /// <summary>
    /// Copies a file to a new location.
    /// </summary>
    /// <param name="sourcePath">The file to copy.</param>
    /// <param name="destinationPath">The destination path.</param>
    void CopyFile(string sourcePath, string destinationPath);

    /// <summary>
    /// Copies a directory to a new location.
    /// </summary>
    /// <param name="sourcePath">The directory to copy.</param>
    /// <param name="destinationPath">The destination path.</param>
    /// <param name="recursive">True to copy subdirectories and files; otherwise, false.</param>
    void CopyDirectory(string sourcePath, string destinationPath, bool recursive);

    /// <summary>
    /// Copies a directory to a new location, optionally overwriting existing files.
    /// </summary>
    /// <param name="sourcePath">The directory to copy.</param>
    /// <param name="destinationPath">The destination path.</param>
    /// <param name="recursive">True to copy subdirectories and files; otherwise, false.</param>
    /// <param name="overwrite">True to overwrite existing files; otherwise, false.</param>
    void CopyDirectory(string sourcePath, string destinationPath, bool recursive, bool overwrite);
}
