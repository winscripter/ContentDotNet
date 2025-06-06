namespace ContentDotNet.Extensions.Mp4.Models.Boxes;

public sealed class MediaHeaderBox : FullBox, IEquatable<MediaHeaderBox?>
{
    public ulong CreationTime { get; set; }
    public ulong ModificationTime { get; set; }
    public uint Timescale { get; set; }
    public ulong Duration { get; set; }
    public ushort Language { get; set; }

    public override bool CanWriteWithoutParameters => true;

    public override bool RequiresChildBoxes => false;

    private MediaBox? _mdia;

    public MediaBox? GetMdia() => _mdia;
    public void UseMdia(MediaBox? mdia) => _mdia = mdia;

    public MediaHeaderBox(uint size, uint type)
        : base(size, type)
    {
    }

    public static MediaHeaderBox Read(BinaryReader reader)
    {
        var (size, type, version, flags) = ParseFullBoxHeader(reader);
        var mdhd = new MediaHeaderBox(size, type)
        {
            Version = version,
            Flags = flags
        };
        if (version == 0)
        {
            mdhd.CreationTime = reader.ReadUInt32();
            mdhd.ModificationTime = reader.ReadUInt32();
            mdhd.Timescale = reader.ReadUInt32();
            mdhd.Duration = reader.ReadUInt32();
        }
        else
        {
            mdhd.CreationTime = reader.ReadUInt64();
            mdhd.ModificationTime = reader.ReadUInt64();
            mdhd.Timescale = reader.ReadUInt32();
            mdhd.Duration = reader.ReadUInt64();
        }

        mdhd.Language = reader.ReadUInt16();
        _ = reader.ReadUInt16();

        return mdhd;
    }

    public override void Write(BinaryWriter writer)
    {
        WriteFullBoxHeader(writer);
        if (Version == 0)
        {
            writer.Write((uint)CreationTime);
            writer.Write((uint)ModificationTime);
            writer.Write(Timescale);
            writer.Write((uint)Duration);
        }
        else
        {
            writer.Write(CreationTime);
            writer.Write(ModificationTime);
            writer.Write(Timescale);
            writer.Write(Duration);
        }
        writer.Write(Language);
        writer.Write((ushort)0);
    }

    public override bool IsCompatibleWith(Box other)
    {
        throw new NotImplementedException();
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as MediaHeaderBox);
    }

    public bool Equals(MediaHeaderBox? other)
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
               Timescale == other.Timescale &&
               Duration == other.Duration &&
               Language == other.Language &&
               CanWriteWithoutParameters == other.CanWriteWithoutParameters &&
               RequiresChildBoxes == other.RequiresChildBoxes &&
               EqualityComparer<MediaBox?>.Default.Equals(_mdia, other._mdia);
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(Size);
        hash.Add(Type);
        hash.Add(CanWriteWithoutParameters);
        hash.Add(Children);
        hash.Add(RequiresChildBoxes);
        hash.Add(Version);
        hash.Add(Flags);
        hash.Add(CreationTime);
        hash.Add(ModificationTime);
        hash.Add(Timescale);
        hash.Add(Duration);
        hash.Add(Language);
        hash.Add(CanWriteWithoutParameters);
        hash.Add(RequiresChildBoxes);
        hash.Add(_mdia);
        return hash.ToHashCode();
    }

    public static bool operator ==(MediaHeaderBox? left, MediaHeaderBox? right)
    {
        return EqualityComparer<MediaHeaderBox>.Default.Equals(left, right);
    }

    public static bool operator !=(MediaHeaderBox? left, MediaHeaderBox? right)
    {
        return !(left == right);
    }
}
