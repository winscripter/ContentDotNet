namespace ContentDotNet.Extensions.G722.Internal;

internal sealed class G722InputSampleBuffer
{
    private readonly short[] _buffer;
    private int _ptr = 0;

    public G722InputSampleBuffer()
    {
        _buffer = new short[24];
    }

    public void Add(short value)
    {
        if (_ptr == 23)
        {
            ShiftSamplesBackwards();
            _buffer[_ptr] = value;
        }
        else
        {
            _buffer[_ptr++] = value;
        }
    }

    private void ShiftSamplesBackwards()
    {
        for (int i = 1; i < 24; i++)
            _buffer[i - 1] = _buffer[i];
    }

    public short this[int index]
    {
        get => _buffer[index];
        set => _buffer[index] = value;
    }

    public int Pointer => _ptr;
}
