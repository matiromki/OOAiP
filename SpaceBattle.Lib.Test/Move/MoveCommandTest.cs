namespace SpaceBattle.Lib.Test;

public class MoveCommandTest
{
    [Fact]
    public void ChangePositionTest()
    {
        var movable = new Mock<IMoveable>();
        movable.SetupProperty(m => m.position, new Vector(12, 5));
        movable.SetupGet<Vector>(m => m.velocity).Returns(new Vector(-7, 3));

        var move = new MoveCommand(movable.Object);
        move.Execute();

        movable.VerifySet(m => m.position = new Vector(5, 8));
    }

    [Fact]
    public void UnreadablePositionTest()
    {
        var movable = new Mock<IMoveable>();
        movable.SetupGet(m => m.position).Throws<Exception>();
        movable.SetupGet<Vector>(m => m.velocity).Returns(new Vector(-7, 3));

        var move = new MoveCommand(movable.Object);

        Assert.Throws<Exception>(() => move.Execute());
    }

    [Fact]
    public void UnreadableVelocityTest()
    {
        var movable = new Mock<IMoveable>();
        movable.SetupProperty(m => m.position, new Vector(12, 5));
        movable.SetupGet<Vector>(m => m.velocity).Throws<Exception>();

        var move = new MoveCommand(movable.Object);

        Assert.Throws<Exception>(() => move.Execute());
    }

    [Fact]
    public void UnchangeablePositionTest()
    {
        var movable = new Mock<IMoveable>();
        movable.SetupProperty(m => m.position, new Vector(12, 5));
        movable.SetupSet(m => m.position = It.IsAny<Vector>()).Throws<Exception>();
        movable.SetupGet<Vector>(m => m.velocity).Returns(new Vector(-7, 3));

        var move = new MoveCommand(movable.Object);

        Assert.Throws<Exception>(() => move.Execute());
    }
}