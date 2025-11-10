namespace ContentDotNet.Video.Formats.Mp4.Boxes
{
    using ContentDotNet.Api.Primitives;
    using ContentDotNet.Video.Formats.Mp4.Boxes.Presets;
    using System.Collections.Generic;
    using System.IO;

    public class FileTypeBox : Mp4BoxBase
    {
        public FourCC MajorBrand { get; set; }
        public uint MinorVersion { get; set; }
        public List<FourCC> CompatibleBrands { get; set; } = [];

        public static FileTypeBox Parse(BinaryReader reader, long boxSize)
        {
            var box = new FileTypeBox
            {
                Size = boxSize,
                Type = new FourCC("ftyp"),
                MajorBrand = new FourCC(reader.ReadUInt32()),
                MinorVersion = reader.ReadUInt32()
            };

            long remaining = boxSize - 16; // skip size/type header (8) + 8 bytes read

            int compatibleCount = (int)(remaining / 4);
            for (int i = 0; i < compatibleCount; i++)
            {
                box.CompatibleBrands.Add(new FourCC(reader.ReadUInt32()));
            }

            return box;
        }

        public void Write(BinaryWriter writer)
        {
            long start = writer.BaseStream.Position;

            // Calculate total size
            Size = 8 + 4 + 4 + (CompatibleBrands.Count * 4);

            WriteBase(writer);

            writer.Write(MajorBrand.Value);
            writer.Write(MinorVersion);

            foreach (var brand in CompatibleBrands)
                writer.Write(brand.Value);

            // Optionally verify written size
            long written = writer.BaseStream.Position - start;
            if (written != Size)
                throw new InvalidDataException($"FileTypeBox size mismatch: expected {Size}, wrote {written}");
        }

        public override string ToString()
        {
            var brands = string.Join(", ", CompatibleBrands);
            return $"ftyp: Major={MajorBrand}, Minor={MinorVersion}, Compatible=[{brands}]";
        }
    }
}
