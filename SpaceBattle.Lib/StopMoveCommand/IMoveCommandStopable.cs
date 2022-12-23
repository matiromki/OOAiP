namespace SpaceBattle.Lib;

public interface IMoveCommandStopable
{
    IUObject uobject { get; }

    IEnumerable<string> properties { get; }
    
}