namespace ContentDotNet.Extensions.Audio.G722.Internal
{
    using ContentDotNet.Extensions.Audio.G722.Internal.Components;

    internal class SbAdpcmEncoder
    {
        private const double Beta = 127D / 128D;
        private const double TriangleMin = 2D / 16384D;
        private const int CurrentSample = 0;

        private Variables _last22Variables = new();

        public void InitializeSample()
        {
            _last22Variables.Add(new G722Variables());
        }

        public Variables Last22Variables
        {
            get => _last22Variables;
            set => _last22Variables = value;
        }

        #region Transmit QMF (3.1)

        public double ComputeXL() => ComputeXA() + ComputeXB();
        public double ComputeXH() => ComputeXA() - ComputeXB();

        public double ComputeXA()
        {
            double sum = 0;
            for (int i = 0; i <= 11; i++)
                sum += GetH(2 * i) * _last22Variables[2 * i].xin;
            return sum;
        }

        public double ComputeXB()
        {
            double sum = 0;
            for (int i = 0; i <= 11; i++)
                sum += GetH(2 * i + 1) * _last22Variables[2 * i - 1].xin;
            return sum;
        }

        public static double GetH(int index) => TransmitReceiveOmfCoefficients.Coefficients[index];

        #endregion

        #region Difference Signal Computation (3.2)

        public double GetEL(int n) => _last22Variables[n].xL - _last22Variables[n].SL;
        public double GetEH(int n) => _last22Variables[n].xH - _last22Variables[n].SH;

        #endregion

        #region Adaptive Quantizer (3.3)

        public double ILn(int mL)
        {
            double eL = GetEL(0);
            if (eL >= 0) return DecisionLevelsAndOutputCodes.ILP[mL];
            else return DecisionLevelsAndOutputCodes.ILN[mL];
        }

        public double ILp(int mH)
        {
            double eH = GetEH(0);
            if (eH >= 0) return DecisionLevelsAndOutputCodes.IHP[mH];
            else return DecisionLevelsAndOutputCodes.IHN[mH];
        }

        #endregion

        #region Inverse adaptive quantizers (3.4)

        public double DLt(int n)
        {
            return OutputValuesAndMultipliers.QL4minus1[(int)_last22Variables[n].ILr] * _last22Variables[n].triangleUpL * Math.Sign(_last22Variables[n].ILr);
        }

        #endregion

        #region Quantizer Adaptation (3.5)

        private void UpdateLogScalingFactors(int n = 0)
        {
            G722Variables vars = _last22Variables[n];

            vars.triangleDownL = Beta * _last22Variables[n - 1].triangleDownL + OutputValuesAndMultipliers.WL[(int)_last22Variables[n - 1].ILr];
            vars.triangleDownH = Beta * _last22Variables[n - 1].triangleDownH + OutputValuesAndMultipliers.WH[(int)_last22Variables[n - 1].ILr];

            _last22Variables[n] = vars;
        }

        private void LimitLogScalingFactors(int n = 0)
        {
            G722Variables vars = _last22Variables[n];

            vars.triangleDownL = Math.Clamp(vars.triangleDownL, 0, 9);
            vars.triangleDownH = Math.Clamp(vars.triangleDownH, 0, 11);

            _last22Variables[n] = vars;
        }

        private void ComputeLinearScalingFactors(int n)
        {
            G722Variables vars = _last22Variables[n];

            vars.triangleUpL = Math.Pow(2, vars.triangleDownL + 2) * TriangleMin;
            vars.triangleUpH = 2 * vars.triangleDownH * TriangleMin;

            _last22Variables[n] = vars;
        }

        public void AdaptQuantizers(int n)
        {
            UpdateLogScalingFactors(n);
            LimitLogScalingFactors(n);
            ComputeLinearScalingFactors(n);
        }

        #endregion

        #region Adaptive prediction (3.6)

        private void UpdatePoleSections(int n)
        {
            G722Variables vars = _last22Variables[n];

            double sumL = 0;
            for (int i = 1; i <= 2; i++)
                sumL += _last22Variables[n - 1].aLi * _last22Variables[n - i].rLt;
            double sumH = 0;
            for (int i = 1; i <= 2; i++)
                sumH += _last22Variables[n - 1].aHi * _last22Variables[n - i].rH;
            vars.SLp = sumL;
            vars.SHp = sumH;

            _last22Variables[n] = vars;
        }

