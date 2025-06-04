namespace ContentDotNet.Rtsp.Sdp.Packets;

/// <summary>
/// Abstract base class representing a generic SDP packet line.
/// Each line consists of a single character, '=', and a value (e.g., "v=0").
/// </summary>
public abstract class SdpPacket : IEquatable<SdpPacket>
{
    /// <summary>
    /// Gets the character identifying the SDP packet type (e.g., 'v', 'o', 's').
    /// </summary>
    public abstract char Character { get; }

    /// <summary>
    /// Gets the parsed value (data after the '=' character).
    /// </summary>
    public abstract string Value { get; }

    /// <summary>
    /// Gets the full textual SDP line (e.g., "v=0").
    /// </summary>
    public string Text => $"{Character}={Value}";

    /// <summary>
    /// Gets the total raw length of the SDP line.
    /// </summary>
    public int RawLength => Text.Length;

    /// <summary>
    /// Gets the length of the value section (everything after '=').
    /// </summary>
    public int Length => Value.Length;

    /// <summary>
    /// Indicates whether this SDP packet is the same type as another.
    /// </summary>
    public bool IsSameTypeAs(SdpPacket other) =>
        other != null && other.Character == this.Character;

    /// <summary>
    /// Validates the structure of the packet.
    /// Override in derived classes for stricter checks.
    /// </summary>
    public virtual bool IsValid() =>
        !string.IsNullOrWhiteSpace(Value);

    /// <summary>
    /// Returns a deep copy of the packet.
    /// </summary>
    public abstract SdpPacket Clone();

    /// <inheritdoc/>
    public override string ToString() => Text;

    /// <inheritdoc/>
    public bool Equals(SdpPacket? other) =>
        other != null && Character == other.Character && Value == other.Value;

    public override bool Equals(object? obj) => Equals(obj as SdpPacket);

    public override int GetHashCode() => HashCode.Combine(Character, Value);
}
