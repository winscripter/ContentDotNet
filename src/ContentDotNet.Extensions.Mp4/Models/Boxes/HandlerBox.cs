using ContentDotNet.Primitives;
using System.Text;

namespace ContentDotNet.Extensions.Mp4.Models.Boxes;

public sealed class HandlerBox : FullBox, IEquatable<HandlerBox?>
{
    public FourCC HandlerType { get; set; } = default;
    public string Name { get; set; } = string.Empty;

    public override bool CanWriteWithoutParameters => true;

    public override bool RequiresChildBoxes => false;

    public HandlerBox(uint size, uint type)
        : base(size, type)
    {
    }

    private MediaBox? _mdia;

    public MediaBox? GetMdia() => _mdia;
    public void UseMdia(MediaBox? mdia) => _mdia = mdia;

    public static HandlerBox Read(BinaryReader reader)
    {
        var (size, type, version, flags) = ParseFullBoxHeader(reader);
        var hdlr = new HandlerBox(size, type)
        {
            Version = version,
            Flags = flags
        };
        _ = reader.ReadUInt32(); // predefined
        hdlr.HandlerType = reader.ReadUInt32();
        for (int i = 0; i < 3; i++)
            _ = reader.ReadUInt32(); // reserved
        hdlr.Name = reader.ReadNullTerminatedString();
        return hdlr;
    }

    public override void Write(BinaryWriter writer)
    {
        WriteFullBoxHeader(writer);
        writer.Write(0u);
        writer.Write(this.HandlerType);
        for (int i = 0; i < 3; i++)
            writer.Write(0u);
        writer.WriteNullTerminatedString(Name);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as HandlerBox);
    }

    public bool Equals(HandlerBox? other)
    {
        return other is not null &&
               Size == other.Size &&
               Type == other.Type &&
               CanWriteWithoutParameters == other.CanWriteWithoutParameters &&
               EqualityComparer<Box[]?>.Default.Equals(Children, other.Children) &&
               RequiresChildBoxes == other.RequiresChildBoxes &&
               Version == other.Version &&
               Flags == other.Flags &&
               HandlerType.Equals(other.HandlerType) &&
               Name == other.Name;
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
        hash.Add(HandlerType);
        hash.Add(Name);
        return hash.ToHashCode();
    }

    public override bool IsCompatibleWith(Box other)
    {
        return Type == other.Type;
    }

    public static bool operator ==(HandlerBox? left, HandlerBox? right)
    {
        return EqualityComparer<HandlerBox>.Default.Equals(left, right);
    }

    public static bool operator !=(HandlerBox? left, HandlerBox? right)
    {
        return !(left == right);
    }
}
