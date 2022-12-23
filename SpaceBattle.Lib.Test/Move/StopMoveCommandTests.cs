using Hwdtech;
using Hwdtech.Ioc;

namespace SpaceBattle.Lib.Test;

public class StopMoveCommandeTests
{
    public StopMoveCommandeTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var mockCommand = new Mock<SpaceBattle.Lib.ICommand>();
        mockCommand.Setup(x => x.Execute());

        var mockInjecting = new Mock<IInjectable>();
        mockInjecting.Setup(x => x.Inject(It.IsAny<SpaceBattle.Lib.ICommand>()));

        var mockStrategyReturnCommand = new Mock<IStrategy>();
        mockStrategyReturnCommand.Setup(x => x.RunStrategy(It.IsAny<object[]>())).Returns(mockCommand.Object);
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceBattle.RemoveProperty", (object[] args) => mockStrategyReturnCommand.Object.RunStrategy(args)).Execute();

        var mockStrategyReturnIInjectable = new Mock<IStrategy>();
        mockStrategyReturnIInjectable.Setup(x => x.RunStrategy(It.IsAny<object[]>())).Returns(mockInjecting.Object);
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceBattle.Commands.SetupCommand", (object[] args) => mockStrategyReturnIInjectable.Object.RunStrategy(args)).Execute();

        var mockStrategyReturnEmpty = new Mock<IStrategy>();
        mockStrategyReturnEmpty.Setup(x => x.RunStrategy()).Returns(mockCommand.Object);
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceBattle.Commands.Empty", (object[] args) => mockStrategyReturnEmpty.Object.RunStrategy(args)).Execute();

    }

    [Fact]
    public void StopMoveCommandPositive()
    {
        var stopable = new Mock<IMoveCommandStopable>();
        var obj = new Mock<IUObject>();
        stopable.SetupGet(a => a.uobject).Returns(obj.Object).Verifiable();
        stopable.SetupGet(a => a.properties).Returns(new List<string>() { "Velocity" }).Verifiable();
        ICommand smc = new StopMoveCommand(stopable.Object);

        smc.Execute();
        stopable.Verify();
    }

    [Fact]
    public void ExceptionFromUobjectNegative()
    {
        var stopable = new Mock<IMoveCommandStopable>();
        stopable.SetupGet(a => a.uobject).Throws<Exception>().Verifiable();
        stopable.SetupGet(a => a.properties).Returns(new List<string>() { "Velocity" }).Verifiable();
        ICommand smc = new StopMoveCommand(stopable.Object);

        Assert.Throws<Exception>(() => smc.Execute());
    }

    [Fact]
    public void ExceptionFromVelocityNegative()
    {
        var stopable = new Mock<IMoveCommandStopable>();
        var obj = new Mock<IUObject>();
        stopable.SetupGet(a => a.uobject).Returns(obj.Object).Verifiable();
        stopable.SetupGet(a => a.properties).Throws<Exception>().Verifiable();
        ICommand smc = new StopMoveCommand(stopable.Object);

        Assert.Throws<Exception>(() => smc.Execute());
    }

}