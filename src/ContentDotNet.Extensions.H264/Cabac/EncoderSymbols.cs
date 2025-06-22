using ContentDotNet.Extensions.H264.Cabac.Internal;
using ContentDotNet.Extensions.H264.Helpers;

namespace ContentDotNet.Extensions.H264.Cabac;

/// <summary>
///   Symbols for the H.264 CABAC CArithmetic Encoder.
/// </summary>
public struct EncoderSymbols
{
    /// <summary>
    ///   PStateIdx
    /// </summary>
    public int PStateIdx { get; set; }

    /// <summary>
    ///   CtxIdx
    /// </summary>
    public int CtxIdx { get; set; }

    /// <summary>
    ///   ValMps
    /// </summary>
    public bool ValMps { get; set; }

    private EncoderSymbols(int pStateIdx, int ctxIdx, bool valMps)
    {
        PStateIdx = pStateIdx;
        CtxIdx = ctxIdx;
        ValMps = valMps;
    }

    private EncoderSymbols(int m, int n, int sliceQPY, int ctxIdx)
    {
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

        CtxIdx = ctxIdx;
    }

    /// <summary>
    ///   Creates encoder parameters from given parameters.
    /// </summary>
    /// <param name="sliceQPY">Slice QPY</param>
    /// <param name="isIOrSISlice">Is the current slice of type I or SI?</param>
    /// <param name="ctxIdx">Context index</param>
    /// <param name="cabacInitIdc">Taken from the SPS</param>
    /// <returns></returns>
    public static EncoderSymbols From(int sliceQPY, bool isIOrSISlice, int ctxIdx, uint cabacInitIdc)
    {
        (int m, int n) = isIOrSISlice ? CabacFunctions.GetInitDataForIOrSISlice(ctxIdx) : CabacFunctions.GetInitData(ctxIdx, cabacInitIdc);
        return new EncoderSymbols(m, n, sliceQPY, ctxIdx);
    }
}
