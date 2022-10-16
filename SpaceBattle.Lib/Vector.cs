namespace SpaceBattle.Lib;

public class Vector
{
    public int[] coord;
    public int size;

    public Vector(params int[] coord)
    {
        size = coord.Length;
        this.coord = new int[size];
        for (int i = 0; i < size; i++) this.coord[i] = coord[i];
    }

    public override bool Equals(object? obj) => obj is Vector;

    public override int GetHashCode() => HashCode.Combine(coord);


    public override string ToString()
    {
        string str = "[+] Vector (";

        for (int i = 0; i < size - 1; i++)
        {
            str += coord[i] + ", ";
        }

        str += coord[size - 1] + ")";

        return str;
    }

    public int this[int index]
    {
        get => coord[index];
        set => coord[index] = value;
    }

    public static Vector operator +(Vector x, Vector y)
    {
        if (x.size != y.size)
        {
            throw new System.ArgumentException();
        }
        else
        {
            int[] coord = new int[x.size];

            for (int i = 0; i < x.size; i++)
            {
                coord[i] = x[i] + y[i];
            }
            return new Vector(coord);
        }
    }

    public static Vector operator -(Vector x, Vector y)
    {
        if (x.size != y.size)
        {
            throw new System.ArgumentException();
        }
        else
        {
            int[] coord = new int[x.size];

            for (int i = 0; i < x.size; i++)
            {
                coord[i] = x[i] - y[i];
            }
            return new Vector(coord);
        }
    }

    public static Vector operator *(int n, Vector x)
    {
        int[] coord = new int[x.size];

        for (int i = 0; i < x.size; i++)
        {
            coord[i] = n * x[i];
        }
        return new Vector(coord);
    }

    public static bool operator ==(Vector x, Vector y)
    {
        if (x.size != y.size)
        {
            return false;
        }
        for (int i = 0; i < x.size; i++)
        {
            if (x[i] != y[i])
            {
                return false;
            }
        }
        return true;
    }

    public static bool operator !=(Vector x, Vector y)
    {
        if (x == y)
        {
            return false;
        }
        return true;
    }

}