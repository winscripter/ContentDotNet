namespace ContentDotNet.Subtitles.Formats.WebVtt
{
    using System;
    using System.Globalization;

    internal class WebVttSubtitleTimeFormatter : ISubtitleTimeFormatter
    {
        private static readonly string[] s_webVTTtimeFormats = [@"hh\:mm\:ss\.fff", @"mm\:ss\.fff"];

        private const string WithHoursFormat = @"hh\:mm\:ss\.fff";
        private const string WithMinutesFormat = @"mm\:ss\.fff";

        public static readonly WebVttSubtitleTimeFormatter Instance = new();

        public string Format(TimeSpan input)
        {
            return input.ToString(input.Hours > 0 ? WithHoursFormat : WithMinutesFormat);
        }

        public TimeSpan Parse(string input)
        {
            foreach (var format in s_webVTTtimeFormats)
            {
                if (TimeSpan.TryParseExact(input, format, CultureInfo.InvariantCulture, out TimeSpan result))
                {
                    return result;
                }
            }
            throw new FormatException("Invalid WebVTT time format.");
        }
    }
}
