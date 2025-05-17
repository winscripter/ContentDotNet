using ContentDotNet.BitStream;

namespace ContentDotNet.Abstractions;

/// <summary>
///   A low level writer.
/// </summary>
public interface ILowLevelWriter : IDisposable
{
    /// <summary>
    ///   Represents the stream where writing is done.
    /// </summary>
    Stream BaseStream { get; }

    /// <summary>
    ///   A bitstream writer that uses <see cref="BaseStream"/> as the backing stream,
    ///   if present.
    /// </summary>
    BitStreamWriter? BitStreamWriter { get; }

    /// <summary>
    ///   Represents a boolean that implies whether this low level writer
    ///   uses bit stream writer over the direct stream.
    /// </summary>
    bool IsBitStreamBased { get; }
}
