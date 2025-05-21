namespace ContentDotNet.DataReferences;

/// <summary>
///   A URN data reference.
/// </summary>
public sealed class UrnDataReference : ExplicitDataReference
{
    /// <inheritdoc cref="DataReference.TypeId"/>
    public override string TypeId => "urn";

    private readonly string _text;

    /// <summary>
    ///   Initializes a new instance of the <see cref="UrnDataReference"/> class.
    /// </summary>
    /// <param name="text">The text value of the data reference.</param>
    /// <param name="isNullTerminated">Is the data reference null terminated?</param>
    /// <param name="isVariableLength">Is the data reference variable length?</param>
    public UrnDataReference(string text, bool isNullTerminated, bool isVariableLength)
        : base(isNullTerminated, isVariableLength)
    {
        _text = text;
    }

    public override string GetText() => _text;
}
