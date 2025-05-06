namespace ContentDotNet.Extensions.H264;

/// <summary>
///   Factory for reference picture lists.
/// </summary>
public interface IReferencePictureListFactory
{
    /// <summary>
    ///   Creates the reference picture list.
    /// </summary>
    /// <param name="width">Width of each frame.</param>
    /// <param name="height">Height of each frame.</param>
    /// <param name="maximumPicNumber">Maximum picture number.</param>
    /// <returns>The reference picture list.</returns>
    ReferencePictureList Create(int width, int height, int maximumPicNumber);
}
