namespace ContentDotNet.Protocols.Rtp.Rtcp
{
    /// <summary>
    ///   Type of RTCP packet.
    /// </summary>
    public enum RtcpPacketType
    {
        SR = 200,
        RR,
        Sdes,
        Bye,
        ApplicationDefined
    }
}
