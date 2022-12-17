namespace SpaceBattle.Lib;

public interface IStrategy
{
    object RunObject(params object[] args);
}