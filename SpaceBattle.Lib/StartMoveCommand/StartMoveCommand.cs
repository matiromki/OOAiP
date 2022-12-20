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
        obj.properties.ToList().ForEach(o => IoC.Resolve<ICommand>("Game.SetProperty", obj.uobject, o.Key, o.Value).Execute());
        ICommand moveCommand = IoC.Resolve<ICommand>("Command.Move", obj.uobject);
        IoC.Resolve<ICommand>("Game.SetProperty", obj.uobject, "Commands.Movement", moveCommand).Execute();
        IoC.Resolve<ICommand>("Queue.Push", moveCommand).Execute();
    }
}