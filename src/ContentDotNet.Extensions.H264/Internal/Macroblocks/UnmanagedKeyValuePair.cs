namespace ContentDotNet.Extensions.H264.Internal.Macroblocks;

internal struct UnmanagedKeyValuePair<TKey, TValue> : IEquatable<UnmanagedKeyValuePair<TKey, TValue>>
    where TKey : unmanaged
    where TValue : unmanaged
{
    public TKey Key;
    public TValue Value;

    public UnmanagedKeyValuePair(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }

    public readonly override bool Equals(object? obj)
    {
        return obj is UnmanagedKeyValuePair<TKey, TValue> pair && Equals(pair);
    }

    public readonly bool Equals(UnmanagedKeyValuePair<TKey, TValue> other)
    {
        return EqualityComparer<TKey>.Default.Equals(Key, other.Key) &&
               EqualityComparer<TValue>.Default.Equals(Value, other.Value);
    }

    public readonly override int GetHashCode()
    {
        return HashCode.Combine(Key, Value);
    }

    public static bool operator ==(UnmanagedKeyValuePair<TKey, TValue> left, UnmanagedKeyValuePair<TKey, TValue> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(UnmanagedKeyValuePair<TKey, TValue> left, UnmanagedKeyValuePair<TKey, TValue> right)
    {
        return !(left == right);
    }
}
