using System.Collections.Generic;

public class Scene
{
    private List<Sphere> spheres = new List<Sphere>();

    public void Add(Sphere sphere)
    {
        spheres.Add(sphere);
    }

    internal void BuildNativeScene()
    {
        NativeMethods.rt_clear_scene();

        foreach (var sphere in spheres)
        {
            sphere.AddToNative();
        }
    }
}
