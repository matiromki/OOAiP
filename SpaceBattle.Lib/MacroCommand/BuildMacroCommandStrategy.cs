using Hwdtech;

namespace SpaceBattle.Lib;

public class BuildMacroCommandStrategy : IStrategy
{   
    public object RunStrategy(params object[] args)
    {
        string key = (string)args[0];
        IUObject obj = (IUObject)args[1];

        IList<string> dependencies = IoC.Resolve<List<string>>("SpaceBattle.Operation." +key);

        var listofCmds = new List<ICommand>();
        foreach (string dep in dependencies)
        {
            listofCmds.Add(IoC.Resolve<ICommand>(dep, obj));
        }

        return new MacroCommand(listofCmds);
    }
}
