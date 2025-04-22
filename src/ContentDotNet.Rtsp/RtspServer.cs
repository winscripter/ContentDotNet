using System.Net.Sockets;

namespace ContentDotNet.Rtsp;

/// <summary>
///   An RTSP server to house real-time media. You can use it to send
///   videos and audios into one or more connected RTSP client(s).
/// </summary>
public abstract class RtspServer : IDisposable
{
    private readonly Socket _networkSocket;
    private bool _disposeInner;

    public RtspServer(Socket networkSocket)
    {
        _networkSocket = networkSocket;
    }

    public RtspServer(Socket networkSocket, bool disposeInner)
        : this(networkSocket)
    {
        _disposeInner = disposeInner;
    }

    /// <summary>
    ///   Invoked when a client connects.
    /// </summary>
    public event EventHandler<RtspClientConnectEventArgs>? ClientConnect;

    /// <summary>
    ///   Should the inner socket be disposed?
    /// </summary>
    public bool DisposeInner
    {
        get { return _disposeInner; }
        set { _disposeInner = value; }
    }

    /// <summary>
    ///   Sends the frame into all RTSP clients.
    /// </summary>
    /// <param name="frameType">Type of the frame.</param>
    /// <param name="codecType">Type of the codec.</param>
    /// <param name="frameIndex">Frame index.</param>
    /// <param name="data">Raw data that will later be decoded.</param>
    public void Send(uint frameType, uint codecType, long frameIndex, Stream data) =>
        SendCore(frameType, codecType, frameIndex, data);

    /// <summary>
    ///   Sends the frame into all RTSP clients.
    /// </summary>
    /// <param name="frameType">Type of the frame.</param>
    /// <param name="codecType">Type of the codec.</param>
    /// <param name="frameIndex">Frame index.</param>
    /// <param name="data">Raw data that will later be decoded.</param>
    public void Send(uint frameType, uint codecType, long frameIndex, byte[] data)
    {
        using var memoryStream = new MemoryStream(data);
        SendCore(frameType, codecType, frameIndex, memoryStream);
    }

    /// <summary>
    ///   Sends the frame into all RTSP clients.
    /// </summary>
    /// <param name="frameType">Type of the frame.</param>
    /// <param name="codecType">Type of the codec.</param>
    /// <param name="frameIndex">Frame index.</param>
    /// <param name="data">Raw data that will later be decoded.</param>
    public void Send(uint frameType, uint codecType, long frameIndex, Memory<byte> data)
    {
        using var memoryStream = new MemoryStream(data.ToArray());
        SendCore(frameType, codecType, frameIndex, memoryStream);
    }

    /// <summary>
    ///   Sends the frame into all RTSP clients.
    /// </summary>
    /// <param name="frameType">Type of the frame.</param>
    /// <param name="codecType">Type of the codec.</param>
    /// <param name="frameIndex">Frame index.</param>
    /// <param name="data">Raw data that will later be decoded.</param>
    public void Send(uint frameType, uint codecType, long frameIndex, Span<byte> data)
    {
        using var memoryStream = new MemoryStream(data.ToArray());
        SendCore(frameType, codecType, frameIndex, memoryStream);
    }

    /// <summary>
    ///   Where actual sending logic is performed.
    /// </summary>
    /// <param name="frameType">Type of the frame.</param>
    /// <param name="codecType">Type of the codec.</param>
    /// <param name="frameIndex">Index of the frame.</param>
    /// <param name="data">Frame data.</param>
    protected abstract void SendCore(uint frameType, uint codecType, long frameIndex, Stream data);

    /// <summary>
    ///   Disposes the inner socket.
    /// </summary>
    public void Dispose()
    {
        if (_disposeInner)
            _networkSocket.Dispose();

        GC.SuppressFinalize(this);
    }
}
