namespace ContentDotNet.Subtitles.Formats.Ssa
{
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    ///   SSA writer
    /// </summary>
    public class SsaWriter : ISubtitleWriter
    {
        public StreamWriter Writer { get; }

        private bool _writingEvents = false;

        public SsaWriter(StreamWriter writer, ISubtitleTimeFormatter formatter)
        {
            Writer = writer;
            Formatter = formatter;
        }

        public ISubtitleTimeFormatter Formatter { get; set; } = SsaSubtitleTimeFormatter.Instance;

        public void WriteScriptInfo(SsaScriptInfoSection scriptInfo)
        {
            if (_writingEvents)
                throw new InvalidOperationException("This SsaWriter is already writing the [Events] section.");

            Writer.WriteLine("[Script Info]");
            if (scriptInfo.Title != null) Writer.WriteLine($"Title: {scriptInfo.Title}");
            if (scriptInfo.ScriptType != null) Writer.WriteLine($"ScriptType: {scriptInfo.ScriptType}");
            if (scriptInfo.WrapStyle != null) Writer.WriteLine($"WrapStyle: {scriptInfo.WrapStyle}");
            if (scriptInfo.PlayResX != null) Writer.WriteLine($"PlayResX: {scriptInfo.PlayResX}");
            if (scriptInfo.PlayResY != null) Writer.WriteLine($"PlayResY: {scriptInfo.PlayResY}");
            if (scriptInfo.ScaledBorderAndShadow != null) Writer.WriteLine($"ScaledBorderAndShadow: {scriptInfo.ScaledBorderAndShadow}");
        }

        public async Task WriteScriptInfoAsync(SsaScriptInfoSection scriptInfo)
        {
            if (_writingEvents)
                throw new InvalidOperationException("This SsaWriter is already writing the [Events] section.");

            await Writer.WriteLineAsync("[Script Info]");
            if (scriptInfo.Title != null) await Writer.WriteLineAsync($"Title: {scriptInfo.Title}");
            if (scriptInfo.ScriptType != null) await Writer.WriteLineAsync($"ScriptType: {scriptInfo.ScriptType}");
            if (scriptInfo.WrapStyle != null) await Writer.WriteLineAsync($"WrapStyle: {scriptInfo.WrapStyle}");
            if (scriptInfo.PlayResX != null) await Writer.WriteLineAsync($"PlayResX: {scriptInfo.PlayResX}");
            if (scriptInfo.PlayResY != null) await Writer.WriteLineAsync($"PlayResY: {scriptInfo.PlayResY}");
            if (scriptInfo.ScaledBorderAndShadow != null) await Writer.WriteLineAsync($"ScaledBorderAndShadow: {scriptInfo.ScaledBorderAndShadow}");
        }

        public void WriteV4PlusStyles(string? format, List<string> lines)
        {
            if (_writingEvents)
                throw new InvalidOperationException("This SsaWriter is already writing the [Events] section.");

            Writer.WriteLine("[V4+ Styles]");
            Writer.WriteLine($"Format: {format}");
            foreach (string line in lines)
                Writer.WriteLine($"Style: {line}");
        }

        public async Task WriteV4PlusStylesAsync(string? format, List<string> lines)
        {
            if (_writingEvents)
                throw new InvalidOperationException("This SsaWriter is already writing the [Events] section.");

            await Writer.WriteLineAsync("[V4+ Styles]");
            await Writer.WriteLineAsync($"Format: {format}");
            foreach (string line in lines)
                await Writer.WriteLineAsync($"Style: {line}");
        }

        public void WriteLine(ISubtitleLine line)
        {
            if (line is not SsaLine ssaLine)
                throw new InvalidOperationException("Input ISubtitleLine must be SsaLine");

            if (!_writingEvents)
            {
                _writingEvents = true;
                Writer.WriteLine("[Events]");
                Writer.WriteLine("Format: " + ssaLine.Format);
            }

            Writer.WriteLine($"Dialogue: {line.Text}");
        }

        public async Task WriteLineAsync(ISubtitleLine line)
        {
            if (line is not SsaLine ssaLine)
                throw new InvalidOperationException("Input ISubtitleLine must be SsaLine");

            if (!_writingEvents)
            {
                _writingEvents = true;
                await Writer.WriteLineAsync("[Events]");
                await Writer.WriteLineAsync("Format: " + ssaLine.Format);
            }

            await Writer.WriteLineAsync($"Dialogue: {line.Text}");
        }
    }
}
