namespace ContentDotNet.Image.Formats.Jpeg.Components
{
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Runtime.Intrinsics;
    using System.Runtime.Intrinsics.X86;

    internal static class JpegMath
    {
        /// <summary>
        ///   Dimensions and sampling factors (Page 28)
        /// </summary>
        /// <param name="xOrY"></param>
        /// <param name="hOrV"></param>
        /// <param name="hMaxOrVMax"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DimensionAndSamplingFactor(float xOrY, float hOrV, float hMaxOrVMax)
        {
            return xOrY * (hOrV / hMaxOrVMax);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int DctLevelShift(int P)
        {
            return (int)MathF.Pow(2, P - 1);
        }

        public static void ComputeDimensionsAndScalingFactors(Span<float> source, Span<float> output, float value /* i.e. X or Y */)
        {
            int max = source.Length;
            //if (Avx.IsSupported && output.Length % Vector256<float>.Count == 0)
            //{
            //    ReadOnlySpan<Vector256<float>> srcV = MemoryMarshal.Cast<float, Vector256<float>>(source);
            //    Vector256<float> maxV = Vector256.Create((float)max);
            //    Vector256<float> valueV = Vector256.Create(value);
            //    ref float reference = ref MemoryMarshal.GetReference(output);
            //    for (int i = 0; i < srcV.Length; i++)
            //    {
            //        Vector256<float> intermediate = Avx.Divide(srcV[i], maxV);
            //        Vector256<float> reconstructed = Avx.Multiply(intermediate, valueV);
            //        Unsafe.WriteUnaligned(
            //            ref Unsafe.As<float, byte>(ref Unsafe.Add(ref reference, i * Vector256<float>.Count)),
            //            reconstructed
            //        );
            //    }
            //}
            for (int i = 0; i < max; i++)
            {
                output[i] = value * (source[i] / max);
            }
        }

        public static float GetSqvu(float svu, float qvu) => MathF.Round(svu / qvu);
        public static float GetRvu(float sqvu, float qvu) => sqvu * qvu;

        public static float EncodeDifferentialDc(float dci, float pred) => dci - pred;


    }
}
