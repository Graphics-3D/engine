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
    public Color ClearColor { get; set; } = Color.White;
    public int DistanceRender { get; init; } 
    private readonly float d;

    public Camera(Point3D location, Vector3 normal, int width, int height, float fov, int distanceRender)
    {
        Location = location;
        Normal = normal;
        Width = width;
        Height = height;
        FOV = fov;
        DistanceRender = distanceRender;

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
        g.Clear(ClearColor);

        foreach (var mesh in scene.Meshes)
        {
            foreach (var face in mesh.Faces)
            {
                if (!ShouldRender(face.p) && !ShouldRender(face.q) && !ShouldRender(face.r))
                    continue;

                g.DrawPolygon(Pens.Black, new PointF[]
                {
                    face.p.Projection(FOV),
                    face.q.Projection(FOV),
                    face.r.Projection(FOV)
                });
            }
        }
    }

    public bool ShouldRender(Point3D point)
        =>  IsInFrontOfThePlan(point) &&
            Location.Dist(point) <= DistanceRender;

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