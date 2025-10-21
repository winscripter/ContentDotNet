namespace ContentDotNet.Protocols.Rtsp
{
    using System.Text;

    /// <summary>
    ///   The RTSP client.
    /// </summary>
    public class RtspClient
    {
        private readonly Stream _rtspServer;

        /// <summary>
        ///   Initializes a new instance of the <see cref="RtspClient"/> class.
        /// </summary>
        /// <param name="rtspServer">The RTSP server.</param>
        public RtspClient(Stream rtspServer)
        {
            _rtspServer = rtspServer;
            CreateStreamReader = () => new StreamReader(_rtspServer, Encoding, leaveOpen: true);
        }

        /// <summary>
        ///   The encoding to use.
        /// </summary>
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        /// <summary>
        ///   Delegate to create StreamReader.
        /// </summary>
        public Func<StreamReader> CreateStreamReader { get; set; }

        /// <summary>
        ///   Delegate to dispose the stream reader.
        /// </summary>
        public Action<StreamReader> DisposeStreamReader { get; set; } = (x) => x.Dispose();

        /// <summary>
        ///   Sends the specified RTSP client message to the server.
        /// </summary>
        /// <param name="clientMessage">The client message.</param>
        public void Send(RtspClientMessage clientMessage)
        {
            byte[] utf8 = Encoding.GetBytes(clientMessage.ToString());
            _rtspServer.Write(utf8);
        }

        /// <summary>
        ///   Sends the specified RTSP client message to the server.
        /// </summary>
        /// <param name="clientMessage">The client message.</param>
        public async Task SendAsync(RtspClientMessage clientMessage)
        {
            byte[] utf8 = Encoding.GetBytes(clientMessage.ToString());
            await _rtspServer.WriteAsync(utf8);
        }


    }
}
