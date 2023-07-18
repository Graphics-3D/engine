using System.Numerics;

namespace Engine;

public class Camera
{
    public Point3D Location { get; set; }
    public float FOV { get; set; }
    public Vector3 Normal { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    private readonly float d;

    public Camera(Point3D location, float fov, Vector3 normal, int width, int height)
    {
        Location = location;
        FOV = fov;
        Normal = normal;
        Width = width;
        Height = height;

        var ax = normal.X * location.X;
        var by = normal.Y * location.Y;
        var cz = normal.Z * location.Z;
        
        // ax + by + cz = -d
        d = -(ax + by + cz);
    }

    private float GetD(Point3D point)
    {
        var ax = Normal.X * point.X;
        var by = Normal.Y * point.Y;
        var cz = Normal.Z * point.Z;

        var result = ax + by + cz + d;
        
        return result;
    }

    public bool ShouldRender(Point3D point, int maxDist)
    {
        var isInFrontOf = GetD(point) > 0;

        var isValidDist = Location.Dist(point) <= maxDist;

        return isInFrontOf && isValidDist;
    }
}