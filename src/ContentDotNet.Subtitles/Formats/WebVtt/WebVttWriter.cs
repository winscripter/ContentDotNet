namespace ContentDotNet.Subtitles.Formats.WebVtt
{
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    ///   WebVTT file writer
    /// </summary>
    public class WebVttWriter(StreamWriter sw) : ISubtitleWriter
    {
        private bool _writtenWEBVTT = false;

        public StreamWriter Writer { get; } = sw;

        public ISubtitleTimeFormatter Formatter { get; set; } = WebVttSubtitleTimeFormatter.Instance;

        private void WriteWebVttAllCaps()
        {
            if (!_writtenWEBVTT)
            {
                _writtenWEBVTT = true;
                Writer.WriteLine("WEBVTT");
                Writer.WriteLine();
            }
        }

        private async Task WriteWebVttAllCapsAsync()
        {
            if (!_writtenWEBVTT)
            {
                _writtenWEBVTT = true;
                await Writer.WriteLineAsync("WEBVTT");
                await Writer.WriteLineAsync();
            }
        }

        public void WriteLine(ISubtitleLine line)
        {
            WriteWebVttAllCaps();
            Writer.WriteLine(line);
            Writer.WriteLine();
        }

        public async Task WriteLineAsync(ISubtitleLine line)
        {
            await WriteWebVttAllCapsAsync();
            await Writer.WriteLineAsync(line.ToString());
            await Writer.WriteLineAsync();
        }
    }
}
