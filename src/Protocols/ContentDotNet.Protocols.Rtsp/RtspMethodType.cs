namespace ContentDotNet.Protocols.Rtsp
{
    /// <summary>
    ///   Type of RTSP method.
    /// </summary>
    public enum RtspMethodType
    {
        Options,
        Describe,
        Announce,
        Setup,
        Play,
        Pause,
        Teardown,
        Get_parameter,
        Set_parameter,
        Redirect,
        Record
    }
}
