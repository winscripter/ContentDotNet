namespace ContentDotNet.Api.IO;

/// <summary>
///   The file system factory.
/// </summary>
public static class FileSystemFactory
{
    private static readonly IFileSystem _fsMemory = new MemoryFileSystem();
    private static readonly IFileSystem _fsPhysical = new PhysicalFileSystem();

    /// <summary>
    ///   Returns a transient memory file system.
    /// </summary>
    /// <returns>An in-memory file system.</returns>
    public static IFileSystem GetMemoryFileSystem() => _fsMemory;

    /// <summary>
    ///   Returns a transient physical file system.
    /// </summary>
    /// <returns>A file system whose operations map to the actual user's storage.</returns>
    public static IFileSystem GetPhysicalFileSystem() => _fsPhysical;
}
