namespace SpaceBattle.Lib;

public class Angle
{
    private int numerator;
    private int denominator;
    public Angle(int num, int den)
    {
        if (den == 0)
        {
            throw new Exception();
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
        int gsd = GCD(num, den);

        return new Angle(num / gsd, den / gsd);
    }

    public static bool operator ==(Angle a1, Angle a2)
    {
        int NOD1 = GCD(a1.numerator, a1.denominator);
        int NOD2 = GCD(a2.numerator, a2.denominator);

        return ((a1.numerator / NOD1 == a2.numerator / NOD2) &&
        (a1.denominator / NOD1 == a2.denominator / NOD2));

    }

    public static bool operator !=(Angle a1, Angle a2) => !(a1 == a2);

    private static int GCD(int num, int den)
    {
        return Math.Abs(den) == 0 ? Math.Abs(num) : GCD(Math.Abs(den), Math.Abs(num) % Math.Abs(den));
    }

    public override bool Equals(object? obj) => obj is Angle;

    public override int GetHashCode() => $"{numerator}/{denominator}".GetHashCode();
}