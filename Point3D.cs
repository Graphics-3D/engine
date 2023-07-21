namespace Engine;

using Core;

public struct Point3D : ITransformable<Point3D>
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    public static Point3D Empty
        => new Point3D(0, 0, 0);
    
    public Point3D(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    
    public float Dist(Point3D point)
    {
        var dx = point.X - this.X;
        var dy = point.Y - this.Y;
        var dz = point.Z - this.Z;

        var dist = System.MathF.Sqrt(dx*dx + dy*dy + dz*dz);

        return dist;
    }

    public float DistSquared(Point3D point)
    {
        var dx = point.X - this.X;
        var dy = point.Y - this.Y;
        var dz = point.Z - this.Z;

        var dist = dx*dx + dy*dy + dz*dz;

        return dist;
    }

    public Point3D Translate(float x, float y, float z)
        => new(X + x, Y + y, Z + z);

    public Point3D Scale(float x, float y, float z)
        => new(X * x, Y * y, Z * z);

    public Point3D RotateX(float cos, float sin)
        => new(X, Y * cos - Z * sin, Y * sin + Z * cos);

    public Point3D RotateY(float cos, float sin)
        => new(X * cos + Z * sin, Y, Z * cos - Y * sin);

    public Point3D RotateZ(float cos, float sin)
        => new(X * cos - Y * sin, Y * cos + X * sin, Z);

    public static Point3D operator +(Point3D p, Point3D v)
        => new(p.X + v.X, p.Y + v.Y, p.Z + v.Z);

    public static Vector3 operator -(Point3D p, Point3D v)
        => new(p.X - v.X, p.Y - v.Y, p.Z - v.Z);

    public static implicit operator Point3D((float X, float Y, float Z) axis)
        => new(axis.X, axis.Y, axis.Z);

    public override string? ToString()
        => $"({X}, {Y}, {Z})";
}