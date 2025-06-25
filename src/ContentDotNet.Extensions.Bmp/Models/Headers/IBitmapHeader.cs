namespace ContentDotNet.Extensions.Bmp.Models.Headers;

/// <summary>
///   Represents a bitmap header.
/// </summary>
public interface IBitmapHeader
{
}

/// <summary>
///   Generic bitmap header.
/// </summary>
/// <typeparam name="TSelf">The structure representing themselves.</typeparam>
public interface IBitmapHeader<TSelf> : IBitmapHeader, IEquatable<TSelf>
    where TSelf : unmanaged
{
}
