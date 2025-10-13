namespace ContentDotNet.Protocols.Sdp
{
    using ContentDotNet.Protocols.Sdp.Abstractions;
    using ContentDotNet.Protocols.Sdp.EventArguments;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    ///   An SDP service.
    /// </summary>
    public interface ISdpService
    {
        /// <summary>
        ///   The last exception that was thrown by the <see cref="ISdpLineSerializer.ReadAsync(TextReader)"/> and
        ///   <see cref="ISdpLineSerializer.Read(TextReader)"/> method.
        /// </summary>
        Exception? ExceptionThrownOnRead { get; }

        /// <summary>
        ///   The last SDP line that was parsed or written. This is by default <see langword="null"/>
        ///   if no read or write method was invoked, or the last read method failed.
        /// </summary>
        ISdpLineModel? LastLine { get; }

        /// <summary>
        ///   The SDP serializer to use in the read and write methods. Defaults to <see cref="BuiltInLineSerializer.Instance"/>.
        /// </summary>
        ISdpLineSerializer Serializer { get; set; }

        /// <summary>
        ///   An event invoked when a line is read.
        /// </summary>
        event EventHandler<LineReceivedEventArgs>? LineReceived;

        /// <summary>
        ///   An event invoked when a line is written.
        /// </summary>
        event EventHandler<LineWrittenEventArgs>? LineWritten;

        /// <summary>
        ///   Reads a single line from the <paramref name="reader"/> and returns it, using <see cref="Serializer"/>.
        ///   If the <see cref="ISdpLineSerializer.Read(TextReader)"/> or <see cref="ISdpLineSerializer.ReadAsync(TextReader)"/>
        ///   method throws, the exception that was thrown is set to be the <see cref="ExceptionThrownOnRead"/> property,
        ///   and this method returns <see langword="null"/>. If reading the line was successful, this returns
        ///   the line that was read and <see cref="ExceptionThrownOnRead"/> remains untouched.
        /// </summary>
        /// <param name="reader">The input text reader to read the SDP line from.</param>
        /// <returns>
        ///   <list type="bullet">
        ///     <item>Line failed to parse -&gt; <see langword="null"/></item>
        ///     <item>Line parsed successfully - the SDP line that was parsed.</item>
        ///   </list>
        /// </returns>
        ISdpLineModel? ReadLine(TextReader reader);

        /// <inheritdoc cref="ReadLine(TextReader)" />
        Task<ISdpLineModel?> ReadLineAsync(TextReader reader);

        /// <summary>
        ///   Writes the provided SDP line to the <paramref name="writer"/> parameter.
        /// </summary>
        /// <param name="line">The input SDP line to write.</param>
        /// <param name="writer">The <see cref="TextWriter"/> where the line is written to.</param>
        void WriteLine(ISdpLineModel line, TextWriter writer);

        /// <inheritdoc cref="WriteLine(ISdpLineModel, TextWriter)" />
        Task WriteLineAsync(ISdpLineModel line, TextWriter writer);
    }
}