using System.Numerics;

public class Camera
{
    public Point3D Location { get; set; }
    public float FOV { get; set; }
    public float CrrX
    {
        get
        {
            return CrrX;
        }
        set
        {
            CrrX = value % 360;
        }
    }
    public float CrrY
    {
        get
        {
            return CrrY;
        }
        set
        {
            CrrY = value % 360;
        }
    }

    public Camera(Point3D location, float fov, float crrX, float crrY)
    {
        Location = location;
        FOV = fov;
        CrrX = crrX;
        CrrY = crrY;
    }

    private float GetD(Point3D point)
    {
        var v = new Vector3(
            point.X - Location.X,
            point.Y - Location.Y,
            point.Z - Location.Z
        );

        var ax = v.X * Location.X;
        var by = v.Y * Location.Y;
        var cz = v.Z * Location.Z;
        
        // ax + by + cz + d = 0
        // ax + by + cz = -d
        var d = -(ax + by + cz);
        
        return d;
    }

    public bool ShouldRender(Point3D point, int maxDist)
    {
        var d = GetD(point);

        var isInFrontOf = d > 0;
        
        var isValidDist = Location.Dist(point) <= maxDist;

        return isInFrontOf && isValidDist;
    }
}