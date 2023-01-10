namespace SpaceBattle.Lib;
using Hwdtech;

public class CreateReapeatableStrategy: IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var dependenceName = (string) args[0];
        var obj = (IUObject) args[1];

        var macro = IoC.Resolve<ICommand>("SpaceBattle.Operation.Macro", dependenceName, obj);
        var inj = IoC.Resolve<ICommand>("SpaceBattle.Operation.Inject", macro);
        var cmd = IoC.Resolve<ICommand>("SpaceBattle.Operation.Repeat");

        return cmd;
    }
}
