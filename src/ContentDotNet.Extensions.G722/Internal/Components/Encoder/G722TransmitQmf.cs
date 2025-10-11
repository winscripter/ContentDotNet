namespace ContentDotNet.Extensions.G722.Internal.Components.Encoder;

internal sealed class G722TransmitQmf
{
    private readonly G722InputSampleBuffer _buffer;

    public G722TransmitQmf(G722InputSampleBuffer buffer)
    {
        _buffer = buffer;
    }

    public decimal ComputeXA(int j)
    {
        decimal sum = 0;
        for (int i = 0; i < 11 + 1; i++)
        {
            sum += G722TROmfCoeffs.Value[2 * i] * this._buffer[j - (2 * i)];
        }
        return sum;
    }

    public decimal ComputeXB(int j)
    {
        decimal sum = 0;
        for (int i = 0; i < 11 + 1; i++)
        {
            sum += G722TROmfCoeffs.Value[2 * i + 1] * this._buffer[j - (2 * i) - 1];
        }
        return sum;
    }

    public decimal GetXL(int j)
    {
        return ComputeXA(j) + ComputeXB(j);
    }

    public decimal GetXH(int j)
    {
        return ComputeXA(j) - ComputeXB(j);
    }
}
