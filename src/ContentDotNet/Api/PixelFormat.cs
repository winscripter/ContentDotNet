namespace ContentDotNet.Api
{
    /// <summary>
    ///   Built-in pixel format
    /// </summary>
    public enum PixelFormat
    {
        Rgb,
        Rgba,
        Argb,
        Bgr,
        Bgra,
        Abgr,

        Yuv,
        YCbCr = Yuv,
        Y,
        L = Y,
        Uv,
        CbCr = Uv,
    }
}
