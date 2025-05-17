namespace ContentDotNet.Abstractions;

/// <summary>
///   Represents a video codec service.
/// </summary>
public interface IVideoCodecService
{
    /// <summary>
    ///   Represents performance options for the video codec. Note that
    ///   some settings will not apply if unsupported. For instance,
    ///   if <see cref="PerformanceMetrics.EnableHardwareAcceleration"/>
    ///   is <see langword="true"/> but <see cref="SupportsHardwareAcceleration"/>
    ///   is <see langword="false"/>, a <see cref="PerformanceMetricsMismatchException"/>
    ///   will be thrown when any operation will be attempted to be executed.
    /// </summary>
    PerformanceMetrics PerformanceMetrics { get; }

    /// <summary>
    ///   Is multithreading supported?
    /// </summary>
    bool SupportsMultithreading { get; }

    /// <summary>
    ///   Is hardware acceleration supported?
    /// </summary>
    bool SupportsHardwareAcceleration { get; }

    /// <summary>
    ///   Is asynchronous read supported?
    /// </summary>
    bool SupportsAsyncRead { get; }

    /// <summary>
    ///   Loads video from the given stream.
    /// </summary>
    /// <param name="stream">A stream where the video is located.</param>
    IVideoCodec Read(Stream stream);

    /// <summary>
    ///   Loads video from the given stream.
    /// </summary>
    /// <param name="stream">A stream where the video is located.</param>
    /// <exception cref="InvalidOperationException">Thrown if <see cref="SupportsAsyncRead"/> is <see langword="false"/>.</exception>
    Task<IVideoCodec> ReadAsync(Stream stream);
}
