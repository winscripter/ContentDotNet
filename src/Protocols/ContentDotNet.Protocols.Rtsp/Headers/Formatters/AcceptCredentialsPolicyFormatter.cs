namespace ContentDotNet.Protocols.Rtsp.Headers.Formatters
{
    using ContentDotNet.Protocols.Rtsp.Headers.Enumerations;

    /// <summary>
    ///   Formatter for the Accept-Credentials policy.
    /// </summary>
    public class AcceptCredentialsPolicyFormatter : RtspHeaderFormatter
    {
        public override string Format<T>(T value)
        {
            if (value is AcceptCredentialsPolicy pol)
            {
                return pol.ToString().ToLower();
            }

            throw new InvalidOperationException("This method accepts AcceptCredentialsPolicy");
        }

        public override T Parse<T>(string value)
        {
            if (typeof(T) != typeof(AcceptCredentialsPolicy))
                throw new InvalidOperationException("This method accepts AcceptCredentialsPolicy");

            if (value == "User") return (T)(object)AcceptCredentialsPolicy.User;
            if (value == "Any") return (T)(object)AcceptCredentialsPolicy.Any;
            if (value == "Proxy") return (T)(object)AcceptCredentialsPolicy.Proxy;

            throw new InvalidOperationException("Invalid input-string");
        }
    }
}
