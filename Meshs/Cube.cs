using Engine.Core;

namespace Engine.Meshs;

public class Cube : Mesh
{
    private Point3D location;
    public Point3D Location
    {
        get => location;
        set
        {
            location = value;
            Update();
        }
    }

    private float size;
    public float Size
    {
        get => size;
        set
        {
            size = value;
            Update();
        }
    }

    private int polygonsPerSide;
    public int PolygonsPerSide
    {
        get => polygonsPerSide;
        set
        {
            polygonsPerSide = value;
            Update();
        }
    }

    private readonly int sides = 6;

    public Cube(Point3D location, float size) : this (location, size, 8) { }

    public Cube(Point3D location, float size, int polygonsPerSide)
    {
        Location = location;
        Size = size;
        PolygonsPerSide = polygonsPerSide;

        Update();
    }

    private void Update()
    {
        // this.Faces = new Face[this.sides * this.PolygonsPerSide];
        this.Faces = new Face[48];
        
        var VectorI = new Vector3(1, 0, 0);
        var VectorJ = new Vector3(0, 1, 0);
        var VectorK = new Vector3(0, 0, 1);

        MakeFace(VectorI * size, VectorJ * size, VectorK * size, 0);
        MakeFace(-VectorI * size, VectorJ * size, VectorK * size, 8);
        
        MakeFace(VectorJ * size, VectorK * size, VectorI * size, 16);
        MakeFace(-VectorJ * size, VectorI * size, VectorK * size, 24);

        MakeFace(VectorK * size, VectorJ * size, VectorI * size, 32);
        MakeFace(-VectorK * size, VectorJ * size, VectorI * size, 40);
    }

    Point3D Sub(Point3D point, Vector3 vec)
    {
        return new Point3D(
            point.X - vec.X,
            point.Y - vec.Y,
            point.Z - vec.Z
        );
    }

    Point3D Sum(Point3D point, Vector3 vec)
    {
        return new Point3D(
            point.X + vec.X,
            point.Y + vec.Y,
            point.Z + vec.Z
        );
    }

    void MakeFace(Vector3 u, Vector3 tp, Vector3 lf, int baseIndex)
    {
        var center = Sum(Location, u);

        this.Faces[baseIndex++] = new Face(
            Sum(center, lf + tp),
            Sum(center, lf),
            Sum(center, tp)
        );

        this.Faces[baseIndex++] = new Face(
            center,
            Sum(center, lf),
            Sum(center, tp)
        );

        this.Faces[baseIndex++] = new Face(
            Sum(center, -lf + tp),
            Sum(center, -lf),
            Sum(center, tp)
        );

        this.Faces[baseIndex++] = new Face(
            center,
            Sum(center, -lf),
            Sum(center, tp)
        );

        this.Faces[baseIndex++] = new Face(
            Sum(center, -lf - tp),
            Sum(center, -lf),
            Sum(center, -tp)
        );

        this.Faces[baseIndex++] = new Face(
            center,
            Sum(center, -lf),
            Sum(center, -tp)
        );

        this.Faces[baseIndex++] = new Face(
            Sum(center, lf - tp),
            Sum(center, lf),
            Sum(center, -tp)
        );

        this.Faces[baseIndex++] = new Face(
            center,
            Sum(center, lf),
            Sum(center, -tp)
        );
    }

    // public void MakeSide(int index)
    // {
    //     var points = new List<Point3D>();
    //     var sizeFrac = size / (2 * PolygonsPerSide);

    //     for (int i = -1; i < 2; i += 2)
    //     {
    //         for (int j = -1; j < 2; j += 2)
    //         {
    //             for (int k = -1; k < 2; k += 2)
    //             {
    //                 var newPoint = new Point3D(
    //                     Location.X + sizeFrac * i,
    //                     Location.Y + sizeFrac * j,
    //                     Location.Z + sizeFrac * k
    //                 );
    //                 points.Add(newPoint);
    //             }
    //         }
    //     }

    //     for (int i = 0; i < length; i++)
    //     {
    //         if (i % 2 == 0)
    //         {

    //         }
    //     }

    //     // Front
    //     // 0, 1, 2, 3

    //     // Back
    //     // 4, 5, 6, 7

    //     // Right
    //     // 0, 1, 4, 5

    //     // Left
    //     // 2, 3, 6, 7

    //     // Top
    //     // 1, 3, 5, 7

    //     // Bottom
    //     // 0, 2, 4, 6
    // }
}