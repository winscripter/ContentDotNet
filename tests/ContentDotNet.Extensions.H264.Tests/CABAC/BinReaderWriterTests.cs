using ContentDotNet.BitStream;
using ContentDotNet.Extensions.H264.Cabac;
using ContentDotNet.Primitives;

namespace ContentDotNet.Extensions.H264.Tests.CABAC;

public class BinReaderWriterTests
{
    //[Fact]
    //public void SingleBinRW_PSlice_ShouldNotThrow()
    //{
    //    const int CtxIdx1 = 70;
    //    const int CtxIdx2 = 55;

    //    using var reader = UseArithmeticWriter(
    //        writer =>
    //        {
    //            var symbol = new CabacContext(CtxIdx1, 0, true, false, 26);
    //            CabacBinarizationEncoder.EncodeMbType(writer, ref symbol, 1, GeneralSliceType.P);
    //            symbol = new CabacContext(CtxIdx1, 0, true, false, 26);
    //            CabacBinarizationEncoder.EncodeMbType(writer, ref symbol, 2, GeneralSliceType.P);
    //            symbol = new CabacContext(CtxIdx1, 0, true, false, 26);
    //            CabacBinarizationEncoder.EncodeMbType(writer, ref symbol, 3, GeneralSliceType.P);
    //            symbol = new CabacContext(CtxIdx1, 0, true, false, 26);
    //            CabacBinarizationEncoder.EncodeMbType(writer, ref symbol, 6, GeneralSliceType.P);

    //            symbol = new CabacContext(CtxIdx2, 0, true, false, 26);
    //            CabacBinarizationEncoder.EncodeUnary(writer, ref symbol, 150);

    //            writer.BaseWriter.WriteBits(0, 32);
    //        });

    //    var binReader = new ArithmeticDecoder(reader);
    //    var symbols1 = new CabacContext(CtxIdx1, 0, true, false, 26);
    //    var symbols2 = new CabacContext(CtxIdx2, 0, true, false, 26);

    //    Assert.Equal(1, CabacBinarization.BinarizeMacroblockOrSubMacroblockType(binReader, ref symbols1, false, false, true, false));
    //    symbols1 = new CabacContext(CtxIdx1, 0, true, false, 26);
    //    Assert.Equal(2, CabacBinarization.BinarizeMacroblockOrSubMacroblockType(binReader, ref symbols1, false, false, true, false));
    //    symbols1 = new CabacContext(CtxIdx1, 0, true, false, 26);
    //    Assert.Equal(3, CabacBinarization.BinarizeMacroblockOrSubMacroblockType(binReader, ref symbols1, false, false, true, false));
    //    symbols1 = new CabacContext(CtxIdx1, 0, true, false, 26);
    //    Assert.Equal(6, CabacBinarization.BinarizeMacroblockOrSubMacroblockType(binReader, ref symbols1, false, false, true, false));

    //    Assert.Equal(150, CabacBinarization.UnaryBinarize(binReader, ref symbols2));
    //}

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

    private static BitStreamReader UseWriter(
        Action<BitStreamWriter> enc)
    {
        using var ms = new MemoryStream();
        using var bsw = new BitStreamWriter(ms);
        enc(bsw);

        bsw.GoToStart();

        byte[] result = ms.ToArray();
        var newMs = new MemoryStream(result);
        return new BitStreamReader(newMs);
    }

    [Fact]
    public void TestBins()
    {
        using var bsr = UseArithmeticWriter(
            writer =>
            {
                WriterState prevState = writer.BaseWriter.GetState();
                writer.BaseWriter.WriteBits(0, 9);
                var cabac = new CabacContext(0, 0, false, false, 0);
                for (int i = 0; i < 10000; i++)
                    writer.WriteBin(ref cabac, i % 2 == 0);
                writer.EncodeTerminate(true);
                writer.BaseWriter.GoTo(prevState);
                writer.BaseWriter.WriteBits(writer.CodILow, 9);
            });

        var cabac = new CabacContext(0, 0, false, false, 0);
        var dec = new ArithmeticDecoder(bsr);
        for (int i = 0; i < 10000; i++)
        {
            bool expected = i % 2 == 0;
            bool actual = dec.ReadBin(ref cabac);
            if (expected != actual)
                throw new InvalidOperationException($"Expected {expected} got {actual}. Index: {i}");
        }
    }

    ///// <summary>
    /////   Tests CABAC values.
    ///// </summary>
    //[Fact]
    //public void TestCabacValues()
    //{
    //    var reader = UseWriter(
    //        enc =>
    //        {
    //            var cabac = new CabacWriter(enc, MacroblockUtilityBase.Dummy)
    //            {
    //                SliceType = GeneralSliceType.P
    //            };

    //            cabac.WriteMbType(5);
    //            for (int i = 0; i < 8; i++)
    //            {
    //                cabac.WritePrevIntraNxNPredModeFlag(i % 2 == 0 ? 1 : 0);
    //                cabac.WriteRemIntraNxNPredMode(i + 1);
    //            }
    //        });

    //    var cabac = new CabacReader(reader, MacroblockUtilityBase.Dummy)
    //    {
    //        SliceType = GeneralSliceType.P
    //    };

    //    Assert.Equal(5, cabac.ParseMbType());
    //    for (int i = 0; i < 8; i++)
    //    {
    //        Assert.Equal(i % 2 == 0 ? 0 : 1, cabac.ParsePrevIntraNxNPredModeFlag());
    //        Assert.Equal(i + 1, cabac.ParseRemIntraNxNPredMode());
    //    }
    //}
}
