using ContentDotNet.Api.Primitives;

namespace ContentDotNet.Abstractions.Tests;

public class FourCCTests
{
    [Fact]
    public void Number_Without_Conversions()
    {
        var fcc = new FourCC(0xAABBCCDDu);
        Assert.Equal(0xAABBCCDDu, fcc.Value);

        var fcc2 = new FourCC(0x125u);
        Assert.Equal(0x125u, fcc2.Value);
    }

    [Fact]
    public void String_Conversions()
    {
        // "ftyp", "moov", "mdat", "moof", "free" are from the File Type box from an MP4 video file
        TestString("ftyp");
        TestString("moov"); // moo-v! 🐮
        TestString("mdat");
        TestString("moof"); // moo-f! 🐄
        TestString("free");
    }

    [Fact]
    public void Number_To_String_Conversions()
    {
        var fcc = new FourCC(0x6D6F6F76u);
        Assert.Equal(0x6D6F6F76u, fcc.Value);
        Assert.Equal("moov", fcc.ValueText); // moo-v! 🐮

        fcc = new FourCC(0x6D6F6F66u);
        Assert.Equal(0x6D6F6F66u, fcc.Value);
        Assert.Equal("moof", fcc.ValueText); // moo-f! 🐮

        fcc = new FourCC(0x66747970u);
        Assert.Equal(0x66747970u, fcc.Value);
        Assert.Equal("ftyp", fcc.ValueText);

        fcc = new FourCC(0x66726565u);
        Assert.Equal(0x66726565u, fcc.Value);
        Assert.Equal("free", fcc.ValueText);
    }

    [Fact]
    public void String_To_Number_Conversions()
    {
        var fcc = new FourCC("moov");
        Assert.Equal(0x6D6F6F76u, fcc.Value);
        Assert.Equal("moov", fcc.ValueText); // moo-v! 🐮

        fcc = new FourCC("moof");
        Assert.Equal(0x6D6F6F66u, fcc.Value);
        Assert.Equal("moof", fcc.ValueText); // moo-f! 🐮

        fcc = new FourCC("ftyp");
        Assert.Equal(0x66747970u, fcc.Value);
        Assert.Equal("ftyp", fcc.ValueText);

        fcc = new FourCC("free");
        Assert.Equal(0x66726565u, fcc.Value);
        Assert.Equal("free", fcc.ValueText);
    }

    [Fact]
    public void Invalid_String_With_3_Letters_Should_Throw()
    {
        try
        {
            _ = FourCC.Parse("lol");
            Assert.Fail();
        }
        catch (ArgumentException)
        {
        }

        try
        {
            _ = new FourCC("lol");
            Assert.Fail();
        }
        catch (ArgumentException)
        {
        }
    }

    [Fact]
    public void Invalid_String_With_5_Letters_Should_Throw()
    {
        try
        {
            _ = FourCC.Parse("hello");
            Assert.Fail();
        }
        catch (ArgumentException)
        {
        }

        try
        {
            _ = new FourCC("hello");
            Assert.Fail();
        }
        catch (ArgumentException)
        {
        }
    }

    private static void TestString(string s)
    {
        var fcc = FourCC.Parse(s);
        
        // The .Parse method will convert the specified string to
        // uint first. ValueText will then convert that uint back
        // into its string form.

        Assert.Equal(s, fcc.ValueText);
    }
}
