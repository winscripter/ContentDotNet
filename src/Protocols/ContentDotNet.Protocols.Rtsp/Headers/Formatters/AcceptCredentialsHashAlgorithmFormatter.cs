namespace ContentDotNet.Protocols.Rtsp.Headers.Formatters
{
    using ContentDotNet.Protocols.Rtsp.Headers.Enumerations;

    /// <summary>
    ///   Formatter for the Accept-Credentials hash algorithm.
    /// </summary>
    public class AcceptCredentialsHashAlgorithmFormatter : RtspHeaderFormatter
    {
        public override string Format<T>(T value)
        {
            if (value is AcceptCredentialsHashAlgorithm alg)
            {
                if (alg == AcceptCredentialsHashAlgorithm.SHA256) return "sha-256";
                throw new NotImplementedException();
            }
            throw new InvalidOperationException("This method accepts AcceptCredentialsHashAlgorithm");
        }

        public override T Parse<T>(string value)
        {
            if (typeof(T) != typeof(AcceptCredentialsHashAlgorithm))
                throw new InvalidOperationException("This method accepts AcceptCredentialshashAlgorithm");

            if (value == "sha-256") return (T)(object)AcceptCredentialsHashAlgorithm.SHA256;
            throw new InvalidOperationException("Invalid string");
        }
    }
}
