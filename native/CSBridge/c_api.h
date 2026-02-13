#pragma once

#ifdef _WIN32 //if we are using windows
#define API __declspec(dllexport)
#else //if it is not windows
#define API
#endif

#ifdef __cplusplus
extern "C" {
#endif

    typedef void(*ProgressCallback)(int);
    API void rt_clear_scene();
	API void rt_add_sphere(double x, double y, double z, double radius, int material_type, double r, double g, double b, double param);
    API void rt_set_camera(double lookfrom_x, double lookfrom_y, double lookfrom_z,
                    double lookat_x, double lookat_y, double lookat_z,
                    double vup_x, double vup_y, double vup_z,
                    double vfov, int samples_per_pixel, int max_depth,
		            double defocus_angle, double focus_dist);
    API void rt_render(unsigned char* buffer, int width, int height, ProgressCallback callback);

#ifdef __cplusplus
}
#endif