namespace ContentDotNet.Protocols.Bgp.Models
{
    /// <summary>
    ///   The optional parameter.
    /// </summary>
    public class BgpOpenOptionalParameterModel
    {
        /// <summary>
        ///   The type.
        /// </summary>
        public byte Type { get; }

        /// <summary>
        ///   The value.
        /// </summary>
        public byte[] Value { get; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="BgpOpenOptionalParameterModel"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        public BgpOpenOptionalParameterModel(byte type, byte[] value)
        {
            Type = type;
            Value = value;
        }
    }
}
