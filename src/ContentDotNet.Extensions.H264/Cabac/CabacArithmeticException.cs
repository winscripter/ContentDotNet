namespace ContentDotNet.Extensions.H264.Cabac;

/// <summary>
/// Represents errors that occur during CABAC arithmetic operations in H.264 processing.
/// </summary>
public sealed class CabacArithmeticException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CabacArithmeticException"/> class.
    /// </summary>
    public CabacArithmeticException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CabacArithmeticException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public CabacArithmeticException(string? message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CabacArithmeticException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public CabacArithmeticException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
