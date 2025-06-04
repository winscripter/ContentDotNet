using ContentDotNet.BitStream;
using ContentDotNet.Extensions.H264.Cabac.Internal;
using ContentDotNet.Extensions.H264.Helpers;
using ContentDotNet.Extensions.H264.Models;
using ContentDotNet.Primitives;

namespace ContentDotNet.Extensions.H264.Cabac;

/// <summary>
///   Represents context for CABAC bitstream parsing.
/// </summary>
public struct CabacContext
{
    /// <summary>
    ///   PStateIdx
    /// </summary>
    public int PStateIdx;

    /// <summary>
    ///   Most Probable Symbol
    /// </summary>
    public bool ValMps;

    /// <summary>
    ///   CodIRange
    /// </summary>
    public uint CodIRange;

    /// <summary>
    ///   CodIOffset
    /// </summary>
    public uint CodIOffset;

    /// <summary>
    ///   Initializes the CABAC decoding engine.
    /// </summary>
    /// <param name="reader">The bitstream reader.</param>
    /// <param name="ctxIdx">The context index.</param>
    /// <param name="cabacInitIdc">CABAC Initialization Identifier Code. See <see cref="SliceHeader.CabacInitIdc"/></param>
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
    public CabacContext(BitStreamReader reader, int ctxIdx, int cabacInitIdc, bool isIOrSISlice, int sliceQPY)
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

        InitializeArithmeticDecodingEngine(reader);
    }

    private void InitializeArithmeticDecodingEngine(BitStreamReader reader)
    {
        CodIRange = 510;
        CodIOffset = reader.ReadBits(9);

        if (CodIOffset is 510 or 511)
            throw new CabacArithmeticException("CodIOffset must be less than 510 or 511, but was " + CodIOffset + ".");
    }

    /// <summary>
    ///   Reads a CABAC bin.
    /// </summary>
    /// <param name="reader">Reader</param>
    /// <param name="ctxIdx">ctxIdx</param>
    /// <param name="bypassFlag">Bypass flag</param>
    /// <returns>The bin.</returns>
    public bool ReadBin(BitStreamReader reader, int ctxIdx, bool bypassFlag)
    {
        return ReadAEBinaryDecision(reader, PStateIdx, ValMps, ctxIdx, bypassFlag, ref CodIRange, ref CodIOffset);
    }

    internal static bool ReadAEBinaryDecision(BitStreamReader reader, int pStateIdx, bool valMPS, int ctxIdx, bool bypassFlag, ref uint codIRange, ref uint codIOffset)
    {
        if (bypassFlag)
            return AEDecodeBypass(reader, ref codIOffset, codIRange);
        if (ctxIdx == 276)
            return AEDecodeTerminate(reader, ref codIOffset, ref codIRange);
        return AEDecodeDecision(pStateIdx, valMPS, ref codIOffset, ref codIRange);
    }

    private static bool AEDecodeBypass(BitStreamReader reader, ref uint codIOffset, uint codIRange)
    {
        codIOffset *= 2;
        codIOffset |= Int32Boolean.U32(reader.ReadBit());

        if (codIOffset >= codIRange)
        {
            codIOffset -= codIRange;
            return true;
        }
        else
        {
            return false;
        }
    }

    private static bool AEDecodeTerminate(BitStreamReader reader, ref uint codIOffset, ref uint codIRange)
    {
        codIRange -= 2;
        if (codIOffset >= codIRange)
        {
            codIOffset |= 1;
            return true;
        }
        else
        {
            Renormalize(ref codIOffset, ref codIRange);
            return false;
        }

        void Renormalize(ref uint codIOffset, ref uint codIRange)
        {
            if (codIRange < 256)
            {
                while (codIRange < 256)
                {
                    codIRange <<= 1;
                    codIOffset <<= 1;
                    codIOffset |= Int32Boolean.U32(reader.ReadBit());
                }
            }
        }
    }

    private static bool AEDecodeDecision(int pStateIdx, bool valMPS, ref uint codIOffset, ref uint codIRange)
    {
        uint qCodIRangeIdx = codIRange >> 6 & 0x03;
        int codIRangeLPS = CabacFunctions.GetRangeTabLps(pStateIdx, (int)qCodIRangeIdx);

        codIRange -= (uint)codIRangeLPS;

        if (codIOffset >= codIRange)
        {
            codIOffset -= codIRange;
            codIRange = (uint)codIRangeLPS;
            return Int32Boolean.B(1 - Int32Boolean.U32(valMPS));
        }
        else
        {
            return valMPS;
        }
    }
}
