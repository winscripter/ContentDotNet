namespace ContentDotNet.Extensions.Video.H264.Exceptions
{
    /// <summary>
    ///   Exception with the H.264 inter decoder.
    /// </summary>
    public class InterDecoderException : H264Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InterDecoderException"/> class.
        /// </summary>
        public InterDecoderException()
            : this("Could not decode an Inter-coded frame")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterDecoderException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InterDecoderException(string? message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterDecoderException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public InterDecoderException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
