namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;
    using System.Text;

    internal class CacheControlImpl : CacheControlBase, IRtspCacheControlHeader
    {
        public override string Text => "Cache-Control";

        public bool NoCache { get; set; }
        public bool NoStore { get; set; }
        public bool MustRevalidate { get; set; }
        public bool Public { get; set; }
        public bool Private { get; set; }
        public int? MaxAge { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            bool appended = false;
            if (NoCache)
            {
                sb.Append("no-cache");
                appended = true;
            }
            if (NoStore)
            {
                if (appended)
                {
                    sb.Append(", ");
                }
                sb.Append("no-store");
                appended = true;
            }
            if (MustRevalidate)
            {
                if (appended)
                {
                    sb.Append(", ");
                }
                sb.Append("must-revalidate");
                appended = true;
            }
            if (Public)
            {
                if (appended)
                {
                    sb.Append(", ");
                }
                sb.Append("public");
                appended = true;
            }
            if (Private)
            {
                if (appended)
                {
                    sb.Append(", ");
                }
                sb.Append("private");
                appended = true;
            }
            if (MaxAge != null)
            {
                if (appended)
                {
                    sb.Append(", ");
                }
                sb.Append($"max-age={MaxAge}");
            }
            return sb.ToString();
        }
    }
}
