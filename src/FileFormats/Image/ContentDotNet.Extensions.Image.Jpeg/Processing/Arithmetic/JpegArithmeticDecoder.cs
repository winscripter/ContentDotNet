namespace ContentDotNet.Extensions.Image.Jpeg.Processing.Arithmetic
{
    using ContentDotNet.Extensions.Image.Jpeg.Exceptions;

    internal class JpegArithmeticDecoder
    {
        private readonly JpegArithmeticDecoderRegisters _registers;
        private readonly JpegArithmeticContextVariable[] _contextVariables = new JpegArithmeticContextVariable[32];

        public JpegArithmeticDecoder(Stream stream)
        {
            _registers = new JpegArithmeticDecoderRegisters(stream);
        }

        public bool Decode(int s)
        {
            bool retval;

            _registers.A -= _contextVariables[s].Qe;

            if (_registers.Cx < _registers.A)
            {
                if (_registers.A < 0x8000)
                {
                    retval = CondMpsExchange(s);
                    Renormalize();
                }
                else
                {
                    retval = _contextVariables[s].MPS.AsBoolean();
                }
            }
            else
            {
                retval = CondLpsExchange(s);
                Renormalize();
            }

            return retval;
        }

        private bool CondLpsExchange(int s)
        {
            bool retval;

            if (_registers.A < _contextVariables[s].Qe)
            {
                retval = _contextVariables[s].MPS.AsBoolean();
                _registers.Cx -= _registers.A;
                _registers.A = _contextVariables[s].Qe;

                EstimateQeAfterMps(s);
            }
            else
            {
                retval = (1 - _contextVariables[s].MPS).AsBoolean();
                _registers.Cx -= _registers.A;
                _registers.A = _contextVariables[s].Qe;

                EstimateQeAfterLps(s);
            }

            return retval;
        }

        private bool CondMpsExchange(int s)
        {
            bool retval;

            if (_registers.A < _contextVariables[s].Qe)
            {
                retval = (1 - _contextVariables[s].MPS).AsBoolean();
                EstimateQeAfterLps(s);
            }
            else
            {
                retval = _contextVariables[s].MPS.AsBoolean();
                EstimateQeAfterMps(s);
            }

            return retval;
        }

        private void Renormalize()
        {
        start:
            if (_registers.CT != 0)
            {
                ByteIn();
                _registers.CT = 8;
            }

            _registers.A <<= 1;
            _registers.C <<= 1;
            _registers.CT--;

            if (_registers.A < 0x8000)
                goto start;
        }

        private void ByteIn()
        {
            _registers.BP++;
            _registers.Stream.ReadByte();

            if (JpegStreamUtils.GetCurrentByte(_registers.Stream) == 0xFF)
            {
                Unstuff0();
            }
            else
            {
                _registers.C += JpegStreamUtils.GetCurrentByte(_registers.Stream) << 8;
            }
        }

        private void Unstuff0()
        {
            _registers.BP++;
            _registers.Stream.ReadByte();

            if (JpegStreamUtils.GetCurrentByte(_registers.Stream) == 0)
            {
                _registers.C |= 0xFF00;
            }
            else
            {
                Span<byte> markerBuffer = stackalloc byte[2];
                _registers.Stream.Read(markerBuffer);

                if (markerBuffer[0] != 0xFF)
                    throw new JpegException("Not a valid marker");

                _registers.BP = _registers.Stream.Position;
            }
        }
    }
}
