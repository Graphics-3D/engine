using System.Drawing;
using System.Linq;
using System.Numerics;

namespace Engine;

public class Camera
{
    private Point3D location;
    public Point3D Location
    {
        get => location;
        set
        {
            location = value;
            UpdateValues();
        }
    }

    private float scale = 1;
    public float Scale
    {
        get => scale;
        set
        {
            if (value <= 0)
                return;

            scale = value;
            UpdateValues();
        }
    }

    private float fov;
    public float FOV
    {
        get => fov * scale;
        set
        {
            if (value <= 0)
                return;
            
            fov = value;
            UpdateValues();
        }
    }

    private Vector3 normal;
    public Vector3 Normal
    {
        get => normal;
        set
        {
            normal = value;
            UpdateValues();
        }
    }

    private Vector3 vertical;
    public Vector3 Vertical
    {
        get => vertical;
        set
        {
            vertical = value;
            UpdateValues();
        }
    }
    
    private int width = 1280;
    public int Width
    {
        get => width;
        set
        {
            if (value <= 0)
                return;
                
            width = value;
            UpdateScreen();
        }
    }
    
    private int height = 720;
    public int Height
    {
        get => height;
        set
        {
            if (value <= 0)
                return;

            height = value;
            UpdateScreen();
        }
    }

    public int DistanceRender { get; set; }

    private Graphics g;
    private Bitmap bmp;
    private float d;
    private Vector3 productVec;
    private Point3D center;

    public Camera(Point3D location, Vector3 normal, Vector3 vertical, float fov, int distanceRender)
    {
        Location = location;
        Normal = normal;
        Vertical = vertical;
        FOV = fov;
        DistanceRender = distanceRender;

        bmp = new Bitmap(Width, Height);
        g = Graphics.FromImage(bmp);

        UpdateValues();
    }

    public Camera(Point3D location, Vector3 normal, Vector3 vertical, int width, int height, float fov, int distanceRender)
    {
        Location = location;
        Normal = normal;
        Vertical = vertical;
        Width = width;
        Height = height;
        FOV = fov;
        DistanceRender = distanceRender;

        bmp = new Bitmap(Width, Height);
        g = Graphics.FromImage(bmp);

        UpdateValues();
    }

    private void UpdateScreen()
    {
        bmp = new Bitmap(Width, Height);
        g = Graphics.FromImage(bmp);
    }

    private void UpdateValues()
    {
        var ax = Normal.X * Location.X;
        var by = Normal.Y * Location.Y;
        var cz = Normal.Z * Location.Z;

        // ax + by + cz = -d
        d = -(ax + by + cz);

        productVec = Vector3.Cross(Vertical, Normal);
        center = GetPlaneCenter(Location);
    }

    private PointF Transform(Point3D point)
    {
        var pointInPlane = GetPointInPlane(point);

        var point2D = TranformPoint(pointInPlane);

        return point2D;
    }

    public void Render()
        => Render(Scene.Current);

    public void Render(Scene scene)
    {
        g.Clear(scene.BackgroundColor);

        foreach (var mesh in scene.Meshes)
        {
            var shouldRender = mesh.Faces.Any(face => ShouldRender(face.p) && ShouldRender(face.q) && ShouldRender(face.r));

            if (!shouldRender)
                continue;

            foreach (var face in mesh.Faces)
            {
                g.DrawPolygon(mesh.Pen, new PointF[]
                {
                    Transform(face.p),
                    Transform(face.q),
                    Transform(face.r)
                });
            }
        }
    }

    public bool ShouldRender(Point3D point)
        =>  IsInFrontOfThePlane(point) &&
            Location.Dist(point) <= DistanceRender;

    private bool IsInFrontOfThePlane(Point3D point)
    {
        var ax = Normal.X * point.X;
        var by = Normal.Y * point.Y;
        var cz = Normal.Z * point.Z;

        var result = ax + by + cz + d;
        
        return result > 0;
    }

    private PointF TranformPoint(Point3D point)
    {
        var v = this.Vertical;

        var x = point.X - center.X;
        var y = point.Y - center.Y;
        var z = point.Z - center.Z;

        var vx = v.X;
        var vy = v.Y;
        var vz = v.Z;
        
        var ux = productVec.X;
        var uy = productVec.Y;
        var uz = productVec.Z;

        // P = (a, b)
        // a * vx + b * ux = x
        // a * vy + b * uy = y
        // a * vz + b * uz = z

        float a, b;
        if (v.X != 0)
            (a, b) = BaseTranformation(x, y, vx, vy, ux, uy);
        else if (v.Y != 0)
            (a, b) = BaseTranformation(y, z, vy, vz, uy, uz);
        else
            (a, b) = BaseTranformation(z, x, vz, vx, uz, ux);

        return new PointF(a + 480, b + 320);
    }

    private (float a, float b) BaseTranformation(float x, float y, float vx, float vy, float ux, float uy)
    {
        // a * vx + b * ux = x
        // a = (x - b * ux) / vx

        // a * vy + b * uy = y
        // ((x - b * ux) / vx) * vy + b * uy = y
        // (x * vy - b * ux * vy) / vx + b * uy = y
        // x * vy - b * ux * vy + b * uy * vx = y * vx
        // x * vy + b * (-ux * vy + uy * vx) = y * vx
        // b = (y * vx - x * vy) / (-ux * vy + uy * vx)
        var b = (y * vx - x * vy) / (-ux * vy + uy * vx);
        var a = (x - b * ux) / vx;

        return (a, b);
    }

    private Point3D GetPlaneCenter(Point3D point)
        => IntersectionCalc(point, Normal);

    private Point3D GetPointInPlane(Point3D point)
        => IntersectionCalc(point, point - Location);
    
    private Point3D IntersectionCalc(Point3D point, Vector3 vec)
    {
        var vx = vec.X;
        var vy = vec.Y;
        var vz = vec.Z;

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

        // FOV added to d, to move the plane to correct point of view

        var t = (- d + FOV -(nx * px + ny * py + nz * pz)) / (vx * nx + vy * ny + vz * nz);
        

        // r(t) = (px + vx * t, py + vy * t, pz + vz * t)
        var rX = px + vx * t;
        var rY = py + vy * t;
        var rZ = pz + vz * t;

        return new Point3D(rX, rY, rZ);
    }

    public void Draw(Graphics g)
    {
        g.DrawImage(
            bmp,
            Point.Empty
        );
    }

    public void Translate(float x, float y, float z)
        => this.Location = this.Location with
        {
            X = this.Location.X + x,
            Y = this.Location.Y + y,
            Z = this.Location.Z + z
        };

    public void Zoom(float scale)
        => this.scale += scale;
}