namespace SpaceBattle.Lib;
public class GetPropertyStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        string prop = (string)args[0];
        IUObject obj = (IUObject)args[1];
        return (obj.getProperty(prop));
    }
}
