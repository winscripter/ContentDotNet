namespace ContentDotNet.Extensions.Video.H264.Exceptions
{
    /// <summary>
    ///   Base H.264 exception.
    /// </summary>
    public class H264Exception : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="H264Exception"/> class.
        /// </summary>
        public H264Exception()
            : this("An exception occurred during the processing of the H.264 codec data")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="H264Exception"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public H264Exception(string? message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="H264Exception"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public H264Exception(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
