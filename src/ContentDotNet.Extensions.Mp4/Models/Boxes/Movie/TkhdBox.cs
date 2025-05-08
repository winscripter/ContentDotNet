namespace ContentDotNet.Extensions.Mp4.Models.Boxes.Movie;

/// <summary>
///   Represents an MP4 TKHD (Track Header) box.
/// </summary>
public struct TkhdBox : IBoxData, IEquatable<TkhdBox>
{
    /// <summary>
    ///   This represents the version of the Track Header. If it's 0,
    ///   certain fields (Creation Time, Modification Time, and Duration)
    ///   are 4 bytes in size. Otherwise, they're 8 bytes.
    /// </summary>
    public byte Version { get; set; }

    /// <summary>
    ///   These three bytes represent track properties, such as enabled, in movie,
    ///   or in preview.
    /// </summary>
    public uint Flags { get; set; }

    /// <summary>
    ///   This represents the time when this track was created. If <see cref="Version"/>
    ///   is 0, this is 4 bytes. Otherwise, if <see cref="Version"/> is 1, this is 8 bytes.
    /// </summary>
    public ulong CreationTime { get; set; }

    /// <summary>
    ///   This represents the time when this track was modified. If <see cref="Version"/>
    ///   is 0, this is 4 bytes. Otherwise, if <see cref="Version"/> is 1, this is 8 bytes.
    /// </summary>
    public ulong ModificationTime { get; set; }

    /// <summary>
    ///   This is the unique identifier for the track. It's always 4 bytes.
    /// </summary>
    public uint TrackId { get; set; }

    /// <summary>
    ///   4 bytes that must always be zero.
    /// </summary>
    public uint Reserved { get; set; }

    /// <summary>
    ///   This represents how long the track lasts. If <see cref="Version"/>
    ///   is 0, this is 4 bytes. Otherwise, if <see cref="Version"/> is 1, this is 8 bytes.
    /// </summary>
    public ulong Duration { get; set; }

    /// <summary>
    ///   Another 8 bytes that must always be zero.
    /// </summary>
    public ulong Reserved2 { get; set; }

    /// <summary>
    ///   Defines the track's layering order.
    /// </summary>
    public ushort Layer { get; set; }

    /// <summary>
    ///   Two bytes that represent the track's volume (1.0 for full volume).
    /// </summary>
    public FixedPointHalf Volume { get; set; }

    /// <summary>
    ///   Matrix of 36 elements with each element being a byte in size. It is for
    ///   scaling, rotating, or translating video.
    /// </summary>
    public Matrix36 Matrix { get; set; }

    /// <summary>
    ///   Four bytes that represent the track's width, in pixels.
    /// </summary>
    public uint Width { get; set; }

    /// <summary>
    ///   Four bytes that represent the track's height, in pixels.
    /// </summary>
    public uint Height { get; set; }

    /// <summary>
    ///   Initializes a new instance of the <see cref="TkhdBox"/> struct.
    /// </summary>
    /// <param name="version">
    ///   The version of the Track Header. If it's 0, certain fields (Creation Time, Modification Time, and Duration)
    ///   are 4 bytes in size. Otherwise, they're 8 bytes.
    /// </param>
    /// <param name="flags">
    ///   The flags representing track properties, such as enabled, in movie, or in preview.
    /// </param>
    /// <param name="creationTime">
    ///   The time when this track was created. If <paramref name="version"/> is 0, this is 4 bytes.
    ///   Otherwise, if <paramref name="version"/> is 1, this is 8 bytes.
    /// </param>
    /// <param name="modificationTime">
    ///   The time when this track was modified. If <paramref name="version"/> is 0, this is 4 bytes.
    ///   Otherwise, if <paramref name="version"/> is 1, this is 8 bytes.
    /// </param>
    /// <param name="trackId">
    ///   The unique identifier for the track. It's always 4 bytes.
    /// </param>
    /// <param name="reserved">
    ///   Reserved 4 bytes that must always be zero.
    /// </param>
    /// <param name="duration">
    ///   The duration of the track. If <paramref name="version"/> is 0, this is 4 bytes.
    ///   Otherwise, if <paramref name="version"/> is 1, this is 8 bytes.
    /// </param>
    /// <param name="reserved2">
    ///   Reserved 8 bytes that must always be zero.
    /// </param>
    /// <param name="layer">
    ///   The layering order of the track.
    /// </param>
    /// <param name="volume">
    ///   The volume of the track, represented as a fixed-point number (1.0 for full volume).
    /// </param>
    /// <param name="matrix">
    ///   A 36-element matrix for scaling, rotating, or translating video.
    /// </param>
    /// <param name="width">
    ///   The width of the track, in pixels.
    /// </param>
    /// <param name="height">
    ///   The height of the track, in pixels.
    /// </param>
    public TkhdBox(byte version, uint flags, ulong creationTime, ulong modificationTime, uint trackId, uint reserved, ulong duration, ulong reserved2, ushort layer, FixedPointHalf volume, Matrix36 matrix, uint width, uint height)
    {
        Version = version;
        Flags = flags;
        CreationTime = creationTime;
        ModificationTime = modificationTime;
        TrackId = trackId;
        Reserved = reserved;
        Duration = duration;
        Reserved2 = reserved2;
        Layer = layer;
        Volume = volume;
        Matrix = matrix;
        Width = width;
        Height = height;
    }

