namespace ContentDotNet.Subtitles.Formats.Ssa
{
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    ///   SSA file reader
    /// </summary>
    public class SsaReader : ISubtitleReader
    {
        private bool _initialized = false;

        public StreamReader Reader { get; }
        public string? V4StylesFormat { get; set; }
        public List<string> V4Styles { get; set; } = [];
        public SsaScriptInfoSection? ScriptInfo { get; set; } = new();
        public string? EventsFormat { get; set; }

        public SsaReader(StreamReader reader)
        {
            Reader = reader;
        }

        private void Initialize()
        {
            if (!_initialized)
            {
                _initialized = true;
                string? line;
                while ((line = Reader.ReadLine()) != null && line.Trim() != "[Events]")
                {
                    if (line == "[ScriptInfo]")
                    {
                        while ((line = Reader.ReadLine()) != null && !string.IsNullOrWhiteSpace(line))
                        {
                            string? afterColon = line.Split(":")[1];
                            if (line.StartsWith("Title")) ScriptInfo!.Title = afterColon;
                            else if (line.StartsWith("ScriptType")) ScriptInfo!.ScriptType = afterColon;
                            else if (line.StartsWith("WrapStyle")) ScriptInfo!.WrapStyle = int.Parse(afterColon);
                            else if (line.StartsWith("PlayResX")) ScriptInfo!.PlayResX = int.Parse(afterColon);
                            else if (line.StartsWith("PlayResY")) ScriptInfo!.PlayResY = int.Parse(afterColon);
                            else if (line.StartsWith("ScaledBorderAndShadow")) ScriptInfo!.ScaledBorderAndShadow = afterColon == "yes";
                        }
                        if (line == null)
                            break;
                    }
                    else if (line == "[V4+ Styles]")
                    {
                        line = Reader.ReadLine();
                        if (line == null)
                            break;

                        V4StylesFormat = line;

                        while ((line = Reader.ReadLine()) != null && !string.IsNullOrWhiteSpace(line))
                        {
                            V4Styles.Add(line.Split("Dialogue:")[1].Trim());
                        }

                        if (line == null)
                            break;
                    }
                }
                if (line == null)
                    return;
                EventsFormat = Reader.ReadLine()!.Split("Format:")[1].Trim();
            }
        }

        private async Task InitializeAsync()
        {
            if (!_initialized)
            {
                _initialized = true;
                string? line;
                while ((line = await Reader.ReadLineAsync()) != null && line.Trim() != "[Events]")
                {
                    if (line == "[ScriptInfo]")
                    {
                        while ((line = await Reader.ReadLineAsync()) != null && !string.IsNullOrWhiteSpace(line))
                        {
                            string? afterColon = line.Split(":")[1];
                            if (line.StartsWith("Title")) ScriptInfo!.Title = afterColon;
                            else if (line.StartsWith("ScriptType")) ScriptInfo!.ScriptType = afterColon;
                            else if (line.StartsWith("WrapStyle")) ScriptInfo!.WrapStyle = int.Parse(afterColon);
                            else if (line.StartsWith("PlayResX")) ScriptInfo!.PlayResX = int.Parse(afterColon);
                            else if (line.StartsWith("PlayResY")) ScriptInfo!.PlayResY = int.Parse(afterColon);
                            else if (line.StartsWith("ScaledBorderAndShadow")) ScriptInfo!.ScaledBorderAndShadow = afterColon == "yes";
                        }
                        if (line == null)
                            break;
                    }
                    else if (line == "[V4+ Styles]")
                    {
                        line = await Reader.ReadLineAsync();
                        if (line == null)
                            break;

                        V4StylesFormat = line;

                        while ((line = await Reader.ReadLineAsync()) != null && !string.IsNullOrWhiteSpace(line))
                        {
                            V4Styles.Add(line.Split("Dialogue:")[1].Trim());
                        }

                        if (line == null)
                            break;
                    }
                }
                if (line == null)
                    return;
                EventsFormat = ((await Reader.ReadLineAsync())!).Split("Format:")[1].Trim();
            }
        }

        public ISubtitleTimeFormatter Formatter { get; set; } = SsaSubtitleTimeFormatter.Instance;

        public ISubtitleLine? ReadNextLine()
        {
            Initialize();
            string? line = Reader.ReadLine();
            if (line == null || !line.StartsWith("Dialogue:"))
                return null;
            return new SsaLine(line, null, this.EventsFormat);
        }

        public async Task<ISubtitleLine?> ReadNextLineAsync()
        {
            await InitializeAsync();
            string? line = await Reader.ReadLineAsync();
            if (line == null || !line.StartsWith("Dialogue:"))
                return null;
            return new SsaLine(line, null, this.EventsFormat);
        }
    }
}
