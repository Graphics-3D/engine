namespace Engine.Meshes;

using Core;

public class Mesh : ITransformable<Mesh>
{
    public Face[] Faces { get; protected set; }

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

    public override string ToString()
    {
        var sb = new System.Text.StringBuilder();

        sb.AppendLine("Mesh = {");

        foreach (var face in Faces)
            sb.AppendLine(face.ToString());

        sb.Append("};");

        return sb.ToString();
    }
    
    public static Mesh Cube(float x, float y, float z, float size)
        => new Cube((x, y, z), size);
}