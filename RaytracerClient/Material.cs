public abstract class Material
{
    internal abstract int Type { get; }

    internal abstract void GetParameters(out double r, out double g, out double b, out double param);
}



public class Lambertian : Material
{
    public double R, G, B;

    public Lambertian(double r, double g, double b)
    {
        R = r;
        G = g;
        B = b;
    }

    internal override int Type => 0;

    internal override void GetParameters(out double r, out double g, out double b, out double param)
    {
        r = R;
        g = G;
        b = B;
        param = 0;
    }
}



public class Metal : Material
{
    public double R, G, B;
    public double Fuzz;

    public Metal(double r, double g, double b, double fuzz)
    {
        R = r;
        G = g;
        B = b;
        Fuzz = fuzz;
    }

    internal override int Type => 1;

    internal override void GetParameters(out double r, out double g, out double b, out double param)
    {
        r = R;
        g = G;
        b = B;
        param = Fuzz;
    }
}



public class Dielectric : Material
{
    public double RefractionIndex;

    public Dielectric(double ri)
    {
        RefractionIndex = ri;
    }

    internal override int Type => 2;

    internal override void GetParameters(out double r, out double g, out double b, out double param)
    {
        r = 0;
        g = 0;
        b = 0;
        param = RefractionIndex;
    }
}

