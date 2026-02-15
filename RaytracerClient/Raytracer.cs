using System;

public class Raytracer
{
    private int width;
    private int height;
    private byte[] buffer;

    //stores image size
    public Raytracer(int width, int height)
    {
        this.width = width;
        this.height = height;
        buffer = new byte[width * height * 3];
    }

    public byte[] Render(Scene scene, Action<int>? progress = null)
    {
        scene.BuildNativeScene();

        NativeMethods.ProgressCallback? callback = null;

        if (progress != null)
            callback = percent => progress(percent);

        NativeMethods.rt_render(buffer, width, height, callback);

        return buffer;
    }
}
