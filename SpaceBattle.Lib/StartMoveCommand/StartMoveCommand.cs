using Hwdtech;

namespace SpaceBattle.Lib;

public class StartMoveCommand : ICommand
{
    private IMoveCommandStartable obj;
    public StartMoveCommand(IMoveCommandStartable obj)
    {
        this.obj = obj;
    }

    public void Execute()
    {
        obj.properties.ToList().ForEach(o => IoC.Resolve<ICommand>("SpaceBattle.SetProperty", obj.uobject, o.Key, o.Value).Execute());
        var moveCmd = IoC.Resolve<ICommand>("SpaceBattle.Commands.Move", obj.uobject);
        IoC.Resolve<ICommand>("SpaceBattle.SetProperty", obj.uobject, "Move", moveCmd).Execute();
        IoC.Resolve<ICommand>("SpaceBattle.QueuePush", moveCmd).Execute();
    }
}