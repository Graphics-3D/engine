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

    public static Mesh GenerateRectangle(Point3D p, Point3D q, Point3D r)
    {
        var faces = new List<Face>();
        var points = new Point3D[] { p, q, r };

        for (int i = 0; i < 9; i++)
        {
            var aIndex = i / 3;
            var bIndex = i % 3;

            if (aIndex == bIndex)
                continue;

            var square = GetSquare(points[aIndex], points[bIndex]);
            faces.AddRange(square);
        }

        var mesh = new Mesh(faces);
        
        return mesh;

        static Face[] GetSquare(Point3D a, Point3D b)
        {
            var rightFace = new Face(
                a,
                new Point3D(a.X, b.Y, (a.Z + b.Z) / 2),
                b
            );
            var leftFace = new Face(
                a,
                new Point3D(b.X, a.Y, (a.Z + b.Z) / 2),
                b
            );

            return new Face[] { leftFace, rightFace };
        }
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