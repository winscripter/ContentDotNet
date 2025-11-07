namespace ContentDotNet.Api.Collections.Generic;

using System.Collections;

/// <summary>
///   A list with a maximum item limit.
/// </summary>
public sealed class LimitedList<T> : IList<T>
{
    private readonly List<T> _items;
    private readonly int _maxCount;

    public LimitedList(int maxCount)
    {
        if (maxCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxCount), "Maximum count must be greater than zero.");
        _maxCount = maxCount;
        _items = new List<T>(maxCount);
    }

    public int MaxCount => _maxCount;

    public T this[int index]
    {
        get => _items[index];
        set => _items[index] = value;
    }

    public int Count => _items.Count;

    public bool IsReadOnly => false;

    public void Add(T item)
    {
        if (_items.Count >= _maxCount)
            throw new InvalidOperationException($"Cannot add more than {_maxCount} items to this list.");
        _items.Add(item);
    }

    public void Clear()
    {
        _items.Clear();
    }

    public bool Contains(T item)
    {
        return _items.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        _items.CopyTo(array, arrayIndex);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    public int IndexOf(T item)
    {
        return _items.IndexOf(item);
    }

    public void Insert(int index, T item)
    {
        if (_items.Count >= _maxCount)
            throw new InvalidOperationException($"Cannot add more than {_maxCount} items to this list.");
        _items.Insert(index, item);
    }

    public bool Remove(T item)
    {
        return _items.Remove(item);
    }

    public void RemoveAt(int index)
    {
        _items.RemoveAt(index);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _items.GetEnumerator();
    }
}
