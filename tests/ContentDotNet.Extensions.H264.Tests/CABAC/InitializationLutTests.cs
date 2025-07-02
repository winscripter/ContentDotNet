using ContentDotNet.Extensions.H264.Cabac.Internal;

namespace ContentDotNet.Extensions.H264.Tests.CABAC;

// We shall test the values:
// 0
// 5
// 25
// 100
// 250
// 276
// 450
// 600
// 800
// 1000
// 1023

public class InitializationLutTests
{
    [Fact]
    public void TestInitLUT_IDC0_PSP()
    {
        const int Idc = 0;

        Assert.Equal((20, -15), CabacFunctions.GetInitData(0, Idc));
        Assert.Equal((3, 74), CabacFunctions.GetInitData(5, Idc));
        Assert.Equal((9, 43), CabacFunctions.GetInitData(25, Idc));
        Assert.Equal((6, 69), CabacFunctions.GetInitData(100, Idc));
        Assert.Equal((1, 38), CabacFunctions.GetInitData(250, Idc));
        Assert.Equal((0, 0), CabacFunctions.GetInitData(276, Idc));
        Assert.Equal((2, 59), CabacFunctions.GetInitData(450, Idc));
        Assert.Equal((-1, 84), CabacFunctions.GetInitData(600, Idc));
        Assert.Equal((5, 61), CabacFunctions.GetInitData(800, Idc));
        Assert.Equal((-11, 87), CabacFunctions.GetInitData(1000, Idc));
        Assert.Equal((-23, 126), CabacFunctions.GetInitData(1023, Idc));
    }

    [Fact]
    public void TestInitLUT_IDC1_PSP()
    {
        const int Idc = 1;

        Assert.Equal((20, -15), CabacFunctions.GetInitData(0, Idc));
        Assert.Equal((3, 74), CabacFunctions.GetInitData(5, Idc));
        Assert.Equal((9, 43), CabacFunctions.GetInitData(25, Idc));
        Assert.Equal((8, 61), CabacFunctions.GetInitData(100, Idc));
        Assert.Equal((-10, 57), CabacFunctions.GetInitData(250, Idc));
        Assert.Equal((0, 0), CabacFunctions.GetInitData(276, Idc));
        Assert.Equal((2, 58), CabacFunctions.GetInitData(450, Idc));
        Assert.Equal((27, 36), CabacFunctions.GetInitData(600, Idc));
        Assert.Equal((1, 67), CabacFunctions.GetInitData(800, Idc));
        Assert.Equal((-25, 111), CabacFunctions.GetInitData(1000, Idc));
        Assert.Equal((-31, 127), CabacFunctions.GetInitData(1023, Idc));
    }

    [Fact]
    public void TestInitLUT_IDC2_PSP()
    {
        const int Idc = 2;

        Assert.Equal((20, -15), CabacFunctions.GetInitData(0, Idc));
        Assert.Equal((3, 74), CabacFunctions.GetInitData(5, Idc));
        Assert.Equal((9, 43), CabacFunctions.GetInitData(25, Idc));
        Assert.Equal((-4, 92), CabacFunctions.GetInitData(100, Idc));
        Assert.Equal((-8, 66), CabacFunctions.GetInitData(250, Idc));
        Assert.Equal((0, 0), CabacFunctions.GetInitData(276, Idc));
        Assert.Equal((-11, 68), CabacFunctions.GetInitData(450, Idc));
        Assert.Equal((8, 63), CabacFunctions.GetInitData(600, Idc));
        Assert.Equal((-1, 73), CabacFunctions.GetInitData(800, Idc));
        Assert.Equal((-15, 98), CabacFunctions.GetInitData(1000, Idc));
        Assert.Equal((-30, 127), CabacFunctions.GetInitData(1023, Idc));
    }

    [Fact]
    public void TestInitLUT_ISI()
    {
        Assert.Equal((20, -15), CabacFunctions.GetInitDataForIOrSISlice(0));
        Assert.Equal((3, 74), CabacFunctions.GetInitDataForIOrSISlice(5));
        Assert.Equal((9, 43), CabacFunctions.GetInitDataForIOrSISlice(25));
        Assert.Equal((-20, 127), CabacFunctions.GetInitDataForIOrSISlice(100));
        Assert.Equal((-6, 62), CabacFunctions.GetInitDataForIOrSISlice(250));
        Assert.Equal((0, 0), CabacFunctions.GetInitDataForIOrSISlice(276));
        Assert.Equal((2, 59), CabacFunctions.GetInitDataForIOrSISlice(450));
        Assert.Equal((0, 89), CabacFunctions.GetInitDataForIOrSISlice(600));
        Assert.Equal((5, 42), CabacFunctions.GetInitDataForIOrSISlice(800));
        Assert.Equal((-6, 77), CabacFunctions.GetInitDataForIOrSISlice(1000));
        Assert.Equal((-30, 127), CabacFunctions.GetInitDataForIOrSISlice(1023));
    }
}
