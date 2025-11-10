namespace ContentDotNet.Video.Codecs.H264.Components
{
    using ContentDotNet.Api.BitStream;
    using ContentDotNet.Utilities;
    using System.Runtime.CompilerServices;

    /// <summary>
    ///   Binary arithmetic (BiAri) reader for H.264.
    /// </summary>
    public class H264CabacArithmeticReader
    {
        private const int Half = 0x1FE;
        private const int Quarter = 0x100;

        private readonly BitStreamReader _decodeStream;
        private int _value;
        private int _bitsLeft;
        private int _range;

        /// <summary>
        ///   Initializes a new instance of the <see cref="H264CabacArithmeticReader"/> class.
        /// </summary>
        /// <param name="reader">The bit-stream reader.</param>
        public H264CabacArithmeticReader(BitStreamReader reader)
        {
            this._decodeStream = reader;
            this._value = (GetByte() << 16) | GetByte();
            this._bitsLeft = 15;
            this._range = Half;
        }

        private int GetByte() => (int)_decodeStream.ReadByte();
        private int GetWord() => (GetByte() << 8) | GetByte();

        /// <summary>
        ///   Reads a single symbol.
        /// </summary>
        /// <param name="contextVariable">Reference to the context variable</param>
        /// <returns>The symbol.</returns>
        public bool DecodeSymbol(ref H264CabacContextVariable contextVariable)
        {
            // NOTE: This method is too complex to inline, so don't do it.

            int bit = contextVariable.Mps.AsInt32();

            int rLPS = H264Tables.RangeTabLPS[contextVariable.State, (_range >> 6) & 0x3];
            _range -= rLPS;

            if (_value < (_range << _bitsLeft))
            {
                contextVariable.State = H264Tables.AcNextStateMps[contextVariable.State];
                if (_range >= Quarter) return bit.AsBoolean();
                else
                {
                    _range <<= 1;
                    _bitsLeft--;
                }
            }
            else
            {
                int renorm = H264Tables.RenormTable32[(rLPS >> 3) & 0x1F];
                _value -= _range << _bitsLeft;
                _range = rLPS << renorm;
                _bitsLeft -= renorm;
                bit ^= 0x1;
                if (contextVariable.State == 0) contextVariable.Mps = !contextVariable.Mps;
                contextVariable.State = H264Tables.AcNextStateLps[contextVariable.State];
            }

            if (_bitsLeft > 0) return bit.AsBoolean();
            else
            {
                _value <<= 16;
                _value |= GetWord();
                _bitsLeft += 16;
                return bit.AsBoolean();
            }
        }

        /// <summary>
        ///   Decodes a symbol with equal probability.
        /// </summary>
        /// <returns>The decoded symbol</returns>
        public bool DecodeSymbolWithEqualProbability()
        {
            if (--_bitsLeft == 0)
            {
                _value = (_value << 16) | GetWord();
                _bitsLeft = 16;
            }
            int temporaryValue = _value - (_range << _bitsLeft);
            if (temporaryValue < 0) return false;
            else
            {
                _value = temporaryValue;
                return true;
            }
        }

        /// <summary>
        ///   Decodes final.
        /// </summary>
        /// <returns></returns>
        public bool DecodeFinal()
        {
            int range = _range - 2;
            if (_value < 0)
            {
                if (range >= Quarter)
                {
                    _range = range;
                    return false;
                }
                else
                {
                    _range = range << 1;
                    if (--_bitsLeft > 0) return false;
                    else
                    {
                        _value = (_value << 16) | GetWord();
                        _bitsLeft = 16;
                        return false;
                    }
                }
            }
            else return true;
        }

        /// <summary>
        ///   Decodes the symbol as an integer.
        /// </summary>
        /// <param name="contextVariable"></param>
        /// <returns>The decoded symbol.</returns>
        public int DecodeSymbolAsInt32(ref H264CabacContextVariable contextVariable) => DecodeSymbol(ref contextVariable).AsInt32();

        /// <summary>
        ///   Decodes the symbol as an integer.
        /// </summary>
        /// <returns>The decoded symbol.</returns>
        public int DecodeSymbolWithEqualProbabilityAsInt32() => DecodeSymbolWithEqualProbability().AsInt32();

        /// <summary>
        ///   Decodes the final as an integer.
        /// </summary>
        /// <returns>The decoded final.</returns>
        public int DecodeFinalAsInt32() => DecodeFinal().AsInt32();
    }

    /// <summary>
    ///   CABAC Contexts.
    /// </summary>
    /// <remarks>
    ///   <b>⚠️ Warning:</b> This class is subject to change in the future. Do not use. <b>⚠️</b>
    /// </remarks>
    public class H264CabacContexts
    {
        private const int NumberOfBlockTypes = 22; // Because we allow 4:4:4 chroma subsampling

        private H264CabacContextVariable[][] MbType { get; set; } = JaggedArrayFactory.Create2D<H264CabacContextVariable>(3, 11);
        private H264CabacContextVariable[][] MvContexts { get; set; } = JaggedArrayFactory.Create2D<H264CabacContextVariable>(2, 10);
        private H264CabacContextVariable[][] RefNoContexts { get; set; } = JaggedArrayFactory.Create2D<H264CabacContextVariable>(2, 6);

        // Texture
        private H264CabacContextVariable[] IntraPredModeContexts { get; set; } = new H264CabacContextVariable[2];
        private H264CabacContextVariable[][] OneContexts { get; set; } = JaggedArrayFactory.Create2D<H264CabacContextVariable>(NumberOfBlockTypes, 5);
        private H264CabacContextVariable[][] AbsoluteContexts { get; set; } = JaggedArrayFactory.Create2D<H264CabacContextVariable>(NumberOfBlockTypes, 5);

        /// <summary>
        ///   Returns the reference to the motion vector ctx.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref H264CabacContextVariable GetMotionVectorContextRef(int x, int y)
        {
            return ref MvContexts[x][y];
        }

        /// <summary>
        ///   Returns the reference to the macroblock type ctx.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref H264CabacContextVariable GetMacroblockTypeContextRef(int x, int y)
        {
            return ref MbType[x][y];
        }

        /// <summary>
        ///   Returns the reference to the intra prediction mode ctx.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref H264CabacContextVariable GetIntraPredModeContextRef(int index)
        {
            return ref IntraPredModeContexts[index];
        }

        /// <summary>
        ///   Returns the reference to the reference index ctx.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref H264CabacContextVariable GetReferenceIndexContextRef(int x, int y)
        {
            return ref RefNoContexts[x][y];
        }

        /// <summary>
        ///   Returns the reference to the one ctx.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref H264CabacContextVariable GetOneContextRef(int x, int y)
        {
            return ref OneContexts[x][y];
        }

        /// <summary>
        ///   Returns the reference to the absolute ctx.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref H264CabacContextVariable GetAbsoluteContextRef(int x, int y)
        {
            return ref AbsoluteContexts[x][y];
        }
    }

    /// <summary>
    ///   Single context variable.
    /// </summary>
    public struct H264CabacContextVariable
    {
        /// <summary>
        ///   The state.
        /// </summary>
        public ushort State;

        /// <summary>
        ///   MPS?
        /// </summary>
        public bool Mps;

        public H264CabacContextVariable(ushort state, bool mps)
        {
            State = state;
            Mps = mps;
        }
    }
}
