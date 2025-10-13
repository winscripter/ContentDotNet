namespace ContentDotNet.Protocols.Bgp.MessageFormats.Data
{
	using ContentDotNet.Protocols.Bgp.Abstractions;
	using ContentDotNet.Protocols.Bgp.Models;

	
	/// <summary>
	///   Represents the BGP message format data.
	/// </summary>
	public record BgpMessageHeaderFormat : IMessageFormatData
	{
	
		/// <summary>
		///   Represents the Marker property of this BGP message format data.
		/// </summary>
		public List<byte> Marker { get; set; }

	
		/// <summary>
		///   Represents the Length property of this BGP message format data.
		/// </summary>
		public ushort Length { get; set; }

	
		/// <summary>
		///   Represents the Type property of this BGP message format data.
		/// </summary>
		public byte Type { get; set; }

			
		/// <summary>
		///   Initializes a new instance of the <see cref="BgpMessageHeaderFormat" /> message format record.
		/// </summary>
		/// param name="Marker">One of the message format's parameters.</param>
		/// param name="Length">One of the message format's parameters.</param>
		/// param name="Type">One of the message format's parameters.</param>
		public BgpMessageHeaderFormat(List<byte> marker, ushort length, byte type)
		{
		
			this.Marker = marker;

		
			this.Length = length;

		
			this.Type = type;

				}
	}

	
	/// <summary>
	///   Represents the BGP message format data.
	/// </summary>
	public record BgpOpenMessageFormat : IMessageFormatData
	{
	
		/// <summary>
		///   Represents the Version property of this BGP message format data.
		/// </summary>
		public byte Version { get; set; }

	
		/// <summary>
		///   Represents the MyAutonomousSystem property of this BGP message format data.
		/// </summary>
		public ushort MyAutonomousSystem { get; set; }

	
		/// <summary>
		///   Represents the HoldTime property of this BGP message format data.
		/// </summary>
		public ushort HoldTime { get; set; }

	
		/// <summary>
		///   Represents the BgpIdentifier property of this BGP message format data.
		/// </summary>
		public uint BgpIdentifier { get; set; }

	
		/// <summary>
		///   Represents the OptionalParameters property of this BGP message format data.
		/// </summary>
		public List<BgpOpenOptionalParameterModel> OptionalParameters { get; set; }

			
		/// <summary>
		///   Initializes a new instance of the <see cref="BgpOpenMessageFormat" /> message format record.
		/// </summary>
		/// param name="Version">One of the message format's parameters.</param>
		/// param name="MyAutonomousSystem">One of the message format's parameters.</param>
		/// param name="HoldTime">One of the message format's parameters.</param>
		/// param name="BgpIdentifier">One of the message format's parameters.</param>
		/// param name="OptionalParameters">One of the message format's parameters.</param>
		public BgpOpenMessageFormat(byte version, ushort myAutonomousSystem, ushort holdTime, uint bgpIdentifier, List<BgpOpenOptionalParameterModel> optionalParameters)
		{
		
			this.Version = version;

		
			this.MyAutonomousSystem = myAutonomousSystem;

		
			this.HoldTime = holdTime;

		
			this.BgpIdentifier = bgpIdentifier;

		
			this.OptionalParameters = optionalParameters;

				}
	}

	
	/// <summary>
	///   Represents the BGP message format data.
	/// </summary>
	public record BgpUpdateMessageFormat : IMessageFormatData
	{
	
		/// <summary>
		///   Represents the WithdrawnRoutesLength property of this BGP message format data.
		/// </summary>
		public ushort WithdrawnRoutesLength { get; set; }

	
		/// <summary>
		///   Represents the WithdrawnRoutes property of this BGP message format data.
		/// </summary>
		public List<string> WithdrawnRoutes { get; set; }

	
		/// <summary>
		///   Represents the TotalPathAttributeLength property of this BGP message format data.
		/// </summary>
		public ushort TotalPathAttributeLength { get; set; }

	
		/// <summary>
		///   Represents the PathAttributes property of this BGP message format data.
		/// </summary>
		public List<BgpPathAttributeModel> PathAttributes { get; set; }

	
		/// <summary>
		///   Represents the NetworkLayerReachabilityInformation property of this BGP message format data.
		/// </summary>
		public List<string> NetworkLayerReachabilityInformation { get; set; }

			
		/// <summary>
		///   Initializes a new instance of the <see cref="BgpUpdateMessageFormat" /> message format record.
		/// </summary>
		/// param name="WithdrawnRoutesLength">One of the message format's parameters.</param>
		/// param name="WithdrawnRoutes">One of the message format's parameters.</param>
		/// param name="TotalPathAttributeLength">One of the message format's parameters.</param>
		/// param name="PathAttributes">One of the message format's parameters.</param>
		/// param name="NetworkLayerReachabilityInformation">One of the message format's parameters.</param>
		public BgpUpdateMessageFormat(ushort withdrawnRoutesLength, List<string> withdrawnRoutes, ushort totalPathAttributeLength, List<BgpPathAttributeModel> pathAttributes, List<string> networkLayerReachabilityInformation)
		{
		
			this.WithdrawnRoutesLength = withdrawnRoutesLength;

		
			this.WithdrawnRoutes = withdrawnRoutes;

		
			this.TotalPathAttributeLength = totalPathAttributeLength;

		
			this.PathAttributes = pathAttributes;

		
			this.NetworkLayerReachabilityInformation = networkLayerReachabilityInformation;

				}
	}

	
	/// <summary>
	///   Represents the BGP message format data.
	/// </summary>
	public record BgpKeepAliveMessageFormat : IMessageFormatData
	{
			
		/// <summary>
		///   Initializes a new instance of the <see cref="BgpKeepAliveMessageFormat" /> message format record.
		/// </summary>
		public BgpKeepAliveMessageFormat()
		{
				}
	}

	
	/// <summary>
	///   Represents the BGP message format data.
	/// </summary>
	public record BgpNotificationMessageFormat : IMessageFormatData
	{
	
		/// <summary>
		///   Represents the ErrorCode property of this BGP message format data.
		/// </summary>
		public byte ErrorCode { get; set; }

	
		/// <summary>
		///   Represents the ErrorSubCode property of this BGP message format data.
		/// </summary>
		public byte ErrorSubCode { get; set; }

	
		/// <summary>
		///   Represents the Data property of this BGP message format data.
		/// </summary>
		public List<byte> Data { get; set; }

			
		/// <summary>
		///   Initializes a new instance of the <see cref="BgpNotificationMessageFormat" /> message format record.
		/// </summary>
		/// param name="ErrorCode">One of the message format's parameters.</param>
		/// param name="ErrorSubCode">One of the message format's parameters.</param>
		/// param name="Data">One of the message format's parameters.</param>
		public BgpNotificationMessageFormat(byte errorCode, byte errorSubCode, List<byte> data)
		{
		
			this.ErrorCode = errorCode;

		
			this.ErrorSubCode = errorSubCode;

		
			this.Data = data;

				}
	}

	}

