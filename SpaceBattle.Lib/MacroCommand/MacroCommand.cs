namespace SpaceBattle.Lib;

public class MacroCommand : ICommand
{   
    private IList<ICommand> listofCmds;

    public MacroCommand(IList<ICommand> cmds)
    {
        this.listofCmds = cmds;
    }
    public void Execute()
    {
        foreach (ICommand cmd in listofCmds)
        {
            cmd.Execute();
        }
    }
}
