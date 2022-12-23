using Hwdtech;
namespace SpaceBattle.Lib;

public class StopMoveCommande: ICommand
{
    private IMoveCommandStopable obj;
    
    public StopMoveCommande(IMoveCommandStopable obj)
    {
        this.obj = obj;
    }

    public void Execute()
    {
        obj.properties.ToList().ForEach(p => IoC.Resolve<ICommand>("SpaceBattle.SetProperty", obj.uobject, p).Execute());
        IoC.Resolve<IInjectable>("SpaceBattle.Commands.SetupCommand", obj.uobject).Inject(IoC.Resolve<ICommand>("SpaceBattle.Commands.Empty"));
    }

}