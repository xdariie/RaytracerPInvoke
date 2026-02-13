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
    public static extern void rt_render(byte[] buffer, int width, int height, ProgressCallback callback);
}
