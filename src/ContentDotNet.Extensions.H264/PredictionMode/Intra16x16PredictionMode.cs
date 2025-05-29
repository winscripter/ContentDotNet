namespace ContentDotNet.Extensions.H264.PredictionMode;

/// <summary>
///   Specifies the Intra 16x16 prediction modes.
/// </summary>
public readonly struct Intra16x16PredictionMode : IEquatable<Intra16x16PredictionMode>,
#if NET7_0_OR_GREATER
    IParsable<Intra16x16PredictionMode>,
    ISpanParsable<Intra16x16PredictionMode>
#endif
{
    private readonly int _macroblockType;
    private readonly int _cbp;
    private readonly int _residualPresence;

    /// <summary>
    ///   Initializes a new instance of the <see cref="Intra16x16PredictionMode"/> structure.
    /// </summary>
    /// <param name="macroblockType">Type of the macroblock.</param>
    /// <param name="cbp">Coded Block Pattern</param>
    /// <param name="residualPresence">Presence of the residual</param>
    public Intra16x16PredictionMode(int macroblockType, int cbp, int residualPresence)
    {
        _macroblockType = macroblockType;
        _cbp = cbp;
        _residualPresence = residualPresence;
    }

    /// <summary>
    ///   Defines the first part - the type of the macroblock.
    /// </summary>
    public int MacroblockType => _macroblockType;

    /// <summary>
    ///   Defines the middle part - the Coded Block Pattern (CBP).
    /// </summary>
    public int CodedBlockPattern => _cbp;

    /// <summary>
    ///   Defines the last part - the presence of the residual data.
    /// </summary>
    public int ResidualPresence => _residualPresence;

    /// <summary>
    ///   Parses a string representation of an <see cref="Intra16x16PredictionMode"/> using the specified format provider.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <returns>An <see cref="Intra16x16PredictionMode"/> parsed from the string.</returns>
    /// <exception cref="FormatException">Thrown when the string is not in a valid format.</exception>
    public static Intra16x16PredictionMode Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
    {
        if (!TryParse(s, provider, out var result))
            throw new FormatException();
        return result;
    }

    /// <summary>
    ///   Tries to parse a string representation of an <see cref="Intra16x16PredictionMode"/> using the specified format provider.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="result">When this method returns, contains the parsed <see cref="Intra16x16PredictionMode"/>, or the default value if parsing failed.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out Intra16x16PredictionMode result)
    {
        // At least n_n_n where n is a number is required
        if (s.Length < 5)
        {
            result = default;
            return false;
        }

        int underscoreCount = 0;
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '_')
            {
                underscoreCount++;
            }
        }

        // Must have two underscores to represent 'n_n_n' where n is a number
        if (underscoreCount < 2)
        {
            result = default;
            return false;
        }

        var firstSlice = s[..5]; // Intra
        if (firstSlice[0] == 'I' && firstSlice[1] == 'n' && firstSlice[2] == 't' && firstSlice[3] == 'r' && firstSlice[4] == 'a')
        {
            s = s[5..]; // Skip "Intra"
        }
        else if (firstSlice[0] == 'I' && firstSlice[1] == '_')
        {
            s = s[2..]; // Skip "I_"
        }

        int n0 = 0;
        int n1 = 0;
        int n2; // Assigned after exiting the foreach loop
        int target = 0; // n0
        int buffer = 0;
        int n0CharCount = 0;
        int n1CharCount = 0;
        int n2CharCount = 0;

        foreach (char c in s)
        {
            if (c == '_')
            {
                if (target == 0)
                    n0 = buffer;
                else if (target == 1)
                    n1 = buffer;
                target++;
                buffer = 0;
            }
            else
            {
                buffer *= 10;
                buffer += c - '0';
                if (target == 0)
                    n0CharCount++;
                else if (target == 1)
                    n1CharCount++;
                else if (target == 2)
                    n2CharCount++;
            }
        }

        // NOTE: We already performed a check to make sure that there are two underscores,
        // so we can confidently say we are at the last part now. Though, we have to ensure that the last
        // character isn't an underscore, otherwise n_n__ is considered valid. Also we need character
        // count variables to ensure that __ or n__n is not considered valid.

        if (target != 2 || n0CharCount == 0 || n1CharCount == 0 || n2CharCount == 0)
        {
            result = default;
            return false;
        }

        n2 = buffer;

        result = new Intra16x16PredictionMode(n0, n1, n2);
        return true;
    }

    /// <summary>
    ///   Is the current <see cref="Intra16x16PredictionMode"/> valid?
    /// </summary>
    public bool IsValid
    {
        get => _macroblockType is >= 0 and <= 3 && _cbp is >= 0 and <= 2 && _residualPresence is >= 0 and <= 1;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is Intra16x16PredictionMode mode && Equals(mode);
    }

    /// <summary>
    /// Determines whether the specified <see cref="Intra16x16PredictionMode"/> is equal to the current instance.
    /// </summary>
    /// <param name="other">The <see cref="Intra16x16PredictionMode"/> to compare with the current instance.</param>
    /// <returns><c>true</c> if the specified <see cref="Intra16x16PredictionMode"/> is equal to the current instance; otherwise, <c>false</c>.</returns>
    public bool Equals(Intra16x16PredictionMode other)
    {
        return _macroblockType == other._macroblockType &&
               _cbp == other._cbp &&
               _residualPresence == other._residualPresence &&
               MacroblockType == other.MacroblockType &&
               CodedBlockPattern == other.CodedBlockPattern &&
               ResidualPresence == other.ResidualPresence;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(_macroblockType, _cbp, _residualPresence, MacroblockType, CodedBlockPattern, ResidualPresence);
    }

    /// <summary>
    ///   Parses a string representation of an <see cref="Intra16x16PredictionMode"/> using the specified format provider.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <returns>An <see cref="Intra16x16PredictionMode"/> parsed from the string.</returns>
    /// <exception cref="FormatException">Thrown when the string is not in a valid format.</exception>
    public static Intra16x16PredictionMode Parse(string s, IFormatProvider? provider)
    {
        return Parse(s.AsSpan(), provider);
    }

    /// <summary>
    ///   Tries to parse a string representation of an <see cref="Intra16x16PredictionMode"/> using the specified format provider.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="result">When this method returns, contains the parsed <see cref="Intra16x16PredictionMode"/>, or the default value if parsing failed.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParse(string? s, IFormatProvider? provider, out Intra16x16PredictionMode result)
    {
        result = default;
        return s is not null && TryParse(s.AsSpan(), provider, out result);
    }

    /// <summary>
    ///   Determines whether two <see cref="Intra16x16PredictionMode"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Intra16x16PredictionMode"/> to compare.</param>
    /// <param name="right">The second <see cref="Intra16x16PredictionMode"/> to compare.</param>
    /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(Intra16x16PredictionMode left, Intra16x16PredictionMode right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///   Determines whether two <see cref="Intra16x16PredictionMode"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Intra16x16PredictionMode"/> to compare.</param>
    /// <param name="right">The second <see cref="Intra16x16PredictionMode"/> to compare.</param>
    /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(Intra16x16PredictionMode left, Intra16x16PredictionMode right)
    {
        return !(left == right);
    }
}
