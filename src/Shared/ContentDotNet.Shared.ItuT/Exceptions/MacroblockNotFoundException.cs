namespace ContentDotNet.Shared.ItuT.Exceptions
{
    /// <summary>
    ///   Represents an exception thrown when an ITU-T H.26X series macroblock wasn't found or is out of range of
    ///   yet discovered macroblocks.
    /// </summary>
    public class MacroblockNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MacroblockNotFoundException"/> class.
        /// </summary>
        public MacroblockNotFoundException()
            : this("The H.26X macroblock was not found or is out of range")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MacroblockNotFoundException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MacroblockNotFoundException(string? message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MacroblockNotFoundException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public MacroblockNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
