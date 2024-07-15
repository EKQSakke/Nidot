namespace TopDownZombies.Nidot.Helpers;

public static class DrawTools
{
    public static void DrawRay(MeshInstance3D rayMesh, Vector3 start, Vector3 end)
    {
        rayMesh.GlobalPosition = start;
        var immediateMesh = new ImmediateMesh();
        immediateMesh.ClearSurfaces();
        immediateMesh.SurfaceBegin(Mesh.PrimitiveType.Lines, rayMesh.MaterialOverride);
        immediateMesh.SurfaceAddVertex(new Vector3(0, 0, 0));
        immediateMesh.SurfaceAddVertex(end - start);
        immediateMesh.SurfaceEnd();
        rayMesh.Mesh = immediateMesh;
    }
}