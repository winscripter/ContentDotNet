namespace ContentDotNet.Extensions.Video.H264.Exceptions
{
    /// <summary>
    ///   Exception thrown during intra prediction.
    /// </summary>
    public class IntraPredictionException : H264Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntraPredictionException"/> class.
        /// </summary>
        public IntraPredictionException()
            : this("An Intra prediction error has occurred")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntraPredictionException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public IntraPredictionException(string? message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntraPredictionException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public IntraPredictionException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
