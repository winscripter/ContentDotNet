namespace ContentDotNet.Protocols.Bgp.MessageFormats.Serializers
{
    using ContentDotNet.Binary;
    using ContentDotNet.Protocols.Bgp.Models;
    using System.Net;

    internal static class CommonIOUtilities
    {
        public static T Read<T>(BinaryReader reader)
        {
            if (typeof(T) == typeof(byte)) return (T)(object)reader.ReadByte();
            else if (typeof(T) == typeof(ushort)) return (T)(object)reader.ReadUInt16();
            else if (typeof(T) == typeof(uint)) return (T)(object)reader.ReadUInt16();
            else if (typeof(T) == typeof(List<string>) ||
                typeof(T) == typeof(List<BgpPathAttributeModel>))
            {
                throw new NotSupportedException("Not supported!");
            }
            else if (typeof(T) == typeof(List<byte>))
            {
                return (T)(object)reader.ReadBytes(16);
            }
            else
            {
                throw new NotSupportedException("Not supported!");
            }
        }

        public static async Task<T> ReadAsync<T>(BinaryReader reader)
        {
            if (typeof(T) == typeof(byte)) return (T)(object)await reader.ReadByteAsync();
            else if (typeof(T) == typeof(ushort)) return (T)(object)await reader.ReadUInt16Async();
            else if (typeof(T) == typeof(uint)) return (T)(object)await reader.ReadUInt16Async();
            else if (typeof(T) == typeof(List<string>) ||
                typeof(T) == typeof(List<BgpPathAttributeModel>))
            {
                throw new NotSupportedException("Not supported!");
            }
            else if (typeof(T) == typeof(List<byte>))
            {
                byte[] buffer = new byte[16];
                await reader.BaseStream.ReadAsync(buffer);
                return (T)(object)buffer;
            }
            else
            {
                throw new NotSupportedException("Not supported!");
            }
        }

        public static void Write<T>(object value, BinaryWriter writer)
        {
            if (value == null)
                return;

            Type type = value.GetType();
            if (type == typeof(List<byte>))
            {
                List<byte> lb = (List<byte>)value;
                byte[] array = [.. lb];
                writer.BaseStream.Write(array);
            }
            else if (type == typeof(byte))
            {
                writer.Write((byte)value);
            }
            else if (type == typeof(ushort))
            {
                writer.Write((ushort)value);
            }
            else if (type == typeof(uint))
            {
                writer.Write((uint)value);
            }
            else if (type == typeof(List<string>))
            {
                writer.Write(EncodeBgpPrefixes((List<string>)value));
            }
            else if (type == typeof(List<BgpPathAttributeModel>))
            {
                foreach (BgpPathAttributeModel model in (List<BgpPathAttributeModel>)value)
                {
                    writer.Write(EncodePathAttributes(model));
                }
            }
        }

        public static async Task WriteAsync<T>(object value, BinaryWriter writer)
        {
            if (value == null)
                return;

            Type type = value.GetType();
            if (type == typeof(List<byte>))
            {
                List<byte> lb = (List<byte>)value;
                byte[] array = [.. lb];
                await writer.BaseStream.WriteAsync(array);
            }
            else if (type == typeof(byte))
            {
                await writer.WriteAsync((byte)value);
            }
            else if (type == typeof(ushort))
            {
                await writer.WriteAsync((ushort)value);
            }
            else if (type == typeof(uint))
            {
                await writer.WriteAsync((uint)value);
            }
            else if (type == typeof(List<string>))
            {
                await writer.BaseStream.WriteAsync(EncodeBgpPrefixes((List<string>)value));
            }
            else if (type == typeof(List<BgpPathAttributeModel>))
            {
                foreach (BgpPathAttributeModel model in (List<BgpPathAttributeModel>)value)
                {
                    await writer.BaseStream.WriteAsync(EncodePathAttributes(model));
                }
            }
        }

        public static byte[] EncodeBgpPrefixes(List<string> prefixes)
        {
            var result = new List<byte>();

            foreach (var prefix in prefixes)
            {
                var parts = prefix.Split('/');
                var ip = IPAddress.Parse(parts[0]);
                var prefixLength = byte.Parse(parts[1]);

                result.Add(prefixLength);

                int byteLength = (prefixLength + 7) / 8;
                byte[] ipBytes = ip.GetAddressBytes();

                // Only include the necessary bytes
                result.AddRange(ipBytes.Take(byteLength));
            }

            return [.. result];
        }

        private static byte[] EncodePathAttributes(BgpPathAttributeModel model)
        {
            var result = new List<byte> { model.Flags, model.TypeCode };

            if ((model.Flags & 0x10) != 0) // Extended Length
            {
                result.AddRange(BitConverter.GetBytes((ushort)model.Value.Length).Reverse());
            }
            else
            {
                result.Add((byte)model.Value.Length);
            }

            result.AddRange(model.Value);
            return [.. result];
        }
    }
}
