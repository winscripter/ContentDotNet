namespace ContentDotNet.Extensions.Image.Jpeg.Processing.Arithmetic
{
    internal abstract class ContextVariableHost
    {
        internal readonly JpegArithmeticContextVariable[] _contextVariables = new JpegArithmeticContextVariable[32];

        public JpegArithmeticContextVariable[] ContextVariables => _contextVariables;

        protected void EstimateQeAfterMps(int s)
        {
            int I = _contextVariables[s].Index;
            NextIndices indices = QeTable.NextIndicesLookup[I];
            I = indices.Mps;
            _contextVariables[s].Index = I;
            _contextVariables[s].Qe = indices.Qe;
        }

        protected void EstimateQeAfterLps(int s)
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
    }
}
