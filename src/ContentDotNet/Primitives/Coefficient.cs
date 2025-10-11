namespace ContentDotNet.Primitives;

/// <summary>
///   A primitive structure that defines a binary integer with a length of bits.
/// </summary>
/// <param name="Value">The actual value of the integer.</param>
/// <param name="Length">Number of bits required to represent it. For instance, in 00110, the leading 00 will still account, so this will be 5.</param>
public readonly record struct Coefficient(int Value, int Length);
