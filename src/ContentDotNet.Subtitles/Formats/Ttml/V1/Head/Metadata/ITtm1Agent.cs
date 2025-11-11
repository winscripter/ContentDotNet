namespace ContentDotNet.Subtitles.Formats.Ttml.V1.Head.Metadata
{
    using ContentDotNet.Subtitles.Formats.Ttml.V1.Head.Metadata.Enumerations;

    /// <summary>
    ///   ttm:agent element
    /// </summary>
    public interface ITtm1Agent : ITtm1Element
    {
        /// <summary>
        ///   Adds an agent element.
        /// </summary>
        /// <param name="agentElement">The agent element.</param>
        void AddAgentElement(ITtm1AgentElement agentElement);

        /// <summary>
        ///   All agents.
        /// </summary>
        IEnumerable<ITtm1AgentElement> Agents { get; }

        /// <summary>
        ///   Removes the agent at index <paramref name="i"/>.
        /// </summary>
        /// <param name="i">The index of the agent to remove.</param>
        void RemoveAgentAt(int i);

        /// <summary>
        ///   The agent type.
        /// </summary>
        Ttm1AgentType AgentType { get; set; }
    }
}
