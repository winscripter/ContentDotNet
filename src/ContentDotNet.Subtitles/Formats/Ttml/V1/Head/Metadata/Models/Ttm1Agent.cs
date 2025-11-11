namespace ContentDotNet.Subtitles.Formats.Ttml.V1.Head.Metadata.Models
{
    using ContentDotNet.Subtitles.Formats.Ttml.V1.Head.Metadata.Enumerations;
    using System.Collections.Generic;
    using System.Xml.Linq;

    public class Ttm1Agent(XElement element) : ITtm1Agent
    {
        private readonly List<ITtm1AgentElement> _agentElements = [];

        public IEnumerable<ITtm1AgentElement> Agents => _agentElements;

        public Ttm1AgentType AgentType
        {
            get => Ttm1AgentTypeFormatter.FromString(Element.Attribute("type")?.Value ?? throw new InvalidOperationException("No type attribute")) ?? throw new InvalidOperationException("Invalid type attribute");
            set => Element.SetAttributeValue("type", Ttm1AgentTypeFormatter.ToString(value));
        }

        public XElement Element => element;

        public void AddAgentElement(ITtm1AgentElement agentElement) => _agentElements.Add(agentElement);

        public void RemoveAgentAt(int i) => _agentElements.RemoveAt(i);
    }
}
