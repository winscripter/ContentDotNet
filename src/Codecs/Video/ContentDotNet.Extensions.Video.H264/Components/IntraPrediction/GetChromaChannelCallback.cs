namespace ContentDotNet.Extensions.Video.H264.Components.IntraPrediction
{
    using ContentDotNet.Colors;

    /// <summary>
    ///   A delegate that returns the current chroma channel from <paramref name="yCbCr"/>.
    /// </summary>
    /// <param name="yCbCr">YCbCr</param>
    /// <returns>The chroma channel.</returns>
    public delegate int GetChromaChannelCallback(YCbCr yCbCr);
}
