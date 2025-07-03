using ContentDotNet.Extensions.H264.Cabac;

namespace ContentDotNet.Extensions.H264.Tests.CABAC;

public class InitialContextValueTests
{
    [Fact]
    public void Test_CtxIdx0_CII0_Bypass0_I0_QP0()
    {
        var cabac = new CabacContext(0, 0, false, false, 0);
        Assert.Equal(62, cabac.PStateIdx);
        Assert.False(cabac.ValMps);
    }

    [Fact]
    public void Test_CtxIdx15_CII0_Bypass0_I0_QP26()
    {
        var cabac = new CabacContext(15, 0, false, false, 26);
        Assert.Equal(14, cabac.PStateIdx);
        Assert.False(cabac.ValMps);
    }

    [Fact]
    public void Test_CtxIdx280_CII0_Bypass0_I0_QP46()
    {
        var cabac = new CabacContext(280, 0, false, false, 46);
        Assert.Equal(10, cabac.PStateIdx);
        Assert.False(cabac.ValMps);
    }
}
