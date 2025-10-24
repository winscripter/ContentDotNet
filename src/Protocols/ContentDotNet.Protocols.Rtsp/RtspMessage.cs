namespace ContentDotNet.Protocols.Rtsp
{
    using System.Text;

    /// <summary>
    ///   Abstracts an RTSP message. See <see cref="RtspClientMessage"/> and <see cref="RtspServerMessage"/>.
    /// </summary>
    public abstract class RtspMessage
    {
        /// <summary>
        ///   The raw SDP lines.
        /// </summary>
        public RtspMessageSdpEditor Sdp { get; set; } = new();

        /// <summary>
        ///   RTSP headers.
        /// </summary>
        public IList<string> HeaderLines { get; set; } = [];

        internal static bool IsHeaderLine(string hl)
        {
            return hl.Contains(':');
        }
    }

    /// <summary>
    ///   RTSP message from the Client to Server.
    /// </summary>
    public class RtspClientMessage : RtspMessage
    {
        /// <summary>
        ///   The request line.
        /// </summary>
        public RtspRequestLine RequestLine { get; set; } = new();

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(RequestLine.ToString());
            if (HeaderLines.Count > 0)
            {
                sb.AppendLine();
                foreach (var header in HeaderLines)
                {
                    sb.AppendLine(header.ToString());
                }
            }
            return sb.ToString();
        }

        public static RtspClientMessage Parse(TextReader reader)
        {
            var message = new RtspClientMessage();
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                if (line.Contains(':')) message.HeaderLines.Add(line);
                else if (line.Contains('=')) message.Sdp.Lines.Add(line);
                else if (Enum.TryParse<RtspMethodType>(line.Split(' ')[0], out _)) message.RequestLine = RtspRequestLine.Parse(line);
            }
            return message;
        }

        public static async Task<RtspClientMessage> ParseAsync(TextReader reader)
        {
            var message = new RtspClientMessage();
            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                if (line.Contains(':')) message.HeaderLines.Add(line);
                else if (line.Contains('=')) message.Sdp.Lines.Add(line);
                else if (Enum.TryParse<RtspMethodType>(line.Split(' ')[0], out _)) message.RequestLine = RtspRequestLine.Parse(line);
            }
            return message;
        }
    }

    /// <summary>
    ///   The RTSP request line. In this example:
    ///   <example><code>ANNOUNCE rtsp://server.example.com/fizzle/foo RTSP/1.0</code></example>
    ///   <see cref="MethodType"/> will be <see cref="RtspMethodType.Announce"/> and
    ///   <see cref="Parameter"/> will be <c>rtsp://server.example.com/fizzle/foo RTSP/1.0</c>.
    /// </summary>
    public class RtspRequestLine
    {
        /// <summary>
        ///   The RTSP method type.
        /// </summary>
        public RtspMethodType MethodType { get; set; }

        /// <summary>
        ///   The parameter.
        /// </summary>
        public string Parameter { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{MethodType.ToString().ToLower()} {Parameter}";
        }

        public static RtspRequestLine Parse(string line)
        {
            string[] split = line.Split(' ');
            string before = split[0];
            string after = line[(line.IndexOf('.'))..];

            return new()
            {
                MethodType = Enum.Parse<RtspMethodType>(Utils.FirstCharToUpper(before.ToLower())),
                Parameter = after
            };
        }
    }

    /// <summary>
    ///   The status line.
    /// </summary>
    public class RtspServerMessage : RtspMessage
    {
        /// <summary>
        ///   The status line.
        /// </summary>
        public string StatusLine { get; set; } = string.Empty;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(StatusLine);
            if (HeaderLines.Count > 0)
            {
                sb.AppendLine();
                foreach (var header in HeaderLines)
                {
                    sb.AppendLine(header.ToString());
                }
            }
            return sb.ToString();
        }

        public static RtspServerMessage Parse(TextReader reader)
        {
            var message = new RtspServerMessage();
            bool first = true;
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (first)
                {
                    first = false;
                    message.StatusLine = line;
                    continue;
                }

                if (line.Contains(':')) message.HeaderLines.Add(line);
                else if (line.Contains('=')) message.Sdp.Lines.Add(line);
            }
            return message;
        }

        public static async Task<RtspServerMessage> ParseAsync(TextReader reader)
        {
            var message = new RtspServerMessage();
            bool first = true;
            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (first)
                {
                    first = false;
                    message.StatusLine = line;
                    continue;
                }

                if (line.Contains(':')) message.HeaderLines.Add(line);
                else if (line.Contains('=')) message.Sdp.Lines.Add(line);
            }
            return message;
        }
    }
}
