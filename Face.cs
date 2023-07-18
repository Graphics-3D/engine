using Engine;

public class Face
{
    private Point3D point { get; }
    public Face(float p, float q, float r)
    {
        point = new Point3D(p, q, r);
    }
    public Face RotateX(float cos, float sin) =>
        new Face(
            point.X.RotateX(cos, sin),
            point.Y.RotateX(cos, sin),
            point.Z.RotateX(cos, sin)
            
        );
    public Face RotateY(float cosa, float sina) =>
        new Face(
            point.X.RotateY(cos, sin),
            point.Y.RotateY(cos, sin),
            point.Z.RotateY(cos, sin)
        );

    public Face RotateZ(float cosa, float sina) =>
        new Face(
            point.X.RotateZ(cos, sin),
            point.Y.RotateZ(cos, sin),
            point.Z.RotateZ(cos, sin)
        );

    public Face Scale(float x, float y, float z) =>
        new Face(
            point.X.Scale(x, y, z),
            point.Y.Scale(x, y, z),
            point.Z.Scale(x, y, z)
        );

    public Face Translate(float x, float y, float z) =>
        new Face(
            point.X.Translate(x, y, z),
            point.Y.Translate(x, y, z),
            point.Z.Translate(x, y, z)
        );
        

    public override string ToString()
        => $"{{{point.X}, {point.Y}, {point.Z}}}";
}