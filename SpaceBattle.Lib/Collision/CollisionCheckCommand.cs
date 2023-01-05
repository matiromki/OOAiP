using Hwdtech;
namespace SpaceBattle.Lib;

public class CollisionCheckCommand : ICommand
{
    private IUObject obj1, obj2;
    public CollisionCheckCommand(IUObject first, IUObject second)
    {
        this.obj1 = first;
        this.obj2 = second;
    }
    public void Execute()
    {
        var v = new List<Vector>();
        foreach(string prop in new List<string> {"Position", "Velocity"})
        {
            var firstObj = IoC.Resolve<Vector>("SpaceBattle.GetProperty", obj1, prop);
            var secondObj = IoC.Resolve<Vector>("SpaceBattle.GetProperty", obj2, prop);
            v.Add(firstObj - secondObj);
        }

        bool checkCollision = IoC.Resolve<bool>("SpaceBattle.CheckCollision", v);
        if (checkCollision) throw new Exception(); 
        
    }
}