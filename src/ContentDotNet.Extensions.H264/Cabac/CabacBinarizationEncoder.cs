namespace ContentDotNet.Extensions.H264.Cabac;

/// <summary>
///   Binarization encoder
/// </summary>
public static class CabacBinarizationEncoder
{
    private static readonly Dictionary<int, BitString> s_mbTypeInISlices = new()
    {
        [0] = new BitString(0b0, 1),
        [1] = new BitString(0b000000, 6),
        [2] = new BitString(0b000001, 6),
        [3] = new BitString(0b000010, 6),
        [4] = new BitString(0b000011, 6),
        [5] = new BitString(0b0001000, 7),
        [6] = new BitString(0b0001001, 7),
        [7] = new BitString(0b0001010, 7),
        [8] = new BitString(0b0001011, 7),
        [9] = new BitString(0b0001100, 7),
        [10] = new BitString(0b0001101, 7),
        [11] = new BitString(0b0001110, 7),
        [12] = new BitString(0b0001111, 7),
        [13] = new BitString(0b001000, 6),
        [14] = new BitString(0b001001, 6),
        [15] = new BitString(0b001010, 6),
        [16] = new BitString(0b001011, 6),
        [17] = new BitString(0b0011000, 7),
        [18] = new BitString(0b0011001, 7),
        [19] = new BitString(0b0011010, 7),
        [20] = new BitString(0b0011011, 7),
        [21] = new BitString(0b0011100, 7),
        [22] = new BitString(0b0011101, 7),
        [23] = new BitString(0b0011110, 7),
        [24] = new BitString(0b0011111, 7),
        [25] = new BitString(0b11, 2),
    };

    private static readonly Dictionary<int, BitString> s_mbTypeInPSPSlices = new()
    {
        [0] = new BitString(0b000, 3),
        [1] = new BitString(0b011, 3),
        [2] = new BitString(0b010, 3),
        [3] = new BitString(0b001, 3),
        [5] = new BitString(0b1, 1),
        [6] = new BitString(0b1, 1),
        [7] = new BitString(0b1, 1),
        [8] = new BitString(0b1, 1),
        [9] = new BitString(0b1, 1),
        [10] = new BitString(0b1, 1),
        [11] = new BitString(0b1, 1),
        [12] = new BitString(0b1, 1),
        [13] = new BitString(0b1, 1),
        [14] = new BitString(0b1, 1),
        [15] = new BitString(0b1, 1),
        [16] = new BitString(0b1, 1),
        [17] = new BitString(0b1, 1),
        [18] = new BitString(0b1, 1),
        [19] = new BitString(0b1, 1),
        [20] = new BitString(0b1, 1),
        [21] = new BitString(0b1, 1),
        [22] = new BitString(0b1, 1),
        [23] = new BitString(0b1, 1),
        [24] = new BitString(0b1, 1),
        [25] = new BitString(0b1, 1),
        [26] = new BitString(0b1, 1),
        [27] = new BitString(0b1, 1),
        [28] = new BitString(0b1, 1),
        [29] = new BitString(0b1, 1),
        [30] = new BitString(0b1, 1),
    };

    private static readonly Dictionary<int, BitString> s_mbTypeInBSlices = new()
    {
        [0] = new BitString(0b0, 1),
        [1] = new BitString(0b100, 3),
        [2] = new BitString(0b101, 3),
        [3] = new BitString(0b110000, 6),
        [4] = new BitString(0b110001, 6),
        [5] = new BitString(0b110010, 6),
        [6] = new BitString(0b110011, 6),
        [7] = new BitString(0b110100, 6),
        [8] = new BitString(0b110101, 6),
        [9] = new BitString(0b110110, 6),
        [10] = new BitString(0b110111, 6),
        [11] = new BitString(0b111110, 6),
        [12] = new BitString(0b1110000, 7),
        [13] = new BitString(0b1110001, 7),
        [14] = new BitString(0b1110010, 7),
        [15] = new BitString(0b1110011, 7),
        [16] = new BitString(0b1110100, 7),
        [17] = new BitString(0b1110101, 7),
        [18] = new BitString(0b1110110, 7),
        [19] = new BitString(0b1110111, 7),
        [20] = new BitString(0b1111000, 7),
        [21] = new BitString(0b1111001, 7),
        [22] = new BitString(0b111111, 6),
        [23] = new BitString(0b111101, 6),
        [24] = new BitString(0b111101, 6),
        [25] = new BitString(0b111101, 6),
        [26] = new BitString(0b111101, 6),
        [27] = new BitString(0b111101, 6),
        [28] = new BitString(0b111101, 6),
        [29] = new BitString(0b111101, 6),
        [30] = new BitString(0b111101, 6),
        [31] = new BitString(0b111101, 6),
        [32] = new BitString(0b111101, 6),
        [33] = new BitString(0b111101, 6),
        [34] = new BitString(0b111101, 6),
        [35] = new BitString(0b111101, 6),
        [36] = new BitString(0b111101, 6),
        [37] = new BitString(0b111101, 6),
        [38] = new BitString(0b111101, 6),
        [39] = new BitString(0b111101, 6),
        [40] = new BitString(0b111101, 6),
        [41] = new BitString(0b111101, 6),
        [42] = new BitString(0b111101, 6),
        [43] = new BitString(0b111101, 6),
        [44] = new BitString(0b111101, 6),
        [45] = new BitString(0b111101, 6),
        [46] = new BitString(0b111101, 6),
        [47] = new BitString(0b111101, 6),
        [48] = new BitString(0b111101, 6),
    };

