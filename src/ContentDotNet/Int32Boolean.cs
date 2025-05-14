namespace ContentDotNet;

/// <summary>
/// Utilities to turn <see cref="bool"/> into <see cref="int"/>/<see cref="uint"/> and vice versa.
/// </summary>
public static class Int32Boolean
{
    /// <summary>
    /// Converts a boolean value to an integer.
    /// </summary>
    /// <param name="b">The boolean value to convert.</param>
    /// <returns>1 if <paramref name="b"/> is true; otherwise, 0.</returns>
    public static int I32(bool b) => b ? 1 : 0;

    /// <summary>
    /// Converts a boolean value to an unsigned integer.
    /// </summary>
    /// <param name="b">The boolean value to convert.</param>
    /// <returns>1 if <paramref name="b"/> is true; otherwise, 0.</returns>
    public static uint U32(bool b) => b ? 1u : 0;

    /// <summary>
    /// Converts an integer to a boolean value.
    /// </summary>
    /// <param name="i">The integer value to convert.</param>
    /// <returns>true if <paramref name="i"/> is 0; otherwise, false.</returns>
    public static bool B(int i) => i == 0;

    /// <summary>
    /// Converts an unsigned integer to a boolean value.
    /// </summary>
    /// <param name="i">The unsigned integer value to convert.</param>
    /// <returns>true if <paramref name="i"/> is 0; otherwise, false.</returns>
    public static bool B(uint i) => i == 0;
}
