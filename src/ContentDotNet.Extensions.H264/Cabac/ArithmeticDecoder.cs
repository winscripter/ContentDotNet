using ContentDotNet.BitStream;
using ContentDotNet.Extensions.H264.Cabac.Internal;
using ContentDotNet.Primitives;

namespace ContentDotNet.Extensions.H264.Cabac;

internal sealed class ArithmeticDecoder
{
    private readonly BitStreamReader _boundReader;

    public uint CodIRange { get; set; }
    public uint CodIOffset { get; set; }

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
    public bool ReadBin(int pStateIdx, bool valMPS, bool bypassFlag, int ctxIdx)
    {
        uint codIRange = CodIRange;
        uint codIOffset = CodIOffset;
        bool retVal = ReadAEBinaryDecision(_boundReader, pStateIdx, valMPS, ctxIdx, bypassFlag, ref codIRange, ref codIOffset);
        CodIRange = codIRange;
        CodIOffset = codIOffset;
        return retVal;
    }

    public bool ReadBin(CabacContext cabac)
        => ReadBin(cabac.PStateIdx, cabac.ValMps, false, cabac.CtxIdx);

    private bool ReadAEBinaryDecision(BitStreamReader reader, int pStateIdx, bool valMPS, int ctxIdx, bool bypassFlag, ref uint codIRange, ref uint codIOffset)
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
