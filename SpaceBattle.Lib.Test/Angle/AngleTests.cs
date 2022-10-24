namespace SpaceBattle.Lib.Test;

public class AngleTest
{
    [Fact]
    public void PrintAngleTest()
    {
        var a = new Angle(30, 1);

        Assert.Equal("30", a.ToString());
    }

    [Fact]
    public void AngleZeroExceptionTestNegative()
    {
        Assert.Throws<ArgumentNullException>(() => new Angle(45, 0));
    }

    [Fact]
    public void AngleOperationAddTest()
    {
        var a1 = new Angle(30, 1);
        var a2 = new Angle(90, 1);

        Assert.Equal(new Angle(120, 1), a1 + a2);
    }

    [Fact]
    public void AngleEqualTest()
    {
        var a1 = new Angle(30, 1);
        var a2 = new Angle(60, 2);

        Assert.True(a1 == a2);
    }

    [Fact]
    public void AngleNotEqualTest()
    {
        var a1 = new Angle(30, 1);
        var a2 = new Angle(90, 2);

        Assert.True(a1 != a2);
    }

    [Fact]
    public void AngleHashCodeEqualTest()
    {
        var a1 = new Angle(30, 1);
        var a2 = new Angle(30, 1);

        Assert.True(a1.GetHashCode() == a2.GetHashCode());
    }

    [Fact]
    public void AngleHashCodeNotEqualTest()
    {
        var a1 = new Angle(30, 1);
        var a2 = new Angle(60, 1);

        Assert.True(a1.GetHashCode() != a2.GetHashCode());
    }

    [Fact]
    public void AngleEqualsTestBad()
    {
        var a1 = new Angle(30, 1);
        int b = 2;

        Assert.False(a1.Equals(b));
    }
}