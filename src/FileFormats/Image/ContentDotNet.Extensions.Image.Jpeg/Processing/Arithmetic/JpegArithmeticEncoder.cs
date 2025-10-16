namespace ContentDotNet.Extensions.Image.Jpeg.Processing.Arithmetic
{
    internal class JpegArithmeticEncoder : ContextVariableHost
    {
        private readonly JpegArithmeticEncoderRegisters _registers;

        public JpegArithmeticEncoder(Stream stream)
        {
            _registers = new JpegArithmeticEncoderRegisters(stream);
            InitializeHumbleEncoder();
        }

        public void Code1(int s)
        {
            JpegArithmeticContextVariable cv = _contextVariables[s];
            if (cv.MPS == 1)
            {
                CodeMps(s);
            }
            else
            {
                CodeLps(s);
            }
        }

        public void Code0(int s)
        {
            JpegArithmeticContextVariable cv = _contextVariables[s];
            if (cv.MPS == 0)
            {
                CodeMps(s);
            }
            else
            {
                CodeLps(s);
            }
        }

        public void CodeLps(int s)
        {
            JpegArithmeticContextVariable cv = _contextVariables[s];

            _registers.A -= cv.Qe;

            if (_registers.A < cv.Qe)
            {
                goto skip;
            }
            _registers.C += _registers.A;
            _registers.A = cv.Qe;

        skip:
            EstimateQeAfterLps(s);
            Renormalize();
        }

        public void CodeMps(int s)
        {
            JpegArithmeticContextVariable cv = _contextVariables[s];

            _registers.A -= cv.Qe;

            if (_registers.A < 0x8000)
            {
                if (_registers.A < cv.Qe)
                {
                    _registers.C += _registers.A;
                    _registers.A = cv.Qe;
                }

                EstimateQeAfterMps(s);
                Renormalize();
            }
        }

        private void Renormalize()
        {
        start:

            _registers.A <<= 1;
            _registers.C <<= 1;
            _registers.CT -= 1;

            if (_registers.CT == 0)
            {
                ByteOut();
                _registers.CT = 8;
            }

            if (_registers.A < 0x8000)
                goto start;
        }

        private void ByteOut()
        {
            _registers.T = _registers.C >> 19;
            if (_registers.T > 0xFF)
            {
                // B++
                _registers.Stream.Position--;
                int curr = _registers.Stream.ReadByte();
                _registers.Stream.Position--;
                _registers.Stream.WriteByte((byte)curr);

                Stuff0();
                OutputStackedZeros();

                _registers.Stream.WriteByte((byte)_registers.T);
            }
            else
            {
                if (_registers.T == 0xFF)
                {
                    _registers.ST++;
                }
                else
                {
                    OutputStackedFFs();
                    _registers.Stream.WriteByte((byte)_registers.T);
                }
            }

            _registers.C &= 0x7FFFF;
        }

        private void OutputStackedZeros()
        {
        start:
            if (_registers.ST == 0)
            {
                return;
            }
            else
            {
                _registers.Stream.WriteByte(0);
                _registers.ST--;
                goto start;
            }
        }

        private void OutputStackedFFs()
        {
        start:
            if (_registers.ST == 0)
            {
                return;
            }
            else
            {
                _registers.Stream.WriteByte(0xFF);
                _registers.Stream.WriteByte(0);
                _registers.ST--;
                goto start;
            }
        }

        private void Stuff0()
        {
            if (_registers.Stream.GetCurrentByte() == 0xFF)
            {
                _registers.Stream.WriteByte(0);
            }
        }

        private void InitializeHumbleEncoder()
        {
            _registers.ST = 0;
            _registers.A = 0x10000;
            _registers.C = 0;
            _registers.CT = 11;
            _registers.BPST = _registers.Stream.Position;
            _registers.BP = _registers.Stream.Position - 1L;
            _registers.Stream.Position = _registers.BP;
        }
        
        public void Flush()
        {
            ClearFinalBits();
            _registers.C <<= _registers.CT;
            ByteOut();
            _registers.C <<= 8;
            ByteOut();
            DiscardFinalZeros();
        }

        private void ClearFinalBits()
        {
            _registers.T = _registers.C + _registers.A - 1;
            _registers.T = (int)(_registers.T & 0xFFFF0000);

            if (_registers.T < _registers.C)
            {
                _registers.T += 0x8000;
            }

            _registers.C = _registers.T;
        }

        private void DiscardFinalZeros()
        {
        start:
            if (_registers.BP < _registers.BPST)
            {
                return;
            }
            else
            {
                if (JpegStreamUtils.GetCurrentByte(_registers.Stream) == 0)
                {
                    _registers.BP--;
                    _registers.Stream.Position--;
                    goto start;
                }
                else
                {
                    if (JpegStreamUtils.GetCurrentByte(_registers.Stream) == 0xFF)
                    {
                        _registers.BP++;
                        _registers.Stream.Position++;
                    }

                    return;
                }
            }
        }
    }
}
