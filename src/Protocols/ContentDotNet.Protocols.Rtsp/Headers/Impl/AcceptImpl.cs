namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;
    using ContentDotNet.Protocols.Rtsp.Headers.Records;
    using System.Collections.Generic;
    using System.Text;

    internal class AcceptImpl : AcceptBase, IRtspAcceptHeader
    {
        public override string Text => "Accept";

        public List<AcceptRecord> Value { get; set; } = [];

        public override string ToString()
        {
            return string.Join("; ", Value.Select(x =>
            {
                var sb = new StringBuilder();

                sb.Append(x.MimeType);
                if (x.Quality != null)
                {
                    sb.Append(", ");
                    sb.Append("q=");
                    sb.Append(x.Quality!.Value.ToString(provider: null).Trim());
                }

                return sb.ToString();
            }));
        }
    }
}
