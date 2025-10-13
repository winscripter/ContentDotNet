namespace ContentDotNet.Protocols.Sdp
{
    /// <summary>
    ///   General SDP exception.
    /// </summary>
    public sealed class SdpException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SdpException"/> class.
        /// </summary>
        public SdpException()
            : this("An error occurred while parsing SDP")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SdpException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public SdpException(string? message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SdpException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public SdpException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
