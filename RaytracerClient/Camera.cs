public class Camera
{
    public double LookFromX, LookFromY, LookFromZ;
    public double LookAtX, LookAtY, LookAtZ;
    public double VupX, VupY, VupZ;
    public double VFoV;
    public int SamplesPerPixel;
    public int MaxDepth;
    public double DefocusAngle;
    public double FocusDist;

    public void Apply()
    {
        NativeMethods.rt_set_camera(LookFromX, LookFromY, LookFromZ,
            LookAtX, LookAtY, LookAtZ, VupX, VupY, VupZ, VFoV,
            SamplesPerPixel, MaxDepth, DefocusAngle, FocusDist);
    }
}

