using System.Text;

namespace ContentDotNet.IO;

internal sealed class PhysicalFileSystem : IFileSystem
{
    public void CopyDirectory(string sourcePath, string destinationPath, bool recursive)
    {
        Directory.CreateDirectory(destinationPath);
        if (recursive)
            RecursiveCopy(sourcePath, destinationPath);
        else
            RootCopy(sourcePath, destinationPath);

        static void RootCopy(string sourcePath, string destinationPath)
        {
            Directory.CreateDirectory(destinationPath);
            foreach (string file in Directory.EnumerateFiles(sourcePath, "*.*"))
                File.Copy(file, Path.Combine(destinationPath, file));
        }

        static void RecursiveCopy(string sourcePath, string destinationPath)
        {
            RootCopy(sourcePath, destinationPath);

            foreach (string directory in Directory.EnumerateDirectories(sourcePath, "*.*"))
                RecursiveCopy(directory, Path.Combine(destinationPath, Path.GetFileName(directory)));
        }
    }

    public void CopyDirectory(string sourcePath, string destinationPath, bool recursive, bool overwrite)
    {
        throw new NotImplementedException();
    }

    public void CopyFile(string sourcePath, string destinationPath)
    {
        File.Copy(sourcePath, destinationPath);
    }

    public void CreateDirectory(string path)
    {
        Directory.CreateDirectory(path);
    }

    public void DeleteDirectory(string path, bool recursive)
    {
        Directory.Delete(path, recursive);
    }

    public void DeleteFile(string path)
    {
        File.Delete(path);
    }

    public bool DirectoryExists(string path)
    {
        return Directory.Exists(path);
    }

    public void Dispose()
    {
        // No disposal logic needed for this implementation

        GC.SuppressFinalize(this);
    }

    public IEnumerable<string> EnumerateDirectories(string path, string searchPattern, SearchOption searchOption)
    {
        return Directory.EnumerateDirectories(path, searchPattern, searchOption);
    }

    public IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
    {
        return Directory.EnumerateFiles(path, searchPattern, searchOption);
    }

    public bool FileExists(string path)
    {
        return File.Exists(path);
    }

    public void MoveDirectory(string sourcePath, string destinationPath)
    {
        Directory.Move(sourcePath, destinationPath);
    }

    public void MoveFile(string sourcePath, string destinationPath)
    {
        File.Move(sourcePath, destinationPath);
    }

    public Stream Open(string path, FileMode mode)
    {
        return File.Open(path, mode);
    }

    public async Task<Stream> OpenAsync(string path, FileMode mode, CancellationToken cancellationToken)
    {
        return await Task.Run(() => File.Open(path, mode), cancellationToken);
    }

    public Stream OpenRead(string path)
    {
        return File.OpenRead(path);
    }

    public async Task<Stream> OpenReadAsync(string path, CancellationToken cancellationToken)
    {
        return await Task.Run(() => File.OpenRead(path), cancellationToken);
    }

    public string ReadAllText(string path)
    {
        return File.ReadAllText(path);
    }

    public string ReadAllText(string path, Encoding? encoding)
    {
        return File.ReadAllText(path, encoding ?? Encoding.UTF8);
    }

    public async Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken)
    {
        return await File.ReadAllTextAsync(path, cancellationToken);
    }

    public async Task<string> ReadAllTextAsync(string path, Encoding? encoding, CancellationToken cancellationToken)
    {
        return await File.ReadAllTextAsync(path, encoding ?? Encoding.UTF8, cancellationToken);
    }

    public void WriteAllText(string path, string contents)
    {
        File.WriteAllText(path, contents);
    }

    public void WriteAllText(string path, string contents, Encoding? encoding)
    {
        File.WriteAllText(path, contents, encoding ?? Encoding.UTF8);
    }

    public async Task WriteAllTextAsync(string path, string contents, CancellationToken cancellationToken)
    {
        await File.WriteAllTextAsync(path, contents, cancellationToken);
    }

    public async Task WriteAllTextAsync(string path, string contents, Encoding? encoding, CancellationToken cancellationToken)
    {
        await File.WriteAllTextAsync(path, contents, encoding ?? Encoding.UTF8, cancellationToken);
    }
}
