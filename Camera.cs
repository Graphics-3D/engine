using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace Engine;

public class Camera
{
    public Point3D Location { get; set; }
    public float FOV { get; set; }
    public Vector3 Normal { get; set; }
    public Vector3 Vertical { get; set; }
    public int Width { get; init; }
    public int Height { get; init; }
    public int DistanceRender { get; init; }

    private readonly Graphics g;
    private readonly Bitmap bmp;
    private readonly float d;

    public Camera(Point3D location, Vector3 normal, Vector3 vertical, int width, int height, float fov, int distanceRender)
    {
        Location = location;
        Normal = normal;
        Vertical = vertical;
        Width = width;
        Height = height;
        FOV = fov;
        DistanceRender = distanceRender;

        var ax = normal.X * location.X;
        var by = normal.Y * location.Y;
        var cz = normal.Z * location.Z;
        
        // ax + by + cz = -d
        d = -(ax + by + cz);
        GeneratePlan(new Point3D(10,5,0));
        bmp = new Bitmap(Width, Height);
        g = Graphics.FromImage(bmp);
    }

    public void Render(Scene scene)
    {
        g.Clear(scene.BackgroundColor);

        foreach (var mesh in scene.Meshes)
        {
            foreach (var face in mesh.Faces)
            {
                if (!ShouldRender(face.p) && !ShouldRender(face.q) && !ShouldRender(face.r))
                    continue;

                g.DrawPolygon(mesh.Pen, new PointF[]
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

    private void GeneratePlan(Point3D point)
    {
        var vx = point.X - Location.X;
        var vy = point.Y - Location.Y;
        var vz = point.Z - Location.Z;

        var nx = Normal.X;
        var ny = Normal.Y;
        var nz = Normal.Z;

        var px = point.X;
        var py = point.Y;
        var pz = point.Z;

        
        // nx * X + ny * Y + nz * Z + d = 0
        // r(t) = (px + vx * t, py + vy * t, pz + vz * t)
        // nx(px + vx * t) + ny(py + vy * t) + nz(pz + vz * t) = -d
        // nx * px + vx * t * nx + ny * py + vy * t * ny + nz * pz + vz * t * nz = -d
        // nx * px + ny * py + nz * pz + t(vx * nx + vy * ny + vz * nz) = -d
        // t(vx * nx + vy * ny + vz * nz) = -d -(nx * px + ny * py + nz * pz)
        // t = (-d -(nx * px + ny * py + nz * pz)) / (vx * nx + vy * ny + vz * nz)

        var t = (-(d + FOV) -(nx * px + ny * py + nz * pz)) / (vx * nx + vy * ny + vz * nz);
        

        //r(t) = (px + vx * t, py + vy * t, pz + vz * t)
        var rX = px + vx * t;
        var rY = py + vy * t;
        var rZ = pz + vz * t;

        MessageBox.Show(new Point3D(rX, rY, rZ).ToString() + " --- " + t.ToString());
    }

    public void Draw(Graphics g)
    {
        g.DrawImage(
            bmp,
            Point.Empty
        );
    }
}