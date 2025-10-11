namespace ContentDotNet.Extensions.Video.H264.Exceptions
{
    /// <summary>
    ///   Thrown when the context variable is used but not initialized.
    /// </summary>
    public class UninitializedContextVariableException : H264Exception
    {
        internal static readonly UninitializedContextVariableException Instance = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="UninitializedContextVariableException"/> class.
        /// </summary>
        public UninitializedContextVariableException()
            : this("Context variable at given context index is used but not initialized")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UninitializedContextVariableException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public UninitializedContextVariableException(string? message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UninitializedContextVariableException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public UninitializedContextVariableException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
