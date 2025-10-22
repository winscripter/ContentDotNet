namespace ContentDotNet.Protocols.Sdp
{
    using ContentDotNet.Protocols.Sdp.Abstractions;
    using ContentDotNet.Protocols.Sdp.Lines;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    ///   The default SDP line serializer that uses built-in line models.
    /// </summary>
    public class BuiltInLineSerializer : ISdpLineSerializer
    {
        /// <summary>
        ///   Singleton instance of the <see cref="BuiltInLineSerializer"/> class.
        /// </summary>
        public static readonly BuiltInLineSerializer Instance = new();

        private delegate ISdpLineModel ModelFactory(string? line);

        private static readonly Dictionary<char, ModelFactory> s_models = new()
        {
            { 'v', (x) => new SdpVersionLine(x) },
            { 'o', (x) => new SdpOriginLine(x) },
            { 's', (x) => new SdpSessionNameLine(x) },
            { 'i', (x) => new SdpSessionInformationLine(x) },
            { 'u', (x) => new SdpUriLine(x) },
            { 'e', (x) => new SdpEmailAddressLine(x) },
            { 'p', (x) => new SdpPhoneNumberLine(x) },
            { 'c', (x) => new SdpConnectionInformationLine(x) },
            { 'b', (x) => new SdpBandwidthInformationLine(x) },
            { 't', (x) => new SdpTimeActiveLine(x) },
            { 'r', (x) => new SdpRepeatTimesLine(x) },
            { 'z', (x) => new SdpTimeZoneAdjustmentLine(x) },
            { 'k', (x) => new SdpEncryptionKeysLine(x) },
            { 'a', (x) => new SdpAttributesLine(x) },
            { 'm', (x) => new SdpMediaDescriptorsLine(x) }
        };

        private static ISdpLineModel ReadModel(string? line)
        {
            if (s_models.TryGetValue(line?[0] ?? '\0', out ModelFactory? factory))
            {
                return factory(line);
            }

            throw new SdpException("The character of the provided line before the equals (=) character does not match any known SDP line");
        }

        /// <inheritdoc cref="ISdpLineSerializer.Read(TextReader)" />
        public ISdpLineModel Read(TextReader reader)
        {
            return ReadModel(reader.ReadLine());
        }

        /// <inheritdoc cref="ISdpLineSerializer.ReadAsync(TextReader)" />
        public async Task<ISdpLineModel> ReadAsync(TextReader reader)
        {
            return ReadModel(await reader.ReadLineAsync());
        }

        /// <inheritdoc cref="ISdpLineSerializer.Write(ISdpLineModel, TextWriter)" />
        public void Write(ISdpLineModel model, TextWriter writer)
        {
            if (model.RawText != null)
            {
                writer.WriteLine(model.RawText);
            }
            else
            {
                throw new SdpException("Raw text is missing");
            }
        }

        /// <inheritdoc cref="ISdpLineSerializer.WriteAsync(ISdpLineModel, TextWriter)" />
        public async Task WriteAsync(ISdpLineModel model, TextWriter writer)
        {
            if (model.RawText != null)
            {
                await writer.WriteLineAsync(model.RawText);
            }
            else
            {
                throw new SdpException("Raw text is missing");
            }
        }
    }
}
