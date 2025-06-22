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
    private readonly WriterState _start;

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
        _start = _boundWriter.GetState();
        _boundWriter.WriteBits(0, 9); // for now
    }

    /// <summary>
    ///   The base bitstream writer.
    /// </summary>
    public BitStreamWriter BaseWriter => _boundWriter;

    /// <summary>
    ///   Encodes a CABAC bin.
    /// </summary>
    /// <param name="symbols"></param>
    /// <param name="binVal"></param>
    public void WriteBin(ref CabacContext symbols, bool binVal)
    {
        if (symbols.BypassFlag)
            EncodeBypass(binVal);
        if (symbols.CtxIdx == 276)
            EncodeTerminate(binVal);
        EncodeDecision(ref symbols, binVal);
    }

    /// <summary>
    ///   Encodes a binary decision.
    /// </summary>
    /// <param name="symbols"></param>
    /// <param name="binVal"></param>
    public void EncodeDecision(ref CabacContext symbols, bool binVal)
    {
        uint qCodIRangeIdx = (this.codIRange >> 6) & 3;
        int codIRangeLPS = CabacFunctions.GetRangeTabLps(symbols.PStateIdx, (int)qCodIRangeIdx);
        this.codIRange -= (uint)codIRangeLPS;

        if (binVal != symbols.ValMps)
        {
            this.codILow += codIRange;
            this.codIRange = (uint)codIRangeLPS;

            if (symbols.PStateIdx == 0)
            {
                symbols.ValMps = Int32Boolean.B(1 - Int32Boolean.I32(symbols.ValMps));
            }

            symbols.PStateIdx = StateTransitioning.GetLps(symbols.PStateIdx);
            Renormalize();
            this.BinCountsInNALunits++;
        }
        else
        {
            symbols.PStateIdx = StateTransitioning.GetMps(symbols.PStateIdx);
            Renormalize();
            this.BinCountsInNALunits++;
        }
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
        RecursionCounter counter = new(8192);
        while (this.codIRange < 256)
        {
            if (this.codILow < 256)
            {
                PutBit(false);
                this.codIRange <<= 1;
                this.codILow <<= 1;
            }
            else
            {
                if (this.codILow > 512)
                {
                    this.codILow -= 512;
                    PutBit(true);
                    this.codIRange <<= 1;
                    this.codILow <<= 1;
                }
                else
                {
                    this.codILow -= 256;
                    this.bitsOutstanding++;
                    this.codIRange <<= 1;
                    this.codILow <<= 1;
                }
            }
            counter.Increment();
        }
    }

    private void PutBit(bool b)
    {
        if (Int32Boolean.I32(this.firstBitFlag) != 0)
        {
            this.firstBitFlag = false;
        }
        else
        {
            this._boundWriter.WriteBit(b);
        }

        while (this.bitsOutstanding > 0)
        {
            this._boundWriter.WriteBit(!b);
            this.bitsOutstanding--;
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
