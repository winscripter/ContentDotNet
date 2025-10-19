namespace ContentDotNet.Extensions.Video.Mp4.Boxes.IO.Internal
{
	using ContentDotNet.Binary;
	using ContentDotNet.Extensions.Video.Mp4.Boxes.Data;


	internal class InternalReceivedSsrcBoxIO : Mp4BoxIO
	{
		public static readonly InternalReceivedSsrcBoxIO Instance = new InternalReceivedSsrcBoxIO();

		public override Type TypeOfBoxData => typeof(Mp4ReceivedSsrcBoxData);
		public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4ReceivedSsrcBoxData(stream.ReadUInt32());
		public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4ReceivedSsrcBoxData(await stream.ReadUInt32Async());
		public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4ReceivedSsrcBoxData>(box, out var data);
			stream.Write(data!.Ssrc.GetValueOrDefault());
		}
		public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4ReceivedSsrcBoxData>(box, out var data);
			await stream.WriteAsync(data!.Ssrc.GetValueOrDefault());
		}
	}


	internal class InternalTimescaleEntryBoxIO : Mp4BoxIO
	{
		public static readonly InternalTimescaleEntryBoxIO Instance = new InternalTimescaleEntryBoxIO();

		public override Type TypeOfBoxData => typeof(Mp4TimescaleEntryBoxData);
		public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TimescaleEntryBoxData(stream.ReadUInt32());
		public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TimescaleEntryBoxData(await stream.ReadUInt32Async());
		public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4TimescaleEntryBoxData>(box, out var data);
			stream.Write(data!.Timescale.GetValueOrDefault());
		}
		public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4TimescaleEntryBoxData>(box, out var data);
			await stream.WriteAsync(data!.Timescale.GetValueOrDefault());
		}
	}


	internal class InternalTimeOffsetBoxIO : Mp4BoxIO
	{
		public static readonly InternalTimeOffsetBoxIO Instance = new InternalTimeOffsetBoxIO();

		public override Type TypeOfBoxData => typeof(Mp4TimeOffsetBoxData);
		public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TimeOffsetBoxData(stream.ReadUInt32());
		public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TimeOffsetBoxData(await stream.ReadUInt32Async());
		public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4TimeOffsetBoxData>(box, out var data);
			stream.Write(data!.Offset.GetValueOrDefault());
		}
		public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4TimeOffsetBoxData>(box, out var data);
			await stream.WriteAsync(data!.Offset.GetValueOrDefault());
		}
	}


	internal class InternalSequenceOffsetBoxIO : Mp4BoxIO
	{
		public static readonly InternalSequenceOffsetBoxIO Instance = new InternalSequenceOffsetBoxIO();

		public override Type TypeOfBoxData => typeof(Mp4SequenceOffsetBoxData);
		public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4SequenceOffsetBoxData(stream.ReadUInt32());
		public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4SequenceOffsetBoxData(await stream.ReadUInt32Async());
		public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4SequenceOffsetBoxData>(box, out var data);
			stream.Write(data!.Offset.GetValueOrDefault());
		}
		public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4SequenceOffsetBoxData>(box, out var data);
			await stream.WriteAsync(data!.Offset.GetValueOrDefault());
		}
	}


	internal class InternalTotalBytesSentIncluding12ByteRtpHeadersBoxIO : Mp4BoxIO
	{
		public static readonly InternalTotalBytesSentIncluding12ByteRtpHeadersBoxIO Instance = new InternalTotalBytesSentIncluding12ByteRtpHeadersBoxIO();

		public override Type TypeOfBoxData => typeof(Mp4TotalBytesSentIncluding12ByteRtpHeadersBoxData);
		public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalBytesSentIncluding12ByteRtpHeadersBoxData(stream.ReadUInt64());
		public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalBytesSentIncluding12ByteRtpHeadersBoxData(await stream.ReadUInt64Async());
		public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4TotalBytesSentIncluding12ByteRtpHeadersBoxData>(box, out var data);
			stream.Write(data!.BytesSent.GetValueOrDefault());
		}
		public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4TotalBytesSentIncluding12ByteRtpHeadersBoxData>(box, out var data);
			await stream.WriteAsync(data!.BytesSent.GetValueOrDefault());
		}
	}


	internal class InternalTotalPacketsSentBoxIO : Mp4BoxIO
	{
		public static readonly InternalTotalPacketsSentBoxIO Instance = new InternalTotalPacketsSentBoxIO();

		public override Type TypeOfBoxData => typeof(Mp4TotalPacketsSentBoxData);
		public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalPacketsSentBoxData(stream.ReadUInt64());
		public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalPacketsSentBoxData(await stream.ReadUInt64Async());
		public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4TotalPacketsSentBoxData>(box, out var data);
			stream.Write(data!.PacketsSent.GetValueOrDefault());
		}
		public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4TotalPacketsSentBoxData>(box, out var data);
			await stream.WriteAsync(data!.PacketsSent.GetValueOrDefault());
		}
	}


	internal class InternalTotalBytesSentNotIncludingRtpHeadersBoxIO : Mp4BoxIO
	{
		public static readonly InternalTotalBytesSentNotIncludingRtpHeadersBoxIO Instance = new InternalTotalBytesSentNotIncludingRtpHeadersBoxIO();

		public override Type TypeOfBoxData => typeof(Mp4TotalBytesSentNotIncludingRtpHeadersBoxData);
		public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalBytesSentNotIncludingRtpHeadersBoxData(stream.ReadUInt64());
		public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalBytesSentNotIncludingRtpHeadersBoxData(await stream.ReadUInt64Async());
		public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4TotalBytesSentNotIncludingRtpHeadersBoxData>(box, out var data);
			stream.Write(data!.BytesSent.GetValueOrDefault());
		}
		public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4TotalBytesSentNotIncludingRtpHeadersBoxData>(box, out var data);
			await stream.WriteAsync(data!.BytesSent.GetValueOrDefault());
		}
	}


	internal class InternalTotalBytesSentIncluding12ByteRtpHeaders32BoxIO : Mp4BoxIO
	{
		public static readonly InternalTotalBytesSentIncluding12ByteRtpHeaders32BoxIO Instance = new InternalTotalBytesSentIncluding12ByteRtpHeaders32BoxIO();

		public override Type TypeOfBoxData => typeof(Mp4TotalBytesSentIncluding12ByteRtpHeaders32BoxData);
		public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalBytesSentIncluding12ByteRtpHeaders32BoxData(stream.ReadUInt32());
		public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalBytesSentIncluding12ByteRtpHeaders32BoxData(await stream.ReadUInt32Async());
		public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4TotalBytesSentIncluding12ByteRtpHeaders32BoxData>(box, out var data);
			stream.Write(data!.BytesSent.GetValueOrDefault());
		}
		public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4TotalBytesSentIncluding12ByteRtpHeaders32BoxData>(box, out var data);
			await stream.WriteAsync(data!.BytesSent.GetValueOrDefault());
		}
	}


	internal class InternalTotalPacketsSent32BoxIO : Mp4BoxIO
	{
		public static readonly InternalTotalPacketsSent32BoxIO Instance = new InternalTotalPacketsSent32BoxIO();

		public override Type TypeOfBoxData => typeof(Mp4TotalPacketsSent32BoxData);
		public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalPacketsSent32BoxData(stream.ReadUInt32());
		public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalPacketsSent32BoxData(await stream.ReadUInt32Async());
		public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4TotalPacketsSent32BoxData>(box, out var data);
			stream.Write(data!.PacketsSent.GetValueOrDefault());
		}
		public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4TotalPacketsSent32BoxData>(box, out var data);
			await stream.WriteAsync(data!.PacketsSent.GetValueOrDefault());
		}
	}


	internal class InternalTotalBytesSentNotIncludingRtpHeaders32BoxIO : Mp4BoxIO
	{
		public static readonly InternalTotalBytesSentNotIncludingRtpHeaders32BoxIO Instance = new InternalTotalBytesSentNotIncludingRtpHeaders32BoxIO();

		public override Type TypeOfBoxData => typeof(Mp4TotalBytesSentNotIncludingRtpHeaders32BoxData);
		public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalBytesSentNotIncludingRtpHeaders32BoxData(stream.ReadUInt32());
		public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalBytesSentNotIncludingRtpHeaders32BoxData(await stream.ReadUInt32Async());
		public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4TotalBytesSentNotIncludingRtpHeaders32BoxData>(box, out var data);
			stream.Write(data!.BytesSent.GetValueOrDefault());
		}
		public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4TotalBytesSentNotIncludingRtpHeaders32BoxData>(box, out var data);
			await stream.WriteAsync(data!.BytesSent.GetValueOrDefault());
		}
	}


	internal class InternalTotalBytesSentFromMediaTracksBoxIO : Mp4BoxIO
	{
		public static readonly InternalTotalBytesSentFromMediaTracksBoxIO Instance = new InternalTotalBytesSentFromMediaTracksBoxIO();

		public override Type TypeOfBoxData => typeof(Mp4TotalBytesSentFromMediaTracksBoxData);
		public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalBytesSentFromMediaTracksBoxData(stream.ReadUInt64());
		public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalBytesSentFromMediaTracksBoxData(await stream.ReadUInt64Async());
		public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4TotalBytesSentFromMediaTracksBoxData>(box, out var data);
			stream.Write(data!.BytesSent.GetValueOrDefault());
		}
		public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4TotalBytesSentFromMediaTracksBoxData>(box, out var data);
			await stream.WriteAsync(data!.BytesSent.GetValueOrDefault());
		}
	}


	internal class InternalTotalBytesSentImmediateModeBoxIO : Mp4BoxIO
	{
		public static readonly InternalTotalBytesSentImmediateModeBoxIO Instance = new InternalTotalBytesSentImmediateModeBoxIO();

		public override Type TypeOfBoxData => typeof(Mp4TotalBytesSentImmediateModeBoxData);
		public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalBytesSentImmediateModeBoxData(stream.ReadUInt64());
		public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalBytesSentImmediateModeBoxData(await stream.ReadUInt64Async());
		public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4TotalBytesSentImmediateModeBoxData>(box, out var data);
			stream.Write(data!.BytesSent.GetValueOrDefault());
		}
		public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4TotalBytesSentImmediateModeBoxData>(box, out var data);
			await stream.WriteAsync(data!.BytesSent.GetValueOrDefault());
		}
	}


	internal class InternalTotalBytesInRepeatedPacketsBoxIO : Mp4BoxIO
	{
		public static readonly InternalTotalBytesInRepeatedPacketsBoxIO Instance = new InternalTotalBytesInRepeatedPacketsBoxIO();

		public override Type TypeOfBoxData => typeof(Mp4TotalBytesInRepeatedPacketsBoxData);
		public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalBytesInRepeatedPacketsBoxData(stream.ReadUInt64());
		public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalBytesInRepeatedPacketsBoxData(await stream.ReadUInt64Async());
		public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4TotalBytesInRepeatedPacketsBoxData>(box, out var data);
			stream.Write(data!.BytesSent.GetValueOrDefault());
		}
		public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4TotalBytesInRepeatedPacketsBoxData>(box, out var data);
			await stream.WriteAsync(data!.BytesSent.GetValueOrDefault());
		}
	}


	internal class InternalSmallestRelativeTransmittionTimeMillisecondsBoxIO : Mp4BoxIO
	{
		public static readonly InternalSmallestRelativeTransmittionTimeMillisecondsBoxIO Instance = new InternalSmallestRelativeTransmittionTimeMillisecondsBoxIO();

		public override Type TypeOfBoxData => typeof(Mp4SmallestRelativeTransmittionTimeMillisecondsBoxData);
		public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4SmallestRelativeTransmittionTimeMillisecondsBoxData(stream.ReadUInt32());
		public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4SmallestRelativeTransmittionTimeMillisecondsBoxData(await stream.ReadUInt32Async());
		public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4SmallestRelativeTransmittionTimeMillisecondsBoxData>(box, out var data);
			stream.Write(data!.Time.GetValueOrDefault());
		}
		public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4SmallestRelativeTransmittionTimeMillisecondsBoxData>(box, out var data);
			await stream.WriteAsync(data!.Time.GetValueOrDefault());
		}
	}


	internal class InternalLargestRelativeTransmittionTimeMillisecondsBoxIO : Mp4BoxIO
	{
		public static readonly InternalLargestRelativeTransmittionTimeMillisecondsBoxIO Instance = new InternalLargestRelativeTransmittionTimeMillisecondsBoxIO();

		public override Type TypeOfBoxData => typeof(Mp4LargestRelativeTransmittionTimeMillisecondsBoxData);
		public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4LargestRelativeTransmittionTimeMillisecondsBoxData(stream.ReadUInt32());
		public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4LargestRelativeTransmittionTimeMillisecondsBoxData(await stream.ReadUInt32Async());
		public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4LargestRelativeTransmittionTimeMillisecondsBoxData>(box, out var data);
			stream.Write(data!.Time.GetValueOrDefault());
		}
		public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4LargestRelativeTransmittionTimeMillisecondsBoxData>(box, out var data);
			await stream.WriteAsync(data!.Time.GetValueOrDefault());
		}
	}


	internal class InternalLargestPacketSentIncludingRtpHeaderBoxIO : Mp4BoxIO
	{
		public static readonly InternalLargestPacketSentIncludingRtpHeaderBoxIO Instance = new InternalLargestPacketSentIncludingRtpHeaderBoxIO();

		public override Type TypeOfBoxData => typeof(Mp4LargestPacketSentIncludingRtpHeaderBoxData);
		public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4LargestPacketSentIncludingRtpHeaderBoxData(stream.ReadUInt32());
		public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4LargestPacketSentIncludingRtpHeaderBoxData(await stream.ReadUInt32Async());
		public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4LargestPacketSentIncludingRtpHeaderBoxData>(box, out var data);
			stream.Write(data!.Bytes.GetValueOrDefault());
		}
		public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4LargestPacketSentIncludingRtpHeaderBoxData>(box, out var data);
			await stream.WriteAsync(data!.Bytes.GetValueOrDefault());
		}
	}


	internal class InternalLongestPacketDurationInMillisecondsBoxIO : Mp4BoxIO
	{
		public static readonly InternalLongestPacketDurationInMillisecondsBoxIO Instance = new InternalLongestPacketDurationInMillisecondsBoxIO();

		public override Type TypeOfBoxData => typeof(Mp4LongestPacketDurationInMillisecondsBoxData);
		public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4LongestPacketDurationInMillisecondsBoxData(stream.ReadUInt32());
		public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4LongestPacketDurationInMillisecondsBoxData(await stream.ReadUInt32Async());
		public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4LongestPacketDurationInMillisecondsBoxData>(box, out var data);
			stream.Write(data!.Time.GetValueOrDefault());
		}
		public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4LongestPacketDurationInMillisecondsBoxData>(box, out var data);
			await stream.WriteAsync(data!.Time.GetValueOrDefault());
		}
	}


	internal class InternalSamplingRateBoxIO : Mp4BoxIO
	{
		public static readonly InternalSamplingRateBoxIO Instance = new InternalSamplingRateBoxIO();

		public override Type TypeOfBoxData => typeof(Mp4SamplingRateBoxData);
		public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4SamplingRateBoxData(stream.ReadUInt32());
		public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4SamplingRateBoxData(await stream.ReadUInt32Async());
		public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4SamplingRateBoxData>(box, out var data);
			stream.Write(data!.SamplingRate.GetValueOrDefault());
		}
		public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
		{
			Check.DataIs<Mp4SamplingRateBoxData>(box, out var data);
			await stream.WriteAsync(data!.SamplingRate.GetValueOrDefault());
		}
	}

}