    /// <summary>
    ///   Reads a <see cref="TkhdBox"/> instance from the specified <see cref="BinaryReader"/>.
    /// </summary>
    /// <param name="br">The <see cref="BinaryReader"/> to read from.</param>
    /// <returns>A new instance of <see cref="TkhdBox"/> read from the binary stream.</returns>
    public static TkhdBox Read(BinaryReader br)
    {
        byte version = br.ReadByte();
        uint flags = (uint)((br.ReadByte() << 16) | (br.ReadByte() << 8) | br.ReadByte());

        ulong creationTime = version == 1 ? br.ReadUInt64() : br.ReadUInt32();
        ulong modificationTime = version == 1 ? br.ReadUInt64() : br.ReadUInt32();

        uint trackId = br.ReadUInt32();
        uint reserved = br.ReadUInt32();

        ulong duration = version == 1 ? br.ReadUInt64() : br.ReadUInt32();
        ulong reserved2 = br.ReadUInt64();

        ushort layer = br.ReadUInt16();

        FixedPointHalf volume = FixedPointHalf.Read(br);

        var matrix36 = new Matrix36();
        for (int i = 0; i < 36; i++)
            matrix36[i] = br.ReadByte();

        uint width = br.ReadUInt32();
        uint height = br.ReadUInt32();

        return new TkhdBox(version, flags, creationTime, modificationTime, trackId, reserved, duration, reserved2, layer, volume, matrix36, width, height);
    }

    /// <summary>
    ///   Writes the current <see cref="TkhdBox"/> instance to the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="bw">The <see cref="BinaryWriter"/> to write to.</param>
    public readonly void Write(BinaryWriter bw)
    {
        bw.Write(Version);
        bw.Write((byte)((Flags & 0xFF0000) >> 16));
        bw.Write((byte)((Flags & 0x00FF00) >> 8));
        bw.Write((byte)(Flags & 0x0000FF));

        if (Version == 1)
        {
            bw.Write(CreationTime);
            bw.Write(ModificationTime);
        }
        else if (Version == 0)
        {
            bw.Write((uint)CreationTime);
            bw.Write((uint)ModificationTime);
        }

        bw.Write(TrackId);
        bw.Write(Reserved);

        if (Version == 1) bw.Write(Duration);
        else if (Version == 0) bw.Write((uint)Duration);

        bw.Write(Reserved2);
        bw.Write(Layer);

        this.Volume.Write(bw);

        for (int i = 0; i < 36; i++)
            bw.Write(i);

        bw.Write(Width);
        bw.Write(Height);
    }

    /// <summary>
    ///   Determines whether the specified object is equal to the current instance.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns>
    ///   <c>true</c> if the specified object is equal to the current instance; otherwise, <c>false</c>.
    /// </returns>
    public readonly override bool Equals(object? obj)
    {
        return obj is TkhdBox box && Equals(box);
    }

    /// <summary>
    ///   Determines whether the specified <see cref="TkhdBox"/> is equal to the current instance.
    /// </summary>
    /// <param name="other">The <see cref="TkhdBox"/> to compare with the current instance.</param>
    /// <returns>
    ///   <c>true</c> if the specified <see cref="TkhdBox"/> is equal to the current instance; otherwise, <c>false</c>.
    /// </returns>
    public readonly bool Equals(TkhdBox other)
    {
        return Version == other.Version &&
               Flags == other.Flags &&
               CreationTime == other.CreationTime &&
               ModificationTime == other.ModificationTime &&
               TrackId == other.TrackId &&
               Reserved == other.Reserved &&
               Duration == other.Duration &&
               Reserved2 == other.Reserved2 &&
               Layer == other.Layer &&
               EqualityComparer<FixedPointHalf>.Default.Equals(Volume, other.Volume) &&
               Matrix.Equals(other.Matrix) &&
               Width == other.Width &&
               Height == other.Height;
    }

    /// <summary>
    ///   Returns a hash code for the current instance.
    /// </summary>
    /// <returns>A hash code for the current instance.</returns>
    public readonly override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(Version);
        hash.Add(Flags);
        hash.Add(CreationTime);
        hash.Add(ModificationTime);
        hash.Add(TrackId);
        hash.Add(Reserved);
        hash.Add(Duration);
        hash.Add(Reserved2);
        hash.Add(Layer);
        hash.Add(Volume);
        hash.Add(Matrix);
        hash.Add(Width);
        hash.Add(Height);
        return hash.ToHashCode();
    }


    /// <summary>  
    ///   Determines whether two <see cref="TkhdBox"/> instances are equal.  
    /// </summary>  
    /// <param name="left">The first <see cref="TkhdBox"/> to compare.</param>  
    /// <param name="right">The second <see cref="TkhdBox"/> to compare.</param>  
    /// <returns><c>true</c> if the two instances are equal; otherwise, <c>false</c>.</returns>  
    public static bool operator ==(TkhdBox left, TkhdBox right)
    {
        return left.Equals(right);
    }

    /// <summary>  
    ///   Determines whether two <see cref="TkhdBox"/> instances are not equal.  
    /// </summary>  
    /// <param name="left">The first <see cref="TkhdBox"/> to compare.</param>  
    /// <param name="right">The second <see cref="TkhdBox"/> to compare.</param>  
    /// <returns><c>true</c> if the two instances are not equal; otherwise, <c>false</c>.</returns>  
    public static bool operator !=(TkhdBox left, TkhdBox right)
    {
        return !(left == right);
    }
}
