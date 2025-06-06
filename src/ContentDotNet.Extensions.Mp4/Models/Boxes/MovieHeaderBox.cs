namespace ContentDotNet.Extensions.Mp4.Models.Boxes;

[BoxInfo("mvhd", "Movie Header", "Defines overall information about the movie presentation")]
public sealed class MovieHeaderBox : FullBox, IEquatable<MovieHeaderBox?>
{
    public ulong CreationTime { get; set; }
    public ulong ModificationTime { get; set; }
    public uint TimeScale { get; set; }
    public ulong Duration { get; set; }
    public float Rate { get; set; } // 16.16 fixed-point
    public float Volume { get; set; } // 8.8 fixed-point
    public ushort[] Reserved { get; set; } = new ushort[2];
    public uint[] Reserved2 { get; set; } = new uint[2];
    public int[] Matrix { get; set; } = new int[9];
    public uint[] PreDefined { get; set; } = new uint[6];
    public uint NextTrackID { get; set; }

    private MovieBox? _moov = null;

    public MovieBox? GetMoov() => _moov;

    public void UseMoov(MovieBox? moov) => _moov = moov;

    public override bool CanWriteWithoutParameters => true;

    public override bool RequiresChildBoxes => false;

    public MovieHeaderBox(uint size, uint type) : base(size, type) { }

    public static MovieHeaderBox Read(BinaryReader reader)
    {
        var (size, type, version, flags) = ParseFullBoxHeader(reader);
        var box = new MovieHeaderBox(size, type)
        {
            Version = version,
            Flags = flags
        };

        if (version == 1)
        {
            box.CreationTime = reader.ReadUInt64();
            box.ModificationTime = reader.ReadUInt64();
            box.TimeScale = reader.ReadUInt32();
            box.Duration = reader.ReadUInt64();
        }
        else
        {
            box.CreationTime = reader.ReadUInt32();
            box.ModificationTime = reader.ReadUInt32();
            box.TimeScale = reader.ReadUInt32();
            box.Duration = reader.ReadUInt32();
        }

        box.Rate = ReadFixedPoint1616(reader);
        box.Volume = ReadFixedPoint88(reader);
        box.Reserved[0] = reader.ReadUInt16();
        box.Reserved[1] = reader.ReadUInt16();
        box.Reserved2[0] = reader.ReadUInt32();
        box.Reserved2[1] = reader.ReadUInt32();

        for (int i = 0; i < 9; i++)
            box.Matrix[i] = reader.ReadInt32();

        for (int i = 0; i < 6; i++)
            box.PreDefined[i] = reader.ReadUInt32();

        box.NextTrackID = reader.ReadUInt32();

        return box;
    }

    public override void Write(BinaryWriter writer)
    {
        WriteFullBoxHeader(writer);
        if (Version == 1)
        {
            writer.Write(CreationTime);
            writer.Write(ModificationTime);
            writer.Write(TimeScale);
            writer.Write(Duration);
        }
        else
        {
            writer.Write((uint)CreationTime);
            writer.Write((uint)ModificationTime);
            writer.Write(TimeScale);
            writer.Write((uint)Duration);
        }

        WriteFixedPoint1616(writer, Rate);
        WriteFixedPoint88(writer, Volume);
        writer.Write(Reserved[0]);
        writer.Write(Reserved[1]);
        writer.Write(Reserved2[0]);
        writer.Write(Reserved2[1]);

        foreach (int val in Matrix)
            writer.Write(val);

        foreach (uint val in PreDefined)
            writer.Write(val);

        writer.Write(NextTrackID);
    }

    private static float ReadFixedPoint1616(BinaryReader reader)
    {
        uint val = reader.ReadUInt32();
        return val / 65536f;
    }

    private static void WriteFixedPoint1616(BinaryWriter writer, float value)
    {
        writer.Write((uint)(value * 65536));
    }

    private static float ReadFixedPoint88(BinaryReader reader)
    {
        ushort val = reader.ReadUInt16();
        return val / 256f;
    }

    private static void WriteFixedPoint88(BinaryWriter writer, float value)
    {
        writer.Write((ushort)(value * 256));
    }

    public override bool Equals(object? obj) => Equals(obj as MovieHeaderBox);

    public bool Equals(MovieHeaderBox? other)
    {
        return other != null &&
               base.Equals(other) &&
               CreationTime == other.CreationTime &&
               ModificationTime == other.ModificationTime &&
               TimeScale == other.TimeScale &&
               Duration == other.Duration &&
               Rate == other.Rate &&
               Volume == other.Volume &&
               Reserved.SequenceEqual(other.Reserved) &&
               Reserved2.SequenceEqual(other.Reserved2) &&
               Matrix.SequenceEqual(other.Matrix) &&
               PreDefined.SequenceEqual(other.PreDefined) &&
               NextTrackID == other.NextTrackID;
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(base.GetHashCode());
        hash.Add(CreationTime);
        hash.Add(ModificationTime);
        hash.Add(TimeScale);
        hash.Add(Duration);
        hash.Add(Rate);
        hash.Add(Volume);
        foreach (var val in Reserved) hash.Add(val);
        foreach (var val in Reserved2) hash.Add(val);
        foreach (var val in Matrix) hash.Add(val);
        foreach (var val in PreDefined) hash.Add(val);
        hash.Add(NextTrackID);
        return hash.ToHashCode();
    }

    public override bool IsCompatibleWith(Box other)
    {
        return Type == other.Type;
    }
}
