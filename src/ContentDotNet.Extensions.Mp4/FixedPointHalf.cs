namespace ContentDotNet.Extensions.Mp4;

/// <summary>
///   A fixed-point integer, with one byte for the integer part and one byte for
///   the remainder part.
/// </summary>
public struct FixedPointHalf : IEquatable<FixedPointHalf>
{
    /// <summary>
    ///   Integer part of the fixed-point number.
    /// </summary>
    public byte IntegerPart { get; set; }

    /// <summary>
    ///   Remainder part of the fixed-point number.
    /// </summary>
    public byte RemainderPart { get; set; }

    /// <summary>
    ///   Initializes a new instance of the <see cref="FixedPointHalf"/> struct.
    /// </summary>
    /// <param name="integerPart">The integer part of the fixed-point number.</param>
    /// <param name="remainderPart">The remainder part of the fixed-point number.</param>
    public FixedPointHalf(byte integerPart, byte remainderPart)
    {
        IntegerPart = integerPart;
        RemainderPart = remainderPart;
    }

    /// <summary>
    ///   Reads a <see cref="FixedPointHalf"/> instance from the specified <see cref="BinaryReader"/>.
    /// </summary>
    /// <param name="reader">The <see cref="BinaryReader"/> to read from.</param>
    /// <returns>A new instance of <see cref="FixedPointHalf"/> read from the binary stream.</returns>
    public static FixedPointHalf Read(BinaryReader reader)
    {
        byte integerPart = reader.ReadByte();
        byte remainderPart = reader.ReadByte();
        return new FixedPointHalf(integerPart, remainderPart);
    }

    /// <summary>
    ///   Writes the current <see cref="FixedPointHalf"/> instance to the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="writer">The <see cref="BinaryWriter"/> to write to.</param>
    public readonly void Write(BinaryWriter writer)
    {
        writer.Write(IntegerPart);
        writer.Write(RemainderPart);
    }

    /// <summary>
    ///   Adds two <see cref="FixedPointHalf"/> instances.
    /// </summary>
    /// <param name="left">The first <see cref="FixedPointHalf"/>.</param>
    /// <param name="right">The second <see cref="FixedPointHalf"/>.</param>
    /// <returns>The result of adding the two instances.</returns>
    public static FixedPointHalf operator +(FixedPointHalf left, FixedPointHalf right)
    {
        int integerPart = left.IntegerPart + right.IntegerPart;
        int remainderPart = left.RemainderPart + right.RemainderPart;

        if (remainderPart >= 256)
        {
            integerPart++;
            remainderPart -= 256;
        }

        return new FixedPointHalf((byte)integerPart, (byte)remainderPart);
    }

    /// <summary>
    ///   Subtracts one <see cref="FixedPointHalf"/> instance from another.
    /// </summary>
    /// <param name="left">The first <see cref="FixedPointHalf"/>.</param>
    /// <param name="right">The second <see cref="FixedPointHalf"/>.</param>
    /// <returns>The result of subtracting the second instance from the first.</returns>
    public static FixedPointHalf operator -(FixedPointHalf left, FixedPointHalf right)
    {
        int integerPart = left.IntegerPart - right.IntegerPart;
        int remainderPart = left.RemainderPart - right.RemainderPart;

        if (remainderPart < 0)
        {
            integerPart--;
            remainderPart += 256;
        }

        return new FixedPointHalf((byte)integerPart, (byte)remainderPart);
    }

    /// <summary>
    ///   Multiplies two <see cref="FixedPointHalf"/> instances.
    /// </summary>
    /// <param name="left">The first <see cref="FixedPointHalf"/>.</param>
    /// <param name="right">The second <see cref="FixedPointHalf"/>.</param>
    /// <returns>The result of multiplying the two instances.</returns>
    public static FixedPointHalf operator *(FixedPointHalf left, FixedPointHalf right)
    {
        int result = (left.IntegerPart * 256 + left.RemainderPart) * (right.IntegerPart * 256 + right.RemainderPart);
        byte integerPart = (byte)(result >> 16);
        byte remainderPart = (byte)((result >> 8) & 0xFF);

        return new FixedPointHalf(integerPart, remainderPart);
    }

    /// <summary>
    ///   Divides one <see cref="FixedPointHalf"/> instance by another.
    /// </summary>
    /// <param name="left">The first <see cref="FixedPointHalf"/>.</param>
    /// <param name="right">The second <see cref="FixedPointHalf"/>.</param>
    /// <returns>The result of dividing the first instance by the second.</returns>
    public static FixedPointHalf operator /(FixedPointHalf left, FixedPointHalf right)
    {
        int dividend = (left.IntegerPart * 256 + left.RemainderPart) << 8;
        int divisor = right.IntegerPart * 256 + right.RemainderPart;

        if (divisor == 0)
            throw new DivideByZeroException();

        int result = dividend / divisor;
        byte integerPart = (byte)(result >> 8);
        byte remainderPart = (byte)(result & 0xFF);

        return new FixedPointHalf(integerPart, remainderPart);
    }

    /// <summary>
    ///   Computes the remainder of dividing one <see cref="FixedPointHalf"/> instance by another.
    /// </summary>
    /// <param name="left">The first <see cref="FixedPointHalf"/>.</param>
    /// <param name="right">The second <see cref="FixedPointHalf"/>.</param>
    /// <returns>The remainder of dividing the first instance by the second.</returns>
    public static FixedPointHalf operator %(FixedPointHalf left, FixedPointHalf right)
    {
        int dividend = left.IntegerPart * 256 + left.RemainderPart;
        int divisor = right.IntegerPart * 256 + right.RemainderPart;

        if (divisor == 0)
            throw new DivideByZeroException();

        int result = dividend % divisor;
        byte integerPart = (byte)(result >> 8);
        byte remainderPart = (byte)(result & 0xFF);

        return new FixedPointHalf(integerPart, remainderPart);
    }