        private void UpdateZeroSections(int n)
        {
            G722Variables vars = _last22Variables[n];

            double sumL = 0;
            for (int i = 1; i <= 6; i++)
                sumL += _last22Variables[n - 1].bLi * _last22Variables[n - i].dLt;
            double sumH = 0;
            for (int i = 1; i <= 2; i++)
                sumH += _last22Variables[n - 1].bHi * _last22Variables[n - i].dH;
            vars.SLz = sumL;
            vars.SHz = sumH;

            _last22Variables[n] = vars;
        }

        private void UpdateIntermediatePredictedValues(int n)
        {
            G722Variables vars = _last22Variables[n];

            vars.SL = vars.SLp + vars.SLz;
            vars.SH = vars.SHp + vars.SHz;

            _last22Variables[n] = vars;
        }

        public void AdaptivePrediction(int n)
        {
            UpdatePoleSections(n);
            UpdateZeroSections(n);
            UpdateIntermediatePredictedValues(n);
        }

        public void ComputeReconstructedSignals(int n)
        {
            G722Variables vars = _last22Variables[n];

            vars.rLt = vars.SL + vars.dLt;
            vars.rH = vars.SH + vars.dH;

            vars.PLr = vars.dLt + vars.SLz;
            vars.PH = vars.dH + vars.SHz;

            _last22Variables[n] = vars;
        }

        public void AdaptPoleSelection(int n)
        {
            G722Variables vars = _last22Variables[n];
            G722Variables prevVars = _last22Variables[n - 1];

            double pA = sgn2(vars.PLr) * sgn2(_last22Variables[n - 1].PLr);
            double pB = sgn2(vars.PLr) * sgn2(_last22Variables[n - 2].PLr);

            vars.aL1 = (1 - Math.Pow(2, -8)) * prevVars.aL1 + 3 * Math.Pow(2, -8) * pA;
            vars.aL2 = (1 - Math.Pow(2, -7)) * prevVars.aL2 + Math.Pow(2, -7) * pB - Math.Pow(2, -7) * f() * pA;

            pA = sgn2(vars.PH) * sgn2(_last22Variables[n - 1].PH);
            pB = sgn2(vars.PH) * sgn2(_last22Variables[n - 2].PH);

            vars.aH1 = (1 - Math.Pow(2, -8)) * prevVars.aH1 + 3 * Math.Pow(2, -8) * pA;
            vars.aH2 = (1 - Math.Pow(2, -7)) * prevVars.aH2 + Math.Pow(2, -7) * pB - Math.Pow(2, -7) * f() * pA;

            _last22Variables[n] = vars;
            _last22Variables[n - 1] = prevVars;

            static double sgn2(double x) => x >= 0 ? +1 : -1;

            double f()
            {
                if (Math.Abs(vars.aLi) <= 1D / 2D)
                {
                    return 4D * _last22Variables[n - 1].aLi;
                }
                else
                {
                    return 2D * Math.Sign(_last22Variables[n - 1].aLi);
                }
            }
        }

        public void AdaptZeroSection(int n)
        {
            G722Variables var = _last22Variables[n];

            double sum = 0;
            for (int i = 1; i <= 6; i++)
                sum += sgn2(_last22Variables[n - 1].dLt);

            var.bLi = (1 - Math.Pow(2, -8)) * _last22Variables[n - 1].bLi + Math.Pow(2, -7) * sgn3(var.dLt) * sum;

            _last22Variables[n] = var;

            static double sgn3(double x) => Math.Sign(x);
            static double sgn2(double x) => x >= 0 ? +1 : -1;
        }

        public void AcceptSample()
        {
            AdaptQuantizers(CurrentSample);
            AdaptivePrediction(CurrentSample);
            ComputeReconstructedSignals(CurrentSample);
            AdaptPoleSelection(CurrentSample);
            AdaptZeroSection(CurrentSample);
        }

        #endregion
    }
}
