namespace SpaceBattle.Lib;

public class InjectableCommand : ICommand , IInjectable
{
    ICommand cmd;
    public InjectableCommand(ICommand cmd)
    {
        this.cmd = cmd;
    }
    public void Execute()
    {
        this.cmd.Execute();
    }
    public void Inject(ICommand obj)
    {
        this.cmd = obj;
    }
}
