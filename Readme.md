This project is a small raytracer based on the “Raytracing InOneWeekend” code. 
Originally it was a normal C++ program with main.cc that creates a scene (a lot of 
spheres) and renders it to a PPM image by printing RGB values to the console.
For this assignment I changed it so the raytracer works like a library. The C++ 
raytracer is compiled as a DLL and a C# program becomes the “new main”. The C# 
part defines the scene and calls the renderer through P/Invoke. The rendered 
pixels are returned back to C# through a byte buffer, and then C# saves the final 
image as output.png. I also pass a progress callback from C# to C++ so the C++ 
renderer can report rendering progress while it works.
So the structure is like this:
C# app (scene and saving image) -> C API (wrapper) -> C++ raytracer (engine)

To make this work, I added a C API layer in two new files: c_api.h and c_api.cpp. 
In these files I declared exported functions using extern "C" and __declspec(dllexport) 
so they can be accessed from the DLL. Since C# cannot directly call C++ classes, 
I created simple C-style functions like clearing the scene, adding spheres, 
setting the camera parameters, and rendering the image. These functions 
internally use the original raytracer classes such as camera, sphere, material, 
etc., so the rendering logic itself was not rewritten.

I also modified the rendering part so that instead of writing PPM text to std::cout, 
the renderer writes RGB values directly into an unsigned char* buffer. This buffer is 
created in C# and passed into the render function. The C++ code fills it with pixel 
data in RGB format. After that, C# converts this raw data into a Bitmap and saves it 
as a PNG file. This replaces the old console PPM output with a more modern image file.

Another thing I added is a progress callback. In the render function, I pass a function 
pointer from C# to C++. During rendering, C++ calculates the progress and calls 
this callback function. In C#, I implemented it using a delegate with the correct 
calling convention, and it simply prints the current progress percentage to the 
console.

To build the native part, I changed the CMake configuration so that instead of 
creating an executable, it creates a shared library (DLL). After building, I get 
raytracer.dll. Then I created a C# Console App project, added the DllImport 
declarations with CallingConvention.Cdecl, and implemented the scene creation 
and rendering calls. The DLL has to be placed in the C# output folder so that the 
program can load it at runtime.

In the final version, the original main.cc file is no longer used. Its role is 
replaced by the C# program. The C++ code now acts only as a rendering engine, 
while C# is responsible for scene setup, calling the render function, handling 
the buffer, and saving the final image.


Short instuction how to run this project:
To run the project, the native C++ part must be built first, since the C# 
application depends on the generated raytracer.dll. After building the C++ project 
with CMake, the DLL file is created in the build folder and needs to be copied 
into the C# output directory (for example bin/Debug/netX.X). The C# application 
loads this DLL at runtime using P/Invoke. Once the DLL is in the correct location, 
the C# project can be run normally. The program will render the scene, show 
progress in the console, and save the final image as output.png.