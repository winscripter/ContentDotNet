namespace ContentDotNet.Protocols.Rtsp
{
    /// <summary>
    ///   Factory for RTSP client-to-server messages.
    /// </summary>
    public static class RtspClientMessageFactory
    {
        private static RtspClientMessage CreateClientMessage(RtspMethodType methodType, string parameter)
        {
            return new()
            {
                RequestLine = new()
                {
                    MethodType = methodType,
                    Parameter = parameter
                }
            };
        }

        /// <summary>
        ///   Creates an OPTIONS RTSP client message.
        /// </summary>
        /// <param name="parameter">What comes after the OPTIONS line.</param>
        /// <returns>A new instance of the <see cref="RtspClientMessage"/> class.</returns>
        public static RtspClientMessage Options(string parameter) => CreateClientMessage(RtspMethodType.Options, parameter);

        /// <summary>
        ///   Creates a Describe RTSP client message.
        /// </summary>
        /// <param name="parameter">What comes after the DESCRIBE line.</param>
        /// <returns>A new instance of the <see cref="RtspClientMessage"/> class.</returns>
        public static RtspClientMessage Describe(string parameter) => CreateClientMessage(RtspMethodType.Describe, parameter);

        /// <summary>
        ///   Creates an ANNOUNCE RTSP client message.
        /// </summary>
        /// <param name="parameter">What comes after the ANNOUNCE line.</param>
        /// <returns>A new instance of the <see cref="RtspClientMessage"/> class.</returns>
        public static RtspClientMessage Announce(string parameter) => CreateClientMessage(RtspMethodType.Announce, parameter);

        /// <summary>
        ///   Creates a SETUP RTSP client message.
        /// </summary>
        /// <param name="parameter">What comes after the SETUP line.</param>
        /// <returns>A new instance of the <see cref="RtspClientMessage"/> class.</returns>
        public static RtspClientMessage Setup(string parameter) => CreateClientMessage(RtspMethodType.Setup, parameter);

        /// <summary>
        ///   Creates a PLAY RTSP client message.
        /// </summary>
        /// <param name="parameter">What comes after the PLAY line.</param>
        /// <returns>A new instance of the <see cref="RtspClientMessage"/> class.</returns>
        public static RtspClientMessage Play(string parameter) => CreateClientMessage(RtspMethodType.Play, parameter);

        /// <summary>
        ///   Creates a PAUSE RTSP client message.
        /// </summary>
        /// <param name="parameter">What comes after the PAUSE line.</param>
        /// <returns>A new instance of the <see cref="RtspClientMessage"/> class.</returns>
        public static RtspClientMessage Pause(string parameter) => CreateClientMessage(RtspMethodType.Pause, parameter);

        /// <summary>
        ///   Creates an TEARDOWN RTSP client message.
        /// </summary>
        /// <param name="parameter">What comes after the TEARDOWN line.</param>
        /// <returns>A new instance of the <see cref="RtspClientMessage"/> class.</returns>
        public static RtspClientMessage Teardown(string parameter) => CreateClientMessage(RtspMethodType.Teardown, parameter);

        /// <summary>
        ///   Creates a GET_PARAMETER RTSP client message.
        /// </summary>
        /// <param name="parameter">What comes after the GET_PARAMETER line.</param>
        /// <returns>A new instance of the <see cref="RtspClientMessage"/> class.</returns>
        public static RtspClientMessage GetParameter(string parameter) => CreateClientMessage(RtspMethodType.GetParameter, parameter);

        /// <summary>
        ///   Creates a SET_PARAMETER RTSP client message.
        /// </summary>
        /// <param name="parameter">What comes after the SET_PARAMETER line.</param>
        /// <returns>A new instance of the <see cref="RtspClientMessage"/> class.</returns>
        public static RtspClientMessage SetParameter(string parameter) => CreateClientMessage(RtspMethodType.SetParameter, parameter);

        /// <summary>
        ///   Creates a REDIRECT RTSP client message.
        /// </summary>
        /// <param name="parameter">What comes after the REDIRECT line.</param>
        /// <returns>A new instance of the <see cref="RtspClientMessage"/> class.</returns>
        public static RtspClientMessage Redirect(string parameter) => CreateClientMessage(RtspMethodType.Redirect, parameter);

        /// <summary>
        ///   Creates an RECORD RTSP client message.
        /// </summary>
        /// <param name="parameter">What comes after the RECORD line.</param>
        /// <returns>A new instance of the <see cref="RtspClientMessage"/> class.</returns>
        public static RtspClientMessage Record(string parameter) => CreateClientMessage(RtspMethodType.Record, parameter);
    }
}