    private static readonly Dictionary<int, BitString> s_subMbTypeInPSPSlices = new()
    {
        [0] = new BitString(0b1, 1),
        [1] = new BitString(0b00, 2),
        [2] = new BitString(0b011, 3),
        [3] = new BitString(0b010, 3),
    };

    private static readonly Dictionary<int, BitString> s_subMbTypeInBSlices = new()
    {
        [0] = new BitString(0b0, 1),
        [1] = new BitString(0b100, 3),
        [2] = new BitString(0b101, 3),
        [3] = new BitString(0b11000, 5),
        [4] = new BitString(0b11001, 5),
        [5] = new BitString(0b11010, 5),
        [6] = new BitString(0b11011, 5),
        [7] = new BitString(0b111000, 6),
        [8] = new BitString(0b111001, 6),
        [9] = new BitString(0b111010, 6),
        [10] = new BitString(0b111011, 6),
        [11] = new BitString(0b11110, 5),
        [12] = new BitString(0b11111, 5),
    };

    /// <summary>
    ///   Encodes a U-binarized value.
    /// </summary>
    /// <param name="encoder"></param>
    /// <param name="symbols"></param>
    /// <param name="value"></param>
    public static void EncodeUnary(ArithmeticEncoder encoder, ref CabacContext symbols, int value)
    {
        for (int i = 0; i < value; i++)
            encoder.WriteBin(ref symbols, true);
        encoder.WriteBin(ref symbols, false);
    }

    internal static void WriteBitString(ArithmeticEncoder encoder, ref CabacContext symbols, BitString bs)
    {
        for (int i = 0; i < bs.Length; i++)
            encoder.WriteBin(ref symbols, bs[i]);
    }

    /// <summary>
    ///   Encodes <c>mb_type</c>.
    /// </summary>
    /// <param name="encoder"></param>
    /// <param name="symbols"></param>
    /// <param name="value"></param>
    /// <param name="sliceType"></param>
    public static void EncodeMbType(ArithmeticEncoder encoder, ref CabacContext symbols, int value, GeneralSliceType sliceType)
    {
        switch (sliceType)
        {
            case GeneralSliceType.I:
                WriteBitString(encoder, ref symbols, s_mbTypeInISlices[value]);
                break;

            case GeneralSliceType.P:
            case GeneralSliceType.SP:
                WriteBitString(encoder, ref symbols, s_mbTypeInPSPSlices[value]);
                break;

            case GeneralSliceType.B:
                WriteBitString(encoder, ref symbols, s_mbTypeInBSlices[value]);
                break;
        }
    }

    /// <summary>
    ///   Encodes <c>sub_mb_type</c>.
    /// </summary>
    /// <param name="encoder"></param>
    /// <param name="symbols"></param>
    /// <param name="value"></param>
    /// <param name="sliceType"></param>
    public static void EncodeSubMbType(ArithmeticEncoder encoder, ref CabacContext symbols, int value, GeneralSliceType sliceType)
    {
        switch (sliceType)
        {
            case GeneralSliceType.P:
            case GeneralSliceType.SP:
                WriteBitString(encoder, ref symbols, s_subMbTypeInPSPSlices[value]);
                break;

            case GeneralSliceType.B:
                WriteBitString(encoder, ref symbols, s_subMbTypeInBSlices[value]);
                break;
        }
    }

    /// <summary>
    ///   Encodes a TU-binarized value.
    /// </summary>
    /// <param name="encoder"></param>
    /// <param name="symbols"></param>
    /// <param name="value"></param>
    /// <param name="cMax"></param>
    public static void EncodeTruncatedUnary(ArithmeticEncoder encoder, ref CabacContext symbols, int value, int cMax)
    {
        for (int i = 0; i < cMax; i++)
        {
            if ((value & (1 << i)) == 0)
            {
                encoder.WriteBin(ref symbols, false);
                break;
            }
            else
            {
                encoder.WriteBin(ref symbols, true);
            }
        }
    }
}
