namespace ContentDotNet.Binary
{
    /// <summary>
    ///   Allows reading primitive values like <see cref="int"/> or <see cref="string"/>
    ///   to <see cref="BinaryReader"/> asynchronously.
    /// </summary>
    public static class AsyncBinaryReadHelper
    {
        /// <summary>
        /// Asynchronously reads a <see cref="byte"/> value from the underlying stream of the specified <see cref="BinaryReader"/>.
        /// </summary>
        public static async Task<byte> ReadByteAsync(this BinaryReader reader)
        {
            var buffer = new byte[sizeof(byte)];
            await reader.BaseStream.ReadAsync(buffer);
            return buffer[0];
        }

        /// <summary>
        /// Asynchronously reads a <see cref="sbyte"/> value from the underlying stream of the specified <see cref="BinaryReader"/>.
        /// </summary>
        public static async Task<sbyte> ReadSByteAsync(this BinaryReader reader)
        {
            var buffer = new byte[sizeof(sbyte)];
            await reader.BaseStream.ReadAsync(buffer);
            return (sbyte)buffer[0];
        }

        /// <summary>
        /// Asynchronously reads a <see cref="ushort"/> value from the underlying stream of the specified <see cref="BinaryReader"/>.
        /// </summary>
        public static async Task<ushort> ReadUInt16Async(this BinaryReader reader)
        {
            var buffer = new byte[sizeof(ushort)];
            await reader.BaseStream.ReadAsync(buffer);
            return BitConverter.ToUInt16(buffer);
        }

        /// <summary>
        /// Asynchronously reads a <see cref="short"/> value from the underlying stream of the specified <see cref="BinaryReader"/>.
        /// </summary>
        public static async Task<short> ReadInt16Async(this BinaryReader reader)
        {
            var buffer = new byte[sizeof(short)];
            await reader.BaseStream.ReadAsync(buffer);
            return BitConverter.ToInt16(buffer);
        }

        /// <summary>
        /// Asynchronously reads an <see cref="int"/> value from the underlying stream of the specified <see cref="BinaryReader"/>.
        /// </summary>
        public static async Task<int> ReadInt32Async(this BinaryReader reader)
        {
            var buffer = new byte[sizeof(int)];
            await reader.BaseStream.ReadAsync(buffer);
            return BitConverter.ToInt32(buffer);
        }

        /// <summary>
        /// Asynchronously reads a <see cref="uint"/> value from the underlying stream of the specified <see cref="BinaryReader"/>.
        /// </summary>
        public static async Task<uint> ReadUInt32Async(this BinaryReader reader)
        {
            var buffer = new byte[sizeof(uint)];
            await reader.BaseStream.ReadAsync(buffer);
            return BitConverter.ToUInt32(buffer);
        }

        /// <summary>
        /// Asynchronously reads a <see cref="long"/> value from the underlying stream of the specified <see cref="BinaryReader"/>.
        /// </summary>
        public static async Task<long> ReadInt64Async(this BinaryReader reader)
        {
            var buffer = new byte[sizeof(long)];
            await reader.BaseStream.ReadAsync(buffer);
            return BitConverter.ToInt64(buffer);
        }

        /// <summary>
        /// Asynchronously reads a <see cref="ulong"/> value from the underlying stream of the specified <see cref="BinaryReader"/>.
        /// </summary>
        public static async Task<ulong> ReadUInt64Async(this BinaryReader reader)
        {
            var buffer = new byte[sizeof(ulong)];
            await reader.BaseStream.ReadAsync(buffer);
            return BitConverter.ToUInt64(buffer);
        }

        /// <summary>
        /// Asynchronously reads a <see cref="float"/> value from the underlying stream of the specified <see cref="BinaryReader"/>.
        /// </summary>
        public static async Task<float> ReadSingleAsync(this BinaryReader reader)
        {
            var buffer = new byte[sizeof(float)];
            await reader.BaseStream.ReadAsync(buffer);
            return BitConverter.ToSingle(buffer);
        }

        /// <summary>
        /// Asynchronously reads a <see cref="double"/> value from the underlying stream of the specified <see cref="BinaryReader"/>.
        /// </summary>
        public static async Task<double> ReadDoubleAsync(this BinaryReader reader)
        {
            var buffer = new byte[sizeof(double)];
            await reader.BaseStream.ReadAsync(buffer);
            return BitConverter.ToDouble(buffer);
        }
    }
}
