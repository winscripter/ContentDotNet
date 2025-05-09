using ContentDotNet.Extensions.H26x;

namespace ContentDotNet.Extensions.H264.Pictures;

/// <summary>
///   Represents a frame factory.
/// </summary>
public interface IFrameFactory
{
    /// <summary>
    ///   Creates the picture.
    /// </summary>
    /// <param name="width">Width of the picture</param>
    /// <param name="height">Height of the picture</param>
    /// <returns>The frame</returns>
    IFrame Create(int width, int height);
}
