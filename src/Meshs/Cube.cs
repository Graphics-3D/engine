namespace Engine.Meshes;

using Core;

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

    public Cube(Point3D location, float size)
    {
        Location = location;
        Size = size;

        Update();
    }

    private void Update()
    {
        this.Faces = new Face[12];
        float half = size / 2;
        int index = 0;

        var x = this.Location.X;
        var y = this.Location.Y;
        var z = this.Location.Z;

        this.Faces[index++] = new Face(
            new Point3D(x - half, y - half, z - half),
            new Point3D(x - half, y - half, z + half),
            new Point3D(x - half, y + half, z - half)
        );

        this.Faces[index++] = new Face(
            new Point3D(x - half, y + half, z + half),
            new Point3D(x - half, y - half, z + half),
            new Point3D(x - half, y + half, z - half)
        );
        
        this.Faces[index++] = new Face(
            new Point3D(x - half, y - half, z - half),
            new Point3D(x - half, y - half, z + half),
            new Point3D(x + half, y - half, z - half)
        );
        
        this.Faces[index++] = new Face(
            new Point3D(x + half, y - half, z + half),
            new Point3D(x - half, y - half, z + half),
            new Point3D(x + half, y - half, z - half)
        );

        this.Faces[index++] = new Face(
            new Point3D(x + half, y - half, z + half),
            new Point3D(x + half, y - half, z - half),
            new Point3D(x + half, y + half, z + half)
        );

        this.Faces[index++] = new Face(
            new Point3D(x + half, y + half, z - half),
            new Point3D(x + half, y + half, z + half),
            new Point3D(x + half, y + half, z - half)
        );

        this.Faces[index++] = new Face(
            new Point3D(x - half, y + half, z + half),
            new Point3D(x + half, y + half, z + half),
            new Point3D(x - half, y + half, z - half)
        );

        this.Faces[index++] = new Face(
            new Point3D(x + half, y + half, z - half),
            new Point3D(x + half, y + half, z + half),
            new Point3D(x - half, y + half, z - half)
        );

        this.Faces[index++] = new Face(
            new Point3D(x + half, y - half, z + half),
            new Point3D(x + half, y + half, z + half),
            new Point3D(x - half, y - half, z + half)
        );

        this.Faces[index++] = new Face(
            new Point3D(x - half, y + half, z + half),
            new Point3D(x + half, y + half, z + half),
            new Point3D(x - half, y - half, z + half)
        );

        this.Faces[index++] = new Face(
            new Point3D(x + half, y + half, z - half),
            new Point3D(x - half, y + half, z - half),
            new Point3D(x + half, y - half, z - half)
        );

        this.Faces[index++] = new Face(
            new Point3D(x - half, y - half, z - half),
            new Point3D(x - half, y + half, z - half),
            new Point3D(x + half, y - half, z - half)
        );
    }

    public CollidedResult Collided(Point3D point)
    {
        float
            minX = float.MaxValue,
            minY = float.MaxValue,
            minZ = float.MaxValue,
            maxX = float.MinValue,
            maxY = float.MinValue,
            maxZ = float.MinValue;

        foreach (var face in this.Faces)
        {
            var points = new Point3D[] { face.p, face.q, face.r };
            foreach (var innerPoint in points)
            {
                if (innerPoint.X < minX)
                    minX = innerPoint.X;
                
                if (innerPoint.X > maxX)
                    maxX = innerPoint.X;
                
                if (innerPoint.Y < minY)
                    minY = innerPoint.Y;

                if (innerPoint.Y > maxY)
                    maxY = innerPoint.Y;

                if (innerPoint.Z < minZ)
                    minZ = innerPoint.Z;

                if (innerPoint.Z > maxZ)
                    maxZ = innerPoint.Z;
            }
        }

        float
            x = point.X,
            y = point.Y,
            z = point.Z;

        if (
            x < minX || x > maxX ||
            y < minY || y > maxY ||
            z < minZ || z > maxZ
        ) return CollidedResult.False;

        if (x == minX)
            return CollidedResult.Front;

        if (x == maxX)
            return CollidedResult.Back;

        if (y == minY)
            return CollidedResult.Left;
        
        if (y == maxY)
            return CollidedResult.Right;
        // Error
        if(z == minZ)
            return CollidedResult.Top;

        if (z == maxZ)
            return CollidedResult.Bottom;

        return CollidedResult.True;
    }
}