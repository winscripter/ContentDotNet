namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ContextIndexModel
{
    /// <summary>
    ///   See <see cref="ContextIndexIntegerValue"/> and <see cref="ContextIndexAffixValue"/>.
    /// </summary>
    public interface IContextIndexValue
    {
        /// <summary>
        ///   Uses DecodeBypass()? (defaults to false)
        /// </summary>
        bool UsesDecodeBypass { get; set; }

        /// <summary>
        ///   Has suffix? (defaults to true)
        /// </summary>
        bool HasSuffix { get; set; }
    }
}
