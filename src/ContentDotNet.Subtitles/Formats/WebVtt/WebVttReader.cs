namespace ContentDotNet.Subtitles.Formats.WebVtt
{
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    ///   WebVTT file reader &amp; parser.
    /// </summary>
    public class WebVttReader(StreamReader reader) : ISubtitleReader
    {
        public StreamReader Reader { get; } = reader;

        public ISubtitleTimeFormatter Formatter { get; set; } = WebVttSubtitleTimeFormatter.Instance;

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

        private (TimeSpan start, TimeSpan end) ParseArrowLine(string line)
        {
            string[] split = line.Split("-->");
            return (this.Formatter.Parse(split[0].Trim()), this.Formatter.Parse(split[1].Trim()));
        }

        public ISubtitleLine? ReadNextLine()
        {
            string? line = SkipEmptyLines();
            if (line == null)
                return null;

            string? currLine = Reader.ReadLine();
            if (currLine == "WEBVTT")
                return ReadNextLine(); // Skip WEBVTT

            if (currLine == "STYLE")
            {
                var sb = new StringBuilder();
                while ((line = Reader.ReadLine()) != null && !string.IsNullOrWhiteSpace(line))
                    sb.AppendLine(line);
                return new WebVttCssLine(sb.ToString());
            }
            else if (line.StartsWith("/*"))
            {
                return new WebVttCommentLine(line.Split("/*")[1].Split("*/")[0]);
            }
            else if (!string.IsNullOrWhiteSpace(line))
            {
                // we got subtitle lines
                if (line.Contains("-->"))
                {
                    // without cues
                    var (start, end) = ParseArrowLine(line);
                    var sb = new StringBuilder();
                    while ((line = Reader.ReadLine()) != null && !string.IsNullOrWhiteSpace(line))
                        sb.AppendLine(line);
                    return new WebVttLine(sb.ToString(), start, end);
                }
                else
                {
                    // with cues
                    string? cue = Reader.ReadLine();
                    if (cue == null)
                        return null;
                    var (start, end) = ParseArrowLine(line);
                    var sb = new StringBuilder();
                    while ((line = Reader.ReadLine()) != null && !string.IsNullOrWhiteSpace(line))
                        sb.AppendLine(line);
                    return new WebVttLine(sb.ToString(), start, end, cue);
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<ISubtitleLine?> ReadNextLineAsync()
        {
            string? line = await SkipEmptyLinesAsync();
            if (line == null)
                return null;

            string? currLine = await Reader.ReadLineAsync();
            if (currLine == "WEBVTT")
                return await ReadNextLineAsync(); // Skip WEBVTT

            if (currLine == "STYLE")
            {
                var sb = new StringBuilder();
                while ((line = await Reader.ReadLineAsync()) != null && !string.IsNullOrWhiteSpace(line))
                    sb.AppendLine(line);
                return new WebVttCssLine(sb.ToString());
            }
            else if (line.StartsWith("/*"))
            {
                return new WebVttCommentLine(line.Split("/*")[1].Split("*/")[0]);
            }
            else if (!string.IsNullOrWhiteSpace(line))
            {
                // we got subtitle lines
                if (line.Contains("-->"))
                {
                    // without cues
                    var (start, end) = ParseArrowLine(line);
                    var sb = new StringBuilder();
                    while ((line = await Reader.ReadLineAsync()) != null && !string.IsNullOrWhiteSpace(line))
                        sb.AppendLine(line);
                    return new WebVttLine(sb.ToString(), start, end);
                }
                else
                {
                    // with cues
                    string? cue = await Reader.ReadLineAsync();
                    if (cue == null)
                        return null;
                    var (start, end) = ParseArrowLine(line);
                    var sb = new StringBuilder();
                    while ((line = await Reader.ReadLineAsync()) != null && !string.IsNullOrWhiteSpace(line))
                        sb.AppendLine(line);
                    return new WebVttLine(sb.ToString(), start, end, cue);
                }
            }
            else
            {
                return null;
            }
        }
    }
}
