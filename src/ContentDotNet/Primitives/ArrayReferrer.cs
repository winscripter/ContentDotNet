namespace ContentDotNet.Primitives;

/// <summary>
/// Specifies number of preceding elements in the bitstream, to form an array.
/// </summary>
public readonly struct ArrayReferrer
{
    /// <summary>
    /// Number of elements in the array.
    /// </summary>
    public readonly int Elements;

    public ArrayReferrer(int elements)
    {
        Elements = elements;
    }
}
