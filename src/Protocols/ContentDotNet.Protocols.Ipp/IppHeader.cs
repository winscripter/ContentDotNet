namespace ContentDotNet.Protocols.Ipp
{
    using ContentDotNet.Binary;

    /// <summary>
    ///   The IPP header.
    /// </summary>
    public record struct IppHeader
    {
        /// <summary>
        ///   The major version of the IPP.
        /// </summary>
        public byte MajorVersion { get; set; }

        /// <summary>
        ///   The minor version of the IPP.
        /// </summary>
        public byte MinorVersion { get; set; }

        /// <summary>
        ///   The Operation ID.
        /// </summary>
        public ushort OperationId { get; set; }

        /// <summary>
        ///   The Request ID.
        /// </summary>
        public uint RequestId { get; set; }

        /// <summary>
        ///   Reads the IPP header.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        /// <returns>The IPP header.</returns>
        public static IppHeader Read(BinaryReader reader) => new()
        {
            MajorVersion = reader.ReadByte(),
            MinorVersion = reader.ReadByte(),
            OperationId = reader.ReadUInt16(),
            RequestId = reader.ReadUInt16()
        };

        /// <summary>
        ///   Reads the IPP header.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        /// <returns>The IPP header.</returns>
        public static async Task<IppHeader> ReadAsync(BinaryReader reader) => new()
        {
            MajorVersion = await reader.ReadByteAsync(),
            MinorVersion = await reader.ReadByteAsync(),
            OperationId = await reader.ReadUInt16Async(),
            RequestId = await reader.ReadUInt16Async()
        };

        /// <summary>
        ///   Writes the IPP header to <paramref name="writer"/>.
        /// </summary>
        /// <param name="writer">Output where the IPP header is written to.</param>
        public readonly void Write(BinaryWriter writer)
        {
            writer.Write(MajorVersion);
            writer.Write(MinorVersion);
            writer.Write(OperationId);
            writer.Write(RequestId);
        }

        /// <summary>
        ///   Writes the IPP header to <paramref name="writer"/>.
        /// </summary>
        /// <param name="writer">Output where the IPP header is written to.</param>
        public readonly async Task WriteAsync(BinaryWriter writer)
        {
            await writer.WriteAsync(MajorVersion);
            await writer.WriteAsync(MinorVersion);
            await writer.WriteAsync(OperationId);
            await writer.WriteAsync(RequestId);
        }
    }
}
