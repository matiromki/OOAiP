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
}
