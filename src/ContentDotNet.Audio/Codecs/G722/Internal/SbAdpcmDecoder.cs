namespace ContentDotNet.Audio.Codecs.G722.Internal
{
    using ContentDotNet.Audio.Codecs.G722.Internal.Components;

    internal class SbAdpcmDecoder
    {
        private readonly SbAdpcmEncoder _encoder;

        public SbAdpcmDecoder(SbAdpcmEncoder encoder)
        {
            _encoder = encoder;
        }

        public SbAdpcmEncoder Encoder => _encoder;

        #region Inverse adaptive quantizer (4.1.1)

        public double Dl(int n)
        {
            return OutputValuesAndMultipliers.QL6minus1[(int)_encoder.Last22Variables[n].ILr] *
                _encoder.Last22Variables[n].triangleUpL * Math.Sign(_encoder.Last22Variables[n].ILr);
        }

        #endregion

        #region Reconstructed Signal Computation (4.3.2)

        public double Rl(int n)
        {
            G722Variables variables = _encoder.Last22Variables[n];
            return variables.SL + variables.dL;
        }

        #endregion

        #region Receive QMF (4.4)

        private double Xd(int n, int i)
        {
            return Rl(n - i) - _encoder.Last22Variables[n - i].rH;
        }

        //private double Xs(int n, int i)
        //{
        //    return Rl(n - i) + _encoder.Last22Variables[n - i].rH;
        //}

        public void ComputeOutputSignal(int n = 0)
        {
            double sumJ = 0;
            //double sumJ1 = 0;

            for (int i = 0; i <= 11; i++)
            {
                sumJ += SbAdpcmEncoder.GetH(2 * i) * Xd(n, i);
            }

            sumJ *= 2D;

            //for (int i = 0; i <= 11; i++)
            //{
            //    sumJ1 += SbAdpcmEncoder.GetH(2 * i + 1) * Xs(n, i);
            //}

            //sumJ1 *= 2D;

            G722Variables curr = _encoder.Last22Variables[n];

            curr.xout = (int)sumJ;

            _encoder.Last22Variables[n] = curr;
        }

        #endregion
    }
}
