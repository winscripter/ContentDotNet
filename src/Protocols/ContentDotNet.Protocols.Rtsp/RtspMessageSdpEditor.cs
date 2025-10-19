namespace ContentDotNet.Protocols.Rtsp
{
    /// <summary>
    ///   SDP lines editor for RTSP messages.
    /// </summary>
    public partial class RtspMessageSdpEditor
    {
        private List<string> _lines = [];

        /// <summary>
        ///   Creates a new instance of <see cref="RtspMessageSdpEditor"/>.
        /// </summary>
        public RtspMessageSdpEditor()
        {
        }

        /// <summary>
        ///   All lines.
        /// </summary>
        public List<string> Lines => _lines;

        /// <summary>
        ///   Returns <see langword="true"/> if there's any line with the <paramref name="lineFirstChar"/>= prefix.
        /// </summary>
        /// <param name="lineFirstChar">The character.</param>
        /// <returns>A boolean.</returns>
        public bool ContainsLine(char lineFirstChar)
        {
            Span<char> format = stackalloc char[2];
            format[0] = lineFirstChar;
            format[1] = '=';

            for (int i = 0; i < _lines.Count; i++)
            {
                if (_lines[i].AsSpan().StartsWith(format, StringComparison.Ordinal))
                {
                    return true;
                }
            }

            return false;
        }

        private RtspMessageSdpEditor Line(char prefix, string value)
        {
            // evict
            var newVal = new List<string>();
            foreach (string curr in _lines)
            {
                if (!curr.StartsWith(prefix))
                    newVal.Add(curr);
            }

            newVal.Add($"{prefix}={value}");
            _lines = newVal;

            return this;
        }
    }
}
