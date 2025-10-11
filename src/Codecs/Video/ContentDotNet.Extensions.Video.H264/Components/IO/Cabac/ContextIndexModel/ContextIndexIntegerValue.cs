namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ContextIndexModel
{
    /// <summary>
    ///   The raw integer value.
    /// </summary>
    public class ContextIndexIntegerValue : IContextIndexValue
    {
        /// <summary>
        ///   Actual value.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        ///   Uses DecodeBypass()? (defaults to false)
        /// </summary>
        public bool UsesDecodeBypass { get; set; }

        /// <summary>
        ///   Has suffix? (defaults to true)
        /// </summary>
        public bool HasSuffix { get; set; } = true;

        /// <summary>
        ///   Initializes a new instance of the <see cref="ContextIndexIntegerValue"/> class.
        /// </summary>
        /// <param name="value">The raw value</param>
        public ContextIndexIntegerValue(int value)
        {
            Value = value;
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="ContextIndexIntegerValue"/> class.
        /// </summary>
        public ContextIndexIntegerValue()
            : this(0)
        {
        }
    }
}
