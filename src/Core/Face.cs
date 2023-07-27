namespace Engine.Core;

public class Face : ITransformable<Face>
{
    public Point3D p { get; }
    public Point3D q { get; }
    public Point3D r { get; }

    public Point3D[] Points
    {
        get => new Point3D[] { p, q, r };
    } 

    public Face(Point3D p, Point3D q, Point3D r)
    {
        this.p = p;
        this.q = q;
        this.r = r;
    }

    public Face Translate(float x, float y, float z)
        => new(
            p.Translate(x, y, z),
            q.Translate(x, y, z),
            r.Translate(x, y, z)
        );

    public Face Scale(float x, float y, float z)
        => new(
            p.Scale(x, y, z),
            q.Scale(x, y, z),
            r.Scale(x, y, z)
        );

    public Face RotateX(float cos, float sin)
        => new(
            p.RotateX(cos, sin),
            q.RotateX(cos, sin),
            r.RotateX(cos, sin)
            
        );

    public Face RotateY(float cos, float sin)
        => new(
            p.RotateY(cos, sin),
            q.RotateY(cos, sin),
            r.RotateY(cos, sin)
        );

    public Face RotateZ(float cos, float sin)
        => new(
            p.RotateZ(cos, sin),
            q.RotateZ(cos, sin),
            r.RotateZ(cos, sin)
        );

    public override string ToString()
        => $"{{{p}, {q}, {r}}}";
}