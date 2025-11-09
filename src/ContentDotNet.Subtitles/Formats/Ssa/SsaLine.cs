namespace ContentDotNet.Subtitles.Formats.Ssa
{
    using System;
    using System.Text;

    public class SsaLine : ISubtitleLine
    {
        //
        // ISubtitleLine properties
        //

        public string Text { get; set; }
        public ISubtitleStyling? Styling { get; set; }
        public TimeSpan Start
        {
            get => SsaSubtitleTimeFormatter.Instance.Parse(GetObject("Start")!);
            set => SetObject("Start", SsaSubtitleTimeFormatter.Instance.Format(value));
        }
        public TimeSpan End
        {
            get => SsaSubtitleTimeFormatter.Instance.Parse(GetObject("End")!);
            set => SetObject("End", SsaSubtitleTimeFormatter.Instance.Format(value));
        }

        public SsaLine(string text, ISubtitleStyling? styling, string? format)
        {
            Format = format;
            Text = text;
            Styling = styling;
        }

        public SsaLine(string text, ISubtitleStyling? styling, TimeSpan start, TimeSpan end, string? format)
        {
            Format = format;
            Text = text;
            Styling = styling;
            Start = start;
            End = end;
        }

        //
        // Custom properties
        //

        public string? Format { get; set; }

        //
        // Implementations
        //

        /// <summary>
        ///   The Layer value.
        /// </summary>
        public string? Layer
        {
            get => GetObject("Layer");
            set => SetObject("Layer", value);
        }

        /// <summary>
        ///   The Style value.
        /// </summary>
        public string? Style
        {
            get => GetObject("Style");
            set => SetObject("Style", value);
        }

        /// <summary>
        ///   The Name value.
        /// </summary>
        public string? Name
        {
            get => GetObject("Name");
            set => SetObject("Name", value);
        }

        /// <summary>
        ///   The MarginL value.
        /// </summary>
        public string? MarginL
        {
            get => GetObject("MarginL");
            set => SetObject("MarginL", value);
        }

        /// <summary>
        ///   The MarginR value.
        /// </summary>
        public string? MarginR
        {
            get => GetObject("MarginR");
            set => SetObject("MarginR", value);
        }

        /// <summary>
        ///   The MarginW value.
        /// </summary>
        public string? MarginV
        {
            get => GetObject("MarginV");
            set => SetObject("MarginV", value);
        }

        /// <summary>
        ///   The Effect value.
        /// </summary>
        public string? Effect
        {
            get => GetObject("Effect");
            set => SetObject("Effect", value);
        }

        /// <summary>
        ///   The Text value.
        /// </summary>
        public string? LineText
        {
            get => GetObject("Text");
            set => SetObject("Text", value);
        }

        private string? GetObject(string name)
        {
            string[] split = Format!.Split(',');
            int index = Array.IndexOf(split, name);
            if (index == split.Length - 1)
            {
                //
                // If this is the final element, the rest of the commas can be accounted
                // without acting like splitters. Example:
                //
                // Format: A,B,Text
                // Dialogue: 1,2,Some text, the comma here, is, part of Text
                //
                // Result of Text:
                //   Some text, the comma here, is, part of Text
                // Completely valid.
                //

                string[] lineSplit = Text.Split(',');
                int start = split.Length - 1;
                int end = lineSplit.Length;

                var rawText = new StringBuilder();
                for (int i = start; i < end; i++)
                {
                    rawText.Append(lineSplit[i]);
                    rawText.Append(',');
                }

                return rawText.ToString().TrimEnd(',');
            }
            else
            {
                return Text.Split(',')[index];
            }
        }

        private void SetObject(string name, string? value)
        {
            if (value == null)
            {
                return;
            }

            string[] formatSplit = Format!.Split(',');
            string[] textSplit = Text.Split(',');
            int index = Array.IndexOf(formatSplit, name);

            if (index == -1)
            {
                throw new ArgumentException($"Field '{name}' not found in Format.");
            }

            if (index == formatSplit.Length - 1)
            {
                // Last field: join all remaining parts into one string
                var newTextSplit = new List<string>();
                for (int i = 0; i < index; i++)
                {
                    newTextSplit.Add(i < textSplit.Length ? textSplit[i] : "");
                }
                newTextSplit.Add(value); // Replace the last field with the new value
                Text = string.Join(",", newTextSplit);
            }
            else
            {
                // Non-final field: update directly
                if (textSplit.Length <= index)
                {
                    Array.Resize(ref textSplit, index + 1);
                }
                textSplit[index] = value;
                Text = string.Join(",", textSplit);
            }
        }
    }
}
