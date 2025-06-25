using ContentDotNet.Extensions.H264.Cabac.Internal;

namespace ContentDotNet.Extensions.H264.Tests.CABAC;

public class RangeTabLpsTests
{
    [Fact]
    public void TestRangeTabLpsValues()
    {
        Assert.Equal(128, CabacFunctions.GetRangeTabLps(0, 0));
        Assert.Equal(176, CabacFunctions.GetRangeTabLps(0, 1));
        Assert.Equal(208, CabacFunctions.GetRangeTabLps(0, 2));
        Assert.Equal(240, CabacFunctions.GetRangeTabLps(0, 3));

        Assert.Equal(111, CabacFunctions.GetRangeTabLps(5, 0));
        Assert.Equal(135, CabacFunctions.GetRangeTabLps(5, 1));
        Assert.Equal(160, CabacFunctions.GetRangeTabLps(5, 2));
        Assert.Equal(185, CabacFunctions.GetRangeTabLps(5, 3));

        Assert.Equal(85, CabacFunctions.GetRangeTabLps(10, 0));
        Assert.Equal(104, CabacFunctions.GetRangeTabLps(10, 1));
        Assert.Equal(123, CabacFunctions.GetRangeTabLps(10, 2));
        Assert.Equal(142, CabacFunctions.GetRangeTabLps(10, 3));

        Assert.Equal(51, CabacFunctions.GetRangeTabLps(20, 0));
        Assert.Equal(62, CabacFunctions.GetRangeTabLps(20, 1));
        Assert.Equal(73, CabacFunctions.GetRangeTabLps(20, 2));
        Assert.Equal(85, CabacFunctions.GetRangeTabLps(20, 3));

        Assert.Equal(18, CabacFunctions.GetRangeTabLps(40, 0));
        Assert.Equal(22, CabacFunctions.GetRangeTabLps(40, 1));
        Assert.Equal(26, CabacFunctions.GetRangeTabLps(40, 2));
        Assert.Equal(30, CabacFunctions.GetRangeTabLps(40, 3));

        Assert.Equal(11, CabacFunctions.GetRangeTabLps(50, 0));
        Assert.Equal(13, CabacFunctions.GetRangeTabLps(50, 1));
        Assert.Equal(15, CabacFunctions.GetRangeTabLps(50, 2));
        Assert.Equal(18, CabacFunctions.GetRangeTabLps(50, 3));

        Assert.Equal(6, CabacFunctions.GetRangeTabLps(60, 0));
        Assert.Equal(8, CabacFunctions.GetRangeTabLps(60, 1));
        Assert.Equal(9, CabacFunctions.GetRangeTabLps(60, 2));
        Assert.Equal(11, CabacFunctions.GetRangeTabLps(60, 3));

        Assert.Equal(2, CabacFunctions.GetRangeTabLps(63, 0));
        Assert.Equal(2, CabacFunctions.GetRangeTabLps(63, 1));
        Assert.Equal(2, CabacFunctions.GetRangeTabLps(63, 2));
        Assert.Equal(2, CabacFunctions.GetRangeTabLps(63, 3));
    }
}
