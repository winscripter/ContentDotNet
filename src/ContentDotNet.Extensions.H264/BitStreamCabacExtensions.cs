using ContentDotNet.Extensions.H264.Internal.Entropies;
using ContentDotNet.Extensions.H264.Utilities;

namespace ContentDotNet.Extensions.H264;

/// <summary>
///   Adds extensions to bitstreams for reading CABAC values.
/// </summary>
public static class BitStreamCabacExtensions
{
    internal static bool ReadAEBinaryDecision(this BitStreamReader reader, int pStateIdx, bool valMPS, int ctxIdx, bool bypassFlag, ref uint codIRange, ref uint codIOffset)
    {
        if (bypassFlag)
            return AEDecodeBypass(reader, ref codIOffset, codIRange);
        if (ctxIdx == 276)
            return AEDecodeTerminate(reader, ref codIOffset, ref codIRange);
        return AEDecodeDecision(reader, pStateIdx, valMPS, ref codIOffset, ref codIRange);
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

    private static bool AEDecodeDecision(BitStreamReader reader, int pStateIdx, bool valMPS, ref uint codIOffset, ref uint codIRange)
    {
        uint qCodIRangeIdx = (codIRange >> 6) & 0x03;
        int codIRangeLPS = Cabac.GetRangeTabLps(pStateIdx, (int)qCodIRangeIdx);

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
