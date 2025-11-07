namespace ContentDotNet.Api.Exceptions
{
    /// <summary>
    ///   Represents an exception that is invoked when one is sure that the current code will never execute.
    /// </summary>
    public class UnreachableException : Exception
    {
        /// <summary>
        ///   Singleton instance of <see cref="UnreachableException"/>.
        /// </summary>
        public static readonly UnreachableException Instance = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="UnreachableException"/> class.
        /// </summary>
        public UnreachableException()
            : this("This code should be unreachable.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnreachableException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public UnreachableException(string? message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnreachableException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public UnreachableException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
