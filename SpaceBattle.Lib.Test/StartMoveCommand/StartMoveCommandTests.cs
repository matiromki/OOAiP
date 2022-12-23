using Hwdtech;
using Hwdtech.Ioc;
using Moq;

namespace SpaceBattle.Lib.Test;

public class StartMoveCommandTests
{
    public StartMoveCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var mockcmd = new Mock<ICommand>();
        mockcmd.Setup(c => c.Execute());

        var mockStrategyReturnsCmd = new Mock<IStrategy>();
        mockStrategyReturnsCmd.Setup(sc => sc.RunStrategy(It.IsAny<object[]>())).Returns(mockcmd.Object);
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceBattle.SetProperty", (object[] args) => mockStrategyReturnsCmd.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceBattle.Commands.Move", (object[] args) => mockStrategyReturnsCmd.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceBattle.QueuePush", (object[] args) => mockStrategyReturnsCmd.Object.RunStrategy(args)).Execute();


    }

    [Fact]
    public void SuccessofStartCommandExecute()
    {
        var mcs = new Mock<IMoveCommandStartable>();
        mcs.SetupGet(c => c.uobject).Returns(new Mock<IUObject>().Object).Verifiable();
        mcs.SetupGet(c => c.properties).Returns(new Dictionary<string, object>() { { "Velocity", new Vector(It.IsAny<int>(), It.IsAny<int>()) } }).Verifiable();

        ICommand smc = new StartMoveCommand(mcs.Object);
        smc.Execute();

        mcs.Verify();
    }

    [Fact]
    public void StartMoveCommandThrowsExceptionCantToGetUObjectNegativeTest()
    {
        var mcs = new Mock<IMoveCommandStartable>();
        mcs.SetupGet(c => c.uobject).Throws<Exception>().Verifiable();
        mcs.SetupGet(c => c.properties).Returns(new Dictionary<string, object>() { { "Velocity", new Vector(It.IsAny<int>(), It.IsAny<int>()) } }).Verifiable();

        ICommand smc = new StartMoveCommand(mcs.Object);

        Assert.Throws<Exception>(() => smc.Execute());
    }

    [Fact]
    public void StartMoveCommandThrowsExceptionCantToGetValueOfVelocityNegativeTest()
    {
        var mcs = new Mock<IMoveCommandStartable>();
        mcs.SetupGet(a => a.uobject).Returns(new Mock<IUObject>().Object).Verifiable();
        mcs.SetupGet(a => a.properties).Throws<Exception>().Verifiable();

        ICommand smc = new StartMoveCommand(mcs.Object);

        Assert.Throws<Exception>(() => smc.Execute());
    }
   

        
}