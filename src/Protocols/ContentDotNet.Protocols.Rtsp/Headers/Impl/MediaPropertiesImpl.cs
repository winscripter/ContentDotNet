namespace ContentDotNet.Protocols.Rtsp.Headers.Impl
{
    using ContentDotNet.Protocols.Rtsp.Headers.Impl.Base;
    using System.Text;

    internal class MediaPropertiesImpl : MediaPropertiesBase, IRtspMediaPropertiesHeader
    {
        public override string Text => "Media-Properties";

        public double? RandomAccess { get; set; }
        public bool BeginningOnly { get; set; }
        public bool NoSeeking { get; set; }
        public bool Immutable { get; set; }
        public bool Dynamic { get; set; }
        public bool TimeProgressing { get; set; }
        public bool Unlimited { get; set; }
        public bool TimeLimited { get; set; }
        public bool TimeDuration { get; set; }
        public string? Scales { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            bool applied = false;

            if (RandomAccess != null)
            {
                sb.Append($"Random-Access={RandomAccess}");
                applied = true;
            }
            if (BeginningOnly)
            {
                if (applied)
                {
                    sb.Append(", ");
                }
                sb.Append("Beginning-Only=1");
                applied = true;
            }
            if (NoSeeking)
            {
                if (applied)
                {
                    sb.Append(", ");
                }
                sb.Append("No-Seeking=1");
                applied = true;
            }
            if (Immutable)
            {
                if (applied)
                {
                    sb.Append(", ");
                }
                sb.Append("Immutable=1");
                applied = true;
            }
            if (Dynamic)
            {
                if (applied)
                {
                    sb.Append(", ");
                }
                sb.Append("Dynamic=1");
                applied = true;
            }
            if (TimeProgressing)
            {
                if (applied)
                {
                    sb.Append(", ");
                }
                sb.Append("Time-Progressing=1");
                applied = true;
            }
            if (Unlimited)
            {
                if (applied)
                {
                    sb.Append(", ");
                }
                sb.Append("Unlimited=1");
                applied = true;
            }
            if (TimeLimited)
            {
                if (applied)
                {
                    sb.Append(", ");
                }
                sb.Append("Time-Limited=1");
                applied = true;
            }
            if (TimeDuration)
            {
                if (applied)
                {
                    sb.Append(", ");
                }
                sb.Append("Time-Duration=1");
                applied = true;
            }
            if (Scales != null)
            {
                if (applied)
                {
                    sb.Append(", ");
                }
                sb.Append($"Scales={Scales}");
                applied = true;
            }
            return sb.ToString();
        }
    }
}
