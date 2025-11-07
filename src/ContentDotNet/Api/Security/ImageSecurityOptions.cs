namespace ContentDotNet.Api.Security
{
    using System.Drawing;

    /// <summary>
    ///   Options that might help mitigate memory leaks and Denial of Service (DoS) attacks
    ///   when processing images.
    /// </summary>
    public record struct ImageSecurityOptions
    {
        /// <summary>
        ///   The maximum size per image.
        /// </summary>
        public Size MaximumImageSize;

        /// <summary>
        ///   Throw an exception when a memory leak is suspected.
        /// </summary>
        public bool ThrowOnSuspectedMemoryLeak;

        /// <summary>
        ///   
        /// </summary>
        /// <param name="size"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public readonly void OperatingOnImageSize(Size size)
        {
            if (size.Width > MaximumImageSize.Width ||
                size.Height > MaximumImageSize.Height)
                throw new InvalidOperationException("Attempted to allocate an image that's too large");
        }
    }
}
