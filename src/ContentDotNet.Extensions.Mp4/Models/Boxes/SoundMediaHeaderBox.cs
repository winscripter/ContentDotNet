namespace ContentDotNet.Extensions.Mp4.Models.Boxes;

[BoxInfo("smhd", "Sound Media Header", "The header that describes audio tracks")]
public sealed class SoundMediaHeaderBox : FullBox, IEquatable<SoundMediaHeaderBox?>
{
    public ushort Balance { get; set; }

    public override bool CanWriteWithoutParameters => true;

    public override bool RequiresChildBoxes => false;

    public SoundMediaHeaderBox(uint size, uint type)
        : base(size, type)
    {
    }

    private MediaInformationBox? _minf;

    public MediaInformationBox? GetMinf() => _minf;
    public void UseMinf(MediaInformationBox? minf) => _minf = minf;

    public static SoundMediaHeaderBox Read(BinaryReader reader)
    {
        var (size, type, version, flags) = ParseFullBoxHeader(reader);
        var smhd = new SoundMediaHeaderBox(size, type)
        {
            Version = version,
            Flags = flags,
            Balance = reader.ReadUInt16()
        };
        _ = reader.ReadUInt16();
        return smhd;
    }

    public override void Write(BinaryWriter writer)
    {
        WriteFullBoxHeader(writer);
        writer.Write(Balance);
        writer.Write((ushort)0);
    }

    public override bool IsCompatibleWith(Box other)
    {
        return Type == other.Type;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as SoundMediaHeaderBox);
    }

    public bool Equals(SoundMediaHeaderBox? other)
    {
        return other is not null &&
               Size == other.Size &&
               Type == other.Type &&
               CanWriteWithoutParameters == other.CanWriteWithoutParameters &&
               EqualityComparer<Box[]?>.Default.Equals(Children, other.Children) &&
               RequiresChildBoxes == other.RequiresChildBoxes &&
               Version == other.Version &&
               Flags == other.Flags &&
               Balance == other.Balance &&
               CanWriteWithoutParameters == other.CanWriteWithoutParameters &&
               RequiresChildBoxes == other.RequiresChildBoxes &&
               EqualityComparer<MediaInformationBox?>.Default.Equals(_minf, other._minf);
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
        hash.Add(Balance);
        hash.Add(CanWriteWithoutParameters);
        hash.Add(RequiresChildBoxes);
        hash.Add(_minf);
        return hash.ToHashCode();
    }

    public static bool operator ==(SoundMediaHeaderBox? left, SoundMediaHeaderBox? right)
    {
        return EqualityComparer<SoundMediaHeaderBox>.Default.Equals(left, right);
    }

    public static bool operator !=(SoundMediaHeaderBox? left, SoundMediaHeaderBox? right)
    {
        return !(left == right);
    }
}
