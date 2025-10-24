namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Enumerations;
    using ContentDotNet.Protocols.Rtsp.Headers.Formatters;
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;
    using ContentDotNet.Protocols.Rtsp.Headers.Records;
    using System.Collections.Generic;
    using System.Text;

    internal class AcceptCredentialsImpl : AcceptCredentialsBase, IRtspAcceptCredentialsHeader
    {
        private static readonly AcceptCredentialsPolicyFormatter FormatterInstance = new();
        private static readonly AcceptCredentialsHashAlgorithmFormatter HashAlgorithmFormatterInstance = new();

        public override string Text => "Accept-Credentials";

        public AcceptCredentialsPolicy Policy { get; set; }
        public List<AcceptCredentialsRecord> Value { get; set; } = [];

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(FormatterInstance.Format(Policy));
            sb.Append(' ');
            sb.Append(
                string.Join(", ", Value.Select(x =>
                {
                    var sb2 = new StringBuilder();
                    sb2.Append('"');
                    sb2.Append(x.Origin.ToString());
                    sb2.Append('"');
                    sb2.Append(';');
                    sb2.Append(HashAlgorithmFormatterInstance.Format(x.HashAlgorithm));
                    sb2.Append(';');
                    sb2.Append(x.Digest);
                    return sb2.ToString();
                })));
            return sb.ToString();
        }
    }
}
