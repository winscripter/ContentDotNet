namespace ContentDotNet.Subtitles.Formats.Ttml.V1.Head.Metadata.Enumerations
{
    public static class Ttm1AgentTypeFormatter
    {
        public static string ToString(Ttm1AgentType agentType)
        {
            return agentType.ToString().ToLower();
        }

        public static Ttm1AgentType? FromString(string str)
        {
            return str switch
            {
                "person" => Ttm1AgentType.Person,
                "character" => Ttm1AgentType.Character,
                "group" => Ttm1AgentType.Group,
                "organization" => Ttm1AgentType.Organization,
                "other" => Ttm1AgentType.Other,
                _ => null
            };
        }
    }
}
