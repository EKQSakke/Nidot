using Godot;

public static class CPUParticles3DExtensions
{
    public static void Play(this CPUParticles3D particles3D)
    {
        particles3D.Restart();
        particles3D.Emitting = true;
    }
}