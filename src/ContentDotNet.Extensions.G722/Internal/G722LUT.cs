using ContentDotNet.Primitives;

namespace ContentDotNet.Extensions.G722.Internal;

internal static class G722LUT
{
    public static readonly int[] mL = [
        1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
        11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
        21, 22, 23, 24, 25, 26, 27, 28, 29, 30
    ];

    public static readonly decimal[] LL6 = [
        0.00000m, 0.06817m, 0.14103m, 0.21389m, 0.29212m, 0.37035m,
        0.45482m, 0.53929m, 0.63107m, 0.72286m, 0.82335m, 0.92383m,
        1.03485m, 1.14587m, 1.26989m, 1.39391m, 1.53439m, 1.67486m,
        1.83683m, 1.99880m, 2.19006m, 2.38131m, 2.61482m, 2.84833m,
        3.14822m, 3.44811m, 3.86796m, 4.28782m, 4.99498m, 5.70214m
    ];

    public static readonly decimal[] LU6 = [
        0.06817m, 0.14103m, 0.21389m, 0.29212m, 0.37035m, 0.45482m,
        0.53929m, 0.63107m, 0.72286m, 0.82335m, 0.92383m, 1.03485m,
        1.14587m, 1.26989m, 1.39391m, 1.53439m, 1.67486m, 1.83683m,
        1.99880m, 2.19006m, 2.38131m, 2.61482m, 2.84833m, 3.14822m,
        3.44811m, 3.86796m, 4.28782m, 4.99498m, 5.70214m, decimal.MaxValue
    ];

    public static readonly Coefficient[] ILN = [
        new Coefficient(0b111111, 6), new Coefficient(0b111110, 6), new Coefficient(0b011111, 6), new Coefficient(0b011110, 6),
        new Coefficient(0b011101, 6), new Coefficient(0b011100, 6), new Coefficient(0b011011, 6), new Coefficient(0b011010, 6),
        new Coefficient(0b011001, 6), new Coefficient(0b011000, 6), new Coefficient(0b010111, 6), new Coefficient(0b010110, 6),
        new Coefficient(0b010101, 6), new Coefficient(0b010100, 6), new Coefficient(0b010011, 6), new Coefficient(0b010010, 6),
        new Coefficient(0b010001, 6), new Coefficient(0b010000, 6), new Coefficient(0b001111, 6), new Coefficient(0b001110, 6),
        new Coefficient(0b001101, 6), new Coefficient(0b001100, 6), new Coefficient(0b001011, 6), new Coefficient(0b001010, 6),
        new Coefficient(0b001001, 6), new Coefficient(0b001000, 6), new Coefficient(0b000111, 6), new Coefficient(0b000110, 6),
        new Coefficient(0b000101, 6), new Coefficient(0b000100, 6)
    ];

    public static readonly Coefficient[] ILP = [
        new Coefficient(0b111101, 6), new Coefficient(0b111100, 6), new Coefficient(0b111011, 6), new Coefficient(0b111010, 6),
        new Coefficient(0b111001, 6), new Coefficient(0b111000, 6), new Coefficient(0b110111, 6), new Coefficient(0b110110, 6),
        new Coefficient(0b110101, 6), new Coefficient(0b110100, 6), new Coefficient(0b110011, 6), new Coefficient(0b110010, 6),
        new Coefficient(0b110001, 6), new Coefficient(0b110000, 6), new Coefficient(0b101111, 6), new Coefficient(0b101110, 6),
        new Coefficient(0b101101, 6), new Coefficient(0b101100, 6), new Coefficient(0b101011, 6), new Coefficient(0b101010, 6),
        new Coefficient(0b101001, 6), new Coefficient(0b101000, 6), new Coefficient(0b100111, 6), new Coefficient(0b100110, 6),
        new Coefficient(0b100101, 6), new Coefficient(0b100100, 6), new Coefficient(0b100011, 6), new Coefficient(0b100010, 6),
        new Coefficient(0b100001, 6), new Coefficient(0b100000, 6)
    ];

    public static readonly int[] mH = [1, 2];
    public static readonly decimal[] HL = [0m, 1.10156m];
    public static readonly decimal[] HH = [1.10156m, decimal.MaxValue];
    public static readonly Coefficient[] IHN = [new Coefficient(0b01, 2), new Coefficient(0b00, 2)];
    public static readonly Coefficient[] IHP = [new Coefficient(0b11, 2), new Coefficient(0b10, 2)];

    public static readonly decimal[] QL6Inv = [
        0.03409m, 0.10460m, 0.17746m, 0.25300m, 0.33124m, 0.41259m,
        0.49706m, 0.58518m, 0.67697m, 0.77310m, 0.87359m, 0.97934m,
        1.09036m, 1.20788m, 1.33191m, 1.46415m, 1.60462m, 1.75585m,
        1.91782m, 2.09443m, 2.28568m, 2.49806m, 2.73157m, 2.99827m,
        3.29816m, 3.65804m, 4.07789m, 4.64140m, 5.34856m, 6.05572m
    ];

    public static readonly decimal[] QL5Inv = [
        0.06817m, 0.21389m, 0.37035m, 0.53929m, 0.72286m, 0.92383m,
        1.14587m, 1.39391m, 1.67486m, 1.99880m, 2.38131m, 2.84833m,
        3.44811m, 4.28782m, 5.70214m
    ];

    public static readonly decimal[] QL4Inv = [
        0.00000m, 0.29212m, 0.63107m, 1.03485m, 1.53439m, 2.19006m,
        3.14822m, 4.99498m
    ];

    public static readonly decimal[] WL = [
        -0.02930m, -0.01465m, 0.02832m, 0.08398m, 0.16309m,
        0.26270m, 0.58496m, 1.48535m
    ];
}
