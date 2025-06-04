using System.Text.RegularExpressions;

namespace ContentDotNet.Rtsp.Sdp;

/// <summary>
///   Represents a base SDP packet, in a simple form of character=data.
/// </summary>
/// <param name="Character">The character before the equals character.</param>
/// <param name="Data">The data after the equals character.</param>
public sealed record SdpPacketSource(char Character, string Data)
{
    // NOTE: We can't use GeneratedRegexAttribute because we want ContentDotNet
    // to be compatible with .NET Framework too.

#pragma warning disable SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.
    private static readonly Regex PacketRegex = new(@"(?<char>[a-zA-Z])=(?<text>(\w|\d|_)+)");
#pragma warning restore SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.

    /// <summary>
    ///   Checks if the packet source can be parsed.
    /// </summary>
    /// <param name="data">Input packet source.</param>
    /// <returns>A boolean indicating whether the packet source, <paramref name="data" />, is suitable for parsing.</returns>
    public static bool CanParse(string? data)
    {
        if (data is null)
            return false;
        if (!data.Contains('='))
            return false;

        return PacketRegex.IsMatch(data);
    }

    /// <summary>
    ///   Parses the SDP packet source.
    /// </summary>
    /// <param name="data">SDP packet source.</param>
    /// <returns>The parsed SDP packet or <see langword="null"/> if it's invalid.</returns>
    public static SdpPacketSource? Parse(string? data)
    {
        if (!CanParse(data)) return null;

        Match parsed = PacketRegex.Match(data!);
        return new SdpPacketSource(parsed.Groups["char"].Value[0], parsed.Groups["text"].Value);
    }
}
