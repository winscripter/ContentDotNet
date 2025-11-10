namespace ContentDotNet.Containers.Formats.Ebml
{
    using ContentDotNet.Api.Binary;

    /// <summary>
    ///   Variable integer.
    /// </summary>
    public struct EbmlVariableInteger
    {
        private ulong _vintData;
        private byte _vintLength;

        /// <summary>
        ///   Initializes a new instance of the <see cref="EbmlVariableInteger"/> structure.
        /// </summary>
        /// <param name="vintData">The actual integer.</param>
        /// <param name="vintLength">The length of that integer.</param>
        /// <remarks>
        ///   The length must not be greater than 8 or less than 1.
        /// </remarks>
        public EbmlVariableInteger(ulong vintData, byte vintLength)
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(vintLength, 8, nameof(vintLength));
            ArgumentOutOfRangeException.ThrowIfLessThan(vintLength, 1, nameof(vintLength));

            _vintData = vintData;
            _vintLength = vintLength;
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="EbmlVariableInteger"/> structure. The backing
        ///   variable integer data is set equal to 0.
        /// </summary>
        /// <param name="vintLength">The length of the integer.</param>
        /// <remarks>
        ///   The length must not be greater than 8 or less than 1.
        /// </remarks>
        public EbmlVariableInteger(byte vintLength)
            : this(0uL, vintLength)
        {
        }

        /// <summary>
        ///   The actual data of this variable integer.
        /// </summary>
        public ulong Value
        {
            readonly get => _vintData;
            set => _vintData = value;
        }

        /// <summary>
        ///   The actual length of the variable integer.
        /// </summary>
        /// <remarks>
        ///   The length must not be greater than 8 or less than 1.
        /// </remarks>
        public byte Length
        {
            readonly get => _vintLength;
            set
            {
                ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 8, nameof(value));
                ArgumentOutOfRangeException.ThrowIfLessThan(value, 1, nameof(value));

                _vintLength = value;
            }
        }

        /// <summary>
        ///   Reads the EBML Variable Length Integer from the specified Binary Reader.
        ///   Seeking support is not required.
        /// </summary>
        /// <param name="reader">
        ///   The input Binary Reader that wraps around any Stream. The data is read here.
        /// </param>
        /// <returns>
        ///   The resulting <see cref="EbmlVariableInteger"/> structure that was read from the
        ///   specified <paramref name="reader"/> Binary Reader.
        /// </returns>
        public static EbmlVariableInteger Read(BinaryReader reader)
        {
            byte b = reader.ReadByte();

            byte length = 0;
            byte mask = 0x80; // 1000 0000

            while (length < 8 && (b & mask) == 0)
            {
                mask >>= 1;
                length++;
            }

            length++; // Total length = zeros + 1

            if (length > 8)
                throw new InvalidDataException("Invalid EBML VINT length (max 8 bytes).");

            // Remove the marker bit
            ulong value = (ulong)(b & (~mask));

            // Read remaining bytes
            for (int i = 1; i < length; i++)
            {
                byte nextByte = reader.ReadByte();
                value = (value << 8) | nextByte;
            }

            return new(value, length);
        }

        /// <summary>
        ///   Reads the EBML Variable Length Integer from the specified Binary Reader.
        ///   Seeking support is not required.
        /// </summary>
        /// <param name="reader">
        ///   The input Binary Reader that wraps around any Stream. The data is read here.
        /// </param>
        /// <returns>
        ///   The resulting <see cref="EbmlVariableInteger"/> structure that was read from the
        ///   specified <paramref name="reader"/> Binary Reader.
        /// </returns>
        public static async Task<EbmlVariableInteger> ReadAsync(BinaryReader reader)
        {
            byte b = await reader.ReadByteAsync();

            byte length = 0;
            byte mask = 0x80; // 1000 0000

            while (length < 8 && (b & mask) == 0)
            {
                mask >>= 1;
                length++;
            }

            length++; // Total length = zeros + 1

            if (length > 8)
                throw new InvalidDataException("Invalid EBML VINT length (max 8 bytes).");

            // Remove the marker bit
            ulong value = (ulong)(b & (~mask));

            // Read remaining bytes
            for (int i = 1; i < length; i++)
            {
                byte nextByte = await reader.ReadByteAsync();
                value = (value << 8) | nextByte;
            }

            return new(value, length);
        }

        /// <summary>
        ///   Writes this variable-length integer to the specified binary writer, synchronously.
        /// </summary>
        /// <param name="writer">The specified binary writer.</param>
        /// <param name="isSizeField">Is this the size/value VINT (true), or Element ID (false)?</param>
        public readonly void Write(BinaryWriter writer, bool isSizeField = true)
        {
            // Prepare buffer for big-endian output
            Span<byte> buffer = stackalloc byte[Length];
            ulong value = Value;

            // Write value bytes in big-endian order
            for (int i = 0; i < Length; i++)
            {
                buffer[Length - 1 - i] = (byte)(value & 0xFF);
                value >>= 8;
            }

            // Add the marker bit (based on length)
            byte marker = (byte)(1 << (8 - Length));
            if (isSizeField)
            {
                // For size/value VINTs, clear marker bit position before setting it
                buffer[0] |= marker;
            }
            else
            {
                // For Element IDs, the marker bit is part of the value naturally
                // No extra masking required, but we ensure the marker is set.
                buffer[0] |= marker;
            }

            // Write buffer to the stream
            writer.Write(buffer);
        }

        /// <summary>
        ///   Writes this variable-length integer to the specified binary writer, synchronously.
        /// </summary>
        /// <param name="writer">The specified binary writer.</param>
        /// <param name="isSizeField">Is this the size/value VINT (true), or Element ID (false)?</param>
        /// <remarks>
        ///   <b>Note:</b> Unlike the synchronous overload of the Write method (see <see cref="Write(BinaryWriter, bool)"/>),
        ///   this method <b>performs</b> heap allocations. Although they're low, they can potentially slow things down.
        ///   For a stack-only approach, use <see cref="Write(BinaryWriter, bool)"/>.
        /// </remarks>
        public readonly async Task WriteAsync(BinaryWriter writer, bool isSizeField = true)
        {
            // Prepare buffer for big-endian output
            byte[] buffer = new byte[Length];
            ulong value = Value;

            // Write value bytes in big-endian order
            for (int i = 0; i < Length; i++)
            {
                buffer[Length - 1 - i] = (byte)(value & 0xFF);
                value >>= 8;
            }

            // Add the marker bit (based on length)
            byte marker = (byte)(1 << (8 - Length));
            if (isSizeField)
            {
                // For size/value VINTs, clear marker bit position before setting it
                buffer[0] |= marker;
            }
            else
            {
                // For Element IDs, the marker bit is part of the value naturally
                // No extra masking required, but we ensure the marker is set.
                buffer[0] |= marker;
            }

            // Write buffer to the stream
            await writer.BaseStream.WriteAsync(buffer);
        }
    }
}
