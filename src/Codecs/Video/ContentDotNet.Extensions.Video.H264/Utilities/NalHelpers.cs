namespace ContentDotNet.Extensions.Video.H264.Utilities
{
    using ContentDotNet.BitStream;

    /// <summary>
    ///   Utilities that assist with NAL units.
    /// </summary>
    public static class NalHelpers
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

            Stream stream = reader.BaseStream;
            long originalPosition = stream.Position;

            int b1, b2 = -1, b3 = -1, b4;

            while ((b4 = stream.ReadByte()) != -1)
            {
                b1 = b2;
                b2 = b3;
                b3 = b4;

                if ((b2 == 0x00 && b3 == 0x00 && b4 == 0x01) ||
                    (b1 == 0x00 && b2 == 0x00 && b3 == 0x00))
                {
                    _ = stream.ReadByte();

                    return true;
                }
            }

            stream.Position = originalPosition;
            return false;
        }
    }
}
