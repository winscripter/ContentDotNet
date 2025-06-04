using ContentDotNet.Abstractions;

namespace ContentDotNet.Extensions.G722;

/// <summary>
///   Represents the G.722 codec.
/// </summary>
public interface IG722Codec : IPcmAudioCodec
{
    /// <summary>
    ///   Represents the operation mode (0 to 2). Default value is 0.
    /// </summary>
    int Mode { get; set; }
}
