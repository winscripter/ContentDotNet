namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac
{
    using ContentDotNet.Extensions.Video.H264.Enumerations;
    using ContentDotNet.Extensions.Video.H264.Exceptions;
    using ContentDotNet.Extensions.Video.H264.Models.Cabac;
    using System.Runtime.CompilerServices;
    using static ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.Components.InitializationTables;

    internal static class H264CabacInitializer
    {
        const int INITLUT_ROW_SIZE = 8;

        public static (int m, int n) GetInitDataForIOrSISlice(int ctxIdx)
        {
            return (InitLUT[ctxIdx * INITLUT_ROW_SIZE], InitLUT[ctxIdx * INITLUT_ROW_SIZE + 1]);
        }

        public static (int m, int n) GetInitData(int ctxIdx, int cabacInitIdc)
        {
            if (cabacInitIdc == 0)
                return (InitLUT[ctxIdx * INITLUT_ROW_SIZE + 2], InitLUT[ctxIdx * INITLUT_ROW_SIZE + 3]);
            else if (cabacInitIdc == 1)
                return (InitLUT[ctxIdx * INITLUT_ROW_SIZE + 4], InitLUT[ctxIdx * INITLUT_ROW_SIZE + 5]);
            else if (cabacInitIdc == 2)
                return (InitLUT[ctxIdx * INITLUT_ROW_SIZE + 6], InitLUT[ctxIdx * INITLUT_ROW_SIZE + 7]);
            else
                return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetRangeTabLps(int pStateIdx, int qCodIRangeIdx)
        {
            return RangeTabLPS[pStateIdx][qCodIRangeIdx];
        }

        public static H264ContextVariable CreateContextVariable(int ctxIdx, int cabacInitIdc, H264SliceType sliceType, int sliceQPY)
        {
            (int m, int n) = sliceType is H264SliceType.SI or H264SliceType.I ? GetInitDataForIOrSISlice(ctxIdx) : GetInitData(ctxIdx, cabacInitIdc);
            if (m == na || n == na)
                throw new H264Exception($"m or n is not-an. ctxIdx={ctxIdx}, initIDC={cabacInitIdc}, slice type={sliceType}, QPY={sliceQPY}");

            int preCtxState = IntrinsicFunctions.Clip3(1, 126, ((m * IntrinsicFunctions.Clip3(0, 51, sliceQPY)) >> 4) + n);
            int pStateIdx;
            int valMPS;
            if (preCtxState <= 63)
            {
                pStateIdx = 63 - preCtxState;
                valMPS = 0;
            }
            else
            {
                pStateIdx = preCtxState - 64;
                valMPS = 1;
            }
            return new H264ContextVariable()
            {
                PStateIdx = pStateIdx,
                ValMps = valMPS
            };
        }
    }
}
