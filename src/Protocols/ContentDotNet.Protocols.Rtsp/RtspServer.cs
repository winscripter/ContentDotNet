namespace ContentDotNet.Protocols.Rtsp
{
    using System.Text;

    /// <summary>
    ///   The RTSP server.
    /// </summary>
    public class RtspServer
    {
        private readonly List<Stream> _clients = [];

        /// <summary>
        ///   Initializes a new instance of the <see cref="RtspServer"/> class.
        /// </summary>
        public RtspServer()
        {
        }

        /// <summary>
        ///   Adds a client and returns the index of the client that was added.
        /// </summary>
        /// <param name="client">The client to add.</param>
        /// <returns>Index of the new client.</returns>
        public int AddClient(Stream client)
        {
            _clients.Add(client);
            return _clients.Count - 1;
        }

        /// <summary>
        ///   All clients.
        /// </summary>
        public List<Stream> Clients => _clients;

        /// <summary>
        ///   The encoding to use.
        /// </summary>
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        /// <summary>
        ///   Sends the specified server-side RTSP message to the client of specified index.
        /// </summary>
        /// <param name="serverMessage">The input RTSP message.</param>
        /// <param name="clientIndex">The index of the client to send to.</param>
        public void SendMessageToClient(RtspServerMessage serverMessage, int clientIndex)
        {
            long prevPos = _clients[clientIndex].Position;
            byte[] data = this.Encoding.GetBytes(serverMessage.ToString());
            _clients[clientIndex].Write(data);
            _clients[clientIndex].Position = prevPos;
        }

        /// <summary>
        ///   Sends the specified server-side RTSP message to the client of specified index.
        /// </summary>
        /// <param name="serverMessage">The input RTSP message.</param>
        /// <param name="clientIndex">The index of the client to send to.</param>
        public async Task SendMessageToClientAsync(RtspServerMessage serverMessage, int clientIndex)
        {
            long prevPos = _clients[clientIndex].Position;
            byte[] data = this.Encoding.GetBytes(serverMessage.ToString());
            await _clients[clientIndex].WriteAsync(data);
            _clients[clientIndex].Position = prevPos;
        }

        /// <summary>
        ///   Broadcasts the specified RTSP server-side message to all of the connected RTSP clients.
        /// </summary>
        /// <param name="serverMessage">The input RTSP server-side message.</param>
        public void BroadcastMessage(RtspServerMessage serverMessage)
        {
            for (int i = 0; i < _clients.Count; i++)
                SendMessageToClient(serverMessage, i);
        }

        /// <summary>
        ///   Broadcasts the specified RTSP server-side message to all of the connected RTSP clients.
        /// </summary>
        /// <param name="serverMessage">The input RTSP server-side message.</param>
        public async Task BroadcastMessageAsync(RtspServerMessage serverMessage)
        {
            for (int i = 0; i < _clients.Count; i++)
                await SendMessageToClientAsync(serverMessage, i);
        }

        /// <summary>
        ///   Receives a message from the client of specified index.
        /// </summary>
        /// <param name="clientIndex">The index of the client.</param>
        /// <returns>The message.</returns>
        public RtspClientMessage Receive(int clientIndex)
        {
            using var ms = new MemoryStream();
            int b;
            while ((b = _clients[clientIndex].ReadByte()) > 0)
                ms.WriteByte((byte)b);
            ms.Position = 0;
            using var stringReader = new StringReader(this.Encoding.GetString(ms.ToArray()));
            return RtspClientMessage.Parse(stringReader);
        }

        /// <summary>
        ///   Receives a message from the client of specified index.
        /// </summary>
        /// <param name="clientIndex">The index of the client.</param>
        /// <returns>The message.</returns>
        public async Task<RtspClientMessage> ReceiveAsync(int clientIndex)
        {
            using var ms = new MemoryStream();
            int b;
            while ((b = _clients[clientIndex].ReadByte()) > 0)
                ms.WriteByte((byte)b);
            ms.Position = 0;
            using var stringReader = new StringReader(this.Encoding.GetString(ms.ToArray()));
            return await RtspClientMessage.ParseAsync(stringReader);
        }
    }
}
