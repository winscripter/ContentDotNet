namespace ContentDotNet.Extensions.Image.Jpeg.Processing
{
    using ContentDotNet.Linq;

    internal class JpegHuffman
    {
        private readonly List<int> _sizeTable = [];
        private readonly List<int> _codeTable = [];
        private int[]? ehufco, ehufsi;
        private int _lastk;

        public void OrderCodes(ReadOnlySpan<int> huffval)
        {
            int k = 0;

            int len = huffval.Max();
            this.ehufco = new int[len];
            this.ehufsi = new int[len];

        start:
            int i = huffval[k];
            this.ehufco[i] = this._codeTable[k];
            this.ehufsi[i] = this._sizeTable[k];
            k++;

            if (k < _lastk)
                goto start;
        }

        public void GenerateSizeTable(ReadOnlySpan<int> bits)
        {
            int k = 0, i = 1, j = 1;

        start:

            if (j > bits[i])
            {
                i++;
                j = 1;
                if (i > 16)
                {
                    _sizeTable.Add(0);
                    _lastk = k;
                }
                else
                {
                    goto start;
                }
            }
            else
            {
                _sizeTable.Add(i);
                k++;
                j++;

                goto start;
            }
        }

        public void GenerateCodeTable()
        {
            int k = 0, code = 0;
            int si = _sizeTable[0];

        start:
            _codeTable.Add(code);
            code++;
            k++;

            if (_sizeTable[k] == si)
            {
                goto start;
            }
            else
            {
                if (_sizeTable[k] == 0)
                {
                    return;
                }
                else
                {
                sllLoop:

                    code <<= 1;
                    si++;
                    if (_sizeTable[k] == si)
                    {
                        goto start;
                    }
                    else
                    {
                        goto sllLoop;
                    }
                }
            }
        }
    }
}
