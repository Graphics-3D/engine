using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Engine;

public class Mesh
{
    public Face[] Faces { get; set; }
    public Pen Pen = Pens.Black; 

    public Mesh(params Face[] faces)
        => Faces = faces;

    public Mesh(IEnumerable<Face> faces)
        => Faces = faces.ToArray();

    public Mesh Translate(float x, float y, float z)
    {
        for (int i = 0; i < Faces.Length; i++)
            Faces[i] = Faces[i].Translate(x, y, z);

        return this;
    }

    public Mesh Scale(float x, float y, float z)
    {
        for (int i = 0; i < Faces.Length; i++)
            Faces[i] = Faces[i].Scale(x, y, z);

        return this;
    }

    public Mesh RotateX(float cos, float sin)
    {
        for (int i = 0; i < Faces.Length; i++)
            Faces[i] = Faces[i].RotateX(cos, sin);

        return this;
    }

    public Mesh RotateY(float cos, float sin)
    {
        for (int i = 0; i < Faces.Length; i++)
            Faces[i] = Faces[i].RotateY(cos, sin);

        return this;
    }

    public Mesh RotateZ(float cos, float sin)
    {
        for (int i = 0; i < Faces.Length; i++)
            Faces[i] = Faces[i].RotateZ(cos, sin);

        return this;
    }

    // public static Mesh GenerateRectangle(Point3D a, Point3D b)
    // {
    //     var Faces = new List<Face>();

    //     // Front
    //     Faces.Add(new Face(
    //         new Point3D(a.X, a.Y, a.Z),
    //         new Point3D(b.X, a.Y, a.Z),
    //         new Point3D(a.X, b.Y, a.Z)
    //     ));
    //     Faces.Add(new Face(
    //         new Point3D(a.X, b.Y, a.Z),
    //         new Point3D(b.X, b.Y, a.Z),
    //         new Point3D(b.X, a.Y, a.Z)
    //     ));

    //     // Back
    //     Faces.Add(new Face(
    //         new Point3D(a.X, a.Y, b.Z),
    //         new Point3D(b.X, a.Y, b.Z),
    //         new Point3D(a.X, b.Y, b.Z)
    //     ));
    //     Faces.Add(new Face(
    //         new Point3D(a.X, b.Y, b.Z),
    //         new Point3D(b.X, b.Y, b.Z),
    //         new Point3D(b.X, a.Y, b.Z)
    //     ));

    //     // Right
    //     Faces.Add(new Face(
    //         new Point3D(b.X, a.Y, a.Z),
    //         new Point3D(b.X, b.Y, a.Z),
    //         new Point3D(b.X, a.Y, b.Z)
    //     ));
    //     Faces.Add(new Face(
    //         new Point3D(b.X, a.Y, b.Z),
    //         new Point3D(b.X, b.Y, b.Z),
    //         new Point3D(b.X, b.Y, a.Z)
    //     ));

    //     // Left
    //     Faces.Add(new Face(
    //         new Point3D(a.X, a.Y, a.Z),
    //         new Point3D(a.X, b.Y, a.Z),
    //         new Point3D(a.X, a.Y, b.Z)
    //     ));
    //     Faces.Add(new Face(
    //         new Point3D(a.X, a.Y, b.Z),
    //         new Point3D(a.X, b.Y, b.Z),
    //         new Point3D(a.X, b.Y, a.Z)
    //     ));

    //     // Top
    //     Faces.Add(new Face(
    //         new Point3D(a.X, a.Y, b.Z),
    //         new Point3D(a.X, a.Y, a.Z),
    //         new Point3D(b.X, a.Y, a.Z)
    //     ));
    //     Faces.Add(new Face(
    //         new Point3D(a.X, a.Y, b.Z),
    //         new Point3D(b.X, a.Y, b.Z),
    //         new Point3D(b.X, a.Y, a.Z)
    //     ));

    //     // Back
    //     Faces.Add(new Face(
    //         new Point3D(a.X, b.Y, b.Z),
    //         new Point3D(a.X, b.Y, a.Z),
    //         new Point3D(b.X, b.Y, a.Z)
    //     ));
    //     Faces.Add(new Face(
    //         new Point3D(a.X, b.Y, b.Z),
    //         new Point3D(b.X, b.Y, b.Z),
    //         new Point3D(b.X, b.Y, a.Z)
    //     ));

    //     var mesh = new Mesh(Faces);
        
    //     return mesh;
    // }

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