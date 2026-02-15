using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

public static class NativeMethods
{
    //using C-style rules for passing arguments
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ProgressCallback(int percent);
    //this one says that the function is not in C#, so call it load and call it from the DLL
    [DllImport("raytracer.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void rt_clear_scene();

    [DllImport("raytracer.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void rt_add_sphere(double x, double y, double z, double radius, int material_type, double r, double g, double b, double param);

    [DllImport("raytracer.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void rt_render(byte[] buffer, int width, int height, ProgressCallback? callback);

    [DllImport("raytracer.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void rt_set_camera(double lookfrom_x, double lookfrom_y, double lookfrom_z,
        double lookat_x, double lookat_y, double lookat_z,
        double vup_x, double vup_y, double vup_z,
        double vfov, int samples_per_pixel, int max_depth,
        double defocus_angle, double focus_dist);

    [DllImport("raytracer.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void rt_add_lambertian_sphere(
    double x, double y, double z,
    double radius,
    double r, double g, double b);

    [DllImport("raytracer.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void rt_add_metal_sphere(
        double x, double y, double z,
        double radius,
        double r, double g, double b,
        double fuzz);

    [DllImport("raytracer.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void rt_add_dielectric_sphere(
        double x, double y, double z,
        double radius,
        double refractionIndex);


}
