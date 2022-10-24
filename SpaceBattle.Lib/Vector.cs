namespace SpaceBattle.Lib;
public class Vector
{
    public int[] coordinates;
    public int Size;
    public Vector(params int[] args)
    {
        coordinates = args;
        Size = args.Length;
    }

    public override string ToString()
    {
        return $"({String.Join(", ", coordinates)})";
    }
    public int this[int index]
    {
        get => coordinates[index];
        set => coordinates[index] = value;
    }
    public static Vector operator +(Vector v1, Vector v2)
    {
        if (v1.Size != v2.Size)
        {
            throw new ArgumentException();
        }
        else
        {
            int[] coord = new int[v1.Size];

            for (int i = 0; i < v1.Size; i++) coord[i] = v1[i] + v2[i];

            return new Vector(coord);
        }
    }
    public static bool operator ==(Vector v1, Vector v2)
    {
        if (v1.Size != v2.Size) return false;

        for (int i = 0; i < v1.Size; i++)
        {
            if (v1[i] != v2[i]) return false;
        }

        return true;
    }
    public static bool operator !=(Vector v1, Vector v2)
    {
        return !(v1 == v2);
    }

    public override bool Equals(object? obj) => obj is Vector;

    public override int GetHashCode() => String.Join("", coordinates.Select(x => x.ToString())).GetHashCode();

}