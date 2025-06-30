namespace ContentDotNet.Extensions.H264;

/// <summary>
///   The Coded Block Patterns.
/// </summary>
public readonly record struct CodedBlockPatterns(int CodedBlockPatternLuma, int CodedBlockPatternChroma)
{
    /// <summary>
    ///   Creates the <see cref="CodedBlockPatterns"/> structure using <c>CodedBlockPattern</c>.
    /// </summary>
    /// <param name="codedBlockPattern">The CBP.</param>
    /// <returns>A <see cref="CodedBlockPatterns"/> structure.</returns>
    public static CodedBlockPatterns From(int codedBlockPattern)
        => new(codedBlockPattern % 16, codedBlockPattern / 16);
}
