namespace ContentDotNet;

/// <summary>
///   Interface to provide large data sources efficiently.
/// </summary>
public interface IDataSource
{
    long Length { get; }
    void Write(Stream stream);
    Task WriteAsync(Stream stream);
    void Read(Span<byte> buffer, long offset);
    Task ReadAsync(Memory<byte> buffer, long offset);
}
