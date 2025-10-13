namespace ContentDotNet.Protocols.Rtsp.Headers.Implementation
{
    using ContentDotNet.Protocols.Rtsp.Headers.Records;
    using System.Collections.Generic;
    using System.Globalization;

    internal class Accept : IRtspAcceptHeader
    {
        private List<AcceptRecord> value = [];

        public List<AcceptRecord> Value { get => value; set => this.value = value; }
        public string? RawText
        {
            get
            {
                return string.Join(", ",
                    value.Select(x =>
                    {
                        if (x.Quality == null) return x.MimeType;
                        else return $"{x.MimeType};q={x.Quality.Value}";
                    }));
            }

            set
            {
                if (value == null) return;

                string[] separated = value.Split(',');
                List<AcceptRecord> retval = [];
                foreach (string s in separated)
                {
                    if (s.Contains("q="))
                        retval.Add(new AcceptRecord(s.Split(';')[0], double.Parse(s[s.IndexOf('=')..], NumberStyles.None)));
                    else
                        retval.Add(new AcceptRecord(s, null));
                }

                this.value = retval;
            }
        }
        public string Text => "Accept";
    }
}
