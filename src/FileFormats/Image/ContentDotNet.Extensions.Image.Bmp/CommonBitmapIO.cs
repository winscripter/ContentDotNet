namespace ContentDotNet.Extensions.Image.Bmp
{
    using ContentDotNet.Binary;
    using ContentDotNet.Extensions.Image.Bmp.Headers;

    internal static class CommonBitmapIO
    {
        public static LONG ReadLONG(BinaryReader reader) => reader.ReadInt32();
        public static async Task<LONG> ReadLONGAsync(BinaryReader reader) => await reader.ReadInt32Async();

        public static DWORD ReadDWORD(BinaryReader reader) => reader.ReadInt32();
        public static async Task<DWORD> ReadDWORDAsync(BinaryReader reader) => await reader.ReadInt32Async();

        public static WORD ReadWORD(BinaryReader reader) => reader.ReadUInt16();
        public static async Task<WORD> ReadWORDAsync(BinaryReader reader) => await reader.ReadUInt16Async();

        public static BmpCieXyz ReadBmpCieXyz(BinaryReader reader) => BmpCieXyz.Read(reader);
        public static async Task<BmpCieXyz> ReadBmpCieXyzAsync(BinaryReader reader) => await BmpCieXyz.ReadAsync(reader);

        public static BmpCieXyzTriple ReadBmpCieXyzTriple(BinaryReader reader) => BmpCieXyzTriple.Read(reader);
        public static async Task<BmpCieXyzTriple> ReadBmpCieXyzTripleAsync(BinaryReader reader) => await BmpCieXyzTriple.ReadAsync(reader);

        public static void WriteLONG(BinaryWriter writer, LONG value) => writer.Write(value);
        public static async Task WriteLONGAsync(BinaryWriter writer, LONG value) => await writer.WriteAsync(value);

        public static void WriteDWORD(BinaryWriter writer, DWORD value) => writer.Write(value);
        public static async Task WriteDWORDAsync(BinaryWriter writer, DWORD value) => await writer.WriteAsync(value);

        public static void WriteWORD(BinaryWriter writer, WORD value) => writer.Write(value);
        public static async Task WriteWORDAsync(BinaryWriter writer, WORD value) => await writer.WriteAsync(value);

        public static void WriteBmpCieXyz(BinaryWriter writer, BmpCieXyz value) => value.Write(writer);
        public static async Task WriteBmpCieXyzAsync(BinaryWriter writer, BmpCieXyz value) => await value.WriteAsync(writer);

        public static void WriteBmpCieXyzTriple(BinaryWriter writer, BmpCieXyzTriple value) => value.Write(writer);
        public static async Task WriteBmpCieXyzTripleAsync(BinaryWriter writer, BmpCieXyzTriple value) => await value.WriteAsync(writer);
    }
}
