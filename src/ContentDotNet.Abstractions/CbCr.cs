namespace ContentDotNet.Abstractions;

/// <summary>  
/// Represents a chroma component with Cb (blue-difference) and Cr (red-difference) values.  
/// </summary>  
/// <param name="Cb">The blue-difference chroma component.</param>  
/// <param name="Cr">The red-difference chroma component.</param>  
public readonly record struct CbCr(byte Cb, byte Cr);
