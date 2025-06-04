using ContentDotNet.BitStream;
using System.Runtime.CompilerServices;

namespace ContentDotNet.Extensions.G722.Internal;

internal sealed class G722Codec : IG722Codec
{
    private readonly G722InputSampleBuffer _buffer;

    private double xA;
    private double xB;
    private double _scaleFactorH, _scaleFactorL;

    public G722Codec()
    {
        _buffer = new G722InputSampleBuffer();

        UpdateXAandXB();
    }

    private void UpdateXAandXB() // xA, xB
    {
        int j = _buffer.Pointer;
        if (j > 22)
            j = 22;

        for (int i = 0; i <= 11; i++)
        {
            xA += G722Lookups.H[2 * i] * _buffer[j - (2 * i)];
        }

        for (int i = 0; i <= 11; i++)
        {
            xA += G722Lookups.H[(2 * i) + 1] * _buffer[j - (2 * i) - 1];
        }
    }

    public int SampleRate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public int BitRate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public bool CanChangeBitRate => false;

    public bool CanChangeSampleRate => false;

    public BitStreamReader Stream => throw new NotImplementedException();

    public int ChannelCount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public string Name => "G722";

    public string DisplayName => "G.722";

    public int Mode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public void ReadInterleavedSamples(Span<short> samplesBuffer, int length)
    {
        throw new NotImplementedException();
    }

    public void ReadInterleavedSamples(Span<byte> samplesBuffer, int length)
    {
        throw new NotImplementedException();
    }

    public void ReadSamples(Span<short> samplesBuffer)
    {
        throw new NotImplementedException();
    }

    public void ReadSamples(Span<byte> samplesBuffer)
    {
        throw new NotImplementedException();
    }

    public void ReadSamples(Span<short> channel1, Span<short> channel2, Span<short> channel3, int length)
    {
        throw new NotImplementedException();
    }

    public void ReadSamples(Span<byte> channel1, Span<byte> channel2, Span<byte> channel3, int length)
    {
        throw new NotImplementedException();
    }

    private ushort ReadSample() => throw new NotImplementedException();
    
    private int ILr() => this.Mode;

    private double XL() => xA + xB;
    private double XH() => xA - xB;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double Sgn2(double q) => q >= 0 ? +1 : -1;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double Sgn3(double q) => q > 0 ? +1 : q == 0 ? 0 : -1;
}
