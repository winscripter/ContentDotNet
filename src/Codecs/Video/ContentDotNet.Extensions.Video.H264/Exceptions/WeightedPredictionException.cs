namespace ContentDotNet.Extensions.Video.H264.Exceptions
{
    /// <summary>
    ///   Represents an exception that occurs during weighted prediction.
    /// </summary>
    public class WeightedPredictionException : H264Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeightedPredictionException"/> class.
        /// </summary>
        public WeightedPredictionException()
            : this("An exception occurred during weighted prediction")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeightedPredictionException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public WeightedPredictionException(string? message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeightedPredictionException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public WeightedPredictionException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
