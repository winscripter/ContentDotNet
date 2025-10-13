namespace ContentDotNet.Protocols.Bgp.Models
{
    /// <summary>
    ///   The BGP path attribute.
    /// </summary>
    public record BgpPathAttributeModel
    {
        /// <summary>
        ///   The flags.
        /// </summary>
        public byte Flags { get; }

        /// <summary>
        ///   The type code.
        /// </summary>
        public byte TypeCode { get; }

        /// <summary>
        ///   The value.
        /// </summary>
        public byte[] Value { get; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="BgpPathAttributeModel"/> class.
        /// </summary>
        /// <param name="flags">The flags.</param>
        /// <param name="typeCode">The type code.</param>
        /// <param name="value">The value.</param>
        public BgpPathAttributeModel(byte flags, byte typeCode, byte[] value)
        {
            Flags = flags;
            TypeCode = typeCode;
            Value = value;
        }
    }
}
