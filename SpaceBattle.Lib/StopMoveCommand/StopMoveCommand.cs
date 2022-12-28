using Hwdtech;
namespace SpaceBattle.Lib;

public class StopMoveCommand : ICommand
{
    private IMoveCommandStopable obj;

    public StopMoveCommand(IMoveCommandStopable obj)
    {
        this.obj = obj;
    }

    public void Execute()
    {
        obj.properties.ToList().ForEach(p => IoC.Resolve<ICommand>("SpaceBattle.RemoveProperty", obj.uobject, p).Execute());
        IoC.Resolve<IInjectable>("SpaceBattle.Commands.SetupCommand", obj.uobject).Inject(IoC.Resolve<ICommand>("SpaceBattle.Commands.Empty"));
    }

}
