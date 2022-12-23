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
        obj.properties.ToList().ForEach(p => IoC.Resolve<ICommand>("SpaceBattle.Game.SetProperty", obj.uobject, p).Execute());
        
    }

}