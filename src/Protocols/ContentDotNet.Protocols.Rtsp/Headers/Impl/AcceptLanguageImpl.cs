namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;
    using ContentDotNet.Protocols.Rtsp.Headers.Records;

    internal class AcceptLanguageImpl : AcceptLanguageBase, IRtspAcceptLanguageHeader
    {
        public override string Text => "Accept-Language";

        public List<AcceptLanguageRecord> Value { get; set; } = [];

        public override string ToString()
        {
            // Since Accept-Language is structurally similar to Accept,
            // we'll just convert the Accept-Language records to Accept records
            // and use Accept to get the output. Same result.

            var accept = new AcceptImpl()
            {
                Value = [.. this.Value.Select(x => new AcceptRecord(x.Language, x.Quality))]
            };
            return accept.ToString();
        }
    }
}
