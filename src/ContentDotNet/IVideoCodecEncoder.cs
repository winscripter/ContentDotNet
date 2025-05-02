namespace ContentDotNet;

/// <summary>
/// Represents an encoder for video codecs, providing methods and properties for encoding video frames.
/// </summary>
public interface IVideoCodecEncoder : IDisposable
{
    /// <summary>
    /// Gets the stream associated with the encoder.
    /// </summary>
    Stream Stream { get; }

    /// <summary>
    /// Gets the index of the current frame being encoded.
    /// </summary>
    int CurrentFrame { get; }

    /// <summary>
    /// Gets the frames per second (FPS) of the video being encoded.
    /// </summary>
    float FPS { get; }

    /// <summary>
    /// Gets the aspect ratio of the video. Default is <see cref="AspectRatio.AR1_1"/>.
    /// </summary>
    AspectRatio AspectRatio { get; } // default: AspectRatio.AR1_1

    /// <summary>
    /// Gets the bit depth for the luma channel. Default is 8.
    /// </summary>
    byte LumaBitDepth { get; } // Default: 8

    /// <summary>
    /// Gets the bit depth for the chroma channel. Default is 8.
    /// </summary>
    byte ChromaBitDepth { get; } // Default: 8

    /// <summary>
    /// Gets a dictionary of encoder-specific parameters.
    /// </summary>
    Dictionary<string, object> EncoderParameters { get; }

    /// <summary>
    /// Marks the start of a frame in the encoding process.
    /// </summary>
    void MarkFrameStart();

    /// <summary>
    /// Marks the end of a frame in the encoding process.
    /// </summary>
    void MarkFrameEnd();

    /// <summary>
    /// Advances to the next row in the current frame.
    /// </summary>
    void NextRow();

    /// <summary>
    /// Writes a 16x16 block of YUV data to the current frame.
    /// </summary>
    /// <param name="block16x16">The 16x16 block of YUV data to write.</param>
    void WriteBlock(Span<Yuv> block16x16);

    /// <summary>
    /// Writes a block of YUV data to the current frame at the specified position.
    /// </summary>
    /// <param name="block">The block of YUV data to write.</param>
    /// <param name="blockX">The horizontal position of the block.</param>
    /// <param name="blockY">The vertical position of the block.</param>
    void WriteBlock(Span<Yuv> block, int blockX, int blockY);

    /// <summary>
    /// Gets or sets the number of blocks per row in the current frame.
    /// </summary>
    int BlocksPerRow { get; set; }

    /// <summary>
    /// Gets or sets the number of rows in the current frame.
    /// </summary>
    int Rows { get; set; }

    /// <summary>
    /// Gets or sets the total number of frames in the video.
    /// </summary>
    int Frames { get; set; }

    /// <summary>
    /// Encodes the current frame using the specified codec.
    /// </summary>
    /// <param name="codec">The codec to use for encoding the frame.</param>
    void CodeCurrentFrame(IVideoCodec codec);

    /// <summary>
    /// Encodes the current frame and advances both the encoder and the codec to the next frame.
    /// </summary>
    /// <param name="codec">The codec to use for encoding the frame.</param>
    void CodeCurrentFrameAndAdvance(IVideoCodec codec);

    /// <summary>
    /// Encodes the current frame and optionally advances the encoder and/or the codec to the next frame.
    /// </summary>
    /// <param name="codec">The codec to use for encoding the frame.</param>
    /// <param name="advanceInEncoder">Whether to advance the encoder to the next frame.</param>
    /// <param name="advanceInCodec">Whether to advance the codec to the next frame.</param>
    void CodeCurrentFrameAndAdvance(IVideoCodec codec, bool advanceInEncoder, bool advanceInCodec);
}
