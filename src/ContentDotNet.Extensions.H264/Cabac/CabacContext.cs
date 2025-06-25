using ContentDotNet.Extensions.H264.Cabac.Internal;
using ContentDotNet.Extensions.H264.Helpers;
using ContentDotNet.Extensions.H264.Models;

namespace ContentDotNet.Extensions.H264.Cabac;

/// <summary>
///   Represents context for CABAC bitstream parsing.
/// </summary>
public struct CabacContext
{
    /// <summary>
    ///   PStateIdx
    /// </summary>
    public int PStateIdx { get; set; }

    /// <summary>
    ///   Most Probable Symbol
    /// </summary>
    public bool ValMps { get; set; }

    /// <summary>
    ///   Context index
    /// </summary>
    public int CtxIdx { get; set; }

    /// <summary>
    ///   BypassFlag
    /// </summary>
    public bool BypassFlag { get; set; }

    /// <summary>
    ///   Initializes the CABAC decoding engine.
    /// </summary>
    /// <param name="ctxIdx">The context index.</param>
    /// <param name="cabacInitIdc">CABAC Initialization Identifier Code. See <see cref="SliceHeader.CabacInitIdc"/></param>
    /// <param name="bypassFlag">The bypass flag.</param>
    /// <param name="isIOrSISlice">
    /// Is the current slice an I or SI slice? See the following methods:
    /// <list type="bullet">
    ///   <item><see cref="SliceTypes.IsI(int)"/></item>
    ///   <item><see cref="SliceTypes.IsI(uint)"/></item>
    ///   <item><see cref="SliceTypes.IsSI(int)"/></item>
    ///   <item><see cref="SliceTypes.IsSI(uint)"/></item>
    /// </list>
    /// as well as <see cref="SliceHeader.SliceType"/>.
    /// </param>
    /// <param name="sliceQPY">Slice Quantization Parameter for the Luma channel.</param>
    public CabacContext(int ctxIdx, int cabacInitIdc, bool isIOrSISlice, bool bypassFlag, int sliceQPY)
    {
        BypassFlag = bypassFlag;

        (int m, int n) = GetMn(ctxIdx, (uint)cabacInitIdc, isIOrSISlice);

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
    ///   Returns the variables M and N.
    /// </summary>
    /// <param name="ctxIdx">CtxIdx</param>
    /// <param name="cabacInitIdc">See <see cref="SliceHeader.CabacInitIdc"/></param>
    /// <param name="isIOrSISlice">See <see cref="SliceTypes.IsI(int)"/> and <see cref="SliceTypes.IsSI(int)"/></param>
    /// <returns></returns>
    public static (int m, int n) GetMn(int ctxIdx, uint cabacInitIdc, bool isIOrSISlice)
        => isIOrSISlice ? CabacFunctions.GetInitDataForIOrSISlice(ctxIdx) : CabacFunctions.GetInitData(ctxIdx, cabacInitIdc);
}
