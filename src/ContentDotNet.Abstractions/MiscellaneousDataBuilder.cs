using System.Collections;
using System.Diagnostics;

namespace ContentDotNet.Abstractions;

/// <summary>  
///   Builds data that might not be necessary, but can still provide  
///   insights to the content file (video, audio, etc).  
/// </summary>  
[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public sealed class MiscellaneousDataBuilder :
   IEquatable<MiscellaneousDataBuilder?>,
   IEnumerable<IMiscellaneousData>
{
    private readonly List<IMiscellaneousData> _miscellaneousDataBacking;

    /// <summary>  
    ///   Initializes a new instance of the <see cref="MiscellaneousDataBuilder"/> class.  
    /// </summary>  
    public MiscellaneousDataBuilder()
    {
        _miscellaneousDataBacking = [];
    }

    /// <summary>  
    ///   Adds a new miscellaneous data item to the builder.  
    /// </summary>  
    /// <param name="miscellaneousData">The miscellaneous data to add.</param>  
    /// <exception cref="InvalidOperationException">Thrown if the data already exists.</exception>  
    public void Add(IMiscellaneousData miscellaneousData)
    {
        if (this.ContainsData(miscellaneousData))
            throw new InvalidOperationException("Data already exists");
        this._miscellaneousDataBacking.Add(miscellaneousData);
    }

    /// <summary>  
    ///   Replaces an existing miscellaneous data item with a new one.  
    /// </summary>  
    /// <param name="with">The new miscellaneous data to replace the existing one.</param>  
    /// <exception cref="InvalidOperationException">Thrown if no matching data exists.</exception>  
    public void Mutate(IMiscellaneousData with)
    {
        if (!this.ContainsData(with))
            throw new InvalidOperationException("No element with category " + with.DisplayName);

        IMiscellaneousData matching = this._miscellaneousDataBacking.First(e => e.DisplayName == with.DisplayName);
        this._miscellaneousDataBacking.Remove(matching);

        this._miscellaneousDataBacking.Add(with);
    }

    /// <summary>  
    ///   Gets the count of miscellaneous data items in the builder.  
    /// </summary>  
    public int Count => this._miscellaneousDataBacking.Count;

    /// <summary>  
    ///   Checks if the builder contains a specific miscellaneous data item.  
    /// </summary>  
    /// <param name="miscellaneousData">The miscellaneous data to check for.</param>  
    /// <returns>True if the data exists; otherwise, false.</returns>  
    public bool ContainsData(IMiscellaneousData miscellaneousData)
    {
        return ContainsCategory(miscellaneousData.DisplayName);
    }

    /// <summary>  
    ///   Checks if the builder contains a miscellaneous data item with a specific category.  
    /// </summary>  
    /// <param name="category">The category to check for.</param>  
    /// <returns>True if a matching category exists; otherwise, false.</returns>  
    public bool ContainsCategory(string? category)
    {
        return this._miscellaneousDataBacking.Any(e => e.DisplayName == category);
    }

    /// <summary>  
    ///   Retrieves a miscellaneous data item by its category.  
    /// </summary>  
    /// <param name="category">The category of the data to retrieve.</param>  
    /// <returns>The matching miscellaneous data item.</returns>  
    /// <exception cref="InvalidOperationException">Thrown if no matching data exists.</exception>  
    public IMiscellaneousData GetData(string category)
    {
        if (!this.ContainsCategory(category))
            throw new InvalidOperationException("No element with category " + category);

        return this._miscellaneousDataBacking.First(e => e.DisplayName == category);
    }

    /// <summary>  
    ///   Retrieves a miscellaneous data item by its category, or null if no match is found.  
    /// </summary>  
    /// <param name="category">The category of the data to retrieve.</param>  
    /// <returns>The matching miscellaneous data item, or null if no match is found.</returns>  
    public IMiscellaneousData? GetDataOrNull(string category)
    {
        return this._miscellaneousDataBacking.FirstOrDefault(e => e.DisplayName == category);
    }

    /// <inheritdoc />  
    public override bool Equals(object? obj)
    {
        return Equals(obj as MiscellaneousDataBuilder);
    }

    /// <summary>  
    ///   Determines whether the specified <see cref="MiscellaneousDataBuilder"/> is equal to the current instance.  
    /// </summary>  
    /// <param name="other">The other builder to compare with.</param>  
    /// <returns>True if the builders are equal; otherwise, false.</returns>  
    public bool Equals(MiscellaneousDataBuilder? other)
    {
        return other is not null &&
               EqualityComparer<List<IMiscellaneousData>>.Default.Equals(_miscellaneousDataBacking, other._miscellaneousDataBacking);
    }

    /// <inheritdoc />  
    public override int GetHashCode()
    {
        return HashCode.Combine(_miscellaneousDataBacking);
    }

    /// <summary>  
    ///   Returns an enumerator that iterates through the miscellaneous data items.  
    /// </summary>  
    /// <returns>An enumerator for the miscellaneous data items.</returns>  
    public IEnumerator<IMiscellaneousData> GetEnumerator()
    {
        return ((IEnumerable<IMiscellaneousData>)_miscellaneousDataBacking).GetEnumerator();
    }

    /// <inheritdoc />  
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_miscellaneousDataBacking).GetEnumerator();
    }

    /// <summary>  
    ///   Returns a string representation of the builder.  
    /// </summary>  
    /// <returns>A string representing the builder.</returns>  
    public override string ToString()
    {
        return $"Items: {Count}";
    }

    /// <summary>  
    ///   Determines whether two <see cref="MiscellaneousDataBuilder"/> instances are equal.  
    /// </summary>  
    /// <param name="left">The first builder to compare.</param>  
    /// <param name="right">The second builder to compare.</param>  
    /// <returns>True if the builders are equal; otherwise, false.</returns>  
    public static bool operator ==(MiscellaneousDataBuilder? left, MiscellaneousDataBuilder? right)
    {
        return EqualityComparer<MiscellaneousDataBuilder>.Default.Equals(left, right);
    }

    /// <summary>  
    ///   Determines whether two <see cref="MiscellaneousDataBuilder"/> instances are not equal.  
    /// </summary>  
    /// <param name="left">The first builder to compare.</param>  
    /// <param name="right">The second builder to compare.</param>  
    /// <returns>True if the builders are not equal; otherwise, false.</returns>  
    public static bool operator !=(MiscellaneousDataBuilder? left, MiscellaneousDataBuilder? right)
    {
        return !(left == right);
    }

    /// <summary>  
    ///   Gets a string representation for debugging purposes.  
    /// </summary>  
    /// <returns>A string representing the builder for debugging.</returns>  
    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
