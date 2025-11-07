namespace ContentDotNet.Api.Primitives;
/// <summary>
///   Exception thrown when a decoder for videos, audios, or images encounters an
///   unrecoverable error.
/// </summary>
public class DecoderException : Exception
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="DecoderException"/> class.
    /// </summary>
    public DecoderException()
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="DecoderException"/> class.
    /// </summary>
    /// <param name="message">Exception message</param>
    public DecoderException(string? message) : base(message)
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="DecoderException"/> class.
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="innerException">Inner exception</param>
    public DecoderException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}

/// <summary>
///   Exception thrown when a decoder for videos encounters an unrecoverable error.
/// </summary>
public class VideoCodecDecoderException : DecoderException
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="VideoCodecDecoderException"/> class.
    /// </summary>
    public VideoCodecDecoderException()
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="VideoCodecDecoderException"/> class.
    /// </summary>
    /// <param name="message">Exception message</param>
    public VideoCodecDecoderException(string? message) : base(message)
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="VideoCodecDecoderException"/> class.
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="innerException">Inner exception</param>
    public VideoCodecDecoderException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
