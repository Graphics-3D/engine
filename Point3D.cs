using System.Numerics;
using System;
using System.Drawing;

public struct Point3D
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

    public float Angle(Point3D point)
    {
        // TODO
        var matrix = new Matrix4x4(
            1, 0, 0, 0,
            0, 1, 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 1
        );

        var det = matrix.GetDeterminant();

        return det;
    }

    public float Dist(Point3D point)
    {
        var dx = point.X - this.X;
        var dy = point.Y - this.Y;
        var dz = point.Z - this.Z;

        var dist = MathF.Sqrt(dx*dx + dy*dy + dz*dz);

        return dist;
    }

    public PointF Projection(float FOV)
    {
        var pojectedX = (X * FOV) / (Z + FOV);
        var pojectedY = (Y * FOV) / (Z + FOV);

        return new PointF(pojectedX, pojectedY);
    }
}