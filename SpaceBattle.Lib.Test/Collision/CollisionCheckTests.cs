using Hwdtech;
using Hwdtech.Ioc;
using Moq;

namespace SpaceBattle.Lib.Test;

public class CollisionCheckTests
{
    
    public CollisionCheckTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var getPropStrategy = new GetPropertyStrategy();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceBattle.GetProperty", (object[] args) => getPropStrategy.RunStrategy(args)).Execute();


    }

    [Fact]
    public void CollisionCheckReturnsTrue()
    {
        var obj1 = new Mock<IUObject>();
        var obj2 = new Mock<IUObject>();

        foreach (string prop in new List<string>() { "Position", "Velocity" })
        {
            obj1.Setup(x => x.getProperty(prop)).Returns(new Vector(It.IsAny<int>(), It.IsAny<int>()));
            obj2.Setup(x => x.getProperty(prop)).Returns(new Vector(It.IsAny<int>(), It.IsAny<int>()));
        }

        var CheckCollisionStrategy = new Mock<IStrategy>();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceBattle.CheckCollision", (object[] args) => CheckCollisionStrategy.Object.RunStrategy(args)).Execute();
        CheckCollisionStrategy.Setup(col => col.RunStrategy(It.IsAny<object[]>())).Returns(true).Verifiable();

        var checkCollision = new CollisionCheckCommand(obj1.Object, obj2.Object);

        Assert.Throws<Exception>(() => checkCollision.Execute());
        CheckCollisionStrategy.Verify();
    }

    [Fact]
    public void CollisionCheckReturnsFalse()
    {
        var obj1 = new Mock<IUObject>();
        var obj2 = new Mock<IUObject>();

        foreach (string prop in new List<string>() { "Position", "Velocity" })
        {
            obj1.Setup(x => x.getProperty(prop)).Returns(new Vector(It.IsAny<int>(), It.IsAny<int>()));
            obj2.Setup(x => x.getProperty(prop)).Returns(new Vector(It.IsAny<int>(), It.IsAny<int>()));
        }

        var CheckCollisionStrategy = new Mock<IStrategy>();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceBattle.CheckCollision", (object[] args) => CheckCollisionStrategy.Object.RunStrategy(args)).Execute();
        CheckCollisionStrategy.Setup(col => col.RunStrategy(It.IsAny<object[]>())).Returns(false).Verifiable();

        var checkCollision = new CollisionCheckCommand(obj1.Object, obj2.Object);
        checkCollision.Execute();

        CheckCollisionStrategy.Verify();
    }
}