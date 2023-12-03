using System;

public class Fraction
{
    public int Numerator { get; private set; }
    public int Denominator { get; private set; }

    public Fraction(int numerator, int denominator)
    {
        if (denominator == 0)
            throw new ArgumentException("Denominator cannot be zero.");

        int gcd = GreatestCommonDivisor(numerator, denominator);
        Numerator = numerator / gcd;
        Denominator = denominator / gcd;
    }

    public Fraction(int numerator)
    {
        Numerator = numerator;
        Denominator = 1;
    }

    public static Fraction FromFloat(float value, int maxDenominator = 10000)
    {
        if (value == 0)
            return new Fraction(0);

        int sign = Math.Sign(value);
        value = Math.Abs(value);
        int gcd = GreatestCommonDivisor((int)(value * maxDenominator), maxDenominator);

        return new Fraction(sign * (int)(value * maxDenominator) / gcd, maxDenominator / gcd);
    }

    private static int GreatestCommonDivisor(int a, int b)
    {
        return b == 0 ? a : GreatestCommonDivisor(b, a % b);
    }

    public override string ToString()
    {
        if (Denominator == 1)
            return Numerator.ToString();
        else
            return Numerator + "/" + Denominator;
    }

    // Define basic arithmetic operations for fractions
    public static Fraction operator +(Fraction a, Fraction b)
    {
        return new Fraction(a.Numerator * b.Denominator + b.Numerator * a.Denominator, a.Denominator * b.Denominator);
    }

    public static Fraction operator -(Fraction a, Fraction b)
    {
        return new Fraction(a.Numerator * b.Denominator - b.Numerator * a.Denominator, a.Denominator * b.Denominator);
    }

    public static Fraction operator *(Fraction a, Fraction b)
    {
        return new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator);
    }

    public static Fraction operator /(Fraction a, Fraction b)
    {
        if (b.Numerator == 0)
            throw new DivideByZeroException("Division by zero.");

        return new Fraction(a.Numerator * b.Denominator, a.Denominator * b.Numerator);
    }
}
