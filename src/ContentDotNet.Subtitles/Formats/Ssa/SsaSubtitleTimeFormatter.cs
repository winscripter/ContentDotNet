namespace ContentDotNet.Subtitles.Formats.Ssa
{
    using System.Text;

    internal class SsaSubtitleTimeFormatter : ISubtitleTimeFormatter
    {
        public static readonly SsaSubtitleTimeFormatter Instance = new();

        // Example input of an SSA time span:
        // 00:02:16.612
        // which is:
        // "2 minutes 16 seconds 612 milliseconds"

        public string Format(TimeSpan input)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(input.Hours.ToString().PadLeft(2, '0'));
            stringBuilder.Append(':');
            stringBuilder.Append(input.Minutes.ToString().PadLeft(2, '0'));
            stringBuilder.Append(':');
            stringBuilder.Append(input.Seconds.ToString().PadLeft(2, '0'));
            stringBuilder.Append('.');
            stringBuilder.Append(input.Milliseconds.ToString().PadLeft(3, '0'));

            return stringBuilder.ToString();
        }

        public TimeSpan Parse(string input)
        {
            string[] split = input.Split(':');
            if (split.Length != 3)
                throw new FormatException("Could not parse SubRip subtitle");

            var ts = new TimeSpan();
            if (int.TryParse(split[0], out int hours)) ts += TimeSpan.FromHours(hours);
            if (int.TryParse(split[1], out int minutes)) ts += TimeSpan.FromMinutes(minutes);

            string[] commaSplit = split[2].Split('.');
            if (commaSplit.Length != 2)
                throw new FormatException("Could not parse SubRip subtitle");

            if (int.TryParse(commaSplit[0], out int seconds)) ts += TimeSpan.FromSeconds(seconds);
            if (int.TryParse(commaSplit[1], out int ms)) ts += TimeSpan.FromMilliseconds(ms);

            return ts;
        }
    }
}
