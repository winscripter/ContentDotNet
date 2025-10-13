namespace ContentDotNet.Protocols.Rtsp.Headers.Records
{
    /// <summary>
    ///   Record for the Via header.
    /// </summary>
    public record ViaRecord
    {
        /// <summary>
        ///   The protocol and version.
        /// </summary>
        public string? ProtocolAndVersion { get; set; }

        /// <summary>
        ///   The host.
        /// </summary>
        public string? Host { get; set; }

        /// <summary>
        ///   The port.
        /// </summary>
        public int? Port { get; set; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="ViaRecord"/> class.
        /// </summary>
        /// <param name="protocolAndVersion">See <see cref="ProtocolAndVersion"/></param>
        /// <param name="host">See <see cref="Host"/></param>
        /// <param name="port">See <see cref="Port"/></param>
        public ViaRecord(string? protocolAndVersion, string? host, int? port)
        {
            ProtocolAndVersion = protocolAndVersion;
            Host = host;
            Port = port;
        }
    }
}
