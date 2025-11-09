namespace ContentDotNet.Subtitles.Formats.SubRip
{
    using ContentDotNet.Api;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    ///   SubRip subtitle reader
    /// </summary>
    public class SubRipReader : ISubtitleReader
    {
        private const int MessageLinesLimit = 10000; // To prevent DDoS attacks

        public StreamReader Reader { get; }

        public SubRipReader(StreamReader reader) => Reader = reader;

        public ISubtitleTimeFormatter Formatter { get; set; } = SubRipSubtitleTimeFormatter.Instance;

        private string? SkipEmptyLines()
        {
            string? last;
            while ((last = Reader.ReadLine()) != null)
            {
                if (!string.IsNullOrWhiteSpace(last))
                    break;
            }
            return last;
        }

        private async Task<string?> SkipEmptyLinesAsync()
        {
            string? last;
            while ((last = await Reader.ReadLineAsync()) != null)
            {
                if (!string.IsNullOrWhiteSpace(last))
                    break;
            }
            return last;
        }

        public ISubtitleLine? ReadNextLine()
        {
            string? lastLine = this.SkipEmptyLines();
            if (lastLine == null)
            {
                return null;
            }

            // Skip over a line that's numbers only
            if (int.TryParse(lastLine, out _))
            {
                lastLine = this.Reader.ReadLine();
            }
            else
            {
                this.Reader.ReadLine();
                return null;
            }

            if (lastLine == null)
            {
                return null;
            }

            // Now we must be in the time span range zone
            string[] splitWithArrow = lastLine.Split("-->");
            TimeSpan start = this.Formatter.Parse(splitWithArrow[0].Trim());
            TimeSpan end = this.Formatter.Parse(splitWithArrow[1].Trim());

            var messageData = new StringBuilder();
            int i = 0;
            do
            {
                lastLine = this.Reader.ReadLine();

                // If we're reaching past the end of the stream,
                // or the current line is blank, that's the end of the line.
                if (lastLine == null || string.IsNullOrWhiteSpace(lastLine) || string.IsNullOrEmpty(lastLine))
                {
                    break;
                }

                messageData.AppendLine(lastLine);
            }
            while (i++ < MessageLinesLimit);

            // Prevent potential DDoS attacks. ContentDotNet shall be secure.
            if (i >= MessageLinesLimit)
            {
                throw new InvalidOperationException("The line is too large to process");
            }

            return new GenericLine(
                messageData.ToString(),
                new GenericStyling(Color.Black, Color.White, TextDecoration.None),
                start, end);
        }

        public async Task<ISubtitleLine?> ReadNextLineAsync()
        {
            string? lastLine = await this.SkipEmptyLinesAsync();
            if (lastLine == null)
            {
                return null;
            }

            // Skip over a line that's numbers only
            if (int.TryParse(lastLine, out _))
            {
                lastLine = this.Reader.ReadLine();
            }
            else
            {
                await this.Reader.ReadLineAsync();
                return null;
            }

            if (lastLine == null)
            {
                return null;
            }

            // Now we must be in the time span range zone
            string[] splitWithArrow = lastLine.Split("-->");
            TimeSpan start = this.Formatter.Parse(splitWithArrow[0].Trim());
            TimeSpan end = this.Formatter.Parse(splitWithArrow[1].Trim());

            var messageData = new StringBuilder();
            int i = 0;
            do
            {
                lastLine = await this.Reader.ReadLineAsync();

                // If we're reaching past the end of the stream,
                // or the current line is blank, that's the end of the line.
                if (lastLine == null || string.IsNullOrWhiteSpace(lastLine) || string.IsNullOrEmpty(lastLine))
                {
                    break;
                }

                messageData.AppendLine(lastLine);
            }
            while (i++ < MessageLinesLimit);

            // Prevent potential DDoS attacks. ContentDotNet shall be secure.
            if (i >= MessageLinesLimit)
            {
                throw new InvalidOperationException("The line is too large to process");
            }

            return new GenericLine(
                messageData.ToString(),
                new GenericStyling(Color.Black, Color.White, TextDecoration.None),
                start, end);
        }
    }
}
