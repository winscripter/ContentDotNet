using ContentDotNet.BitStream;
using ContentDotNet.Extensions.H264.Cabac;

namespace ContentDotNet.Extensions.H264.Tests.CABAC;

public class BinReaderWriterTests
{
    [Fact]
    public void SingleBinRW_PSlice_ShouldNotThrow()
    {
        const int CtxIdx1 = 70;
        const int CtxIdx2 = 55;

        using var reader = UseArithmeticWriter(
            writer =>
            {
                var symbol = new CabacContext(CtxIdx1, 0, true, false, 26);
                CabacBinarizationEncoder.EncodeMbType(writer, ref symbol, 1, GeneralSliceType.P);
                CabacBinarizationEncoder.EncodeMbType(writer, ref symbol, 2, GeneralSliceType.P);
                CabacBinarizationEncoder.EncodeMbType(writer, ref symbol, 3, GeneralSliceType.P);
                CabacBinarizationEncoder.EncodeMbType(writer, ref symbol, 6, GeneralSliceType.P);

                symbol = new CabacContext(CtxIdx2, 0, true, false, 26);
                CabacBinarizationEncoder.EncodeUnary(writer, ref symbol, 150);

                writer.BaseWriter.WriteBits(0, 32);
            });

        var binReader = new ArithmeticDecoder(reader);
        var symbols1 = new CabacContext(CtxIdx1, 0, true, false, 26);
        var symbols2 = new CabacContext(CtxIdx2, 0, true, false, 26);

        Assert.Equal(1, CabacBinarization.BinarizeMacroblockOrSubMacroblockType(binReader, ref symbols1, false, false, true, false));
        Assert.Equal(2, CabacBinarization.BinarizeMacroblockOrSubMacroblockType(binReader, ref symbols1, false, false, true, false));
        Assert.Equal(3, CabacBinarization.BinarizeMacroblockOrSubMacroblockType(binReader, ref symbols1, false, false, true, false));
        Assert.Equal(6, CabacBinarization.BinarizeMacroblockOrSubMacroblockType(binReader, ref symbols1, false, false, true, false));

        Assert.Equal(150, CabacBinarization.UnaryBinarize(binReader, ref symbols2));
    }

    private static BitStreamReader UseArithmeticWriter(
        Action<ArithmeticEncoder> enc)
    {
        using var ms = new MemoryStream();
        using var bsw = new BitStreamWriter(ms);
        var arithEnc = new ArithmeticEncoder(bsw);
        enc(arithEnc);

        bsw.GoToStart();

        byte[] result = ms.ToArray();
        var newMs = new MemoryStream(result);
        return new BitStreamReader(newMs);
    }
}
