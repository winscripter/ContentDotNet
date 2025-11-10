namespace ContentDotNet.Video.Formats.Mp4.Boxes
{
    using ContentDotNet.Api.Primitives;

    public class EditBox : Mp4BoxBase
    {
        public EditListBox? EditList { get; set; }

        public static EditBox Parse(BinaryReader reader, long boxSize)
        {
            var box = new EditBox
            {
                Size = boxSize,
                Type = new FourCC("edts")
            };

            long start = reader.BaseStream.Position;
            long end = start + (boxSize - 8);

            while (reader.BaseStream.Position < end)
            {
                uint childSize = reader.ReadUInt32();
                var childType = new FourCC(reader.ReadUInt32());

                if (childType.ToString() == "elst")
                    box.EditList = EditListBox.Parse(reader, childSize);
                else
                    reader.BaseStream.Seek(childSize - 8, SeekOrigin.Current);
            }

            return box;
        }

        public void Write(BinaryWriter writer)
        {
            using var ms = new MemoryStream();
            using (var childWriter = new BinaryWriter(ms))
            {
                EditList?.Write(childWriter);
            }

            Size = 8 + ms.Length;
            WriteBase(writer);
            writer.Write(ms.ToArray());
        }

        public override string ToString() =>
            $"edts: {(EditList != null ? EditList.Entries.Count + " edits" : "empty")}";
    }
}
