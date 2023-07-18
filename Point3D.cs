using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace Engine;

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
    
    public float Dist(Point3D point)
    {
        var dx = point.X - this.X;
        var dy = point.Y - this.Y;
        var dz = point.Z - this.Z;

        var dist = MathF.Sqrt(dx*dx + dy*dy + dz*dz);

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

    public PointF Projection(float FOV)
    {
        var projectedX = (X * FOV) / (Z + FOV);
        var projectedY = (Y * FOV) / (Z + FOV);

        return new PointF(projectedX, projectedY);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
        => base.Equals(obj);

    public override int GetHashCode()
        => base.GetHashCode();

    public override string? ToString()
        => $"({X}, {Y}, {Z})";
}