    /// <summary>  
    ///   Converts a <see cref="FixedPointHalf"/> instance to a <see cref="byte"/>.  
    /// </summary>  
    /// <param name="half">The <see cref="FixedPointHalf"/> instance to convert.</param>  
    /// <returns>The integer part of the <see cref="FixedPointHalf"/> as a <see cref="byte"/>.</returns>  
    public static explicit operator byte(FixedPointHalf half) => half.IntegerPart;

    /// <summary>  
    ///   Converts a <see cref="FixedPointHalf"/> instance to a <see cref="sbyte"/>.  
    /// </summary>  
    /// <param name="half">The <see cref="FixedPointHalf"/> instance to convert.</param>  
    /// <returns>The integer part of the <see cref="FixedPointHalf"/> as a <see cref="sbyte"/>.</returns>  
    public static explicit operator sbyte(FixedPointHalf half) => (sbyte)half.IntegerPart;

    /// <summary>  
    ///   Converts a <see cref="FixedPointHalf"/> instance to a <see cref="short"/>.  
    /// </summary>  
    /// <param name="half">The <see cref="FixedPointHalf"/> instance to convert.</param>  
    /// <returns>The integer part of the <see cref="FixedPointHalf"/> as a <see cref="short"/>.</returns>  
    public static explicit operator short(FixedPointHalf half) => half.IntegerPart;

    /// <summary>  
    ///   Converts a <see cref="FixedPointHalf"/> instance to a <see cref="ushort"/>.  
    /// </summary>  
    /// <param name="half">The <see cref="FixedPointHalf"/> instance to convert.</param>  
    /// <returns>The integer part of the <see cref="FixedPointHalf"/> as a <see cref="ushort"/>.</returns>  
    public static explicit operator ushort(FixedPointHalf half) => half.IntegerPart;

    /// <summary>  
    ///   Converts a <see cref="FixedPointHalf"/> instance to an <see cref="int"/>.  
    /// </summary>  
    /// <param name="half">The <see cref="FixedPointHalf"/> instance to convert.</param>  
    /// <returns>The integer part of the <see cref="FixedPointHalf"/> as an <see cref="int"/>.</returns>  
    public static explicit operator int(FixedPointHalf half) => half.IntegerPart;

    /// <summary>  
    ///   Converts a <see cref="FixedPointHalf"/> instance to a <see cref="uint"/>.  
    /// </summary>  
    /// <param name="half">The <see cref="FixedPointHalf"/> instance to convert.</param>  
    /// <returns>The integer part of the <see cref="FixedPointHalf"/> as a <see cref="uint"/>.</returns>  
    public static explicit operator uint(FixedPointHalf half) => half.IntegerPart;

    /// <summary>  
    ///   Converts a <see cref="FixedPointHalf"/> instance to a <see cref="long"/>.  
    /// </summary>  
    /// <param name="half">The <see cref="FixedPointHalf"/> instance to convert.</param>  
    /// <returns>The integer part of the <see cref="FixedPointHalf"/> as a <see cref="long"/>.</returns>  
    public static explicit operator long(FixedPointHalf half) => half.IntegerPart;

    /// <summary>  
    ///   Converts a <see cref="FixedPointHalf"/> instance to a <see cref="ulong"/>.  
    /// </summary>  
    /// <param name="half">The <see cref="FixedPointHalf"/> instance to convert.</param>  
    /// <returns>The integer part of the <see cref="FixedPointHalf"/> as a <see cref="ulong"/>.</returns>  
    public static explicit operator ulong(FixedPointHalf half) => half.IntegerPart;

    /// <summary>
    ///   Determines whether the specified object is equal to the current instance.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns>
    ///   <c>true</c> if the specified object is equal to the current instance; otherwise, <c>false</c>.
    /// </returns>
    public readonly override bool Equals(object? obj)
    {
        return obj is FixedPointHalf half && Equals(half);
    }

    /// <summary>
    ///   Determines whether the specified <see cref="FixedPointHalf"/> is equal to the current instance.
    /// </summary>
    /// <param name="other">The <see cref="FixedPointHalf"/> to compare with the current instance.</param>
    /// <returns>
    ///   <c>true</c> if the specified <see cref="FixedPointHalf"/> is equal to the current instance; otherwise, <c>false</c>.
    /// </returns>
    public readonly bool Equals(FixedPointHalf other)
    {
        return IntegerPart == other.IntegerPart &&
               RemainderPart == other.RemainderPart;
    }

    /// <summary>
    ///   Returns a hash code for the current instance.
    /// </summary>
    /// <returns>A hash code for the current instance.</returns>
    public readonly override int GetHashCode()
    {
        return HashCode.Combine(IntegerPart, RemainderPart);
    }

    /// <summary>
    ///   Determines whether two <see cref="FixedPointHalf"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="FixedPointHalf"/> to compare.</param>
    /// <param name="right">The second <see cref="FixedPointHalf"/> to compare.</param>
    /// <returns>
    ///   <c>true</c> if the two <see cref="FixedPointHalf"/> instances are equal; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator ==(FixedPointHalf left, FixedPointHalf right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///   Determines whether two <see cref="FixedPointHalf"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="FixedPointHalf"/> to compare.</param>
    /// <param name="right">The second <see cref="FixedPointHalf"/> to compare.</param>
    /// <returns>
    ///   <c>true</c> if the two <see cref="FixedPointHalf"/> instances are not equal; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator !=(FixedPointHalf left, FixedPointHalf right)
    {
        return !(left == right);
    }
}
