namespace SpaceBattle.Lib;
using Hwdtech;

public class RepeatableCommand : ICommand
{
    private ICommand cmd;

    public class RepeatableCommand(ICommand cmd)
    {
        this.cmd = cmd;
    }
    public void Execute()
    {

    }
}