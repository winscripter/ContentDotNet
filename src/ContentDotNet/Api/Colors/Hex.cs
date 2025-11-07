using System.Runtime.CompilerServices;

namespace ContentDotNet.Api.Colors;

/// <summary>
///   Represents a hexadecimal color.
/// </summary>
public struct Hex : IEquatable<Hex>
{
    /// <summary>
    ///   Represents the R channel.
    /// </summary>
    public byte R;

    /// <summary>
    ///   Represents the G channel.
    /// </summary>
    public byte G;

    /// <summary>
    ///   Represents the B channel.
    /// </summary>
    public byte B;

    /// <summary>
    ///   Represents the A channel.
    /// </summary>
    public byte A;

    /// <summary>
    ///   Is the <see cref="A"/> channel present?
    /// </summary>
    public bool AlphaChannelPresent;

    /// <summary>
    ///   Does it use one letter per channel or two?
    /// </summary>
    public bool UsesOneLetterPerChannel;

    /// <summary>
    ///   Initializes a new instance of the <see cref="Hex"/> structure.
    /// </summary>
    /// <param name="r">R</param>
    /// <param name="g">G</param>
    /// <param name="b">B</param>
    /// <param name="a">A</param>
    /// <param name="alphaChannelPresent">Is the alpha channel present?</param>
    /// <param name="usesOneLetterPerChannel">Is there just one letter per channel?</param>
    public Hex(byte r, byte g, byte b, byte a, bool alphaChannelPresent, bool usesOneLetterPerChannel)
    {
        R = r;
        G = g;
        B = b;
        A = a;
        AlphaChannelPresent = alphaChannelPresent;
        UsesOneLetterPerChannel = usesOneLetterPerChannel;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="Hex"/> structure.
    /// </summary>
    /// <param name="r">R</param>
    /// <param name="g">G</param>
    /// <param name="b">B</param>
    /// <param name="alphaChannelPresent">Is the alpha channel present?</param>
    /// <param name="usesOneLetterPerChannel">Is there just one letter per channel?</param>
    public Hex(byte r, byte g, byte b, bool alphaChannelPresent, bool usesOneLetterPerChannel)
        : this(r, g, b, 255, alphaChannelPresent, usesOneLetterPerChannel)
    {
    }

    /// <summary>
    ///   Attempts parsing the hexadecimal color from a string.
    /// </summary>
    /// <param name="source">String where it's parsed.</param>
    /// <param name="result">Result, or <see langword="default"/> if the return value is <see langword="false"/>.</param>
    /// <returns>A boolean indicating whether parsing was successful.</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static bool TryParse(ReadOnlySpan<char> source, out Hex result)
    {
        // Lengths can be:
        //   #RGB - 4
        //   RGB - 3
        //   #RGBA - 5
        //   RGBA - 4
        //   #RRGGBB - 7
        //   RRGGBB - 6
        //   #RRGGBBAA - 9
        //   RRGGBBAA - 8

        if (source.Length < 3 || source.Length > 9)
        {
            result = default;
            return false;
        }

        switch (source.Length)
        {
            case 3:
                // RGB, can never have a hashtag
                // So just parse it right away.
                result = new Hex(
                    HexToByte(source[0]),
                    HexToByte(source[1]),
                    HexToByte(source[2]),
                    false,
                    false
                );
                return true;

            case 4:
                if (source[0] == '#')
                {
                    // #RGB
                    result = new Hex(
                        HexToByte(source[1]),
                        HexToByte(source[2]),
                        HexToByte(source[3]),
                        false,
                        true
                    );
                    return true;
                }
                else
                {
                    // RGBA
                    result = new Hex(
                        HexToByte(source[0]),
                        HexToByte(source[1]),
                        HexToByte(source[2]),
                        HexToByte(source[3]),
                        true,
                        true
                    );
                    return true;
                }

            case 5:
                // Just #RGBA
                result = new Hex(
                    HexToByte(source[1]),
                    HexToByte(source[2]),
                    HexToByte(source[3]),
                    HexToByte(source[4]),
                    true,
                    true
                );
                return true;

            case 6:
                // RRGGBB
                // No hashtag; there's two letters per channel
                result = new Hex(
                    HexToByte2(source[0], source[1]),
                    HexToByte2(source[2], source[3]),
                    HexToByte2(source[4], source[5]),
                    false,
                    false
                );
                return true;

            case 7:
                // #RRGGBB
                result = new Hex(
                    HexToByte2(source[1], source[2]),
                    HexToByte2(source[3], source[4]),
                    HexToByte2(source[5], source[6]),
                    false,
                    false
                );
                return true;

            case 8:
                // RRGGBBAA
                // No hashtag; there's two letters per channel
                result = new Hex(
                    HexToByte2(source[0], source[1]),
                    HexToByte2(source[2], source[3]),
                    HexToByte2(source[4], source[5]),
                    HexToByte2(source[6], source[7]),
                    true,
                    false
                );
                return true;

            case 9:
                // #RRGGBBAA
                result = new Hex(
                    HexToByte2(source[1], source[2]),
                    HexToByte2(source[3], source[4]),
                    HexToByte2(source[5], source[6]),
                    HexToByte2(source[7], source[8]),
                    true,
                    false
                );
                return true;
        }

        // Note: We will never reach here, but the compiler
        //       still errors out.

        result = default;
        return false;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static byte HexToByte(char c)
        {
            if ((int)c is >= 0 and <= 9)
                return (byte)(c - '0');
            else if ((int)c is >= 'a' and <= 'f')
                return (byte)(c - 'a' + 10);
            else if ((int)c is >= 'A' and <= 'F')
                return (byte)(c - 'A' + 10);
            else
                throw new ArgumentOutOfRangeException(nameof(c), "Invalid hex character");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static byte HexToByte2(char a, char b) => (byte)((HexToByte(a) << 8) | HexToByte(b));
    }

    /// <summary>
    ///   Forces parsing the input string.
    /// </summary>
    /// <param name="input">String to parse.</param>
    /// <returns>The parsed representation of <paramref name="input"/>.</returns>
    /// <exception cref="FormatException"></exception>
    public static Hex Parse(ReadOnlySpan<char> input)
    {
        if (!TryParse(input, out var result))
            throw new FormatException("Invalid hex format");
        return result;
    }

    /// <inheritdoc/>
    public readonly override bool Equals(object? obj)
    {
        return obj is Hex hex && Equals(hex);
    }

    /// <summary>
    /// Determines whether the specified <see cref="Hex"/> is equal to the current <see cref="Hex"/>.
    /// </summary>
    /// <param name="other">The <see cref="Hex"/> to compare with the current <see cref="Hex"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="Hex"/> is equal to the current <see cref="Hex"/>; otherwise, <c>false</c>.</returns>
    public readonly bool Equals(Hex other)
    {
        return R == other.R &&
               G == other.G &&
               B == other.B &&
               A == other.A &&
               AlphaChannelPresent == other.AlphaChannelPresent &&
               UsesOneLetterPerChannel == other.UsesOneLetterPerChannel;
    }

    /// <inheritdoc/>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(R, G, B, A, AlphaChannelPresent, UsesOneLetterPerChannel);
    }

    /// <summary>
    ///   Determines whether two <see cref="Hex"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Hex"/> to compare.</param>
    /// <param name="right">The second <see cref="Hex"/> to compare.</param>
    /// <returns><c>true</c> if the two <see cref="Hex"/> instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(Hex left, Hex right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///   Determines whether two <see cref="Hex"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Hex"/> to compare.</param>
    /// <param name="right">The second <see cref="Hex"/> to compare.</param>
    /// <returns><c>true</c> if the two <see cref="Hex"/> instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(Hex left, Hex right)
    {
        return !(left == right);
    }
}
