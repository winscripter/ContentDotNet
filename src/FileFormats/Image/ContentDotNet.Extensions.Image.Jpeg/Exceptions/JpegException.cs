namespace ContentDotNet.Extensions.Image.Jpeg.Exceptions
{
    /// <summary>
    ///   An exception with the JPEG procedure.
    /// </summary>
    public class JpegException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JpegException"/> class.
        /// </summary>
        public JpegException()
            : this("Invalid decoding pipeline during JPEG processing")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JpegException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public JpegException(string? message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JpegException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public JpegException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
