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
        a1.numerator /= GCD(a1.numerator, a1.denominator);
        a1.denominator /= GCD(a1.numerator, a1.denominator);

        a2.numerator /= GCD(a2.numerator, a2.denominator);
        a2.denominator /= GCD(a2.numerator, a2.denominator);

        return a1.Equals(a2);
    }

    public static bool operator !=(Angle a1, Angle a2) => !(a1 == a2);

    private static int GCD(int num, int den)
    {
        return Math.Abs(den) == 0 ? Math.Abs(num) : GCD(Math.Abs(den), Math.Abs(num) % Math.Abs(den));
    }

    public override bool Equals(object? obj) => obj is Angle a && numerator == a.numerator && denominator == a.denominator;

    public override int GetHashCode() => $"{numerator}/{denominator}".GetHashCode();
}