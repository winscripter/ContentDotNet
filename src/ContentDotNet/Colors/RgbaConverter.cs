namespace ContentDotNet.Colors
{
    /// <summary>
    ///   Convert colors to and from RGBA.
    /// </summary>
    public abstract class RgbaConverter
    {
        /// <summary>
        ///   Convert to RGBA.
        /// </summary>
        /// <param name="color">Source color of known type.</param>
        /// <returns>The color as RGBA.</returns>
        public abstract Rgba ToRgba(IColor color);

        /// <summary>
        ///   Convert from RGBA.
        /// </summary>
        /// <param name="rgba">Source RGBA color.</param>
        /// <returns>Color of known type.</returns>
        public abstract IColor FromRgba(Rgba rgba);
    }
}
