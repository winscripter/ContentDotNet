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
	public class Mp4ScrambleSchemeInfoBoxData : IMp4BoxData, IEquatable<Mp4ScrambleSchemeInfoBoxData?>
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
		///   Initializes a new instance of the <see cref="Mp4ScrambleSchemeInfoBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="schemeTypeBox">The parameter that assigns <see cref="SchemeTypeBox" /> directly.</param>
		/// <param name="info">The parameter that assigns <see cref="Info" /> directly.</param>
		public Mp4ScrambleSchemeInfoBoxData(Mp4Box schemeTypeBox, Mp4Box info)
		{
			this.SchemeTypeBox = schemeTypeBox;
			this.Info = info;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ScrambleSchemeInfoBoxData" /> class.
		/// </summary>
		public Mp4ScrambleSchemeInfoBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4ScrambleSchemeInfoBoxData val
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
		public bool Equals(Mp4ScrambleSchemeInfoBoxData? other) => Equals((object?)other);
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
	public class Mp4SubTrackBoxData : IMp4BoxData, IEquatable<Mp4SubTrackBoxData?>
	{


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SubTrackBoxData" /> class.
		/// </summary>
		public Mp4SubTrackBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4SubTrackBoxData val
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
		public bool Equals(Mp4SubTrackBoxData? other) => Equals((object?)other);
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
	public class Mp4UrlBoxData : IMp4BoxData, IEquatable<Mp4UrlBoxData?>
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
		///   Initializes a new instance of the <see cref="Mp4UrlBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		/// <param name="location">The parameter that assigns <see cref="Location" /> directly.</param>
		public Mp4UrlBoxData(uint flags, string location)
		{
			this.Flags = flags;
			this.Location = location;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4UrlBoxData" /> class.
		/// </summary>
		public Mp4UrlBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4UrlBoxData val
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
		public bool Equals(Mp4UrlBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>urn </c> box data representation.
	/// </summary>
	[FourCC("urn ")]
	public class Mp4UrnBoxData : IMp4BoxData, IEquatable<Mp4UrnBoxData?>
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
		///   Initializes a new instance of the <see cref="Mp4UrnBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		/// <param name="name">The parameter that assigns <see cref="Name" /> directly.</param>
		/// <param name="location">The parameter that assigns <see cref="Location" /> directly.</param>
		public Mp4UrnBoxData(uint flags, string name, string location)
		{
			this.Flags = flags;
			this.Name = name;
			this.Location = location;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4UrnBoxData" /> class.
		/// </summary>
		public Mp4UrnBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4UrnBoxData val
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
		public bool Equals(Mp4UrnBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>imdt</c> box data representation.
	/// </summary>
	[FourCC("imdt")]
	public class Mp4IdentifiedMediaDataImdtBoxData : IMp4BoxData, IEquatable<Mp4IdentifiedMediaDataImdtBoxData?>
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
		///   Initializes a new instance of the <see cref="Mp4IdentifiedMediaDataImdtBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		/// <param name="imdaRefIdentifier">The parameter that assigns <see cref="ImdaRefIdentifier" /> directly.</param>
		public Mp4IdentifiedMediaDataImdtBoxData(uint flags, uint imdaRefIdentifier)
		{
			this.Flags = flags;
			this.ImdaRefIdentifier = imdaRefIdentifier;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4IdentifiedMediaDataImdtBoxData" /> class.
		/// </summary>
		public Mp4IdentifiedMediaDataImdtBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4IdentifiedMediaDataImdtBoxData val
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
		public bool Equals(Mp4IdentifiedMediaDataImdtBoxData? other) => Equals((object?)other);
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
	public class Mp4ItemPropertyContainerBoxData : IMp4BoxData, IEquatable<Mp4ItemPropertyContainerBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Properties.
		/// </summary>
		public IList<Mp4Box>? Properties { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ItemPropertyContainerBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="properties">The parameter that assigns <see cref="Properties" /> directly.</param>
		public Mp4ItemPropertyContainerBoxData(IList<Mp4Box> properties)
		{
			this.Properties = properties;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ItemPropertyContainerBoxData" /> class.
		/// </summary>
		public Mp4ItemPropertyContainerBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4ItemPropertyContainerBoxData val
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
		public bool Equals(Mp4ItemPropertyContainerBoxData? other) => Equals((object?)other);
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
		///   Represents one of MP4 box properties, named, Children.
		/// </summary>
		public IList<Mp4Box>? Children { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4DataInformationBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="children">The parameter that assigns <see cref="Children" /> directly.</param>
		public Mp4DataInformationBoxData(IList<Mp4Box> children)
		{
			this.Children = children;
		}

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
				&& EqualityComparer<IList<Mp4Box>?>.Default.Equals(this.Children, val.Children)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Children?.GetHashCode() ?? 0);
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
	///   The MP4 <c>gitn</c> box data representation.
	/// </summary>
	[FourCC("gitn")]
	public class Mp4GroupIdToNameBoxData : IMp4BoxData, IEquatable<Mp4GroupIdToNameBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, EntryCount.
		/// </summary>
		public ushort? EntryCount { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, GroupId.
		/// </summary>
		public IList<uint>? GroupId { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, GroupName.
		/// </summary>
		public IList<string>? GroupName { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4GroupIdToNameBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="entryCount">The parameter that assigns <see cref="EntryCount" /> directly.</param>
		/// <param name="groupId">The parameter that assigns <see cref="GroupId" /> directly.</param>
		/// <param name="groupName">The parameter that assigns <see cref="GroupName" /> directly.</param>
		public Mp4GroupIdToNameBoxData(ushort entryCount, IList<uint> groupId, IList<string> groupName)
		{
			this.EntryCount = entryCount;
			this.GroupId = groupId;
			this.GroupName = groupName;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4GroupIdToNameBoxData" /> class.
		/// </summary>
		public Mp4GroupIdToNameBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4GroupIdToNameBoxData val
				&& EqualityComparer<ushort?>.Default.Equals(this.EntryCount, val.EntryCount)
				&& EqualityComparer<IList<uint>?>.Default.Equals(this.GroupId, val.GroupId)
				&& EqualityComparer<IList<string>?>.Default.Equals(this.GroupName, val.GroupName)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (EntryCount?.GetHashCode() ?? 0);
				hash = hash * 23 + (GroupId?.GetHashCode() ?? 0);
				hash = hash * 23 + (GroupName?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4GroupIdToNameBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>grpl</c> box data representation.
	/// </summary>
	[FourCC("grpl")]
	public class Mp4GroupsListBoxData : IMp4BoxData, IEquatable<Mp4GroupsListBoxData?>
	{


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4GroupsListBoxData" /> class.
		/// </summary>
		public Mp4GroupsListBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4GroupsListBoxData val
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
		public bool Equals(Mp4GroupsListBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>hdlr</c> box data representation.
	/// </summary>
	[FourCC("hdlr")]
	public class Mp4HandlerBoxData : IMp4FullBoxData, IEquatable<Mp4HandlerBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Predefined.
		/// </summary>
		public uint? Predefined { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, HandlerType.
		/// </summary>
		public uint? HandlerType { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Reserved.
		/// </summary>
		public IList<uint>? Reserved { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Name.
		/// </summary>
		public string? Name { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4HandlerBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="predefined">The parameter that assigns <see cref="Predefined" /> directly.</param>
		/// <param name="handlerType">The parameter that assigns <see cref="HandlerType" /> directly.</param>
		/// <param name="reserved">The parameter that assigns <see cref="Reserved" /> directly.</param>
		/// <param name="name">The parameter that assigns <see cref="Name" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4HandlerBoxData(uint predefined, uint handlerType, IList<uint> reserved, string name, byte version, uint flags)
		{
			this.Predefined = predefined;
			this.HandlerType = handlerType;
			this.Reserved = reserved;
			this.Name = name;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4HandlerBoxData" /> class.
		/// </summary>
		public Mp4HandlerBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4HandlerBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.Predefined, val.Predefined)
				&& EqualityComparer<uint?>.Default.Equals(this.HandlerType, val.HandlerType)
				&& EqualityComparer<IList<uint>?>.Default.Equals(this.Reserved, val.Reserved)
				&& EqualityComparer<string?>.Default.Equals(this.Name, val.Name)
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
				hash = hash * 23 + (Predefined?.GetHashCode() ?? 0);
				hash = hash * 23 + (HandlerType?.GetHashCode() ?? 0);
				hash = hash * 23 + (Reserved?.GetHashCode() ?? 0);
				hash = hash * 23 + (Name?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4HandlerBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>hmhd</c> box data representation.
	/// </summary>
	[FourCC("hmhd")]
	public class Mp4HintMediaHeaderBoxData : IMp4FullBoxData, IEquatable<Mp4HintMediaHeaderBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, MaxPduSize.
		/// </summary>
		public ushort? MaxPduSize { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, AvgPduSize.
		/// </summary>
		public ushort? AvgPduSize { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, MaxBitRate.
		/// </summary>
		public uint? MaxBitRate { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, AvgBitRate.
		/// </summary>
		public uint? AvgBitRate { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Reserved.
		/// </summary>
		public uint? Reserved { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4HintMediaHeaderBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="maxPduSize">The parameter that assigns <see cref="MaxPduSize" /> directly.</param>
		/// <param name="avgPduSize">The parameter that assigns <see cref="AvgPduSize" /> directly.</param>
		/// <param name="maxBitRate">The parameter that assigns <see cref="MaxBitRate" /> directly.</param>
		/// <param name="avgBitRate">The parameter that assigns <see cref="AvgBitRate" /> directly.</param>
		/// <param name="reserved">The parameter that assigns <see cref="Reserved" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4HintMediaHeaderBoxData(ushort maxPduSize, ushort avgPduSize, uint maxBitRate, uint avgBitRate, uint reserved, byte version, uint flags)
		{
			this.MaxPduSize = maxPduSize;
			this.AvgPduSize = avgPduSize;
			this.MaxBitRate = maxBitRate;
			this.AvgBitRate = avgBitRate;
			this.Reserved = reserved;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4HintMediaHeaderBoxData" /> class.
		/// </summary>
		public Mp4HintMediaHeaderBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4HintMediaHeaderBoxData val
				&& EqualityComparer<ushort?>.Default.Equals(this.MaxPduSize, val.MaxPduSize)
				&& EqualityComparer<ushort?>.Default.Equals(this.AvgPduSize, val.AvgPduSize)
				&& EqualityComparer<uint?>.Default.Equals(this.MaxBitRate, val.MaxBitRate)
				&& EqualityComparer<uint?>.Default.Equals(this.AvgBitRate, val.AvgBitRate)
				&& EqualityComparer<uint?>.Default.Equals(this.Reserved, val.Reserved)
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
				hash = hash * 23 + (MaxPduSize?.GetHashCode() ?? 0);
				hash = hash * 23 + (AvgPduSize?.GetHashCode() ?? 0);
				hash = hash * 23 + (MaxBitRate?.GetHashCode() ?? 0);
				hash = hash * 23 + (AvgBitRate?.GetHashCode() ?? 0);
				hash = hash * 23 + (Reserved?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4HintMediaHeaderBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>idat</c> box data representation.
	/// </summary>
	[FourCC("idat")]
	public class Mp4ItemDataBoxData : IMp4BoxData, IEquatable<Mp4ItemDataBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Data.
		/// </summary>
		public byte[]? Data { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ItemDataBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="data">The parameter that assigns <see cref="Data" /> directly.</param>
		public Mp4ItemDataBoxData(byte[] data)
		{
			this.Data = data;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ItemDataBoxData" /> class.
		/// </summary>
		public Mp4ItemDataBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4ItemDataBoxData val
				&& EqualityComparer<byte[]?>.Default.Equals(this.Data, val.Data)
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
		public bool Equals(Mp4ItemDataBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>iinf</c> box data representation.
	/// </summary>
	[FourCC("iinf")]
	public class Mp4ItemInformationBoxData : IMp4FullBoxData, IEquatable<Mp4ItemInformationBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, EntryCount.
		/// </summary>
		public uint? EntryCount { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, ItemInfos.
		/// </summary>
		public IList<Mp4Box>? ItemInfos { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ItemInformationBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="entryCount">The parameter that assigns <see cref="EntryCount" /> directly.</param>
		/// <param name="itemInfos">The parameter that assigns <see cref="ItemInfos" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4ItemInformationBoxData(uint entryCount, IList<Mp4Box> itemInfos, byte version, uint flags)
		{
			this.EntryCount = entryCount;
			this.ItemInfos = itemInfos;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ItemInformationBoxData" /> class.
		/// </summary>
		public Mp4ItemInformationBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4ItemInformationBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.EntryCount, val.EntryCount)
				&& EqualityComparer<IList<Mp4Box>?>.Default.Equals(this.ItemInfos, val.ItemInfos)
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
				hash = hash * 23 + (ItemInfos?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4ItemInformationBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>imda</c> box data representation.
	/// </summary>
	[FourCC("imda")]
	public class Mp4IdentifiedMediaDataImdaBoxData : IMp4BoxData, IEquatable<Mp4IdentifiedMediaDataImdaBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, ImdaIdentifier.
		/// </summary>
		public uint? ImdaIdentifier { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Data.
		/// </summary>
		public byte[]? Data { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4IdentifiedMediaDataImdaBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="imdaIdentifier">The parameter that assigns <see cref="ImdaIdentifier" /> directly.</param>
		/// <param name="data">The parameter that assigns <see cref="Data" /> directly.</param>
		public Mp4IdentifiedMediaDataImdaBoxData(uint imdaIdentifier, byte[] data)
		{
			this.ImdaIdentifier = imdaIdentifier;
			this.Data = data;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4IdentifiedMediaDataImdaBoxData" /> class.
		/// </summary>
		public Mp4IdentifiedMediaDataImdaBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4IdentifiedMediaDataImdaBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.ImdaIdentifier, val.ImdaIdentifier)
				&& EqualityComparer<byte[]?>.Default.Equals(this.Data, val.Data)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (ImdaIdentifier?.GetHashCode() ?? 0);
				hash = hash * 23 + (Data?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4IdentifiedMediaDataImdaBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>ipro</c> box data representation.
	/// </summary>
	[FourCC("ipro")]
	public class Mp4ItemProtectionBoxData : IMp4FullBoxData, IEquatable<Mp4ItemProtectionBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, ProtectionCount.
		/// </summary>
		public ushort? ProtectionCount { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, ProtectionInformation.
		/// </summary>
		public IList<Mp4Box>? ProtectionInformation { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ItemProtectionBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="protectionCount">The parameter that assigns <see cref="ProtectionCount" /> directly.</param>
		/// <param name="protectionInformation">The parameter that assigns <see cref="ProtectionInformation" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4ItemProtectionBoxData(ushort protectionCount, IList<Mp4Box> protectionInformation, byte version, uint flags)
		{
			this.ProtectionCount = protectionCount;
			this.ProtectionInformation = protectionInformation;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ItemProtectionBoxData" /> class.
		/// </summary>
		public Mp4ItemProtectionBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4ItemProtectionBoxData val
				&& EqualityComparer<ushort?>.Default.Equals(this.ProtectionCount, val.ProtectionCount)
				&& EqualityComparer<IList<Mp4Box>?>.Default.Equals(this.ProtectionInformation, val.ProtectionInformation)
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
				hash = hash * 23 + (ProtectionCount?.GetHashCode() ?? 0);
				hash = hash * 23 + (ProtectionInformation?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4ItemProtectionBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>iref</c> box data representation.
	/// </summary>
	[FourCC("iref")]
	public class Mp4ItemReferenceBoxData : IMp4FullBoxData, IEquatable<Mp4ItemReferenceBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Boxes.
		/// </summary>
		public IList<Mp4Box>? Boxes { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ItemReferenceBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="boxes">The parameter that assigns <see cref="Boxes" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4ItemReferenceBoxData(IList<Mp4Box> boxes, byte version, uint flags)
		{
			this.Boxes = boxes;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ItemReferenceBoxData" /> class.
		/// </summary>
		public Mp4ItemReferenceBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4ItemReferenceBoxData val
				&& EqualityComparer<IList<Mp4Box>?>.Default.Equals(this.Boxes, val.Boxes)
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
				hash = hash * 23 + (Boxes?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4ItemReferenceBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>mdat</c> box data representation.
	/// </summary>
	[FourCC("mdat")]
	public class Mp4MediaDataBoxData : IMp4BoxData, IEquatable<Mp4MediaDataBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, RawData.
		/// </summary>
		public Stream? RawData { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4MediaDataBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="rawData">The parameter that assigns <see cref="RawData" /> directly.</param>
		public Mp4MediaDataBoxData(Stream rawData)
		{
			this.RawData = rawData;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4MediaDataBoxData" /> class.
		/// </summary>
		public Mp4MediaDataBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4MediaDataBoxData val
				&& EqualityComparer<Stream?>.Default.Equals(this.RawData, val.RawData)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (RawData?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4MediaDataBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>mdhd</c> box data representation.
	/// </summary>
	[FourCC("mdhd")]
	public class Mp4MediaHeaderBoxData : IMp4FullBoxData, IEquatable<Mp4MediaHeaderBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, CreationTime.
		/// </summary>
		public ulong? CreationTime { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, ModificationTime.
		/// </summary>
		public ulong? ModificationTime { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Timescale.
		/// </summary>
		public uint? Timescale { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Duration.
		/// </summary>
		public ulong? Duration { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Pad.
		/// </summary>
		public bool? Pad { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Language.
		/// </summary>
		public IList<uint>? Language { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Predefined.
		/// </summary>
		public ushort? Predefined { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4MediaHeaderBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="creationTime">The parameter that assigns <see cref="CreationTime" /> directly.</param>
		/// <param name="modificationTime">The parameter that assigns <see cref="ModificationTime" /> directly.</param>
		/// <param name="timescale">The parameter that assigns <see cref="Timescale" /> directly.</param>
		/// <param name="duration">The parameter that assigns <see cref="Duration" /> directly.</param>
		/// <param name="pad">The parameter that assigns <see cref="Pad" /> directly.</param>
		/// <param name="language">The parameter that assigns <see cref="Language" /> directly.</param>
		/// <param name="predefined">The parameter that assigns <see cref="Predefined" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4MediaHeaderBoxData(ulong creationTime, ulong modificationTime, uint timescale, ulong duration, bool pad, IList<uint> language, ushort predefined, byte version, uint flags)
		{
			this.CreationTime = creationTime;
			this.ModificationTime = modificationTime;
			this.Timescale = timescale;
			this.Duration = duration;
			this.Pad = pad;
			this.Language = language;
			this.Predefined = predefined;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4MediaHeaderBoxData" /> class.
		/// </summary>
		public Mp4MediaHeaderBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4MediaHeaderBoxData val
				&& EqualityComparer<ulong?>.Default.Equals(this.CreationTime, val.CreationTime)
				&& EqualityComparer<ulong?>.Default.Equals(this.ModificationTime, val.ModificationTime)
				&& EqualityComparer<uint?>.Default.Equals(this.Timescale, val.Timescale)
				&& EqualityComparer<ulong?>.Default.Equals(this.Duration, val.Duration)
				&& EqualityComparer<bool?>.Default.Equals(this.Pad, val.Pad)
				&& EqualityComparer<IList<uint>?>.Default.Equals(this.Language, val.Language)
				&& EqualityComparer<ushort?>.Default.Equals(this.Predefined, val.Predefined)
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
				hash = hash * 23 + (CreationTime?.GetHashCode() ?? 0);
				hash = hash * 23 + (ModificationTime?.GetHashCode() ?? 0);
				hash = hash * 23 + (Timescale?.GetHashCode() ?? 0);
				hash = hash * 23 + (Duration?.GetHashCode() ?? 0);
				hash = hash * 23 + (Pad?.GetHashCode() ?? 0);
				hash = hash * 23 + (Language?.GetHashCode() ?? 0);
				hash = hash * 23 + (Predefined?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4MediaHeaderBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>mdia</c> box data representation.
	/// </summary>
	[FourCC("mdia")]
	public class Mp4MediaBoxData : IMp4BoxData, IEquatable<Mp4MediaBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Children.
		/// </summary>
		public IList<Mp4Box>? Children { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4MediaBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="children">The parameter that assigns <see cref="Children" /> directly.</param>
		public Mp4MediaBoxData(IList<Mp4Box> children)
		{
			this.Children = children;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4MediaBoxData" /> class.
		/// </summary>
		public Mp4MediaBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4MediaBoxData val
				&& EqualityComparer<IList<Mp4Box>?>.Default.Equals(this.Children, val.Children)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Children?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4MediaBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>mehd</c> box data representation.
	/// </summary>
	[FourCC("mehd")]
	public class Mp4MovieExtendsHeaderBoxData : IMp4BoxData, IEquatable<Mp4MovieExtendsHeaderBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, FragmentDuration.
		/// </summary>
		public ulong? FragmentDuration { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4MovieExtendsHeaderBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="fragmentDuration">The parameter that assigns <see cref="FragmentDuration" /> directly.</param>
		public Mp4MovieExtendsHeaderBoxData(ulong fragmentDuration)
		{
			this.FragmentDuration = fragmentDuration;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4MovieExtendsHeaderBoxData" /> class.
		/// </summary>
		public Mp4MovieExtendsHeaderBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4MovieExtendsHeaderBoxData val
				&& EqualityComparer<ulong?>.Default.Equals(this.FragmentDuration, val.FragmentDuration)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (FragmentDuration?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4MovieExtendsHeaderBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>meta</c> box data representation.
	/// </summary>
	[FourCC("meta")]
	public class Mp4MetaBoxBoxData : IMp4FullBoxData, IEquatable<Mp4MetaBoxBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Boxes.
		/// </summary>
		public IList<Mp4Box>? Boxes { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4MetaBoxBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="boxes">The parameter that assigns <see cref="Boxes" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4MetaBoxBoxData(IList<Mp4Box> boxes, byte version, uint flags)
		{
			this.Boxes = boxes;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4MetaBoxBoxData" /> class.
		/// </summary>
		public Mp4MetaBoxBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4MetaBoxBoxData val
				&& EqualityComparer<IList<Mp4Box>?>.Default.Equals(this.Boxes, val.Boxes)
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
				hash = hash * 23 + (Boxes?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4MetaBoxBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>mfhd</c> box data representation.
	/// </summary>
	[FourCC("mfhd")]
	public class Mp4MovieFragmentHeaderBoxData : IMp4FullBoxData, IEquatable<Mp4MovieFragmentHeaderBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, SequenceNumber.
		/// </summary>
		public uint? SequenceNumber { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4MovieFragmentHeaderBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="sequenceNumber">The parameter that assigns <see cref="SequenceNumber" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4MovieFragmentHeaderBoxData(uint sequenceNumber, byte version, uint flags)
		{
			this.SequenceNumber = sequenceNumber;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4MovieFragmentHeaderBoxData" /> class.
		/// </summary>
		public Mp4MovieFragmentHeaderBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4MovieFragmentHeaderBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.SequenceNumber, val.SequenceNumber)
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
				hash = hash * 23 + (SequenceNumber?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4MovieFragmentHeaderBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>mfra</c> box data representation.
	/// </summary>
	[FourCC("mfra")]
	public class Mp4MovieFragmentRandomAccessBoxData : IMp4BoxData, IEquatable<Mp4MovieFragmentRandomAccessBoxData?>
	{


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4MovieFragmentRandomAccessBoxData" /> class.
		/// </summary>
		public Mp4MovieFragmentRandomAccessBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4MovieFragmentRandomAccessBoxData val
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
		public bool Equals(Mp4MovieFragmentRandomAccessBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>mfro</c> box data representation.
	/// </summary>
	[FourCC("mfro")]
	public class Mp4MovieFragmentRandomAccessOffsetBoxData : IMp4FullBoxData, IEquatable<Mp4MovieFragmentRandomAccessOffsetBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, ParentSize.
		/// </summary>
		public uint? ParentSize { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4MovieFragmentRandomAccessOffsetBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="parentSize">The parameter that assigns <see cref="ParentSize" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4MovieFragmentRandomAccessOffsetBoxData(uint parentSize, byte version, uint flags)
		{
			this.ParentSize = parentSize;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4MovieFragmentRandomAccessOffsetBoxData" /> class.
		/// </summary>
		public Mp4MovieFragmentRandomAccessOffsetBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4MovieFragmentRandomAccessOffsetBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.ParentSize, val.ParentSize)
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
				hash = hash * 23 + (ParentSize?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4MovieFragmentRandomAccessOffsetBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>minf</c> box data representation.
	/// </summary>
	[FourCC("minf")]
	public class Mp4MediaInformationBoxData : IMp4BoxData, IEquatable<Mp4MediaInformationBoxData?>
	{


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4MediaInformationBoxData" /> class.
		/// </summary>
		public Mp4MediaInformationBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4MediaInformationBoxData val
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
		public bool Equals(Mp4MediaInformationBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>moof</c> box data representation.
	/// </summary>
	[FourCC("moof")]
	public class Mp4MovieFragmentBoxData : IMp4BoxData, IEquatable<Mp4MovieFragmentBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Children.
		/// </summary>
		public IList<Mp4Box>? Children { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4MovieFragmentBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="children">The parameter that assigns <see cref="Children" /> directly.</param>
		public Mp4MovieFragmentBoxData(IList<Mp4Box> children)
		{
			this.Children = children;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4MovieFragmentBoxData" /> class.
		/// </summary>
		public Mp4MovieFragmentBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4MovieFragmentBoxData val
				&& EqualityComparer<IList<Mp4Box>?>.Default.Equals(this.Children, val.Children)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Children?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4MovieFragmentBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>moov</c> box data representation.
	/// </summary>
	[FourCC("moov")]
	public class Mp4MovieBoxData : IMp4BoxData, IEquatable<Mp4MovieBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Children.
		/// </summary>
		public IList<Mp4Box>? Children { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4MovieBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="children">The parameter that assigns <see cref="Children" /> directly.</param>
		public Mp4MovieBoxData(IList<Mp4Box> children)
		{
			this.Children = children;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4MovieBoxData" /> class.
		/// </summary>
		public Mp4MovieBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4MovieBoxData val
				&& EqualityComparer<IList<Mp4Box>?>.Default.Equals(this.Children, val.Children)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Children?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4MovieBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>mvex</c> box data representation.
	/// </summary>
	[FourCC("mvex")]
	public class Mp4MovieExtendsBoxData : IMp4BoxData, IEquatable<Mp4MovieExtendsBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Children.
		/// </summary>
		public IList<Mp4Box>? Children { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4MovieExtendsBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="children">The parameter that assigns <see cref="Children" /> directly.</param>
		public Mp4MovieExtendsBoxData(IList<Mp4Box> children)
		{
			this.Children = children;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4MovieExtendsBoxData" /> class.
		/// </summary>
		public Mp4MovieExtendsBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4MovieExtendsBoxData val
				&& EqualityComparer<IList<Mp4Box>?>.Default.Equals(this.Children, val.Children)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Children?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4MovieExtendsBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>mvhd</c> box data representation.
	/// </summary>
	[FourCC("mvhd")]
	public class Mp4MovieHeaderBoxData : IMp4BoxData, IEquatable<Mp4MovieHeaderBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Children.
		/// </summary>
		public IList<Mp4Box>? Children { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4MovieHeaderBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="children">The parameter that assigns <see cref="Children" /> directly.</param>
		public Mp4MovieHeaderBoxData(IList<Mp4Box> children)
		{
			this.Children = children;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4MovieHeaderBoxData" /> class.
		/// </summary>
		public Mp4MovieHeaderBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4MovieHeaderBoxData val
				&& EqualityComparer<IList<Mp4Box>?>.Default.Equals(this.Children, val.Children)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Children?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4MovieHeaderBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>nmhd</c> box data representation.
	/// </summary>
	[FourCC("nmhd")]
	public class Mp4NullMediaHeaderBoxData : IMp4FullBoxData, IEquatable<Mp4NullMediaHeaderBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4NullMediaHeaderBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4NullMediaHeaderBoxData(byte version, uint flags)
		{
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4NullMediaHeaderBoxData" /> class.
		/// </summary>
		public Mp4NullMediaHeaderBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4NullMediaHeaderBoxData val
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
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4NullMediaHeaderBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>otyp</c> box data representation.
	/// </summary>
	[FourCC("otyp")]
	public class Mp4OriginalFileTypeBoxBoxData : IMp4BoxData, IEquatable<Mp4OriginalFileTypeBoxBoxData?>
	{


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4OriginalFileTypeBoxBoxData" /> class.
		/// </summary>
		public Mp4OriginalFileTypeBoxBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4OriginalFileTypeBoxBoxData val
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
		public bool Equals(Mp4OriginalFileTypeBoxBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>padb</c> box data representation.
	/// </summary>
	[FourCC("padb")]
	public class Mp4SamplePaddingBitsBoxData : IMp4FullBoxData, IEquatable<Mp4SamplePaddingBitsBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, SampleCount.
		/// </summary>
		public uint? SampleCount { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Reserved1.
		/// </summary>
		public IList<bool>? Reserved1 { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Pad1.
		/// </summary>
		public IList<byte>? Pad1 { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Reserved2.
		/// </summary>
		public IList<bool>? Reserved2 { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Pad2.
		/// </summary>
		public IList<byte>? Pad2 { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SamplePaddingBitsBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="sampleCount">The parameter that assigns <see cref="SampleCount" /> directly.</param>
		/// <param name="reserved1">The parameter that assigns <see cref="Reserved1" /> directly.</param>
		/// <param name="pad1">The parameter that assigns <see cref="Pad1" /> directly.</param>
		/// <param name="reserved2">The parameter that assigns <see cref="Reserved2" /> directly.</param>
		/// <param name="pad2">The parameter that assigns <see cref="Pad2" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4SamplePaddingBitsBoxData(uint sampleCount, IList<bool> reserved1, IList<byte> pad1, IList<bool> reserved2, IList<byte> pad2, byte version, uint flags)
		{
			this.SampleCount = sampleCount;
			this.Reserved1 = reserved1;
			this.Pad1 = pad1;
			this.Reserved2 = reserved2;
			this.Pad2 = pad2;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SamplePaddingBitsBoxData" /> class.
		/// </summary>
		public Mp4SamplePaddingBitsBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4SamplePaddingBitsBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.SampleCount, val.SampleCount)
				&& EqualityComparer<IList<bool>?>.Default.Equals(this.Reserved1, val.Reserved1)
				&& EqualityComparer<IList<byte>?>.Default.Equals(this.Pad1, val.Pad1)
				&& EqualityComparer<IList<bool>?>.Default.Equals(this.Reserved2, val.Reserved2)
				&& EqualityComparer<IList<byte>?>.Default.Equals(this.Pad2, val.Pad2)
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
				hash = hash * 23 + (SampleCount?.GetHashCode() ?? 0);
				hash = hash * 23 + (Reserved1?.GetHashCode() ?? 0);
				hash = hash * 23 + (Pad1?.GetHashCode() ?? 0);
				hash = hash * 23 + (Reserved2?.GetHashCode() ?? 0);
				hash = hash * 23 + (Pad2?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4SamplePaddingBitsBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>pdin</c> box data representation.
	/// </summary>
	[FourCC("pdin")]
	public class Mp4ProgressiveDownloadInformationBoxData : IMp4FullBoxData, IEquatable<Mp4ProgressiveDownloadInformationBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Rate.
		/// </summary>
		public IList<uint>? Rate { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, InitialDelay.
		/// </summary>
		public IList<uint>? InitialDelay { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ProgressiveDownloadInformationBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="rate">The parameter that assigns <see cref="Rate" /> directly.</param>
		/// <param name="initialDelay">The parameter that assigns <see cref="InitialDelay" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4ProgressiveDownloadInformationBoxData(IList<uint> rate, IList<uint> initialDelay, byte version, uint flags)
		{
			this.Rate = rate;
			this.InitialDelay = initialDelay;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ProgressiveDownloadInformationBoxData" /> class.
		/// </summary>
		public Mp4ProgressiveDownloadInformationBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4ProgressiveDownloadInformationBoxData val
				&& EqualityComparer<IList<uint>?>.Default.Equals(this.Rate, val.Rate)
				&& EqualityComparer<IList<uint>?>.Default.Equals(this.InitialDelay, val.InitialDelay)
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
				hash = hash * 23 + (Rate?.GetHashCode() ?? 0);
				hash = hash * 23 + (InitialDelay?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4ProgressiveDownloadInformationBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>pitm</c> box data representation.
	/// </summary>
	[FourCC("pitm")]
	public class Mp4PrimaryItemReferenceBoxData : IMp4FullBoxData, IEquatable<Mp4PrimaryItemReferenceBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, ItemId.
		/// </summary>
		public uint? ItemId { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4PrimaryItemReferenceBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="itemId">The parameter that assigns <see cref="ItemId" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4PrimaryItemReferenceBoxData(uint itemId, byte version, uint flags)
		{
			this.ItemId = itemId;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4PrimaryItemReferenceBoxData" /> class.
		/// </summary>
		public Mp4PrimaryItemReferenceBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4PrimaryItemReferenceBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.ItemId, val.ItemId)
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
				hash = hash * 23 + (ItemId?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4PrimaryItemReferenceBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>prft</c> box data representation.
	/// </summary>
	[FourCC("prft")]
	public class Mp4ProducerReferenceTimeBoxData : IMp4FullBoxData, IEquatable<Mp4ProducerReferenceTimeBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, ReferenceTrackId.
		/// </summary>
		public uint? ReferenceTrackId { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, NtpTimestamp.
		/// </summary>
		public ulong? NtpTimestamp { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, MediaTime.
		/// </summary>
		public ulong? MediaTime { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ProducerReferenceTimeBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="referenceTrackId">The parameter that assigns <see cref="ReferenceTrackId" /> directly.</param>
		/// <param name="ntpTimestamp">The parameter that assigns <see cref="NtpTimestamp" /> directly.</param>
		/// <param name="mediaTime">The parameter that assigns <see cref="MediaTime" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4ProducerReferenceTimeBoxData(uint referenceTrackId, ulong ntpTimestamp, ulong mediaTime, byte version, uint flags)
		{
			this.ReferenceTrackId = referenceTrackId;
			this.NtpTimestamp = ntpTimestamp;
			this.MediaTime = mediaTime;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ProducerReferenceTimeBoxData" /> class.
		/// </summary>
		public Mp4ProducerReferenceTimeBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4ProducerReferenceTimeBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.ReferenceTrackId, val.ReferenceTrackId)
				&& EqualityComparer<ulong?>.Default.Equals(this.NtpTimestamp, val.NtpTimestamp)
				&& EqualityComparer<ulong?>.Default.Equals(this.MediaTime, val.MediaTime)
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
				hash = hash * 23 + (ReferenceTrackId?.GetHashCode() ?? 0);
				hash = hash * 23 + (NtpTimestamp?.GetHashCode() ?? 0);
				hash = hash * 23 + (MediaTime?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4ProducerReferenceTimeBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>rinf</c> box data representation.
	/// </summary>
	[FourCC("rinf")]
	public class Mp4RestrictedSchemeInformationBoxData : IMp4BoxData, IEquatable<Mp4RestrictedSchemeInformationBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, OriginalFormat.
		/// </summary>
		public Mp4Box? OriginalFormat { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, SchemeTypeBox.
		/// </summary>
		public Mp4Box? SchemeTypeBox { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Info.
		/// </summary>
		public Mp4Box? Info { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4RestrictedSchemeInformationBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="originalFormat">The parameter that assigns <see cref="OriginalFormat" /> directly.</param>
		/// <param name="schemeTypeBox">The parameter that assigns <see cref="SchemeTypeBox" /> directly.</param>
		/// <param name="info">The parameter that assigns <see cref="Info" /> directly.</param>
		public Mp4RestrictedSchemeInformationBoxData(Mp4Box originalFormat, Mp4Box schemeTypeBox, Mp4Box info)
		{
			this.OriginalFormat = originalFormat;
			this.SchemeTypeBox = schemeTypeBox;
			this.Info = info;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4RestrictedSchemeInformationBoxData" /> class.
		/// </summary>
		public Mp4RestrictedSchemeInformationBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4RestrictedSchemeInformationBoxData val
				&& EqualityComparer<Mp4Box?>.Default.Equals(this.OriginalFormat, val.OriginalFormat)
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
				hash = hash * 23 + (OriginalFormat?.GetHashCode() ?? 0);
				hash = hash * 23 + (SchemeTypeBox?.GetHashCode() ?? 0);
				hash = hash * 23 + (Info?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4RestrictedSchemeInformationBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>saio</c> box data representation.
	/// </summary>
	[FourCC("saio")]
	public class Mp4SampleAuxiliaryInformationOffsetsBoxData : IMp4FullBoxData, IEquatable<Mp4SampleAuxiliaryInformationOffsetsBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, AuxInfoType.
		/// </summary>
		public uint? AuxInfoType { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, AuxInfoTypeParameter.
		/// </summary>
		public uint? AuxInfoTypeParameter { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, EntryCount.
		/// </summary>
		public uint? EntryCount { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Offsets.
		/// </summary>
		public IList<ulong>? Offsets { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SampleAuxiliaryInformationOffsetsBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="auxInfoType">The parameter that assigns <see cref="AuxInfoType" /> directly.</param>
		/// <param name="auxInfoTypeParameter">The parameter that assigns <see cref="AuxInfoTypeParameter" /> directly.</param>
		/// <param name="entryCount">The parameter that assigns <see cref="EntryCount" /> directly.</param>
		/// <param name="offsets">The parameter that assigns <see cref="Offsets" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4SampleAuxiliaryInformationOffsetsBoxData(uint auxInfoType, uint auxInfoTypeParameter, uint entryCount, IList<ulong> offsets, byte version, uint flags)
		{
			this.AuxInfoType = auxInfoType;
			this.AuxInfoTypeParameter = auxInfoTypeParameter;
			this.EntryCount = entryCount;
			this.Offsets = offsets;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SampleAuxiliaryInformationOffsetsBoxData" /> class.
		/// </summary>
		public Mp4SampleAuxiliaryInformationOffsetsBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4SampleAuxiliaryInformationOffsetsBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.AuxInfoType, val.AuxInfoType)
				&& EqualityComparer<uint?>.Default.Equals(this.AuxInfoTypeParameter, val.AuxInfoTypeParameter)
				&& EqualityComparer<uint?>.Default.Equals(this.EntryCount, val.EntryCount)
				&& EqualityComparer<IList<ulong>?>.Default.Equals(this.Offsets, val.Offsets)
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
				hash = hash * 23 + (AuxInfoType?.GetHashCode() ?? 0);
				hash = hash * 23 + (AuxInfoTypeParameter?.GetHashCode() ?? 0);
				hash = hash * 23 + (EntryCount?.GetHashCode() ?? 0);
				hash = hash * 23 + (Offsets?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4SampleAuxiliaryInformationOffsetsBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>saiz</c> box data representation.
	/// </summary>
	[FourCC("saiz")]
	public class Mp4SampleAuxiliaryInformationSizeBoxData : IMp4FullBoxData, IEquatable<Mp4SampleAuxiliaryInformationSizeBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, AuxInfoType.
		/// </summary>
		public uint? AuxInfoType { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, AuxInfoTypeParameter.
		/// </summary>
		public uint? AuxInfoTypeParameter { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, DefaultSampleInfoSize.
		/// </summary>
		public byte? DefaultSampleInfoSize { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, SampleCount.
		/// </summary>
		public uint? SampleCount { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, SampleInfoSize.
		/// </summary>
		public IList<byte>? SampleInfoSize { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SampleAuxiliaryInformationSizeBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="auxInfoType">The parameter that assigns <see cref="AuxInfoType" /> directly.</param>
		/// <param name="auxInfoTypeParameter">The parameter that assigns <see cref="AuxInfoTypeParameter" /> directly.</param>
		/// <param name="defaultSampleInfoSize">The parameter that assigns <see cref="DefaultSampleInfoSize" /> directly.</param>
		/// <param name="sampleCount">The parameter that assigns <see cref="SampleCount" /> directly.</param>
		/// <param name="sampleInfoSize">The parameter that assigns <see cref="SampleInfoSize" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4SampleAuxiliaryInformationSizeBoxData(uint auxInfoType, uint auxInfoTypeParameter, byte defaultSampleInfoSize, uint sampleCount, IList<byte> sampleInfoSize, byte version, uint flags)
		{
			this.AuxInfoType = auxInfoType;
			this.AuxInfoTypeParameter = auxInfoTypeParameter;
			this.DefaultSampleInfoSize = defaultSampleInfoSize;
			this.SampleCount = sampleCount;
			this.SampleInfoSize = sampleInfoSize;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SampleAuxiliaryInformationSizeBoxData" /> class.
		/// </summary>
		public Mp4SampleAuxiliaryInformationSizeBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4SampleAuxiliaryInformationSizeBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.AuxInfoType, val.AuxInfoType)
				&& EqualityComparer<uint?>.Default.Equals(this.AuxInfoTypeParameter, val.AuxInfoTypeParameter)
				&& EqualityComparer<byte?>.Default.Equals(this.DefaultSampleInfoSize, val.DefaultSampleInfoSize)
				&& EqualityComparer<uint?>.Default.Equals(this.SampleCount, val.SampleCount)
				&& EqualityComparer<IList<byte>?>.Default.Equals(this.SampleInfoSize, val.SampleInfoSize)
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
				hash = hash * 23 + (AuxInfoType?.GetHashCode() ?? 0);
				hash = hash * 23 + (AuxInfoTypeParameter?.GetHashCode() ?? 0);
				hash = hash * 23 + (DefaultSampleInfoSize?.GetHashCode() ?? 0);
				hash = hash * 23 + (SampleCount?.GetHashCode() ?? 0);
				hash = hash * 23 + (SampleInfoSize?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4SampleAuxiliaryInformationSizeBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>sbgp</c> box data representation.
	/// </summary>
	[FourCC("sbgp")]
	public class Mp4SampleToGroupBoxData : IMp4FullBoxData, IEquatable<Mp4SampleToGroupBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, GroupingType.
		/// </summary>
		public uint? GroupingType { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, GroupingTypeParameter.
		/// </summary>
		public uint? GroupingTypeParameter { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, EntryCount.
		/// </summary>
		public uint? EntryCount { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, SampleCount.
		/// </summary>
		public IList<uint>? SampleCount { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, GroupDescriptionIndex.
		/// </summary>
		public IList<uint>? GroupDescriptionIndex { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SampleToGroupBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="groupingType">The parameter that assigns <see cref="GroupingType" /> directly.</param>
		/// <param name="groupingTypeParameter">The parameter that assigns <see cref="GroupingTypeParameter" /> directly.</param>
		/// <param name="entryCount">The parameter that assigns <see cref="EntryCount" /> directly.</param>
		/// <param name="sampleCount">The parameter that assigns <see cref="SampleCount" /> directly.</param>
		/// <param name="groupDescriptionIndex">The parameter that assigns <see cref="GroupDescriptionIndex" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4SampleToGroupBoxData(uint groupingType, uint groupingTypeParameter, uint entryCount, IList<uint> sampleCount, IList<uint> groupDescriptionIndex, byte version, uint flags)
		{
			this.GroupingType = groupingType;
			this.GroupingTypeParameter = groupingTypeParameter;
			this.EntryCount = entryCount;
			this.SampleCount = sampleCount;
			this.GroupDescriptionIndex = groupDescriptionIndex;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SampleToGroupBoxData" /> class.
		/// </summary>
		public Mp4SampleToGroupBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4SampleToGroupBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.GroupingType, val.GroupingType)
				&& EqualityComparer<uint?>.Default.Equals(this.GroupingTypeParameter, val.GroupingTypeParameter)
				&& EqualityComparer<uint?>.Default.Equals(this.EntryCount, val.EntryCount)
				&& EqualityComparer<IList<uint>?>.Default.Equals(this.SampleCount, val.SampleCount)
				&& EqualityComparer<IList<uint>?>.Default.Equals(this.GroupDescriptionIndex, val.GroupDescriptionIndex)
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
				hash = hash * 23 + (GroupingType?.GetHashCode() ?? 0);
				hash = hash * 23 + (GroupingTypeParameter?.GetHashCode() ?? 0);
				hash = hash * 23 + (EntryCount?.GetHashCode() ?? 0);
				hash = hash * 23 + (SampleCount?.GetHashCode() ?? 0);
				hash = hash * 23 + (GroupDescriptionIndex?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4SampleToGroupBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>schi</c> box data representation.
	/// </summary>
	[FourCC("schi")]
	public class Mp4SchemeInformationBoxData : IMp4BoxData, IEquatable<Mp4SchemeInformationBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, SchemeSpecificData.
		/// </summary>
		public IList<Mp4Box>? SchemeSpecificData { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SchemeInformationBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="schemeSpecificData">The parameter that assigns <see cref="SchemeSpecificData" /> directly.</param>
		public Mp4SchemeInformationBoxData(IList<Mp4Box> schemeSpecificData)
		{
			this.SchemeSpecificData = schemeSpecificData;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SchemeInformationBoxData" /> class.
		/// </summary>
		public Mp4SchemeInformationBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4SchemeInformationBoxData val
				&& EqualityComparer<IList<Mp4Box>?>.Default.Equals(this.SchemeSpecificData, val.SchemeSpecificData)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (SchemeSpecificData?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4SchemeInformationBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>schm</c> box data representation.
	/// </summary>
	[FourCC("schm")]
	public class Mp4SchemeTypeBoxData : IMp4FullBoxData, IEquatable<Mp4SchemeTypeBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, SchemeType.
		/// </summary>
		public uint? SchemeType { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, SchemeVersion.
		/// </summary>
		public uint? SchemeVersion { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, SchemeUri.
		/// </summary>
		public string? SchemeUri { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SchemeTypeBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="schemeType">The parameter that assigns <see cref="SchemeType" /> directly.</param>
		/// <param name="schemeVersion">The parameter that assigns <see cref="SchemeVersion" /> directly.</param>
		/// <param name="schemeUri">The parameter that assigns <see cref="SchemeUri" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4SchemeTypeBoxData(uint schemeType, uint schemeVersion, string schemeUri, byte version, uint flags)
		{
			this.SchemeType = schemeType;
			this.SchemeVersion = schemeVersion;
			this.SchemeUri = schemeUri;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SchemeTypeBoxData" /> class.
		/// </summary>
		public Mp4SchemeTypeBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4SchemeTypeBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.SchemeType, val.SchemeType)
				&& EqualityComparer<uint?>.Default.Equals(this.SchemeVersion, val.SchemeVersion)
				&& EqualityComparer<string?>.Default.Equals(this.SchemeUri, val.SchemeUri)
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
				hash = hash * 23 + (SchemeType?.GetHashCode() ?? 0);
				hash = hash * 23 + (SchemeVersion?.GetHashCode() ?? 0);
				hash = hash * 23 + (SchemeUri?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4SchemeTypeBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>csch</c> box data representation.
	/// </summary>
	[FourCC("csch")]
	public class Mp4CompatibleSchemeTypeBoxData : IMp4FullBoxData, IEquatable<Mp4CompatibleSchemeTypeBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, SchemeType.
		/// </summary>
		public uint? SchemeType { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, SchemeVersion.
		/// </summary>
		public uint? SchemeVersion { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, SchemeUri.
		/// </summary>
		public string? SchemeUri { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4CompatibleSchemeTypeBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="schemeType">The parameter that assigns <see cref="SchemeType" /> directly.</param>
		/// <param name="schemeVersion">The parameter that assigns <see cref="SchemeVersion" /> directly.</param>
		/// <param name="schemeUri">The parameter that assigns <see cref="SchemeUri" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4CompatibleSchemeTypeBoxData(uint schemeType, uint schemeVersion, string schemeUri, byte version, uint flags)
		{
			this.SchemeType = schemeType;
			this.SchemeVersion = schemeVersion;
			this.SchemeUri = schemeUri;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4CompatibleSchemeTypeBoxData" /> class.
		/// </summary>
		public Mp4CompatibleSchemeTypeBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4CompatibleSchemeTypeBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.SchemeType, val.SchemeType)
				&& EqualityComparer<uint?>.Default.Equals(this.SchemeVersion, val.SchemeVersion)
				&& EqualityComparer<string?>.Default.Equals(this.SchemeUri, val.SchemeUri)
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
				hash = hash * 23 + (SchemeType?.GetHashCode() ?? 0);
				hash = hash * 23 + (SchemeVersion?.GetHashCode() ?? 0);
				hash = hash * 23 + (SchemeUri?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4CompatibleSchemeTypeBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>sdtp</c> box data representation.
	/// </summary>
	[FourCC("sdtp")]
	public class Mp4SampleDependencyTypeBoxData : IMp4FullBoxData, IEquatable<Mp4SampleDependencyTypeBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, IsLeading.
		/// </summary>
		public IList<byte>? IsLeading { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, SampleDependsOn.
		/// </summary>
		public IList<byte>? SampleDependsOn { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, SampleIsDependedOn.
		/// </summary>
		public IList<byte>? SampleIsDependedOn { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, SampleHasRedundancy.
		/// </summary>
		public IList<byte>? SampleHasRedundancy { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SampleDependencyTypeBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="isLeading">The parameter that assigns <see cref="IsLeading" /> directly.</param>
		/// <param name="sampleDependsOn">The parameter that assigns <see cref="SampleDependsOn" /> directly.</param>
		/// <param name="sampleIsDependedOn">The parameter that assigns <see cref="SampleIsDependedOn" /> directly.</param>
		/// <param name="sampleHasRedundancy">The parameter that assigns <see cref="SampleHasRedundancy" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4SampleDependencyTypeBoxData(IList<byte> isLeading, IList<byte> sampleDependsOn, IList<byte> sampleIsDependedOn, IList<byte> sampleHasRedundancy, byte version, uint flags)
		{
			this.IsLeading = isLeading;
			this.SampleDependsOn = sampleDependsOn;
			this.SampleIsDependedOn = sampleIsDependedOn;
			this.SampleHasRedundancy = sampleHasRedundancy;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SampleDependencyTypeBoxData" /> class.
		/// </summary>
		public Mp4SampleDependencyTypeBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4SampleDependencyTypeBoxData val
				&& EqualityComparer<IList<byte>?>.Default.Equals(this.IsLeading, val.IsLeading)
				&& EqualityComparer<IList<byte>?>.Default.Equals(this.SampleDependsOn, val.SampleDependsOn)
				&& EqualityComparer<IList<byte>?>.Default.Equals(this.SampleIsDependedOn, val.SampleIsDependedOn)
				&& EqualityComparer<IList<byte>?>.Default.Equals(this.SampleHasRedundancy, val.SampleHasRedundancy)
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
				hash = hash * 23 + (IsLeading?.GetHashCode() ?? 0);
				hash = hash * 23 + (SampleDependsOn?.GetHashCode() ?? 0);
				hash = hash * 23 + (SampleIsDependedOn?.GetHashCode() ?? 0);
				hash = hash * 23 + (SampleHasRedundancy?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4SampleDependencyTypeBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>sidx</c> box data representation.
	/// </summary>
	[FourCC("sidx")]
	public class Mp4SegmentIndexBoxData : IMp4BoxData, IEquatable<Mp4SegmentIndexBoxData?>
	{


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SegmentIndexBoxData" /> class.
		/// </summary>
		public Mp4SegmentIndexBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4SegmentIndexBoxData val
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
		public bool Equals(Mp4SegmentIndexBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>skip</c> box data representation.
	/// </summary>
	[FourCC("skip")]
	public class Mp4FreeSpaceSkipBoxData : IMp4BoxData, IEquatable<Mp4FreeSpaceSkipBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, RawData.
		/// </summary>
		public Stream? RawData { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4FreeSpaceSkipBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="rawData">The parameter that assigns <see cref="RawData" /> directly.</param>
		public Mp4FreeSpaceSkipBoxData(Stream rawData)
		{
			this.RawData = rawData;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4FreeSpaceSkipBoxData" /> class.
		/// </summary>
		public Mp4FreeSpaceSkipBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4FreeSpaceSkipBoxData val
				&& EqualityComparer<Stream?>.Default.Equals(this.RawData, val.RawData)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (RawData?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4FreeSpaceSkipBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>smhd</c> box data representation.
	/// </summary>
	[FourCC("smhd")]
	public class Mp4SoundMediaHeaderBoxData : IMp4FullBoxData, IEquatable<Mp4SoundMediaHeaderBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Balance.
		/// </summary>
		public ushort? Balance { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Reserved.
		/// </summary>
		public ushort? Reserved { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SoundMediaHeaderBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="balance">The parameter that assigns <see cref="Balance" /> directly.</param>
		/// <param name="reserved">The parameter that assigns <see cref="Reserved" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4SoundMediaHeaderBoxData(ushort balance, ushort reserved, byte version, uint flags)
		{
			this.Balance = balance;
			this.Reserved = reserved;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SoundMediaHeaderBoxData" /> class.
		/// </summary>
		public Mp4SoundMediaHeaderBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4SoundMediaHeaderBoxData val
				&& EqualityComparer<ushort?>.Default.Equals(this.Balance, val.Balance)
				&& EqualityComparer<ushort?>.Default.Equals(this.Reserved, val.Reserved)
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
				hash = hash * 23 + (Balance?.GetHashCode() ?? 0);
				hash = hash * 23 + (Reserved?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4SoundMediaHeaderBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>srpp</c> box data representation.
	/// </summary>
	[FourCC("srpp")]
	public class Mp4StrpProcessBoxData : IMp4FullBoxData, IEquatable<Mp4StrpProcessBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, EncryptionAlgorithmRtp.
		/// </summary>
		public uint? EncryptionAlgorithmRtp { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, EncryptionAlgorithmRtcp.
		/// </summary>
		public uint? EncryptionAlgorithmRtcp { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, IntegrityAlgorithmRtp.
		/// </summary>
		public uint? IntegrityAlgorithmRtp { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, IntegrityAlgorithmRtcp.
		/// </summary>
		public uint? IntegrityAlgorithmRtcp { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, SchemeTypeBox.
		/// </summary>
		public Mp4Box? SchemeTypeBox { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Info.
		/// </summary>
		public Mp4Box? Info { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4StrpProcessBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="encryptionAlgorithmRtp">The parameter that assigns <see cref="EncryptionAlgorithmRtp" /> directly.</param>
		/// <param name="encryptionAlgorithmRtcp">The parameter that assigns <see cref="EncryptionAlgorithmRtcp" /> directly.</param>
		/// <param name="integrityAlgorithmRtp">The parameter that assigns <see cref="IntegrityAlgorithmRtp" /> directly.</param>
		/// <param name="integrityAlgorithmRtcp">The parameter that assigns <see cref="IntegrityAlgorithmRtcp" /> directly.</param>
		/// <param name="schemeTypeBox">The parameter that assigns <see cref="SchemeTypeBox" /> directly.</param>
		/// <param name="info">The parameter that assigns <see cref="Info" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4StrpProcessBoxData(uint encryptionAlgorithmRtp, uint encryptionAlgorithmRtcp, uint integrityAlgorithmRtp, uint integrityAlgorithmRtcp, Mp4Box schemeTypeBox, Mp4Box info, byte version, uint flags)
		{
			this.EncryptionAlgorithmRtp = encryptionAlgorithmRtp;
			this.EncryptionAlgorithmRtcp = encryptionAlgorithmRtcp;
			this.IntegrityAlgorithmRtp = integrityAlgorithmRtp;
			this.IntegrityAlgorithmRtcp = integrityAlgorithmRtcp;
			this.SchemeTypeBox = schemeTypeBox;
			this.Info = info;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4StrpProcessBoxData" /> class.
		/// </summary>
		public Mp4StrpProcessBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4StrpProcessBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.EncryptionAlgorithmRtp, val.EncryptionAlgorithmRtp)
				&& EqualityComparer<uint?>.Default.Equals(this.EncryptionAlgorithmRtcp, val.EncryptionAlgorithmRtcp)
				&& EqualityComparer<uint?>.Default.Equals(this.IntegrityAlgorithmRtp, val.IntegrityAlgorithmRtp)
				&& EqualityComparer<uint?>.Default.Equals(this.IntegrityAlgorithmRtcp, val.IntegrityAlgorithmRtcp)
				&& EqualityComparer<Mp4Box?>.Default.Equals(this.SchemeTypeBox, val.SchemeTypeBox)
				&& EqualityComparer<Mp4Box?>.Default.Equals(this.Info, val.Info)
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
				hash = hash * 23 + (EncryptionAlgorithmRtp?.GetHashCode() ?? 0);
				hash = hash * 23 + (EncryptionAlgorithmRtcp?.GetHashCode() ?? 0);
				hash = hash * 23 + (IntegrityAlgorithmRtp?.GetHashCode() ?? 0);
				hash = hash * 23 + (IntegrityAlgorithmRtcp?.GetHashCode() ?? 0);
				hash = hash * 23 + (SchemeTypeBox?.GetHashCode() ?? 0);
				hash = hash * 23 + (Info?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4StrpProcessBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>ssix</c> box data representation.
	/// </summary>
	[FourCC("ssix")]
	public class Mp4SubsampleIndexBoxData : IMp4BoxData, IEquatable<Mp4SubsampleIndexBoxData?>
	{


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SubsampleIndexBoxData" /> class.
		/// </summary>
		public Mp4SubsampleIndexBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4SubsampleIndexBoxData val
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
		public bool Equals(Mp4SubsampleIndexBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>stbl</c> box data representation.
	/// </summary>
	[FourCC("stbl")]
	public class Mp4SampleTableBoxData : IMp4BoxData, IEquatable<Mp4SampleTableBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Children.
		/// </summary>
		public IList<Mp4Box>? Children { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SampleTableBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="children">The parameter that assigns <see cref="Children" /> directly.</param>
		public Mp4SampleTableBoxData(IList<Mp4Box> children)
		{
			this.Children = children;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SampleTableBoxData" /> class.
		/// </summary>
		public Mp4SampleTableBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4SampleTableBoxData val
				&& EqualityComparer<IList<Mp4Box>?>.Default.Equals(this.Children, val.Children)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Children?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4SampleTableBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>stco</c> box data representation.
	/// </summary>
	[FourCC("stco")]
	public class Mp4ChunkOffsetBoxData : IMp4BoxData, IEquatable<Mp4ChunkOffsetBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, EntryCount.
		/// </summary>
		public uint? EntryCount { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, ChunkOffset.
		/// </summary>
		public IList<uint>? ChunkOffset { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ChunkOffsetBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="entryCount">The parameter that assigns <see cref="EntryCount" /> directly.</param>
		/// <param name="chunkOffset">The parameter that assigns <see cref="ChunkOffset" /> directly.</param>
		public Mp4ChunkOffsetBoxData(uint entryCount, IList<uint> chunkOffset)
		{
			this.EntryCount = entryCount;
			this.ChunkOffset = chunkOffset;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ChunkOffsetBoxData" /> class.
		/// </summary>
		public Mp4ChunkOffsetBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4ChunkOffsetBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.EntryCount, val.EntryCount)
				&& EqualityComparer<IList<uint>?>.Default.Equals(this.ChunkOffset, val.ChunkOffset)
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
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4ChunkOffsetBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>stdp</c> box data representation.
	/// </summary>
	[FourCC("stdp")]
	public class Mp4SampleDegradationPriorityBoxData : IMp4BoxData, IEquatable<Mp4SampleDegradationPriorityBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Priority.
		/// </summary>
		public IList<ushort>? Priority { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SampleDegradationPriorityBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="priority">The parameter that assigns <see cref="Priority" /> directly.</param>
		public Mp4SampleDegradationPriorityBoxData(IList<ushort> priority)
		{
			this.Priority = priority;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SampleDegradationPriorityBoxData" /> class.
		/// </summary>
		public Mp4SampleDegradationPriorityBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4SampleDegradationPriorityBoxData val
				&& EqualityComparer<IList<ushort>?>.Default.Equals(this.Priority, val.Priority)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Priority?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4SampleDegradationPriorityBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>sthd</c> box data representation.
	/// </summary>
	[FourCC("sthd")]
	public class Mp4SubtitleMediaHeaderBoxData : IMp4FullBoxData, IEquatable<Mp4SubtitleMediaHeaderBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SubtitleMediaHeaderBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4SubtitleMediaHeaderBoxData(byte version, uint flags)
		{
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SubtitleMediaHeaderBoxData" /> class.
		/// </summary>
		public Mp4SubtitleMediaHeaderBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4SubtitleMediaHeaderBoxData val
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
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4SubtitleMediaHeaderBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>strd</c> box data representation.
	/// </summary>
	[FourCC("strd")]
	public class Mp4SubTrackDefinitionBoxBoxData : IMp4BoxData, IEquatable<Mp4SubTrackDefinitionBoxBoxData?>
	{


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SubTrackDefinitionBoxBoxData" /> class.
		/// </summary>
		public Mp4SubTrackDefinitionBoxBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4SubTrackDefinitionBoxBoxData val
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
		public bool Equals(Mp4SubTrackDefinitionBoxBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>stri</c> box data representation.
	/// </summary>
	[FourCC("stri")]
	public class Mp4SubTrackInformationBoxData : IMp4FullBoxData, IEquatable<Mp4SubTrackInformationBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, SwitchGroup.
		/// </summary>
		public short? SwitchGroup { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, AlternateGroup.
		/// </summary>
		public short? AlternateGroup { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, SubTrackId.
		/// </summary>
		public uint? SubTrackId { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, AttributeList.
		/// </summary>
		public IList<uint>? AttributeList { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SubTrackInformationBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="switchGroup">The parameter that assigns <see cref="SwitchGroup" /> directly.</param>
		/// <param name="alternateGroup">The parameter that assigns <see cref="AlternateGroup" /> directly.</param>
		/// <param name="subTrackId">The parameter that assigns <see cref="SubTrackId" /> directly.</param>
		/// <param name="attributeList">The parameter that assigns <see cref="AttributeList" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4SubTrackInformationBoxData(short switchGroup, short alternateGroup, uint subTrackId, IList<uint> attributeList, byte version, uint flags)
		{
			this.SwitchGroup = switchGroup;
			this.AlternateGroup = alternateGroup;
			this.SubTrackId = subTrackId;
			this.AttributeList = attributeList;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SubTrackInformationBoxData" /> class.
		/// </summary>
		public Mp4SubTrackInformationBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4SubTrackInformationBoxData val
				&& EqualityComparer<short?>.Default.Equals(this.SwitchGroup, val.SwitchGroup)
				&& EqualityComparer<short?>.Default.Equals(this.AlternateGroup, val.AlternateGroup)
				&& EqualityComparer<uint?>.Default.Equals(this.SubTrackId, val.SubTrackId)
				&& EqualityComparer<IList<uint>?>.Default.Equals(this.AttributeList, val.AttributeList)
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
				hash = hash * 23 + (AlternateGroup?.GetHashCode() ?? 0);
				hash = hash * 23 + (SubTrackId?.GetHashCode() ?? 0);
				hash = hash * 23 + (AttributeList?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4SubTrackInformationBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>stsc</c> box data representation.
	/// </summary>
	[FourCC("stsc")]
	public class Mp4SampleToChunkBoxData : IMp4FullBoxData, IEquatable<Mp4SampleToChunkBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, EntryCount.
		/// </summary>
		public uint? EntryCount { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, FirstChunk.
		/// </summary>
		public IList<uint>? FirstChunk { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, SamplesPerChunk.
		/// </summary>
		public IList<uint>? SamplesPerChunk { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, SampleDescriptionIndex.
		/// </summary>
		public IList<uint>? SampleDescriptionIndex { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SampleToChunkBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="entryCount">The parameter that assigns <see cref="EntryCount" /> directly.</param>
		/// <param name="firstChunk">The parameter that assigns <see cref="FirstChunk" /> directly.</param>
		/// <param name="samplesPerChunk">The parameter that assigns <see cref="SamplesPerChunk" /> directly.</param>
		/// <param name="sampleDescriptionIndex">The parameter that assigns <see cref="SampleDescriptionIndex" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4SampleToChunkBoxData(uint entryCount, IList<uint> firstChunk, IList<uint> samplesPerChunk, IList<uint> sampleDescriptionIndex, byte version, uint flags)
		{
			this.EntryCount = entryCount;
			this.FirstChunk = firstChunk;
			this.SamplesPerChunk = samplesPerChunk;
			this.SampleDescriptionIndex = sampleDescriptionIndex;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SampleToChunkBoxData" /> class.
		/// </summary>
		public Mp4SampleToChunkBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4SampleToChunkBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.EntryCount, val.EntryCount)
				&& EqualityComparer<IList<uint>?>.Default.Equals(this.FirstChunk, val.FirstChunk)
				&& EqualityComparer<IList<uint>?>.Default.Equals(this.SamplesPerChunk, val.SamplesPerChunk)
				&& EqualityComparer<IList<uint>?>.Default.Equals(this.SampleDescriptionIndex, val.SampleDescriptionIndex)
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
				hash = hash * 23 + (FirstChunk?.GetHashCode() ?? 0);
				hash = hash * 23 + (SamplesPerChunk?.GetHashCode() ?? 0);
				hash = hash * 23 + (SampleDescriptionIndex?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4SampleToChunkBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>stsd</c> box data representation.
	/// </summary>
	[FourCC("stsd")]
	public class Mp4SampleDescriptionBoxData : IMp4FullBoxData, IEquatable<Mp4SampleDescriptionBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, EntryCount.
		/// </summary>
		public uint? EntryCount { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, SampleEntries.
		/// </summary>
		public IList<Mp4Box>? SampleEntries { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SampleDescriptionBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="entryCount">The parameter that assigns <see cref="EntryCount" /> directly.</param>
		/// <param name="sampleEntries">The parameter that assigns <see cref="SampleEntries" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4SampleDescriptionBoxData(uint entryCount, IList<Mp4Box> sampleEntries, byte version, uint flags)
		{
			this.EntryCount = entryCount;
			this.SampleEntries = sampleEntries;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SampleDescriptionBoxData" /> class.
		/// </summary>
		public Mp4SampleDescriptionBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4SampleDescriptionBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.EntryCount, val.EntryCount)
				&& EqualityComparer<IList<Mp4Box>?>.Default.Equals(this.SampleEntries, val.SampleEntries)
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
				hash = hash * 23 + (SampleEntries?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4SampleDescriptionBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>stsg</c> box data representation.
	/// </summary>
	[FourCC("stsg")]
	public class Mp4SubtrackSampleGroupingBoxData : IMp4FullBoxData, IEquatable<Mp4SubtrackSampleGroupingBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, GroupingType.
		/// </summary>
		public uint? GroupingType { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, ItemCount.
		/// </summary>
		public ushort? ItemCount { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, GroupDescriptionIndex.
		/// </summary>
		public IList<uint>? GroupDescriptionIndex { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SubtrackSampleGroupingBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="groupingType">The parameter that assigns <see cref="GroupingType" /> directly.</param>
		/// <param name="itemCount">The parameter that assigns <see cref="ItemCount" /> directly.</param>
		/// <param name="groupDescriptionIndex">The parameter that assigns <see cref="GroupDescriptionIndex" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4SubtrackSampleGroupingBoxData(uint groupingType, ushort itemCount, IList<uint> groupDescriptionIndex, byte version, uint flags)
		{
			this.GroupingType = groupingType;
			this.ItemCount = itemCount;
			this.GroupDescriptionIndex = groupDescriptionIndex;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SubtrackSampleGroupingBoxData" /> class.
		/// </summary>
		public Mp4SubtrackSampleGroupingBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4SubtrackSampleGroupingBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.GroupingType, val.GroupingType)
				&& EqualityComparer<ushort?>.Default.Equals(this.ItemCount, val.ItemCount)
				&& EqualityComparer<IList<uint>?>.Default.Equals(this.GroupDescriptionIndex, val.GroupDescriptionIndex)
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
				hash = hash * 23 + (GroupingType?.GetHashCode() ?? 0);
				hash = hash * 23 + (ItemCount?.GetHashCode() ?? 0);
				hash = hash * 23 + (GroupDescriptionIndex?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4SubtrackSampleGroupingBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>stsh</c> box data representation.
	/// </summary>
	[FourCC("stsh")]
	public class Mp4ShadowSyncSampleTableBoxData : IMp4FullBoxData, IEquatable<Mp4ShadowSyncSampleTableBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, EntryCount.
		/// </summary>
		public uint? EntryCount { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, ShadowedSampleNumber.
		/// </summary>
		public IList<uint>? ShadowedSampleNumber { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, SyncSampleNumber.
		/// </summary>
		public IList<uint>? SyncSampleNumber { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ShadowSyncSampleTableBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="entryCount">The parameter that assigns <see cref="EntryCount" /> directly.</param>
		/// <param name="shadowedSampleNumber">The parameter that assigns <see cref="ShadowedSampleNumber" /> directly.</param>
		/// <param name="syncSampleNumber">The parameter that assigns <see cref="SyncSampleNumber" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4ShadowSyncSampleTableBoxData(uint entryCount, IList<uint> shadowedSampleNumber, IList<uint> syncSampleNumber, byte version, uint flags)
		{
			this.EntryCount = entryCount;
			this.ShadowedSampleNumber = shadowedSampleNumber;
			this.SyncSampleNumber = syncSampleNumber;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4ShadowSyncSampleTableBoxData" /> class.
		/// </summary>
		public Mp4ShadowSyncSampleTableBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4ShadowSyncSampleTableBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.EntryCount, val.EntryCount)
				&& EqualityComparer<IList<uint>?>.Default.Equals(this.ShadowedSampleNumber, val.ShadowedSampleNumber)
				&& EqualityComparer<IList<uint>?>.Default.Equals(this.SyncSampleNumber, val.SyncSampleNumber)
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
				hash = hash * 23 + (ShadowedSampleNumber?.GetHashCode() ?? 0);
				hash = hash * 23 + (SyncSampleNumber?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4ShadowSyncSampleTableBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>stss</c> box data representation.
	/// </summary>
	[FourCC("stss")]
	public class Mp4SyncSampleTableBoxData : IMp4FullBoxData, IEquatable<Mp4SyncSampleTableBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, EntryCount.
		/// </summary>
		public uint? EntryCount { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, SampleNumber.
		/// </summary>
		public IList<uint>? SampleNumber { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SyncSampleTableBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="entryCount">The parameter that assigns <see cref="EntryCount" /> directly.</param>
		/// <param name="sampleNumber">The parameter that assigns <see cref="SampleNumber" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4SyncSampleTableBoxData(uint entryCount, IList<uint> sampleNumber, byte version, uint flags)
		{
			this.EntryCount = entryCount;
			this.SampleNumber = sampleNumber;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SyncSampleTableBoxData" /> class.
		/// </summary>
		public Mp4SyncSampleTableBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4SyncSampleTableBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.EntryCount, val.EntryCount)
				&& EqualityComparer<IList<uint>?>.Default.Equals(this.SampleNumber, val.SampleNumber)
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
				hash = hash * 23 + (SampleNumber?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4SyncSampleTableBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>stsz</c> box data representation.
	/// </summary>
	[FourCC("stsz")]
	public class Mp4SampleSizeBoxData : IMp4FullBoxData, IEquatable<Mp4SampleSizeBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, SampleSize.
		/// </summary>
		public uint? SampleSize { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, SampleCount.
		/// </summary>
		public uint? SampleCount { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, EntrySize.
		/// </summary>
		public IList<uint>? EntrySize { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SampleSizeBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="sampleSize">The parameter that assigns <see cref="SampleSize" /> directly.</param>
		/// <param name="sampleCount">The parameter that assigns <see cref="SampleCount" /> directly.</param>
		/// <param name="entrySize">The parameter that assigns <see cref="EntrySize" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4SampleSizeBoxData(uint sampleSize, uint sampleCount, IList<uint> entrySize, byte version, uint flags)
		{
			this.SampleSize = sampleSize;
			this.SampleCount = sampleCount;
			this.EntrySize = entrySize;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4SampleSizeBoxData" /> class.
		/// </summary>
		public Mp4SampleSizeBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4SampleSizeBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.SampleSize, val.SampleSize)
				&& EqualityComparer<uint?>.Default.Equals(this.SampleCount, val.SampleCount)
				&& EqualityComparer<IList<uint>?>.Default.Equals(this.EntrySize, val.EntrySize)
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
				hash = hash * 23 + (SampleSize?.GetHashCode() ?? 0);
				hash = hash * 23 + (SampleCount?.GetHashCode() ?? 0);
				hash = hash * 23 + (EntrySize?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4SampleSizeBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>stts</c> box data representation.
	/// </summary>
	[FourCC("stts")]
	public class Mp4TimeToSampleBoxData : IMp4FullBoxData, IEquatable<Mp4TimeToSampleBoxData?>
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
		///   Represents one of MP4 box properties, named, SampleDelta.
		/// </summary>
		public IList<uint>? SampleDelta { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TimeToSampleBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="entryCount">The parameter that assigns <see cref="EntryCount" /> directly.</param>
		/// <param name="sampleCount">The parameter that assigns <see cref="SampleCount" /> directly.</param>
		/// <param name="sampleDelta">The parameter that assigns <see cref="SampleDelta" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4TimeToSampleBoxData(uint entryCount, IList<uint> sampleCount, IList<uint> sampleDelta, byte version, uint flags)
		{
			this.EntryCount = entryCount;
			this.SampleCount = sampleCount;
			this.SampleDelta = sampleDelta;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TimeToSampleBoxData" /> class.
		/// </summary>
		public Mp4TimeToSampleBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4TimeToSampleBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.EntryCount, val.EntryCount)
				&& EqualityComparer<IList<uint>?>.Default.Equals(this.SampleCount, val.SampleCount)
				&& EqualityComparer<IList<uint>?>.Default.Equals(this.SampleDelta, val.SampleDelta)
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
				hash = hash * 23 + (SampleDelta?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4TimeToSampleBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>tfdt</c> box data representation.
	/// </summary>
	[FourCC("tfdt")]
	public class Mp4TrackFrameDecodeTimeBoxData : IMp4FullBoxData, IEquatable<Mp4TrackFrameDecodeTimeBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, BaseMediaDecodeTime.
		/// </summary>
		public ulong? BaseMediaDecodeTime { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TrackFrameDecodeTimeBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="baseMediaDecodeTime">The parameter that assigns <see cref="BaseMediaDecodeTime" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4TrackFrameDecodeTimeBoxData(ulong baseMediaDecodeTime, byte version, uint flags)
		{
			this.BaseMediaDecodeTime = baseMediaDecodeTime;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TrackFrameDecodeTimeBoxData" /> class.
		/// </summary>
		public Mp4TrackFrameDecodeTimeBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4TrackFrameDecodeTimeBoxData val
				&& EqualityComparer<ulong?>.Default.Equals(this.BaseMediaDecodeTime, val.BaseMediaDecodeTime)
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
				hash = hash * 23 + (BaseMediaDecodeTime?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4TrackFrameDecodeTimeBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>tkhd</c> box data representation.
	/// </summary>
	[FourCC("tkhd")]
	public class Mp4TrackHeaderBoxData : IMp4FullBoxData, IEquatable<Mp4TrackHeaderBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, CreationTime.
		/// </summary>
		public ulong? CreationTime { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, ModificationTime.
		/// </summary>
		public ulong? ModificationTime { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, TrackId.
		/// </summary>
		public uint? TrackId { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Reserved.
		/// </summary>
		public uint? Reserved { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Duration.
		/// </summary>
		public ulong? Duration { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TrackHeaderBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="creationTime">The parameter that assigns <see cref="CreationTime" /> directly.</param>
		/// <param name="modificationTime">The parameter that assigns <see cref="ModificationTime" /> directly.</param>
		/// <param name="trackId">The parameter that assigns <see cref="TrackId" /> directly.</param>
		/// <param name="reserved">The parameter that assigns <see cref="Reserved" /> directly.</param>
		/// <param name="duration">The parameter that assigns <see cref="Duration" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4TrackHeaderBoxData(ulong creationTime, ulong modificationTime, uint trackId, uint reserved, ulong duration, byte version, uint flags)
		{
			this.CreationTime = creationTime;
			this.ModificationTime = modificationTime;
			this.TrackId = trackId;
			this.Reserved = reserved;
			this.Duration = duration;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TrackHeaderBoxData" /> class.
		/// </summary>
		public Mp4TrackHeaderBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4TrackHeaderBoxData val
				&& EqualityComparer<ulong?>.Default.Equals(this.CreationTime, val.CreationTime)
				&& EqualityComparer<ulong?>.Default.Equals(this.ModificationTime, val.ModificationTime)
				&& EqualityComparer<uint?>.Default.Equals(this.TrackId, val.TrackId)
				&& EqualityComparer<uint?>.Default.Equals(this.Reserved, val.Reserved)
				&& EqualityComparer<ulong?>.Default.Equals(this.Duration, val.Duration)
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
				hash = hash * 23 + (CreationTime?.GetHashCode() ?? 0);
				hash = hash * 23 + (ModificationTime?.GetHashCode() ?? 0);
				hash = hash * 23 + (TrackId?.GetHashCode() ?? 0);
				hash = hash * 23 + (Reserved?.GetHashCode() ?? 0);
				hash = hash * 23 + (Duration?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4TrackHeaderBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>traf</c> box data representation.
	/// </summary>
	[FourCC("traf")]
	public class Mp4TrackFragmentBoxData : IMp4BoxData, IEquatable<Mp4TrackFragmentBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Children.
		/// </summary>
		public IList<Mp4Box>? Children { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TrackFragmentBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="children">The parameter that assigns <see cref="Children" /> directly.</param>
		public Mp4TrackFragmentBoxData(IList<Mp4Box> children)
		{
			this.Children = children;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TrackFragmentBoxData" /> class.
		/// </summary>
		public Mp4TrackFragmentBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4TrackFragmentBoxData val
				&& EqualityComparer<IList<Mp4Box>?>.Default.Equals(this.Children, val.Children)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Children?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4TrackFragmentBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>trak</c> box data representation.
	/// </summary>
	[FourCC("trak")]
	public class Mp4TrackBoxData : IMp4BoxData, IEquatable<Mp4TrackBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Children.
		/// </summary>
		public IList<Mp4Box>? Children { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TrackBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="children">The parameter that assigns <see cref="Children" /> directly.</param>
		public Mp4TrackBoxData(IList<Mp4Box> children)
		{
			this.Children = children;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TrackBoxData" /> class.
		/// </summary>
		public Mp4TrackBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4TrackBoxData val
				&& EqualityComparer<IList<Mp4Box>?>.Default.Equals(this.Children, val.Children)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Children?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4TrackBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>tref</c> box data representation.
	/// </summary>
	[FourCC("tref")]
	public class Mp4TrackReferenceContainerBoxData : IMp4BoxData, IEquatable<Mp4TrackReferenceContainerBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Boxes.
		/// </summary>
		public IList<Mp4Box>? Boxes { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TrackReferenceContainerBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="boxes">The parameter that assigns <see cref="Boxes" /> directly.</param>
		public Mp4TrackReferenceContainerBoxData(IList<Mp4Box> boxes)
		{
			this.Boxes = boxes;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TrackReferenceContainerBoxData" /> class.
		/// </summary>
		public Mp4TrackReferenceContainerBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4TrackReferenceContainerBoxData val
				&& EqualityComparer<IList<Mp4Box>?>.Default.Equals(this.Boxes, val.Boxes)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Boxes?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4TrackReferenceContainerBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>trep</c> box data representation.
	/// </summary>
	[FourCC("trep")]
	public class Mp4TrackExtensionPropertiesBoxData : IMp4FullBoxData, IEquatable<Mp4TrackExtensionPropertiesBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, TrackId.
		/// </summary>
		public uint? TrackId { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Boxes.
		/// </summary>
		public IList<Mp4Box>? Boxes { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TrackExtensionPropertiesBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="trackId">The parameter that assigns <see cref="TrackId" /> directly.</param>
		/// <param name="boxes">The parameter that assigns <see cref="Boxes" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4TrackExtensionPropertiesBoxData(uint trackId, IList<Mp4Box> boxes, byte version, uint flags)
		{
			this.TrackId = trackId;
			this.Boxes = boxes;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TrackExtensionPropertiesBoxData" /> class.
		/// </summary>
		public Mp4TrackExtensionPropertiesBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4TrackExtensionPropertiesBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.TrackId, val.TrackId)
				&& EqualityComparer<IList<Mp4Box>?>.Default.Equals(this.Boxes, val.Boxes)
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
				hash = hash * 23 + (TrackId?.GetHashCode() ?? 0);
				hash = hash * 23 + (Boxes?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4TrackExtensionPropertiesBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>trex</c> box data representation.
	/// </summary>
	[FourCC("trex")]
	public class Mp4TrackExtendsBoxData : IMp4FullBoxData, IEquatable<Mp4TrackExtendsBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, TrackId.
		/// </summary>
		public uint? TrackId { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, DefaultSampleDescriptionIndex.
		/// </summary>
		public uint? DefaultSampleDescriptionIndex { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, DefaultSampleDuration.
		/// </summary>
		public uint? DefaultSampleDuration { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, DefaultSampleSize.
		/// </summary>
		public uint? DefaultSampleSize { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, DefaultSampleFlags.
		/// </summary>
		public uint? DefaultSampleFlags { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TrackExtendsBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="trackId">The parameter that assigns <see cref="TrackId" /> directly.</param>
		/// <param name="defaultSampleDescriptionIndex">The parameter that assigns <see cref="DefaultSampleDescriptionIndex" /> directly.</param>
		/// <param name="defaultSampleDuration">The parameter that assigns <see cref="DefaultSampleDuration" /> directly.</param>
		/// <param name="defaultSampleSize">The parameter that assigns <see cref="DefaultSampleSize" /> directly.</param>
		/// <param name="defaultSampleFlags">The parameter that assigns <see cref="DefaultSampleFlags" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4TrackExtendsBoxData(uint trackId, uint defaultSampleDescriptionIndex, uint defaultSampleDuration, uint defaultSampleSize, uint defaultSampleFlags, byte version, uint flags)
		{
			this.TrackId = trackId;
			this.DefaultSampleDescriptionIndex = defaultSampleDescriptionIndex;
			this.DefaultSampleDuration = defaultSampleDuration;
			this.DefaultSampleSize = defaultSampleSize;
			this.DefaultSampleFlags = defaultSampleFlags;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TrackExtendsBoxData" /> class.
		/// </summary>
		public Mp4TrackExtendsBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4TrackExtendsBoxData val
				&& EqualityComparer<uint?>.Default.Equals(this.TrackId, val.TrackId)
				&& EqualityComparer<uint?>.Default.Equals(this.DefaultSampleDescriptionIndex, val.DefaultSampleDescriptionIndex)
				&& EqualityComparer<uint?>.Default.Equals(this.DefaultSampleDuration, val.DefaultSampleDuration)
				&& EqualityComparer<uint?>.Default.Equals(this.DefaultSampleSize, val.DefaultSampleSize)
				&& EqualityComparer<uint?>.Default.Equals(this.DefaultSampleFlags, val.DefaultSampleFlags)
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
				hash = hash * 23 + (TrackId?.GetHashCode() ?? 0);
				hash = hash * 23 + (DefaultSampleDescriptionIndex?.GetHashCode() ?? 0);
				hash = hash * 23 + (DefaultSampleDuration?.GetHashCode() ?? 0);
				hash = hash * 23 + (DefaultSampleSize?.GetHashCode() ?? 0);
				hash = hash * 23 + (DefaultSampleFlags?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4TrackExtendsBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>trgr</c> box data representation.
	/// </summary>
	[FourCC("trgr")]
	public class Mp4TrackGroupingInformationBoxData : IMp4BoxData, IEquatable<Mp4TrackGroupingInformationBoxData?>
	{


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TrackGroupingInformationBoxData" /> class.
		/// </summary>
		public Mp4TrackGroupingInformationBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4TrackGroupingInformationBoxData val
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
		public bool Equals(Mp4TrackGroupingInformationBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>tyco</c> box data representation.
	/// </summary>
	[FourCC("tyco")]
	public class Mp4TypeAndCombinationBoxData : IMp4BoxData, IEquatable<Mp4TypeAndCombinationBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, CompatibleBrands.
		/// </summary>
		public IList<uint>? CompatibleBrands { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TypeAndCombinationBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="compatibleBrands">The parameter that assigns <see cref="CompatibleBrands" /> directly.</param>
		public Mp4TypeAndCombinationBoxData(IList<uint> compatibleBrands)
		{
			this.CompatibleBrands = compatibleBrands;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4TypeAndCombinationBoxData" /> class.
		/// </summary>
		public Mp4TypeAndCombinationBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4TypeAndCombinationBoxData val
				&& EqualityComparer<IList<uint>?>.Default.Equals(this.CompatibleBrands, val.CompatibleBrands)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (CompatibleBrands?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4TypeAndCombinationBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>udta</c> box data representation.
	/// </summary>
	[FourCC("udta")]
	public class Mp4UserDataBoxData : IMp4BoxData, IEquatable<Mp4UserDataBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Children.
		/// </summary>
		public IList<Mp4Box>? Children { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4UserDataBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="children">The parameter that assigns <see cref="Children" /> directly.</param>
		public Mp4UserDataBoxData(IList<Mp4Box> children)
		{
			this.Children = children;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4UserDataBoxData" /> class.
		/// </summary>
		public Mp4UserDataBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4UserDataBoxData val
				&& EqualityComparer<IList<Mp4Box>?>.Default.Equals(this.Children, val.Children)
				;
		}

		/// <inheritdoc cref="object.GetHashCode" />
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + (Children?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4UserDataBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>vmhd</c> box data representation.
	/// </summary>
	[FourCC("vmhd")]
	public class Mp4VideoMediaHeaderBoxData : IMp4FullBoxData, IEquatable<Mp4VideoMediaHeaderBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, GraphicsMode.
		/// </summary>
		public ushort? GraphicsMode { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, TopColor.
		/// </summary>
		public IList<ushort>? TopColor { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4VideoMediaHeaderBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="graphicsMode">The parameter that assigns <see cref="GraphicsMode" /> directly.</param>
		/// <param name="topColor">The parameter that assigns <see cref="TopColor" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4VideoMediaHeaderBoxData(ushort graphicsMode, IList<ushort> topColor, byte version, uint flags)
		{
			this.GraphicsMode = graphicsMode;
			this.TopColor = topColor;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4VideoMediaHeaderBoxData" /> class.
		/// </summary>
		public Mp4VideoMediaHeaderBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4VideoMediaHeaderBoxData val
				&& EqualityComparer<ushort?>.Default.Equals(this.GraphicsMode, val.GraphicsMode)
				&& EqualityComparer<IList<ushort>?>.Default.Equals(this.TopColor, val.TopColor)
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
				hash = hash * 23 + (GraphicsMode?.GetHashCode() ?? 0);
				hash = hash * 23 + (TopColor?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4VideoMediaHeaderBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>xml </c> box data representation.
	/// </summary>
	[FourCC("xml ")]
	public class Mp4XmlContainerBoxData : IMp4FullBoxData, IEquatable<Mp4XmlContainerBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Xml.
		/// </summary>
		public string? Xml { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4XmlContainerBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="xml">The parameter that assigns <see cref="Xml" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4XmlContainerBoxData(string xml, byte version, uint flags)
		{
			this.Xml = xml;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4XmlContainerBoxData" /> class.
		/// </summary>
		public Mp4XmlContainerBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4XmlContainerBoxData val
				&& EqualityComparer<string?>.Default.Equals(this.Xml, val.Xml)
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
				hash = hash * 23 + (Xml?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4XmlContainerBoxData? other) => Equals((object?)other);
	}

	/// <summary>
	///   The MP4 <c>uri </c> box data representation.
	/// </summary>
	[FourCC("uri ")]
	public class Mp4UriBoxData : IMp4FullBoxData, IEquatable<Mp4UriBoxData?>
	{
	
		/// <summary>
		///   Represents one of MP4 box properties, named, Uri.
		/// </summary>
		public string? Uri { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Version.
		/// </summary>
		public byte? Version { get; set; }

	
		/// <summary>
		///   Represents one of MP4 box properties, named, Flags.
		/// </summary>
		public uint? Flags { get; set; }


		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4UriBoxData" /> class with the specified value.
		/// </summary>
		/// <param name="uri">The parameter that assigns <see cref="Uri" /> directly.</param>
		/// <param name="version">The parameter that assigns <see cref="Version" /> directly.</param>
		/// <param name="flags">The parameter that assigns <see cref="Flags" /> directly.</param>
		public Mp4UriBoxData(string uri, byte version, uint flags)
		{
			this.Uri = uri;
			this.Version = version;
			this.Flags = flags;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Mp4UriBoxData" /> class.
		/// </summary>
		public Mp4UriBoxData()
		{
		}

		/// <inheritdoc cref="object.Equals" />
		public override bool Equals(object? other)
		{
			return other is Mp4UriBoxData val
				&& EqualityComparer<string?>.Default.Equals(this.Uri, val.Uri)
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
				hash = hash * 23 + (Uri?.GetHashCode() ?? 0);
				hash = hash * 23 + (Version?.GetHashCode() ?? 0);
				hash = hash * 23 + (Flags?.GetHashCode() ?? 0);
				return hash;
			}
		}

		/// <inheritdoc cref="IEquatable{T}.Equals" />
		public bool Equals(Mp4UriBoxData? other) => Equals((object?)other);
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
		///   Changes the <see cref="Mp4ScrambleSchemeInfoBoxData.SchemeTypeBox" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ScrambleSchemeInfoBoxData.SchemeTypeBox" />
		///   property.
		/// </returns>
		public static Mp4ScrambleSchemeInfoBoxData WithSchemeTypeBox(
			this Mp4ScrambleSchemeInfoBoxData sourceBox,
			Mp4Box valueToReplaceWith)
		{
			sourceBox.SchemeTypeBox = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4ScrambleSchemeInfoBoxData.Info" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ScrambleSchemeInfoBoxData.Info" />
		///   property.
		/// </returns>
		public static Mp4ScrambleSchemeInfoBoxData WithInfo(
			this Mp4ScrambleSchemeInfoBoxData sourceBox,
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
		///   Changes the <see cref="Mp4UrlBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4UrlBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4UrlBoxData WithFlags(
			this Mp4UrlBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4UrlBoxData.Location" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4UrlBoxData.Location" />
		///   property.
		/// </returns>
		public static Mp4UrlBoxData WithLocation(
			this Mp4UrlBoxData sourceBox,
			string valueToReplaceWith)
		{
			sourceBox.Location = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4UrnBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4UrnBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4UrnBoxData WithFlags(
			this Mp4UrnBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4UrnBoxData.Name" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4UrnBoxData.Name" />
		///   property.
		/// </returns>
		public static Mp4UrnBoxData WithName(
			this Mp4UrnBoxData sourceBox,
			string valueToReplaceWith)
		{
			sourceBox.Name = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4UrnBoxData.Location" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4UrnBoxData.Location" />
		///   property.
		/// </returns>
		public static Mp4UrnBoxData WithLocation(
			this Mp4UrnBoxData sourceBox,
			string valueToReplaceWith)
		{
			sourceBox.Location = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4IdentifiedMediaDataImdtBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4IdentifiedMediaDataImdtBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4IdentifiedMediaDataImdtBoxData WithFlags(
			this Mp4IdentifiedMediaDataImdtBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4IdentifiedMediaDataImdtBoxData.ImdaRefIdentifier" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4IdentifiedMediaDataImdtBoxData.ImdaRefIdentifier" />
		///   property.
		/// </returns>
		public static Mp4IdentifiedMediaDataImdtBoxData WithImdaRefIdentifier(
			this Mp4IdentifiedMediaDataImdtBoxData sourceBox,
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
		///   Changes the <see cref="Mp4ItemPropertyContainerBoxData.Properties" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ItemPropertyContainerBoxData.Properties" />
		///   property.
		/// </returns>
		public static Mp4ItemPropertyContainerBoxData WithProperties(
			this Mp4ItemPropertyContainerBoxData sourceBox,
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
		///   Changes the <see cref="Mp4DataInformationBoxData.Children" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4DataInformationBoxData.Children" />
		///   property.
		/// </returns>
		public static Mp4DataInformationBoxData WithChildren(
			this Mp4DataInformationBoxData sourceBox,
			IList<Mp4Box> valueToReplaceWith)
		{
			sourceBox.Children = valueToReplaceWith;
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

		/// <summary>
		///   Changes the <see cref="Mp4GroupIdToNameBoxData.EntryCount" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4GroupIdToNameBoxData.EntryCount" />
		///   property.
		/// </returns>
		public static Mp4GroupIdToNameBoxData WithEntryCount(
			this Mp4GroupIdToNameBoxData sourceBox,
			ushort valueToReplaceWith)
		{
			sourceBox.EntryCount = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4GroupIdToNameBoxData.GroupId" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4GroupIdToNameBoxData.GroupId" />
		///   property.
		/// </returns>
		public static Mp4GroupIdToNameBoxData WithGroupId(
			this Mp4GroupIdToNameBoxData sourceBox,
			IList<uint> valueToReplaceWith)
		{
			sourceBox.GroupId = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4GroupIdToNameBoxData.GroupName" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4GroupIdToNameBoxData.GroupName" />
		///   property.
		/// </returns>
		public static Mp4GroupIdToNameBoxData WithGroupName(
			this Mp4GroupIdToNameBoxData sourceBox,
			IList<string> valueToReplaceWith)
		{
			sourceBox.GroupName = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4HandlerBoxData.Predefined" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4HandlerBoxData.Predefined" />
		///   property.
		/// </returns>
		public static Mp4HandlerBoxData WithPredefined(
			this Mp4HandlerBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Predefined = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4HandlerBoxData.HandlerType" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4HandlerBoxData.HandlerType" />
		///   property.
		/// </returns>
		public static Mp4HandlerBoxData WithHandlerType(
			this Mp4HandlerBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.HandlerType = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4HandlerBoxData.Reserved" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4HandlerBoxData.Reserved" />
		///   property.
		/// </returns>
		public static Mp4HandlerBoxData WithReserved(
			this Mp4HandlerBoxData sourceBox,
			IList<uint> valueToReplaceWith)
		{
			sourceBox.Reserved = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4HandlerBoxData.Name" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4HandlerBoxData.Name" />
		///   property.
		/// </returns>
		public static Mp4HandlerBoxData WithName(
			this Mp4HandlerBoxData sourceBox,
			string valueToReplaceWith)
		{
			sourceBox.Name = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4HandlerBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4HandlerBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4HandlerBoxData WithVersion(
			this Mp4HandlerBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4HandlerBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4HandlerBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4HandlerBoxData WithFlags(
			this Mp4HandlerBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4HintMediaHeaderBoxData.MaxPduSize" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4HintMediaHeaderBoxData.MaxPduSize" />
		///   property.
		/// </returns>
		public static Mp4HintMediaHeaderBoxData WithMaxPduSize(
			this Mp4HintMediaHeaderBoxData sourceBox,
			ushort valueToReplaceWith)
		{
			sourceBox.MaxPduSize = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4HintMediaHeaderBoxData.AvgPduSize" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4HintMediaHeaderBoxData.AvgPduSize" />
		///   property.
		/// </returns>
		public static Mp4HintMediaHeaderBoxData WithAvgPduSize(
			this Mp4HintMediaHeaderBoxData sourceBox,
			ushort valueToReplaceWith)
		{
			sourceBox.AvgPduSize = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4HintMediaHeaderBoxData.MaxBitRate" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4HintMediaHeaderBoxData.MaxBitRate" />
		///   property.
		/// </returns>
		public static Mp4HintMediaHeaderBoxData WithMaxBitRate(
			this Mp4HintMediaHeaderBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.MaxBitRate = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4HintMediaHeaderBoxData.AvgBitRate" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4HintMediaHeaderBoxData.AvgBitRate" />
		///   property.
		/// </returns>
		public static Mp4HintMediaHeaderBoxData WithAvgBitRate(
			this Mp4HintMediaHeaderBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.AvgBitRate = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4HintMediaHeaderBoxData.Reserved" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4HintMediaHeaderBoxData.Reserved" />
		///   property.
		/// </returns>
		public static Mp4HintMediaHeaderBoxData WithReserved(
			this Mp4HintMediaHeaderBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Reserved = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4HintMediaHeaderBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4HintMediaHeaderBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4HintMediaHeaderBoxData WithVersion(
			this Mp4HintMediaHeaderBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4HintMediaHeaderBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4HintMediaHeaderBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4HintMediaHeaderBoxData WithFlags(
			this Mp4HintMediaHeaderBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4ItemDataBoxData.Data" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ItemDataBoxData.Data" />
		///   property.
		/// </returns>
		public static Mp4ItemDataBoxData WithData(
			this Mp4ItemDataBoxData sourceBox,
			byte[] valueToReplaceWith)
		{
			sourceBox.Data = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4ItemInformationBoxData.EntryCount" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ItemInformationBoxData.EntryCount" />
		///   property.
		/// </returns>
		public static Mp4ItemInformationBoxData WithEntryCount(
			this Mp4ItemInformationBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.EntryCount = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4ItemInformationBoxData.ItemInfos" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ItemInformationBoxData.ItemInfos" />
		///   property.
		/// </returns>
		public static Mp4ItemInformationBoxData WithItemInfos(
			this Mp4ItemInformationBoxData sourceBox,
			IList<Mp4Box> valueToReplaceWith)
		{
			sourceBox.ItemInfos = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4ItemInformationBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ItemInformationBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4ItemInformationBoxData WithVersion(
			this Mp4ItemInformationBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4ItemInformationBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ItemInformationBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4ItemInformationBoxData WithFlags(
			this Mp4ItemInformationBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4IdentifiedMediaDataImdaBoxData.ImdaIdentifier" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4IdentifiedMediaDataImdaBoxData.ImdaIdentifier" />
		///   property.
		/// </returns>
		public static Mp4IdentifiedMediaDataImdaBoxData WithImdaIdentifier(
			this Mp4IdentifiedMediaDataImdaBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.ImdaIdentifier = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4IdentifiedMediaDataImdaBoxData.Data" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4IdentifiedMediaDataImdaBoxData.Data" />
		///   property.
		/// </returns>
		public static Mp4IdentifiedMediaDataImdaBoxData WithData(
			this Mp4IdentifiedMediaDataImdaBoxData sourceBox,
			byte[] valueToReplaceWith)
		{
			sourceBox.Data = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4ItemProtectionBoxData.ProtectionCount" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ItemProtectionBoxData.ProtectionCount" />
		///   property.
		/// </returns>
		public static Mp4ItemProtectionBoxData WithProtectionCount(
			this Mp4ItemProtectionBoxData sourceBox,
			ushort valueToReplaceWith)
		{
			sourceBox.ProtectionCount = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4ItemProtectionBoxData.ProtectionInformation" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ItemProtectionBoxData.ProtectionInformation" />
		///   property.
		/// </returns>
		public static Mp4ItemProtectionBoxData WithProtectionInformation(
			this Mp4ItemProtectionBoxData sourceBox,
			IList<Mp4Box> valueToReplaceWith)
		{
			sourceBox.ProtectionInformation = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4ItemProtectionBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ItemProtectionBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4ItemProtectionBoxData WithVersion(
			this Mp4ItemProtectionBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4ItemProtectionBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ItemProtectionBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4ItemProtectionBoxData WithFlags(
			this Mp4ItemProtectionBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4ItemReferenceBoxData.Boxes" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ItemReferenceBoxData.Boxes" />
		///   property.
		/// </returns>
		public static Mp4ItemReferenceBoxData WithBoxes(
			this Mp4ItemReferenceBoxData sourceBox,
			IList<Mp4Box> valueToReplaceWith)
		{
			sourceBox.Boxes = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4ItemReferenceBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ItemReferenceBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4ItemReferenceBoxData WithVersion(
			this Mp4ItemReferenceBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4ItemReferenceBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ItemReferenceBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4ItemReferenceBoxData WithFlags(
			this Mp4ItemReferenceBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4MediaDataBoxData.RawData" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MediaDataBoxData.RawData" />
		///   property.
		/// </returns>
		public static Mp4MediaDataBoxData WithRawData(
			this Mp4MediaDataBoxData sourceBox,
			Stream valueToReplaceWith)
		{
			sourceBox.RawData = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4MediaHeaderBoxData.CreationTime" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MediaHeaderBoxData.CreationTime" />
		///   property.
		/// </returns>
		public static Mp4MediaHeaderBoxData WithCreationTime(
			this Mp4MediaHeaderBoxData sourceBox,
			ulong valueToReplaceWith)
		{
			sourceBox.CreationTime = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4MediaHeaderBoxData.ModificationTime" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MediaHeaderBoxData.ModificationTime" />
		///   property.
		/// </returns>
		public static Mp4MediaHeaderBoxData WithModificationTime(
			this Mp4MediaHeaderBoxData sourceBox,
			ulong valueToReplaceWith)
		{
			sourceBox.ModificationTime = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4MediaHeaderBoxData.Timescale" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MediaHeaderBoxData.Timescale" />
		///   property.
		/// </returns>
		public static Mp4MediaHeaderBoxData WithTimescale(
			this Mp4MediaHeaderBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Timescale = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4MediaHeaderBoxData.Duration" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MediaHeaderBoxData.Duration" />
		///   property.
		/// </returns>
		public static Mp4MediaHeaderBoxData WithDuration(
			this Mp4MediaHeaderBoxData sourceBox,
			ulong valueToReplaceWith)
		{
			sourceBox.Duration = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4MediaHeaderBoxData.Pad" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MediaHeaderBoxData.Pad" />
		///   property.
		/// </returns>
		public static Mp4MediaHeaderBoxData WithPad(
			this Mp4MediaHeaderBoxData sourceBox,
			bool valueToReplaceWith)
		{
			sourceBox.Pad = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4MediaHeaderBoxData.Language" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MediaHeaderBoxData.Language" />
		///   property.
		/// </returns>
		public static Mp4MediaHeaderBoxData WithLanguage(
			this Mp4MediaHeaderBoxData sourceBox,
			IList<uint> valueToReplaceWith)
		{
			sourceBox.Language = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4MediaHeaderBoxData.Predefined" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MediaHeaderBoxData.Predefined" />
		///   property.
		/// </returns>
		public static Mp4MediaHeaderBoxData WithPredefined(
			this Mp4MediaHeaderBoxData sourceBox,
			ushort valueToReplaceWith)
		{
			sourceBox.Predefined = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4MediaHeaderBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MediaHeaderBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4MediaHeaderBoxData WithVersion(
			this Mp4MediaHeaderBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4MediaHeaderBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MediaHeaderBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4MediaHeaderBoxData WithFlags(
			this Mp4MediaHeaderBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4MediaBoxData.Children" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MediaBoxData.Children" />
		///   property.
		/// </returns>
		public static Mp4MediaBoxData WithChildren(
			this Mp4MediaBoxData sourceBox,
			IList<Mp4Box> valueToReplaceWith)
		{
			sourceBox.Children = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4MovieExtendsHeaderBoxData.FragmentDuration" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MovieExtendsHeaderBoxData.FragmentDuration" />
		///   property.
		/// </returns>
		public static Mp4MovieExtendsHeaderBoxData WithFragmentDuration(
			this Mp4MovieExtendsHeaderBoxData sourceBox,
			ulong valueToReplaceWith)
		{
			sourceBox.FragmentDuration = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4MetaBoxBoxData.Boxes" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MetaBoxBoxData.Boxes" />
		///   property.
		/// </returns>
		public static Mp4MetaBoxBoxData WithBoxes(
			this Mp4MetaBoxBoxData sourceBox,
			IList<Mp4Box> valueToReplaceWith)
		{
			sourceBox.Boxes = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4MetaBoxBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MetaBoxBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4MetaBoxBoxData WithVersion(
			this Mp4MetaBoxBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4MetaBoxBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MetaBoxBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4MetaBoxBoxData WithFlags(
			this Mp4MetaBoxBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4MovieFragmentHeaderBoxData.SequenceNumber" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MovieFragmentHeaderBoxData.SequenceNumber" />
		///   property.
		/// </returns>
		public static Mp4MovieFragmentHeaderBoxData WithSequenceNumber(
			this Mp4MovieFragmentHeaderBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.SequenceNumber = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4MovieFragmentHeaderBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MovieFragmentHeaderBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4MovieFragmentHeaderBoxData WithVersion(
			this Mp4MovieFragmentHeaderBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4MovieFragmentHeaderBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MovieFragmentHeaderBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4MovieFragmentHeaderBoxData WithFlags(
			this Mp4MovieFragmentHeaderBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4MovieFragmentRandomAccessOffsetBoxData.ParentSize" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MovieFragmentRandomAccessOffsetBoxData.ParentSize" />
		///   property.
		/// </returns>
		public static Mp4MovieFragmentRandomAccessOffsetBoxData WithParentSize(
			this Mp4MovieFragmentRandomAccessOffsetBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.ParentSize = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4MovieFragmentRandomAccessOffsetBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MovieFragmentRandomAccessOffsetBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4MovieFragmentRandomAccessOffsetBoxData WithVersion(
			this Mp4MovieFragmentRandomAccessOffsetBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4MovieFragmentRandomAccessOffsetBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MovieFragmentRandomAccessOffsetBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4MovieFragmentRandomAccessOffsetBoxData WithFlags(
			this Mp4MovieFragmentRandomAccessOffsetBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4MovieFragmentBoxData.Children" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MovieFragmentBoxData.Children" />
		///   property.
		/// </returns>
		public static Mp4MovieFragmentBoxData WithChildren(
			this Mp4MovieFragmentBoxData sourceBox,
			IList<Mp4Box> valueToReplaceWith)
		{
			sourceBox.Children = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4MovieBoxData.Children" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MovieBoxData.Children" />
		///   property.
		/// </returns>
		public static Mp4MovieBoxData WithChildren(
			this Mp4MovieBoxData sourceBox,
			IList<Mp4Box> valueToReplaceWith)
		{
			sourceBox.Children = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4MovieExtendsBoxData.Children" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MovieExtendsBoxData.Children" />
		///   property.
		/// </returns>
		public static Mp4MovieExtendsBoxData WithChildren(
			this Mp4MovieExtendsBoxData sourceBox,
			IList<Mp4Box> valueToReplaceWith)
		{
			sourceBox.Children = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4MovieHeaderBoxData.Children" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4MovieHeaderBoxData.Children" />
		///   property.
		/// </returns>
		public static Mp4MovieHeaderBoxData WithChildren(
			this Mp4MovieHeaderBoxData sourceBox,
			IList<Mp4Box> valueToReplaceWith)
		{
			sourceBox.Children = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4NullMediaHeaderBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4NullMediaHeaderBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4NullMediaHeaderBoxData WithVersion(
			this Mp4NullMediaHeaderBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4NullMediaHeaderBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4NullMediaHeaderBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4NullMediaHeaderBoxData WithFlags(
			this Mp4NullMediaHeaderBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SamplePaddingBitsBoxData.SampleCount" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SamplePaddingBitsBoxData.SampleCount" />
		///   property.
		/// </returns>
		public static Mp4SamplePaddingBitsBoxData WithSampleCount(
			this Mp4SamplePaddingBitsBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.SampleCount = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SamplePaddingBitsBoxData.Reserved1" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SamplePaddingBitsBoxData.Reserved1" />
		///   property.
		/// </returns>
		public static Mp4SamplePaddingBitsBoxData WithReserved1(
			this Mp4SamplePaddingBitsBoxData sourceBox,
			IList<bool> valueToReplaceWith)
		{
			sourceBox.Reserved1 = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SamplePaddingBitsBoxData.Pad1" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SamplePaddingBitsBoxData.Pad1" />
		///   property.
		/// </returns>
		public static Mp4SamplePaddingBitsBoxData WithPad1(
			this Mp4SamplePaddingBitsBoxData sourceBox,
			IList<byte> valueToReplaceWith)
		{
			sourceBox.Pad1 = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SamplePaddingBitsBoxData.Reserved2" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SamplePaddingBitsBoxData.Reserved2" />
		///   property.
		/// </returns>
		public static Mp4SamplePaddingBitsBoxData WithReserved2(
			this Mp4SamplePaddingBitsBoxData sourceBox,
			IList<bool> valueToReplaceWith)
		{
			sourceBox.Reserved2 = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SamplePaddingBitsBoxData.Pad2" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SamplePaddingBitsBoxData.Pad2" />
		///   property.
		/// </returns>
		public static Mp4SamplePaddingBitsBoxData WithPad2(
			this Mp4SamplePaddingBitsBoxData sourceBox,
			IList<byte> valueToReplaceWith)
		{
			sourceBox.Pad2 = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SamplePaddingBitsBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SamplePaddingBitsBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4SamplePaddingBitsBoxData WithVersion(
			this Mp4SamplePaddingBitsBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SamplePaddingBitsBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SamplePaddingBitsBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4SamplePaddingBitsBoxData WithFlags(
			this Mp4SamplePaddingBitsBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4ProgressiveDownloadInformationBoxData.Rate" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ProgressiveDownloadInformationBoxData.Rate" />
		///   property.
		/// </returns>
		public static Mp4ProgressiveDownloadInformationBoxData WithRate(
			this Mp4ProgressiveDownloadInformationBoxData sourceBox,
			IList<uint> valueToReplaceWith)
		{
			sourceBox.Rate = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4ProgressiveDownloadInformationBoxData.InitialDelay" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ProgressiveDownloadInformationBoxData.InitialDelay" />
		///   property.
		/// </returns>
		public static Mp4ProgressiveDownloadInformationBoxData WithInitialDelay(
			this Mp4ProgressiveDownloadInformationBoxData sourceBox,
			IList<uint> valueToReplaceWith)
		{
			sourceBox.InitialDelay = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4ProgressiveDownloadInformationBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ProgressiveDownloadInformationBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4ProgressiveDownloadInformationBoxData WithVersion(
			this Mp4ProgressiveDownloadInformationBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4ProgressiveDownloadInformationBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ProgressiveDownloadInformationBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4ProgressiveDownloadInformationBoxData WithFlags(
			this Mp4ProgressiveDownloadInformationBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4PrimaryItemReferenceBoxData.ItemId" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4PrimaryItemReferenceBoxData.ItemId" />
		///   property.
		/// </returns>
		public static Mp4PrimaryItemReferenceBoxData WithItemId(
			this Mp4PrimaryItemReferenceBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.ItemId = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4PrimaryItemReferenceBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4PrimaryItemReferenceBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4PrimaryItemReferenceBoxData WithVersion(
			this Mp4PrimaryItemReferenceBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4PrimaryItemReferenceBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4PrimaryItemReferenceBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4PrimaryItemReferenceBoxData WithFlags(
			this Mp4PrimaryItemReferenceBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4ProducerReferenceTimeBoxData.ReferenceTrackId" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ProducerReferenceTimeBoxData.ReferenceTrackId" />
		///   property.
		/// </returns>
		public static Mp4ProducerReferenceTimeBoxData WithReferenceTrackId(
			this Mp4ProducerReferenceTimeBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.ReferenceTrackId = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4ProducerReferenceTimeBoxData.NtpTimestamp" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ProducerReferenceTimeBoxData.NtpTimestamp" />
		///   property.
		/// </returns>
		public static Mp4ProducerReferenceTimeBoxData WithNtpTimestamp(
			this Mp4ProducerReferenceTimeBoxData sourceBox,
			ulong valueToReplaceWith)
		{
			sourceBox.NtpTimestamp = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4ProducerReferenceTimeBoxData.MediaTime" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ProducerReferenceTimeBoxData.MediaTime" />
		///   property.
		/// </returns>
		public static Mp4ProducerReferenceTimeBoxData WithMediaTime(
			this Mp4ProducerReferenceTimeBoxData sourceBox,
			ulong valueToReplaceWith)
		{
			sourceBox.MediaTime = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4ProducerReferenceTimeBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ProducerReferenceTimeBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4ProducerReferenceTimeBoxData WithVersion(
			this Mp4ProducerReferenceTimeBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4ProducerReferenceTimeBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ProducerReferenceTimeBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4ProducerReferenceTimeBoxData WithFlags(
			this Mp4ProducerReferenceTimeBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4RestrictedSchemeInformationBoxData.OriginalFormat" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4RestrictedSchemeInformationBoxData.OriginalFormat" />
		///   property.
		/// </returns>
		public static Mp4RestrictedSchemeInformationBoxData WithOriginalFormat(
			this Mp4RestrictedSchemeInformationBoxData sourceBox,
			Mp4Box valueToReplaceWith)
		{
			sourceBox.OriginalFormat = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4RestrictedSchemeInformationBoxData.SchemeTypeBox" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4RestrictedSchemeInformationBoxData.SchemeTypeBox" />
		///   property.
		/// </returns>
		public static Mp4RestrictedSchemeInformationBoxData WithSchemeTypeBox(
			this Mp4RestrictedSchemeInformationBoxData sourceBox,
			Mp4Box valueToReplaceWith)
		{
			sourceBox.SchemeTypeBox = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4RestrictedSchemeInformationBoxData.Info" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4RestrictedSchemeInformationBoxData.Info" />
		///   property.
		/// </returns>
		public static Mp4RestrictedSchemeInformationBoxData WithInfo(
			this Mp4RestrictedSchemeInformationBoxData sourceBox,
			Mp4Box valueToReplaceWith)
		{
			sourceBox.Info = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleAuxiliaryInformationOffsetsBoxData.AuxInfoType" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleAuxiliaryInformationOffsetsBoxData.AuxInfoType" />
		///   property.
		/// </returns>
		public static Mp4SampleAuxiliaryInformationOffsetsBoxData WithAuxInfoType(
			this Mp4SampleAuxiliaryInformationOffsetsBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.AuxInfoType = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleAuxiliaryInformationOffsetsBoxData.AuxInfoTypeParameter" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleAuxiliaryInformationOffsetsBoxData.AuxInfoTypeParameter" />
		///   property.
		/// </returns>
		public static Mp4SampleAuxiliaryInformationOffsetsBoxData WithAuxInfoTypeParameter(
			this Mp4SampleAuxiliaryInformationOffsetsBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.AuxInfoTypeParameter = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleAuxiliaryInformationOffsetsBoxData.EntryCount" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleAuxiliaryInformationOffsetsBoxData.EntryCount" />
		///   property.
		/// </returns>
		public static Mp4SampleAuxiliaryInformationOffsetsBoxData WithEntryCount(
			this Mp4SampleAuxiliaryInformationOffsetsBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.EntryCount = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleAuxiliaryInformationOffsetsBoxData.Offsets" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleAuxiliaryInformationOffsetsBoxData.Offsets" />
		///   property.
		/// </returns>
		public static Mp4SampleAuxiliaryInformationOffsetsBoxData WithOffsets(
			this Mp4SampleAuxiliaryInformationOffsetsBoxData sourceBox,
			IList<ulong> valueToReplaceWith)
		{
			sourceBox.Offsets = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleAuxiliaryInformationOffsetsBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleAuxiliaryInformationOffsetsBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4SampleAuxiliaryInformationOffsetsBoxData WithVersion(
			this Mp4SampleAuxiliaryInformationOffsetsBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleAuxiliaryInformationOffsetsBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleAuxiliaryInformationOffsetsBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4SampleAuxiliaryInformationOffsetsBoxData WithFlags(
			this Mp4SampleAuxiliaryInformationOffsetsBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleAuxiliaryInformationSizeBoxData.AuxInfoType" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleAuxiliaryInformationSizeBoxData.AuxInfoType" />
		///   property.
		/// </returns>
		public static Mp4SampleAuxiliaryInformationSizeBoxData WithAuxInfoType(
			this Mp4SampleAuxiliaryInformationSizeBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.AuxInfoType = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleAuxiliaryInformationSizeBoxData.AuxInfoTypeParameter" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleAuxiliaryInformationSizeBoxData.AuxInfoTypeParameter" />
		///   property.
		/// </returns>
		public static Mp4SampleAuxiliaryInformationSizeBoxData WithAuxInfoTypeParameter(
			this Mp4SampleAuxiliaryInformationSizeBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.AuxInfoTypeParameter = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleAuxiliaryInformationSizeBoxData.DefaultSampleInfoSize" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleAuxiliaryInformationSizeBoxData.DefaultSampleInfoSize" />
		///   property.
		/// </returns>
		public static Mp4SampleAuxiliaryInformationSizeBoxData WithDefaultSampleInfoSize(
			this Mp4SampleAuxiliaryInformationSizeBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.DefaultSampleInfoSize = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleAuxiliaryInformationSizeBoxData.SampleCount" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleAuxiliaryInformationSizeBoxData.SampleCount" />
		///   property.
		/// </returns>
		public static Mp4SampleAuxiliaryInformationSizeBoxData WithSampleCount(
			this Mp4SampleAuxiliaryInformationSizeBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.SampleCount = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleAuxiliaryInformationSizeBoxData.SampleInfoSize" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleAuxiliaryInformationSizeBoxData.SampleInfoSize" />
		///   property.
		/// </returns>
		public static Mp4SampleAuxiliaryInformationSizeBoxData WithSampleInfoSize(
			this Mp4SampleAuxiliaryInformationSizeBoxData sourceBox,
			IList<byte> valueToReplaceWith)
		{
			sourceBox.SampleInfoSize = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleAuxiliaryInformationSizeBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleAuxiliaryInformationSizeBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4SampleAuxiliaryInformationSizeBoxData WithVersion(
			this Mp4SampleAuxiliaryInformationSizeBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleAuxiliaryInformationSizeBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleAuxiliaryInformationSizeBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4SampleAuxiliaryInformationSizeBoxData WithFlags(
			this Mp4SampleAuxiliaryInformationSizeBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleToGroupBoxData.GroupingType" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleToGroupBoxData.GroupingType" />
		///   property.
		/// </returns>
		public static Mp4SampleToGroupBoxData WithGroupingType(
			this Mp4SampleToGroupBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.GroupingType = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleToGroupBoxData.GroupingTypeParameter" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleToGroupBoxData.GroupingTypeParameter" />
		///   property.
		/// </returns>
		public static Mp4SampleToGroupBoxData WithGroupingTypeParameter(
			this Mp4SampleToGroupBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.GroupingTypeParameter = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleToGroupBoxData.EntryCount" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleToGroupBoxData.EntryCount" />
		///   property.
		/// </returns>
		public static Mp4SampleToGroupBoxData WithEntryCount(
			this Mp4SampleToGroupBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.EntryCount = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleToGroupBoxData.SampleCount" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleToGroupBoxData.SampleCount" />
		///   property.
		/// </returns>
		public static Mp4SampleToGroupBoxData WithSampleCount(
			this Mp4SampleToGroupBoxData sourceBox,
			IList<uint> valueToReplaceWith)
		{
			sourceBox.SampleCount = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleToGroupBoxData.GroupDescriptionIndex" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleToGroupBoxData.GroupDescriptionIndex" />
		///   property.
		/// </returns>
		public static Mp4SampleToGroupBoxData WithGroupDescriptionIndex(
			this Mp4SampleToGroupBoxData sourceBox,
			IList<uint> valueToReplaceWith)
		{
			sourceBox.GroupDescriptionIndex = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleToGroupBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleToGroupBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4SampleToGroupBoxData WithVersion(
			this Mp4SampleToGroupBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleToGroupBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleToGroupBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4SampleToGroupBoxData WithFlags(
			this Mp4SampleToGroupBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SchemeInformationBoxData.SchemeSpecificData" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SchemeInformationBoxData.SchemeSpecificData" />
		///   property.
		/// </returns>
		public static Mp4SchemeInformationBoxData WithSchemeSpecificData(
			this Mp4SchemeInformationBoxData sourceBox,
			IList<Mp4Box> valueToReplaceWith)
		{
			sourceBox.SchemeSpecificData = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SchemeTypeBoxData.SchemeType" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SchemeTypeBoxData.SchemeType" />
		///   property.
		/// </returns>
		public static Mp4SchemeTypeBoxData WithSchemeType(
			this Mp4SchemeTypeBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.SchemeType = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SchemeTypeBoxData.SchemeVersion" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SchemeTypeBoxData.SchemeVersion" />
		///   property.
		/// </returns>
		public static Mp4SchemeTypeBoxData WithSchemeVersion(
			this Mp4SchemeTypeBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.SchemeVersion = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SchemeTypeBoxData.SchemeUri" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SchemeTypeBoxData.SchemeUri" />
		///   property.
		/// </returns>
		public static Mp4SchemeTypeBoxData WithSchemeUri(
			this Mp4SchemeTypeBoxData sourceBox,
			string valueToReplaceWith)
		{
			sourceBox.SchemeUri = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SchemeTypeBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SchemeTypeBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4SchemeTypeBoxData WithVersion(
			this Mp4SchemeTypeBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SchemeTypeBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SchemeTypeBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4SchemeTypeBoxData WithFlags(
			this Mp4SchemeTypeBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4CompatibleSchemeTypeBoxData.SchemeType" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4CompatibleSchemeTypeBoxData.SchemeType" />
		///   property.
		/// </returns>
		public static Mp4CompatibleSchemeTypeBoxData WithSchemeType(
			this Mp4CompatibleSchemeTypeBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.SchemeType = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4CompatibleSchemeTypeBoxData.SchemeVersion" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4CompatibleSchemeTypeBoxData.SchemeVersion" />
		///   property.
		/// </returns>
		public static Mp4CompatibleSchemeTypeBoxData WithSchemeVersion(
			this Mp4CompatibleSchemeTypeBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.SchemeVersion = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4CompatibleSchemeTypeBoxData.SchemeUri" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4CompatibleSchemeTypeBoxData.SchemeUri" />
		///   property.
		/// </returns>
		public static Mp4CompatibleSchemeTypeBoxData WithSchemeUri(
			this Mp4CompatibleSchemeTypeBoxData sourceBox,
			string valueToReplaceWith)
		{
			sourceBox.SchemeUri = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4CompatibleSchemeTypeBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4CompatibleSchemeTypeBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4CompatibleSchemeTypeBoxData WithVersion(
			this Mp4CompatibleSchemeTypeBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4CompatibleSchemeTypeBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4CompatibleSchemeTypeBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4CompatibleSchemeTypeBoxData WithFlags(
			this Mp4CompatibleSchemeTypeBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleDependencyTypeBoxData.IsLeading" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleDependencyTypeBoxData.IsLeading" />
		///   property.
		/// </returns>
		public static Mp4SampleDependencyTypeBoxData WithIsLeading(
			this Mp4SampleDependencyTypeBoxData sourceBox,
			IList<byte> valueToReplaceWith)
		{
			sourceBox.IsLeading = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleDependencyTypeBoxData.SampleDependsOn" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleDependencyTypeBoxData.SampleDependsOn" />
		///   property.
		/// </returns>
		public static Mp4SampleDependencyTypeBoxData WithSampleDependsOn(
			this Mp4SampleDependencyTypeBoxData sourceBox,
			IList<byte> valueToReplaceWith)
		{
			sourceBox.SampleDependsOn = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleDependencyTypeBoxData.SampleIsDependedOn" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleDependencyTypeBoxData.SampleIsDependedOn" />
		///   property.
		/// </returns>
		public static Mp4SampleDependencyTypeBoxData WithSampleIsDependedOn(
			this Mp4SampleDependencyTypeBoxData sourceBox,
			IList<byte> valueToReplaceWith)
		{
			sourceBox.SampleIsDependedOn = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleDependencyTypeBoxData.SampleHasRedundancy" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleDependencyTypeBoxData.SampleHasRedundancy" />
		///   property.
		/// </returns>
		public static Mp4SampleDependencyTypeBoxData WithSampleHasRedundancy(
			this Mp4SampleDependencyTypeBoxData sourceBox,
			IList<byte> valueToReplaceWith)
		{
			sourceBox.SampleHasRedundancy = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleDependencyTypeBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleDependencyTypeBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4SampleDependencyTypeBoxData WithVersion(
			this Mp4SampleDependencyTypeBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleDependencyTypeBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleDependencyTypeBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4SampleDependencyTypeBoxData WithFlags(
			this Mp4SampleDependencyTypeBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4FreeSpaceSkipBoxData.RawData" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4FreeSpaceSkipBoxData.RawData" />
		///   property.
		/// </returns>
		public static Mp4FreeSpaceSkipBoxData WithRawData(
			this Mp4FreeSpaceSkipBoxData sourceBox,
			Stream valueToReplaceWith)
		{
			sourceBox.RawData = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SoundMediaHeaderBoxData.Balance" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SoundMediaHeaderBoxData.Balance" />
		///   property.
		/// </returns>
		public static Mp4SoundMediaHeaderBoxData WithBalance(
			this Mp4SoundMediaHeaderBoxData sourceBox,
			ushort valueToReplaceWith)
		{
			sourceBox.Balance = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SoundMediaHeaderBoxData.Reserved" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SoundMediaHeaderBoxData.Reserved" />
		///   property.
		/// </returns>
		public static Mp4SoundMediaHeaderBoxData WithReserved(
			this Mp4SoundMediaHeaderBoxData sourceBox,
			ushort valueToReplaceWith)
		{
			sourceBox.Reserved = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SoundMediaHeaderBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SoundMediaHeaderBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4SoundMediaHeaderBoxData WithVersion(
			this Mp4SoundMediaHeaderBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SoundMediaHeaderBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SoundMediaHeaderBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4SoundMediaHeaderBoxData WithFlags(
			this Mp4SoundMediaHeaderBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4StrpProcessBoxData.EncryptionAlgorithmRtp" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4StrpProcessBoxData.EncryptionAlgorithmRtp" />
		///   property.
		/// </returns>
		public static Mp4StrpProcessBoxData WithEncryptionAlgorithmRtp(
			this Mp4StrpProcessBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.EncryptionAlgorithmRtp = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4StrpProcessBoxData.EncryptionAlgorithmRtcp" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4StrpProcessBoxData.EncryptionAlgorithmRtcp" />
		///   property.
		/// </returns>
		public static Mp4StrpProcessBoxData WithEncryptionAlgorithmRtcp(
			this Mp4StrpProcessBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.EncryptionAlgorithmRtcp = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4StrpProcessBoxData.IntegrityAlgorithmRtp" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4StrpProcessBoxData.IntegrityAlgorithmRtp" />
		///   property.
		/// </returns>
		public static Mp4StrpProcessBoxData WithIntegrityAlgorithmRtp(
			this Mp4StrpProcessBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.IntegrityAlgorithmRtp = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4StrpProcessBoxData.IntegrityAlgorithmRtcp" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4StrpProcessBoxData.IntegrityAlgorithmRtcp" />
		///   property.
		/// </returns>
		public static Mp4StrpProcessBoxData WithIntegrityAlgorithmRtcp(
			this Mp4StrpProcessBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.IntegrityAlgorithmRtcp = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4StrpProcessBoxData.SchemeTypeBox" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4StrpProcessBoxData.SchemeTypeBox" />
		///   property.
		/// </returns>
		public static Mp4StrpProcessBoxData WithSchemeTypeBox(
			this Mp4StrpProcessBoxData sourceBox,
			Mp4Box valueToReplaceWith)
		{
			sourceBox.SchemeTypeBox = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4StrpProcessBoxData.Info" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4StrpProcessBoxData.Info" />
		///   property.
		/// </returns>
		public static Mp4StrpProcessBoxData WithInfo(
			this Mp4StrpProcessBoxData sourceBox,
			Mp4Box valueToReplaceWith)
		{
			sourceBox.Info = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4StrpProcessBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4StrpProcessBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4StrpProcessBoxData WithVersion(
			this Mp4StrpProcessBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4StrpProcessBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4StrpProcessBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4StrpProcessBoxData WithFlags(
			this Mp4StrpProcessBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleTableBoxData.Children" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleTableBoxData.Children" />
		///   property.
		/// </returns>
		public static Mp4SampleTableBoxData WithChildren(
			this Mp4SampleTableBoxData sourceBox,
			IList<Mp4Box> valueToReplaceWith)
		{
			sourceBox.Children = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4ChunkOffsetBoxData.EntryCount" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ChunkOffsetBoxData.EntryCount" />
		///   property.
		/// </returns>
		public static Mp4ChunkOffsetBoxData WithEntryCount(
			this Mp4ChunkOffsetBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.EntryCount = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4ChunkOffsetBoxData.ChunkOffset" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ChunkOffsetBoxData.ChunkOffset" />
		///   property.
		/// </returns>
		public static Mp4ChunkOffsetBoxData WithChunkOffset(
			this Mp4ChunkOffsetBoxData sourceBox,
			IList<uint> valueToReplaceWith)
		{
			sourceBox.ChunkOffset = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleDegradationPriorityBoxData.Priority" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleDegradationPriorityBoxData.Priority" />
		///   property.
		/// </returns>
		public static Mp4SampleDegradationPriorityBoxData WithPriority(
			this Mp4SampleDegradationPriorityBoxData sourceBox,
			IList<ushort> valueToReplaceWith)
		{
			sourceBox.Priority = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SubtitleMediaHeaderBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SubtitleMediaHeaderBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4SubtitleMediaHeaderBoxData WithVersion(
			this Mp4SubtitleMediaHeaderBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SubtitleMediaHeaderBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SubtitleMediaHeaderBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4SubtitleMediaHeaderBoxData WithFlags(
			this Mp4SubtitleMediaHeaderBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SubTrackInformationBoxData.SwitchGroup" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SubTrackInformationBoxData.SwitchGroup" />
		///   property.
		/// </returns>
		public static Mp4SubTrackInformationBoxData WithSwitchGroup(
			this Mp4SubTrackInformationBoxData sourceBox,
			short valueToReplaceWith)
		{
			sourceBox.SwitchGroup = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SubTrackInformationBoxData.AlternateGroup" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SubTrackInformationBoxData.AlternateGroup" />
		///   property.
		/// </returns>
		public static Mp4SubTrackInformationBoxData WithAlternateGroup(
			this Mp4SubTrackInformationBoxData sourceBox,
			short valueToReplaceWith)
		{
			sourceBox.AlternateGroup = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SubTrackInformationBoxData.SubTrackId" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SubTrackInformationBoxData.SubTrackId" />
		///   property.
		/// </returns>
		public static Mp4SubTrackInformationBoxData WithSubTrackId(
			this Mp4SubTrackInformationBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.SubTrackId = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SubTrackInformationBoxData.AttributeList" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SubTrackInformationBoxData.AttributeList" />
		///   property.
		/// </returns>
		public static Mp4SubTrackInformationBoxData WithAttributeList(
			this Mp4SubTrackInformationBoxData sourceBox,
			IList<uint> valueToReplaceWith)
		{
			sourceBox.AttributeList = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SubTrackInformationBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SubTrackInformationBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4SubTrackInformationBoxData WithVersion(
			this Mp4SubTrackInformationBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SubTrackInformationBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SubTrackInformationBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4SubTrackInformationBoxData WithFlags(
			this Mp4SubTrackInformationBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleToChunkBoxData.EntryCount" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleToChunkBoxData.EntryCount" />
		///   property.
		/// </returns>
		public static Mp4SampleToChunkBoxData WithEntryCount(
			this Mp4SampleToChunkBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.EntryCount = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleToChunkBoxData.FirstChunk" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleToChunkBoxData.FirstChunk" />
		///   property.
		/// </returns>
		public static Mp4SampleToChunkBoxData WithFirstChunk(
			this Mp4SampleToChunkBoxData sourceBox,
			IList<uint> valueToReplaceWith)
		{
			sourceBox.FirstChunk = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleToChunkBoxData.SamplesPerChunk" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleToChunkBoxData.SamplesPerChunk" />
		///   property.
		/// </returns>
		public static Mp4SampleToChunkBoxData WithSamplesPerChunk(
			this Mp4SampleToChunkBoxData sourceBox,
			IList<uint> valueToReplaceWith)
		{
			sourceBox.SamplesPerChunk = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleToChunkBoxData.SampleDescriptionIndex" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleToChunkBoxData.SampleDescriptionIndex" />
		///   property.
		/// </returns>
		public static Mp4SampleToChunkBoxData WithSampleDescriptionIndex(
			this Mp4SampleToChunkBoxData sourceBox,
			IList<uint> valueToReplaceWith)
		{
			sourceBox.SampleDescriptionIndex = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleToChunkBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleToChunkBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4SampleToChunkBoxData WithVersion(
			this Mp4SampleToChunkBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleToChunkBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleToChunkBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4SampleToChunkBoxData WithFlags(
			this Mp4SampleToChunkBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleDescriptionBoxData.EntryCount" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleDescriptionBoxData.EntryCount" />
		///   property.
		/// </returns>
		public static Mp4SampleDescriptionBoxData WithEntryCount(
			this Mp4SampleDescriptionBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.EntryCount = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleDescriptionBoxData.SampleEntries" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleDescriptionBoxData.SampleEntries" />
		///   property.
		/// </returns>
		public static Mp4SampleDescriptionBoxData WithSampleEntries(
			this Mp4SampleDescriptionBoxData sourceBox,
			IList<Mp4Box> valueToReplaceWith)
		{
			sourceBox.SampleEntries = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleDescriptionBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleDescriptionBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4SampleDescriptionBoxData WithVersion(
			this Mp4SampleDescriptionBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleDescriptionBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleDescriptionBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4SampleDescriptionBoxData WithFlags(
			this Mp4SampleDescriptionBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SubtrackSampleGroupingBoxData.GroupingType" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SubtrackSampleGroupingBoxData.GroupingType" />
		///   property.
		/// </returns>
		public static Mp4SubtrackSampleGroupingBoxData WithGroupingType(
			this Mp4SubtrackSampleGroupingBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.GroupingType = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SubtrackSampleGroupingBoxData.ItemCount" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SubtrackSampleGroupingBoxData.ItemCount" />
		///   property.
		/// </returns>
		public static Mp4SubtrackSampleGroupingBoxData WithItemCount(
			this Mp4SubtrackSampleGroupingBoxData sourceBox,
			ushort valueToReplaceWith)
		{
			sourceBox.ItemCount = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SubtrackSampleGroupingBoxData.GroupDescriptionIndex" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SubtrackSampleGroupingBoxData.GroupDescriptionIndex" />
		///   property.
		/// </returns>
		public static Mp4SubtrackSampleGroupingBoxData WithGroupDescriptionIndex(
			this Mp4SubtrackSampleGroupingBoxData sourceBox,
			IList<uint> valueToReplaceWith)
		{
			sourceBox.GroupDescriptionIndex = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SubtrackSampleGroupingBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SubtrackSampleGroupingBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4SubtrackSampleGroupingBoxData WithVersion(
			this Mp4SubtrackSampleGroupingBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SubtrackSampleGroupingBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SubtrackSampleGroupingBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4SubtrackSampleGroupingBoxData WithFlags(
			this Mp4SubtrackSampleGroupingBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4ShadowSyncSampleTableBoxData.EntryCount" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ShadowSyncSampleTableBoxData.EntryCount" />
		///   property.
		/// </returns>
		public static Mp4ShadowSyncSampleTableBoxData WithEntryCount(
			this Mp4ShadowSyncSampleTableBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.EntryCount = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4ShadowSyncSampleTableBoxData.ShadowedSampleNumber" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ShadowSyncSampleTableBoxData.ShadowedSampleNumber" />
		///   property.
		/// </returns>
		public static Mp4ShadowSyncSampleTableBoxData WithShadowedSampleNumber(
			this Mp4ShadowSyncSampleTableBoxData sourceBox,
			IList<uint> valueToReplaceWith)
		{
			sourceBox.ShadowedSampleNumber = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4ShadowSyncSampleTableBoxData.SyncSampleNumber" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ShadowSyncSampleTableBoxData.SyncSampleNumber" />
		///   property.
		/// </returns>
		public static Mp4ShadowSyncSampleTableBoxData WithSyncSampleNumber(
			this Mp4ShadowSyncSampleTableBoxData sourceBox,
			IList<uint> valueToReplaceWith)
		{
			sourceBox.SyncSampleNumber = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4ShadowSyncSampleTableBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ShadowSyncSampleTableBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4ShadowSyncSampleTableBoxData WithVersion(
			this Mp4ShadowSyncSampleTableBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4ShadowSyncSampleTableBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4ShadowSyncSampleTableBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4ShadowSyncSampleTableBoxData WithFlags(
			this Mp4ShadowSyncSampleTableBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SyncSampleTableBoxData.EntryCount" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SyncSampleTableBoxData.EntryCount" />
		///   property.
		/// </returns>
		public static Mp4SyncSampleTableBoxData WithEntryCount(
			this Mp4SyncSampleTableBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.EntryCount = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SyncSampleTableBoxData.SampleNumber" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SyncSampleTableBoxData.SampleNumber" />
		///   property.
		/// </returns>
		public static Mp4SyncSampleTableBoxData WithSampleNumber(
			this Mp4SyncSampleTableBoxData sourceBox,
			IList<uint> valueToReplaceWith)
		{
			sourceBox.SampleNumber = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SyncSampleTableBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SyncSampleTableBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4SyncSampleTableBoxData WithVersion(
			this Mp4SyncSampleTableBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SyncSampleTableBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SyncSampleTableBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4SyncSampleTableBoxData WithFlags(
			this Mp4SyncSampleTableBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleSizeBoxData.SampleSize" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleSizeBoxData.SampleSize" />
		///   property.
		/// </returns>
		public static Mp4SampleSizeBoxData WithSampleSize(
			this Mp4SampleSizeBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.SampleSize = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleSizeBoxData.SampleCount" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleSizeBoxData.SampleCount" />
		///   property.
		/// </returns>
		public static Mp4SampleSizeBoxData WithSampleCount(
			this Mp4SampleSizeBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.SampleCount = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleSizeBoxData.EntrySize" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleSizeBoxData.EntrySize" />
		///   property.
		/// </returns>
		public static Mp4SampleSizeBoxData WithEntrySize(
			this Mp4SampleSizeBoxData sourceBox,
			IList<uint> valueToReplaceWith)
		{
			sourceBox.EntrySize = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleSizeBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleSizeBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4SampleSizeBoxData WithVersion(
			this Mp4SampleSizeBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4SampleSizeBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4SampleSizeBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4SampleSizeBoxData WithFlags(
			this Mp4SampleSizeBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4TimeToSampleBoxData.EntryCount" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TimeToSampleBoxData.EntryCount" />
		///   property.
		/// </returns>
		public static Mp4TimeToSampleBoxData WithEntryCount(
			this Mp4TimeToSampleBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.EntryCount = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4TimeToSampleBoxData.SampleCount" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TimeToSampleBoxData.SampleCount" />
		///   property.
		/// </returns>
		public static Mp4TimeToSampleBoxData WithSampleCount(
			this Mp4TimeToSampleBoxData sourceBox,
			IList<uint> valueToReplaceWith)
		{
			sourceBox.SampleCount = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4TimeToSampleBoxData.SampleDelta" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TimeToSampleBoxData.SampleDelta" />
		///   property.
		/// </returns>
		public static Mp4TimeToSampleBoxData WithSampleDelta(
			this Mp4TimeToSampleBoxData sourceBox,
			IList<uint> valueToReplaceWith)
		{
			sourceBox.SampleDelta = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4TimeToSampleBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TimeToSampleBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4TimeToSampleBoxData WithVersion(
			this Mp4TimeToSampleBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4TimeToSampleBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TimeToSampleBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4TimeToSampleBoxData WithFlags(
			this Mp4TimeToSampleBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4TrackFrameDecodeTimeBoxData.BaseMediaDecodeTime" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackFrameDecodeTimeBoxData.BaseMediaDecodeTime" />
		///   property.
		/// </returns>
		public static Mp4TrackFrameDecodeTimeBoxData WithBaseMediaDecodeTime(
			this Mp4TrackFrameDecodeTimeBoxData sourceBox,
			ulong valueToReplaceWith)
		{
			sourceBox.BaseMediaDecodeTime = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4TrackFrameDecodeTimeBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackFrameDecodeTimeBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4TrackFrameDecodeTimeBoxData WithVersion(
			this Mp4TrackFrameDecodeTimeBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4TrackFrameDecodeTimeBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackFrameDecodeTimeBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4TrackFrameDecodeTimeBoxData WithFlags(
			this Mp4TrackFrameDecodeTimeBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4TrackHeaderBoxData.CreationTime" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackHeaderBoxData.CreationTime" />
		///   property.
		/// </returns>
		public static Mp4TrackHeaderBoxData WithCreationTime(
			this Mp4TrackHeaderBoxData sourceBox,
			ulong valueToReplaceWith)
		{
			sourceBox.CreationTime = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4TrackHeaderBoxData.ModificationTime" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackHeaderBoxData.ModificationTime" />
		///   property.
		/// </returns>
		public static Mp4TrackHeaderBoxData WithModificationTime(
			this Mp4TrackHeaderBoxData sourceBox,
			ulong valueToReplaceWith)
		{
			sourceBox.ModificationTime = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4TrackHeaderBoxData.TrackId" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackHeaderBoxData.TrackId" />
		///   property.
		/// </returns>
		public static Mp4TrackHeaderBoxData WithTrackId(
			this Mp4TrackHeaderBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.TrackId = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4TrackHeaderBoxData.Reserved" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackHeaderBoxData.Reserved" />
		///   property.
		/// </returns>
		public static Mp4TrackHeaderBoxData WithReserved(
			this Mp4TrackHeaderBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Reserved = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4TrackHeaderBoxData.Duration" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackHeaderBoxData.Duration" />
		///   property.
		/// </returns>
		public static Mp4TrackHeaderBoxData WithDuration(
			this Mp4TrackHeaderBoxData sourceBox,
			ulong valueToReplaceWith)
		{
			sourceBox.Duration = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4TrackHeaderBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackHeaderBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4TrackHeaderBoxData WithVersion(
			this Mp4TrackHeaderBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4TrackHeaderBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackHeaderBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4TrackHeaderBoxData WithFlags(
			this Mp4TrackHeaderBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4TrackFragmentBoxData.Children" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackFragmentBoxData.Children" />
		///   property.
		/// </returns>
		public static Mp4TrackFragmentBoxData WithChildren(
			this Mp4TrackFragmentBoxData sourceBox,
			IList<Mp4Box> valueToReplaceWith)
		{
			sourceBox.Children = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4TrackBoxData.Children" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackBoxData.Children" />
		///   property.
		/// </returns>
		public static Mp4TrackBoxData WithChildren(
			this Mp4TrackBoxData sourceBox,
			IList<Mp4Box> valueToReplaceWith)
		{
			sourceBox.Children = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4TrackReferenceContainerBoxData.Boxes" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackReferenceContainerBoxData.Boxes" />
		///   property.
		/// </returns>
		public static Mp4TrackReferenceContainerBoxData WithBoxes(
			this Mp4TrackReferenceContainerBoxData sourceBox,
			IList<Mp4Box> valueToReplaceWith)
		{
			sourceBox.Boxes = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4TrackExtensionPropertiesBoxData.TrackId" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackExtensionPropertiesBoxData.TrackId" />
		///   property.
		/// </returns>
		public static Mp4TrackExtensionPropertiesBoxData WithTrackId(
			this Mp4TrackExtensionPropertiesBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.TrackId = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4TrackExtensionPropertiesBoxData.Boxes" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackExtensionPropertiesBoxData.Boxes" />
		///   property.
		/// </returns>
		public static Mp4TrackExtensionPropertiesBoxData WithBoxes(
			this Mp4TrackExtensionPropertiesBoxData sourceBox,
			IList<Mp4Box> valueToReplaceWith)
		{
			sourceBox.Boxes = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4TrackExtensionPropertiesBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackExtensionPropertiesBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4TrackExtensionPropertiesBoxData WithVersion(
			this Mp4TrackExtensionPropertiesBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4TrackExtensionPropertiesBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackExtensionPropertiesBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4TrackExtensionPropertiesBoxData WithFlags(
			this Mp4TrackExtensionPropertiesBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4TrackExtendsBoxData.TrackId" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackExtendsBoxData.TrackId" />
		///   property.
		/// </returns>
		public static Mp4TrackExtendsBoxData WithTrackId(
			this Mp4TrackExtendsBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.TrackId = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4TrackExtendsBoxData.DefaultSampleDescriptionIndex" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackExtendsBoxData.DefaultSampleDescriptionIndex" />
		///   property.
		/// </returns>
		public static Mp4TrackExtendsBoxData WithDefaultSampleDescriptionIndex(
			this Mp4TrackExtendsBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.DefaultSampleDescriptionIndex = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4TrackExtendsBoxData.DefaultSampleDuration" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackExtendsBoxData.DefaultSampleDuration" />
		///   property.
		/// </returns>
		public static Mp4TrackExtendsBoxData WithDefaultSampleDuration(
			this Mp4TrackExtendsBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.DefaultSampleDuration = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4TrackExtendsBoxData.DefaultSampleSize" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackExtendsBoxData.DefaultSampleSize" />
		///   property.
		/// </returns>
		public static Mp4TrackExtendsBoxData WithDefaultSampleSize(
			this Mp4TrackExtendsBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.DefaultSampleSize = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4TrackExtendsBoxData.DefaultSampleFlags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackExtendsBoxData.DefaultSampleFlags" />
		///   property.
		/// </returns>
		public static Mp4TrackExtendsBoxData WithDefaultSampleFlags(
			this Mp4TrackExtendsBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.DefaultSampleFlags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4TrackExtendsBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackExtendsBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4TrackExtendsBoxData WithVersion(
			this Mp4TrackExtendsBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4TrackExtendsBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TrackExtendsBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4TrackExtendsBoxData WithFlags(
			this Mp4TrackExtendsBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4TypeAndCombinationBoxData.CompatibleBrands" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4TypeAndCombinationBoxData.CompatibleBrands" />
		///   property.
		/// </returns>
		public static Mp4TypeAndCombinationBoxData WithCompatibleBrands(
			this Mp4TypeAndCombinationBoxData sourceBox,
			IList<uint> valueToReplaceWith)
		{
			sourceBox.CompatibleBrands = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4UserDataBoxData.Children" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4UserDataBoxData.Children" />
		///   property.
		/// </returns>
		public static Mp4UserDataBoxData WithChildren(
			this Mp4UserDataBoxData sourceBox,
			IList<Mp4Box> valueToReplaceWith)
		{
			sourceBox.Children = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4VideoMediaHeaderBoxData.GraphicsMode" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4VideoMediaHeaderBoxData.GraphicsMode" />
		///   property.
		/// </returns>
		public static Mp4VideoMediaHeaderBoxData WithGraphicsMode(
			this Mp4VideoMediaHeaderBoxData sourceBox,
			ushort valueToReplaceWith)
		{
			sourceBox.GraphicsMode = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4VideoMediaHeaderBoxData.TopColor" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4VideoMediaHeaderBoxData.TopColor" />
		///   property.
		/// </returns>
		public static Mp4VideoMediaHeaderBoxData WithTopColor(
			this Mp4VideoMediaHeaderBoxData sourceBox,
			IList<ushort> valueToReplaceWith)
		{
			sourceBox.TopColor = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4VideoMediaHeaderBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4VideoMediaHeaderBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4VideoMediaHeaderBoxData WithVersion(
			this Mp4VideoMediaHeaderBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4VideoMediaHeaderBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4VideoMediaHeaderBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4VideoMediaHeaderBoxData WithFlags(
			this Mp4VideoMediaHeaderBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4XmlContainerBoxData.Xml" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4XmlContainerBoxData.Xml" />
		///   property.
		/// </returns>
		public static Mp4XmlContainerBoxData WithXml(
			this Mp4XmlContainerBoxData sourceBox,
			string valueToReplaceWith)
		{
			sourceBox.Xml = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4XmlContainerBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4XmlContainerBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4XmlContainerBoxData WithVersion(
			this Mp4XmlContainerBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4XmlContainerBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4XmlContainerBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4XmlContainerBoxData WithFlags(
			this Mp4XmlContainerBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4UriBoxData.Uri" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4UriBoxData.Uri" />
		///   property.
		/// </returns>
		public static Mp4UriBoxData WithUri(
			this Mp4UriBoxData sourceBox,
			string valueToReplaceWith)
		{
			sourceBox.Uri = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4UriBoxData.Version" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4UriBoxData.Version" />
		///   property.
		/// </returns>
		public static Mp4UriBoxData WithVersion(
			this Mp4UriBoxData sourceBox,
			byte valueToReplaceWith)
		{
			sourceBox.Version = valueToReplaceWith;
			return sourceBox;
		}

		/// <summary>
		///   Changes the <see cref="Mp4UriBoxData.Flags" /> property inside
		///   the given <paramref name="sourceBox" /> parameter.
		/// </summary>
		/// <param name="sourceBox">Input MP4 box</param>
		/// <param name="valueToReplaceWith">The value to replace with</param>
		/// <returns>
		///   <paramref name="sourceBox" /> with the new <see cref="Mp4UriBoxData.Flags" />
		///   property.
		/// </returns>
		public static Mp4UriBoxData WithFlags(
			this Mp4UriBoxData sourceBox,
			uint valueToReplaceWith)
		{
			sourceBox.Flags = valueToReplaceWith;
			return sourceBox;
		}

	}
}

