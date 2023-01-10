using Hwdtech;
using Hwdtech.Ioc;
using Moq;

namespace SpaceBattle.Lib.Test;

public class MacroCommandTests
{
    public MacroCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var cmd = new Mock<ICommand>();
        cmd.Setup(c => c.Execute());

        var propStrategy = new Mock<IStrategy>();
        propStrategy.Setup(s => s.RunStrategy(It.IsAny<object[]>())).Returns(cmd.Object);
        var listStrategy = new Mock<IStrategy>();
        listStrategy.Setup(_i => _i.RunStrategy()).Returns(new string[] { "Second" });

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceBattle.Operation.First", (object[] props) => listStrategy.Object.RunStrategy(props)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Second", (object[] props) => propStrategy.Object.RunStrategy(props)).Execute();
    }

    [Fact]
    public void CreateMacroCommandStrategyPositiveTest()
    {
        var obj = new Mock<IUObject>();
        var createMC = new BuildMacroCommandStrategy();

        var mc = (ICommand)createMC.RunStrategy("First", obj.Object);

        mc.Execute();
    }
}
