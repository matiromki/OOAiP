namespace SpaceBattle.Lib.Test;

public class VectorTest
{

    [Fact]
    public void PrintVectorTest()
    {
        var v = new Vector(1, 2);

        Assert.Equal("(1, 2)", v.ToString());
    }

    [Fact]
    public void VectorGetIndex()
    {
        var v = new Vector(1, 2, 3);

        Assert.True(v[0] == 1);
    }

    [Fact]
    public void VectorSetIndex()
    {
        var v = new Vector(3, 4);
        v[1] = 2;

        Assert.True(v[1] == 2);
    }

    [Fact]
    public void PositiveOperationAddVectorTest()
    {
        var v1 = new Vector(1, 2);
        var v2 = new Vector(4, 5);

        var v3 = v1 + v2;

        Assert.Equal("(5, 7)", v3.ToString());
    }

    [Fact]
    public void NegativeOperationAddVectorTest()
    {
        var v1 = new Vector(1, 2, 3);
        var v2 = new Vector(2, 3);

        var action = () => v1 + v2;

        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void PositiveVectorEqualsTest()
    {
        var v1 = new Vector(1, 2);
        var v2 = new Vector(1, 2);

        Assert.True(v1 == v2);
    }

    [Fact]
    public void NegativeVectorEqualsTest()
    {
        var v1 = new Vector(1, 2);
        var v2 = new Vector(4, 5);

        Assert.False(v1 == v2);
    }

    [Fact]
    public void PositiveVectorNotEqualsTest()
    {
        var v1 = new Vector(1, 2);
        var v2 = new Vector(4, 5);

        Assert.True(v1 != v2);
    }

    [Fact]
    public void NegativeVectorNotEqualsTest1()
    {
        var v1 = new Vector(1, 2);
        var v2 = new Vector(1, 2);

        Assert.False(v1 != v2);
    }

    [Fact]
    public void NegativeVectorNotEqualsTest2()
    {
        var v1 = new Vector(1, 2, 3);
        var v2 = new Vector(1, 2);

        Assert.True(v1 != v2);
    }

    [Fact]
    public void VectorHashCodeGood()
    {
        var v1 = new Vector(1, 2, 3);
        var v2 = new Vector(1, 2, 3);

        Assert.True(v1.GetHashCode() == v2.GetHashCode());
    }

    [Fact]
    public void VectorHashCodeBad()
    {
        var v1 = new Vector(1, 2, 3);
        var v2 = new Vector(1, 0, 1);

        Assert.True(v1.GetHashCode() != v2.GetHashCode());

    }

}