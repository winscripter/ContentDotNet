using ContentDotNet.BitStream;
using ContentDotNet.Extensions.H264.Cabac.Internal;
using ContentDotNet.Extensions.H264.Helpers;

namespace ContentDotNet.Extensions.H264.Cabac;

/// <summary>
///   CABAC
/// </summary>
public struct ContextAdaptiveBinaryArithmetic
{
    /// <summary>
    ///   PStateIdx
    /// </summary>
    public int PStateIdx;

    /// <summary>
    ///   Most Probable Symbol
    /// </summary>
    public bool ValMps;

    public ContextAdaptiveBinaryArithmetic(int ctxIdx, int cabacInitIdc, bool isIOrSISlice, int sliceQPY)
    {
        (int m, int n) = isIOrSISlice ? CabacFunctions.GetInitDataForIOrSISlice(ctxIdx) : CabacFunctions.GetInitData(ctxIdx, cabacInitIdc);

        int preCtxState = Util264.Clip3(1, 126, ((m * Util264.Clip3(0, 51, sliceQPY)) >> 4) + n);
        if (preCtxState <= 63)
        {
            PStateIdx = 63 - preCtxState;
            ValMps = false;
        }
        else
        {
            PStateIdx = preCtxState - 64;
            ValMps = true;
        }
    }
}
