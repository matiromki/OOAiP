namespace SpaceBattle.Lib.Test;

public class RotateCommandTest
{
    [Fact]
    public void ChangeDirectionTest()
    {
        var rotatable = new Mock<IRotatable>();
        rotatable.SetupProperty(r => r.direction, 45);
        rotatable.SetupGet<int>(r => r.directionVelocity).Returns(90);

        var rotate = new RotateCommand(rotatable.Object);
        rotate.Execute();

        rotatable.VerifySet(r => r.direction = 135);
    }

    [Fact]
    public void UnreadableDirectionTest()
    {
        var rotatable = new Mock<IRotatable>();
        rotatable.SetupGet(r => r.direction).Throws<Exception>();
        rotatable.SetupGet<int>(r => r.directionVelocity).Returns(90);

        var rotate = new RotateCommand(rotatable.Object);

        Assert.Throws<Exception>(() => rotate.Execute());
    }

    [Fact]
    public void UnreadableDirectionVelocityTest()
    {
        var rotatable = new Mock<IRotatable>();
        rotatable.SetupProperty(r => r.direction, 45);
        rotatable.SetupGet<int>(r => r.directionVelocity).Throws<Exception>();

        var rotate = new RotateCommand(rotatable.Object);

        Assert.Throws<Exception>(() => rotate.Execute());
    }

    [Fact]
    public void UnchangeableDirectionTest()
    {
        var rotatable = new Mock<IRotatable>();
        rotatable.SetupProperty(r => r.direction, 45);
        rotatable.SetupSet(r => r.direction = It.IsAny<int>()).Throws<Exception>();
        rotatable.SetupGet<int>(r => r.directionVelocity).Returns(90);

        var rotate = new RotateCommand(rotatable.Object);

        Assert.Throws<Exception>(() => rotate.Execute());
    }
}