using ContentDotNet.Extensions.H264.Cabac.Internal;
using ContentDotNet.Extensions.H264.Models;

namespace ContentDotNet.Extensions.H264.Cabac;

/// <summary>
/// Provides static methods for CABAC binarization operations in H.264 decoding.
/// </summary>
public static class CabacBinarization
{
    /// <summary>
    /// Performs unary binarization using the specified arithmetic decoder and CABAC context.
    /// </summary>
    /// <param name="dec">The arithmetic decoder.</param>
    /// <param name="ctx">The CABAC context.</param>
    /// <returns>The decoded value.</returns>
    public static int UnaryBinarize(ArithmeticDecoder dec, ref CabacContext ctx)
        => Binarization.UnaryBinarize(dec, ref ctx);

    /// <summary>
    /// Performs truncated unary binarization with a maximum value using the specified arithmetic decoder and CABAC context.
    /// </summary>
    /// <param name="dec">The arithmetic decoder.</param>
    /// <param name="ctx">The CABAC context.</param>
    /// <param name="cMax">The maximum value for truncation.</param>
    /// <returns>The decoded value.</returns>
    public static int TruncatedUnaryBinarize(ArithmeticDecoder dec, ref CabacContext ctx, int cMax)
        => Binarization.TruncatedUnaryBinarize(dec, ref ctx, cMax);

    /// <summary>
    /// Performs UEGk binarization using the specified arithmetic decoder and CABAC context.
    /// </summary>
    /// <param name="dec">The arithmetic decoder.</param>
    /// <param name="ctx">The CABAC context.</param>
    /// <param name="signedValFlag">Indicates if the value is signed.</param>
    /// <param name="k">The k parameter for UEGk binarization.</param>
    /// <param name="uCoff">The coefficient for UEGk binarization.</param>
    /// <returns>The decoded value.</returns>
    public static int UegkBinarize(ArithmeticDecoder dec, ref CabacContext ctx, bool signedValFlag, int k, int uCoff)
        => Binarization.UegkBinarize(dec, ref ctx, signedValFlag, k, uCoff);

    /// <summary>
    /// Performs fixed-length binarization using the specified arithmetic decoder and CABAC context.
    /// </summary>
    /// <param name="dec">The arithmetic decoder.</param>
    /// <param name="ctx">The CABAC context.</param>
    /// <param name="cMax">The maximum value for the fixed length.</param>
    /// <returns>The decoded value.</returns>
    public static int FixedLengthBinarize(ArithmeticDecoder dec, ref CabacContext ctx, int cMax)
        => Binarization.FixedLengthBinarize(dec, ref ctx, cMax);

    /// <summary>
    /// Binarizes the macroblock or sub-macroblock type using the specified parameters.
    /// </summary>
    /// <param name="dec">The arithmetic decoder.</param>
    /// <param name="ctx">The CABAC context.</param>
    /// <param name="isISlice">Indicates if the current slice is an I slice.</param>
    /// <param name="isBSlice">Indicates if the current slice is a B slice.</param>
    /// <param name="isPorSPSlice">Indicates if the current slice is a P or SP slice.</param>
    /// <param name="isSubMbType">Indicates if the type is a sub-macroblock type.</param>
    /// <returns>The decoded macroblock or sub-macroblock type.</returns>
    public static int BinarizeMacroblockOrSubMacroblockType(ArithmeticDecoder dec, ref CabacContext ctx, bool isISlice, bool isBSlice, bool isPorSPSlice, bool isSubMbType)
        => Binarization.BinarizeMacroblockOrSubMacroblockType(dec, ref ctx, isISlice, isBSlice, isPorSPSlice, isSubMbType);

    /// <summary>
    /// Binarizes the coded block pattern using the specified arithmetic decoder, CABAC context, and chroma array type.
    /// </summary>
    /// <param name="dec">The arithmetic decoder.</param>
    /// <param name="ctx">The CABAC context.</param>
    /// <param name="chromaArrayType">The chroma array type.</param>
    /// <returns>The decoded coded block pattern.</returns>
    public static int BinarizeCodedBlockPattern(ArithmeticDecoder dec, ref CabacContext ctx, int chromaArrayType)
        => Binarization.BinarizeCodedBlockPattern(dec, ref ctx, chromaArrayType);

    /// <summary>
    /// Binarizes the macroblock QP delta using the specified arithmetic decoder and CABAC context.
    /// </summary>
    /// <param name="dec">The arithmetic decoder.</param>
    /// <param name="ctx">The CABAC context.</param>
    /// <returns>The decoded macroblock QP delta.</returns>
    public static int BinarizeMbQpDelta(ArithmeticDecoder dec, ref CabacContext ctx)
        => Binarization.BinarizeMbQpDelta(dec, ref ctx);

    /// <summary>
    /// Gets binarization fields for the specified syntax element and context.
    /// </summary>
    /// <param name="element">The syntax element.</param>
    /// <param name="sliceType">The slice type.</param>
    /// <param name="residualBlkType">The residual block type.</param>
    /// <param name="NumC8x8">The number of 8x8 chroma blocks.</param>
    /// <param name="isFrameMacroblock">Indicates if the macroblock is a frame macroblock.</param>
    /// <returns>A tuple containing the maximum bin index context, context index offset, and bypass flag.</returns>
    public static (int maxBinIdxCtx, int ctxIdxOffset, bool bypassFlag) GetFields(SyntaxElement element, GeneralSliceType sliceType, ResidualBlockType residualBlkType, int NumC8x8, bool isFrameMacroblock)
        => Binarization.GetFields(element, sliceType, residualBlkType, NumC8x8, isFrameMacroblock);

    /// <summary>
    /// Binarizes a syntax element using the specified arithmetic decoder, CABAC context, slice type, and chroma array type.
    /// </summary>
    /// <param name="dec">The arithmetic decoder.</param>
    /// <param name="ctx">The CABAC context.</param>
    /// <param name="sliceType">The slice type.</param>
    /// <param name="element">The syntax element to binarize.</param>
    /// <param name="chromaArrayType">The chroma array type.</param>
    /// <returns>The decoded value for the syntax element.</returns>
    public static int Binarize(ArithmeticDecoder dec, ref CabacContext ctx, GeneralSliceType sliceType, SyntaxElement element, int chromaArrayType)
        => Binarization.Binarize(dec, ref ctx, sliceType, element, chromaArrayType);
}
