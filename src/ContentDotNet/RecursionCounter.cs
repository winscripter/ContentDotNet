namespace ContentDotNet;

/// <summary>
///   An exception thrown when an infinite loop is detected, which could cause permanent deadlock,
///   or, worse case, memory leak which could cause the user's system to crash or hang.
/// </summary>
public sealed class InfiniteLoopException : Exception
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="InfiniteLoopException"/> class.
    /// </summary>
    public InfiniteLoopException()
        : this("An infinite loop has been detected")
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="InfiniteLoopException"/> class with the specified
    ///   exception message.
    /// </summary>
    /// <param name="message">Exception message</param>
    public InfiniteLoopException(string? message) : base(message)
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="InfiniteLoopException"/> class with the specified
    ///   exception message and inner exception.
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="innerException">Inner exceptions</param>
    public InfiniteLoopException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}

/// <summary>
///   A recursion counter is a solution to infinite loops.
/// </summary>
public struct RecursionCounter : IEquatable<RecursionCounter>
{
    private int _counter;
    private readonly int _limit;

    /// <summary>
    ///   Initializes a new instance of the <see cref="RecursionCounter"/> struct with a specified counter and limit.
    /// </summary>
    /// <param name="counter">The initial value of the counter.</param>
    /// <param name="limit">The maximum allowed value for the counter before an infinite loop is detected.</param>
    public RecursionCounter(int counter, int limit)
    {
        _counter = counter;
        _limit = limit;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="RecursionCounter"/> struct with a specified limit.
    ///   The counter is initialized to 0.
    /// </summary>
    /// <param name="limit">The maximum allowed value for the counter before an infinite loop is detected.</param>
    public RecursionCounter(int limit)
        : this(0, limit)
    {
    }

    /// <summary>
    ///   Gets or sets the current value of the counter.
    /// </summary>
    public int Counter
    {
        readonly get
        {
            return _counter;
        }

        set
        {
            _counter = value;
        }
    }

    /// <summary>
    ///   Gets the maximum allowed value for the counter before an infinite loop is detected.
    /// </summary>
    public readonly int Limit
    {
        get => _limit;
    }

    /// <summary>
    ///   Tries to increment once. If that reaches the limit, an <see cref="InfiniteLoopException"/> is thrown.
    /// </summary>
    /// <exception cref="InfiniteLoopException"></exception>
    public void Increment()
    {
        if (_counter >= _limit)
            throw new InfiniteLoopException();

        _counter++;
    }

    /// <summary>
    ///   Decrements once.
    /// </summary>
    public void Decrement()
    {
        if (_counter > 0)
            _counter--;
    }

    /// <summary>
    ///   Determines whether the specified object is equal to the current <see cref="RecursionCounter"/>.
    /// </summary>
    /// <param name="obj">The object to compare with the current <see cref="RecursionCounter"/>.</param>
    /// <returns>
    ///   <c>true</c> if the specified object is equal to the current <see cref="RecursionCounter"/>; otherwise, <c>false</c>.
    /// </returns>
    public readonly override bool Equals(object? obj)
    {
        return obj is RecursionCounter counter && Equals(counter);
    }

    /// <summary>
    ///   Determines whether the specified <see cref="RecursionCounter"/> is equal to the current <see cref="RecursionCounter"/>.
    /// </summary>
    /// <param name="other">The <see cref="RecursionCounter"/> to compare with the current <see cref="RecursionCounter"/>.</param>
    /// <returns>
    ///   <c>true</c> if the specified <see cref="RecursionCounter"/> is equal to the current <see cref="RecursionCounter"/>; otherwise, <c>false</c>.
    /// </returns>
    public readonly bool Equals(RecursionCounter other)
    {
        return _counter == other._counter &&
               _limit == other._limit &&
               Counter == other.Counter &&
               Limit == other.Limit;
    }

    /// <summary>
    ///   Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current <see cref="RecursionCounter"/>.</returns>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(_counter, _limit, Counter, Limit);
    }

    /// <summary>
    ///   Determines whether two specified <see cref="RecursionCounter"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="RecursionCounter"/> to compare.</param>
    /// <param name="right">The second <see cref="RecursionCounter"/> to compare.</param>
    /// <returns>
    ///   <c>true</c> if the two <see cref="RecursionCounter"/> instances are equal; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator ==(RecursionCounter left, RecursionCounter right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///   Determines whether two specified <see cref="RecursionCounter"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="RecursionCounter"/> to compare.</param>
    /// <param name="right">The second <see cref="RecursionCounter"/> to compare.</param>
    /// <returns>
    ///   <c>true</c> if the two <see cref="RecursionCounter"/> instances are not equal; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator !=(RecursionCounter left, RecursionCounter right)
    {
        return !(left == right);
    }
}
