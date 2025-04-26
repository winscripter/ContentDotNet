namespace ContentDotNet.Extensions.H264.Helpers;

internal static class CavlcResidualLUT
{
    public static ReadOnlySpan<byte> LUT =>
    [
        0b00,       0b00,       0b01,       0b11,       0b1111,     0b000011,       0b01,       0b01,
        0b00,       0b01,       0b000101,   0b001011,   0b001111,   0b000000,       0b000111,   0b0001111,
    ];
}
