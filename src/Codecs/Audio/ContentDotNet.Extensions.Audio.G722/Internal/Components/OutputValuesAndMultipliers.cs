namespace ContentDotNet.Extensions.Audio.G722.Internal.Components
{
    internal static class OutputValuesAndMultipliers
    {
        public static ReadOnlySpan<double> QL6minus1 =>
        [
            0.03409,
            0.10460,
            0.17746,
            0.25300,
            0.33124,
            0.41259,
            0.49706,
            0.58518,
            0.67697,
            0.77310,
            0.87359,
            0.97934,
            1.09036,
            1.20788,
            1.33191,
            1.46415,
            1.60462,
            1.75585,
            1.91782,
            2.09443,
            2.28568,
            2.49806,
            2.73157,
            2.99827,
            3.29816,
            3.65804,
            4.07789,
            4.64140,
            5.34856,
            6.05572,
        ];

        public static ReadOnlySpan<double> QL5minus1 =>
        [
            0.06817,
            0.21389,
            0.37035,
            0.53929,
            0.72286,
            0.92383,
            1.14587,
            1.39391,
            1.67486,
            1.99880,
            2.38131,
            2.84833,
            3.44811,
            4.28782,
            5.70214
        ];

        public static ReadOnlySpan<double> QL4minus1 =>
        [
            0.0000,
            0.29212,
            0.63107,
            1.03485,
            1.53439,
            2.19006,
            3.14822,
            4.99498
        ];

        public static ReadOnlySpan<double> WL =>
        [
            -0.02930,
            -0.01465,
            0.02832,
            0.08398,
            0.16309,
            0.26270,
            0.58496,
            1.48535
        ];

        public static ReadOnlySpan<double> Q2minus1 =>
        [
            0.39453,
            1.80859,
        ];

        public static ReadOnlySpan<double> WH =>
        [
            -0.10449,
            0.38965,
        ];
    }
}
