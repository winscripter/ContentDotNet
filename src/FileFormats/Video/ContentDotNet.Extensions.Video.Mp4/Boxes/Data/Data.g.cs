#nullable enable

namespace ContentDotNet.Extensions.Video.Mp4.Boxes.Data
{
	using ContentDotNet.Extensions.Video.Mp4.Annotations;
	using System.ComponentModel;


	/// <summary>
	///   The MP4 <c>rssr</c> box data representation.
	/// </summary>
	[FourCC("rssr")]
	public class Mp4ReceivedSsrcBoxData : IMp4BoxData, IEquatable<Mp4ReceivedSsrcBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Ssrc.
		/// </summary>
		public uint? Ssrc { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ReceivedSsrcBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="ssrc">The parameter that assigns <see cref="Ssrc" /> directly.</param>
		public Mp4ReceivedSsrcBoxData(uint ssrc)
		{
			this.Ssrc = ssrc;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ReceivedSsrcBoxData" /> class.
		/// </summary>
		public Mp4ReceivedSsrcBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4ReceivedSsrcBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.Ssrc, val.Ssrc)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Ssrc?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4ReceivedSsrcBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>tssy</c> box data representation.
	/// </summary>
	[FourCC("tssy")]
	public class Mp4TimestampSynchronizationBoxData : IMp4BoxData, IEquatable<Mp4TimestampSynchronizationBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Reserved.
		/// </summary>
		public byte? Reserved { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, TimestampSync.
		/// </summary>
		public byte? TimestampSync { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TimestampSynchronizationBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="reserved">The parameter that assigns <see cref="Reserved" /> directly.</param>
		/// <param name="timestampSync">The parameter that assigns <see cref="TimestampSync" /> directly.</param>
		public Mp4TimestampSynchronizationBoxData(byte reserved, byte timestampSync)
		{
			this.Reserved = reserved;
			this.TimestampSync = timestampSync;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TimestampSynchronizationBoxData" /> class.
		/// </summary>
		public Mp4TimestampSynchronizationBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4TimestampSynchronizationBoxData val
				&& EqualityComparer<byte?>.Default.Equals(this.Reserved, val.Reserved)
				&& EqualityComparer<byte?>.Default.Equals(this.TimestampSync, val.TimestampSync)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Reserved?.GetHashCode() ?? 0);
				hash = hash * 23 + (TimestampSync?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4TimestampSynchronizationBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>tims</c> box data representation.
	/// </summary>
	[FourCC("tims")]
	public class Mp4TimescaleEntryBoxData : IMp4BoxData, IEquatable<Mp4TimescaleEntryBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Timescale.
		/// </summary>
		public uint? Timescale { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TimescaleEntryBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="timescale">The parameter that assigns <see cref="Timescale" /> directly.</param>
		public Mp4TimescaleEntryBoxData(uint timescale)
		{
			this.Timescale = timescale;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TimescaleEntryBoxData" /> class.
		/// </summary>
		public Mp4TimescaleEntryBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4TimescaleEntryBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.Timescale, val.Timescale)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Timescale?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4TimescaleEntryBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>tsro</c> box data representation.
	/// </summary>
	[FourCC("tsro")]
	public class Mp4TimeOffsetBoxData : IMp4BoxData, IEquatable<Mp4TimeOffsetBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Offset.
		/// </summary>
		public uint? Offset { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TimeOffsetBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="offset">The parameter that assigns <see cref="Offset" /> directly.</param>
		public Mp4TimeOffsetBoxData(uint offset)
		{
			this.Offset = offset;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TimeOffsetBoxData" /> class.
		/// </summary>
		public Mp4TimeOffsetBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4TimeOffsetBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.Offset, val.Offset)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Offset?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4TimeOffsetBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>snro</c> box data representation.
	/// </summary>
	[FourCC("snro")]
	public class Mp4SequenceOffsetBoxData : IMp4BoxData, IEquatable<Mp4SequenceOffsetBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Offset.
		/// </summary>
		public uint? Offset { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SequenceOffsetBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="offset">The parameter that assigns <see cref="Offset" /> directly.</param>
		public Mp4SequenceOffsetBoxData(uint offset)
		{
			this.Offset = offset;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SequenceOffsetBoxData" /> class.
		/// </summary>
		public Mp4SequenceOffsetBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4SequenceOffsetBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.Offset, val.Offset)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Offset?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4SequenceOffsetBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>trpy</c> box data representation.
	/// </summary>
	[FourCC("trpy")]
	public class Mp4TotalBytesSentIncluding12ByteRtpHeadersBoxData : IMp4BoxData, IEquatable<Mp4TotalBytesSentIncluding12ByteRtpHeadersBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, BytesSent.
		/// </summary>
		public ulong? BytesSent { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TotalBytesSentIncluding12ByteRtpHeadersBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="bytesSent">The parameter that assigns <see cref="BytesSent" /> directly.</param>
		public Mp4TotalBytesSentIncluding12ByteRtpHeadersBoxData(ulong bytesSent)
		{
			this.BytesSent = bytesSent;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TotalBytesSentIncluding12ByteRtpHeadersBoxData" /> class.
		/// </summary>
		public Mp4TotalBytesSentIncluding12ByteRtpHeadersBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4TotalBytesSentIncluding12ByteRtpHeadersBoxData val
				&& EqualityComparer<ulong?>.Default.Equals(this.BytesSent, val.BytesSent)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (BytesSent?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4TotalBytesSentIncluding12ByteRtpHeadersBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>nump</c> box data representation.
	/// </summary>
	[FourCC("nump")]
	public class Mp4TotalPacketsSentBoxData : IMp4BoxData, IEquatable<Mp4TotalPacketsSentBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, PacketsSent.
		/// </summary>
		public ulong? PacketsSent { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TotalPacketsSentBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="packetsSent">The parameter that assigns <see cref="PacketsSent" /> directly.</param>
		public Mp4TotalPacketsSentBoxData(ulong packetsSent)
		{
			this.PacketsSent = packetsSent;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TotalPacketsSentBoxData" /> class.
		/// </summary>
		public Mp4TotalPacketsSentBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4TotalPacketsSentBoxData val
				&& EqualityComparer<ulong?>.Default.Equals(this.PacketsSent, val.PacketsSent)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (PacketsSent?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4TotalPacketsSentBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>tpyl</c> box data representation.
	/// </summary>
	[FourCC("tpyl")]
	public class Mp4TotalBytesSentNotIncludingRtpHeadersBoxData : IMp4BoxData, IEquatable<Mp4TotalBytesSentNotIncludingRtpHeadersBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, BytesSent.
		/// </summary>
		public ulong? BytesSent { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TotalBytesSentNotIncludingRtpHeadersBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="bytesSent">The parameter that assigns <see cref="BytesSent" /> directly.</param>
		public Mp4TotalBytesSentNotIncludingRtpHeadersBoxData(ulong bytesSent)
		{
			this.BytesSent = bytesSent;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TotalBytesSentNotIncludingRtpHeadersBoxData" /> class.
		/// </summary>
		public Mp4TotalBytesSentNotIncludingRtpHeadersBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4TotalBytesSentNotIncludingRtpHeadersBoxData val
				&& EqualityComparer<ulong?>.Default.Equals(this.BytesSent, val.BytesSent)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (BytesSent?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4TotalBytesSentNotIncludingRtpHeadersBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>totl</c> box data representation.
	/// </summary>
	[FourCC("totl")]
	public class Mp4TotalBytesSentIncluding12ByteRtpHeaders32BoxData : IMp4BoxData, IEquatable<Mp4TotalBytesSentIncluding12ByteRtpHeaders32BoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, BytesSent.
		/// </summary>
		public uint? BytesSent { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TotalBytesSentIncluding12ByteRtpHeaders32BoxData" /> class with the specified value.
		/// </summary>
		/// <param name="bytesSent">The parameter that assigns <see cref="BytesSent" /> directly.</param>
		public Mp4TotalBytesSentIncluding12ByteRtpHeaders32BoxData(uint bytesSent)
		{
			this.BytesSent = bytesSent;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TotalBytesSentIncluding12ByteRtpHeaders32BoxData" /> class.
		/// </summary>
		public Mp4TotalBytesSentIncluding12ByteRtpHeaders32BoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4TotalBytesSentIncluding12ByteRtpHeaders32BoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.BytesSent, val.BytesSent)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (BytesSent?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4TotalBytesSentIncluding12ByteRtpHeaders32BoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>npck</c> box data representation.
	/// </summary>
	[FourCC("npck")]
	public class Mp4TotalPacketsSent32BoxData : IMp4BoxData, IEquatable<Mp4TotalPacketsSent32BoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, PacketsSent.
		/// </summary>
		public uint? PacketsSent { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TotalPacketsSent32BoxData" /> class with the specified value.
		/// </summary>
		/// <param name="packetsSent">The parameter that assigns <see cref="PacketsSent" /> directly.</param>
		public Mp4TotalPacketsSent32BoxData(uint packetsSent)
		{
			this.PacketsSent = packetsSent;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TotalPacketsSent32BoxData" /> class.
		/// </summary>
		public Mp4TotalPacketsSent32BoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4TotalPacketsSent32BoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.PacketsSent, val.PacketsSent)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (PacketsSent?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4TotalPacketsSent32BoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>tpay</c> box data representation.
	/// </summary>
	[FourCC("tpay")]
	public class Mp4TotalBytesSentNotIncludingRtpHeaders32BoxData : IMp4BoxData, IEquatable<Mp4TotalBytesSentNotIncludingRtpHeaders32BoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, BytesSent.
		/// </summary>
		public uint? BytesSent { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TotalBytesSentNotIncludingRtpHeaders32BoxData" /> class with the specified value.
		/// </summary>
		/// <param name="bytesSent">The parameter that assigns <see cref="BytesSent" /> directly.</param>
		public Mp4TotalBytesSentNotIncludingRtpHeaders32BoxData(uint bytesSent)
		{
			this.BytesSent = bytesSent;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TotalBytesSentNotIncludingRtpHeaders32BoxData" /> class.
		/// </summary>
		public Mp4TotalBytesSentNotIncludingRtpHeaders32BoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4TotalBytesSentNotIncludingRtpHeaders32BoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.BytesSent, val.BytesSent)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (BytesSent?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4TotalBytesSentNotIncludingRtpHeaders32BoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>maxr</c> box data representation.
	/// </summary>
	[FourCC("maxr")]
	public class Mp4MaximumDataRateBoxData : IMp4BoxData, IEquatable<Mp4MaximumDataRateBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Period.
		/// </summary>
		public uint? Period { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Bytes.
		/// </summary>
		public uint? Bytes { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4MaximumDataRateBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="period">The parameter that assigns <see cref="Period" /> directly.</param>
		/// <param name="bytes">The parameter that assigns <see cref="Bytes" /> directly.</param>
		public Mp4MaximumDataRateBoxData(uint period, uint bytes)
		{
			this.Period = period;
			this.Bytes = bytes;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4MaximumDataRateBoxData" /> class.
		/// </summary>
		public Mp4MaximumDataRateBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4MaximumDataRateBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.Period, val.Period)
				&& EqualityComparer<uint?>.Default.Equals(this.Bytes, val.Bytes)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Period?.GetHashCode() ?? 0);
				hash = hash * 23 + (Bytes?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4MaximumDataRateBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>dmed</c> box data representation.
	/// </summary>
	[FourCC("dmed")]
	public class Mp4TotalBytesSentFromMediaTracksBoxData : IMp4BoxData, IEquatable<Mp4TotalBytesSentFromMediaTracksBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, BytesSent.
		/// </summary>
		public ulong? BytesSent { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TotalBytesSentFromMediaTracksBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="bytesSent">The parameter that assigns <see cref="BytesSent" /> directly.</param>
		public Mp4TotalBytesSentFromMediaTracksBoxData(ulong bytesSent)
		{
			this.BytesSent = bytesSent;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TotalBytesSentFromMediaTracksBoxData" /> class.
		/// </summary>
		public Mp4TotalBytesSentFromMediaTracksBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4TotalBytesSentFromMediaTracksBoxData val
				&& EqualityComparer<ulong?>.Default.Equals(this.BytesSent, val.BytesSent)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (BytesSent?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4TotalBytesSentFromMediaTracksBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>dimm</c> box data representation.
	/// </summary>
	[FourCC("dimm")]
	public class Mp4TotalBytesSentImmediateModeBoxData : IMp4BoxData, IEquatable<Mp4TotalBytesSentImmediateModeBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, BytesSent.
		/// </summary>
		public ulong? BytesSent { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TotalBytesSentImmediateModeBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="bytesSent">The parameter that assigns <see cref="BytesSent" /> directly.</param>
		public Mp4TotalBytesSentImmediateModeBoxData(ulong bytesSent)
		{
			this.BytesSent = bytesSent;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TotalBytesSentImmediateModeBoxData" /> class.
		/// </summary>
		public Mp4TotalBytesSentImmediateModeBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4TotalBytesSentImmediateModeBoxData val
				&& EqualityComparer<ulong?>.Default.Equals(this.BytesSent, val.BytesSent)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (BytesSent?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4TotalBytesSentImmediateModeBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>drep</c> box data representation.
	/// </summary>
	[FourCC("drep")]
	public class Mp4TotalBytesInRepeatedPacketsBoxData : IMp4BoxData, IEquatable<Mp4TotalBytesInRepeatedPacketsBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, BytesSent.
		/// </summary>
		public ulong? BytesSent { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TotalBytesInRepeatedPacketsBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="bytesSent">The parameter that assigns <see cref="BytesSent" /> directly.</param>
		public Mp4TotalBytesInRepeatedPacketsBoxData(ulong bytesSent)
		{
			this.BytesSent = bytesSent;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TotalBytesInRepeatedPacketsBoxData" /> class.
		/// </summary>
		public Mp4TotalBytesInRepeatedPacketsBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4TotalBytesInRepeatedPacketsBoxData val
				&& EqualityComparer<ulong?>.Default.Equals(this.BytesSent, val.BytesSent)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (BytesSent?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4TotalBytesInRepeatedPacketsBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>tmin</c> box data representation.
	/// </summary>
	[FourCC("tmin")]
	public class Mp4SmallestRelativeTransmittionTimeMillisecondsBoxData : IMp4BoxData, IEquatable<Mp4SmallestRelativeTransmittionTimeMillisecondsBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Time.
		/// </summary>
		public uint? Time { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SmallestRelativeTransmittionTimeMillisecondsBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="time">The parameter that assigns <see cref="Time" /> directly.</param>
		public Mp4SmallestRelativeTransmittionTimeMillisecondsBoxData(uint time)
		{
			this.Time = time;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SmallestRelativeTransmittionTimeMillisecondsBoxData" /> class.
		/// </summary>
		public Mp4SmallestRelativeTransmittionTimeMillisecondsBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4SmallestRelativeTransmittionTimeMillisecondsBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.Time, val.Time)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Time?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4SmallestRelativeTransmittionTimeMillisecondsBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>tmax</c> box data representation.
	/// </summary>
	[FourCC("tmax")]
	public class Mp4LargestRelativeTransmittionTimeMillisecondsBoxData : IMp4BoxData, IEquatable<Mp4LargestRelativeTransmittionTimeMillisecondsBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Time.
		/// </summary>
		public uint? Time { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4LargestRelativeTransmittionTimeMillisecondsBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="time">The parameter that assigns <see cref="Time" /> directly.</param>
		public Mp4LargestRelativeTransmittionTimeMillisecondsBoxData(uint time)
		{
			this.Time = time;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4LargestRelativeTransmittionTimeMillisecondsBoxData" /> class.
		/// </summary>
		public Mp4LargestRelativeTransmittionTimeMillisecondsBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4LargestRelativeTransmittionTimeMillisecondsBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.Time, val.Time)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Time?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4LargestRelativeTransmittionTimeMillisecondsBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>pmax</c> box data representation.
	/// </summary>
	[FourCC("pmax")]
	public class Mp4LargestPacketSentIncludingRtpHeaderBoxData : IMp4BoxData, IEquatable<Mp4LargestPacketSentIncludingRtpHeaderBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Bytes.
		/// </summary>
		public uint? Bytes { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4LargestPacketSentIncludingRtpHeaderBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="bytes">The parameter that assigns <see cref="Bytes" /> directly.</param>
		public Mp4LargestPacketSentIncludingRtpHeaderBoxData(uint bytes)
		{
			this.Bytes = bytes;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4LargestPacketSentIncludingRtpHeaderBoxData" /> class.
		/// </summary>
		public Mp4LargestPacketSentIncludingRtpHeaderBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4LargestPacketSentIncludingRtpHeaderBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.Bytes, val.Bytes)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Bytes?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4LargestPacketSentIncludingRtpHeaderBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>dmax</c> box data representation.
	/// </summary>
	[FourCC("dmax")]
	public class Mp4LongestPacketDurationInMillisecondsBoxData : IMp4BoxData, IEquatable<Mp4LongestPacketDurationInMillisecondsBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Time.
		/// </summary>
		public uint? Time { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4LongestPacketDurationInMillisecondsBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="time">The parameter that assigns <see cref="Time" /> directly.</param>
		public Mp4LongestPacketDurationInMillisecondsBoxData(uint time)
		{
			this.Time = time;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4LongestPacketDurationInMillisecondsBoxData" /> class.
		/// </summary>
		public Mp4LongestPacketDurationInMillisecondsBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4LongestPacketDurationInMillisecondsBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.Time, val.Time)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Time?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4LongestPacketDurationInMillisecondsBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>payt</c> box data representation.
	/// </summary>
	[FourCC("payt")]
	public class Mp4PayloadIdUsedInRtpPacketsBoxData : IMp4BoxData, IEquatable<Mp4PayloadIdUsedInRtpPacketsBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, PayloadId.
		/// </summary>
		public uint? PayloadId { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4PayloadIdUsedInRtpPacketsBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="payloadId">The parameter that assigns <see cref="PayloadId" /> directly.</param>
		public Mp4PayloadIdUsedInRtpPacketsBoxData(uint payloadId)
		{
			this.PayloadId = payloadId;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4PayloadIdUsedInRtpPacketsBoxData" /> class.
		/// </summary>
		public Mp4PayloadIdUsedInRtpPacketsBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4PayloadIdUsedInRtpPacketsBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.PayloadId, val.PayloadId)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (PayloadId?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4PayloadIdUsedInRtpPacketsBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>stvi</c> box data representation.
	/// </summary>
	[FourCC("stvi")]
	public class Mp4StereoVideoInfoBoxData : IMp4FullBoxData, IEquatable<Mp4StereoVideoInfoBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Reserved.
		/// </summary>
		public uint? Reserved { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, SingleViewAllowed.
		/// </summary>
		public uint? SingleViewAllowed { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, StereoScheme.
		/// </summary>
		public uint? StereoScheme { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Length.
		/// </summary>
		public uint? Length { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, StereoIndicationType.
		/// </summary>
		public byte[]? StereoIndicationType { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, AnyBox.
		/// </summary>
		public IList<Mp4Box>? AnyBox { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4StereoVideoInfoBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="reserved">The parameter that assigns <see cref="Reserved" /> directly.</param>
		/// <param name="singleViewAllowed">The parameter that assigns <see cref="SingleViewAllowed" /> directly.</param>
		/// <param name="stereoScheme">The parameter that assigns <see cref="StereoScheme" /> directly.</param>
		/// <param name="length">The parameter that assigns <see cref="Length" /> directly.</param>
		/// <param name="stereoIndicationType">The parameter that assigns <see cref="StereoIndicationType" /> directly.</param>
		/// <param name="anyBox">The parameter that assigns <see cref="AnyBox" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4StereoVideoInfoBoxData(uint reserved, uint singleViewAllowed, uint stereoScheme, uint length, byte[] stereoIndicationType, IList<Mp4Box> anyBox, byte version, uint flags)
		{
			this.Reserved = reserved;
			this.SingleViewAllowed = singleViewAllowed;
			this.StereoScheme = stereoScheme;
			this.Length = length;
			this.StereoIndicationType = stereoIndicationType;
			this.AnyBox = anyBox;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4StereoVideoInfoBoxData" /> class.
		/// </summary>
		public Mp4StereoVideoInfoBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4StereoVideoInfoBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.Reserved, val.Reserved)
				&& EqualityComparer<uint?>.Default.Equals(this.SingleViewAllowed, val.SingleViewAllowed)
				&& EqualityComparer<uint?>.Default.Equals(this.StereoScheme, val.StereoScheme)
				&& EqualityComparer<uint?>.Default.Equals(this.Length, val.Length)
				&& EqualityComparer<byte[]?>.Default.Equals(this.StereoIndicationType, val.StereoIndicationType)
				&& EqualityComparer<IList<Mp4Box>?>.Default.Equals(this.AnyBox, val.AnyBox)
				&& EqualityComparer<byte?>.Default.Equals(this.Version, val.Version)
				&& EqualityComparer<uint?>.Default.Equals(this.Flags, val.Flags)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Reserved?.GetHashCode() ?? 0);
				hash = hash * 23 + (SingleViewAllowed?.GetHashCode() ?? 0);
				hash = hash * 23 + (StereoScheme?.GetHashCode() ?? 0);
				hash = hash * 23 + (Length?.GetHashCode() ?? 0);
				hash = hash * 23 + (StereoIndicationType?.GetHashCode() ?? 0);
				hash = hash * 23 + (AnyBox?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4StereoVideoInfoBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>elng</c> box data representation.
	/// </summary>
	[FourCC("elng")]
	public class Mp4ExtendedLanguageBoxData : IMp4FullBoxData, IEquatable<Mp4ExtendedLanguageBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, ExtendedLanguage.
		/// </summary>
		public string? ExtendedLanguage { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ExtendedLanguageBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="extendedLanguage">The parameter that assigns <see cref="ExtendedLanguage" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4ExtendedLanguageBoxData(string extendedLanguage, byte version, uint flags)
		{
			this.ExtendedLanguage = extendedLanguage;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ExtendedLanguageBoxData" /> class.
		/// </summary>
		public Mp4ExtendedLanguageBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4ExtendedLanguageBoxData val
				&& EqualityComparer<string?>.Default.Equals(this.ExtendedLanguage, val.ExtendedLanguage)
				&& EqualityComparer<byte?>.Default.Equals(this.Version, val.Version)
				&& EqualityComparer<uint?>.Default.Equals(this.Flags, val.Flags)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (ExtendedLanguage?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4ExtendedLanguageBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>btrt</c> box data representation.
	/// </summary>
	[FourCC("btrt")]
	public class Mp4BitRateInformationBoxData : IMp4BoxData, IEquatable<Mp4BitRateInformationBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, BufferSizeDb.
		/// </summary>
		public uint? BufferSizeDb { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, MaxBitRate.
		/// </summary>
		public uint? MaxBitRate { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, AverageBitRate.
		/// </summary>
		public uint? AverageBitRate { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4BitRateInformationBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="bufferSizeDb">The parameter that assigns <see cref="BufferSizeDb" /> directly.</param>
		/// <param name="maxBitRate">The parameter that assigns <see cref="MaxBitRate" /> directly.</param>
		/// <param name="averageBitRate">The parameter that assigns <see cref="AverageBitRate" /> directly.</param>
		public Mp4BitRateInformationBoxData(uint bufferSizeDb, uint maxBitRate, uint averageBitRate)
		{
			this.BufferSizeDb = bufferSizeDb;
			this.MaxBitRate = maxBitRate;
			this.AverageBitRate = averageBitRate;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4BitRateInformationBoxData" /> class.
		/// </summary>
		public Mp4BitRateInformationBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4BitRateInformationBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.BufferSizeDb, val.BufferSizeDb)
				&& EqualityComparer<uint?>.Default.Equals(this.MaxBitRate, val.MaxBitRate)
				&& EqualityComparer<uint?>.Default.Equals(this.AverageBitRate, val.AverageBitRate)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (BufferSizeDb?.GetHashCode() ?? 0);
				hash = hash * 23 + (MaxBitRate?.GetHashCode() ?? 0);
				hash = hash * 23 + (AverageBitRate?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4BitRateInformationBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>pasp</c> box data representation.
	/// </summary>
	[FourCC("pasp")]
	public class Mp4PixelAspectRatioBoxData : IMp4BoxData, IEquatable<Mp4PixelAspectRatioBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, HSpacing.
		/// </summary>
		public uint? HSpacing { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, VSpacing.
		/// </summary>
		public uint? VSpacing { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4PixelAspectRatioBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="hSpacing">The parameter that assigns <see cref="HSpacing" /> directly.</param>
		/// <param name="vSpacing">The parameter that assigns <see cref="VSpacing" /> directly.</param>
		public Mp4PixelAspectRatioBoxData(uint hSpacing, uint vSpacing)
		{
			this.HSpacing = hSpacing;
			this.VSpacing = vSpacing;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4PixelAspectRatioBoxData" /> class.
		/// </summary>
		public Mp4PixelAspectRatioBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4PixelAspectRatioBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.HSpacing, val.HSpacing)
				&& EqualityComparer<uint?>.Default.Equals(this.VSpacing, val.VSpacing)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (HSpacing?.GetHashCode() ?? 0);
				hash = hash * 23 + (VSpacing?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4PixelAspectRatioBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>clap</c> box data representation.
	/// </summary>
	[FourCC("clap")]
	public class Mp4CleanApertureBoxData : IMp4BoxData, IEquatable<Mp4CleanApertureBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, CleanApertureWidthN.
		/// </summary>
		public uint? CleanApertureWidthN { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, CleanApertureWidthD.
		/// </summary>
		public uint? CleanApertureWidthD { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, CleanApertureHeightN.
		/// </summary>
		public uint? CleanApertureHeightN { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, CleanApertureHeightD.
		/// </summary>
		public uint? CleanApertureHeightD { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, HorizOffN.
		/// </summary>
		public uint? HorizOffN { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, HorizOffD.
		/// </summary>
		public uint? HorizOffD { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, VertOffN.
		/// </summary>
		public uint? VertOffN { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, VertOffD.
		/// </summary>
		public uint? VertOffD { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4CleanApertureBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="cleanApertureWidthN">The parameter that assigns <see cref="CleanApertureWidthN" /> directly.</param>
		/// <param name="cleanApertureWidthD">The parameter that assigns <see cref="CleanApertureWidthD" /> directly.</param>
		/// <param name="cleanApertureHeightN">The parameter that assigns <see cref="CleanApertureHeightN" /> directly.</param>
		/// <param name="cleanApertureHeightD">The parameter that assigns <see cref="CleanApertureHeightD" /> directly.</param>
		/// <param name="horizOffN">The parameter that assigns <see cref="HorizOffN" /> directly.</param>
		/// <param name="horizOffD">The parameter that assigns <see cref="HorizOffD" /> directly.</param>
		/// <param name="vertOffN">The parameter that assigns <see cref="VertOffN" /> directly.</param>
		/// <param name="vertOffD">The parameter that assigns <see cref="VertOffD" /> directly.</param>
		public Mp4CleanApertureBoxData(uint cleanApertureWidthN, uint cleanApertureWidthD, uint cleanApertureHeightN, uint cleanApertureHeightD, uint horizOffN, uint horizOffD, uint vertOffN, uint vertOffD)
		{
			this.CleanApertureWidthN = cleanApertureWidthN;
			this.CleanApertureWidthD = cleanApertureWidthD;
			this.CleanApertureHeightN = cleanApertureHeightN;
			this.CleanApertureHeightD = cleanApertureHeightD;
			this.HorizOffN = horizOffN;
			this.HorizOffD = horizOffD;
			this.VertOffN = vertOffN;
			this.VertOffD = vertOffD;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4CleanApertureBoxData" /> class.
		/// </summary>
		public Mp4CleanApertureBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4CleanApertureBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.CleanApertureWidthN, val.CleanApertureWidthN)
				&& EqualityComparer<uint?>.Default.Equals(this.CleanApertureWidthD, val.CleanApertureWidthD)
				&& EqualityComparer<uint?>.Default.Equals(this.CleanApertureHeightN, val.CleanApertureHeightN)
				&& EqualityComparer<uint?>.Default.Equals(this.CleanApertureHeightD, val.CleanApertureHeightD)
				&& EqualityComparer<uint?>.Default.Equals(this.HorizOffN, val.HorizOffN)
				&& EqualityComparer<uint?>.Default.Equals(this.HorizOffD, val.HorizOffD)
				&& EqualityComparer<uint?>.Default.Equals(this.VertOffN, val.VertOffN)
				&& EqualityComparer<uint?>.Default.Equals(this.VertOffD, val.VertOffD)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (CleanApertureWidthN?.GetHashCode() ?? 0);
				hash = hash * 23 + (CleanApertureWidthD?.GetHashCode() ?? 0);
				hash = hash * 23 + (CleanApertureHeightN?.GetHashCode() ?? 0);
				hash = hash * 23 + (CleanApertureHeightD?.GetHashCode() ?? 0);
				hash = hash * 23 + (HorizOffN?.GetHashCode() ?? 0);
				hash = hash * 23 + (HorizOffD?.GetHashCode() ?? 0);
				hash = hash * 23 + (VertOffN?.GetHashCode() ?? 0);
				hash = hash * 23 + (VertOffD?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4CleanApertureBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>cclv</c> box data representation.
	/// </summary>
	[FourCC("cclv")]
	public class Mp4ContentColourVolumeBoxData : IMp4BoxData, IEquatable<Mp4ContentColourVolumeBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Reserved1.
		/// </summary>
		public bool? Reserved1 { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Reserved2.
		/// </summary>
		public bool? Reserved2 { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, CcvPrimariesPresentFlag.
		/// </summary>
		public bool? CcvPrimariesPresentFlag { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, CcvMinLuminanceValuePresentFlag.
		/// </summary>
		public bool? CcvMinLuminanceValuePresentFlag { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, CcvMaxLuminanceValuePresentFlag.
		/// </summary>
		public bool? CcvMaxLuminanceValuePresentFlag { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, CcvAvgLuminanceValuePresentFlag.
		/// </summary>
		public bool? CcvAvgLuminanceValuePresentFlag { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, CcvReservedZero2Bits.
		/// </summary>
		public uint? CcvReservedZero2Bits { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, CcvPrimariesX.
		/// </summary>
		public IList<uint>? CcvPrimariesX { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, CcvPrimariesY.
		/// </summary>
		public IList<uint>? CcvPrimariesY { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, CcvMinLuminanceValue.
		/// </summary>
		public uint? CcvMinLuminanceValue { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, CcvMaxLuminanceValue.
		/// </summary>
		public uint? CcvMaxLuminanceValue { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, CcvAvgLuminanceValue.
		/// </summary>
		public uint? CcvAvgLuminanceValue { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ContentColourVolumeBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="reserved1">The parameter that assigns <see cref="Reserved1" /> directly.</param>
		/// <param name="reserved2">The parameter that assigns <see cref="Reserved2" /> directly.</param>
		/// <param name="ccvPrimariesPresentFlag">The parameter that assigns <see cref="CcvPrimariesPresentFlag" /> directly.</param>
		/// <param name="ccvMinLuminanceValuePresentFlag">The parameter that assigns <see cref="CcvMinLuminanceValuePresentFlag" /> directly.</param>
		/// <param name="ccvMaxLuminanceValuePresentFlag">The parameter that assigns <see cref="CcvMaxLuminanceValuePresentFlag" /> directly.</param>
		/// <param name="ccvAvgLuminanceValuePresentFlag">The parameter that assigns <see cref="CcvAvgLuminanceValuePresentFlag" /> directly.</param>
		/// <param name="ccvReservedZero2Bits">The parameter that assigns <see cref="CcvReservedZero2Bits" /> directly.</param>
		/// <param name="ccvPrimariesX">The parameter that assigns <see cref="CcvPrimariesX" /> directly.</param>
		/// <param name="ccvPrimariesY">The parameter that assigns <see cref="CcvPrimariesY" /> directly.</param>
		/// <param name="ccvMinLuminanceValue">The parameter that assigns <see cref="CcvMinLuminanceValue" /> directly.</param>
		/// <param name="ccvMaxLuminanceValue">The parameter that assigns <see cref="CcvMaxLuminanceValue" /> directly.</param>
		/// <param name="ccvAvgLuminanceValue">The parameter that assigns <see cref="CcvAvgLuminanceValue" /> directly.</param>
		public Mp4ContentColourVolumeBoxData(bool reserved1, bool reserved2, bool ccvPrimariesPresentFlag, bool ccvMinLuminanceValuePresentFlag, bool ccvMaxLuminanceValuePresentFlag, bool ccvAvgLuminanceValuePresentFlag, uint ccvReservedZero2Bits, IList<uint> ccvPrimariesX, IList<uint> ccvPrimariesY, uint ccvMinLuminanceValue, uint ccvMaxLuminanceValue, uint ccvAvgLuminanceValue)
		{
			this.Reserved1 = reserved1;
			this.Reserved2 = reserved2;
			this.CcvPrimariesPresentFlag = ccvPrimariesPresentFlag;
			this.CcvMinLuminanceValuePresentFlag = ccvMinLuminanceValuePresentFlag;
			this.CcvMaxLuminanceValuePresentFlag = ccvMaxLuminanceValuePresentFlag;
			this.CcvAvgLuminanceValuePresentFlag = ccvAvgLuminanceValuePresentFlag;
			this.CcvReservedZero2Bits = ccvReservedZero2Bits;
			this.CcvPrimariesX = ccvPrimariesX;
			this.CcvPrimariesY = ccvPrimariesY;
			this.CcvMinLuminanceValue = ccvMinLuminanceValue;
			this.CcvMaxLuminanceValue = ccvMaxLuminanceValue;
			this.CcvAvgLuminanceValue = ccvAvgLuminanceValue;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ContentColourVolumeBoxData" /> class.
		/// </summary>
		public Mp4ContentColourVolumeBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4ContentColourVolumeBoxData val
				&& EqualityComparer<bool?>.Default.Equals(this.Reserved1, val.Reserved1)
				&& EqualityComparer<bool?>.Default.Equals(this.Reserved2, val.Reserved2)
				&& EqualityComparer<bool?>.Default.Equals(this.CcvPrimariesPresentFlag, val.CcvPrimariesPresentFlag)
				&& EqualityComparer<bool?>.Default.Equals(this.CcvMinLuminanceValuePresentFlag, val.CcvMinLuminanceValuePresentFlag)
				&& EqualityComparer<bool?>.Default.Equals(this.CcvMaxLuminanceValuePresentFlag, val.CcvMaxLuminanceValuePresentFlag)
				&& EqualityComparer<bool?>.Default.Equals(this.CcvAvgLuminanceValuePresentFlag, val.CcvAvgLuminanceValuePresentFlag)
				&& EqualityComparer<uint?>.Default.Equals(this.CcvReservedZero2Bits, val.CcvReservedZero2Bits)
				&& EqualityComparer<IList<uint>?>.Default.Equals(this.CcvPrimariesX, val.CcvPrimariesX)
				&& EqualityComparer<IList<uint>?>.Default.Equals(this.CcvPrimariesY, val.CcvPrimariesY)
				&& EqualityComparer<uint?>.Default.Equals(this.CcvMinLuminanceValue, val.CcvMinLuminanceValue)
				&& EqualityComparer<uint?>.Default.Equals(this.CcvMaxLuminanceValue, val.CcvMaxLuminanceValue)
				&& EqualityComparer<uint?>.Default.Equals(this.CcvAvgLuminanceValue, val.CcvAvgLuminanceValue)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Reserved1?.GetHashCode() ?? 0);
				hash = hash * 23 + (Reserved2?.GetHashCode() ?? 0);
				hash = hash * 23 + (CcvPrimariesPresentFlag?.GetHashCode() ?? 0);
				hash = hash * 23 + (CcvMinLuminanceValuePresentFlag?.GetHashCode() ?? 0);
				hash = hash * 23 + (CcvMaxLuminanceValuePresentFlag?.GetHashCode() ?? 0);
				hash = hash * 23 + (CcvAvgLuminanceValuePresentFlag?.GetHashCode() ?? 0);
				hash = hash * 23 + (CcvReservedZero2Bits?.GetHashCode() ?? 0);
				hash = hash * 23 + (CcvPrimariesX?.GetHashCode() ?? 0);
				hash = hash * 23 + (CcvPrimariesY?.GetHashCode() ?? 0);
				hash = hash * 23 + (CcvMinLuminanceValue?.GetHashCode() ?? 0);
				hash = hash * 23 + (CcvMaxLuminanceValue?.GetHashCode() ?? 0);
				hash = hash * 23 + (CcvAvgLuminanceValue?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4ContentColourVolumeBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>colr</c> box data representation.
	/// </summary>
	[FourCC("colr")]
	public class Mp4ColorInformationBoxData : IMp4BoxData, IEquatable<Mp4ColorInformationBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, ColourType.
		/// </summary>
		public uint? ColourType { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, ColourPrimaries.
		/// </summary>
		public ushort? ColourPrimaries { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, TransferCharacteristics.
		/// </summary>
		public ushort? TransferCharacteristics { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, MatrixCoefficients.
		/// </summary>
		public ushort? MatrixCoefficients { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, FullRangeFlag.
		/// </summary>
		public bool? FullRangeFlag { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Reserved.
		/// </summary>
		public uint? Reserved { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ColorInformationBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="colourType">The parameter that assigns <see cref="ColourType" /> directly.</param>
		/// <param name="colourPrimaries">The parameter that assigns <see cref="ColourPrimaries" /> directly.</param>
		/// <param name="transferCharacteristics">The parameter that assigns <see cref="TransferCharacteristics" /> directly.</param>
		/// <param name="matrixCoefficients">The parameter that assigns <see cref="MatrixCoefficients" /> directly.</param>
		/// <param name="fullRangeFlag">The parameter that assigns <see cref="FullRangeFlag" /> directly.</param>
		/// <param name="reserved">The parameter that assigns <see cref="Reserved" /> directly.</param>
		public Mp4ColorInformationBoxData(uint colourType, ushort colourPrimaries, ushort transferCharacteristics, ushort matrixCoefficients, bool fullRangeFlag, uint reserved)
		{
			this.ColourType = colourType;
			this.ColourPrimaries = colourPrimaries;
			this.TransferCharacteristics = transferCharacteristics;
			this.MatrixCoefficients = matrixCoefficients;
			this.FullRangeFlag = fullRangeFlag;
			this.Reserved = reserved;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ColorInformationBoxData" /> class.
		/// </summary>
		public Mp4ColorInformationBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4ColorInformationBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.ColourType, val.ColourType)
				&& EqualityComparer<ushort?>.Default.Equals(this.ColourPrimaries, val.ColourPrimaries)
				&& EqualityComparer<ushort?>.Default.Equals(this.TransferCharacteristics, val.TransferCharacteristics)
				&& EqualityComparer<ushort?>.Default.Equals(this.MatrixCoefficients, val.MatrixCoefficients)
				&& EqualityComparer<bool?>.Default.Equals(this.FullRangeFlag, val.FullRangeFlag)
				&& EqualityComparer<uint?>.Default.Equals(this.Reserved, val.Reserved)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (ColourType?.GetHashCode() ?? 0);
				hash = hash * 23 + (ColourPrimaries?.GetHashCode() ?? 0);
				hash = hash * 23 + (TransferCharacteristics?.GetHashCode() ?? 0);
				hash = hash * 23 + (MatrixCoefficients?.GetHashCode() ?? 0);
				hash = hash * 23 + (FullRangeFlag?.GetHashCode() ?? 0);
				hash = hash * 23 + (Reserved?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4ColorInformationBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>clli</c> box data representation.
	/// </summary>
	[FourCC("clli")]
	public class Mp4ContentLightLevelBoxData : IMp4BoxData, IEquatable<Mp4ContentLightLevelBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, MaxContentLightLevel.
		/// </summary>
		public ushort? MaxContentLightLevel { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, MaxPicAverageLightLevel.
		/// </summary>
		public ushort? MaxPicAverageLightLevel { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ContentLightLevelBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="maxContentLightLevel">The parameter that assigns <see cref="MaxContentLightLevel" /> directly.</param>
		/// <param name="maxPicAverageLightLevel">The parameter that assigns <see cref="MaxPicAverageLightLevel" /> directly.</param>
		public Mp4ContentLightLevelBoxData(ushort maxContentLightLevel, ushort maxPicAverageLightLevel)
		{
			this.MaxContentLightLevel = maxContentLightLevel;
			this.MaxPicAverageLightLevel = maxPicAverageLightLevel;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ContentLightLevelBoxData" /> class.
		/// </summary>
		public Mp4ContentLightLevelBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4ContentLightLevelBoxData val
				&& EqualityComparer<ushort?>.Default.Equals(this.MaxContentLightLevel, val.MaxContentLightLevel)
				&& EqualityComparer<ushort?>.Default.Equals(this.MaxPicAverageLightLevel, val.MaxPicAverageLightLevel)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (MaxContentLightLevel?.GetHashCode() ?? 0);
				hash = hash * 23 + (MaxPicAverageLightLevel?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4ContentLightLevelBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>mdcv</c> box data representation.
	/// </summary>
	[FourCC("mdcv")]
	public class Mp4MasteringDisplayColourVolumeBoxData : IMp4BoxData, IEquatable<Mp4MasteringDisplayColourVolumeBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, DisplayPrimariesX.
		/// </summary>
		public ushort[]? DisplayPrimariesX { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, DisplayPrimariesY.
		/// </summary>
		public ushort[]? DisplayPrimariesY { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, WhitePointX.
		/// </summary>
		public ushort? WhitePointX { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, WhitePointY.
		/// </summary>
		public ushort? WhitePointY { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, MaxDisplayMasteringLuminance.
		/// </summary>
		public uint? MaxDisplayMasteringLuminance { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, MinDisplayMasteringLuminance.
		/// </summary>
		public uint? MinDisplayMasteringLuminance { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4MasteringDisplayColourVolumeBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="displayPrimariesX">The parameter that assigns <see cref="DisplayPrimariesX" /> directly.</param>
		/// <param name="displayPrimariesY">The parameter that assigns <see cref="DisplayPrimariesY" /> directly.</param>
		/// <param name="whitePointX">The parameter that assigns <see cref="WhitePointX" /> directly.</param>
		/// <param name="whitePointY">The parameter that assigns <see cref="WhitePointY" /> directly.</param>
		/// <param name="maxDisplayMasteringLuminance">The parameter that assigns <see cref="MaxDisplayMasteringLuminance" /> directly.</param>
		/// <param name="minDisplayMasteringLuminance">The parameter that assigns <see cref="MinDisplayMasteringLuminance" /> directly.</param>
		public Mp4MasteringDisplayColourVolumeBoxData(ushort[] displayPrimariesX, ushort[] displayPrimariesY, ushort whitePointX, ushort whitePointY, uint maxDisplayMasteringLuminance, uint minDisplayMasteringLuminance)
		{
			this.DisplayPrimariesX = displayPrimariesX;
			this.DisplayPrimariesY = displayPrimariesY;
			this.WhitePointX = whitePointX;
			this.WhitePointY = whitePointY;
			this.MaxDisplayMasteringLuminance = maxDisplayMasteringLuminance;
			this.MinDisplayMasteringLuminance = minDisplayMasteringLuminance;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4MasteringDisplayColourVolumeBoxData" /> class.
		/// </summary>
		public Mp4MasteringDisplayColourVolumeBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4MasteringDisplayColourVolumeBoxData val
				&& EqualityComparer<ushort[]?>.Default.Equals(this.DisplayPrimariesX, val.DisplayPrimariesX)
				&& EqualityComparer<ushort[]?>.Default.Equals(this.DisplayPrimariesY, val.DisplayPrimariesY)
				&& EqualityComparer<ushort?>.Default.Equals(this.WhitePointX, val.WhitePointX)
				&& EqualityComparer<ushort?>.Default.Equals(this.WhitePointY, val.WhitePointY)
				&& EqualityComparer<uint?>.Default.Equals(this.MaxDisplayMasteringLuminance, val.MaxDisplayMasteringLuminance)
				&& EqualityComparer<uint?>.Default.Equals(this.MinDisplayMasteringLuminance, val.MinDisplayMasteringLuminance)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (DisplayPrimariesX?.GetHashCode() ?? 0);
				hash = hash * 23 + (DisplayPrimariesY?.GetHashCode() ?? 0);
				hash = hash * 23 + (WhitePointX?.GetHashCode() ?? 0);
				hash = hash * 23 + (WhitePointY?.GetHashCode() ?? 0);
				hash = hash * 23 + (MaxDisplayMasteringLuminance?.GetHashCode() ?? 0);
				hash = hash * 23 + (MinDisplayMasteringLuminance?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4MasteringDisplayColourVolumeBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>scrb</c> box data representation.
	/// </summary>
	[FourCC("scrb")]
	public class Mp4ScrambleSchemeInfoBoxBoxData : IMp4BoxData, IEquatable<Mp4ScrambleSchemeInfoBoxBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, SchemeTypeBox.
		/// </summary>
		public Mp4Box? SchemeTypeBox { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Info.
		/// </summary>
		public Mp4Box? Info { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ScrambleSchemeInfoBoxBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="schemeTypeBox">The parameter that assigns <see cref="SchemeTypeBox" /> directly.</param>
		/// <param name="info">The parameter that assigns <see cref="Info" /> directly.</param>
		public Mp4ScrambleSchemeInfoBoxBoxData(Mp4Box schemeTypeBox, Mp4Box info)
		{
			this.SchemeTypeBox = schemeTypeBox;
			this.Info = info;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ScrambleSchemeInfoBoxBoxData" /> class.
		/// </summary>
		public Mp4ScrambleSchemeInfoBoxBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4ScrambleSchemeInfoBoxBoxData val
				&& EqualityComparer<Mp4Box?>.Default.Equals(this.SchemeTypeBox, val.SchemeTypeBox)
				&& EqualityComparer<Mp4Box?>.Default.Equals(this.Info, val.Info)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (SchemeTypeBox?.GetHashCode() ?? 0);
				hash = hash * 23 + (Info?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4ScrambleSchemeInfoBoxBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>srat</c> box data representation.
	/// </summary>
	[FourCC("srat")]
	public class Mp4SamplingRateBoxData : IMp4BoxData, IEquatable<Mp4SamplingRateBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, SamplingRate.
		/// </summary>
		public uint? SamplingRate { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SamplingRateBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="samplingRate">The parameter that assigns <see cref="SamplingRate" /> directly.</param>
		public Mp4SamplingRateBoxData(uint samplingRate)
		{
			this.SamplingRate = samplingRate;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SamplingRateBoxData" /> class.
		/// </summary>
		public Mp4SamplingRateBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4SamplingRateBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.SamplingRate, val.SamplingRate)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (SamplingRate?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4SamplingRateBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>txtC</c> box data representation.
	/// </summary>
	[FourCC("txtC")]
	public class Mp4TextStreamConfigurationBoxData : IMp4BoxData, IEquatable<Mp4TextStreamConfigurationBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, TextConfig.
		/// </summary>
		public string? TextConfig { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TextStreamConfigurationBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="textConfig">The parameter that assigns <see cref="TextConfig" /> directly.</param>
		public Mp4TextStreamConfigurationBoxData(string textConfig)
		{
			this.TextConfig = textConfig;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TextStreamConfigurationBoxData" /> class.
		/// </summary>
		public Mp4TextStreamConfigurationBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4TextStreamConfigurationBoxData val
				&& EqualityComparer<string?>.Default.Equals(this.TextConfig, val.TextConfig)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (TextConfig?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4TextStreamConfigurationBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>uriI</c> box data representation.
	/// </summary>
	[FourCC("uriI")]
	public class Mp4UriInformationBoxData : IMp4FullBoxData, IEquatable<Mp4UriInformationBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, UriInitializationData.
		/// </summary>
		public byte[]? UriInitializationData { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4UriInformationBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="uriInitializationData">The parameter that assigns <see cref="UriInitializationData" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4UriInformationBoxData(byte[] uriInitializationData, byte version, uint flags)
		{
			this.UriInitializationData = uriInitializationData;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4UriInformationBoxData" /> class.
		/// </summary>
		public Mp4UriInformationBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4UriInformationBoxData val
				&& EqualityComparer<byte[]?>.Default.Equals(this.UriInitializationData, val.UriInitializationData)
				&& EqualityComparer<byte?>.Default.Equals(this.Version, val.Version)
				&& EqualityComparer<uint?>.Default.Equals(this.Flags, val.Flags)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (UriInitializationData?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4UriInformationBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>cprt</c> box data representation.
	/// </summary>
	[FourCC("cprt")]
	public class Mp4CopyrightBoxData : IMp4FullBoxData, IEquatable<Mp4CopyrightBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Pad.
		/// </summary>
		public bool? Pad { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Language.
		/// </summary>
		public byte[]? Language { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Notice.
		/// </summary>
		public string? Notice { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4CopyrightBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="pad">The parameter that assigns <see cref="Pad" /> directly.</param>
		/// <param name="language">The parameter that assigns <see cref="Language" /> directly.</param>
		/// <param name="notice">The parameter that assigns <see cref="Notice" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4CopyrightBoxData(bool pad, byte[] language, string notice, byte version, uint flags)
		{
			this.Pad = pad;
			this.Language = language;
			this.Notice = notice;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4CopyrightBoxData" /> class.
		/// </summary>
		public Mp4CopyrightBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4CopyrightBoxData val
				&& EqualityComparer<bool?>.Default.Equals(this.Pad, val.Pad)
				&& EqualityComparer<byte[]?>.Default.Equals(this.Language, val.Language)
				&& EqualityComparer<string?>.Default.Equals(this.Notice, val.Notice)
				&& EqualityComparer<byte?>.Default.Equals(this.Version, val.Version)
				&& EqualityComparer<uint?>.Default.Equals(this.Flags, val.Flags)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Pad?.GetHashCode() ?? 0);
				hash = hash * 23 + (Language?.GetHashCode() ?? 0);
				hash = hash * 23 + (Notice?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4CopyrightBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>kind</c> box data representation.
	/// </summary>
	[FourCC("kind")]
	public class Mp4TrackKindBoxData : IMp4FullBoxData, IEquatable<Mp4TrackKindBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, SchemeUri.
		/// </summary>
		public string? SchemeUri { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Value.
		/// </summary>
		public string? Value { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TrackKindBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="schemeUri">The parameter that assigns <see cref="SchemeUri" /> directly.</param>
		/// <param name="value">The parameter that assigns <see cref="Value" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4TrackKindBoxData(string schemeUri, string value, byte version, uint flags)
		{
			this.SchemeUri = schemeUri;
			this.Value = value;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TrackKindBoxData" /> class.
		/// </summary>
		public Mp4TrackKindBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4TrackKindBoxData val
				&& EqualityComparer<string?>.Default.Equals(this.SchemeUri, val.SchemeUri)
				&& EqualityComparer<string?>.Default.Equals(this.Value, val.Value)
				&& EqualityComparer<byte?>.Default.Equals(this.Version, val.Version)
				&& EqualityComparer<uint?>.Default.Equals(this.Flags, val.Flags)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (SchemeUri?.GetHashCode() ?? 0);
				hash = hash * 23 + (Value?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4TrackKindBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>tsel</c> box data representation.
	/// </summary>
	[FourCC("tsel")]
	public class Mp4TrackSelectionBoxData : IMp4FullBoxData, IEquatable<Mp4TrackSelectionBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, SwitchGroup.
		/// </summary>
		public int? SwitchGroup { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, AttributeList.
		/// </summary>
		public uint[]? AttributeList { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TrackSelectionBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="switchGroup">The parameter that assigns <see cref="SwitchGroup" /> directly.</param>
		/// <param name="attributeList">The parameter that assigns <see cref="AttributeList" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4TrackSelectionBoxData(int switchGroup, uint[] attributeList, byte version, uint flags)
		{
			this.SwitchGroup = switchGroup;
			this.AttributeList = attributeList;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TrackSelectionBoxData" /> class.
		/// </summary>
		public Mp4TrackSelectionBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4TrackSelectionBoxData val
				&& EqualityComparer<int?>.Default.Equals(this.SwitchGroup, val.SwitchGroup)
				&& EqualityComparer<uint[]?>.Default.Equals(this.AttributeList, val.AttributeList)
				&& EqualityComparer<byte?>.Default.Equals(this.Version, val.Version)
				&& EqualityComparer<uint?>.Default.Equals(this.Flags, val.Flags)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (SwitchGroup?.GetHashCode() ?? 0);
				hash = hash * 23 + (AttributeList?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4TrackSelectionBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>strk</c> box data representation.
	/// </summary>
	[FourCC("strk")]
	public class Mp4SubTrackBoxBoxData : IMp4BoxData, IEquatable<Mp4SubTrackBoxBoxData?>
	{


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SubTrackBoxBoxData" /> class.
		/// </summary>
		public Mp4SubTrackBoxBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4SubTrackBoxBoxData val
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4SubTrackBoxBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>hnti</c> box data representation.
	/// </summary>
	[FourCC("hnti")]
	public class Mp4HintInformationBoxData : IMp4BoxData, IEquatable<Mp4HintInformationBoxData?>
	{


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4HintInformationBoxData" /> class.
		/// </summary>
		public Mp4HintInformationBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4HintInformationBoxData val
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4HintInformationBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>sdp </c> box data representation.
	/// </summary>
	[FourCC("sdp ")]
	public class Mp4SdpInformationBoxData : IMp4BoxData, IEquatable<Mp4SdpInformationBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, SdpText.
		/// </summary>
		public string? SdpText { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SdpInformationBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="sdpText">The parameter that assigns <see cref="SdpText" /> directly.</param>
		public Mp4SdpInformationBoxData(string sdpText)
		{
			this.SdpText = sdpText;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SdpInformationBoxData" /> class.
		/// </summary>
		public Mp4SdpInformationBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4SdpInformationBoxData val
				&& EqualityComparer<string?>.Default.Equals(this.SdpText, val.SdpText)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (SdpText?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4SdpInformationBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>rtp </c> box data representation.
	/// </summary>
	[FourCC("rtp ")]
	public class Mp4RtpInformationBoxData : IMp4BoxData, IEquatable<Mp4RtpInformationBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, DescriptionFormat.
		/// </summary>
		public uint? DescriptionFormat { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, SdpText.
		/// </summary>
		public string? SdpText { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4RtpInformationBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="descriptionFormat">The parameter that assigns <see cref="DescriptionFormat" /> directly.</param>
		/// <param name="sdpText">The parameter that assigns <see cref="SdpText" /> directly.</param>
		public Mp4RtpInformationBoxData(uint descriptionFormat, string sdpText)
		{
			this.DescriptionFormat = descriptionFormat;
			this.SdpText = sdpText;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4RtpInformationBoxData" /> class.
		/// </summary>
		public Mp4RtpInformationBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4RtpInformationBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.DescriptionFormat, val.DescriptionFormat)
				&& EqualityComparer<string?>.Default.Equals(this.SdpText, val.SdpText)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (DescriptionFormat?.GetHashCode() ?? 0);
				hash = hash * 23 + (SdpText?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4RtpInformationBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>hinf</c> box data representation.
	/// </summary>
	[FourCC("hinf")]
	public class Mp4HintStatisticsBoxData : IMp4BoxData, IEquatable<Mp4HintStatisticsBoxData?>
	{


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4HintStatisticsBoxData" /> class.
		/// </summary>
		public Mp4HintStatisticsBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4HintStatisticsBoxData val
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4HintStatisticsBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>url </c> box data representation.
	/// </summary>
	[FourCC("url ")]
	public class Mp4UrlBoxBoxData : IMp4BoxData, IEquatable<Mp4UrlBoxBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Location.
		/// </summary>
		public string? Location { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4UrlBoxBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		/// <param name="location">The parameter that assigns <see cref="Location" /> directly.</param>
		public Mp4UrlBoxBoxData(uint flags, string location)
		{
			this.Flags = flags;
			this.Location = location;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4UrlBoxBoxData" /> class.
		/// </summary>
		public Mp4UrlBoxBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4UrlBoxBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.Flags, val.Flags)
				&& EqualityComparer<string?>.Default.Equals(this.Location, val.Location)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				hash = hash * 23 + (Location?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4UrlBoxBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>urn </c> box data representation.
	/// </summary>
	[FourCC("urn ")]
	public class Mp4UrnBoxBoxData : IMp4BoxData, IEquatable<Mp4UrnBoxBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Name.
		/// </summary>
		public string? Name { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Location.
		/// </summary>
		public string? Location { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4UrnBoxBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		/// <param name="name">The parameter that assigns <see cref="Name" /> directly.</param>
		/// <param name="location">The parameter that assigns <see cref="Location" /> directly.</param>
		public Mp4UrnBoxBoxData(uint flags, string name, string location)
		{
			this.Flags = flags;
			this.Name = name;
			this.Location = location;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4UrnBoxBoxData" /> class.
		/// </summary>
		public Mp4UrnBoxBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4UrnBoxBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.Flags, val.Flags)
				&& EqualityComparer<string?>.Default.Equals(this.Name, val.Name)
				&& EqualityComparer<string?>.Default.Equals(this.Location, val.Location)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				hash = hash * 23 + (Name?.GetHashCode() ?? 0);
				hash = hash * 23 + (Location?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4UrnBoxBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>imdt</c> box data representation.
	/// </summary>
	[FourCC("imdt")]
	public class Mp4IdentifiedMediaDataBoxData : IMp4BoxData, IEquatable<Mp4IdentifiedMediaDataBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, ImdaRefIdentifier.
		/// </summary>
		public uint? ImdaRefIdentifier { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4IdentifiedMediaDataBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		/// <param name="imdaRefIdentifier">The parameter that assigns <see cref="ImdaRefIdentifier" /> directly.</param>
		public Mp4IdentifiedMediaDataBoxData(uint flags, uint imdaRefIdentifier)
		{
			this.Flags = flags;
			this.ImdaRefIdentifier = imdaRefIdentifier;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4IdentifiedMediaDataBoxData" /> class.
		/// </summary>
		public Mp4IdentifiedMediaDataBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4IdentifiedMediaDataBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.Flags, val.Flags)
				&& EqualityComparer<uint?>.Default.Equals(this.ImdaRefIdentifier, val.ImdaRefIdentifier)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				hash = hash * 23 + (ImdaRefIdentifier?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4IdentifiedMediaDataBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>snim</c> box data representation.
	/// </summary>
	[FourCC("snim")]
	public class Mp4SequenceNumberIdentifiedMediaDataBoxData : IMp4BoxData, IEquatable<Mp4SequenceNumberIdentifiedMediaDataBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SequenceNumberIdentifiedMediaDataBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4SequenceNumberIdentifiedMediaDataBoxData(uint flags)
		{
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SequenceNumberIdentifiedMediaDataBoxData" /> class.
		/// </summary>
		public Mp4SequenceNumberIdentifiedMediaDataBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4SequenceNumberIdentifiedMediaDataBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.Flags, val.Flags)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4SequenceNumberIdentifiedMediaDataBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>ipco</c> box data representation.
	/// </summary>
	[FourCC("ipco")]
	public class Mp4ItemPropertyContainerBoxBoxData : IMp4BoxData, IEquatable<Mp4ItemPropertyContainerBoxBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Properties.
		/// </summary>
		public IList<Mp4Box>? Properties { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ItemPropertyContainerBoxBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="properties">The parameter that assigns <see cref="Properties" /> directly.</param>
		public Mp4ItemPropertyContainerBoxBoxData(IList<Mp4Box> properties)
		{
			this.Properties = properties;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ItemPropertyContainerBoxBoxData" /> class.
		/// </summary>
		public Mp4ItemPropertyContainerBoxBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4ItemPropertyContainerBoxBoxData val
				&& EqualityComparer<IList<Mp4Box>?>.Default.Equals(this.Properties, val.Properties)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Properties?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4ItemPropertyContainerBoxBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>ipma</c> box data representation.
	/// </summary>
	[FourCC("ipma")]
	public class Mp4ItemPropertyAssociationBoxData : IMp4FullBoxData, IEquatable<Mp4ItemPropertyAssociationBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, EntryCount.
		/// </summary>
		public uint? EntryCount { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, ItemId.
		/// </summary>
		public IList<uint>? ItemId { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, AssociationCount.
		/// </summary>
		public IList<byte>? AssociationCount { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Essential.
		/// </summary>
		public IList<IList<bool>>? Essential { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, PropertyIndex.
		/// </summary>
		public IList<IList<ushort>>? PropertyIndex { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ItemPropertyAssociationBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="entryCount">The parameter that assigns <see cref="EntryCount" /> directly.</param>
		/// <param name="itemId">The parameter that assigns <see cref="ItemId" /> directly.</param>
		/// <param name="associationCount">The parameter that assigns <see cref="AssociationCount" /> directly.</param>
		/// <param name="essential">The parameter that assigns <see cref="Essential" /> directly.</param>
		/// <param name="propertyIndex">The parameter that assigns <see cref="PropertyIndex" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4ItemPropertyAssociationBoxData(uint entryCount, IList<uint> itemId, IList<byte> associationCount, IList<IList<bool>> essential, IList<IList<ushort>> propertyIndex, byte version, uint flags)
		{
			this.EntryCount = entryCount;
			this.ItemId = itemId;
			this.AssociationCount = associationCount;
			this.Essential = essential;
			this.PropertyIndex = propertyIndex;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ItemPropertyAssociationBoxData" /> class.
		/// </summary>
		public Mp4ItemPropertyAssociationBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4ItemPropertyAssociationBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.EntryCount, val.EntryCount)
				&& EqualityComparer<IList<uint>?>.Default.Equals(this.ItemId, val.ItemId)
				&& EqualityComparer<IList<byte>?>.Default.Equals(this.AssociationCount, val.AssociationCount)
				&& EqualityComparer<IList<IList<bool>>?>.Default.Equals(this.Essential, val.Essential)
				&& EqualityComparer<IList<IList<ushort>>?>.Default.Equals(this.PropertyIndex, val.PropertyIndex)
				&& EqualityComparer<byte?>.Default.Equals(this.Version, val.Version)
				&& EqualityComparer<uint?>.Default.Equals(this.Flags, val.Flags)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (EntryCount?.GetHashCode() ?? 0);
				hash = hash * 23 + (ItemId?.GetHashCode() ?? 0);
				hash = hash * 23 + (AssociationCount?.GetHashCode() ?? 0);
				hash = hash * 23 + (Essential?.GetHashCode() ?? 0);
				hash = hash * 23 + (PropertyIndex?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4ItemPropertyAssociationBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>iprp</c> box data representation.
	/// </summary>
	[FourCC("iprp")]
	public class Mp4ItemPropertiesBoxData : IMp4BoxData, IEquatable<Mp4ItemPropertiesBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, PropertyContainer.
		/// </summary>
		public IList<Mp4Box>? PropertyContainer { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Association.
		/// </summary>
		public IList<Mp4Box>? Association { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ItemPropertiesBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="propertyContainer">The parameter that assigns <see cref="PropertyContainer" /> directly.</param>
		/// <param name="association">The parameter that assigns <see cref="Association" /> directly.</param>
		public Mp4ItemPropertiesBoxData(IList<Mp4Box> propertyContainer, IList<Mp4Box> association)
		{
			this.PropertyContainer = propertyContainer;
			this.Association = association;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ItemPropertiesBoxData" /> class.
		/// </summary>
		public Mp4ItemPropertiesBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4ItemPropertiesBoxData val
				&& EqualityComparer<IList<Mp4Box>?>.Default.Equals(this.PropertyContainer, val.PropertyContainer)
				&& EqualityComparer<IList<Mp4Box>?>.Default.Equals(this.Association, val.Association)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (PropertyContainer?.GetHashCode() ?? 0);
				hash = hash * 23 + (Association?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4ItemPropertiesBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>assp</c> box data representation.
	/// </summary>
	[FourCC("assp")]
	public class Mp4ActiveSequenceStartupPropertiesBoxData : IMp4FullBoxData, IEquatable<Mp4ActiveSequenceStartupPropertiesBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, MinInitialStartupOffset.
		/// </summary>
		public int? MinInitialStartupOffset { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, NumEntries.
		/// </summary>
		public uint? NumEntries { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, GroupingTypeParameter.
		/// </summary>
		public uint[]? GroupingTypeParameter { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, MinInitialStartupOffset2.
		/// </summary>
		public uint[]? MinInitialStartupOffset2 { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ActiveSequenceStartupPropertiesBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="minInitialStartupOffset">The parameter that assigns <see cref="MinInitialStartupOffset" /> directly.</param>
		/// <param name="numEntries">The parameter that assigns <see cref="NumEntries" /> directly.</param>
		/// <param name="groupingTypeParameter">The parameter that assigns <see cref="GroupingTypeParameter" /> directly.</param>
		/// <param name="minInitialStartupOffset2">The parameter that assigns <see cref="MinInitialStartupOffset2" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4ActiveSequenceStartupPropertiesBoxData(int minInitialStartupOffset, uint numEntries, uint[] groupingTypeParameter, uint[] minInitialStartupOffset2, byte version, uint flags)
		{
			this.MinInitialStartupOffset = minInitialStartupOffset;
			this.NumEntries = numEntries;
			this.GroupingTypeParameter = groupingTypeParameter;
			this.MinInitialStartupOffset2 = minInitialStartupOffset2;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ActiveSequenceStartupPropertiesBoxData" /> class.
		/// </summary>
		public Mp4ActiveSequenceStartupPropertiesBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4ActiveSequenceStartupPropertiesBoxData val
				&& EqualityComparer<int?>.Default.Equals(this.MinInitialStartupOffset, val.MinInitialStartupOffset)
				&& EqualityComparer<uint?>.Default.Equals(this.NumEntries, val.NumEntries)
				&& EqualityComparer<uint[]?>.Default.Equals(this.GroupingTypeParameter, val.GroupingTypeParameter)
				&& EqualityComparer<uint[]?>.Default.Equals(this.MinInitialStartupOffset2, val.MinInitialStartupOffset2)
				&& EqualityComparer<byte?>.Default.Equals(this.Version, val.Version)
				&& EqualityComparer<uint?>.Default.Equals(this.Flags, val.Flags)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (MinInitialStartupOffset?.GetHashCode() ?? 0);
				hash = hash * 23 + (NumEntries?.GetHashCode() ?? 0);
				hash = hash * 23 + (GroupingTypeParameter?.GetHashCode() ?? 0);
				hash = hash * 23 + (MinInitialStartupOffset2?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4ActiveSequenceStartupPropertiesBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>bxml</c> box data representation.
	/// </summary>
	[FourCC("bxml")]
	public class Mp4BinaryXmlBoxData : IMp4FullBoxData, IEquatable<Mp4BinaryXmlBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Data.
		/// </summary>
		public byte[]? Data { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4BinaryXmlBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="data">The parameter that assigns <see cref="Data" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4BinaryXmlBoxData(byte[] data, byte version, uint flags)
		{
			this.Data = data;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4BinaryXmlBoxData" /> class.
		/// </summary>
		public Mp4BinaryXmlBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4BinaryXmlBoxData val
				&& EqualityComparer<byte[]?>.Default.Equals(this.Data, val.Data)
				&& EqualityComparer<byte?>.Default.Equals(this.Version, val.Version)
				&& EqualityComparer<uint?>.Default.Equals(this.Flags, val.Flags)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Data?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4BinaryXmlBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>cinf</c> box data representation.
	/// </summary>
	[FourCC("cinf")]
	public class Mp4CompleteTrackInformationBoxData : IMp4BoxData, IEquatable<Mp4CompleteTrackInformationBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, OriginalFormat.
		/// </summary>
		public Mp4Box? OriginalFormat { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4CompleteTrackInformationBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="originalFormat">The parameter that assigns <see cref="OriginalFormat" /> directly.</param>
		public Mp4CompleteTrackInformationBoxData(Mp4Box originalFormat)
		{
			this.OriginalFormat = originalFormat;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4CompleteTrackInformationBoxData" /> class.
		/// </summary>
		public Mp4CompleteTrackInformationBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4CompleteTrackInformationBoxData val
				&& EqualityComparer<Mp4Box?>.Default.Equals(this.OriginalFormat, val.OriginalFormat)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (OriginalFormat?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4CompleteTrackInformationBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>co64</c> box data representation.
	/// </summary>
	[FourCC("co64")]
	public class Mp4ChunkLargeOffsetBoxData : IMp4FullBoxData, IEquatable<Mp4ChunkLargeOffsetBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, EntryCount.
		/// </summary>
		public uint? EntryCount { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, ChunkOffset.
		/// </summary>
		public IList<ulong>? ChunkOffset { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ChunkLargeOffsetBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="entryCount">The parameter that assigns <see cref="EntryCount" /> directly.</param>
		/// <param name="chunkOffset">The parameter that assigns <see cref="ChunkOffset" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4ChunkLargeOffsetBoxData(uint entryCount, IList<ulong> chunkOffset, byte version, uint flags)
		{
			this.EntryCount = entryCount;
			this.ChunkOffset = chunkOffset;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ChunkLargeOffsetBoxData" /> class.
		/// </summary>
		public Mp4ChunkLargeOffsetBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4ChunkLargeOffsetBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.EntryCount, val.EntryCount)
				&& EqualityComparer<IList<ulong>?>.Default.Equals(this.ChunkOffset, val.ChunkOffset)
				&& EqualityComparer<byte?>.Default.Equals(this.Version, val.Version)
				&& EqualityComparer<uint?>.Default.Equals(this.Flags, val.Flags)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (EntryCount?.GetHashCode() ?? 0);
				hash = hash * 23 + (ChunkOffset?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4ChunkLargeOffsetBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>cslg</c> box data representation.
	/// </summary>
	[FourCC("cslg")]
	public class Mp4CompositionToDecodeTimelineMappingBoxData : IMp4FullBoxData, IEquatable<Mp4CompositionToDecodeTimelineMappingBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, CompositionToDtsShift.
		/// </summary>
		public long? CompositionToDtsShift { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, LeastDecodeToDisplayDelta.
		/// </summary>
		public long? LeastDecodeToDisplayDelta { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, GreatestDecodeToDisplayDelta.
		/// </summary>
		public long? GreatestDecodeToDisplayDelta { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, CompositionStartTime.
		/// </summary>
		public long? CompositionStartTime { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, CompositionEndTime.
		/// </summary>
		public long? CompositionEndTime { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4CompositionToDecodeTimelineMappingBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="compositionToDtsShift">The parameter that assigns <see cref="CompositionToDtsShift" /> directly.</param>
		/// <param name="leastDecodeToDisplayDelta">The parameter that assigns <see cref="LeastDecodeToDisplayDelta" /> directly.</param>
		/// <param name="greatestDecodeToDisplayDelta">The parameter that assigns <see cref="GreatestDecodeToDisplayDelta" /> directly.</param>
		/// <param name="compositionStartTime">The parameter that assigns <see cref="CompositionStartTime" /> directly.</param>
		/// <param name="compositionEndTime">The parameter that assigns <see cref="CompositionEndTime" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4CompositionToDecodeTimelineMappingBoxData(long compositionToDtsShift, long leastDecodeToDisplayDelta, long greatestDecodeToDisplayDelta, long compositionStartTime, long compositionEndTime, byte version, uint flags)
		{
			this.CompositionToDtsShift = compositionToDtsShift;
			this.LeastDecodeToDisplayDelta = leastDecodeToDisplayDelta;
			this.GreatestDecodeToDisplayDelta = greatestDecodeToDisplayDelta;
			this.CompositionStartTime = compositionStartTime;
			this.CompositionEndTime = compositionEndTime;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4CompositionToDecodeTimelineMappingBoxData" /> class.
		/// </summary>
		public Mp4CompositionToDecodeTimelineMappingBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4CompositionToDecodeTimelineMappingBoxData val
				&& EqualityComparer<long?>.Default.Equals(this.CompositionToDtsShift, val.CompositionToDtsShift)
				&& EqualityComparer<long?>.Default.Equals(this.LeastDecodeToDisplayDelta, val.LeastDecodeToDisplayDelta)
				&& EqualityComparer<long?>.Default.Equals(this.GreatestDecodeToDisplayDelta, val.GreatestDecodeToDisplayDelta)
				&& EqualityComparer<long?>.Default.Equals(this.CompositionStartTime, val.CompositionStartTime)
				&& EqualityComparer<long?>.Default.Equals(this.CompositionEndTime, val.CompositionEndTime)
				&& EqualityComparer<byte?>.Default.Equals(this.Version, val.Version)
				&& EqualityComparer<uint?>.Default.Equals(this.Flags, val.Flags)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (CompositionToDtsShift?.GetHashCode() ?? 0);
				hash = hash * 23 + (LeastDecodeToDisplayDelta?.GetHashCode() ?? 0);
				hash = hash * 23 + (GreatestDecodeToDisplayDelta?.GetHashCode() ?? 0);
				hash = hash * 23 + (CompositionStartTime?.GetHashCode() ?? 0);
				hash = hash * 23 + (CompositionEndTime?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4CompositionToDecodeTimelineMappingBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>ctts</c> box data representation.
	/// </summary>
	[FourCC("ctts")]
	public class Mp4CompositionTimeToSampleBoxData : IMp4FullBoxData, IEquatable<Mp4CompositionTimeToSampleBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, EntryCount.
		/// </summary>
		public uint? EntryCount { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, SampleCount.
		/// </summary>
		public IList<uint>? SampleCount { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, SampleOffset.
		/// </summary>
		public IList<int>? SampleOffset { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4CompositionTimeToSampleBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="entryCount">The parameter that assigns <see cref="EntryCount" /> directly.</param>
		/// <param name="sampleCount">The parameter that assigns <see cref="SampleCount" /> directly.</param>
		/// <param name="sampleOffset">The parameter that assigns <see cref="SampleOffset" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4CompositionTimeToSampleBoxData(uint entryCount, IList<uint> sampleCount, IList<int> sampleOffset, byte version, uint flags)
		{
			this.EntryCount = entryCount;
			this.SampleCount = sampleCount;
			this.SampleOffset = sampleOffset;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4CompositionTimeToSampleBoxData" /> class.
		/// </summary>
		public Mp4CompositionTimeToSampleBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4CompositionTimeToSampleBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.EntryCount, val.EntryCount)
				&& EqualityComparer<IList<uint>?>.Default.Equals(this.SampleCount, val.SampleCount)
				&& EqualityComparer<IList<int>?>.Default.Equals(this.SampleOffset, val.SampleOffset)
				&& EqualityComparer<byte?>.Default.Equals(this.Version, val.Version)
				&& EqualityComparer<uint?>.Default.Equals(this.Flags, val.Flags)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (EntryCount?.GetHashCode() ?? 0);
				hash = hash * 23 + (SampleCount?.GetHashCode() ?? 0);
				hash = hash * 23 + (SampleOffset?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4CompositionTimeToSampleBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>dinf</c> box data representation.
	/// </summary>
	[FourCC("dinf")]
	public class Mp4DataInformationBoxData : IMp4BoxData, IEquatable<Mp4DataInformationBoxData?>
	{


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4DataInformationBoxData" /> class.
		/// </summary>
		public Mp4DataInformationBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4DataInformationBoxData val
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4DataInformationBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>dref</c> box data representation.
	/// </summary>
	[FourCC("dref")]
	public class Mp4DataReferenceBoxData : IMp4BoxData, IEquatable<Mp4DataReferenceBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, EntryCount.
		/// </summary>
		public uint? EntryCount { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Boxes.
		/// </summary>
		public IList<Mp4Box>? Boxes { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4DataReferenceBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="entryCount">The parameter that assigns <see cref="EntryCount" /> directly.</param>
		/// <param name="boxes">The parameter that assigns <see cref="Boxes" /> directly.</param>
		public Mp4DataReferenceBoxData(uint entryCount, IList<Mp4Box> boxes)
		{
			this.EntryCount = entryCount;
			this.Boxes = boxes;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4DataReferenceBoxData" /> class.
		/// </summary>
		public Mp4DataReferenceBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4DataReferenceBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.EntryCount, val.EntryCount)
				&& EqualityComparer<IList<Mp4Box>?>.Default.Equals(this.Boxes, val.Boxes)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (EntryCount?.GetHashCode() ?? 0);
				hash = hash * 23 + (Boxes?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4DataReferenceBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>edts</c> box data representation.
	/// </summary>
	[FourCC("edts")]
	public class Mp4EditListContainerBoxData : IMp4BoxData, IEquatable<Mp4EditListContainerBoxData?>
	{


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4EditListContainerBoxData" /> class.
		/// </summary>
		public Mp4EditListContainerBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4EditListContainerBoxData val
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4EditListContainerBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>elst</c> box data representation.
	/// </summary>
	[FourCC("elst")]
	public class Mp4EditListBoxData : IMp4FullBoxData, IEquatable<Mp4EditListBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, EntryCount.
		/// </summary>
		public uint? EntryCount { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, EditDuration.
		/// </summary>
		public IList<ulong>? EditDuration { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, MediaTime.
		/// </summary>
		public IList<ulong>? MediaTime { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, MediaRateInteger.
		/// </summary>
		public IList<short>? MediaRateInteger { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, MediaRateFraction.
		/// </summary>
		public IList<short>? MediaRateFraction { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4EditListBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="entryCount">The parameter that assigns <see cref="EntryCount" /> directly.</param>
		/// <param name="editDuration">The parameter that assigns <see cref="EditDuration" /> directly.</param>
		/// <param name="mediaTime">The parameter that assigns <see cref="MediaTime" /> directly.</param>
		/// <param name="mediaRateInteger">The parameter that assigns <see cref="MediaRateInteger" /> directly.</param>
		/// <param name="mediaRateFraction">The parameter that assigns <see cref="MediaRateFraction" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4EditListBoxData(uint entryCount, IList<ulong> editDuration, IList<ulong> mediaTime, IList<short> mediaRateInteger, IList<short> mediaRateFraction, byte version, uint flags)
		{
			this.EntryCount = entryCount;
			this.EditDuration = editDuration;
			this.MediaTime = mediaTime;
			this.MediaRateInteger = mediaRateInteger;
			this.MediaRateFraction = mediaRateFraction;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4EditListBoxData" /> class.
		/// </summary>
		public Mp4EditListBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4EditListBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.EntryCount, val.EntryCount)
				&& EqualityComparer<IList<ulong>?>.Default.Equals(this.EditDuration, val.EditDuration)
				&& EqualityComparer<IList<ulong>?>.Default.Equals(this.MediaTime, val.MediaTime)
				&& EqualityComparer<IList<short>?>.Default.Equals(this.MediaRateInteger, val.MediaRateInteger)
				&& EqualityComparer<IList<short>?>.Default.Equals(this.MediaRateFraction, val.MediaRateFraction)
				&& EqualityComparer<byte?>.Default.Equals(this.Version, val.Version)
				&& EqualityComparer<uint?>.Default.Equals(this.Flags, val.Flags)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (EntryCount?.GetHashCode() ?? 0);
				hash = hash * 23 + (EditDuration?.GetHashCode() ?? 0);
				hash = hash * 23 + (MediaTime?.GetHashCode() ?? 0);
				hash = hash * 23 + (MediaRateInteger?.GetHashCode() ?? 0);
				hash = hash * 23 + (MediaRateFraction?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4EditListBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>etyp</c> box data representation.
	/// </summary>
	[FourCC("etyp")]
	public class Mp4ExtendedTypeBoxData : IMp4BoxData, IEquatable<Mp4ExtendedTypeBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, CompatibleCombinations.
		/// </summary>
		public IList<Mp4Box>? CompatibleCombinations { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ExtendedTypeBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="compatibleCombinations">The parameter that assigns <see cref="CompatibleCombinations" /> directly.</param>
		public Mp4ExtendedTypeBoxData(IList<Mp4Box> compatibleCombinations)
		{
			this.CompatibleCombinations = compatibleCombinations;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ExtendedTypeBoxData" /> class.
		/// </summary>
		public Mp4ExtendedTypeBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4ExtendedTypeBoxData val
				&& EqualityComparer<IList<Mp4Box>?>.Default.Equals(this.CompatibleCombinations, val.CompatibleCombinations)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (CompatibleCombinations?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4ExtendedTypeBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>fdel</c> box data representation.
	/// </summary>
	[FourCC("fdel")]
	public class Mp4FileDeliveryInformationBoxData : IMp4BoxData, IEquatable<Mp4FileDeliveryInformationBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, ContentLocation.
		/// </summary>
		public string? ContentLocation { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, ContentMd5.
		/// </summary>
		public string? ContentMd5 { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, ContentLength.
		/// </summary>
		public ulong? ContentLength { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, TransferLength.
		/// </summary>
		public ulong? TransferLength { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, EntryCount.
		/// </summary>
		public byte? EntryCount { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, GroupId.
		/// </summary>
		public IList<uint>? GroupId { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4FileDeliveryInformationBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="contentLocation">The parameter that assigns <see cref="ContentLocation" /> directly.</param>
		/// <param name="contentMd5">The parameter that assigns <see cref="ContentMd5" /> directly.</param>
		/// <param name="contentLength">The parameter that assigns <see cref="ContentLength" /> directly.</param>
		/// <param name="transferLength">The parameter that assigns <see cref="TransferLength" /> directly.</param>
		/// <param name="entryCount">The parameter that assigns <see cref="EntryCount" /> directly.</param>
		/// <param name="groupId">The parameter that assigns <see cref="GroupId" /> directly.</param>
		public Mp4FileDeliveryInformationBoxData(string contentLocation, string contentMd5, ulong contentLength, ulong transferLength, byte entryCount, IList<uint> groupId)
		{
			this.ContentLocation = contentLocation;
			this.ContentMd5 = contentMd5;
			this.ContentLength = contentLength;
			this.TransferLength = transferLength;
			this.EntryCount = entryCount;
			this.GroupId = groupId;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4FileDeliveryInformationBoxData" /> class.
		/// </summary>
		public Mp4FileDeliveryInformationBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4FileDeliveryInformationBoxData val
				&& EqualityComparer<string?>.Default.Equals(this.ContentLocation, val.ContentLocation)
				&& EqualityComparer<string?>.Default.Equals(this.ContentMd5, val.ContentMd5)
				&& EqualityComparer<ulong?>.Default.Equals(this.ContentLength, val.ContentLength)
				&& EqualityComparer<ulong?>.Default.Equals(this.TransferLength, val.TransferLength)
				&& EqualityComparer<byte?>.Default.Equals(this.EntryCount, val.EntryCount)
				&& EqualityComparer<IList<uint>?>.Default.Equals(this.GroupId, val.GroupId)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (ContentLocation?.GetHashCode() ?? 0);
				hash = hash * 23 + (ContentMd5?.GetHashCode() ?? 0);
				hash = hash * 23 + (ContentLength?.GetHashCode() ?? 0);
				hash = hash * 23 + (TransferLength?.GetHashCode() ?? 0);
				hash = hash * 23 + (EntryCount?.GetHashCode() ?? 0);
				hash = hash * 23 + (GroupId?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4FileDeliveryInformationBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>fecr</c> box data representation.
	/// </summary>
	[FourCC("fecr")]
	public class Mp4FecReservoirBoxData : IMp4FullBoxData, IEquatable<Mp4FecReservoirBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, EntryCount.
		/// </summary>
		public uint? EntryCount { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, ItemId.
		/// </summary>
		public IList<uint>? ItemId { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, SymbolCount.
		/// </summary>
		public IList<uint>? SymbolCount { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4FecReservoirBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="entryCount">The parameter that assigns <see cref="EntryCount" /> directly.</param>
		/// <param name="itemId">The parameter that assigns <see cref="ItemId" /> directly.</param>
		/// <param name="symbolCount">The parameter that assigns <see cref="SymbolCount" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4FecReservoirBoxData(uint entryCount, IList<uint> itemId, IList<uint> symbolCount, byte version, uint flags)
		{
			this.EntryCount = entryCount;
			this.ItemId = itemId;
			this.SymbolCount = symbolCount;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4FecReservoirBoxData" /> class.
		/// </summary>
		public Mp4FecReservoirBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4FecReservoirBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.EntryCount, val.EntryCount)
				&& EqualityComparer<IList<uint>?>.Default.Equals(this.ItemId, val.ItemId)
				&& EqualityComparer<IList<uint>?>.Default.Equals(this.SymbolCount, val.SymbolCount)
				&& EqualityComparer<byte?>.Default.Equals(this.Version, val.Version)
				&& EqualityComparer<uint?>.Default.Equals(this.Flags, val.Flags)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (EntryCount?.GetHashCode() ?? 0);
				hash = hash * 23 + (ItemId?.GetHashCode() ?? 0);
				hash = hash * 23 + (SymbolCount?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4FecReservoirBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>free</c> box data representation.
	/// </summary>
	[FourCC("free")]
	public class Mp4FreeSpaceBoxData : IMp4BoxData, IEquatable<Mp4FreeSpaceBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Data.
		/// </summary>
		public IList<byte>? Data { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4FreeSpaceBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="data">The parameter that assigns <see cref="Data" /> directly.</param>
		public Mp4FreeSpaceBoxData(IList<byte> data)
		{
			this.Data = data;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4FreeSpaceBoxData" /> class.
		/// </summary>
		public Mp4FreeSpaceBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4FreeSpaceBoxData val
				&& EqualityComparer<IList<byte>?>.Default.Equals(this.Data, val.Data)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Data?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4FreeSpaceBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   Methods for method-chained mutation of properties within MP4 boxes.
	/// </summary>
	public static class FluentBoxExtensions
	{
		/// <summary>
		///   Changes the <see cref="Mp4ReceivedSsrcBoxData.Ssrc" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ReceivedSsrcBoxData.Ssrc" />
		///   property.
		/// </returns>
		public static Mp4ReceivedSsrcBoxData WithSsrc(
			this Mp4ReceivedSsrcBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Ssrc = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4TimestampSynchronizationBoxData.Reserved" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TimestampSynchronizationBoxData.Reserved" />
		///   property.
		/// </returns>
		public static Mp4TimestampSynchronizationBoxData WithReserved(
			this Mp4TimestampSynchronizationBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Reserved = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4TimestampSynchronizationBoxData.TimestampSync" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TimestampSynchronizationBoxData.TimestampSync" />
		///   property.
		/// </returns>
		public static Mp4TimestampSynchronizationBoxData WithTimestampSync(
			this Mp4TimestampSynchronizationBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.TimestampSync = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4TimescaleEntryBoxData.Timescale" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TimescaleEntryBoxData.Timescale" />
		///   property.
		/// </returns>
		public static Mp4TimescaleEntryBoxData WithTimescale(
			this Mp4TimescaleEntryBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Timescale = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4TimeOffsetBoxData.Offset" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TimeOffsetBoxData.Offset" />
		///   property.
		/// </returns>
		public static Mp4TimeOffsetBoxData WithOffset(
			this Mp4TimeOffsetBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Offset = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4SequenceOffsetBoxData.Offset" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SequenceOffsetBoxData.Offset" />
		///   property.
		/// </returns>
		public static Mp4SequenceOffsetBoxData WithOffset(
			this Mp4SequenceOffsetBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Offset = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4TotalBytesSentIncluding12ByteRtpHeadersBoxData.BytesSent" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TotalBytesSentIncluding12ByteRtpHeadersBoxData.BytesSent" />
		///   property.
		/// </returns>
		public static Mp4TotalBytesSentIncluding12ByteRtpHeadersBoxData WithBytesSent(
			this Mp4TotalBytesSentIncluding12ByteRtpHeadersBoxData sourceBox,
			ulong valueToReplaceWith)
		{
			sourceBox.BytesSent = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4TotalPacketsSentBoxData.PacketsSent" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TotalPacketsSentBoxData.PacketsSent" />
		///   property.
		/// </returns>
		public static Mp4TotalPacketsSentBoxData WithPacketsSent(
			this Mp4TotalPacketsSentBoxData sourceBox,
			ulong valueToReplaceWith)
		{
			sourceBox.PacketsSent = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4TotalBytesSentNotIncludingRtpHeadersBoxData.BytesSent" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TotalBytesSentNotIncludingRtpHeadersBoxData.BytesSent" />
		///   property.
		/// </returns>
		public static Mp4TotalBytesSentNotIncludingRtpHeadersBoxData WithBytesSent(
			this Mp4TotalBytesSentNotIncludingRtpHeadersBoxData sourceBox,
			ulong valueToReplaceWith)
		{
			sourceBox.BytesSent = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4TotalBytesSentIncluding12ByteRtpHeaders32BoxData.BytesSent" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TotalBytesSentIncluding12ByteRtpHeaders32BoxData.BytesSent" />
		///   property.
		/// </returns>
		public static Mp4TotalBytesSentIncluding12ByteRtpHeaders32BoxData WithBytesSent(
			this Mp4TotalBytesSentIncluding12ByteRtpHeaders32BoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.BytesSent = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4TotalPacketsSent32BoxData.PacketsSent" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TotalPacketsSent32BoxData.PacketsSent" />
		///   property.
		/// </returns>
		public static Mp4TotalPacketsSent32BoxData WithPacketsSent(
			this Mp4TotalPacketsSent32BoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.PacketsSent = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4TotalBytesSentNotIncludingRtpHeaders32BoxData.BytesSent" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TotalBytesSentNotIncludingRtpHeaders32BoxData.BytesSent" />
		///   property.
		/// </returns>
		public static Mp4TotalBytesSentNotIncludingRtpHeaders32BoxData WithBytesSent(
			this Mp4TotalBytesSentNotIncludingRtpHeaders32BoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.BytesSent = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4MaximumDataRateBoxData.Period" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MaximumDataRateBoxData.Period" />
		///   property.
		/// </returns>
		public static Mp4MaximumDataRateBoxData WithPeriod(
			this Mp4MaximumDataRateBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Period = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4MaximumDataRateBoxData.Bytes" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MaximumDataRateBoxData.Bytes" />
		///   property.
		/// </returns>
		public static Mp4MaximumDataRateBoxData WithBytes(
			this Mp4MaximumDataRateBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Bytes = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4TotalBytesSentFromMediaTracksBoxData.BytesSent" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TotalBytesSentFromMediaTracksBoxData.BytesSent" />
		///   property.
		/// </returns>
		public static Mp4TotalBytesSentFromMediaTracksBoxData WithBytesSent(
			this Mp4TotalBytesSentFromMediaTracksBoxData sourceBox,
			ulong valueToReplaceWith)
		{
			sourceBox.BytesSent = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4TotalBytesSentImmediateModeBoxData.BytesSent" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TotalBytesSentImmediateModeBoxData.BytesSent" />
		///   property.
		/// </returns>
		public static Mp4TotalBytesSentImmediateModeBoxData WithBytesSent(
			this Mp4TotalBytesSentImmediateModeBoxData sourceBox,
			ulong valueToReplaceWith)
		{
			sourceBox.BytesSent = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4TotalBytesInRepeatedPacketsBoxData.BytesSent" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TotalBytesInRepeatedPacketsBoxData.BytesSent" />
		///   property.
		/// </returns>
		public static Mp4TotalBytesInRepeatedPacketsBoxData WithBytesSent(
			this Mp4TotalBytesInRepeatedPacketsBoxData sourceBox,
			ulong valueToReplaceWith)
		{
			sourceBox.BytesSent = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4SmallestRelativeTransmittionTimeMillisecondsBoxData.Time" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SmallestRelativeTransmittionTimeMillisecondsBoxData.Time" />
		///   property.
		/// </returns>
		public static Mp4SmallestRelativeTransmittionTimeMillisecondsBoxData WithTime(
			this Mp4SmallestRelativeTransmittionTimeMillisecondsBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Time = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4LargestRelativeTransmittionTimeMillisecondsBoxData.Time" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4LargestRelativeTransmittionTimeMillisecondsBoxData.Time" />
		///   property.
		/// </returns>
		public static Mp4LargestRelativeTransmittionTimeMillisecondsBoxData WithTime(
			this Mp4LargestRelativeTransmittionTimeMillisecondsBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Time = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4LargestPacketSentIncludingRtpHeaderBoxData.Bytes" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4LargestPacketSentIncludingRtpHeaderBoxData.Bytes" />
		///   property.
		/// </returns>
		public static Mp4LargestPacketSentIncludingRtpHeaderBoxData WithBytes(
			this Mp4LargestPacketSentIncludingRtpHeaderBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Bytes = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4LongestPacketDurationInMillisecondsBoxData.Time" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4LongestPacketDurationInMillisecondsBoxData.Time" />
		///   property.
		/// </returns>
		public static Mp4LongestPacketDurationInMillisecondsBoxData WithTime(
			this Mp4LongestPacketDurationInMillisecondsBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Time = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4PayloadIdUsedInRtpPacketsBoxData.PayloadId" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4PayloadIdUsedInRtpPacketsBoxData.PayloadId" />
		///   property.
		/// </returns>
		public static Mp4PayloadIdUsedInRtpPacketsBoxData WithPayloadId(
			this Mp4PayloadIdUsedInRtpPacketsBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.PayloadId = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4StereoVideoInfoBoxData.Reserved" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4StereoVideoInfoBoxData.Reserved" />
		///   property.
		/// </returns>
		public static Mp4StereoVideoInfoBoxData WithReserved(
			this Mp4StereoVideoInfoBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Reserved = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4StereoVideoInfoBoxData.SingleViewAllowed" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4StereoVideoInfoBoxData.SingleViewAllowed" />
		///   property.
		/// </returns>
		public static Mp4StereoVideoInfoBoxData WithSingleViewAllowed(
			this Mp4StereoVideoInfoBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.SingleViewAllowed = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4StereoVideoInfoBoxData.StereoScheme" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4StereoVideoInfoBoxData.StereoScheme" />
		///   property.
		/// </returns>
		public static Mp4StereoVideoInfoBoxData WithStereoScheme(
			this Mp4StereoVideoInfoBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.StereoScheme = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4StereoVideoInfoBoxData.Length" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4StereoVideoInfoBoxData.Length" />
		///   property.
		/// </returns>
		public static Mp4StereoVideoInfoBoxData WithLength(
			this Mp4StereoVideoInfoBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Length = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4StereoVideoInfoBoxData.StereoIndicationType" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4StereoVideoInfoBoxData.StereoIndicationType" />
		///   property.
		/// </returns>
		public static Mp4StereoVideoInfoBoxData WithStereoIndicationType(
			this Mp4StereoVideoInfoBoxData sourceBox,
			byte[] valueToReplaceWith)
		{
			sourceBox.StereoIndicationType = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4StereoVideoInfoBoxData.AnyBox" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4StereoVideoInfoBoxData.AnyBox" />
		///   property.
		/// </returns>
		public static Mp4StereoVideoInfoBoxData WithAnyBox(
			this Mp4StereoVideoInfoBoxData sourceBox,
			IList<Mp4Box> valueToReplaceWith)
		{
			sourceBox.AnyBox = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4StereoVideoInfoBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4StereoVideoInfoBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4StereoVideoInfoBoxData WithVersion(
			this Mp4StereoVideoInfoBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4StereoVideoInfoBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4StereoVideoInfoBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4StereoVideoInfoBoxData WithFlags(
			this Mp4StereoVideoInfoBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ExtendedLanguageBoxData.ExtendedLanguage" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ExtendedLanguageBoxData.ExtendedLanguage" />
		///   property.
		/// </returns>
		public static Mp4ExtendedLanguageBoxData WithExtendedLanguage(
			this Mp4ExtendedLanguageBoxData sourceBox,
			string valueToReplaceWith)
		{
			sourceBox.ExtendedLanguage = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ExtendedLanguageBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ExtendedLanguageBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4ExtendedLanguageBoxData WithVersion(
			this Mp4ExtendedLanguageBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ExtendedLanguageBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ExtendedLanguageBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4ExtendedLanguageBoxData WithFlags(
			this Mp4ExtendedLanguageBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4BitRateInformationBoxData.BufferSizeDb" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4BitRateInformationBoxData.BufferSizeDb" />
		///   property.
		/// </returns>
		public static Mp4BitRateInformationBoxData WithBufferSizeDb(
			this Mp4BitRateInformationBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.BufferSizeDb = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4BitRateInformationBoxData.MaxBitRate" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4BitRateInformationBoxData.MaxBitRate" />
		///   property.
		/// </returns>
		public static Mp4BitRateInformationBoxData WithMaxBitRate(
			this Mp4BitRateInformationBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.MaxBitRate = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4BitRateInformationBoxData.AverageBitRate" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4BitRateInformationBoxData.AverageBitRate" />
		///   property.
		/// </returns>
		public static Mp4BitRateInformationBoxData WithAverageBitRate(
			this Mp4BitRateInformationBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.AverageBitRate = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4PixelAspectRatioBoxData.HSpacing" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4PixelAspectRatioBoxData.HSpacing" />
		///   property.
		/// </returns>
		public static Mp4PixelAspectRatioBoxData WithHSpacing(
			this Mp4PixelAspectRatioBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.HSpacing = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4PixelAspectRatioBoxData.VSpacing" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4PixelAspectRatioBoxData.VSpacing" />
		///   property.
		/// </returns>
		public static Mp4PixelAspectRatioBoxData WithVSpacing(
			this Mp4PixelAspectRatioBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.VSpacing = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4CleanApertureBoxData.CleanApertureWidthN" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4CleanApertureBoxData.CleanApertureWidthN" />
		///   property.
		/// </returns>
		public static Mp4CleanApertureBoxData WithCleanApertureWidthN(
			this Mp4CleanApertureBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.CleanApertureWidthN = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4CleanApertureBoxData.CleanApertureWidthD" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4CleanApertureBoxData.CleanApertureWidthD" />
		///   property.
		/// </returns>
		public static Mp4CleanApertureBoxData WithCleanApertureWidthD(
			this Mp4CleanApertureBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.CleanApertureWidthD = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4CleanApertureBoxData.CleanApertureHeightN" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4CleanApertureBoxData.CleanApertureHeightN" />
		///   property.
		/// </returns>
		public static Mp4CleanApertureBoxData WithCleanApertureHeightN(
			this Mp4CleanApertureBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.CleanApertureHeightN = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4CleanApertureBoxData.CleanApertureHeightD" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4CleanApertureBoxData.CleanApertureHeightD" />
		///   property.
		/// </returns>
		public static Mp4CleanApertureBoxData WithCleanApertureHeightD(
			this Mp4CleanApertureBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.CleanApertureHeightD = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4CleanApertureBoxData.HorizOffN" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4CleanApertureBoxData.HorizOffN" />
		///   property.
		/// </returns>
		public static Mp4CleanApertureBoxData WithHorizOffN(
			this Mp4CleanApertureBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.HorizOffN = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4CleanApertureBoxData.HorizOffD" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4CleanApertureBoxData.HorizOffD" />
		///   property.
		/// </returns>
		public static Mp4CleanApertureBoxData WithHorizOffD(
			this Mp4CleanApertureBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.HorizOffD = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4CleanApertureBoxData.VertOffN" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4CleanApertureBoxData.VertOffN" />
		///   property.
		/// </returns>
		public static Mp4CleanApertureBoxData WithVertOffN(
			this Mp4CleanApertureBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.VertOffN = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4CleanApertureBoxData.VertOffD" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4CleanApertureBoxData.VertOffD" />
		///   property.
		/// </returns>
		public static Mp4CleanApertureBoxData WithVertOffD(
			this Mp4CleanApertureBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.VertOffD = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ContentColourVolumeBoxData.Reserved1" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ContentColourVolumeBoxData.Reserved1" />
		///   property.
		/// </returns>
		public static Mp4ContentColourVolumeBoxData WithReserved1(
			this Mp4ContentColourVolumeBoxData sourceBox,
			bool valueToReplaceWith)
		{
			sourceBox.Reserved1 = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ContentColourVolumeBoxData.Reserved2" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ContentColourVolumeBoxData.Reserved2" />
		///   property.
		/// </returns>
		public static Mp4ContentColourVolumeBoxData WithReserved2(
			this Mp4ContentColourVolumeBoxData sourceBox,
			bool valueToReplaceWith)
		{
			sourceBox.Reserved2 = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ContentColourVolumeBoxData.CcvPrimariesPresentFlag" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ContentColourVolumeBoxData.CcvPrimariesPresentFlag" />
		///   property.
		/// </returns>
		public static Mp4ContentColourVolumeBoxData WithCcvPrimariesPresentFlag(
			this Mp4ContentColourVolumeBoxData sourceBox,
			bool valueToReplaceWith)
		{
			sourceBox.CcvPrimariesPresentFlag = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ContentColourVolumeBoxData.CcvMinLuminanceValuePresentFlag" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ContentColourVolumeBoxData.CcvMinLuminanceValuePresentFlag" />
		///   property.
		/// </returns>
		public static Mp4ContentColourVolumeBoxData WithCcvMinLuminanceValuePresentFlag(
			this Mp4ContentColourVolumeBoxData sourceBox,
			bool valueToReplaceWith)
		{
			sourceBox.CcvMinLuminanceValuePresentFlag = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ContentColourVolumeBoxData.CcvMaxLuminanceValuePresentFlag" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ContentColourVolumeBoxData.CcvMaxLuminanceValuePresentFlag" />
		///   property.
		/// </returns>
		public static Mp4ContentColourVolumeBoxData WithCcvMaxLuminanceValuePresentFlag(
			this Mp4ContentColourVolumeBoxData sourceBox,
			bool valueToReplaceWith)
		{
			sourceBox.CcvMaxLuminanceValuePresentFlag = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ContentColourVolumeBoxData.CcvAvgLuminanceValuePresentFlag" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ContentColourVolumeBoxData.CcvAvgLuminanceValuePresentFlag" />
		///   property.
		/// </returns>
		public static Mp4ContentColourVolumeBoxData WithCcvAvgLuminanceValuePresentFlag(
			this Mp4ContentColourVolumeBoxData sourceBox,
			bool valueToReplaceWith)
		{
			sourceBox.CcvAvgLuminanceValuePresentFlag = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ContentColourVolumeBoxData.CcvReservedZero2Bits" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ContentColourVolumeBoxData.CcvReservedZero2Bits" />
		///   property.
		/// </returns>
		public static Mp4ContentColourVolumeBoxData WithCcvReservedZero2Bits(
			this Mp4ContentColourVolumeBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.CcvReservedZero2Bits = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ContentColourVolumeBoxData.CcvPrimariesX" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ContentColourVolumeBoxData.CcvPrimariesX" />
		///   property.
		/// </returns>
		public static Mp4ContentColourVolumeBoxData WithCcvPrimariesX(
			this Mp4ContentColourVolumeBoxData sourceBox,
			IList<uint> valueToReplaceWith)
		{
			sourceBox.CcvPrimariesX = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ContentColourVolumeBoxData.CcvPrimariesY" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ContentColourVolumeBoxData.CcvPrimariesY" />
		///   property.
		/// </returns>
		public static Mp4ContentColourVolumeBoxData WithCcvPrimariesY(
			this Mp4ContentColourVolumeBoxData sourceBox,
			IList<uint> valueToReplaceWith)
		{
			sourceBox.CcvPrimariesY = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ContentColourVolumeBoxData.CcvMinLuminanceValue" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ContentColourVolumeBoxData.CcvMinLuminanceValue" />
		///   property.
		/// </returns>
		public static Mp4ContentColourVolumeBoxData WithCcvMinLuminanceValue(
			this Mp4ContentColourVolumeBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.CcvMinLuminanceValue = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ContentColourVolumeBoxData.CcvMaxLuminanceValue" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ContentColourVolumeBoxData.CcvMaxLuminanceValue" />
		///   property.
		/// </returns>
		public static Mp4ContentColourVolumeBoxData WithCcvMaxLuminanceValue(
			this Mp4ContentColourVolumeBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.CcvMaxLuminanceValue = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ContentColourVolumeBoxData.CcvAvgLuminanceValue" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ContentColourVolumeBoxData.CcvAvgLuminanceValue" />
		///   property.
		/// </returns>
		public static Mp4ContentColourVolumeBoxData WithCcvAvgLuminanceValue(
			this Mp4ContentColourVolumeBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.CcvAvgLuminanceValue = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ColorInformationBoxData.ColourType" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ColorInformationBoxData.ColourType" />
		///   property.
		/// </returns>
		public static Mp4ColorInformationBoxData WithColourType(
			this Mp4ColorInformationBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.ColourType = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ColorInformationBoxData.ColourPrimaries" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ColorInformationBoxData.ColourPrimaries" />
		///   property.
		/// </returns>
		public static Mp4ColorInformationBoxData WithColourPrimaries(
			this Mp4ColorInformationBoxData sourceBox,
			ushort valueToReplaceWith)
		{
			sourceBox.ColourPrimaries = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ColorInformationBoxData.TransferCharacteristics" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ColorInformationBoxData.TransferCharacteristics" />
		///   property.
		/// </returns>
		public static Mp4ColorInformationBoxData WithTransferCharacteristics(
			this Mp4ColorInformationBoxData sourceBox,
			ushort valueToReplaceWith)
		{
			sourceBox.TransferCharacteristics = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ColorInformationBoxData.MatrixCoefficients" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ColorInformationBoxData.MatrixCoefficients" />
		///   property.
		/// </returns>
		public static Mp4ColorInformationBoxData WithMatrixCoefficients(
			this Mp4ColorInformationBoxData sourceBox,
			ushort valueToReplaceWith)
		{
			sourceBox.MatrixCoefficients = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ColorInformationBoxData.FullRangeFlag" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ColorInformationBoxData.FullRangeFlag" />
		///   property.
		/// </returns>
		public static Mp4ColorInformationBoxData WithFullRangeFlag(
			this Mp4ColorInformationBoxData sourceBox,
			bool valueToReplaceWith)
		{
			sourceBox.FullRangeFlag = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ColorInformationBoxData.Reserved" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ColorInformationBoxData.Reserved" />
		///   property.
		/// </returns>
		public static Mp4ColorInformationBoxData WithReserved(
			this Mp4ColorInformationBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Reserved = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ContentLightLevelBoxData.MaxContentLightLevel" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ContentLightLevelBoxData.MaxContentLightLevel" />
		///   property.
		/// </returns>
		public static Mp4ContentLightLevelBoxData WithMaxContentLightLevel(
			this Mp4ContentLightLevelBoxData sourceBox,
			ushort valueToReplaceWith)
		{
			sourceBox.MaxContentLightLevel = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ContentLightLevelBoxData.MaxPicAverageLightLevel" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ContentLightLevelBoxData.MaxPicAverageLightLevel" />
		///   property.
		/// </returns>
		public static Mp4ContentLightLevelBoxData WithMaxPicAverageLightLevel(
			this Mp4ContentLightLevelBoxData sourceBox,
			ushort valueToReplaceWith)
		{
			sourceBox.MaxPicAverageLightLevel = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4MasteringDisplayColourVolumeBoxData.DisplayPrimariesX" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MasteringDisplayColourVolumeBoxData.DisplayPrimariesX" />
		///   property.
		/// </returns>
		public static Mp4MasteringDisplayColourVolumeBoxData WithDisplayPrimariesX(
			this Mp4MasteringDisplayColourVolumeBoxData sourceBox,
			ushort[] valueToReplaceWith)
		{
			sourceBox.DisplayPrimariesX = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4MasteringDisplayColourVolumeBoxData.DisplayPrimariesY" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MasteringDisplayColourVolumeBoxData.DisplayPrimariesY" />
		///   property.
		/// </returns>
		public static Mp4MasteringDisplayColourVolumeBoxData WithDisplayPrimariesY(
			this Mp4MasteringDisplayColourVolumeBoxData sourceBox,
			ushort[] valueToReplaceWith)
		{
			sourceBox.DisplayPrimariesY = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4MasteringDisplayColourVolumeBoxData.WhitePointX" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MasteringDisplayColourVolumeBoxData.WhitePointX" />
		///   property.
		/// </returns>
		public static Mp4MasteringDisplayColourVolumeBoxData WithWhitePointX(
			this Mp4MasteringDisplayColourVolumeBoxData sourceBox,
			ushort valueToReplaceWith)
		{
			sourceBox.WhitePointX = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4MasteringDisplayColourVolumeBoxData.WhitePointY" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MasteringDisplayColourVolumeBoxData.WhitePointY" />
		///   property.
		/// </returns>
		public static Mp4MasteringDisplayColourVolumeBoxData WithWhitePointY(
			this Mp4MasteringDisplayColourVolumeBoxData sourceBox,
			ushort valueToReplaceWith)
		{
			sourceBox.WhitePointY = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4MasteringDisplayColourVolumeBoxData.MaxDisplayMasteringLuminance" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MasteringDisplayColourVolumeBoxData.MaxDisplayMasteringLuminance" />
		///   property.
		/// </returns>
		public static Mp4MasteringDisplayColourVolumeBoxData WithMaxDisplayMasteringLuminance(
			this Mp4MasteringDisplayColourVolumeBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.MaxDisplayMasteringLuminance = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4MasteringDisplayColourVolumeBoxData.MinDisplayMasteringLuminance" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MasteringDisplayColourVolumeBoxData.MinDisplayMasteringLuminance" />
		///   property.
		/// </returns>
		public static Mp4MasteringDisplayColourVolumeBoxData WithMinDisplayMasteringLuminance(
			this Mp4MasteringDisplayColourVolumeBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.MinDisplayMasteringLuminance = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ScrambleSchemeInfoBoxBoxData.SchemeTypeBox" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ScrambleSchemeInfoBoxBoxData.SchemeTypeBox" />
		///   property.
		/// </returns>
		public static Mp4ScrambleSchemeInfoBoxBoxData WithSchemeTypeBox(
			this Mp4ScrambleSchemeInfoBoxBoxData sourceBox,
			Mp4Box valueToReplaceWith)
		{
			sourceBox.SchemeTypeBox = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ScrambleSchemeInfoBoxBoxData.Info" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ScrambleSchemeInfoBoxBoxData.Info" />
		///   property.
		/// </returns>
		public static Mp4ScrambleSchemeInfoBoxBoxData WithInfo(
			this Mp4ScrambleSchemeInfoBoxBoxData sourceBox,
			Mp4Box valueToReplaceWith)
		{
			sourceBox.Info = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4SamplingRateBoxData.SamplingRate" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SamplingRateBoxData.SamplingRate" />
		///   property.
		/// </returns>
		public static Mp4SamplingRateBoxData WithSamplingRate(
			this Mp4SamplingRateBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.SamplingRate = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4TextStreamConfigurationBoxData.TextConfig" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TextStreamConfigurationBoxData.TextConfig" />
		///   property.
		/// </returns>
		public static Mp4TextStreamConfigurationBoxData WithTextConfig(
			this Mp4TextStreamConfigurationBoxData sourceBox,
			string valueToReplaceWith)
		{
			sourceBox.TextConfig = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4UriInformationBoxData.UriInitializationData" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4UriInformationBoxData.UriInitializationData" />
		///   property.
		/// </returns>
		public static Mp4UriInformationBoxData WithUriInitializationData(
			this Mp4UriInformationBoxData sourceBox,
			byte[] valueToReplaceWith)
		{
			sourceBox.UriInitializationData = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4UriInformationBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4UriInformationBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4UriInformationBoxData WithVersion(
			this Mp4UriInformationBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4UriInformationBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4UriInformationBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4UriInformationBoxData WithFlags(
			this Mp4UriInformationBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4CopyrightBoxData.Pad" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4CopyrightBoxData.Pad" />
		///   property.
		/// </returns>
		public static Mp4CopyrightBoxData WithPad(
			this Mp4CopyrightBoxData sourceBox,
			bool valueToReplaceWith)
		{
			sourceBox.Pad = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4CopyrightBoxData.Language" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4CopyrightBoxData.Language" />
		///   property.
		/// </returns>
		public static Mp4CopyrightBoxData WithLanguage(
			this Mp4CopyrightBoxData sourceBox,
			byte[] valueToReplaceWith)
		{
			sourceBox.Language = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4CopyrightBoxData.Notice" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4CopyrightBoxData.Notice" />
		///   property.
		/// </returns>
		public static Mp4CopyrightBoxData WithNotice(
			this Mp4CopyrightBoxData sourceBox,
			string valueToReplaceWith)
		{
			sourceBox.Notice = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4CopyrightBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4CopyrightBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4CopyrightBoxData WithVersion(
			this Mp4CopyrightBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4CopyrightBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4CopyrightBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4CopyrightBoxData WithFlags(
			this Mp4CopyrightBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4TrackKindBoxData.SchemeUri" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackKindBoxData.SchemeUri" />
		///   property.
		/// </returns>
		public static Mp4TrackKindBoxData WithSchemeUri(
			this Mp4TrackKindBoxData sourceBox,
			string valueToReplaceWith)
		{
			sourceBox.SchemeUri = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4TrackKindBoxData.Value" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackKindBoxData.Value" />
		///   property.
		/// </returns>
		public static Mp4TrackKindBoxData WithValue(
			this Mp4TrackKindBoxData sourceBox,
			string valueToReplaceWith)
		{
			sourceBox.Value = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4TrackKindBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackKindBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4TrackKindBoxData WithVersion(
			this Mp4TrackKindBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4TrackKindBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackKindBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4TrackKindBoxData WithFlags(
			this Mp4TrackKindBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4TrackSelectionBoxData.SwitchGroup" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackSelectionBoxData.SwitchGroup" />
		///   property.
		/// </returns>
		public static Mp4TrackSelectionBoxData WithSwitchGroup(
			this Mp4TrackSelectionBoxData sourceBox,
			int valueToReplaceWith)
		{
			sourceBox.SwitchGroup = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4TrackSelectionBoxData.AttributeList" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackSelectionBoxData.AttributeList" />
		///   property.
		/// </returns>
		public static Mp4TrackSelectionBoxData WithAttributeList(
			this Mp4TrackSelectionBoxData sourceBox,
			uint[] valueToReplaceWith)
		{
			sourceBox.AttributeList = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4TrackSelectionBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackSelectionBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4TrackSelectionBoxData WithVersion(
			this Mp4TrackSelectionBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4TrackSelectionBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackSelectionBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4TrackSelectionBoxData WithFlags(
			this Mp4TrackSelectionBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4SdpInformationBoxData.SdpText" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SdpInformationBoxData.SdpText" />
		///   property.
		/// </returns>
		public static Mp4SdpInformationBoxData WithSdpText(
			this Mp4SdpInformationBoxData sourceBox,
			string valueToReplaceWith)
		{
			sourceBox.SdpText = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4RtpInformationBoxData.DescriptionFormat" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4RtpInformationBoxData.DescriptionFormat" />
		///   property.
		/// </returns>
		public static Mp4RtpInformationBoxData WithDescriptionFormat(
			this Mp4RtpInformationBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.DescriptionFormat = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4RtpInformationBoxData.SdpText" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4RtpInformationBoxData.SdpText" />
		///   property.
		/// </returns>
		public static Mp4RtpInformationBoxData WithSdpText(
			this Mp4RtpInformationBoxData sourceBox,
			string valueToReplaceWith)
		{
			sourceBox.SdpText = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4UrlBoxBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4UrlBoxBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4UrlBoxBoxData WithFlags(
			this Mp4UrlBoxBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4UrlBoxBoxData.Location" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4UrlBoxBoxData.Location" />
		///   property.
		/// </returns>
		public static Mp4UrlBoxBoxData WithLocation(
			this Mp4UrlBoxBoxData sourceBox,
			string valueToReplaceWith)
		{
			sourceBox.Location = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4UrnBoxBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4UrnBoxBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4UrnBoxBoxData WithFlags(
			this Mp4UrnBoxBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4UrnBoxBoxData.Name" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4UrnBoxBoxData.Name" />
		///   property.
		/// </returns>
		public static Mp4UrnBoxBoxData WithName(
			this Mp4UrnBoxBoxData sourceBox,
			string valueToReplaceWith)
		{
			sourceBox.Name = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4UrnBoxBoxData.Location" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4UrnBoxBoxData.Location" />
		///   property.
		/// </returns>
		public static Mp4UrnBoxBoxData WithLocation(
			this Mp4UrnBoxBoxData sourceBox,
			string valueToReplaceWith)
		{
			sourceBox.Location = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4IdentifiedMediaDataBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4IdentifiedMediaDataBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4IdentifiedMediaDataBoxData WithFlags(
			this Mp4IdentifiedMediaDataBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4IdentifiedMediaDataBoxData.ImdaRefIdentifier" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4IdentifiedMediaDataBoxData.ImdaRefIdentifier" />
		///   property.
		/// </returns>
		public static Mp4IdentifiedMediaDataBoxData WithImdaRefIdentifier(
			this Mp4IdentifiedMediaDataBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.ImdaRefIdentifier = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4SequenceNumberIdentifiedMediaDataBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SequenceNumberIdentifiedMediaDataBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4SequenceNumberIdentifiedMediaDataBoxData WithFlags(
			this Mp4SequenceNumberIdentifiedMediaDataBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ItemPropertyContainerBoxBoxData.Properties" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ItemPropertyContainerBoxBoxData.Properties" />
		///   property.
		/// </returns>
		public static Mp4ItemPropertyContainerBoxBoxData WithProperties(
			this Mp4ItemPropertyContainerBoxBoxData sourceBox,
			IList<Mp4Box> valueToReplaceWith)
		{
			sourceBox.Properties = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ItemPropertyAssociationBoxData.EntryCount" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ItemPropertyAssociationBoxData.EntryCount" />
		///   property.
		/// </returns>
		public static Mp4ItemPropertyAssociationBoxData WithEntryCount(
			this Mp4ItemPropertyAssociationBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.EntryCount = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ItemPropertyAssociationBoxData.ItemId" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ItemPropertyAssociationBoxData.ItemId" />
		///   property.
		/// </returns>
		public static Mp4ItemPropertyAssociationBoxData WithItemId(
			this Mp4ItemPropertyAssociationBoxData sourceBox,
			IList<uint> valueToReplaceWith)
		{
			sourceBox.ItemId = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ItemPropertyAssociationBoxData.AssociationCount" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ItemPropertyAssociationBoxData.AssociationCount" />
		///   property.
		/// </returns>
		public static Mp4ItemPropertyAssociationBoxData WithAssociationCount(
			this Mp4ItemPropertyAssociationBoxData sourceBox,
			IList<byte> valueToReplaceWith)
		{
			sourceBox.AssociationCount = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ItemPropertyAssociationBoxData.Essential" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ItemPropertyAssociationBoxData.Essential" />
		///   property.
		/// </returns>
		public static Mp4ItemPropertyAssociationBoxData WithEssential(
			this Mp4ItemPropertyAssociationBoxData sourceBox,
			IList<IList<bool>> valueToReplaceWith)
		{
			sourceBox.Essential = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ItemPropertyAssociationBoxData.PropertyIndex" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ItemPropertyAssociationBoxData.PropertyIndex" />
		///   property.
		/// </returns>
		public static Mp4ItemPropertyAssociationBoxData WithPropertyIndex(
			this Mp4ItemPropertyAssociationBoxData sourceBox,
			IList<IList<ushort>> valueToReplaceWith)
		{
			sourceBox.PropertyIndex = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ItemPropertyAssociationBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ItemPropertyAssociationBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4ItemPropertyAssociationBoxData WithVersion(
			this Mp4ItemPropertyAssociationBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ItemPropertyAssociationBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ItemPropertyAssociationBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4ItemPropertyAssociationBoxData WithFlags(
			this Mp4ItemPropertyAssociationBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ItemPropertiesBoxData.PropertyContainer" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ItemPropertiesBoxData.PropertyContainer" />
		///   property.
		/// </returns>
		public static Mp4ItemPropertiesBoxData WithPropertyContainer(
			this Mp4ItemPropertiesBoxData sourceBox,
			IList<Mp4Box> valueToReplaceWith)
		{
			sourceBox.PropertyContainer = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ItemPropertiesBoxData.Association" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ItemPropertiesBoxData.Association" />
		///   property.
		/// </returns>
		public static Mp4ItemPropertiesBoxData WithAssociation(
			this Mp4ItemPropertiesBoxData sourceBox,
			IList<Mp4Box> valueToReplaceWith)
		{
			sourceBox.Association = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ActiveSequenceStartupPropertiesBoxData.MinInitialStartupOffset" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ActiveSequenceStartupPropertiesBoxData.MinInitialStartupOffset" />
		///   property.
		/// </returns>
		public static Mp4ActiveSequenceStartupPropertiesBoxData WithMinInitialStartupOffset(
			this Mp4ActiveSequenceStartupPropertiesBoxData sourceBox,
			int valueToReplaceWith)
		{
			sourceBox.MinInitialStartupOffset = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ActiveSequenceStartupPropertiesBoxData.NumEntries" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ActiveSequenceStartupPropertiesBoxData.NumEntries" />
		///   property.
		/// </returns>
		public static Mp4ActiveSequenceStartupPropertiesBoxData WithNumEntries(
			this Mp4ActiveSequenceStartupPropertiesBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.NumEntries = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ActiveSequenceStartupPropertiesBoxData.GroupingTypeParameter" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ActiveSequenceStartupPropertiesBoxData.GroupingTypeParameter" />
		///   property.
		/// </returns>
		public static Mp4ActiveSequenceStartupPropertiesBoxData WithGroupingTypeParameter(
			this Mp4ActiveSequenceStartupPropertiesBoxData sourceBox,
			uint[] valueToReplaceWith)
		{
			sourceBox.GroupingTypeParameter = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ActiveSequenceStartupPropertiesBoxData.MinInitialStartupOffset2" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ActiveSequenceStartupPropertiesBoxData.MinInitialStartupOffset2" />
		///   property.
		/// </returns>
		public static Mp4ActiveSequenceStartupPropertiesBoxData WithMinInitialStartupOffset2(
			this Mp4ActiveSequenceStartupPropertiesBoxData sourceBox,
			uint[] valueToReplaceWith)
		{
			sourceBox.MinInitialStartupOffset2 = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ActiveSequenceStartupPropertiesBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ActiveSequenceStartupPropertiesBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4ActiveSequenceStartupPropertiesBoxData WithVersion(
			this Mp4ActiveSequenceStartupPropertiesBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ActiveSequenceStartupPropertiesBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ActiveSequenceStartupPropertiesBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4ActiveSequenceStartupPropertiesBoxData WithFlags(
			this Mp4ActiveSequenceStartupPropertiesBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4BinaryXmlBoxData.Data" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4BinaryXmlBoxData.Data" />
		///   property.
		/// </returns>
		public static Mp4BinaryXmlBoxData WithData(
			this Mp4BinaryXmlBoxData sourceBox,
			byte[] valueToReplaceWith)
		{
			sourceBox.Data = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4BinaryXmlBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4BinaryXmlBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4BinaryXmlBoxData WithVersion(
			this Mp4BinaryXmlBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4BinaryXmlBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4BinaryXmlBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4BinaryXmlBoxData WithFlags(
			this Mp4BinaryXmlBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4CompleteTrackInformationBoxData.OriginalFormat" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4CompleteTrackInformationBoxData.OriginalFormat" />
		///   property.
		/// </returns>
		public static Mp4CompleteTrackInformationBoxData WithOriginalFormat(
			this Mp4CompleteTrackInformationBoxData sourceBox,
			Mp4Box valueToReplaceWith)
		{
			sourceBox.OriginalFormat = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ChunkLargeOffsetBoxData.EntryCount" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ChunkLargeOffsetBoxData.EntryCount" />
		///   property.
		/// </returns>
		public static Mp4ChunkLargeOffsetBoxData WithEntryCount(
			this Mp4ChunkLargeOffsetBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.EntryCount = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ChunkLargeOffsetBoxData.ChunkOffset" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ChunkLargeOffsetBoxData.ChunkOffset" />
		///   property.
		/// </returns>
		public static Mp4ChunkLargeOffsetBoxData WithChunkOffset(
			this Mp4ChunkLargeOffsetBoxData sourceBox,
			IList<ulong> valueToReplaceWith)
		{
			sourceBox.ChunkOffset = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ChunkLargeOffsetBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ChunkLargeOffsetBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4ChunkLargeOffsetBoxData WithVersion(
			this Mp4ChunkLargeOffsetBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ChunkLargeOffsetBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ChunkLargeOffsetBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4ChunkLargeOffsetBoxData WithFlags(
			this Mp4ChunkLargeOffsetBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4CompositionToDecodeTimelineMappingBoxData.CompositionToDtsShift" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4CompositionToDecodeTimelineMappingBoxData.CompositionToDtsShift" />
		///   property.
		/// </returns>
		public static Mp4CompositionToDecodeTimelineMappingBoxData WithCompositionToDtsShift(
			this Mp4CompositionToDecodeTimelineMappingBoxData sourceBox,
			long valueToReplaceWith)
		{
			sourceBox.CompositionToDtsShift = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4CompositionToDecodeTimelineMappingBoxData.LeastDecodeToDisplayDelta" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4CompositionToDecodeTimelineMappingBoxData.LeastDecodeToDisplayDelta" />
		///   property.
		/// </returns>
		public static Mp4CompositionToDecodeTimelineMappingBoxData WithLeastDecodeToDisplayDelta(
			this Mp4CompositionToDecodeTimelineMappingBoxData sourceBox,
			long valueToReplaceWith)
		{
			sourceBox.LeastDecodeToDisplayDelta = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4CompositionToDecodeTimelineMappingBoxData.GreatestDecodeToDisplayDelta" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4CompositionToDecodeTimelineMappingBoxData.GreatestDecodeToDisplayDelta" />
		///   property.
		/// </returns>
		public static Mp4CompositionToDecodeTimelineMappingBoxData WithGreatestDecodeToDisplayDelta(
			this Mp4CompositionToDecodeTimelineMappingBoxData sourceBox,
			long valueToReplaceWith)
		{
			sourceBox.GreatestDecodeToDisplayDelta = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4CompositionToDecodeTimelineMappingBoxData.CompositionStartTime" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4CompositionToDecodeTimelineMappingBoxData.CompositionStartTime" />
		///   property.
		/// </returns>
		public static Mp4CompositionToDecodeTimelineMappingBoxData WithCompositionStartTime(
			this Mp4CompositionToDecodeTimelineMappingBoxData sourceBox,
			long valueToReplaceWith)
		{
			sourceBox.CompositionStartTime = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4CompositionToDecodeTimelineMappingBoxData.CompositionEndTime" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4CompositionToDecodeTimelineMappingBoxData.CompositionEndTime" />
		///   property.
		/// </returns>
		public static Mp4CompositionToDecodeTimelineMappingBoxData WithCompositionEndTime(
			this Mp4CompositionToDecodeTimelineMappingBoxData sourceBox,
			long valueToReplaceWith)
		{
			sourceBox.CompositionEndTime = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4CompositionToDecodeTimelineMappingBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4CompositionToDecodeTimelineMappingBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4CompositionToDecodeTimelineMappingBoxData WithVersion(
			this Mp4CompositionToDecodeTimelineMappingBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4CompositionToDecodeTimelineMappingBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4CompositionToDecodeTimelineMappingBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4CompositionToDecodeTimelineMappingBoxData WithFlags(
			this Mp4CompositionToDecodeTimelineMappingBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4CompositionTimeToSampleBoxData.EntryCount" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4CompositionTimeToSampleBoxData.EntryCount" />
		///   property.
		/// </returns>
		public static Mp4CompositionTimeToSampleBoxData WithEntryCount(
			this Mp4CompositionTimeToSampleBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.EntryCount = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4CompositionTimeToSampleBoxData.SampleCount" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4CompositionTimeToSampleBoxData.SampleCount" />
		///   property.
		/// </returns>
		public static Mp4CompositionTimeToSampleBoxData WithSampleCount(
			this Mp4CompositionTimeToSampleBoxData sourceBox,
			IList<uint> valueToReplaceWith)
		{
			sourceBox.SampleCount = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4CompositionTimeToSampleBoxData.SampleOffset" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4CompositionTimeToSampleBoxData.SampleOffset" />
		///   property.
		/// </returns>
		public static Mp4CompositionTimeToSampleBoxData WithSampleOffset(
			this Mp4CompositionTimeToSampleBoxData sourceBox,
			IList<int> valueToReplaceWith)
		{
			sourceBox.SampleOffset = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4CompositionTimeToSampleBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4CompositionTimeToSampleBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4CompositionTimeToSampleBoxData WithVersion(
			this Mp4CompositionTimeToSampleBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4CompositionTimeToSampleBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4CompositionTimeToSampleBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4CompositionTimeToSampleBoxData WithFlags(
			this Mp4CompositionTimeToSampleBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4DataReferenceBoxData.EntryCount" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4DataReferenceBoxData.EntryCount" />
		///   property.
		/// </returns>
		public static Mp4DataReferenceBoxData WithEntryCount(
			this Mp4DataReferenceBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.EntryCount = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4DataReferenceBoxData.Boxes" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4DataReferenceBoxData.Boxes" />
		///   property.
		/// </returns>
		public static Mp4DataReferenceBoxData WithBoxes(
			this Mp4DataReferenceBoxData sourceBox,
			IList<Mp4Box> valueToReplaceWith)
		{
			sourceBox.Boxes = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4EditListBoxData.EntryCount" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4EditListBoxData.EntryCount" />
		///   property.
		/// </returns>
		public static Mp4EditListBoxData WithEntryCount(
			this Mp4EditListBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.EntryCount = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4EditListBoxData.EditDuration" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4EditListBoxData.EditDuration" />
		///   property.
		/// </returns>
		public static Mp4EditListBoxData WithEditDuration(
			this Mp4EditListBoxData sourceBox,
			IList<ulong> valueToReplaceWith)
		{
			sourceBox.EditDuration = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4EditListBoxData.MediaTime" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4EditListBoxData.MediaTime" />
		///   property.
		/// </returns>
		public static Mp4EditListBoxData WithMediaTime(
			this Mp4EditListBoxData sourceBox,
			IList<ulong> valueToReplaceWith)
		{
			sourceBox.MediaTime = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4EditListBoxData.MediaRateInteger" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4EditListBoxData.MediaRateInteger" />
		///   property.
		/// </returns>
		public static Mp4EditListBoxData WithMediaRateInteger(
			this Mp4EditListBoxData sourceBox,
			IList<short> valueToReplaceWith)
		{
			sourceBox.MediaRateInteger = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4EditListBoxData.MediaRateFraction" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4EditListBoxData.MediaRateFraction" />
		///   property.
		/// </returns>
		public static Mp4EditListBoxData WithMediaRateFraction(
			this Mp4EditListBoxData sourceBox,
			IList<short> valueToReplaceWith)
		{
			sourceBox.MediaRateFraction = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4EditListBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4EditListBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4EditListBoxData WithVersion(
			this Mp4EditListBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4EditListBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4EditListBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4EditListBoxData WithFlags(
			this Mp4EditListBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4ExtendedTypeBoxData.CompatibleCombinations" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ExtendedTypeBoxData.CompatibleCombinations" />
		///   property.
		/// </returns>
		public static Mp4ExtendedTypeBoxData WithCompatibleCombinations(
			this Mp4ExtendedTypeBoxData sourceBox,
			IList<Mp4Box> valueToReplaceWith)
		{
			sourceBox.CompatibleCombinations = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4FileDeliveryInformationBoxData.ContentLocation" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4FileDeliveryInformationBoxData.ContentLocation" />
		///   property.
		/// </returns>
		public static Mp4FileDeliveryInformationBoxData WithContentLocation(
			this Mp4FileDeliveryInformationBoxData sourceBox,
			string valueToReplaceWith)
		{
			sourceBox.ContentLocation = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4FileDeliveryInformationBoxData.ContentMd5" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4FileDeliveryInformationBoxData.ContentMd5" />
		///   property.
		/// </returns>
		public static Mp4FileDeliveryInformationBoxData WithContentMd5(
			this Mp4FileDeliveryInformationBoxData sourceBox,
			string valueToReplaceWith)
		{
			sourceBox.ContentMd5 = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4FileDeliveryInformationBoxData.ContentLength" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4FileDeliveryInformationBoxData.ContentLength" />
		///   property.
		/// </returns>
		public static Mp4FileDeliveryInformationBoxData WithContentLength(
			this Mp4FileDeliveryInformationBoxData sourceBox,
			ulong valueToReplaceWith)
		{
			sourceBox.ContentLength = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4FileDeliveryInformationBoxData.TransferLength" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4FileDeliveryInformationBoxData.TransferLength" />
		///   property.
		/// </returns>
		public static Mp4FileDeliveryInformationBoxData WithTransferLength(
			this Mp4FileDeliveryInformationBoxData sourceBox,
			ulong valueToReplaceWith)
		{
			sourceBox.TransferLength = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4FileDeliveryInformationBoxData.EntryCount" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4FileDeliveryInformationBoxData.EntryCount" />
		///   property.
		/// </returns>
		public static Mp4FileDeliveryInformationBoxData WithEntryCount(
			this Mp4FileDeliveryInformationBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.EntryCount = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4FileDeliveryInformationBoxData.GroupId" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4FileDeliveryInformationBoxData.GroupId" />
		///   property.
		/// </returns>
		public static Mp4FileDeliveryInformationBoxData WithGroupId(
			this Mp4FileDeliveryInformationBoxData sourceBox,
			IList<uint> valueToReplaceWith)
		{
			sourceBox.GroupId = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4FecReservoirBoxData.EntryCount" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4FecReservoirBoxData.EntryCount" />
		///   property.
		/// </returns>
		public static Mp4FecReservoirBoxData WithEntryCount(
			this Mp4FecReservoirBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.EntryCount = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4FecReservoirBoxData.ItemId" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4FecReservoirBoxData.ItemId" />
		///   property.
		/// </returns>
		public static Mp4FecReservoirBoxData WithItemId(
			this Mp4FecReservoirBoxData sourceBox,
			IList<uint> valueToReplaceWith)
		{
			sourceBox.ItemId = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4FecReservoirBoxData.SymbolCount" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4FecReservoirBoxData.SymbolCount" />
		///   property.
		/// </returns>
		public static Mp4FecReservoirBoxData WithSymbolCount(
			this Mp4FecReservoirBoxData sourceBox,
			IList<uint> valueToReplaceWith)
		{
			sourceBox.SymbolCount = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4FecReservoirBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4FecReservoirBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4FecReservoirBoxData WithVersion(
			this Mp4FecReservoirBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4FecReservoirBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4FecReservoirBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4FecReservoirBoxData WithFlags(
			this Mp4FecReservoirBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}
		/// <summary>
		///   Changes the <see cref="Mp4FreeSpaceBoxData.Data" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4FreeSpaceBoxData.Data" />
		///   property.
		/// </returns>
		public static Mp4FreeSpaceBoxData WithData(
			this Mp4FreeSpaceBoxData sourceBox,
			IList<byte> valueToReplaceWith)
		{
			sourceBox.Data = valueToReplaceWith;
			return sourceBox;
		}
	}
}

