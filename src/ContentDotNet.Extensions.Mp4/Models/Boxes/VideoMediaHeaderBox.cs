namespace ContentDotNet.Extensions.Mp4.Models.Boxes;

[BoxInfo("vmhd", "Video Media Header", "The header that describes video tracks")]
public sealed class VideoMediaHeaderBox : FullBox, IEquatable<VideoMediaHeaderBox?>
{
    public ushort GraphicsMode { get; set; }
    public ushort[] Opcolor { get; set; } = new ushort[3];

    public override bool CanWriteWithoutParameters => true;

    public override bool RequiresChildBoxes => false;

    public VideoMediaHeaderBox(uint size, uint type)
        : base(size, type)
    {
    }

    private MediaInformationBox? _minf;

    public MediaInformationBox? GetMinf() => _minf;
    public void UseMinf(MediaInformationBox? minf) => _minf = minf;

    public static VideoMediaHeaderBox Read(BinaryReader reader)
    {
        var (size, type, version, flags) = ParseFullBoxHeader(reader);
        var vmhd = new VideoMediaHeaderBox(size, type)
        {
            Version = version,
            Flags = flags,
            GraphicsMode = reader.ReadUInt16(),
            Opcolor = new ushort[3]
        };
        vmhd.Opcolor[0] = reader.ReadUInt16();
        vmhd.Opcolor[1] = reader.ReadUInt16();
        vmhd.Opcolor[2] = reader.ReadUInt16();
        return vmhd;
    }

    public override void Write(BinaryWriter writer)
    {
        WriteFullBoxHeader(writer);
        writer.Write(GraphicsMode);
        for (int i = 0; i < 3; i++)
            writer.Write(Opcolor[i]);
    }

    public override bool IsCompatibleWith(Box other)
    {
        return Type == other.Type;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as VideoMediaHeaderBox);
    }

    public bool Equals(VideoMediaHeaderBox? other)
    {
        return other is not null &&
               Size == other.Size &&
               Type == other.Type &&
               CanWriteWithoutParameters == other.CanWriteWithoutParameters &&
               EqualityComparer<Box[]?>.Default.Equals(Children, other.Children) &&
               RequiresChildBoxes == other.RequiresChildBoxes &&
               Version == other.Version &&
               Flags == other.Flags &&
               GraphicsMode == other.GraphicsMode &&
               EqualityComparer<ushort[]>.Default.Equals(Opcolor, other.Opcolor) &&
               CanWriteWithoutParameters == other.CanWriteWithoutParameters &&
               RequiresChildBoxes == other.RequiresChildBoxes;
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
        hash.Add(GraphicsMode);
        hash.Add(Opcolor);
        hash.Add(CanWriteWithoutParameters);
        hash.Add(RequiresChildBoxes);
        return hash.ToHashCode();
    }

    public static bool operator ==(VideoMediaHeaderBox? left, VideoMediaHeaderBox? right)
    {
        return EqualityComparer<VideoMediaHeaderBox>.Default.Equals(left, right);
    }

    public static bool operator !=(VideoMediaHeaderBox? left, VideoMediaHeaderBox? right)
    {
        return !(left == right);
    }
}
