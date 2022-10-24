namespace SpaceBattle.Lib;

public class Angle
{
    private int numerator;
    private int denominator;
    public Angle(int num, int den)
    {
        if (den == 0)
        {
            throw new ArgumentNullException();
        }
        numerator = num;
        denominator = den;
    }

    public override string ToString()
    {
        return $"{numerator / denominator}";
    }

    public static Angle operator +(Angle a1, Angle a2)
    {
        int num = a1.numerator * a2.denominator + a2.numerator * a1.denominator;
        int den = a1.denominator * a2.denominator;
        int gsd = GSD(num, den);

        return new Angle(num / gsd, den / gsd);
    }

    public static bool operator ==(Angle a1, Angle a2)
    {
        int NOD1 = GSD(a1.numerator, a1.denominator);
        int NOD2 = GSD(a2.numerator, a2.denominator);
        if ((a1.numerator / NOD1 == a2.numerator / NOD2) &&
        (a1.denominator / NOD1 == a2.denominator / NOD2))
        {
            return true;
        }

        return false;

    }

    public static bool operator !=(Angle a1, Angle a2)
    {
        return !(a1 == a2);
    }

    private static int GSD(int num, int den)
    {
        return Math.Abs(den) == 0 ? Math.Abs(num) : GSD(Math.Abs(den), Math.Abs(num) % Math.Abs(den));
    }

    public override bool Equals(object? obj)
    {
        return obj is Angle a && numerator == a.numerator && denominator == a.denominator;
    }

    public override int GetHashCode() => $"{numerator}{denominator}".GetHashCode();
}