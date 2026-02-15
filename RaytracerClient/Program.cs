using System;
using System.Drawing;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        int width = 800;
        int height = 450;

        Random rand = new Random();

        //create scene
        Scene scene = new Scene();

        //create camera
        Camera cam = new Camera
        {
            LookFromX = 13,
            LookFromY = 2,
            LookFromZ = 3,
            LookAtX = 0,
            LookAtY = 0,
            LookAtZ = 0,
            VupX = 0,
            VupY = 1,
            VupZ = 0,
            VFoV = 20,
            SamplesPerPixel = 10,
            MaxDepth = 20,
            DefocusAngle = 0.6,
            FocusDist = 10
        };

        cam.Apply();


        //ground sphere
        scene.Add(new Sphere(0, -1000, 0, 1000, new Lambertian(0.5, 0.5, 0.5)));

        for (int a = -11; a < 11; a++)
        {
            for (int b = -11; b < 11; b++)
            {
                double chooseMat = rand.NextDouble();

                double centerX = a + 0.9 * rand.NextDouble();
                double centerY = 0.2;
                double centerZ = b + 0.9 * rand.NextDouble();

                //svoiding overlapping big sphere
                double dx = centerX - 4;
                double dz = centerZ;
                double distance = Math.Sqrt(dx * dx + dz * dz);

                if (distance > 0.9)
                {
                    if (chooseMat < 0.8)
                    {
                        //diffuse
                        scene.Add(new Sphere(centerX, centerY, centerZ, 0.2,
                            new Lambertian(rand.NextDouble(),rand.NextDouble(),rand.NextDouble())));
                    }
                    else if (chooseMat < 0.95)
                    {
                        //metal
                        scene.Add(new Sphere(centerX, centerY, centerZ, 0.2,
                            new Metal(rand.NextDouble() * 0.5 + 0.5,rand.NextDouble() * 0.5 + 0.5,rand.NextDouble() * 0.5 + 0.5,rand.NextDouble() * 0.5)));
                    }
                    else
                    {
                        //glass
                        scene.Add(new Sphere(centerX, centerY, centerZ, 0.2, new Dielectric(1.5)));
                    }
                }
            }
        }

        //center sphere
        scene.Add(new Sphere(0, 1, 0, 1, new Dielectric(1.5)));

        //left sphere
        scene.Add(new Sphere(-4, 1, 0, 1, new Lambertian(0.4, 0.2, 0.1)));

        //right sphere
        scene.Add(new Sphere(4, 1, 0, 1, new Metal(0.7, 0.6, 0.5, 0.0)));

        //create renderer
        Raytracer renderer = new Raytracer(width, height);

        //render scene
        byte[] image = renderer.Render(scene, percent => Console.WriteLine($"Progress: {percent}%"));

        //convert to Bitmap
        using Bitmap bmp = new Bitmap(width, height);

        int index = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int r = image[index++];
                int g = image[index++];
                int b = image[index++];

                bmp.SetPixel(x, y, Color.FromArgb(r, g, b));
            }
        }

        bmp.Save("output.png", ImageFormat.Png);
        Console.WriteLine("Rendering complete. Image saved as output.png");
    }
}
