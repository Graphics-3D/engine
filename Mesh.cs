using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Engine;

public class Mesh
{
    public IEnumerable<Face> Faces { get; set; }
    public Pen Pen = Pens.Black; 

    public Mesh(params Face[] faces)
        => Faces = faces;

    public Mesh(IEnumerable<Face> faces)
        => Faces = faces;

    public Mesh Translate(float x, float y, float z)
    {
        Faces = Faces.Select(f => f.Translate(x, y, z));

        return this;
    }

    public Mesh Scale(float x, float y, float z)
    {
        Faces = Faces.Select(f => f.Scale(x, y, z));

        return this;
    }

    public Mesh RotateX(float cos, float sin)
    {
        Faces = Faces.Select(f => f.RotateX(cos, sin));

        return this;
    }

    public Mesh RotateY(float cos, float sin)
    {
        Faces = Faces.Select(f => f.RotateY(cos, sin));

        return this;
    }

    public Mesh RotateZ(float cos, float sin)
    {
        Faces = Faces.Select(f => f.RotateZ(cos, sin));

        return this;
    }

    public static Mesh GenerateRectangle(Point3D a, Point3D b)
    {
        var faces = new List<Face>();

        // Front
        faces.Add(new Face(
            new Point3D(a.X, a.Y, a.Z),
            new Point3D(b.X, a.Y, a.Z),
            new Point3D(a.X, b.Y, a.Z)
        ));
        faces.Add(new Face(
            new Point3D(a.X, b.Y, a.Z),
            new Point3D(b.X, b.Y, a.Z),
            new Point3D(b.X, a.Y, a.Z)
        ));

        // Back
        faces.Add(new Face(
            new Point3D(a.X, a.Y, b.Z),
            new Point3D(b.X, a.Y, b.Z),
            new Point3D(a.X, b.Y, b.Z)
        ));
        faces.Add(new Face(
            new Point3D(a.X, b.Y, b.Z),
            new Point3D(b.X, b.Y, b.Z),
            new Point3D(b.X, a.Y, b.Z)
        ));

        // Right
        faces.Add(new Face(
            new Point3D(b.X, a.Y, a.Z),
            new Point3D(b.X, b.Y, a.Z),
            new Point3D(b.X, a.Y, b.Z)
        ));
        faces.Add(new Face(
            new Point3D(b.X, a.Y, b.Z),
            new Point3D(b.X, b.Y, b.Z),
            new Point3D(b.X, b.Y, a.Z)
        ));

        // Left
        faces.Add(new Face(
            new Point3D(a.X, a.Y, a.Z),
            new Point3D(a.X, b.Y, a.Z),
            new Point3D(a.X, a.Y, b.Z)
        ));
        faces.Add(new Face(
            new Point3D(a.X, a.Y, b.Z),
            new Point3D(a.X, b.Y, b.Z),
            new Point3D(a.X, b.Y, a.Z)
        ));

        // Top
        faces.Add(new Face(
            new Point3D(a.X, a.Y, b.Z),
            new Point3D(a.X, a.Y, a.Z),
            new Point3D(b.X, a.Y, a.Z)
        ));
        faces.Add(new Face(
            new Point3D(a.X, a.Y, b.Z),
            new Point3D(b.X, a.Y, b.Z),
            new Point3D(b.X, a.Y, a.Z)
        ));

        // Back
        faces.Add(new Face(
            new Point3D(a.X, b.Y, b.Z),
            new Point3D(a.X, b.Y, a.Z),
            new Point3D(b.X, b.Y, a.Z)
        ));
        faces.Add(new Face(
            new Point3D(a.X, b.Y, b.Z),
            new Point3D(b.X, b.Y, b.Z),
            new Point3D(b.X, b.Y, a.Z)
        ));

        var mesh = new Mesh(faces);
        
        return mesh;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine("Mesh = {");

        foreach (var face in Faces)
            sb.AppendLine(face.ToString());

        sb.Append("};");

        return sb.ToString();
    }
}