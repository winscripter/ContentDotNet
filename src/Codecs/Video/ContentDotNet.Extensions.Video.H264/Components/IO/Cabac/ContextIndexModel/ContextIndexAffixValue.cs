namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ContextIndexModel
{
    /// <summary>
    ///   The affix value.
    /// </summary>
    public class ContextIndexAffixValue : IContextIndexValue
    {
        /// <summary>
        ///   Prefix value
        /// </summary>
        public int Prefix { get; set; }

        /// <summary>
        ///   Suffix value
        /// </summary>
        public int Suffix { get; set; }

        /// <summary>
        ///   Any suffix? (defaults to true)
        /// </summary>
        public bool HasSuffix { get; set; } = true;

        /// <summary>
        ///   Uses DecodeBypass()? (defaults to false)
        /// </summary>
        public bool UsesDecodeBypass { get; set; } = false;

        /// <summary>
        ///   Initializes a new instance of the <see cref="ContextIndexAffixValue"/> class.
        /// </summary>
        /// <param name="prefix">Prefix</param>
        /// <param name="suffix">Suffix</param>
        public ContextIndexAffixValue(int prefix, int suffix)
        {
            Prefix = prefix;
            Suffix = suffix;
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="ContextIndexAffixValue"/> class.
        /// </summary>
        public ContextIndexAffixValue()
            : this(0, 0)
        {
        }
    }
}
