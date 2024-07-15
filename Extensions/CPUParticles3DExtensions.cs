namespace TopDownZombies.Nidot.Extensions;

public static class CPUParticles3DExtensions
{
    public static void Play(this CpuParticles3D particles3D)
    {
        particles3D.Restart();
        particles3D.Emitting = true;
    }
}
