using System.Text;

namespace ContentDotNet.IO;

internal sealed class MemoryFileSystem : IFileSystem
{
    internal sealed class DirectoryEntry
    {
        public string Name { get; set; }
        public List<DirectoryEntry> SubDirectories { get; set; }
        public List<FileEntry> Files { get; set; }

        public DirectoryEntry(List<DirectoryEntry> subDirectories, List<FileEntry> files, string name)
        {
            SubDirectories = subDirectories;
            Files = files;
            Name = name;
        }
    }

    internal sealed class FileEntry
    {
        public string Name { get; set; }
        public MemoryStream Contents { get; set; }

        public FileEntry(string name, MemoryStream contents)
        {
            Name = name;
            Contents = contents;
        }
    }

    private readonly DirectoryEntry _root;

    public MemoryFileSystem()
    {
        _root = new DirectoryEntry(
            [],
            [],
            "/"
        );
    }

    private static bool MatchesPattern(string name, string searchPattern)
    {
        return searchPattern == "*" || name.Contains(searchPattern.Replace("*", ""));
    }

    private DirectoryEntry? FindParentDirectory(string path)
    {
        var segments = path.Trim('/').Split('/');
        if (segments.Length == 1) return null;

        var parentPath = string.Join("/", segments.Take(segments.Length - 1));
        return FindDirectory(parentPath);
    }

    private FileEntry? FindFile(string path)
    {
        var fileName = Path.GetFileName(path);
        var dirPath = Path.GetDirectoryName(path);
        var directory = FindDirectory(dirPath!);

        return directory?.Files.FirstOrDefault(f => f.Name == fileName);
    }

    private DirectoryEntry? FindDirectory(string path)
    {
        var segments = path.Trim('/').Split('/');
        var current = _root;

        foreach (var segment in segments)
        {
            current = current.SubDirectories.FirstOrDefault(d => d.Name == segment);
            if (current == null)
                return null;
        }

        return current;
    }

    public void CopyDirectory(string sourcePath, string destinationPath, bool recursive)
    {
        var sourceDir = FindDirectory(sourcePath) ?? throw new DirectoryNotFoundException($"Source directory {sourcePath} not found");

        var destDir = FindDirectory(destinationPath) ?? throw new DirectoryNotFoundException($"Destination directory {destinationPath} not found");

        var newDir = new DirectoryEntry([], [], sourceDir.Name);
        destDir.SubDirectories.Add(newDir);

        foreach (var file in sourceDir.Files)
            newDir.Files.Add(new FileEntry(file.Name, new MemoryStream(file.Contents.ToArray())));

        if (recursive)
            foreach (var subDir in sourceDir.SubDirectories)
                CopyDirectory($"{sourcePath}/{subDir.Name}", $"{destinationPath}/{subDir.Name}", recursive);
    }

    public void CopyDirectory(string sourcePath, string destinationPath, bool recursive, bool overwrite)
    {
        var sourceDir = FindDirectory(sourcePath) ?? throw new DirectoryNotFoundException($"Source directory {sourcePath} not found");

        var destDir = FindDirectory(destinationPath);
        if (destDir == null)
        {
            destDir = new DirectoryEntry([], [], Path.GetFileName(destinationPath));
            var parentDestDir = FindDirectory(Path.GetDirectoryName(destinationPath)!);
            if (parentDestDir != null)
                parentDestDir.SubDirectories.Add(destDir);
            else
                throw new DirectoryNotFoundException($"Parent destination directory {Path.GetDirectoryName(destinationPath)} not found");
        }

        foreach (var file in sourceDir.Files)
        {
            var existingFile = destDir.Files.FirstOrDefault(f => f.Name == file.Name);
            if (existingFile != null && overwrite)
            {
                existingFile.Contents = new MemoryStream(file.Contents.ToArray());
            }
            else if (existingFile == null)
            {
                destDir.Files.Add(new FileEntry(file.Name, new MemoryStream(file.Contents.ToArray())));
            }
        }

        if (recursive)
        {
            foreach (var subDir in sourceDir.SubDirectories)
            {
                CopyDirectory($"{sourcePath}/{subDir.Name}", $"{destinationPath}/{subDir.Name}", recursive, overwrite);
            }
        }
    }

    public void CopyFile(string sourcePath, string destinationPath)
    {
        var sourceFile = FindFile(sourcePath) ?? throw new FileNotFoundException($"Source file {sourcePath} not found");
        
        var destDirPath = Path.GetDirectoryName(destinationPath)!;
        var destDirectory = FindDirectory(destDirPath) ?? throw new DirectoryNotFoundException($"Destination directory {destDirPath} not found");
        
        var destFile = new FileEntry(Path.GetFileName(destinationPath), new MemoryStream());
        sourceFile.Contents.Position = 0;
        sourceFile.Contents.CopyTo(destFile.Contents);

        destDirectory.Files.Add(destFile);
    }

    public void CreateDirectory(string path)
    {
        var segments = path.Trim('/').Split('/');
        var current = _root;

        foreach (var segment in segments)
        {
            var subDir = current.SubDirectories.FirstOrDefault(d => d.Name == segment);
            if (subDir == null)
            {
                subDir = new DirectoryEntry([], [], segment);
                current.SubDirectories.Add(subDir);
            }
            current = subDir;
        }
    }

