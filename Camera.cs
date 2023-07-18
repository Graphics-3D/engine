using System.Drawing;
using System.Numerics;

namespace Engine;

public class Camera
{
    private readonly Graphics g;

    public Point3D Location { get; set; }
    public float FOV { get; set; }
    public Vector3 Normal { get; set; }
    public int Width { get; init; }
    public int Height { get; init; }
    private readonly float d;

    public Camera(Point3D location, Vector3 normal, int width, int height, float fov)
    {
        Location = location;
        Normal = normal;
        Width = width;
        Height = height;
        FOV = fov;

        var ax = normal.X * location.X;
        var by = normal.Y * location.Y;
        var cz = normal.Z * location.Z;
        
        // ax + by + cz = -d
        d = -(ax + by + cz);

        var bmp = new Bitmap(Width, Height);
        g = Graphics.FromImage(bmp);
    }

    public void Render(Scene scene)
    {

        
    }

    public bool ShouldRender(Point3D point, int maxDist)
        =>  IsInFrontOfThePlan(point) &&
            Location.Dist(point) <= maxDist;

    private bool IsInFrontOfThePlan(Point3D point)
    {
        var ax = Normal.X * point.X;
        var by = Normal.Y * point.Y;
        var cz = Normal.Z * point.Z;

        var result = ax + by + cz + d;
        
        return result > 0;
    }

    public void Draw()
    {

    }
}