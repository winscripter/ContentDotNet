namespace ContentDotNet.Extensions.Video.H264.Exceptions
{
    /// <summary>
    /// Represents an exception where the requested entropy coding mode does not match the actual entropy coding mode.
    /// </summary>
    public class MismatchedEntropyException : H264Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MismatchedEntropyException"/> class.
        /// </summary>
        public MismatchedEntropyException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MismatchedEntropyException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MismatchedEntropyException(string? message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MismatchedEntropyException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public MismatchedEntropyException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
