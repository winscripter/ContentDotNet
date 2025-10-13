namespace ContentDotNet.Rtsp.Sdp
{
    using ContentDotNet.Rtsp.Sdp.Abstractions;
    using ContentDotNet.Rtsp.Sdp.EventArguments;

    /// <summary>
    ///   The default SDP service.
    /// </summary>
    public class SdpService : ISdpService
    {
        private ISdpLineModel? _lastLineModel;
        private ISdpLineSerializer _serializer;

        /// <summary>
        ///   Initializes a new instance of the <see cref="SdpService"/> class.
        /// </summary>
        public SdpService()
        {
            _serializer = BuiltInLineSerializer.Instance;
        }

        /// <inheritdoc cref="ISdpService.LastLine" />
        public ISdpLineModel? LastLine => _lastLineModel;

        /// <inheritdoc cref="ISdpService.ExceptionThrownOnRead" />
        public Exception? ExceptionThrownOnRead { get; private set; }

        /// <inheritdoc cref="ISdpService.Serializer" />
        public ISdpLineSerializer Serializer
        {
            get => _serializer;
            set
            {
                ArgumentNullException.ThrowIfNull(value, nameof(value));

                _serializer = value;
            }
        }

        /// <inheritdoc cref="ISdpService.LineReceived" />
        public event EventHandler<LineReceivedEventArgs>? LineReceived;

        /// <inheritdoc cref="ISdpService.LineWritten" />
        public event EventHandler<LineWrittenEventArgs>? LineWritten;

        /// <inheritdoc cref="ISdpService.WriteLine(ISdpLineModel, TextWriter)" />
        public void WriteLine(ISdpLineModel line, TextWriter writer)
        {
            this.Serializer.Write(line, writer);
            this.PostProcess(line, true, false);
        }

        /// <inheritdoc cref="ISdpService.WriteLineAsync(ISdpLineModel, TextWriter)" />
        public async Task WriteLineAsync(ISdpLineModel line, TextWriter writer)
        {
            await this.Serializer.WriteAsync(line, writer);
            this.PostProcess(line, true, true);
        }

        /// <inheritdoc cref="ISdpService.ReadLine(TextReader)" />
        public ISdpLineModel? ReadLine(TextReader reader)
        {
            ISdpLineModel? line;
            Exception? ex = null;

            try
            {
                line = this.Serializer.Read(reader);
            }
            catch (Exception e)
            {
                ex = e;
                line = null;
            }

            PostProcess(line, false, false, ex != null);
            if (ex != null)
                ExceptionThrownOnRead = ex;

            return line;
        }

        /// <inheritdoc cref="ISdpService.ReadLineAsync(TextReader)" />
        public async Task<ISdpLineModel?> ReadLineAsync(TextReader reader)
        {
            ISdpLineModel? line;
            Exception? ex = null;

            try
            {
                line = await this.Serializer.ReadAsync(reader);
            }
            catch (Exception e)
            {
                ex = e;
                line = null;
            }

            PostProcess(line, false, false, ex != null);
            if (ex != null)
                ExceptionThrownOnRead = ex;

            return line;
        }

        private void PostProcess(ISdpLineModel? line, bool write, bool async, bool successful = true)
        {
            this._lastLineModel = line;

            if (write)
                this.LineWritten?.Invoke(this, new LineWrittenEventArgs(line, async));
            else
                this.LineReceived?.Invoke(this, new LineReceivedEventArgs(successful, line, async));
        }
    }
}
