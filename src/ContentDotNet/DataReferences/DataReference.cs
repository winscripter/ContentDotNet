namespace ContentDotNet.DataReferences;

/// <summary>
///   Abstracts the data reference.
/// </summary>
public abstract class DataReference
{
    /// <summary>
    ///   Is the string a variable length string?
    /// </summary>
    public abstract bool IsVariableLength { get; }

    /// <summary>
    ///   Is the string a null terminated string?
    /// </summary>
    public abstract bool IsNullTerminated { get; }

    /// <summary>
    ///   Returns the string value of the data reference.
    /// </summary>
    /// <returns>The data reference.</returns>
    public abstract string GetText();

    /// <summary>
    ///   Identifier of the data reference kind.
    /// </summary>
    public abstract string TypeId { get; }

    /// <summary>
    ///   Checks if the <see cref="TypeId"/> property is equal to <c>url</c> or <c>urn</c>.
    /// </summary>
    public bool IsUrnOrUrl => TypeId == "urn" || TypeId == "url";
}
