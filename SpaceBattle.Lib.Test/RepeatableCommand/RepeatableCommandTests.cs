using Hwdtech;
using Hwdtech.Ioc;

namespace SpaceBattle.Lib.Test;

public class CreateRepeatableStrategyTests
{
    public CreateRepeatableStrategyTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
    }

    [Fact]
    public void CreateReapeatableStrategyPosTest()
    {
        var cmd = new Mock<ICommand>();

        var PropStrat = new Mock<IStrategy>();
        PropStrat.Setup(s => s.RunStrategy(It.IsAny<object[]>())).Returns(cmd.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceBattle.Operation.MacroCommand", (object[] args) => PropStrat.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceBattle.Operation.Inject", (object[] args) => new CreateInjectableStart().RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceBattle.Operation.Repeat", (object[] args) => new CreateRepeatableStart().RunStrategy(args)).Execute();

        var mockobj = new Mock<IUObject>();
        var CreateRS = new CreateReapeatableStrategy();

        var longOperation = CreateRS.RunStrategy(It.IsAny<string>(), mockobj.Object);

        Assert.IsAssignableFrom<ICommand>(longOperation);
        PropStrat.Verify();
    }

    [Fact]
    public void InjectableCommandExecutePosTest()
    {
        var cmd = new Mock<ICommand>();
        cmd.Setup(s => s.Execute()).Verifiable();

        var InjCmd = new InjectableCommand(cmd.Object);

        InjCmd.Execute();
        cmd.Verify();
    }

    [Fact]
    public void InjectableCommandInjectPosTest()
    {
        var cmd = new Mock<ICommand>();
        cmd.Setup(s => s.Execute()).Verifiable();

        var InjCmd = new InjectableCommand(cmd.Object);

        InjCmd.Inject(cmd.Object);
        InjCmd.Execute();

        cmd.Verify();
    }

    [Fact]
    public void RepeatableCommandExecutePosTest()
    {
        var cmd = new Mock<ICommand>();

        var PropStrat = new Mock<IStrategy>();
        PropStrat.Setup(s => s.RunStrategy(It.IsAny<object[]>())).Returns(cmd.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceBattle.Queue.Push", (object[] args) => PropStrat.Object.RunStrategy(args)).Execute();

        cmd.Setup(s => s.Execute()).Verifiable();
        var RepCmd = new RepeatableCommand(cmd.Object);

        RepCmd.Execute();

        cmd.Verify();
    }

    public class CreateInjectableStart : IStrategy
    {
        public object RunStrategy(params object[] args)
        {
            var cmd = (ICommand)args[0];
            return new InjectableCommand(cmd);
        }
    }

    public class CreateRepeatableStart : IStrategy
    {
        public object RunStrategy(params object[] args)
        {
            var cmd = (ICommand)args[0];
            return new RepeatableCommand(cmd);
        }
    }
}
