using ContentDotNet.BitStream;
using ContentDotNet.Extensions.H264.Cabac.Internal;
using ContentDotNet.Primitives;
using System.Reflection.PortableExecutable;

namespace ContentDotNet.Extensions.H264.Cabac;

/// <summary>
///   An H.264 arithmetic decoding engine.
/// </summary>
public sealed class ArithmeticDecoder
{
    private readonly BitStreamReader _boundReader;

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
    /// <returns>The bin.</returns>
    public bool ReadBin(ref CabacContext cabac, bool bypassFlag)
    {
        uint codIRange = CodIRange;
        uint codIOffset = CodIOffset;
        bool retVal = ReadAEBinaryDecision(_boundReader, ref cabac, bypassFlag, ref codIRange, ref codIOffset);
        CodIRange = codIRange;
        CodIOffset = codIOffset;
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
        codIOffset = (codIOffset << 1) | Int32Boolean.U32(reader.ReadBit());

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
            Renormalize(reader, ref codIOffset, ref codIRange);
            return false;
        }
    }

    static void Renormalize(BitStreamReader reader, ref uint codIOffset, ref uint codIRange)
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

    private static bool AEDecodeDecision(BitStreamReader reader, ref CabacContext cabac, ref uint codIOffset, ref uint codIRange)
    {
        uint qCodIRangeIdx = codIRange >> 6 & 0x03;
        int codIRangeLPS = CabacFunctions.GetRangeTabLps(cabac.PStateIdx, (int)qCodIRangeIdx);

        codIRange -= (uint)codIRangeLPS;

        bool binVal;

        if (codIOffset >= codIRange)
        {
            codIOffset -= codIRange;
            codIRange = (uint)codIRangeLPS;
            binVal = Int32Boolean.B(1 - Int32Boolean.U32(cabac.ValMps));
        }
        else
        {
            binVal = cabac.ValMps;
        }

        StateTransitioning.Apply(ref cabac, binVal);

        Renormalize(reader, ref codIRange, ref codIOffset);

        return binVal;
    }
}
