namespace ContentDotNet.Video.Codecs.H264.Components
{
    using ContentDotNet.Api.BitStream;
    using ContentDotNet.BitStream;

    /// <summary>
    ///   Utilities that assist with NAL units.
    /// </summary>
    public static class H264NalHelpers
    {
        /// <summary>
        ///   Skips to the start of the H.264 NAL unit, which is either 0x00 0x00 0x01 or 0x00 0x00 0x00 0x01 (the 4 byte start code has
        ///   bigger priority than the 3 byte one). For example:
        ///   <para />
        ///   Previous cursor (points to the 12 byte):
        ///   <code>
        ///     12 34 56 00 00 00 01 78 9A BC
        ///     ^
        ///     |
        ///   </code>
        ///   <para />
        ///   Cursor after invoking (points to the 78 byte):
        ///   <code>
        ///     12 34 56 00 00 00 01 78 9A BC
        ///                          ^
        ///                          |
        ///   </code>
        /// </summary>
        /// <param name="reader">
        ///   <para>
        ///     This is the source instance of <see cref="BitStreamReader"/>. Methods like <see cref="BitStreamReader.ReadBits(uint)"/>
        ///     can be used to read multiple bits, and <see cref="BitStreamReader.ReadByte"/> to read the next byte. The following code
        ///     snippet can be used to align to byte boundary, which, this method does:
        ///     <code>
        ///       while (reader.GetState().BitPosition != 0)
        ///           _ = reader.ReadBit();
        ///     </code>
        ///   </para>
        ///   <para>
        ///     The <see cref="BitStreamReader.BaseStream"/> property is the backing <see cref="Stream"/> instance.
        ///   </para>
        /// </param>
        /// <returns>
        ///   This method will return <see langword="bool"/>. The value is as follows:
        ///   <list type="bullet">
        ///     <item>
        ///       <para>If the start code was found and skipped, <see langword="true"/>.</para>
        ///     </item>
        ///     <item>
        ///       <para>Otherwise (the start code was not found, neither the 3 byte one nor the 4 byte one), <see langword="false"/>.</para>
        ///     </item>
        ///   </list>
        /// </returns>
        public static bool SkipToStartOfNalUnit(BitStreamReader reader)
        {
            // Align to byte boundary
            while (reader.GetState().BitPosition != 0)
                _ = reader.ReadBit();

            long originalPosition = reader.BaseStream.Position;

            int b1, b2 = -1, b3 = -1, b4 = -1;

            while (true)
            {
                int byteRead = reader.BaseStream.ReadByte();
                if (byteRead == -1)
                    break;

                b1 = b2;
                b2 = b3;
                b3 = b4;
                b4 = byteRead;

                if ((b2 == 0x00 && b3 == 0x00 && b4 == 0x01))
                {
                    //_ = reader.ReadByte();

                    return true;
                }
            }

            reader.BaseStream.Position = originalPosition;
            return false;
        }

        /// <summary>
        ///   Returns the length of the current NAL unit, assuming that the position of the NAL unit is within
        ///   the NAL data boundary, which can include the NAL Unit Header or RBSP bytes. In the following example:
        ///   <code>
        ///     1A B5 00 00 01 50 74 38 25 09 00 00 00 01 52 56
        ///                    ^
        ///                    |
        ///                    Position is here
        ///   </code>
        ///   <para>That will return 5.</para>
        ///   <para>In the following example:</para>
        ///   <code>
        ///     1A B5 00 00 01 50 74 38 25 09 00 00 00 01 52 56
        ///                                               ^
        ///                                               |
        ///                                               Position is here
        ///   </code>
        ///   <para>That returns 2.</para>
        ///   <para>This method works for both NAL units with a 3 byte start code <c>00 00 01</c> and 4-byte start code <c>00 00 00 01</c>.</para>
        ///   <para>This method will never throw.</para>
        ///   <para>This method retains the location in the specified <paramref name="reader"/> when returning.</para>
        /// </summary>
        /// <param name="reader">
        ///   Input bit-stream reader.
        /// </param>
        /// <returns>
        ///   The length of the NAL unit, in bytes.
        /// </returns>
        public static long GetNalUnitLength(BitStreamReader reader)
        {
            // Align to byte boundary
            while (reader.GetState().BitPosition != 0)
                _ = reader.ReadBit();

            var stream = reader.BaseStream;
            long startPosition = stream.Position;

            int b1, b2 = -1, b3 = -1, b4 = -1;
            long currentPosition = startPosition;

            while (true)
            {
                int byteRead = stream.ReadByte();
                if (byteRead == -1)
                    break;

                currentPosition++;

                b1 = b2;
                b2 = b3;
                b3 = b4;
                b4 = byteRead;

                // Check for 4-byte start code
                if (b1 == 0x00 && b2 == 0x00 && b3 == 0x00 && b4 == 0x01)
                {
                    stream.Position = startPosition;
                    return currentPosition - startPosition - 4;
                }

                // Check for 3-byte start code
                if (b2 == 0x00 && b3 == 0x00 && b4 == 0x01)
                {
                    stream.Position = startPosition;
                    return currentPosition - startPosition - 3;
                }
            }

            // No further start code found; return until end of stream
            long endPosition = stream.Position;
            stream.Position = startPosition;
            return endPosition - startPosition;
        }
    }
}
