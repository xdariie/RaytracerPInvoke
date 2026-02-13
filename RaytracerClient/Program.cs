using System.Drawing;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        int width = 800;
        int height = 450;

        byte[] buffer = new byte[width * height * 3];

        //define scene in C#
        NativeMethods.rt_clear_scene();

        Random rand = new Random();

        for (int a = -11; a < 11; a++)
        {
            for (int b = -11; b < 11; b++)
            {
                double chooseMat = rand.NextDouble();

                double x = a + 0.9 * rand.NextDouble();
                double y = 0.2;
                double z = b + 0.9 * rand.NextDouble();

                double dx = x - 4;
                double dz = z;

                if (Math.Sqrt(dx * dx + dz * dz) > 0.9)
                {
                    if (chooseMat < 0.8)
                    {
                        //diffuse
                        NativeMethods.rt_add_sphere(x, y, z, 0.2, 0, rand.NextDouble() * rand.NextDouble(), rand.NextDouble() * rand.NextDouble(), rand.NextDouble() * rand.NextDouble(), 0);
                    }
                    else if (chooseMat < 0.95)
                    {
                        //metal
                        NativeMethods.rt_add_sphere(x, y, z, 0.2, 1, 0.5 + rand.NextDouble() * 0.5, 0.5 + rand.NextDouble() * 0.5, 0.5 + rand.NextDouble() * 0.5, rand.NextDouble() * 0.5);
                    }
                    else
                    {
                        //glass
                        NativeMethods.rt_add_sphere(x, y, z, 0.2, 2, 1, 1, 1, 1.5);
                    }
                }
            }
        }

        //ground sphere
        NativeMethods.rt_add_sphere(0, -1000, 0, 1000, 0, 0.5, 0.5, 0.5, 0);

        //center sphere
        NativeMethods.rt_add_sphere(0, 1, 0, 1, 2, 1, 1, 1, 1.5);

        //left sphere
        NativeMethods.rt_add_sphere(-4, 1, 0, 1, 0, 0.4, 0.2, 0.1, 0);

        //right sphere
        NativeMethods.rt_add_sphere(4, 1, 0, 1, 1, 0.7, 0.6, 0.5, 0);

        NativeMethods.ProgressCallback callback = (percent) =>
        {
            Console.WriteLine($"Progress: {percent}%");
        };

        Console.WriteLine("Rendering...");
        NativeMethods.rt_render(buffer, width, height, callback);

        SaveImage(buffer, width, height);
        Console.WriteLine("Done!");
    }

    static void SaveImage(byte[] buffer, int width, int height)
    {
        //store in bitmap instead of writing to a PPM file using std::cout
        using Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);

        int index = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                byte r = buffer[index++];
                byte g = buffer[index++];
                byte b = buffer[index++];

                bmp.SetPixel(x, y, Color.FromArgb(r, g, b));
            }
        }
        //save it as a PNG image instead of a PPM file
        bmp.Save("output.png", ImageFormat.Png);
    }
}