namespace ContentDotNet.Protocols.Bgp.MessageFormats.Serializers
{
    using ContentDotNet.Protocols.Bgp.Abstractions;
    using ContentDotNet.Protocols.Bgp.MessageFormats.Data;

    // NOTE: This was initially auto-generated, so we basically just took it out of
    // generated code, changed the text template so it doesn't generate this code, and changed
    // the code to fix a few bugs.

    /// <summary>
	///   Represents the BGP message format serializer.
	/// </summary>
	public class BgpNotificationMessageFormatSerializer : IMessageFormatSerializer
    {
        /// <summary>
        ///   Singleton instane of the <see cref="BgpNotificationMessageFormatSerializer" /> class.
        /// </summary>
        public static readonly BgpNotificationMessageFormatSerializer Instance = new();

        /// <inheritdoc cref="IMessageFormatSerializer.TypeOfModel" />
        public Type TypeOfModel => typeof(BgpNotificationMessageFormat);

        /// <inheritdoc cref="IMessageFormatSerializer.Read(BinaryReader)" />
        public IMessageFormatData Read(BinaryReader reader)
        {
            byte _ErrorCode = CommonIOUtilities.Read<byte>(reader);
            byte _ErrorSubCode = CommonIOUtilities.Read<byte>(reader);
            List<byte> _Data = CommonIOUtilities.Read<List<byte>>(reader);
            return new BgpNotificationMessageFormat(_ErrorCode, _ErrorSubCode, _Data);
        }

        /// <inheritdoc cref="IMessageFormatSerializer.ReadAsync(BinaryReader)" />
        public async Task<IMessageFormatData> ReadAsync(BinaryReader reader)
        {
            byte _ErrorCode = await CommonIOUtilities.ReadAsync<byte>(reader);
            byte _ErrorSubCode = await CommonIOUtilities.ReadAsync<byte>(reader);
            List<byte> _Data = await CommonIOUtilities.ReadAsync<List<byte>>(reader);
            return new BgpNotificationMessageFormat(_ErrorCode, _ErrorSubCode, _Data);
        }

        /// <inheritdoc cref="IMessageFormatSerializer.Write(IMessageFormatData, BinaryWriter)" />
        public void Write(IMessageFormatData data, BinaryWriter writer)
        {
            if (data is not BgpNotificationMessageFormat actualData)
                throw new ArgumentException($"Input must be BgpNotificationMessageFormat; it is {data.GetType().FullName}", nameof(data));


            CommonIOUtilities.Write<byte>(actualData.ErrorCode, writer);
            CommonIOUtilities.Write<byte>(actualData.ErrorSubCode, writer);
            CommonIOUtilities.Write<List<byte>>(actualData.Data, writer);
        }

        /// <inheritdoc cref="IMessageFormatSerializer.Write(IMessageFormatData, BinaryWriter)" />
        public async Task WriteAsync(IMessageFormatData data, BinaryWriter writer)
        {
            if (data is not BgpNotificationMessageFormat actualData)
                throw new ArgumentException($"Input must be BgpNotificationMessageFormat; it is {data.GetType().FullName}", nameof(data));


            await CommonIOUtilities.WriteAsync<byte>(actualData.ErrorCode, writer);
            await CommonIOUtilities.WriteAsync<byte>(actualData.ErrorSubCode, writer);
            await CommonIOUtilities.WriteAsync<List<byte>>(actualData.Data, writer);
        }
    }
}
