using ContentDotNet.BitStream;
using ContentDotNet.Extensions.H264.Cabac.Internal;
using ContentDotNet.Primitives;

namespace ContentDotNet.Extensions.H264.Cabac;

/// <summary>
///   Writes CABAC Bins.
/// </summary>
public sealed class ArithmeticEncoder
{
    private uint codILow;
    private uint codIRange;
    private bool firstBitFlag;
    private int bitsOutstanding;
    private int BinCountsInNALunits;
    private readonly BitStreamWriter _boundWriter;
    private BitString _previouslyWrittenBins = default;

    /// <summary>
    ///   Initializes a new instance of the <see cref="ArithmeticEncoder"/> class.
    /// </summary>
    /// <param name="boundWriter">The bound writer.</param>
    public ArithmeticEncoder(BitStreamWriter boundWriter)
    {
        codILow = 0;
        codIRange = 510;
        firstBitFlag = true;
        bitsOutstanding = 0;
        _boundWriter = boundWriter;
        BinCountsInNALunits = 0;
    }

    /// <summary>
    ///   The base bitstream writer.
    /// </summary>
    public BitStreamWriter BaseWriter => _boundWriter;

    /// <summary>
    ///   Bins that were previously written.
    /// </summary>
    public BitString PreviouslyWrittenBins => _previouslyWrittenBins;

    /// <summary>
    ///   Encodes a CABAC bin.
    /// </summary>
    /// <param name="symbols"></param>
    /// <param name="binVal"></param>
    public void WriteBin(ref CabacContext symbols, bool binVal)
    {
        if (symbols.BypassFlag)
            EncodeBypass(binVal);
        else if (symbols.CtxIdx == 276)
            EncodeTerminate(binVal);
        else
            EncodeDecision(ref symbols, binVal);

        _previouslyWrittenBins += new BitString(Int32Boolean.I32(binVal), 1);
    }

    /// <summary>
    ///   Encodes a binary decision.
    /// </summary>
    /// <param name="symbols"></param>
    /// <param name="binVal"></param>
    public void EncodeDecision(ref CabacContext symbols, bool binVal)
    {
        int qCodIRangeIdx = (int)(this.codIRange >> 6) & 3;
        int codIRangeLPS = CabacFunctions.GetRangeTabLps(symbols.PStateIdx, qCodIRangeIdx);
        this.codIRange -= (uint)codIRangeLPS;

        if (binVal != symbols.ValMps)
        {
            this.codILow += this.codIRange;
            this.codIRange = (uint)codIRangeLPS;

            if (symbols.PStateIdx != 0)
                symbols.ValMps = !symbols.ValMps;

            symbols.PStateIdx = StateTransitioning.GetLps(symbols.PStateIdx);
        }
        else
        {
            symbols.PStateIdx = StateTransitioning.GetMps(symbols.PStateIdx);
        }

        Renormalize();
        this.BinCountsInNALunits++;
    }

    /// <summary>
    ///   Encodes a bypass binary decision.
    /// </summary>
    /// <param name="binVal"></param>
    public void EncodeBypass(bool binVal)
    {
        this.codILow <<= 1;
        if (Int32Boolean.I32(binVal) != 0)
        {
            this.codILow += this.codIRange;
        }

        if (this.codILow >= 1024)
        {
            PutBit(true);
            this.codILow -= 1024;
        }
        else
        {
            if (this.codILow < 512)
            {
                PutBit(false);
            }
            else
            {
                this.codILow -= 512;
                this.bitsOutstanding++;
            }
        }

        this.BinCountsInNALunits++;
    }

    private void Renormalize()
    {
        RecursionCounter rc = new(8192);

    start:
        rc.Increment();

        if (this.codIRange < 256)
        {
            if (this.codILow < 256)
            {
                PutBit(false);
                goto finishThenLoop;
            }
            else
            {
                if (this.codILow >= 512)
                {
                    this.codILow -= 512;
                    PutBit(true);

                    goto finishThenLoop;
                }
                else
                {
                    this.codILow -= 256;
                    this.bitsOutstanding++;
                    goto finishThenLoop;
                }
            }
        }
        else
        {
            return;
        }

    finishThenLoop:
        codIRange <<= 1;
        codILow <<= 1;
        goto start;
    }

    private void PutBit(bool b)
    {
        if (Int32Boolean.I32(this.firstBitFlag) != 0)
        {
            this.firstBitFlag = false;
        }
        else
        {
            this.BaseWriter.WriteBit(b);
        }

        RecursionCounter rc = new(8192);

    loop:
        rc.Increment();
        if (this.bitsOutstanding > 0)
        {
            this.BaseWriter.WriteBit(!b);
            this.bitsOutstanding--;

            goto loop;
        }
    }

    private void EncodeFlush()
    {
        this.codIRange = 2;
        Renormalize();
        PutBit(Int32Boolean.B((this.codILow >> 9) & 1));
        this._boundWriter.WriteBits(((this.codILow >> 7) & 3) | 1, 2);
    }

    /// <summary>
    ///   Encodes a binary decision before termination.
    /// </summary>
    /// <param name="binVal"></param>
    public void EncodeTerminate(bool binVal)
    {
        this.codIRange -= 2;
        if (Int32Boolean.I32(binVal) != 0)
        {
            this.codILow += this.codIRange;
            EncodeFlush();
        }
        else
        {
            Renormalize();
        }
        this.BinCountsInNALunits++;
    }
}
