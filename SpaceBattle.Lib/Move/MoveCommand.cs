namespace SpaceBattle.Lib;

public class MoveCommand : ICommand
{
    private IMoveable obj;

    public MoveCommand(IMoveable obj)
    {
        this.obj = obj;
    }

    public void Execute()
    {
        obj.position += obj.velocity;
    }
}