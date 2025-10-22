namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.Components
{
    using ContentDotNet.Extensions.Video.H264.Enumerations;

    internal static class CabacLevelListIndexToCtxIdxInc
    {
        // The next 3 arrays map the input levelListIdx to ctxIdxInc.
        // They depend on the syntax element being parsed and whether or
        // not is the current macroblock a frame macroblock or a field
        // macroblock.
        //
        // Example:
        // int ctxIdxInc = SignificantCoeffFlagField[25]
        //
        // returns ctxIdxInc for levelListIdx being 25, for field macroblocks,
        // syntax elements SignificantCoeffFlag

        private static readonly int[] SignificantCoeffFlagField =
        [
            // 0-31
            0, 1, 1, 2, 2, 3, 3, 4, 5, 6, 7, 7, 7, 8, 4, 5, 6, 9,
            10, 10, 8, 11, 12, 11, 9, 9, 10, 10, 8, 11, 12, 11,

            // 32-62
            9, 9, 10, 10, 8, 11, 12, 11, 9, 9, 10, 10, 8, 13, 13,
            9, 9, 10, 10, 8, 13, 13, 9, 9, 10, 10, 14, 14, 14, 14, 14
        ];

        private static readonly int[] SignificantCoeffFlagFrame =
        [
            // 0-31
            0, 1, 2, 3, 4, 5, 5, 4, 4, 3, 3, 4, 4, 4, 5, 5, 4, 4, 4, 4,
            3, 3, 6, 7, 7, 7, 8, 9, 10, 9, 8, 7,

            // 32-62
            7, 6, 11, 12, 13, 11, 6, 7, 8, 9, 14, 10, 9, 8, 6, 11, 12, 13,
            11, 6, 9, 14, 10, 9, 11, 12, 13, 11, 14, 10, 12
        ];

        private static readonly int[] LastSignificantCoeffFlag = new int[63];

        static CabacLevelListIndexToCtxIdxInc()
        {
            LastSignificantCoeffFlag[0] = 0;
            for (int i = 1; i <= 15; i++) LastSignificantCoeffFlag[i] = 1;
            for (int i = 16; i <= 31; i++) LastSignificantCoeffFlag[i] = 2;
            for (int i = 32; i <= 39; i++) LastSignificantCoeffFlag[i] = 3;
            for (int i = 40; i <= 47; i++) LastSignificantCoeffFlag[i] = 4;
            for (int i = 48; i <= 51; i++) LastSignificantCoeffFlag[i] = 5;
            for (int i = 52; i <= 55; i++) LastSignificantCoeffFlag[i] = 6;
            for (int i = 56; i <= 59; i++) LastSignificantCoeffFlag[i] = 7;
            for (int i = 60; i <= 62; i++) LastSignificantCoeffFlag[i] = 8;
        }

        public static int Get(int levelListIdx, StandaloneCtxIdxIncDerivativeMode mode, bool frameMB)
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(levelListIdx, 62, nameof(levelListIdx));

            if (mode == StandaloneCtxIdxIncDerivativeMode.SignificantCoefficientFlag)
            {
                return frameMB 
                    ? SignificantCoeffFlagFrame[levelListIdx]
                    : SignificantCoeffFlagField[levelListIdx];
            }
            else
            {
                return LastSignificantCoeffFlag[levelListIdx];
            }
        }
    }
}
