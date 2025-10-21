namespace ContentDotNet.Protocols.Rtsp
{
    internal static class Utils
    {
        public static string FirstCharToUpper(string input)
        {
            // https://stackoverflow.com/a/4405876/21072788
            return string.Concat(input.First().ToString().ToUpper(), input.AsSpan(1));
        }
    }
}
