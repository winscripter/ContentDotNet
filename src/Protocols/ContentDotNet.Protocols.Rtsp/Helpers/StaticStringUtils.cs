namespace ContentDotNet.Protocols.Rtsp.Helpers
{
    // Use with 'using static'!
    internal static class StaticStringUtils
    {
        public static string Quoted(string nonQuotedString) => $"\"{nonQuotedString}\"";
        public static string? QuotedOrNull(string? nonQuotedString) => nonQuotedString == null ? null : Quoted(nonQuotedString);

    }
}
