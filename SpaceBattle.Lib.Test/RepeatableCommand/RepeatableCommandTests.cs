using Hwdtech;
using Hwdtech.Ioc;

namespace SpaceBattle.Lib.Test;

public class RepeatableCommandTests
{
    public RepeatableCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
    }

    [Fact]
    public void CreateMacroCommandStrategyPositiveTest()
    {
        var macroc = new Mock<ICommand>();
        macroc.Setup(c => c.Execute());

        var getMacroCommandStrategy = new Mock<IStrategy>();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceBattle.Operation.MacroCommand", (object[] args) => getMacroCommandStrategy.Object.RunStrategy(args)).Execute();
        getMacroCommandStrategy.Setup(s => s.RunStrategy(It.IsAny<string>(), new Mock<IUObject>())).Returns(macroc.Object);


        var InjCommand = new InjectableCommand(macroc.Object);
        var getInjectableStrategy = new Mock<IStrategy>();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceBattle.Operation.Inject", (object[] args) => getInjectableStrategy.Object.RunStrategy(args)).Execute();
        getMacroCommandStrategy.Setup(s => s.RunStrategy(It.IsAny<object[]>())).Returns(InjCommand);


        var RepCommand = new RepeatableCommand(InjCommand);
        var getRepeatableStrategy = new Mock<IStrategy>();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceBattle.Operation.Repeat", (object[] args) => getRepeatableStrategy.Object.RunStrategy(args)).Execute();
        getMacroCommandStrategy.Setup(s => s.RunStrategy(It.IsAny<object[]>())).Returns(new CreateReapeatableStrategy());
    }
}
