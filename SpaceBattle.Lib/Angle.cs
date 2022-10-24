namespace SpaceBattle.Lib;

class Angle
{
    private int numerator;
    private int denominator;
    public Angle(int num, int den)
    {
        numerator = num;
        denominator = den;
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
        int a = num;
        int b = den;
        if (a < b) swap(a, b);
        while (a != 0)
        {
            a = a % b;
            swap(a, b);
        }

        return a;
    }
    private static void swap(int a, int b)
    {
        int c;
        c = a;
        a = b;
        b = a;
    }

    public override bool Equals(object? obj)
    {
        return obj is Angle a && numerator == a.numerator && denominator == a.denominator;
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}