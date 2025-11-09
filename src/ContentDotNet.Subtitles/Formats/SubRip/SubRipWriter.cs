namespace ContentDotNet.Subtitles.Formats.SubRip
{
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    ///   SubRip subtitle writer
    /// </summary>
    public class SubRipWriter : ISubtitleWriter
    {
        private int _activeIndex;

        public StreamWriter Writer { get; }

        public SubRipWriter(StreamWriter writer, ISubtitleTimeFormatter formatter)
        {
            Writer = writer;
            Formatter = formatter;
        }

        public ISubtitleTimeFormatter Formatter { get; set; } = SubRipSubtitleTimeFormatter.Instance;

        // Note: SubRip doesn't support styling.

        public void WriteLine(ISubtitleLine line)
        {
            Writer.WriteLine(_activeIndex++.ToString());
            Writer.WriteLine($"{Formatter.Format(line.Start)} --> {Formatter.Format(line.End)}");
            Writer.WriteLine(line.Text);
            Writer.WriteLine();
        }

        public async Task WriteLineAsync(ISubtitleLine line)
        {
            await Writer.WriteLineAsync(_activeIndex++.ToString());
            await Writer.WriteLineAsync($"{Formatter.Format(line.Start)} --> {Formatter.Format(line.End)}");
            await Writer.WriteLineAsync(line.Text);
            await Writer.WriteLineAsync();
        }
    }
}
