namespace SpaceBattle.Lib.Test;

public class MoveCommandTest
{
    [Fact]
    public void ChangePositionTestPositive()
    {
        var movable = new Mock<IMoveable>();
        movable.SetupProperty(m => m.position, new Vector(12, 5));
        movable.SetupGet<Vector>(m => m.velocity).Returns(new Vector(-7, 3));

        var move = new MoveCommand(movable.Object);
        move.Execute();

        Assert.Equal(new Vector(12, 5), movable.Object.position);
    }

    [Fact]
    public void UnreadablePositionTestNegative()
    {
        var movable = new Mock<IMoveable>();
        movable.SetupGet(m => m.position).Throws<Exception>();
        movable.SetupGet<Vector>(m => m.velocity).Returns(new Vector(-7, 3));

        var move = new MoveCommand(movable.Object);

        Assert.Throws<Exception>(() => move.Execute());
    }

    [Fact]
    public void UnreadableVelocityTestNegative()
    {
        var movable = new Mock<IMoveable>();
        movable.SetupProperty(m => m.position, new Vector(12, 5));
        movable.SetupGet<Vector>(m => m.velocity).Throws<Exception>();

        var move = new MoveCommand(movable.Object);

        Assert.Throws<Exception>(() => move.Execute());
    }

    [Fact]
    public void UnchangeablePositionTestNegative()
    {
        var movable = new Mock<IMoveable>();
        movable.SetupProperty(m => m.position, new Vector(12, 5));
        movable.SetupSet(m => m.position = It.IsAny<Vector>()).Throws<Exception>();
        movable.SetupGet<Vector>(m => m.velocity).Returns(new Vector(-7, 3));

        var move = new MoveCommand(movable.Object);

        Assert.Throws<Exception>(() => move.Execute());
    }

}