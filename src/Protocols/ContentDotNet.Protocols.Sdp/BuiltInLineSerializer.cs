namespace ContentDotNet.Protocols.Sdp
{
    using ContentDotNet.Protocols.Sdp.Abstractions;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    ///   The default SDP line serializer that uses built-in line models.
    /// </summary>
    public class BuiltInLineSerializer : ISdpLineSerializer
    {
        private delegate ISdpLineModel ModelFactory();

        private static readonly Type ModelInterface = typeof(ISdpLineModel);
        private static readonly List<ModelFactory> s_models = [];

        /// <summary>
        ///   Singleton instance.
        /// </summary>
        public static readonly BuiltInLineSerializer Instance = new();

        /// <summary>
        ///   The static constructor initializes the <see cref="s_models"/> private static field.
        /// </summary>
        static BuiltInLineSerializer()
        {
            // Here we just get all the classes that inherit from ISdpLineModel
            // throughout the entire assembly and instantiate them using the
            // 'Activator.CreateInstance' method. They're located in the Lines folder.

            s_models =
                [.. typeof(BuiltInLineSerializer)
                .Assembly
                .GetTypes()
                .Where(type => type.BaseType == ModelInterface)
                .Select(x =>
                    {
                        return (ModelFactory)(() => (ISdpLineModel)Activator.CreateInstance(x, [null])!);
                    })];
        }

        private static ISdpLineModel ReadModel(string? line)
        {
            if (line == null)
                throw new EndOfStreamException();

            foreach (ModelFactory factory in s_models)
            {
                var model = factory();
                if (model.Character == line[0])
                {
                    model.RawText = line;
                    return model;
                }
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
