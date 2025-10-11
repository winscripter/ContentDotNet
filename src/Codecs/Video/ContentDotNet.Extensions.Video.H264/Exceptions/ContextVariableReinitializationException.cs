namespace ContentDotNet.Extensions.Video.H264.Exceptions
{
    /// <summary>
    ///   CABAC exception thrown when the context variable was reinitialized when it shouldn't.
    /// </summary>
    public class ContextVariableReinitializationException : H264Exception
    {
        internal static readonly ContextVariableReinitializationException Instance = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextVariableReinitializationException"/> class.
        /// </summary>
        public ContextVariableReinitializationException()
            : this("Context variable reinitialized when it shouldn't")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextVariableReinitializationException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ContextVariableReinitializationException(string? message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextVariableReinitializationException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public ContextVariableReinitializationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
