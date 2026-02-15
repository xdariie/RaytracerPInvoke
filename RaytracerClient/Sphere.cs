public class Sphere
{
    public double X, Y, Z;
    public double Radius;
    public Material Material;

    public Sphere(double x, double y, double z, double radius, Material material)
    {
        X = x;
        Y = y;
        Z = z;
        Radius = radius;
        Material = material;
    }

    internal void AddToNative()
    {
        Material.GetParameters(out double r, out double g, out double b, out double param);

        NativeMethods.rt_add_sphere(X, Y, Z, Radius, Material.Type, r, g, b, param);
    }
}


