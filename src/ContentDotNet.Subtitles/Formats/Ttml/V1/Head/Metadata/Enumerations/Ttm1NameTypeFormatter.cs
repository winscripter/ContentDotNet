namespace ContentDotNet.Subtitles.Formats.Ttml.V1.Head.Metadata.Enumerations
{
    public static class Ttm1NameTypeFormatter
    {
        public static string ToString(Ttm1NameType agentType)
        {
            return agentType.ToString().ToLower();
        }

        public static Ttm1NameType? FromString(string str)
        {
            return str switch
            {
                "full" => Ttm1NameType.Full,
                "family" => Ttm1NameType.Family,
                "given" => Ttm1NameType.Given,
                "alias" => Ttm1NameType.Alias,
                "other" => Ttm1NameType.Other,
                _ => null
            };
        }
    }
}
