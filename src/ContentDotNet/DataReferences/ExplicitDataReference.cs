namespace ContentDotNet.DataReferences;

/// <summary>
///   A data reference where the <see cref="DataReference.IsNullTerminated"/>
///   and <see cref="DataReference.IsVariableLength"/> properties are explicitly
///   provided.
/// </summary>
public abstract class ExplicitDataReference : DataReference
{
    /// <inheritdoc cref="DataReference.IsVariableLength" />
    public override bool IsVariableLength { get; }

    /// <inheritdoc cref="DataReference.IsNullTerminated" />
    public override bool IsNullTerminated { get; }

    /// <summary>
    ///   Initializes a new instance of the <see cref="ExplicitDataReference"/> class.
    /// </summary>
    /// <param name="isNullTerminated">Is the data reference null terminated?</param>
    /// <param name="isVariableLength">Is the data reference variable length?</param>
    protected ExplicitDataReference(bool isNullTerminated, bool isVariableLength)
    {
        IsNullTerminated = isNullTerminated;
        IsVariableLength = isVariableLength;
    }
}
