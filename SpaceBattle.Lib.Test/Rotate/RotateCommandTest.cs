namespace SpaceBattle.Lib.Test;

public class RotateCommandTest
{
    [Fact]
    public void ChangeDirectionTest()
    {
        var rotatable = new Mock<IRotatable>();
        rotatable.SetupProperty(r => r.direction, new Angle(45, 1));
        rotatable.SetupGet<Angle>(r => r.directionVelocity).Returns(new Angle(90, 1));

        var rotate = new RotateCommand(rotatable.Object);
        rotate.Execute();

        rotatable.VerifySet(r => r.direction = new(135, 1));
    }

    [Fact]
    public void UnreadableDirectionTest()
    {
        var rotatable = new Mock<IRotatable>();
        rotatable.SetupGet(r => r.direction).Throws<Exception>();
        rotatable.SetupGet<Angle>(r => r.directionVelocity).Returns(new Angle(90, 1));

        var rotate = new RotateCommand(rotatable.Object);

        Assert.Throws<Exception>(() => rotate.Execute());
    }

    [Fact]
    public void UnreadableDirectionVelocityTest()
    {
        var rotatable = new Mock<IRotatable>();
        rotatable.SetupProperty(r => r.direction, new Angle(45, 1));
        rotatable.SetupGet<Angle>(r => r.directionVelocity).Throws<Exception>();

        var rotate = new RotateCommand(rotatable.Object);

        Assert.Throws<Exception>(() => rotate.Execute());
    }

    [Fact]
    public void UnchangeableDirectionTest()
    {
        var rotatable = new Mock<IRotatable>();
        rotatable.SetupProperty(r => r.direction, new Angle(45, 1));
        rotatable.SetupSet(r => r.direction = It.IsAny<Angle>()).Throws<Exception>();
        rotatable.SetupGet<Angle>(r => r.directionVelocity).Returns(new Angle(90, 1));

        var rotate = new RotateCommand(rotatable.Object);

        Assert.Throws<Exception>(() => rotate.Execute());
    }
}