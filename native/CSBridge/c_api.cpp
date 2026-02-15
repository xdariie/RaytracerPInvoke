#include "c_api.h"

#include "rtweekend.h"
#include "camera.h"
#include "hittable_list.h"
#include "material.h"
#include "sphere.h"

static hittable_list world;
static camera cam;
static bool cam_set = false;

API void rt_clear_scene() {
	world.clear();
}

API void rt_add_sphere(double x, double y, double z, double radius, int material_type, double r, double g, double b, double param) {
	point3 center(x, y, z);
	shared_ptr<material> mat;
	if (material_type == 0) {
		mat = make_shared<lambertian>(color(r, g, b));
	}
	else if (material_type == 1) {
		mat = make_shared<metal>(color(r, g, b), param);
	}
	else if (material_type == 2) {
		mat = make_shared<dielectric>(param);
	}
	world.add(make_shared<sphere>(center, radius, mat));
}

API void rt_set_camera(double lookfrom_x, double lookfrom_y, double lookfrom_z,
				   double lookat_x, double lookat_y, double lookat_z,
				   double vup_x, double vup_y, double vup_z,
				   double vfov, int samples_per_pixel, int max_depth,
				   double defocus_angle, double focus_dist) {
	cam.lookfrom = point3(lookfrom_x, lookfrom_y, lookfrom_z);
	cam.lookat = point3(lookat_x, lookat_y, lookat_z);
	cam.vup = vec3(vup_x, vup_y, vup_z);
	cam.vfov = vfov;
	cam.samples_per_pixel = samples_per_pixel;
	cam.max_depth = max_depth;
	cam.defocus_angle = defocus_angle;
	cam.focus_dist = focus_dist;
	cam_set = true;
}

API void rt_render(unsigned char* buffer, int width, int height, ProgressCallback callback) {
	if (!cam_set) {
		cam.samples_per_pixel = 10;
		cam.max_depth = 20;
		cam.vfov = 20;
		cam.lookfrom = point3(12, 2, 3);
		cam.lookat = point3(0, 0, 0);
		cam.vup = vec3(0, 1, 0);
		cam.defocus_angle = 0.6;
		cam.focus_dist = 10;
	}
	cam.render_to_buffer(world, buffer, width, height, callback);
}

API void rt_add_lambertian_sphere(double x, double y, double z, double radius, double r, double g, double b)
{
	auto mat = make_shared<lambertian>(color(r, g, b));
	world.add(make_shared<sphere>(point3(x, y, z), radius, mat));
}

API void rt_add_metal_sphere(double x, double y, double z, double radius, double r, double g, double b, double fuzz)
{
	auto mat = make_shared<metal>(color(r, g, b), fuzz);
	world.add(make_shared<sphere>(point3(x, y, z), radius, mat));
}

API void rt_add_dielectric_sphere(double x, double y, double z, double radius, double refraction_index)
{
	auto mat = make_shared<dielectric>(refraction_index);
	world.add(make_shared<sphere>(point3(x, y, z), radius, mat));
}
