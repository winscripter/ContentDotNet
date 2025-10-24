namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;
    using ContentDotNet.Protocols.Rtsp.Headers.Records;
    using System.Collections.Generic;

    internal class AcceptEncodingImpl : AcceptEncodingBase, IRtspAcceptEncodingHeader
    {
        public override string Text => "Accept-Encoding";

        public List<AcceptEncodingRecord> Value { get; set; } = [];

        public override string ToString()
        {
            // Since Accept-Encoding is structurally similar to Accept,
            // we'll just convert the Accept-Encoding records to Accept records
            // and use Accept to get the output. Same result.

            var accept = new AcceptImpl()
            {
                Value = [.. this.Value.Select(x => new AcceptRecord(x.Encoding, x.Quality))]
            };
            return accept.ToString();
        }
    }
}