    public void DeleteDirectory(string path, bool recursive)
    {
        var parentDir = FindParentDirectory(path);
        var dirToDelete = FindDirectory(path);

        if (dirToDelete == null || parentDir == null)
            throw new DirectoryNotFoundException($"Directory {path} not found");

        if (!recursive && dirToDelete.SubDirectories.Count > 0)
            throw new IOException($"Directory {path} is not empty");

        parentDir.SubDirectories.Remove(dirToDelete);
    }

    public void DeleteFile(string path)
    {
        var fileName = Path.GetFileName(path);
        var dirPath = Path.GetDirectoryName(path);

        var directory = FindDirectory(dirPath!) ?? throw new DirectoryNotFoundException($"Directory {dirPath} not found");
        var file = directory.Files.FirstOrDefault(f => f.Name == fileName) ?? throw new FileNotFoundException($"File {fileName} not found");
        
        directory.Files.Remove(file);
    }

    public bool DirectoryExists(string path)
    {
        var segments = path.Trim('/').Split('/');
        var current = _root;

        foreach (var segment in segments)
        {
            current = current.SubDirectories.FirstOrDefault(d => d.Name == segment);
            if (current == null)
                return false;
        }

        return true;
    }

    public void Dispose()
    {
        DisposeDirectory(_root);
    }

    private static void DisposeDirectory(DirectoryEntry directory)
    {
        foreach (var file in directory.Files)
            file.Contents.Dispose();

        foreach (var subDirectory in directory.SubDirectories)
            DisposeDirectory(subDirectory);
    }


    public IEnumerable<string> EnumerateDirectories(string path, string searchPattern, SearchOption searchOption)
    {
        var directory = FindDirectory(path) ?? throw new DirectoryNotFoundException($"Directory {path} not found");

        var results = new List<string>();
        EnumerateDirectoriesRecursive(directory, searchPattern, searchOption, results, path);
        return results;
    }

    private static void EnumerateDirectoriesRecursive(DirectoryEntry directory, string searchPattern, SearchOption searchOption, List<string> results, string currentPath)
    {
        foreach (var subDirectory in directory.SubDirectories)
        {
            if (MatchesPattern(subDirectory.Name, searchPattern))
                results.Add($"{currentPath}/{subDirectory.Name}");

            if (searchOption == SearchOption.AllDirectories)
                EnumerateDirectoriesRecursive(subDirectory, searchPattern, searchOption, results, $"{currentPath}/{subDirectory.Name}");
        }
    }

    public IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
    {
        var directory = FindDirectory(path) ?? throw new DirectoryNotFoundException($"Directory {path} not found");

        var results = new List<string>();
        EnumerateFilesRecursive(directory, searchPattern, searchOption, results, path);
        return results;
    }

    private static void EnumerateFilesRecursive(DirectoryEntry directory, string searchPattern, SearchOption searchOption, List<string> results, string currentPath)
    {
        foreach (var file in directory.Files)
        {
            if (MatchesPattern(file.Name, searchPattern))
                results.Add($"{currentPath}/{file.Name}");
        }

        if (searchOption == SearchOption.AllDirectories)
        {
            foreach (var subDirectory in directory.SubDirectories)
                EnumerateFilesRecursive(subDirectory, searchPattern, searchOption, results, $"{currentPath}/{subDirectory.Name}");
        }
    }

    public bool FileExists(string path) => FindFile(path) is not null;

    public void MoveDirectory(string sourcePath, string destinationPath)
    {
        var sourceDir = FindDirectory(sourcePath) ?? throw new DirectoryNotFoundException($"Source directory {sourcePath} not found");

        var destDir = FindDirectory(destinationPath) ?? throw new DirectoryNotFoundException($"Destination directory {destinationPath} not found");

        destDir.SubDirectories.Add(sourceDir);

        var parentSourceDir = FindParentDirectory(sourcePath);
        parentSourceDir?.SubDirectories.Remove(sourceDir);
    }

    public void MoveFile(string sourcePath, string destinationPath)
    {
        var sourceFile = FindFile(sourcePath) ?? throw new FileNotFoundException($"Source file {sourcePath} not found");

        var destDirPath = Path.GetDirectoryName(destinationPath);
        var destDirectory = FindDirectory(destDirPath!) ?? throw new DirectoryNotFoundException($"Destination directory {destDirPath} not found");

        var destFile = new FileEntry(Path.GetFileName(destinationPath), sourceFile.Contents);
        destDirectory.Files.Add(destFile);

        var sourceDir = FindDirectory(Path.GetDirectoryName(sourcePath)!);
        sourceDir?.Files.Remove(sourceFile);
    }

