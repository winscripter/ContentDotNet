namespace ContentDotNet.Extensions.Video.Mp4.Boxes.IO
{
    using ContentDotNet.Binary;
    using ContentDotNet.Extensions.Video.Mp4.Boxes.Data;

    
    /// <summary>
    ///   IO class for Mp4ReceivedSsrcBoxData
    /// </summary>
    public class Mp4ReceivedSsrcBoxIO : Mp4BoxIO
    {
        /// <summary>
        ///   Singleton instance of Mp4ReceivedSsrcBoxIO
        /// </summary>
        public static readonly Mp4ReceivedSsrcBoxIO Instance = new Mp4ReceivedSsrcBoxIO();

        /// <inheritdoc cref="Mp4BoxIO.TypeOfBoxData" />
        public override Type TypeOfBoxData => typeof(Mp4ReceivedSsrcBoxData);

        /// <inheritdoc cref="Mp4BoxIO.ReadBoxData" />
        public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4ReceivedSsrcBoxData(stream.ReadUInt32());
        
        /// <inheritdoc cref="Mp4BoxIO.ReadBoxDataAsync" />
        public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4ReceivedSsrcBoxData(await stream.ReadUInt32Async());
        
        /// <inheritdoc cref="Mp4BoxIO.WriteBoxData" />
        public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4ReceivedSsrcBoxData>(box, out var data);
            stream.Write(data!.Ssrc.GetValueOrDefault());
        }

        /// <inheritdoc cref="Mp4BoxIO.WriteBoxDataAsync" />
        public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4ReceivedSsrcBoxData>(box, out var data);
            await stream.WriteAsync(data!.Ssrc.GetValueOrDefault());
        }
    }

    
    /// <summary>
    ///   IO class for Mp4TimescaleEntryBoxData
    /// </summary>
    public class Mp4TimescaleEntryBoxIO : Mp4BoxIO
    {
        /// <summary>
        ///   Singleton instance of Mp4TimescaleEntryBoxIO
        /// </summary>
        public static readonly Mp4TimescaleEntryBoxIO Instance = new Mp4TimescaleEntryBoxIO();

        /// <inheritdoc cref="Mp4BoxIO.TypeOfBoxData" />
        public override Type TypeOfBoxData => typeof(Mp4TimescaleEntryBoxData);

        /// <inheritdoc cref="Mp4BoxIO.ReadBoxData" />
        public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TimescaleEntryBoxData(stream.ReadUInt32());
        
        /// <inheritdoc cref="Mp4BoxIO.ReadBoxDataAsync" />
        public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TimescaleEntryBoxData(await stream.ReadUInt32Async());
        
        /// <inheritdoc cref="Mp4BoxIO.WriteBoxData" />
        public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4TimescaleEntryBoxData>(box, out var data);
            stream.Write(data!.Timescale.GetValueOrDefault());
        }

        /// <inheritdoc cref="Mp4BoxIO.WriteBoxDataAsync" />
        public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4TimescaleEntryBoxData>(box, out var data);
            await stream.WriteAsync(data!.Timescale.GetValueOrDefault());
        }
    }

    
    /// <summary>
    ///   IO class for Mp4TimeOffsetBoxData
    /// </summary>
    public class Mp4TimeOffsetBoxIO : Mp4BoxIO
    {
        /// <summary>
        ///   Singleton instance of Mp4TimeOffsetBoxIO
        /// </summary>
        public static readonly Mp4TimeOffsetBoxIO Instance = new Mp4TimeOffsetBoxIO();

        /// <inheritdoc cref="Mp4BoxIO.TypeOfBoxData" />
        public override Type TypeOfBoxData => typeof(Mp4TimeOffsetBoxData);

        /// <inheritdoc cref="Mp4BoxIO.ReadBoxData" />
        public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TimeOffsetBoxData(stream.ReadUInt32());
        
        /// <inheritdoc cref="Mp4BoxIO.ReadBoxDataAsync" />
        public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TimeOffsetBoxData(await stream.ReadUInt32Async());
        
        /// <inheritdoc cref="Mp4BoxIO.WriteBoxData" />
        public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4TimeOffsetBoxData>(box, out var data);
            stream.Write(data!.Offset.GetValueOrDefault());
        }

        /// <inheritdoc cref="Mp4BoxIO.WriteBoxDataAsync" />
        public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4TimeOffsetBoxData>(box, out var data);
            await stream.WriteAsync(data!.Offset.GetValueOrDefault());
        }
    }

    
    /// <summary>
    ///   IO class for Mp4SequenceOffsetBoxData
    /// </summary>
    public class Mp4SequenceOffsetBoxIO : Mp4BoxIO
    {
        /// <summary>
        ///   Singleton instance of Mp4SequenceOffsetBoxIO
        /// </summary>
        public static readonly Mp4SequenceOffsetBoxIO Instance = new Mp4SequenceOffsetBoxIO();

        /// <inheritdoc cref="Mp4BoxIO.TypeOfBoxData" />
        public override Type TypeOfBoxData => typeof(Mp4SequenceOffsetBoxData);

        /// <inheritdoc cref="Mp4BoxIO.ReadBoxData" />
        public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4SequenceOffsetBoxData(stream.ReadUInt32());
        
        /// <inheritdoc cref="Mp4BoxIO.ReadBoxDataAsync" />
        public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4SequenceOffsetBoxData(await stream.ReadUInt32Async());
        
        /// <inheritdoc cref="Mp4BoxIO.WriteBoxData" />
        public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4SequenceOffsetBoxData>(box, out var data);
            stream.Write(data!.Offset.GetValueOrDefault());
        }

        /// <inheritdoc cref="Mp4BoxIO.WriteBoxDataAsync" />
        public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4SequenceOffsetBoxData>(box, out var data);
            await stream.WriteAsync(data!.Offset.GetValueOrDefault());
        }
    }

    
    /// <summary>
    ///   IO class for Mp4TotalBytesSentIncluding12ByteRtpHeadersBoxData
    /// </summary>
    public class Mp4TotalBytesSentIncluding12ByteRtpHeadersBoxIO : Mp4BoxIO
    {
        /// <summary>
        ///   Singleton instance of Mp4TotalBytesSentIncluding12ByteRtpHeadersBoxIO
        /// </summary>
        public static readonly Mp4TotalBytesSentIncluding12ByteRtpHeadersBoxIO Instance = new Mp4TotalBytesSentIncluding12ByteRtpHeadersBoxIO();

        /// <inheritdoc cref="Mp4BoxIO.TypeOfBoxData" />
        public override Type TypeOfBoxData => typeof(Mp4TotalBytesSentIncluding12ByteRtpHeadersBoxData);

        /// <inheritdoc cref="Mp4BoxIO.ReadBoxData" />
        public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalBytesSentIncluding12ByteRtpHeadersBoxData(stream.ReadUInt64());
        
        /// <inheritdoc cref="Mp4BoxIO.ReadBoxDataAsync" />
        public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalBytesSentIncluding12ByteRtpHeadersBoxData(await stream.ReadUInt64Async());
        
        /// <inheritdoc cref="Mp4BoxIO.WriteBoxData" />
        public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4TotalBytesSentIncluding12ByteRtpHeadersBoxData>(box, out var data);
            stream.Write(data!.BytesSent.GetValueOrDefault());
        }

        /// <inheritdoc cref="Mp4BoxIO.WriteBoxDataAsync" />
        public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4TotalBytesSentIncluding12ByteRtpHeadersBoxData>(box, out var data);
            await stream.WriteAsync(data!.BytesSent.GetValueOrDefault());
        }
    }

    
    /// <summary>
    ///   IO class for Mp4TotalPacketsSentBoxData
    /// </summary>
    public class Mp4TotalPacketsSentBoxIO : Mp4BoxIO
    {
        /// <summary>
        ///   Singleton instance of Mp4TotalPacketsSentBoxIO
        /// </summary>
        public static readonly Mp4TotalPacketsSentBoxIO Instance = new Mp4TotalPacketsSentBoxIO();

        /// <inheritdoc cref="Mp4BoxIO.TypeOfBoxData" />
        public override Type TypeOfBoxData => typeof(Mp4TotalPacketsSentBoxData);

        /// <inheritdoc cref="Mp4BoxIO.ReadBoxData" />
        public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalPacketsSentBoxData(stream.ReadUInt64());
        
        /// <inheritdoc cref="Mp4BoxIO.ReadBoxDataAsync" />
        public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalPacketsSentBoxData(await stream.ReadUInt64Async());
        
        /// <inheritdoc cref="Mp4BoxIO.WriteBoxData" />
        public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4TotalPacketsSentBoxData>(box, out var data);
            stream.Write(data!.PacketsSent.GetValueOrDefault());
        }

        /// <inheritdoc cref="Mp4BoxIO.WriteBoxDataAsync" />
        public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4TotalPacketsSentBoxData>(box, out var data);
            await stream.WriteAsync(data!.PacketsSent.GetValueOrDefault());
        }
    }

    
    /// <summary>
    ///   IO class for Mp4TotalBytesSentNotIncludingRtpHeadersBoxData
    /// </summary>
    public class Mp4TotalBytesSentNotIncludingRtpHeadersBoxIO : Mp4BoxIO
    {
        /// <summary>
        ///   Singleton instance of Mp4TotalBytesSentNotIncludingRtpHeadersBoxIO
        /// </summary>
        public static readonly Mp4TotalBytesSentNotIncludingRtpHeadersBoxIO Instance = new Mp4TotalBytesSentNotIncludingRtpHeadersBoxIO();

        /// <inheritdoc cref="Mp4BoxIO.TypeOfBoxData" />
        public override Type TypeOfBoxData => typeof(Mp4TotalBytesSentNotIncludingRtpHeadersBoxData);

        /// <inheritdoc cref="Mp4BoxIO.ReadBoxData" />
        public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalBytesSentNotIncludingRtpHeadersBoxData(stream.ReadUInt64());
        
        /// <inheritdoc cref="Mp4BoxIO.ReadBoxDataAsync" />
        public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalBytesSentNotIncludingRtpHeadersBoxData(await stream.ReadUInt64Async());
        
        /// <inheritdoc cref="Mp4BoxIO.WriteBoxData" />
        public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4TotalBytesSentNotIncludingRtpHeadersBoxData>(box, out var data);
            stream.Write(data!.BytesSent.GetValueOrDefault());
        }

        /// <inheritdoc cref="Mp4BoxIO.WriteBoxDataAsync" />
        public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4TotalBytesSentNotIncludingRtpHeadersBoxData>(box, out var data);
            await stream.WriteAsync(data!.BytesSent.GetValueOrDefault());
        }
    }

    
    /// <summary>
    ///   IO class for Mp4TotalBytesSentIncluding12ByteRtpHeaders32BoxData
    /// </summary>
    public class Mp4TotalBytesSentIncluding12ByteRtpHeaders32BoxIO : Mp4BoxIO
    {
        /// <summary>
        ///   Singleton instance of Mp4TotalBytesSentIncluding12ByteRtpHeaders32BoxIO
        /// </summary>
        public static readonly Mp4TotalBytesSentIncluding12ByteRtpHeaders32BoxIO Instance = new Mp4TotalBytesSentIncluding12ByteRtpHeaders32BoxIO();

        /// <inheritdoc cref="Mp4BoxIO.TypeOfBoxData" />
        public override Type TypeOfBoxData => typeof(Mp4TotalBytesSentIncluding12ByteRtpHeaders32BoxData);

        /// <inheritdoc cref="Mp4BoxIO.ReadBoxData" />
        public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalBytesSentIncluding12ByteRtpHeaders32BoxData(stream.ReadUInt32());
        
        /// <inheritdoc cref="Mp4BoxIO.ReadBoxDataAsync" />
        public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalBytesSentIncluding12ByteRtpHeaders32BoxData(await stream.ReadUInt32Async());
        
        /// <inheritdoc cref="Mp4BoxIO.WriteBoxData" />
        public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4TotalBytesSentIncluding12ByteRtpHeaders32BoxData>(box, out var data);
            stream.Write(data!.BytesSent.GetValueOrDefault());
        }

        /// <inheritdoc cref="Mp4BoxIO.WriteBoxDataAsync" />
        public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4TotalBytesSentIncluding12ByteRtpHeaders32BoxData>(box, out var data);
            await stream.WriteAsync(data!.BytesSent.GetValueOrDefault());
        }
    }

    
    /// <summary>
    ///   IO class for Mp4TotalPacketsSent32BoxData
    /// </summary>
    public class Mp4TotalPacketsSent32BoxIO : Mp4BoxIO
    {
        /// <summary>
        ///   Singleton instance of Mp4TotalPacketsSent32BoxIO
        /// </summary>
        public static readonly Mp4TotalPacketsSent32BoxIO Instance = new Mp4TotalPacketsSent32BoxIO();

        /// <inheritdoc cref="Mp4BoxIO.TypeOfBoxData" />
        public override Type TypeOfBoxData => typeof(Mp4TotalPacketsSent32BoxData);

        /// <inheritdoc cref="Mp4BoxIO.ReadBoxData" />
        public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalPacketsSent32BoxData(stream.ReadUInt32());
        
        /// <inheritdoc cref="Mp4BoxIO.ReadBoxDataAsync" />
        public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalPacketsSent32BoxData(await stream.ReadUInt32Async());
        
        /// <inheritdoc cref="Mp4BoxIO.WriteBoxData" />
        public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4TotalPacketsSent32BoxData>(box, out var data);
            stream.Write(data!.PacketsSent.GetValueOrDefault());
        }

        /// <inheritdoc cref="Mp4BoxIO.WriteBoxDataAsync" />
        public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4TotalPacketsSent32BoxData>(box, out var data);
            await stream.WriteAsync(data!.PacketsSent.GetValueOrDefault());
        }
    }

    
    /// <summary>
    ///   IO class for Mp4TotalBytesSentNotIncludingRtpHeaders32BoxData
    /// </summary>
    public class Mp4TotalBytesSentNotIncludingRtpHeaders32BoxIO : Mp4BoxIO
    {
        /// <summary>
        ///   Singleton instance of Mp4TotalBytesSentNotIncludingRtpHeaders32BoxIO
        /// </summary>
        public static readonly Mp4TotalBytesSentNotIncludingRtpHeaders32BoxIO Instance = new Mp4TotalBytesSentNotIncludingRtpHeaders32BoxIO();

        /// <inheritdoc cref="Mp4BoxIO.TypeOfBoxData" />
        public override Type TypeOfBoxData => typeof(Mp4TotalBytesSentNotIncludingRtpHeaders32BoxData);

        /// <inheritdoc cref="Mp4BoxIO.ReadBoxData" />
        public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalBytesSentNotIncludingRtpHeaders32BoxData(stream.ReadUInt32());
        
        /// <inheritdoc cref="Mp4BoxIO.ReadBoxDataAsync" />
        public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalBytesSentNotIncludingRtpHeaders32BoxData(await stream.ReadUInt32Async());
        
        /// <inheritdoc cref="Mp4BoxIO.WriteBoxData" />
        public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4TotalBytesSentNotIncludingRtpHeaders32BoxData>(box, out var data);
            stream.Write(data!.BytesSent.GetValueOrDefault());
        }

        /// <inheritdoc cref="Mp4BoxIO.WriteBoxDataAsync" />
        public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4TotalBytesSentNotIncludingRtpHeaders32BoxData>(box, out var data);
            await stream.WriteAsync(data!.BytesSent.GetValueOrDefault());
        }
    }

    
    /// <summary>
    ///   IO class for Mp4TotalBytesSentFromMediaTracksBoxData
    /// </summary>
    public class Mp4TotalBytesSentFromMediaTracksBoxIO : Mp4BoxIO
    {
        /// <summary>
        ///   Singleton instance of Mp4TotalBytesSentFromMediaTracksBoxIO
        /// </summary>
        public static readonly Mp4TotalBytesSentFromMediaTracksBoxIO Instance = new Mp4TotalBytesSentFromMediaTracksBoxIO();

        /// <inheritdoc cref="Mp4BoxIO.TypeOfBoxData" />
        public override Type TypeOfBoxData => typeof(Mp4TotalBytesSentFromMediaTracksBoxData);

        /// <inheritdoc cref="Mp4BoxIO.ReadBoxData" />
        public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalBytesSentFromMediaTracksBoxData(stream.ReadUInt64());
        
        /// <inheritdoc cref="Mp4BoxIO.ReadBoxDataAsync" />
        public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalBytesSentFromMediaTracksBoxData(await stream.ReadUInt64Async());
        
        /// <inheritdoc cref="Mp4BoxIO.WriteBoxData" />
        public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4TotalBytesSentFromMediaTracksBoxData>(box, out var data);
            stream.Write(data!.BytesSent.GetValueOrDefault());
        }

        /// <inheritdoc cref="Mp4BoxIO.WriteBoxDataAsync" />
        public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4TotalBytesSentFromMediaTracksBoxData>(box, out var data);
            await stream.WriteAsync(data!.BytesSent.GetValueOrDefault());
        }
    }

    
    /// <summary>
    ///   IO class for Mp4TotalBytesSentImmediateModeBoxData
    /// </summary>
    public class Mp4TotalBytesSentImmediateModeBoxIO : Mp4BoxIO
    {
        /// <summary>
        ///   Singleton instance of Mp4TotalBytesSentImmediateModeBoxIO
        /// </summary>
        public static readonly Mp4TotalBytesSentImmediateModeBoxIO Instance = new Mp4TotalBytesSentImmediateModeBoxIO();

        /// <inheritdoc cref="Mp4BoxIO.TypeOfBoxData" />
        public override Type TypeOfBoxData => typeof(Mp4TotalBytesSentImmediateModeBoxData);

        /// <inheritdoc cref="Mp4BoxIO.ReadBoxData" />
        public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalBytesSentImmediateModeBoxData(stream.ReadUInt64());
        
        /// <inheritdoc cref="Mp4BoxIO.ReadBoxDataAsync" />
        public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalBytesSentImmediateModeBoxData(await stream.ReadUInt64Async());
        
        /// <inheritdoc cref="Mp4BoxIO.WriteBoxData" />
        public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4TotalBytesSentImmediateModeBoxData>(box, out var data);
            stream.Write(data!.BytesSent.GetValueOrDefault());
        }

        /// <inheritdoc cref="Mp4BoxIO.WriteBoxDataAsync" />
        public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4TotalBytesSentImmediateModeBoxData>(box, out var data);
            await stream.WriteAsync(data!.BytesSent.GetValueOrDefault());
        }
    }

    
    /// <summary>
    ///   IO class for Mp4TotalBytesInRepeatedPacketsBoxData
    /// </summary>
    public class Mp4TotalBytesInRepeatedPacketsBoxIO : Mp4BoxIO
    {
        /// <summary>
        ///   Singleton instance of Mp4TotalBytesInRepeatedPacketsBoxIO
        /// </summary>
        public static readonly Mp4TotalBytesInRepeatedPacketsBoxIO Instance = new Mp4TotalBytesInRepeatedPacketsBoxIO();

        /// <inheritdoc cref="Mp4BoxIO.TypeOfBoxData" />
        public override Type TypeOfBoxData => typeof(Mp4TotalBytesInRepeatedPacketsBoxData);

        /// <inheritdoc cref="Mp4BoxIO.ReadBoxData" />
        public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalBytesInRepeatedPacketsBoxData(stream.ReadUInt64());
        
        /// <inheritdoc cref="Mp4BoxIO.ReadBoxDataAsync" />
        public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4TotalBytesInRepeatedPacketsBoxData(await stream.ReadUInt64Async());
        
        /// <inheritdoc cref="Mp4BoxIO.WriteBoxData" />
        public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4TotalBytesInRepeatedPacketsBoxData>(box, out var data);
            stream.Write(data!.BytesSent.GetValueOrDefault());
        }

        /// <inheritdoc cref="Mp4BoxIO.WriteBoxDataAsync" />
        public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4TotalBytesInRepeatedPacketsBoxData>(box, out var data);
            await stream.WriteAsync(data!.BytesSent.GetValueOrDefault());
        }
    }

    
    /// <summary>
    ///   IO class for Mp4SmallestRelativeTransmittionTimeMillisecondsBoxData
    /// </summary>
    public class Mp4SmallestRelativeTransmittionTimeMillisecondsBoxIO : Mp4BoxIO
    {
        /// <summary>
        ///   Singleton instance of Mp4SmallestRelativeTransmittionTimeMillisecondsBoxIO
        /// </summary>
        public static readonly Mp4SmallestRelativeTransmittionTimeMillisecondsBoxIO Instance = new Mp4SmallestRelativeTransmittionTimeMillisecondsBoxIO();

        /// <inheritdoc cref="Mp4BoxIO.TypeOfBoxData" />
        public override Type TypeOfBoxData => typeof(Mp4SmallestRelativeTransmittionTimeMillisecondsBoxData);

        /// <inheritdoc cref="Mp4BoxIO.ReadBoxData" />
        public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4SmallestRelativeTransmittionTimeMillisecondsBoxData(stream.ReadUInt32());
        
        /// <inheritdoc cref="Mp4BoxIO.ReadBoxDataAsync" />
        public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4SmallestRelativeTransmittionTimeMillisecondsBoxData(await stream.ReadUInt32Async());
        
        /// <inheritdoc cref="Mp4BoxIO.WriteBoxData" />
        public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4SmallestRelativeTransmittionTimeMillisecondsBoxData>(box, out var data);
            stream.Write(data!.Time.GetValueOrDefault());
        }

        /// <inheritdoc cref="Mp4BoxIO.WriteBoxDataAsync" />
        public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4SmallestRelativeTransmittionTimeMillisecondsBoxData>(box, out var data);
            await stream.WriteAsync(data!.Time.GetValueOrDefault());
        }
    }

    
    /// <summary>
    ///   IO class for Mp4LargestRelativeTransmittionTimeMillisecondsBoxData
    /// </summary>
    public class Mp4LargestRelativeTransmittionTimeMillisecondsBoxIO : Mp4BoxIO
    {
        /// <summary>
        ///   Singleton instance of Mp4LargestRelativeTransmittionTimeMillisecondsBoxIO
        /// </summary>
        public static readonly Mp4LargestRelativeTransmittionTimeMillisecondsBoxIO Instance = new Mp4LargestRelativeTransmittionTimeMillisecondsBoxIO();

        /// <inheritdoc cref="Mp4BoxIO.TypeOfBoxData" />
        public override Type TypeOfBoxData => typeof(Mp4LargestRelativeTransmittionTimeMillisecondsBoxData);

        /// <inheritdoc cref="Mp4BoxIO.ReadBoxData" />
        public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4LargestRelativeTransmittionTimeMillisecondsBoxData(stream.ReadUInt32());
        
        /// <inheritdoc cref="Mp4BoxIO.ReadBoxDataAsync" />
        public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4LargestRelativeTransmittionTimeMillisecondsBoxData(await stream.ReadUInt32Async());
        
        /// <inheritdoc cref="Mp4BoxIO.WriteBoxData" />
        public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4LargestRelativeTransmittionTimeMillisecondsBoxData>(box, out var data);
            stream.Write(data!.Time.GetValueOrDefault());
        }

        /// <inheritdoc cref="Mp4BoxIO.WriteBoxDataAsync" />
        public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4LargestRelativeTransmittionTimeMillisecondsBoxData>(box, out var data);
            await stream.WriteAsync(data!.Time.GetValueOrDefault());
        }
    }

    
    /// <summary>
    ///   IO class for Mp4LargestPacketSentIncludingRtpHeaderBoxData
    /// </summary>
    public class Mp4LargestPacketSentIncludingRtpHeaderBoxIO : Mp4BoxIO
    {
        /// <summary>
        ///   Singleton instance of Mp4LargestPacketSentIncludingRtpHeaderBoxIO
        /// </summary>
        public static readonly Mp4LargestPacketSentIncludingRtpHeaderBoxIO Instance = new Mp4LargestPacketSentIncludingRtpHeaderBoxIO();

        /// <inheritdoc cref="Mp4BoxIO.TypeOfBoxData" />
        public override Type TypeOfBoxData => typeof(Mp4LargestPacketSentIncludingRtpHeaderBoxData);

        /// <inheritdoc cref="Mp4BoxIO.ReadBoxData" />
        public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4LargestPacketSentIncludingRtpHeaderBoxData(stream.ReadUInt32());
        
        /// <inheritdoc cref="Mp4BoxIO.ReadBoxDataAsync" />
        public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4LargestPacketSentIncludingRtpHeaderBoxData(await stream.ReadUInt32Async());
        
        /// <inheritdoc cref="Mp4BoxIO.WriteBoxData" />
        public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4LargestPacketSentIncludingRtpHeaderBoxData>(box, out var data);
            stream.Write(data!.Bytes.GetValueOrDefault());
        }

        /// <inheritdoc cref="Mp4BoxIO.WriteBoxDataAsync" />
        public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4LargestPacketSentIncludingRtpHeaderBoxData>(box, out var data);
            await stream.WriteAsync(data!.Bytes.GetValueOrDefault());
        }
    }

    
    /// <summary>
    ///   IO class for Mp4LongestPacketDurationInMillisecondsBoxData
    /// </summary>
    public class Mp4LongestPacketDurationInMillisecondsBoxIO : Mp4BoxIO
    {
        /// <summary>
        ///   Singleton instance of Mp4LongestPacketDurationInMillisecondsBoxIO
        /// </summary>
        public static readonly Mp4LongestPacketDurationInMillisecondsBoxIO Instance = new Mp4LongestPacketDurationInMillisecondsBoxIO();

        /// <inheritdoc cref="Mp4BoxIO.TypeOfBoxData" />
        public override Type TypeOfBoxData => typeof(Mp4LongestPacketDurationInMillisecondsBoxData);

        /// <inheritdoc cref="Mp4BoxIO.ReadBoxData" />
        public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4LongestPacketDurationInMillisecondsBoxData(stream.ReadUInt32());
        
        /// <inheritdoc cref="Mp4BoxIO.ReadBoxDataAsync" />
        public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4LongestPacketDurationInMillisecondsBoxData(await stream.ReadUInt32Async());
        
        /// <inheritdoc cref="Mp4BoxIO.WriteBoxData" />
        public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4LongestPacketDurationInMillisecondsBoxData>(box, out var data);
            stream.Write(data!.Time.GetValueOrDefault());
        }

        /// <inheritdoc cref="Mp4BoxIO.WriteBoxDataAsync" />
        public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4LongestPacketDurationInMillisecondsBoxData>(box, out var data);
            await stream.WriteAsync(data!.Time.GetValueOrDefault());
        }
    }

    
    /// <summary>
    ///   IO class for Mp4SamplingRateBoxData
    /// </summary>
    public class Mp4SamplingRateBoxIO : Mp4BoxIO
    {
        /// <summary>
        ///   Singleton instance of Mp4SamplingRateBoxIO
        /// </summary>
        public static readonly Mp4SamplingRateBoxIO Instance = new Mp4SamplingRateBoxIO();

        /// <inheritdoc cref="Mp4BoxIO.TypeOfBoxData" />
        public override Type TypeOfBoxData => typeof(Mp4SamplingRateBoxData);

        /// <inheritdoc cref="Mp4BoxIO.ReadBoxData" />
        public override void ReadBoxData(Mp4Box box, BinaryReader stream) => box.Data = new Mp4SamplingRateBoxData(stream.ReadUInt32());
        
        /// <inheritdoc cref="Mp4BoxIO.ReadBoxDataAsync" />
        public override async Task ReadBoxDataAsync(Mp4Box box, BinaryReader stream) => box.Data = new Mp4SamplingRateBoxData(await stream.ReadUInt32Async());
        
        /// <inheritdoc cref="Mp4BoxIO.WriteBoxData" />
        public override void WriteBoxData(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4SamplingRateBoxData>(box, out var data);
            stream.Write(data!.SamplingRate.GetValueOrDefault());
        }

        /// <inheritdoc cref="Mp4BoxIO.WriteBoxDataAsync" />
        public override async Task WriteBoxDataAsync(Mp4Box box, BinaryWriter stream)
        {
            Check.DataIs<Mp4SamplingRateBoxData>(box, out var data);
            await stream.WriteAsync(data!.SamplingRate.GetValueOrDefault());
        }
    }

}

