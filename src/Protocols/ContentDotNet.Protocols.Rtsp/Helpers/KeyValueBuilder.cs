namespace ContentDotNet.Protocols.Rtsp.Helpers
{
    using System.Collections.Specialized;
    using System.Text;

    internal class KeyValueBuilder
    {
        private readonly NameValueCollection nvc = [];

        public void SetIfValueNotNull(string key, string? value)
        {
            if (value != null)
            {
                nvc[key] = value;
            }
        }

        public void Set(string key, string? value)
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));

            SetIfValueNotNull(key, value);
        }

        public string BuildCommaSeparatedString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < nvc.Count; i++)
            {
                bool isLast = i >= nvc.Count - 1;

                sb.Append($"{nvc.Keys[i]}={nvc[i]}");
                if (!isLast)
                    sb.Append(", ");
            }

            return sb.ToString();
        }
    }
}