    public Stream Open(string path, FileMode mode)
    {
        var file = FindFile(path);
        if (file == null)
        {
            if (mode == FileMode.Create || mode == FileMode.CreateNew || mode == FileMode.OpenOrCreate)
            {
                var directory = FindDirectory(Path.GetDirectoryName(path)!) ?? throw new DirectoryNotFoundException($"Directory not found for {path}");

                file = new FileEntry(Path.GetFileName(path), new MemoryStream());
                directory.Files.Add(file);
            }
            else
            {
                throw new FileNotFoundException($"File {path} not found");
            }
        }

        if (mode == FileMode.Truncate)
            file.Contents.SetLength(0);

        file.Contents.Position = 0;
        return file.Contents;
    }

    public async Task<Stream> OpenAsync(string path, FileMode mode, CancellationToken cancellationToken)
    {
        return await Task.Run(() => Open(path, mode), cancellationToken);
    }

    public Stream OpenRead(string path)
    {
        var file = FindFile(path) ?? throw new FileNotFoundException($"File {path} not found");

        file.Contents.Position = 0;
        return file.Contents;
    }

    public async Task<Stream> OpenReadAsync(string path, CancellationToken cancellationToken)
    {
        var file = FindFile(path) ?? throw new FileNotFoundException($"File {path} not found");

        file.Contents.Position = 0;
        return await Task.FromResult(file.Contents);
    }

    public string ReadAllText(string path)
    {
        var fileName = Path.GetFileName(path);
        var dirPath = Path.GetDirectoryName(path);

        var directory = FindDirectory(dirPath!);
        var file = (directory?.Files.FirstOrDefault(f => f.Name == fileName)) ?? throw new FileNotFoundException($"File {fileName} not found");
        
        using var reader = new StreamReader(file.Contents);
        return reader.ReadToEnd();
    }

    public string ReadAllText(string path, Encoding? encoding)
    {
        var file = FindFile(path) ?? throw new FileNotFoundException($"File {path} not found");

        file.Contents.Position = 0;
        using var reader = new StreamReader(file.Contents, encoding ?? Encoding.UTF8);
        return reader.ReadToEnd();
    }

    public async Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken)
    {
        var file = FindFile(path) ?? throw new FileNotFoundException($"File {path} not found");

        file.Contents.Position = 0;
        using var reader = new StreamReader(file.Contents);
        return await reader.ReadToEndAsync(cancellationToken);
    }

    public async Task<string> ReadAllTextAsync(string path, Encoding? encoding, CancellationToken cancellationToken)
    {
        var file = FindFile(path) ?? throw new FileNotFoundException($"File {path} not found");

        file.Contents.Position = 0;
        using var reader = new StreamReader(file.Contents, encoding ?? Encoding.UTF8);
        return await reader.ReadToEndAsync(cancellationToken);
    }

    public void WriteAllText(string path, string contents)
    {
        var fileName = Path.GetFileName(path);
        var dirPath = Path.GetDirectoryName(path);

        var directory = FindDirectory(dirPath!) ?? throw new DirectoryNotFoundException($"Directory {dirPath} not found");

        var file = directory.Files.FirstOrDefault(f => f.Name == fileName);
        if (file == null)
        {
            file = new FileEntry(fileName, new MemoryStream(Encoding.UTF8.GetBytes(contents)));
            directory.Files.Add(file);
        }
        else
        {
            file.Contents = new MemoryStream(Encoding.UTF8.GetBytes(contents));
        }
    }

    public void WriteAllText(string path, string contents, Encoding? encoding)
    {
        var file = FindFile(path);
        if (file == null)
        {
            var directory = FindDirectory(Path.GetDirectoryName(path)!) ?? throw new DirectoryNotFoundException($"Directory {Path.GetDirectoryName(path)} not found");

            file = new FileEntry(Path.GetFileName(path), new MemoryStream());
            directory.Files.Add(file);
        }

        file.Contents.SetLength(0);
        file.Contents.Position = 0;
        using var writer = new StreamWriter(file.Contents, encoding ?? Encoding.UTF8);
        writer.Write(contents);
    }

    public async Task WriteAllTextAsync(string path, string contents, CancellationToken cancellationToken)
    {
        var file = FindFile(path);
        if (file == null)
        {
            var directory = FindDirectory(Path.GetDirectoryName(path)!) ?? throw new DirectoryNotFoundException($"Directory {Path.GetDirectoryName(path)} not found");

            file = new FileEntry(Path.GetFileName(path), new MemoryStream());
            directory.Files.Add(file);
        }

        file.Contents.SetLength(0);
        file.Contents.Position = 0;

        using var writer = new StreamWriter(file.Contents);
        await writer.WriteAsync(contents.AsMemory(), cancellationToken);
    }

    public async Task WriteAllTextAsync(string path, string contents, Encoding? encoding, CancellationToken cancellationToken)
    {
        var file = FindFile(path);
        if (file == null)
        {
            var directory = FindDirectory(Path.GetDirectoryName(path)!) ?? throw new DirectoryNotFoundException($"Directory {Path.GetDirectoryName(path)} not found");

            file = new FileEntry(Path.GetFileName(path), new MemoryStream());
            directory.Files.Add(file);
        }

        file.Contents.SetLength(0);
        file.Contents.Position = 0;
        using var writer = new StreamWriter(file.Contents, encoding ?? Encoding.UTF8);
        await writer.WriteAsync(contents);
    }
}
