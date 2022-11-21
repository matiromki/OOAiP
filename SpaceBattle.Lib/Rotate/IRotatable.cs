namespace SpaceBattle.Lib;

public interface IRotatable
{
    Angle direction { get; set; }
    Angle directionVelocity { get; }
}