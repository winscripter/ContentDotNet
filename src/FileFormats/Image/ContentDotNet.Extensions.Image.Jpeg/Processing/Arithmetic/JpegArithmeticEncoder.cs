namespace ContentDotNet.Extensions.Image.Jpeg.Processing.Arithmetic
{
    internal class JpegArithmeticEncoder
    {
        private readonly JpegArithmeticRegisters _registers;
        private readonly JpegArithmeticContextVariable[] _contextVariables = new JpegArithmeticContextVariable[32];

        public JpegArithmeticEncoder(Stream stream)
        {
            _registers = new JpegArithmeticRegisters(stream);
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

        private void EstimateQeAfterMps(int s)
        {
            int I = _contextVariables[s].Index;
            NextIndices indices = QeTable.NextIndicesLookup[I];
            I = indices.Mps;
            _contextVariables[s].Index = I;
            _contextVariables[s].Qe = indices.Qe;
        }

        private void EstimateQeAfterLps(int s)
        {
            int I = _contextVariables[s].Index;

            NextIndices indices = QeTable.NextIndicesLookup[I];

            if (indices.SwitchMps)
            {
                _contextVariables[s].MPS = 1 - _contextVariables[s].MPS;
            }

            I = indices.Lps;
            _contextVariables[s].Index = I;
            _contextVariables[s].Qe = indices.Qe;
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
                _registers.B++;
                Stuff0();
                OutputStackedZeros();
                _registers.BP++;
                _registers.B = _registers.T;
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
                    _registers.BP++;
                    _registers.B = _registers.T;
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
                _registers.BP++;
                _registers.B = 0;
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
                _registers.BP++;
                _registers.B = 0xFF;
                _registers.BP++;
                _registers.B = 0;
                _registers.ST--;
                goto start;
            }
        }

        private void Stuff0()
        {
            if (_registers.B == 0xFF)
            {
                _registers.BP++;
                _registers.B = 0;
            }
        }

        private void InitializeHumbleEncoder()
        {
            _registers.ST = 0;
            _registers.A = 0x10000;
            _registers.C = 0;
            _registers.CT = 11;
            _registers.BP = _registers.BPST - 1;
        }
    }
}
