namespace ContentDotNet.Protocols.Bgp.Abstractions
{
    /// <summary>
    ///   The message format serializer.
    /// </summary>
    public interface IMessageFormatSerializer
    {
        /// <summary>
        ///   Type of the <see cref="IMessageFormatData"/> model.
        /// </summary>
        Type TypeOfModel { get; }

        /// <summary>
        ///   Reads the message format data.
        /// </summary>
        /// <param name="reader">The binary reader to read from.</param>
        /// <returns>The returned message format data.</returns>
        IMessageFormatData Read(BinaryReader reader);

        /// <summary>
        ///   Reads the message format data.
        /// </summary>
        /// <param name="reader">The binary reader to read from.</param>
        /// <returns>The returned message format data.</returns>
        Task<IMessageFormatData> ReadAsync(BinaryReader reader);

        /// <summary>
        ///   Writes the message format data to <paramref name="writer"/>.
        /// </summary>
        /// <param name="data">The input message format data.</param>
        /// <param name="writer">The output binary writer.</param>
        void Write(IMessageFormatData data, BinaryWriter writer);

        /// <summary>
        ///   Writes the message format data to <paramref name="writer"/>.
        /// </summary>
        /// <param name="data">The input message format data.</param>
        /// <param name="writer">The output binary writer.</param>
        Task WriteAsync(IMessageFormatData data, BinaryWriter writer);
    }
}
