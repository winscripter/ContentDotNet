namespace ContentDotNet.Video.Formats.Mp4.Boxes
{
    using ContentDotNet.Api.Primitives;
    using System.IO;

    public class SampleTableBox : Mp4BoxBase
    {
        public SampleDescriptionsBox? SampleDescriptions { get; set; }
        public SampleTimeToSampleBox? TimeToSample { get; set; }
        public CompositionTimeToSampleBox? CompositionTimeToSample { get; set; }
        public SampleToChunkBox? SampleToChunk { get; set; }
        public SampleSizeBox? SampleSizeTable { get; set; }
        public SampleChunkOffsetBox? SampleChunkOffsets { get; set; }
        public SyncSampleBox? SyncSamples { get; set; }

        // ---------------------------
        // READ (Parse)
        // ---------------------------
        public static SampleTableBox Parse(BinaryReader reader, long boxSize)
        {
            var box = new SampleTableBox
            {
                Size = boxSize,
                Type = new FourCC("stbl")
            };

            long start = reader.BaseStream.Position;
            long end = start + (boxSize - 8);

            while (reader.BaseStream.Position < end)
            {
                if (end - reader.BaseStream.Position < 8)
                    break;

                uint childSize = reader.ReadUInt32();
                var childType = new FourCC(reader.ReadUInt32());
                long nextChild = reader.BaseStream.Position + (childSize - 8);

                switch (childType.ToString())
                {
                    case "stsd":
                        box.SampleDescriptions = SampleDescriptionsBox.Parse(reader, childSize, 1_000_000);
                        break;
                    case "stts":
                        box.TimeToSample = SampleTimeToSampleBox.Parse(reader, childSize, 1_000_000);
                        break;
                    case "ctts":
                        box.CompositionTimeToSample = CompositionTimeToSampleBox.Parse(reader, childSize);
                        break;
                    case "stsc":
                        box.SampleToChunk = SampleToChunkBox.Parse(reader, childSize, 1_000_000);
                        break;
                    case "stsz":
                    case "stz2":
                        box.SampleSizeTable = SampleSizeBox.Parse(reader, childSize);
                        break;
                    case "stco":
                    case "co64":
                        box.SampleChunkOffsets = SampleChunkOffsetBox.Parse(reader, childSize);
                        break;
                    case "stss":
                        box.SyncSamples = SyncSampleBox.Parse(reader, childSize);
                        break;
                    default:
                        // Skip unknown box types gracefully
                        reader.BaseStream.Seek(childSize - 8, SeekOrigin.Current);
                        break;
                }

                reader.BaseStream.Position = nextChild;
            }

            return box;
        }

        // ---------------------------
        // WRITE (Serialize)
        // ---------------------------
        public void Write(BinaryWriter writer)
        {
            // Build all children into memory buffer first
            using var ms = new MemoryStream();
            using (var childWriter = new BinaryWriter(ms))
            {
                SampleDescriptions?.Write(childWriter);
                TimeToSample?.Write(childWriter);
                CompositionTimeToSample?.Write(childWriter);
                SampleToChunk?.Write(childWriter);
                SampleSizeTable?.Write(childWriter);
                SampleChunkOffsets?.Write(childWriter);
                SyncSamples?.Write(childWriter);
            }

            // Compute total size and write base header
            Size = 8 + ms.Length;
            WriteBase(writer);

            // Write child boxes
            writer.Write(ms.ToArray());
        }

        public override string ToString()
        {
            var info = "stbl:";
            if (SampleDescriptions != null) info += " stsd";
            if (TimeToSample != null) info += " stts";
            if (CompositionTimeToSample != null) info += " ctts";
            if (SampleToChunk != null) info += " stsc";
            if (SampleSizeTable != null) info += " stsz";
            if (SampleChunkOffsets != null) info += " stco";
            if (SyncSamples != null) info += " stss";
            return info;
        }
    }
}
