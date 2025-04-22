using ContentDotNet.Abstractions;

namespace ContentDotNet.Extensions.H264.Internal.Decoding;

/// <summary>
///   Chroma subsampling and size.
/// </summary>
/// <param name="ChromaSubsampling">Chroma subsampling</param>
/// <param name="SubWidthC">SubWidthC</param>
/// <param name="SubHeightC">SubHeightC</param>
internal readonly record struct ChromaSubsamplingAndSize(ChromaSubsampling ChromaSubsampling, int SubWidthC, int SubHeightC);
