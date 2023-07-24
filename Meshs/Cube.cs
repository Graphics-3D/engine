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
            new Point3D(x - size, y - size, z - size),
            new Point3D(x - size, y - size, z + size),
            new Point3D(x - size, y + size, z - size)
        );

        this.Faces[index++] = new Face(
            new Point3D(x - size, y + size, z + size),
            new Point3D(x - size, y - size, z + size),
            new Point3D(x - size, y + size, z - size)
        );
        
        this.Faces[index++] = new Face(
            new Point3D(x - size, y - size, z - size),
            new Point3D(x - size, y - size, z + size),
            new Point3D(x + size, y - size, z - size)
        );
        
        this.Faces[index++] = new Face(
            new Point3D(x + size, y - size, z + size),
            new Point3D(x - size, y - size, z + size),
            new Point3D(x + size, y - size, z - size)
        );

        this.Faces[index++] = new Face(
            new Point3D(x + size, y - size, z + size),
            new Point3D(x + size, y - size, z - size),
            new Point3D(x + size, y + size, z + size)
        );

        this.Faces[index++] = new Face(
            new Point3D(x + size, y + size, z - size),
            new Point3D(x + size, y + size, z + size),
            new Point3D(x + size, y + size, z - size)
        );

        this.Faces[index++] = new Face(
            new Point3D(x - size, y + size, z + size),
            new Point3D(x + size, y + size, z + size),
            new Point3D(x - size, y + size, z - size)
        );

        this.Faces[index++] = new Face(
            new Point3D(x + size, y + size, z - size),
            new Point3D(x + size, y + size, z + size),
            new Point3D(x - size, y + size, z - size)
        );

        this.Faces[index++] = new Face(
            new Point3D(x + size, y - size, z + size),
            new Point3D(x + size, y + size, z + size),
            new Point3D(x - size, y - size, z + size)
        );

        this.Faces[index++] = new Face(
            new Point3D(x - size, y + size, z + size),
            new Point3D(x + size, y + size, z + size),
            new Point3D(x - size, y - size, z + size)
        );

        this.Faces[index++] = new Face(
            new Point3D(x + size, y + size, z - size),
            new Point3D(x - size, y + size, z - size),
            new Point3D(x + size, y - size, z - size)
        );

        this.Faces[index++] = new Face(
            new Point3D(x - size, y - size, z - size),
            new Point3D(x - size, y + size, z - size),
            new Point3D(x + size, y - size, z - size)
        );
    }
}