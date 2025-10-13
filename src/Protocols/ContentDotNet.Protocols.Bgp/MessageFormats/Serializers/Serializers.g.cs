namespace ContentDotNet.Protocols.Bgp.MessageFormats.Serializers
{
	using ContentDotNet.Protocols.Bgp.Abstractions;
	using ContentDotNet.Protocols.Bgp.Models;
	using ContentDotNet.Protocols.Bgp.MessageFormats.Data;

	
	/// <summary>
	///   Represents the BGP message format serializer.
	/// </summary>
	public class BgpMessageHeaderFormatSerializer : IMessageFormatSerializer
	{
		/// <summary>
		///   Singleton instane of the <see cref="BgpMessageHeaderFormatSerializer" /> class.
		/// </summary>
		public static readonly BgpMessageHeaderFormatSerializer Instance = new();

		/// <inheritdoc cref="IMessageFormatSerializer.TypeOfModel" />
		public Type TypeOfModel => typeof(BgpMessageHeaderFormat);

		/// <inheritdoc cref="IMessageFormatSerializer.Read(BinaryReader)" />
		public IMessageFormatData Read(BinaryReader reader)
		{
			List<byte> _Marker = CommonIOUtilities.Read<List<byte>>(reader);
			ushort _Length = CommonIOUtilities.Read<ushort>(reader);
			byte _Type = CommonIOUtilities.Read<byte>(reader);
			return new BgpMessageHeaderFormat(_Marker, _Length, _Type);
		}
		
		/// <inheritdoc cref="IMessageFormatSerializer.ReadAsync(BinaryReader)" />
		public async Task<IMessageFormatData> ReadAsync(BinaryReader reader)
		{
			List<byte> _Marker = await CommonIOUtilities.ReadAsync<List<byte>>(reader);
			ushort _Length = await CommonIOUtilities.ReadAsync<ushort>(reader);
			byte _Type = await CommonIOUtilities.ReadAsync<byte>(reader);
			return new BgpMessageHeaderFormat(_Marker, _Length, _Type);
		}

		/// <inheritdoc cref="IMessageFormatSerializer.Write(IMessageFormatData, BinaryWriter)" />
		public void Write(IMessageFormatData data, BinaryWriter writer)
		{
			if (data is not BgpMessageHeaderFormat actualData)
				throw new ArgumentException($"Input must be BgpMessageHeaderFormat; it is {data.GetType().FullName}", nameof(data));


			CommonIOUtilities.Write<List<byte>>(actualData.Marker, writer);
			CommonIOUtilities.Write<ushort>(actualData.Length, writer);
			CommonIOUtilities.Write<byte>(actualData.Type, writer);
		}

		/// <inheritdoc cref="IMessageFormatSerializer.Write(IMessageFormatData, BinaryWriter)" />
		public async Task WriteAsync(IMessageFormatData data, BinaryWriter writer)
		{
			if (data is not BgpMessageHeaderFormat actualData)
				throw new ArgumentException($"Input must be BgpMessageHeaderFormat; it is {data.GetType().FullName}", nameof(data));


			await CommonIOUtilities.WriteAsync<List<byte>>(actualData.Marker, writer);
			await CommonIOUtilities.WriteAsync<ushort>(actualData.Length, writer);
			await CommonIOUtilities.WriteAsync<byte>(actualData.Type, writer);
		}
	}

	
	/// <summary>
	///   Represents the BGP message format serializer.
	/// </summary>
	public class BgpOpenMessageFormatSerializer : IMessageFormatSerializer
	{
		/// <summary>
		///   Singleton instane of the <see cref="BgpOpenMessageFormatSerializer" /> class.
		/// </summary>
		public static readonly BgpOpenMessageFormatSerializer Instance = new();

		/// <inheritdoc cref="IMessageFormatSerializer.TypeOfModel" />
		public Type TypeOfModel => typeof(BgpOpenMessageFormat);

		/// <inheritdoc cref="IMessageFormatSerializer.Read(BinaryReader)" />
		public IMessageFormatData Read(BinaryReader reader)
		{
			byte _Version = CommonIOUtilities.Read<byte>(reader);
			ushort _MyAutonomousSystem = CommonIOUtilities.Read<ushort>(reader);
			ushort _HoldTime = CommonIOUtilities.Read<ushort>(reader);
			uint _BgpIdentifier = CommonIOUtilities.Read<uint>(reader);
			List<BgpOpenOptionalParameterModel> _OptionalParameters = CommonIOUtilities.Read<List<BgpOpenOptionalParameterModel>>(reader);
			return new BgpOpenMessageFormat(_Version, _MyAutonomousSystem, _HoldTime, _BgpIdentifier, _OptionalParameters);
		}
		
		/// <inheritdoc cref="IMessageFormatSerializer.ReadAsync(BinaryReader)" />
		public async Task<IMessageFormatData> ReadAsync(BinaryReader reader)
		{
			byte _Version = await CommonIOUtilities.ReadAsync<byte>(reader);
			ushort _MyAutonomousSystem = await CommonIOUtilities.ReadAsync<ushort>(reader);
			ushort _HoldTime = await CommonIOUtilities.ReadAsync<ushort>(reader);
			uint _BgpIdentifier = await CommonIOUtilities.ReadAsync<uint>(reader);
			List<BgpOpenOptionalParameterModel> _OptionalParameters = await CommonIOUtilities.ReadAsync<List<BgpOpenOptionalParameterModel>>(reader);
			return new BgpOpenMessageFormat(_Version, _MyAutonomousSystem, _HoldTime, _BgpIdentifier, _OptionalParameters);
		}

		/// <inheritdoc cref="IMessageFormatSerializer.Write(IMessageFormatData, BinaryWriter)" />
		public void Write(IMessageFormatData data, BinaryWriter writer)
		{
			if (data is not BgpOpenMessageFormat actualData)
				throw new ArgumentException($"Input must be BgpOpenMessageFormat; it is {data.GetType().FullName}", nameof(data));


			CommonIOUtilities.Write<byte>(actualData.Version, writer);
			CommonIOUtilities.Write<ushort>(actualData.MyAutonomousSystem, writer);
			CommonIOUtilities.Write<ushort>(actualData.HoldTime, writer);
			CommonIOUtilities.Write<uint>(actualData.BgpIdentifier, writer);
			CommonIOUtilities.Write<List<BgpOpenOptionalParameterModel>>(actualData.OptionalParameters, writer);
		}

		/// <inheritdoc cref="IMessageFormatSerializer.Write(IMessageFormatData, BinaryWriter)" />
		public async Task WriteAsync(IMessageFormatData data, BinaryWriter writer)
		{
			if (data is not BgpOpenMessageFormat actualData)
				throw new ArgumentException($"Input must be BgpOpenMessageFormat; it is {data.GetType().FullName}", nameof(data));


			await CommonIOUtilities.WriteAsync<byte>(actualData.Version, writer);
			await CommonIOUtilities.WriteAsync<ushort>(actualData.MyAutonomousSystem, writer);
			await CommonIOUtilities.WriteAsync<ushort>(actualData.HoldTime, writer);
			await CommonIOUtilities.WriteAsync<uint>(actualData.BgpIdentifier, writer);
			await CommonIOUtilities.WriteAsync<List<BgpOpenOptionalParameterModel>>(actualData.OptionalParameters, writer);
		}
	}

	
	/// <summary>
	///   Represents the BGP message format serializer.
	/// </summary>
	public class BgpUpdateMessageFormatSerializer : IMessageFormatSerializer
	{
		/// <summary>
		///   Singleton instane of the <see cref="BgpUpdateMessageFormatSerializer" /> class.
		/// </summary>
		public static readonly BgpUpdateMessageFormatSerializer Instance = new();

		/// <inheritdoc cref="IMessageFormatSerializer.TypeOfModel" />
		public Type TypeOfModel => typeof(BgpUpdateMessageFormat);

		/// <inheritdoc cref="IMessageFormatSerializer.Read(BinaryReader)" />
		public IMessageFormatData Read(BinaryReader reader)
		{
			ushort _WithdrawnRoutesLength = CommonIOUtilities.Read<ushort>(reader);
			List<string> _WithdrawnRoutes = CommonIOUtilities.Read<List<string>>(reader);
			ushort _TotalPathAttributeLength = CommonIOUtilities.Read<ushort>(reader);
			List<BgpPathAttributeModel> _PathAttributes = CommonIOUtilities.Read<List<BgpPathAttributeModel>>(reader);
			List<string> _NetworkLayerReachabilityInformation = CommonIOUtilities.Read<List<string>>(reader);
			return new BgpUpdateMessageFormat(_WithdrawnRoutesLength, _WithdrawnRoutes, _TotalPathAttributeLength, _PathAttributes, _NetworkLayerReachabilityInformation);
		}
		
		/// <inheritdoc cref="IMessageFormatSerializer.ReadAsync(BinaryReader)" />
		public async Task<IMessageFormatData> ReadAsync(BinaryReader reader)
		{
			ushort _WithdrawnRoutesLength = await CommonIOUtilities.ReadAsync<ushort>(reader);
			List<string> _WithdrawnRoutes = await CommonIOUtilities.ReadAsync<List<string>>(reader);
			ushort _TotalPathAttributeLength = await CommonIOUtilities.ReadAsync<ushort>(reader);
			List<BgpPathAttributeModel> _PathAttributes = await CommonIOUtilities.ReadAsync<List<BgpPathAttributeModel>>(reader);
			List<string> _NetworkLayerReachabilityInformation = await CommonIOUtilities.ReadAsync<List<string>>(reader);
			return new BgpUpdateMessageFormat(_WithdrawnRoutesLength, _WithdrawnRoutes, _TotalPathAttributeLength, _PathAttributes, _NetworkLayerReachabilityInformation);
		}

		/// <inheritdoc cref="IMessageFormatSerializer.Write(IMessageFormatData, BinaryWriter)" />
		public void Write(IMessageFormatData data, BinaryWriter writer)
		{
			if (data is not BgpUpdateMessageFormat actualData)
				throw new ArgumentException($"Input must be BgpUpdateMessageFormat; it is {data.GetType().FullName}", nameof(data));


			CommonIOUtilities.Write<ushort>(actualData.WithdrawnRoutesLength, writer);
			CommonIOUtilities.Write<List<string>>(actualData.WithdrawnRoutes, writer);
			CommonIOUtilities.Write<ushort>(actualData.TotalPathAttributeLength, writer);
			CommonIOUtilities.Write<List<BgpPathAttributeModel>>(actualData.PathAttributes, writer);
			CommonIOUtilities.Write<List<string>>(actualData.NetworkLayerReachabilityInformation, writer);
		}

		/// <inheritdoc cref="IMessageFormatSerializer.Write(IMessageFormatData, BinaryWriter)" />
		public async Task WriteAsync(IMessageFormatData data, BinaryWriter writer)
		{
			if (data is not BgpUpdateMessageFormat actualData)
				throw new ArgumentException($"Input must be BgpUpdateMessageFormat; it is {data.GetType().FullName}", nameof(data));


			await CommonIOUtilities.WriteAsync<ushort>(actualData.WithdrawnRoutesLength, writer);
			await CommonIOUtilities.WriteAsync<List<string>>(actualData.WithdrawnRoutes, writer);
			await CommonIOUtilities.WriteAsync<ushort>(actualData.TotalPathAttributeLength, writer);
			await CommonIOUtilities.WriteAsync<List<BgpPathAttributeModel>>(actualData.PathAttributes, writer);
			await CommonIOUtilities.WriteAsync<List<string>>(actualData.NetworkLayerReachabilityInformation, writer);
		}
	}

	
	/// <summary>
	///   Represents the BGP message format serializer.
	/// </summary>
	public class BgpKeepAliveMessageFormatSerializer : IMessageFormatSerializer
	{
		/// <summary>
		///   Singleton instane of the <see cref="BgpKeepAliveMessageFormatSerializer" /> class.
		/// </summary>
		public static readonly BgpKeepAliveMessageFormatSerializer Instance = new();

		/// <inheritdoc cref="IMessageFormatSerializer.TypeOfModel" />
		public Type TypeOfModel => typeof(BgpKeepAliveMessageFormat);

		/// <inheritdoc cref="IMessageFormatSerializer.Read(BinaryReader)" />
		public IMessageFormatData Read(BinaryReader reader)
		{
			return new BgpKeepAliveMessageFormat();
		}
		
		/// <inheritdoc cref="IMessageFormatSerializer.ReadAsync(BinaryReader)" />
		public async Task<IMessageFormatData> ReadAsync(BinaryReader reader)
		{
			return new BgpKeepAliveMessageFormat();
		}

		/// <inheritdoc cref="IMessageFormatSerializer.Write(IMessageFormatData, BinaryWriter)" />
		public void Write(IMessageFormatData data, BinaryWriter writer)
		{
			if (data is not BgpKeepAliveMessageFormat actualData)
				throw new ArgumentException($"Input must be BgpKeepAliveMessageFormat; it is {data.GetType().FullName}", nameof(data));


		}

		/// <inheritdoc cref="IMessageFormatSerializer.Write(IMessageFormatData, BinaryWriter)" />
		public async Task WriteAsync(IMessageFormatData data, BinaryWriter writer)
		{
			if (data is not BgpKeepAliveMessageFormat actualData)
				throw new ArgumentException($"Input must be BgpKeepAliveMessageFormat; it is {data.GetType().FullName}", nameof(data));


		}
	}

	}

