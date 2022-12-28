namespace SpaceBattle.Lib;

public interface IMoveCommandStartable
{
    IUObject uobject { get; }
    IDictionary<string, object> properties { get; }
}
