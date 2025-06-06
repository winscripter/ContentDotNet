namespace ContentDotNet.Extensions.Mp4.Models.Boxes;

[BoxInfo("tkhd", "Track Header", "Defines information about the track")]
public sealed class TrackHeaderBox : FullBox, IEquatable<TrackHeaderBox?>
{
    public ulong CreationTime { get; set; }
    public ulong ModificationTime { get; set; }
    public uint TrackID { get; set; }
    public ulong Duration { get; set; }
    public ushort Layer { get; set; }
    public ushort AlternateGroup { get; set; }
    public ushort Volume { get; set; }
    public int[] Matrix { get; set; } = new int[9];
    public uint Width { get; set; }
    public uint Height { get; set; }

    public override bool CanWriteWithoutParameters => true;

    public override bool RequiresChildBoxes => false;

    private TrackBox? _trak;

    public TrackBox? GetTrak() => _trak;
    public void UseTrak(TrackBox? trak) => _trak = trak;

    public override void Write(BinaryWriter writer)
    {
        WriteFullBoxHeader(writer);
        if (Version == 0)
        {
            writer.Write((uint)CreationTime);
            writer.Write((uint)ModificationTime);
            writer.Write(TrackID);
            writer.Write(0u);
            writer.Write((uint)Duration);
        }
        else
        {
            writer.Write(CreationTime);
            writer.Write(ModificationTime);
            writer.Write(TrackID);
            writer.Write(0u);
            writer.Write(Duration);
        }
        writer.Write(0uL);
        writer.Write(Layer);
        writer.Write(AlternateGroup);
        writer.Write(Volume);
        writer.Write((ushort)0);
        foreach (int value in Matrix)
            writer.Write(value);
        writer.Write(Width);
        writer.Write(Height);
    }

    public static TrackHeaderBox Read(BinaryReader reader)
    {
        var (size, type, version, flags) = ParseFullBoxHeader(reader);

        var tkhd = new TrackHeaderBox(size, type)
        {
            Version = version,
            Flags = flags
        };

        if (version == 0)
        {
            tkhd.CreationTime = reader.ReadUInt32();
            tkhd.ModificationTime = reader.ReadUInt32();
            tkhd.TrackID = reader.ReadUInt32();
            _ = reader.ReadUInt32();
            tkhd.Duration = reader.ReadUInt32();
        }
        else
        {
            tkhd.CreationTime = reader.ReadUInt64();
            tkhd.ModificationTime = reader.ReadUInt64();
            tkhd.TrackID = reader.ReadUInt32();
            _ = reader.ReadUInt32();
            tkhd.Duration = reader.ReadUInt64();
        }

        _ = reader.ReadUInt32();
        tkhd.Layer = reader.ReadUInt16();
        tkhd.AlternateGroup = reader.ReadUInt16();
        tkhd.Volume = reader.ReadUInt16();
        _ = reader.ReadUInt16();
        for (int i = 0; i < 9; i++)
            tkhd.Matrix[i] = reader.ReadInt32();
        tkhd.Width = reader.ReadUInt32();
        tkhd.Height = reader.ReadUInt32();

        return tkhd;
    }

    public override bool IsCompatibleWith(Box other)
    {
        return Type == other.Type;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as TrackHeaderBox);
    }

    public bool Equals(TrackHeaderBox? other)
    {
        return other is not null &&
               Size == other.Size &&
               Type == other.Type &&
               CanWriteWithoutParameters == other.CanWriteWithoutParameters &&
               EqualityComparer<Box[]?>.Default.Equals(Children, other.Children) &&
               RequiresChildBoxes == other.RequiresChildBoxes &&
               Version == other.Version &&
               Flags == other.Flags &&
               CreationTime == other.CreationTime &&
               ModificationTime == other.ModificationTime &&
               TrackID == other.TrackID &&
               Duration == other.Duration &&
               Layer == other.Layer &&
               AlternateGroup == other.AlternateGroup &&
               Volume == other.Volume &&
               EqualityComparer<int[]>.Default.Equals(Matrix, other.Matrix) &&
               Width == other.Width &&
               Height == other.Height &&
               CanWriteWithoutParameters == other.CanWriteWithoutParameters &&
               RequiresChildBoxes == other.RequiresChildBoxes &&
               EqualityComparer<TrackBox?>.Default.Equals(_trak, other._trak);
    }

    public override int GetHashCode()
    {
        HashCode hash = new HashCode();
        hash.Add(Size);
        hash.Add(Type);
        hash.Add(CanWriteWithoutParameters);
        hash.Add(Children);
        hash.Add(RequiresChildBoxes);
        hash.Add(Version);
        hash.Add(Flags);
        hash.Add(CreationTime);
        hash.Add(ModificationTime);
        hash.Add(TrackID);
        hash.Add(Duration);
        hash.Add(Layer);
        hash.Add(AlternateGroup);
        hash.Add(Volume);
        hash.Add(Matrix);
        hash.Add(Width);
        hash.Add(Height);
        hash.Add(CanWriteWithoutParameters);
        hash.Add(RequiresChildBoxes);
        hash.Add(_trak);
        return hash.ToHashCode();
    }

    public TrackHeaderBox(uint size, uint type)
        : base(size, type)
    {
    }

    public static bool operator ==(TrackHeaderBox? left, TrackHeaderBox? right)
    {
        return EqualityComparer<TrackHeaderBox>.Default.Equals(left, right);
    }

    public static bool operator !=(TrackHeaderBox? left, TrackHeaderBox? right)
    {
        return !(left == right);
    }
}
