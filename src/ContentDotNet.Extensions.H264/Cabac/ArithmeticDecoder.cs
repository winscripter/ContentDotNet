using ContentDotNet.BitStream;
using ContentDotNet.Extensions.H264.Cabac.Internal;
using ContentDotNet.Primitives;

namespace ContentDotNet.Extensions.H264.Cabac;

/// <summary>
///   An H.264 arithmetic decoding engine.
/// </summary>
public sealed class ArithmeticDecoder
{
    private readonly BitStreamReader _boundReader;
    private BitString _previouslyDecodedBins = default;
    private int _binIdx = 0;

    /// <summary>
    ///   CodIRange
    /// </summary>
    public uint CodIRange { get; set; }

    /// <summary>
    ///   CodIOffset
    /// </summary>
    public uint CodIOffset { get; set; }

    /// <summary>
    ///   The base/bound reader.
    /// </summary>
    public BitStreamReader BaseReader => _boundReader;

    /// <summary>
    ///   Initializes a new instance of the <see cref="ArithmeticDecoder"/> class.
    /// </summary>
    /// <param name="boundReader">The bitstream reader.</param>
    public ArithmeticDecoder(BitStreamReader boundReader)
    {
        _boundReader = boundReader;
        InitializeArithmeticDecodingEngine(boundReader);
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="ArithmeticDecoder"/> class.
    /// </summary>
    /// <param name="boundReader">The bitstream reader.</param>
    /// <param name="codIOffset">CodIOffset</param>
    /// <param name="codIRange">CodIRange</param>
    public ArithmeticDecoder(BitStreamReader boundReader, uint codIOffset, uint codIRange)
    {
        _boundReader = boundReader;
        this.CodIOffset = codIOffset;
        this.CodIRange = codIRange;
    }

    /// <summary>
    ///   Bins that were decoded previously.
    /// </summary>
    public BitString PreviouslyDecodedBins => _previouslyDecodedBins;

    private void InitializeArithmeticDecodingEngine(BitStreamReader reader)
    {
        CodIRange = 510;
        CodIOffset = reader.ReadBits(9);

        if (CodIOffset is 510 or 511)
            throw new CabacArithmeticException("CodIOffset must be less than 510 or 511, but was " + CodIOffset + ".");
    }

    /// <summary>
    ///   The bin index.
    /// </summary>
    public int BinIndex => _binIdx;

    /// <summary>
    ///   Reads a CABAC bin.
    /// </summary>
    /// <returns>The bin.</returns>
    public bool ReadBin(ref CabacContext cabac, bool bypassFlag)
    {
        uint codIRange = CodIRange;
        uint codIOffset = CodIOffset;
        bool retVal = ReadAEBinaryDecision(_boundReader, ref cabac, bypassFlag, ref codIRange, ref codIOffset);
        CodIRange = codIRange;
        CodIOffset = codIOffset;
        _previouslyDecodedBins += new BitString(Int32Boolean.I32(retVal), 1);
        _binIdx++;
        return retVal;
    }

    /// <summary>
    ///   Reads a CABAC bin.
    /// </summary>
    /// <returns>The bin.</returns>
    public bool ReadBin(ref CabacContext ctx) => ReadBin(ref ctx, ctx.BypassFlag);

    private static bool ReadAEBinaryDecision(BitStreamReader reader, ref CabacContext cabacCtx, bool bypassFlag, ref uint codIRange, ref uint codIOffset)
    {
        if (bypassFlag)
            return AEDecodeBypass(reader, ref codIOffset, codIRange);
        if (cabacCtx.CtxIdx == 276)
            return AEDecodeTerminate(reader, ref codIOffset, ref codIRange);
        return AEDecodeDecision(reader, ref cabacCtx, ref codIOffset, ref codIRange);
    }

    private static bool AEDecodeBypass(BitStreamReader reader, ref uint codIOffset, uint codIRange)
    {
        codIOffset <<= 1;
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
            return true;
        }
        else
        {
            Renormalize(reader, ref codIOffset, ref codIRange);
            return false;
        }
    }

    static void Renormalize(BitStreamReader reader, ref uint codIOffset, ref uint codIRange)
    {
    start:
        if (codIRange < 256)
        {
            codIRange <<= 1;
            codIOffset <<= 1;
            codIOffset |= (uint)Int32Boolean.I32(reader.ReadBit());

            goto start;
        }
    }

    private static bool AEDecodeDecision(BitStreamReader reader, ref CabacContext cabac, ref uint codIOffset, ref uint codIRange)
    {
        uint qCodIRangeIdx = (codIRange >> 6) & 3;
        int codIRangeLPS = CabacFunctions.GetRangeTabLps(cabac.PStateIdx, (int)qCodIRangeIdx);
        codIRange -= (uint)codIRangeLPS;

        bool binVal;

        if (codIOffset >= codIRange)
        {
            binVal = !cabac.ValMps;
            codIOffset -= codIRange;
            codIRange = (uint)codIRangeLPS;

            if (cabac.PStateIdx == 0)
            {
                cabac.ValMps = !cabac.ValMps;
            }

            cabac.PStateIdx = StateTransitioning.GetLps(cabac.PStateIdx);
        }
        else
        {
            binVal = cabac.ValMps;
            cabac.PStateIdx = StateTransitioning.GetMps(cabac.PStateIdx);
        }

        Renormalize(reader, ref codIOffset, ref codIRange);

        return binVal;
    }
}
