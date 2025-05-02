namespace ContentDotNet.Abstractions;

/// <summary>
///   Represents a video codec.
/// </summary>
public interface IVideoCodec : IDisposable
{
    /// <summary>
    ///   The video stream.
    /// </summary>
    Stream Stream { get; }

    /// <summary>
    ///   Video context.
    /// </summary>
    VideoContext Context { get; }

    /// <summary>
    ///   Gets the decoder-specific parameters.
    /// </summary>
    Dictionary<string, object> Parameters { get; }

    /// <summary>
    ///   Retrieves the next frame as YUV data.
    /// </summary>
    /// <returns>A memory structure containing YUV data for the next frame.</returns>
    Memory<Memory<Yuv>> GetNextFrameAsYuv();

    /// <summary>
    ///   Writes a 16x16 block of YUV data to the current frame.
    /// </summary>
    /// <param name="block16x16">The 16x16 block of YUV data to write.</param>
    void WriteFrameBlock(Memory<Memory<Yuv>> block16x16);

    /// <summary>
    ///   Writes a 16x16 block of YUV data to the current frame using a span.
    /// </summary>
    /// <param name="block16x16">The 16x16 block of YUV data to write.</param>
    void WriteFrameBlock(Span<Yuv> block16x16);

    /// <summary>
    ///   Advances to the next block in the video stream.
    /// </summary>
    void NextBlock();

    /// <summary>
    ///   Moves to the previous block in the video stream.
    /// </summary>
    void PreviousBlock();

    /// <summary>
    ///   Gets the number of blocks per width in the video.
    /// </summary>
    int BlocksPerWidth { get; }

    /// <summary>
    ///   Gets the number of blocks per height in the video.
    /// </summary>
    int BlocksPerHeight { get; }

    /// <summary>
    ///   Gets the total number of frames in the video.
    /// </summary>
    int Frames { get; }

    /// <summary>
    ///   Gets or sets the current horizontal block index.
    /// </summary>
    int CurrentHorizontalBlock { get; set; }

    /// <summary>
    ///   Gets or sets the current vertical block index.
    /// </summary>
    int CurrentVerticalBlock { get; set; }

    /// <summary>
    ///   Encodes the video using the specified encoder.
    /// </summary>
    /// <param name="encoder">The encoder to use for encoding the video.</param>
    void CodeTo(IVideoCodecEncoder encoder);
}
