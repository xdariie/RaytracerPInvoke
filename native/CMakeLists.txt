cmake_minimum_required ( VERSION 3.1.0...4.0.0 )

project ( raytracer LANGUAGES CXX )

# Set to C++11
set ( CMAKE_CXX_STANDARD          11 )
set ( CMAKE_CXX_STANDARD_REQUIRED ON )

add_library(raytracer SHARED
    CSBridge/c_api.cpp
)

target_include_directories(raytracer PRIVATE .)